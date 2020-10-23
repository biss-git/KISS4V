using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VoiceroidController
{
    public class VoiceroidCommand
    {
        public bool? success { get; set; }
        public string command { get; set; }
        public string TalkText { get; set; }
        public string voiceroidName { get; set; }
    }
}
