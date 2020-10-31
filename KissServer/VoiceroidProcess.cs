using RucheHome.Voiceroid;
using System;
using System.Linq;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using VoiceroidController;

namespace KissServer
{
    public static class VoiceroidProcess
    {
        public static async Task<string> Execute(HttpListenerRequest req, HttpListenerResponse res, string reqBody)
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
                    return await GET(req, res, reqBody);
                case "POST":
                    return await POST(req, res, reqBody);
                case "OPTIONS":
                    res.StatusCode = 200;
                    return "";
                default:
                    res.StatusCode = 405;
                    return "";
            }
        }

        public static async Task<string> GET(HttpListenerRequest req, HttpListenerResponse res, string reqBody)
        {
            await Controller.factory.Update();
            string voiceroidName = null;
            if (req.QueryString.HasKeys() && req.QueryString.AllKeys.Contains("voiceroidName") && !string.IsNullOrWhiteSpace(req.QueryString["voiceroidName"]))
            {
                foreach (var process in Controller.factory.Processes)
                {
                    if (req.QueryString["voiceroidName"] == Enum.GetName(typeof(VoiceroidId), process.Id))
                    {
                        voiceroidName = req.QueryString["voiceroidName"];
                    }
                }
                if (voiceroidName == null)
                {
                    res.StatusCode = 400;
                    return "";
                }
            }

            VProcessList list = new VProcessList();
            foreach (var process in Controller.factory.Processes)
            {
                if (string.IsNullOrWhiteSpace(voiceroidName) || voiceroidName == Enum.GetName(typeof(VoiceroidId), process.Id))
                {
                    VProcess vp = new VProcess(process);
                    await vp.Update();
                    list.processes.Add(vp);
                }
            }
            return JsonSerializer.Serialize(list);// (new ReadUserController(req, res, reqBody)).Execute();
        }


        public static async Task<string> POST(HttpListenerRequest req, HttpListenerResponse res, string reqBody)
        {
            await Controller.factory.Update();
            try
            {
                VoiceroidCommand command = JsonSerializer.Deserialize<VoiceroidCommand>(reqBody);
                command.success = null;
                if (!string.IsNullOrWhiteSpace(command.voiceroidName))
                {
                    bool processExist = false;
                    foreach (var process in Controller.factory.Processes)
                    {
                        if (command.voiceroidName == Enum.GetName(typeof(VoiceroidId), process.Id))
                        {
                            processExist = true;
                        }
                    }
                    if (!processExist)
                    {
                        res.StatusCode = 400;
                        return "{voiceroidName: \"" + command.voiceroidName + "\"}";
                    }
                }
                switch (command.command)
                {
                    case "play":
                        command.success = await Controller.PlayByName(command.TalkText, command.voiceroidName, command);
                        return JsonSerializer.Serialize(command);
                    case "stop":
                        command.success = await Controller.StopByName(command.voiceroidName);
                        return JsonSerializer.Serialize(command);
                    case "save":
                        command.success = !string.IsNullOrWhiteSpace(await Controller.SaveByName(command.TalkText, command.voiceroidName, command: command));
                        return JsonSerializer.Serialize(command);
                    case "run":
                        command.success = await Controller.RunByName(command.voiceroidName);
                        return JsonSerializer.Serialize(command);
                    case "exit":
                        command.success = await Controller.ExitByName(command.voiceroidName);
                        return JsonSerializer.Serialize(command);
                    default:
                        res.StatusCode = 400;
                        return "{command: \"" + command.command + "\"}";
                }
            }
            catch (Exception e)
            {
                res.StatusCode = 400;
                return "";
            }
        }
    }
}
