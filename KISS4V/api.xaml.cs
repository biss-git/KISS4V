using System.Diagnostics;
using System.Windows.Controls;

namespace KISS4V
{
    /// <summary>
    /// api.xaml の相互作用ロジック
    /// </summary>
    public partial class api : UserControl
    {
        public api()
        {
            InitializeComponent();
            KissServer.ApiService.Start();
        }

        private void Button_Swagger(object sender, System.Windows.RoutedEventArgs e)
        {
            Process.Start("https://biss-git.github.io/KISS4V/");
        }
    }
}
