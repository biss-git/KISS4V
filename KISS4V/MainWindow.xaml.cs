using Microsoft.Win32.SafeHandles;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
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
using DataManager;

namespace KISS4V
{
    /// <summary>
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class MainWindow : MahApps.Metro.Controls.MetroWindow
    {
        readonly DataContent DCM = DataContent.Instance;

        public MainWindow()
        {
            InitializeComponent();
            DataContext = DCM;
        }

        private void MenuItem_ShowWindow(object sender, RoutedEventArgs e)
        {
            this.Visibility = Visibility.Visible;
            this.ShowInTaskbar = true;
        }

        private void MenuItem_Shoutdown(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void MetroWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = true;
            this.Visibility = Visibility.Hidden;
            this.ShowInTaskbar = false;
        }

        private void MenuItem_Directory(object sender, RoutedEventArgs e)
        {
            var directory = Directory.GetParent(Assembly.GetEntryAssembly().Location);
            System.Diagnostics.Process.Start("explorer.exe", directory.FullName);
        }

        private async void MainTab_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (mainTab.SelectedContent == null)
            {
                return;
            }

            string tabImage1 = "./image/common.png";
            string tabImage2 = "./image/talk.png";
            string tabImage3 = "./image/listen.png";
            string tabImage4 = "./image/api.png";
            string tabFont1 = "#a0a0a0";
            string tabFont2 = "#a0a0a0";
            string tabFont3 = "#a0a0a0";
            string tabFont4 = "#a0a0a0";
            await Task.Delay(50);
            if (mainTab.SelectedContent.Equals(common))
            {
                tabImage1 = "./image/common_active.png";
                tabFont1 = "#890725";
            }
            else if (mainTab.SelectedContent.Equals(talk))
            {
                tabImage2 = "./image/talk_active.png";
                tabFont2 = "#890725";
            }
            else if (mainTab.SelectedContent.Equals(listen))
            {
                tabImage3 = "./image/listen_active.png";
                tabFont3 = "#890725";
            }
            else if (mainTab.SelectedContent.Equals(api))
            {
                tabImage4 = "./image/api_active.png";
                tabFont4 = "#890725";
            }
            else
            {
                return;
            }
            DCM.tabFontColor1 = tabFont1;
            DCM.tabFontColor2 = tabFont2;
            DCM.tabFontColor3 = tabFont3;
            DCM.tabFontColor4 = tabFont4;
            DCM.tabImage1 = tabImage1;
            DCM.tabImage2 = tabImage2;
            DCM.tabImage3 = tabImage3;
            DCM.tabImage4 = tabImage4;
        }

        ~MainWindow()
        {
            KissServer.ApiService.Stop();
        }
    }
}
