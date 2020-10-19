using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Diagnostics;

namespace KISS4V
{
    /// <summary>
    /// App.xaml の相互作用ロジック
    /// </summary>
    public partial class App : Application
    {
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            string myProcessName = Process.GetCurrentProcess().ProcessName;
            if(Process.GetProcessesByName(myProcessName).Length > 1)
            {
                MessageBox.Show(myProcessName + " はすでに起動しています。\n多重起動はできません。");
                this.Shutdown();
            }
        }
    }
}
