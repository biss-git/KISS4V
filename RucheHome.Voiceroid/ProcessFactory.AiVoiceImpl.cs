using AI.Talk.Editor.Api;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace RucheHome.Voiceroid
{
    partial class ProcessFactory
    {
        /// <summary>
        /// A.I.VOICE用の IProcess インタフェース実装クラス。
        /// </summary>
        private class AiVoiceImpl : ImplBase
        {
            private TtsControl _ttsControl;     // TTS APIの呼び出し用オブジェクト

            /// <summary>
            /// コンストラクタ。
            /// </summary>
            public AiVoiceImpl() : base(VoiceroidId.AiVoice, false)
            {
                _ttsControl = new TtsControl();

                {
                    // 接続先を探す
                    var availableHosts = _ttsControl.GetAvailableHostNames();

                    if (availableHosts.Length > 0)
                    {
                        // APIを初期化する
                        _ttsControl.Initialize(availableHosts[0]);
                    }
                    else
                    {
                        return;
                    }
                }
            }

            protected override Task<bool> CheckPlaying()
            {
                return Task.FromResult(_ttsControl.Status == HostStatus.Busy);
            }

            protected override Task<bool> CheckSaving()
            {
                return Task.FromResult(false);
            }

            protected override Task<string> DoGetTalkText()
            {
                return Task.FromResult(_ttsControl.Text);
            }

            protected override Task<bool> DoPlay()
            {
                return Task.Run(() =>
                {
                    try
                    {
                        Connect();
                        _ttsControl.Play();
                        return true;
                    }
                    catch
                    {
                        return false;
                    }
                });
            }

            protected override Task<FileSaveResult> DoSave(string filePath)
            {
                return Task.Run(() =>
                {
                    try
                    {
                        Connect();
                        _ttsControl.SaveAudioToFile(filePath);
                        return new FileSaveResult(true, filePath);
                    }
                    catch
                    {
                        return new FileSaveResult(false, filePath, error: "失敗しました", extraMessage: "失敗した");
                    }
                });
            }

            protected override Task<bool> DoSetTalkText(string text)
            {
                return Task.Run(() =>
                {
                    try
                    {
                        Connect();
                        _ttsControl.Text = text;
                        return true;
                    }
                    catch
                    {
                        return false;
                    }
                });
            }

            protected override Task<bool> DoStop()
            {
                return Task.Run(() =>
                {
                    try
                    {
                        Connect();
                        _ttsControl.Stop();
                        return true;
                    }
                    catch
                    {
                        return false;
                    }
                });
            }

            /// <summary>
            /// メインウィンドウタイトルであるか否かを取得する。
            /// </summary>
            /// <param name="title">タイトル。</param>
            /// <returns>
            /// メインウィンドウタイトルならば true 。そうでなければ false 。
            /// </returns>
            /// <remarks>
            /// スプラッシュウィンドウ等の判別用に用いる。
            /// </remarks>
            protected override bool IsMainWindowTitle(string title) =>
                title?.Contains(@"A.I.VOICE") == true;

            protected override Task<bool> UpdateDialogShowing()
            {
                return Task.FromResult(false);
            }

            protected override Task<bool> UpdateOnMainWindowChanged()
            {
                return Task.FromResult(true);
            }


            private void Connect()
            {
                {
                    // 接続処理
                    try
                    {
                        if (_ttsControl.Status == HostStatus.NotRunning)
                        {
                            // ホストプログラムを起動する
                            _ttsControl.StartHost();
                        }

                        // ホストプログラムに接続する
                        _ttsControl.Connect();
                    }
                    catch (Exception)
                    {
                        return;
                    }
                }
            }
        }



        ///// <summary>
        ///// A.I.VOICE用の IProcess インタフェース実装クラス。
        ///// </summary>
        //private class AiVoiceImpl : Voiceroid2ImplBase
        //{
        //    /// <summary>
        //    /// コンストラクタ。
        //    /// </summary>
        //    public AiVoiceImpl() : base(VoiceroidId.AiVoice, false)
        //    {
        //    }

        //    #region ImplBase のオーバライド

        //    /// <summary>
        //    /// メインウィンドウタイトルであるか否かを取得する。
        //    /// </summary>
        //    /// <param name="title">タイトル。</param>
        //    /// <returns>
        //    /// メインウィンドウタイトルならば true 。そうでなければ false 。
        //    /// </returns>
        //    /// <remarks>
        //    /// スプラッシュウィンドウ等の判別用に用いる。
        //    /// </remarks>
        //    protected override bool IsMainWindowTitle(string title) =>
        //        title?.Contains(@"A.I.VOICE") == true;

        //    #endregion
        //}
    }
}
