using System;
using System.IO;
using System.Threading;


namespace MathSoftCommonLib
{
    /// <summary>
    /// winform程序写系统日志的类
    /// </summary>
    public class WinformLogWrite
    {
        #region 成员变量
        static Mutex myMutex = new Mutex();
        #endregion

        #region 写日志方法
        /// <summary>
        /// 写日志方法
        /// </summary>
        /// <param name="message"></param>
        public void WriteLog(string message)
        {
            myMutex.WaitOne();
            //在当前系统路径下写日志文件log.txt
            try
            {
                StreamWriter stream = new StreamWriter(AppDomain.CurrentDomain.SetupInformation.ApplicationBase + "log.txt", true);
                stream.WriteLine(DateTime.Now + ":" + message);
                stream.Flush();
                stream.Close();
            }
            finally
            {
                myMutex.ReleaseMutex();
            }
        }
        #endregion
    }
}
