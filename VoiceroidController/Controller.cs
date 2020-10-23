using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using DataManager;
using RucheHome.Voiceroid;

namespace VoiceroidController
{
    public static class Controller
    {
        /// <summary>
        /// ボイスロイドプロセス
        /// </summary>
        public static ProcessFactory factory = new ProcessFactory();

        static readonly DataContent DCM = DataContent.Instance;


        /// <summary>
        /// 再生
        /// </summary>
        /// <param name="text"> 読み上げたいテキスト。無い場合はそのまま再生 </param>
        /// <param name="voiceroidId"> 対象のボイスロイド。無い場合は最初に見つけたやつ </param>
        /// <returns> true/false = 成功/失敗 </returns>
        public static async Task<bool> Play(string text = "", VoiceroidId? voiceroidId = null, VoiceroidCommand command = null)
        {
            await factory.Update();
            foreach( IProcess process in factory.Processes)
            {
                if ((voiceroidId == null || voiceroidId == process.Id) &&
                    (string.IsNullOrWhiteSpace(text) || await SetText(process, text)) &&
                    await process.Play())
                {
                    if (command != null)
                    {
                        command.TalkText = await process.GetTalkText();
                        command.voiceroidName = Enum.GetName(typeof(VoiceroidId), process.Id);
                    }
                    return true;
                }
            }
            return false;
        }
        public static async Task<bool> PlayByName(string text = "", string voiceroidName = "", VoiceroidCommand command = null)
        {
            return await Play(text, GetVoiceroidIdByName(voiceroidName), command);
        }


        /// <summary>
        /// 停止
        /// </summary>
        /// <param name="voiceroidId"> 対象のボイスロイド。ない場合はすべて </param>
        /// <returns> true/false = 成功/失敗 </returns>
        public static async Task<bool> Stop(VoiceroidId? voiceroidId = null)
        {
            await factory.Update();
            bool result = false;
            foreach (IProcess process in factory.Processes)
            {
                if ((voiceroidId == null || voiceroidId == process.Id) &&
                    await process.Stop())
                {
                    result = true;
                }
            }
            return result;
        }
        public static async Task<bool> StopByName(string voiceroidName = "")
        {
            return await Stop(GetVoiceroidIdByName(voiceroidName));
        }


        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="text"> 読み上げたいテキスト。無い場合はそのまま再生 </param>
        /// <param name="voiceroidId"> 対象のボイスロイド。無い場合は最初に見つけたやつ </param>
        /// <param name="filePathRequest"> 保存に使いたいファイル名。無い場合はデフォルト値 </param>
        /// <returns></returns>
        public static async Task<string> Save(string text = "", VoiceroidId? voiceroidId = null, string filePathRequest = null, VoiceroidCommand command = null)
        {
            await factory.Update();
            string filePath = string.IsNullOrWhiteSpace(filePathRequest) ?
                              DefaultFilePath :
                              filePathRequest;
            if (!FilePathUtil.IsValidPath(filePath, out string invalidLetter))
            {
                return null;
            }
            foreach (IProcess process in factory.Processes)
            {
                if ((voiceroidId == null || voiceroidId == process.Id) &&
                    (string.IsNullOrWhiteSpace(text) || await SetText(process, text)))
                {
                    var result = await process.Save(filePath);
                    if (result.IsSucceeded)
                    {
                        if (command != null)
                        {
                            command.TalkText = await process.GetTalkText();
                            command.voiceroidName = Enum.GetName(typeof(VoiceroidId), process.Id);
                        }
                        return result.FilePath;
                    }
                }
            }
            return null;
        }
        public static async Task<string> SaveByName(string text = "", string voiceroidName = "", string filePathRequest = null, VoiceroidCommand command = null)
        {
            return await Save(text, GetVoiceroidIdByName(voiceroidName), filePathRequest, command);
        }

        /// <summary>
        /// ボイスロイドを起動する
        /// </summary>
        /// <param name="voiceroidId"> 対象のボイスロイド。無い場合は最初に見つけたやつ </param>
        /// <returns></returns>
        public static async Task<bool> Run(VoiceroidId? voiceroidId = null)
        {
            await factory.Update();
            bool result = false;
            foreach (IProcess process in factory.Processes)
            {
                if ((voiceroidId == null || voiceroidId == process.Id))
                {
                    string name = Enum.GetName(typeof(VoiceroidId), process.Id);
                    string exe = DCM.GetCharaExe(name);
                    if (File.Exists(exe))
                    {
                        string error = await process.Run(exe);
                        if (error == null)
                        {
                            result = true;
                        }
                    }
                }
            }
            return result;
        }
        public static async Task<bool> RunByName(string voiceroidName = "")
        {
            return await Run(GetVoiceroidIdByName(voiceroidName));
        }

        /// <summary>
        /// ボイスロイドプロセスを終了する
        /// </summary>
        /// <param name="voiceroidId"></param>
        /// <returns></returns>
        public static async Task<bool> Exit(VoiceroidId? voiceroidId = null)
        {
            await factory.Update();
            bool result = false;
            foreach (IProcess process in factory.Processes)
            {
                if ((voiceroidId == null || voiceroidId == process.Id) &&
                    await process.Exit())
                {
                    result = true;
                }
            }
            return result;
        }
        public static async Task<bool> ExitByName(string voiceroidName = "")
        {
            return await Exit(GetVoiceroidIdByName(voiceroidName));
        }


        /// <summary>
        /// テキストの設定
        /// </summary>
        /// <param name="process"> 対象のボイスロイドプロセス </param>
        /// <param name="text"> 入力するテキスト </param>
        /// <returns></returns>
        public static async Task<bool> SetText(IProcess process, string text)
        {
            if (text == null)
            {
                return false;
            }

            if (await process.SetTalkText(text))
            {
                return true;
            }

            if (process.Id.IsVoiceroid2LikeSoftware())
            {
                // VOICEROID2ライクの場合、本体の入力欄が読み取り専用になることがある。
                // 一旦 再生→停止 の操作を行うことで解除を試みる

                if (!(await process.Play()))
                {
                    return false;
                }

                if (await process.Stop() &&
                    await process.SetTalkText(text))
                {
                    return true;
                }
            }
            return false;
        }

        public static VoiceroidId? GetVoiceroidIdByName(string voiceroidName)
        {
            foreach (VoiceroidId id in Enum.GetValues(typeof(VoiceroidId)))
            {
                string name = Enum.GetName(typeof(VoiceroidId), id);
                if(voiceroidName == name)
                {
                    return id;
                }
            }
            return null;
        }

        /// <summary>
        /// ファイルの保存パスを指定しなかったとき用のパス
        /// </summary>
        public static string DefaultFilePath
        {
            get
            {
                string folderPath = Directory.Exists(DCM.saveDirectory) ?
                                    DCM.saveDirectory :
                                    ConstData.DefaultFolder;
                return Path.Combine(folderPath, "fileName.wav");
            }
        }
    }
}
