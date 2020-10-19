using DataManager;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace KissServer
{
    public static class ApiCommon
    {
        /// <summary>
        /// APIパスを取得する
        /// </summary>
        /// <param name="srcPath">URLパス</param>
        /// <returns>APIパス</returns>
        public static string GetApiPath(string srcPath)
        {
            string[] path = srcPath.Split('?');
            string condition = String.Format(@"^/{0}", ConstData.ApiPath);
            return Regex.Replace(path[0], condition, "");
        }
    }
}
