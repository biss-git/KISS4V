using DataManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VoiceroidController;

namespace VoiceRecognitionLibrary
{
    public static class VoiceCommand
    {
        static readonly DataContent DCM = DataContent.Instance;

        public static void StandartCommand(string text)
        {
            if (text.Contains("起動"))
            {
                Controller.RunByName(DCM.selectedCharaName);
            }
            if (text.Contains("再生"))
            {
                Controller.PlayByName("", DCM.selectedCharaName);
            }
            if (text.Contains("停止"))
            {
                Controller.StopByName(DCM.selectedCharaName);
            }
            if (text.Contains("保存"))
            {
                Controller.SaveByName("", DCM.selectedCharaName);
            }
        }


    }
}
