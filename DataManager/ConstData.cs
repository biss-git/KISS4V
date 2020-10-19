using DataManager.Properties;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DataManager
{
    public class ConstData
    {

        public static string DefaultFolder = Directory.GetParent(Assembly.GetExecutingAssembly().Location).ToString();

        public static string ApiPath { get { return Settings.Default.ApiPath; } }

        public static int ApiPort { get { return Settings.Default.ApiPort; } }

    }
}
