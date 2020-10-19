using DataManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using VoiceroidController;
using MSAPI = Microsoft.WindowsAPICodePack;

namespace KISS4V
{
    /// <summary>
    /// talk.xaml の相互作用ロジック
    /// </summary>
    public partial class talk : UserControl
    {
        readonly DataContent DCM = DataContent.Instance;

        public talk()
        {
            InitializeComponent();
            DataContext = DCM;


            for(int i = 0; i < charaCombo.Items.Count; i++)
            {
                var item = charaCombo.Items[i] as ComboBoxItem;
                if (item.DataContext.ToString() == DCM.selectedCharaName)
                {
                    charaCombo.SelectedIndex = i;
                }
            }
        }

        private async void Button_Play(object sender, RoutedEventArgs e)
        {
            if (await Controller.PlayByName("",DCM.selectedCharaName))
            {
                DCM.stateText = "再生に成功しました。";
            }
            else
            {
                DCM.stateText = "再生に失敗しました。";
            }
        }

        private async void Button_Stop(object sender, RoutedEventArgs e)
        {
            if (await Controller.StopByName(voiceroidName:DCM.selectedCharaName))
            {
                DCM.stateText = "停止に成功しました。";
            }
            else
            {
                DCM.stateText = "停止に失敗しました。";
            }
        }

        private async void Button_Save(object sender, RoutedEventArgs e)
        {
            string filePath = await Controller.SaveByName(voiceroidName: DCM.selectedCharaName);
            if (!string.IsNullOrWhiteSpace(filePath))
            {
                DCM.stateText = "保存に成功しました。 path : " + filePath;
            }
            else
            {
                DCM.stateText = "保存に失敗しました。";
            }
        }

        private async void Button_Run(object sender, RoutedEventArgs e)
        {
            if (await Controller.RunByName(voiceroidName: DCM.selectedCharaName))
            {
                DCM.stateText = "起動に成功しました。";
            }
            else
            {
                DCM.stateText = "起動に失敗しました。";
            }
        }

        private async void Button_Exit(object sender, RoutedEventArgs e)
        {
            if (await Controller.ExitByName(voiceroidName: DCM.selectedCharaName))
            {
                DCM.stateText = "終了に成功しました。";
            }
            else
            {
                DCM.stateText = "終了に失敗しました。";
            }
        }

        private void ComboBox_VoiceroidName(object sender, SelectionChangedEventArgs e)
        {
            if (e.RemovedItems.Count == 0) { return; }
            var chara = (e.AddedItems[0] as ComboBoxItem);
            DCM.selectedCharaName = chara.DataContext.ToString();
        }

        private void Button_SaveDirectory(object sender, RoutedEventArgs e)
        {
            var dlg = new MSAPI::Dialogs.CommonOpenFileDialog();
            dlg.IsFolderPicker = true;
            dlg.Title = "音声ファイルの保存先フォルダを選択してください";
            dlg.InitialDirectory = DCM.saveDirectory;

            if (dlg.ShowDialog() == MSAPI::Dialogs.CommonFileDialogResult.Ok)
            {
                DCM.saveDirectory = dlg.FileName;
            }
        }

        private void Button_SelectExe(object sender, RoutedEventArgs e)
        {
            string name = (sender as Button).DataContext.ToString();
            var dlg = new MSAPI::Dialogs.CommonOpenFileDialog();
            dlg.Title = "exeファイルの場所を選択してください";
            dlg.InitialDirectory = DCM.GetCharaExe(name);

            if (dlg.ShowDialog() == MSAPI::Dialogs.CommonFileDialogResult.Ok)
            {
                DCM.SetCharaExe(name, dlg.FileName);
            }
        }

    }
}
