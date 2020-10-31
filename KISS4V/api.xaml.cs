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
    }
}
