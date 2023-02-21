using System;
using System.IO;
using System.Net;
using System.Text;


namespace MathSoftCommonLib
{
    #region  http协议帮助类
    /// <summary>
    /// http协议帮助类
    /// 2019年8月26日
    /// 作者:马良
    /// </summary>
    public class HttpHelpler
    {
        #region Url地址
        /// <summary>
        /// Url地址
        /// </summary>
        public string Url { get; set; }
        #endregion

        #region  数据
        /// <summary>
        /// 数据 类似于UserName=maliang&UserPWD=123456
        /// </summary>
        public string Data { get; set; }
        #endregion

        #region 构造函数
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="url"></param>
        public HttpHelpler(string url)
        {
            this.Url = url;
        }
        #endregion

        #region 构造函数
        /// <summary>
        /// URL地址 这个地址常年有效
        /// </summary>
        /// <param name="url">类似于http://139.224.193.145:5437/Admin/Login/ValidUser</param>
        /// <param name="data">类似于UserName=maliang&UserPWD=123456</param>
        public HttpHelpler(string url, string data)
        {
            this.Url = url;
            this.Data = data;
        }
        #endregion

        #region   执行Post操作
        /// <summary>
        /// 执行Post操作
        /// </summary>
        /// <returns></returns>
        public string ExecutePost()
        {
            try
            {
                HttpWebRequest myRequest = HttpWebRequest.Create(Url) as HttpWebRequest;
                myRequest.Method = "POST";
                //myRequest.ContentType = "application/json;charset=UTF-8";
                myRequest.ContentType = "application/x-www-form-urlencoded;charset=UTF-8";

                myRequest.ReadWriteTimeout = 30000;

                byte[] data = Encoding.UTF8.GetBytes(Data);
                myRequest.ContentLength = data.Length;
                Stream myStream = myRequest.GetRequestStream();
                myStream.Write(data, 0, data.Length);
                myStream.Close();

                HttpWebResponse myResponse = myRequest.GetResponse() as HttpWebResponse;
                StreamReader sr = new StreamReader(myResponse.GetResponseStream(), Encoding.UTF8);
                string res = sr.ReadToEnd();
                return res;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }
        #endregion

        #region 执行Get操作
        /// <summary>
        /// 执行Get操作
        /// </summary>
        /// <returns></returns>
        public string ExecuteGet()
        {
            HttpWebRequest myRequest = HttpWebRequest.Create(Url) as HttpWebRequest;
            myRequest.Method = "GET";
            myRequest.ReadWriteTimeout = 30000;

            HttpWebResponse myResponse = null;
            try
            {
                myResponse = myRequest.GetResponse() as HttpWebResponse;
                StreamReader sr = new StreamReader(myResponse.GetResponseStream(), Encoding.UTF8);
                string res = sr.ReadToEnd();
                return res;
            }
            catch (WebException ex)
            {
                myResponse = ex.Response as HttpWebResponse;
                using (Stream errData = myResponse.GetResponseStream())
                {
                    using (StreamReader sr = new StreamReader(errData))
                    {
                        string res = sr.ReadToEnd();
                        return res;
                    }
                }
            }
        }
        #endregion
    }
    #endregion
}
