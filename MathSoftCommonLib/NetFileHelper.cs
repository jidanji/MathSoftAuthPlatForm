using System;
using System.IO;

namespace MathSoftCommonLib
{
    public static class NetFileHelper
    {
        #region 写文件操作
        /// <summary>
        /// 写文件操作(如有没有会创建，如果有会覆盖)
        /// </summary>
        /// <param name="MapPath"></param>
        /// <param name="text"></param>
        public static void FileWrtieText(string MapPath, string text)
        {
            DateTime time = DateTime.Now;
            string path = AppDomain.CurrentDomain.BaseDirectory;
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);

            string fileFullPath = path + MapPath;

            StreamWriter sw;
            sw = File.CreateText(fileFullPath);
            sw.WriteLine(text);
            sw.Close();
        }
        #endregion

        #region 附加文件操作
        /// <summary>
        /// 附加文件操作
        /// </summary>
        /// <param name="MapPath"></param>
        /// <param name="text"></param>
        public static void AppendWrtieText(string MapPath, string text)
        {
            DateTime time = DateTime.Now;
            string path = AppDomain.CurrentDomain.BaseDirectory;
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);

            string fileFullPath = path + MapPath;

            StreamWriter sw;
            if (!File.Exists(fileFullPath))
            {
                sw = File.CreateText(fileFullPath);
            }
            else
            {
                sw = File.AppendText(fileFullPath);
            }
            sw.WriteLine(text);
            sw.Close();
        }
        #endregion
    }


}
