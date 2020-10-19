using System;
using System.Collections.Generic;
using System.Net;
using System.Reflection;
using System.Text;
using System.Text.Json;

namespace KissServer
{
    public static class Server
    {
        public static string Execute(HttpListenerRequest req, HttpListenerResponse res, string reqBody)
        {
            string path = ApiCommon.GetApiPath(req.RawUrl);
            if (path != "/server")
            {
                res.StatusCode = 404;
                return "";
            }
            switch (req.HttpMethod)
            {
                case "GET":
                    {
                        Assembly assembly = Assembly.GetExecutingAssembly();
                        AssemblyName asmName = assembly.GetName();
                        Version version = asmName.Version;
                        var dic = new Dictionary<string, string>();
                        dic.Add("version", version.ToString());
                        return JsonSerializer.Serialize(dic);
                    }
                // case "POST":
                //     return "";// (new CreateUserController(req, res, reqBody)).Execute();
                default:
                    res.StatusCode = 405;
                    return "";
            }
        }
    }
}
