using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace KissServer
{
    public static class VoiceroidProcess
    {
        public static string Execute(HttpListenerRequest req, HttpListenerResponse res, string reqBody)
        {
            string path = ApiCommon.GetApiPath(req.RawUrl);
            if (path != "/voiceroid/process")
            {
                res.StatusCode = 404;
                return "";
            }
            switch (req.HttpMethod)
            {
                case "GET":
                    return "";// (new ReadUserController(req, res, reqBody)).Execute();
                case "POST":
                    return "";// (new CreateUserController(req, res, reqBody)).Execute();
                default:
                    res.StatusCode = 405;
                    return "";
            }
        }
    }
}
