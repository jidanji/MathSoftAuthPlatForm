using System.Web;

namespace MathSoftCommonLib
{

    public static class URLHelper
    {
        #region 获取当前的根域名(127.0.0.1)
        /// <summary>
        /// 获取当前的根域名(127.0.0.1)
        /// </summary>
        /// <param name="Request"></param>
        /// <returns></returns>
        public static string GetBaseUrl(HttpRequest Request)
        {
            string url = Request.Url.Host;
            return url;
        }
        #endregion

        #region 获取当前的端口号(80)
        /// <summary>
        /// 获取当前的端口号(80)
        /// </summary>
        /// <param name="Request"></param>
        /// <returns></returns>
        public static string GetBasePort(HttpRequest Request)
        {

            string Port = HttpContext.Current.Request.Url.Port.ToString();
            return Port;
        }
        #endregion

        #region 获取域名+端口号 （http://172.0.0.1:8080）
        /// <summary>
        /// 获取域名+端口号 （http://172.0.0.1:8080）
        /// </summary>
        /// <param name="Request"></param>
        /// <returns></returns>
        public static string GetBaseUrlAndPort(HttpRequest Request)
        {
            var url = GetBaseUrl(Request);
            var Port = GetBasePort(Request);
            string ImgURL = string.Format("http://{0}:{1}", url, Port);

            return ImgURL;
        }
        #endregion
    }
}
