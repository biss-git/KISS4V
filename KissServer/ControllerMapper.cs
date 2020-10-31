using System;
using System.Collections.Specialized;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace KissServer
{

    class ControllerMapper
    {
        private const string CONTENT_TYPE_JSON = "application/json";

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public ControllerMapper()
        {
        }

        /// <summary>
        /// 実行する
        /// </summary>
        /// <param name="req">リクエスト情報</param>
        /// <param name="res">レスポンス情報</param>
        public async Task Execute(HttpListenerRequest req, HttpListenerResponse res)
        {
            StreamReader reader = null;
            StreamWriter writer = null;
            string reqBody = null;
            string resBoby = null;

            try
            {
                res.StatusCode = (int)HttpStatusCode.OK;
                res.ContentType = CONTENT_TYPE_JSON;
                res.ContentEncoding = Encoding.UTF8;

                reader = new StreamReader(req.InputStream);
                writer = new StreamWriter(res.OutputStream);
                reqBody = reader.ReadToEnd();

                LogStart(req, reqBody);
                resBoby = await ExecuteController(req, res, reqBody);
            }
            catch (Exception ex)
            {
                /* ～エラー処理～ */
            }
            finally
            {
                try
                {
                    writer.Write(resBoby);
                    writer.Flush();

                    if (null != reader)
                    {
                        reader.Close();
                    }
                    if (null != writer)
                    {
                        writer.Close();
                    }
                    LogEnd(res, resBoby);
                }
                catch (Exception clsEx)
                {
                    // log.Error(clsEx.ToString());
                }
            }
        }

        /// <summary>
        /// リクエストログを出力する
        /// </summary>
        /// <param name="req">リクエスト情報</param>
        /// <param name="body">リクエストボディ文字列</param>
        private void LogStart(HttpListenerRequest req, string body)
        {
        }

        /// <summary>
        /// レスポンスログを出力する
        /// </summary>
        /// <param name="res">レスポンス情報</param>
        /// <param name="body">レスポンスボディ文字列</param>
        private void LogEnd(HttpListenerResponse res, string body)
        {
        }

        /// <summary>
        /// Name-Value文字列を取得する
        /// </summary>
        /// <param name="nvc">nvc</param>
        /// <returns>文字列</returns>
        private string ToNameValueString(NameValueCollection nvc)
        {
            return String.Join(", ", Array.ConvertAll(nvc.AllKeys, key => String.Format("{0}:{1}", key, nvc[key])));
        }

        /// <summary>
        /// APIコントローラを実行する
        /// </summary>
        /// <param name="req">リクエスト情報</param>
        /// <param name="res">レスポンス情報</param>
        /// <param name="reqBody">リクエストボディ</param>
        /// <returns>レスポンス文字列</returns>
        private async Task<string> ExecuteController(HttpListenerRequest req, HttpListenerResponse res, string reqBody)
        {
            string path = ApiCommon.GetApiPath(req.RawUrl);

            if (path.StartsWith("/voiceroid/process"))
            {
                return await VoiceroidProcess.Execute(req, res, reqBody);
            }
            else if (path.StartsWith("/voiceroid/task"))
            {

            }
            else if (path.StartsWith("/server"))
            {
                return Server.Execute(req, res, reqBody);
            }
            else
            {
                res.StatusCode = 404;
            }
            /*
            if ("/user/".Equals(path))
            {
                switch (req.HttpMethod)
                {
                    case "GET":
                        return "";// (new ReadUserController(req, res, reqBody)).Execute();
                    case "POST":
                        return "";// (new CreateUserController(req, res, reqBody)).Execute();
                    case "PUT":
                        return "";// (new UpdateUserController(req, res, reqBody)).Execute();
                    case "DELETE":
                        return "";// (new DeleteUserController(req, res, reqBody)).Execute();
                }
            }
            if ("/users/".Equals(path) && "GET".Equals("GET"))
            {
                return "";// (new ReadUsersController(req, res, reqBody)).Execute();
            }
            */
            return "";
        }
    }

}
