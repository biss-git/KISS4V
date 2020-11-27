using DataManager.Properties;
using System.ComponentModel;

namespace DataManager
{
    public class DataContent : INotifyPropertyChanged
    {
        static public DataContent Instance { get; } = new DataContent();
        private DataContent() { }



        /**
         * 
         * 画面全体
         * 
         */

        public string tabImage1 { get { return tabImage1_; } set { tabImage1_ = value; OnPropertyChanged(nameof(tabImage1)); } }
        string tabImage1_ = "./image/common_active.png";
        public string tabImage2 { get { return tabImage2_; } set { tabImage2_ = value; OnPropertyChanged(nameof(tabImage2)); } }
        string tabImage2_ = "./image/talk.png";
        public string tabImage3 { get { return tabImage3_; } set { tabImage3_ = value; OnPropertyChanged(nameof(tabImage3)); } }
        string tabImage3_ = "./image/listen.png";
        public string tabImage4 { get { return tabImage4_; } set { tabImage4_ = value; OnPropertyChanged(nameof(tabImage4)); } }
        string tabImage4_ = "./image/api.png";
        public string tabFontColor1 { get { return tabFontColor1_; } set { tabFontColor1_ = value; OnPropertyChanged(nameof(tabFontColor1)); } }
        string tabFontColor1_ = "#a0a0a0";
        public string tabFontColor2 { get { return tabFontColor2_; } set { tabFontColor2_ = value; OnPropertyChanged(nameof(tabFontColor2)); } }
        string tabFontColor2_ = "#a0a0a0";
        public string tabFontColor3 { get { return tabFontColor3_; } set { tabFontColor3_ = value; OnPropertyChanged(nameof(tabFontColor3)); } }
        string tabFontColor3_ = "#a0a0a0";
        public string tabFontColor4 { get { return tabFontColor4_; } set { tabFontColor4_ = value; OnPropertyChanged(nameof(tabFontColor4)); } }
        string tabFontColor4_ = "#a0a0a0";


        public string stateText { get { return stateText_; } set { stateText_ = value; OnPropertyChanged(nameof(stateText)); } }
        string stateText_ = "";

        public string vrText { get { return vrText_; } set { vrText_ = value; OnPropertyChanged(nameof(vrText)); } }
        string vrText_ = "";

        public string rootUrl { get { return rootUrl_; } set { rootUrl_ = value; OnPropertyChanged(nameof(rootUrl)); } }
        string rootUrl_ = "";




        /**
         * 
         * 共通・設定 common
         * 
         */



        public bool ShowSettingWindow { get { return Settings.Default.ShowSettingWindow; } set { Settings.Default.ShowSettingWindow = value; OnPropertyChanged(nameof(ShowSettingWindow)); Save(); } }

        public bool FirstStart { get { return Settings.Default.FirstStart; } set { Settings.Default.FirstStart = value; OnPropertyChanged(nameof(FirstStart)); Save(); } }




        /**
         * 
         *  ボイスロイド関係
         * 
         */


        public string saveDirectory { get { return Settings.Default.SaveDirectory; } set { Settings.Default.SaveDirectory = value; OnPropertyChanged(nameof(saveDirectory)); Save(); } }


        public string selectedCharaName { get { return Settings.Default.SelectedCharaName; } set { Settings.Default.SelectedCharaName = value; OnPropertyChanged(nameof(selectedCharaName)); Save(); } }
        public int FileNameRuleIndex { get { return Settings.Default.FileNameRuleIndex; } set { Settings.Default.FileNameRuleIndex = value; OnPropertyChanged(nameof(FileNameRuleIndex)); Save(); } }

        public string TestTextInput { get { return testTextInput; } set { testTextInput = value; OnPropertyChanged(nameof(selectedCharaName)); } }
        string testTextInput = "";

        public string Voiceroid2Exe { get { return Settings.Default.Voiceroid2Exe; } set { Settings.Default.Voiceroid2Exe = value; OnPropertyChanged(nameof(Voiceroid2Exe)); Save(); } }
        public string GynoidTalkExe { get { return Settings.Default.GynoidTalkExe; } set { Settings.Default.GynoidTalkExe = value; OnPropertyChanged(nameof(GynoidTalkExe)); Save(); } }
        public string YukariExExe { get { return Settings.Default.YukariExExe; } set { Settings.Default.YukariExExe = value; OnPropertyChanged(nameof(YukariExExe)); Save(); } }
        public string MakiExExe { get { return Settings.Default.MakiExExe; } set { Settings.Default.MakiExExe = value; OnPropertyChanged(nameof(MakiExExe)); Save(); } }
        public string ZunkoExExe { get { return Settings.Default.ZunkoExExe; } set { Settings.Default.ZunkoExExe = value; OnPropertyChanged(nameof(ZunkoExExe)); Save(); } }
        public string KiritanExExe { get { return Settings.Default.KiritanExExe; } set { Settings.Default.KiritanExExe = value; OnPropertyChanged(nameof(KiritanExExe)); Save(); } }
        public string AkaneExe { get { return Settings.Default.AkaneExe; } set { Settings.Default.AkaneExe = value; OnPropertyChanged(nameof(AkaneExe)); Save(); } }
        public string AoiExe { get { return Settings.Default.AoiExe; } set { Settings.Default.AoiExe = value; OnPropertyChanged(nameof(AoiExe)); Save(); } }
        public string AiExExe { get { return Settings.Default.AiExExe; } set { Settings.Default.AiExExe = value; OnPropertyChanged(nameof(AiExExe)); Save(); } }
        public string ShoutaExExe { get { return Settings.Default.ShoutaExExe; } set { Settings.Default.ShoutaExExe = value; OnPropertyChanged(nameof(ShoutaExExe)); Save(); } }
        public string SeikaExExe { get { return Settings.Default.SeikaExExe; } set { Settings.Default.SeikaExExe = value; OnPropertyChanged(nameof(SeikaExExe)); Save(); } }
        public string KouExExe { get { return Settings.Default.KouExExe; } set { Settings.Default.KouExExe = value; OnPropertyChanged(nameof(KouExExe)); Save(); } }
        public string GalacoTalkExe { get { return Settings.Default.GalacoTalkExe; } set { Settings.Default.GalacoTalkExe = value; OnPropertyChanged(nameof(GalacoTalkExe)); Save(); } }
        public string UnaTalkExExe { get { return Settings.Default.UnaTalkExExe; } set { Settings.Default.UnaTalkExExe = value; OnPropertyChanged(nameof(UnaTalkExExe)); Save(); } }

        public string GetCharaExe(string chara)
        {
            switch (chara)
            {
                case "Voiceroid2": return Voiceroid2Exe;
                case "GynoidTalk": return GynoidTalkExe;
                case "YukariEx": return YukariExExe;
                case "MakiEx": return MakiExExe;
                case "ZunkoEx": return ZunkoExExe;
                case "KiritanEx": return KiritanExExe;
                case "Akane": return AkaneExe;
                case "Aoi": return AoiExe;
                case "AiEx": return AiExExe;
                case "ShoutaEx": return ShoutaExExe;
                case "SeikaEx": return SeikaExExe;
                case "KouEx": return KouExExe;
                case "GalacoTalk": return GalacoTalkExe;
                case "UnaTalkEx": return UnaTalkExExe;
            }
            return "";
        }
        public void SetCharaExe(string chara, string exe)
        {
            switch (chara)
            {
                case "Voiceroid2": Voiceroid2Exe = exe; return;
                case "GynoidTalk": GynoidTalkExe = exe; return;
                case "YukariEx": YukariExExe = exe; return;
                case "MakiEx": MakiExExe = exe; return;
                case "ZunkoEx": ZunkoExExe = exe; return;
                case "KiritanEx": KiritanExExe = exe; return;
                case "Akane": AkaneExe = exe; return;
                case "Aoi": AoiExe = exe; return;
                case "AiEx": AiExExe = exe; return;
                case "ShoutaEx": ShoutaExExe = exe; return;
                case "SeikaEx": SeikaExExe = exe; return;
                case "KouEx": KouExExe = exe; return;
                case "GalacoTalk": GalacoTalkExe = exe; return;
                case "UnaTalkEx": UnaTalkExExe = exe; return;
            }
        }





        /**
         * 
         * 音声認識
         * 
         */

        public bool ListenActive { get { return Settings.Default.ListenActive; } set { Settings.Default.ListenActive = value; OnPropertyChanged(nameof(ListenActive)); Save(); } }




        /// <summary>
        /// ユーザー設定の保存
        /// </summary>
        public static void Save()
        {
            Settings.Default.Save();
        }




        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// 変数の更新
        /// </summary>
        /// <param name="name"></param>
        protected void OnPropertyChanged(string name)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(name));
            }
        }
    }
}
