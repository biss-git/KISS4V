using DataManager;
using System;

namespace VoiceRecognitionLibrary
{
    public static class VoiceRecognitionManager
    {
        readonly static DataContent DCM = DataContent.Instance;

        public static void InitializeSystemSpeech()
        {
            try
            {
                InitializeSystemSpeech_Sub();
                SystemSpeech.RecognizeAsync(true); // 音声認識開始
            }
            catch (Exception e)
            {
                DCM.vrText += "\n 初期化に失敗しました。マイクが接続されているか確認してください。 : " + e.Message;
            }
        }

        public static void Start()
        {
            try
            {
                SystemSpeech.RecognizeAsync(true); // 音声認識開始
            }
            catch (Exception e){}
        }

        public static void Stop()
        {
            try
            {
                SystemSpeech.RecognizeAsyncStop(); // 音声認識開始
            }
            catch (Exception e) { }
        }


        public static void InitializeSystemSpeech_Sub()
        {
            SystemSpeech.CreateEngine();

            SystemSpeech.SpeechRecognitionRejectedEvent = () =>
            {
                DCM.vrText += "\n" + "認識できません。";
            };

            SystemSpeech.SpeechRecognizedEvent = (grammerName, confidence, text, words) =>
            {
                DCM.vrText += "\n" + "確定：" + grammerName + "(" + confidence + ")";
                DCM.vrText += "\n" + "　　　" + text;
                DCM.vrText += "\n" + "　　　 [ ";
                DCM.vrText += words[0];
                for (int i = 1; i < words.Length; i++)
                {
                    DCM.vrText += ", ";
                    DCM.vrText += words[i];
                }
                DCM.vrText += " ] ";
                if(grammerName == "command")
                {
                    VoiceCommand.StandartCommand(text);
                }
            };

            SystemSpeech.SpeechHypothesizedEvent = (grammerName, text, confidence) =>
            {
                //leftText.Text += "\n" + "候補：" + e.Result.Grammar.Name + " " + e.Result.Text + "(" + e.Result.Confidence + ")";
            };

            SystemSpeech.SpeechRecognizeCompletedEvent = (cancelled) =>
            {
                if (cancelled)
                {
                    DCM.vrText += "\n" + "キャンセルされました。";
                }

                DCM.vrText += "\n" + "認識終了";
            };


            SystemSpeech.AddGrammar();
        }

    }


}
