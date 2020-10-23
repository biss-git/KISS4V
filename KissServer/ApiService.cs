using DataManager;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace KissServer
{
    public static class ApiService
    {
        private static HttpListener listener;
        private static ControllerMapper mapper = new ControllerMapper();
        private static DataContent DCM = DataContent.Instance;

        /// <summary>
        /// APIサービスを起動する
        /// </summary>
        public static void Start()
        {
            listener = new HttpListener();
            try
            {
                string rootUrl = String.Format("http://+:{0}/{1}/", ConstData.ApiPort, ConstData.ApiPath);
                //DCM.rootUrl = "http://*:" + ConstData.ApiPort.ToString() + "/";
                DCM.rootUrl = String.Format("http://127.0.0.1:{0}/{1}/", ConstData.ApiPort, ConstData.ApiPath);

                listener.Prefixes.Clear();
                listener.Prefixes.Add(rootUrl);
                listener.Start();
                Task.Run(tick);
            }
            catch (Exception error)
            {
                DCM.stateText = "サーバーの立ち上げに失敗しました。" + error.Message;
            }
        }

        public static void Stop()
        {
            try
            {
                listener?.Stop();
            }
            catch (Exception error)
            {
            }
        }

        private static void tick()
        {
            while (listener.IsListening)
            {
                try
                {
                    IAsyncResult result = listener.BeginGetContext(OnRequested, listener);
                    result.AsyncWaitHandle.WaitOne();
                }
                catch (Exception e)
                {
                }

            }
        }



        /// <summary>
        /// リクエスト時の処理を実行する
        /// </summary>
        /// <param name="result">結果</param>
        private static async void OnRequested(IAsyncResult result)
        {
            HttpListener clsListener = (HttpListener)result.AsyncState;
            if (!clsListener.IsListening)
            {
                return;
            }

            HttpListenerContext context = clsListener.EndGetContext(result);
            HttpListenerRequest req = context.Request;
            HttpListenerResponse res = context.Response;
            res.AddHeader("Access-Control-Allow-Origin", "*"); // どこからのリクエストでも受け取る
            res.AddHeader("Access-Control-Allow-Headers", "*"); // どこからのリクエストでも受け取る
            try
            {
                await mapper.Execute(req, res);
            }
            catch (Exception ex)
            {
                // log.Error(ex.ToString());
            }
            finally
            {
                try
                {
                    if (null != res)
                    {
                        res.Close();
                    }
                }
                catch (Exception clsEx)
                {
                    // log.Error(clsEx.ToString());
                }
            }
        }

    }
}
