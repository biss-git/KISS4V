using DataManager;
using System.Threading.Tasks;
using System.Windows.Controls;
using VoiceRecognitionLibrary;

namespace KISS4V
{
    /// <summary>
    /// listen.xaml の相互作用ロジック
    /// </summary>
    public partial class listen : UserControl
    {
        readonly DataContent DCM = DataContent.Instance;

        public listen()
        {
            InitializeComponent();
            VoiceRecognitionManager.InitializeSystemSpeech();
            DataContext = DCM;
            Task.Run(async () =>
            {
                VoiceRecognitionManager.Start();
                await Task.Delay(1000);
                CheckBox_ListenActive(null, null);
            });

            for (int i = 0; i < charaCombo.Items.Count; i++)
            {
                var item = charaCombo.Items[i] as ComboBoxItem;
                if (item.DataContext.ToString() == DCM.selectedCharaName)
                {
                    charaCombo.SelectedIndex = i;
                }
            }
        }

        private void ComboBox_VoiceroidName(object sender, SelectionChangedEventArgs e)
        {
            if (e.RemovedItems.Count == 0) { return; }
            var chara = (e.AddedItems[0] as ComboBoxItem);
            DCM.selectedCharaName = chara.DataContext.ToString();
        }

        private void CheckBox_ListenActive(object sender, System.Windows.RoutedEventArgs e)
        {

            if (DCM.ListenActive)
            {
                VoiceRecognitionManager.Start();
            }
            else
            {
                VoiceRecognitionManager.Stop();
            }

        }
    }
}
