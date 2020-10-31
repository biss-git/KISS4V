using System.Windows.Controls;
using VoiceRecognitionLibrary;

namespace KISS4V
{
    /// <summary>
    /// listen.xaml の相互作用ロジック
    /// </summary>
    public partial class listen : UserControl
    {
        public listen()
        {
            InitializeComponent();
            VoiceRecognitionManager.InitializeSystemSpeech();
        }
    }
}
