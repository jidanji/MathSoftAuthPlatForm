using System;
using System.Collections.Generic;
using System.Web;

namespace MathSoftCommonLib
{
    #region 网络爬虫相关类
    /// <summary>
    /// 网络爬虫相关类
    /// </summary>
    public class HTMLHelper
    {
        #region URL解码
        /// <summary>
        /// URL解码
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string UrlDecode(String input)
        {
            return HttpUtility.UrlDecode(input);
        }
        #endregion

        #region URL编码
        /// <summary>
        /// URL编码
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string UrlEncode(string input)
        {
            return HttpUtility.UrlEncode(input);
        }
        #endregion

        #region  通过行列标题来生成表格的操作
        /// <summary>
        /// 通过行列标题来生成表格的操作
        /// </summary>
        /// <param name="formtle"></param>
        /// <param name="formhang"></param>
        /// <param name="formlie"></param>
        /// <returns></returns>
        public static String GenerateTableHtml(string id,string formtle, int formhang, int formlie)
        {
            string html = string.Empty;
            html += "<table class=\"desigerTable\"  id=\"" + id + "\"  cellpadding=\"10\" cellspacing=\"10\" ><thead><tr><th  colspan=\"##0##\">##1##</th></thead></tr><tbody>##2##</tbody></table>";
            html = html.Replace("##0##", formlie.ToString());
            html = html.Replace("##1##", formtle);


            List<String> listtr = new List<string>();
            for (int i = 0; i < formhang; i++)
            {
                List<String> listtd = new List<string>();
                for (int j = 0; j < formlie; j++)
                {
                    listtd.Add("<td class=\" targetarea\"></td>");
                }
                listtr.Add(string.Format("<tr>{0}</tr>", string.Join(string.Empty, listtd.ToArray())));
            }
            html = html.Replace("##2##", string.Join(string.Empty, listtr));
            return html;
        }
        #endregion
    }
    #endregion
}
