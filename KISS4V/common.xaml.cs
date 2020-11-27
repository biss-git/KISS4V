using System.Windows.Controls;
using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using DataManager;

namespace KISS4V
{
    /// <summary>
    /// common.xaml の相互作用ロジック
    /// </summary>
    public partial class common : UserControl
    {

        readonly DataContent DCM = DataContent.Instance;


        private string aplTitle = null; // アプリ名
        private string exeFile = null;  // exeファイル名
        private string linkFile = null;  // リンク名


        public common()
        {
            InitializeComponent();
        }

        private void Button_OpenStartUp(object sender, System.Windows.RoutedEventArgs e)
        {
            //string directory = Environment.GetFolderPath(Environment.SpecialFolder.StartMenu);
            string directory = Environment.GetFolderPath(Environment.SpecialFolder.Startup);
            Process.Start("explorer.exe", directory);
        }




        private void UserControl_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            // プロジェクト＞プロパティ＞アセンブリ情報　で指定した「タイトル」を取得
            var assembly = Assembly.GetExecutingAssembly();
            var attribute = Attribute.GetCustomAttribute(
              assembly,
              typeof(AssemblyTitleAttribute)
            ) as AssemblyTitleAttribute;
            aplTitle = attribute.Title;

            // 自身のexeファイル名を取得
            exeFile = Assembly.GetExecutingAssembly().Location.ToString();

            // ショートカットのリンク名
            String sMnu = Environment.GetFolderPath(Environment.SpecialFolder.Startup);
            linkFile = Path.Combine(sMnu, aplTitle + ".lnk");

            if (DCM.FirstStart)
            {
                MakeStartUp();
                DCM.FirstStart = false;
            }
            else if (File.Exists(linkFile))
            {
                // リンクは念のため作り直しておく
                DeleteStartUp();
                MakeStartUp();
            }

            // スタートアップ有無
            StartUp.IsChecked = File.Exists(linkFile);
        }







        private void CheckBox_StartUp(object sender, System.Windows.RoutedEventArgs e)
        {
            string filepath = Assembly.GetExecutingAssembly().Location.ToString();

            if (StartUp.IsChecked == true)
            {
                MakeStartUp();
            }
            else
            {
                DeleteStartUp();
            }

            // スタートアップ有無
            StartUp.IsChecked = File.Exists(linkFile);
        }


        private void MakeStartUp()
        {
            // スタートアップフォルダにショートカット作成
            try
            {
                string shortcutPath = linkFile;
                string targetPath = exeFile;

                // WshShellを作成
                Type t = Type.GetTypeFromCLSID(new Guid("72C24DD5-D70A-438B-8A42-98424B88AFB8"));
                dynamic shell = Activator.CreateInstance(t);

                //WshShortcutを作成
                var shortcut = shell.CreateShortcut(shortcutPath);
                shortcut.TargetPath = targetPath;
                // 引数
                //shortcut.Arguments = "/a /b /c";
                // コメント
                shortcut.Description = "自動生成されたショートカットです。";

                //ショートカットを作成
                shortcut.Save();

                //後始末
                System.Runtime.InteropServices.Marshal.FinalReleaseComObject(shortcut);
                System.Runtime.InteropServices.Marshal.FinalReleaseComObject(shell);

            }
            catch (Exception ex)
            {
            }
        }

        /// <summary>
        /// スタートアップからリンクを削除
        /// </summary>
        private void DeleteStartUp()
        {
            if (File.Exists(linkFile))
            {
                try
                {
                    File.Delete(linkFile);
                }
                catch (IOException ex)
                {
                }
            }
        }

    }
}
