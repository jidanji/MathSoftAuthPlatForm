using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace MathSoftCommonLib
{
    #region 字符串帮助类
    /// <summary>
    /// 字符串帮助类
    /// </summary>
    public static class StringHelper
    {
        #region 批量替换一些东西
        /// <summary>
        /// 批量替换一些东西
        /// </summary>
        /// <param name="input"></param>
        /// <param name="array"></param>
        /// <param name="replaceStr"></param>
        /// <returns></returns>
        public static string ReplaceSome(this String input, IEnumerable<String> array, string replaceStr)
        {
            if (array == null)
            {

            }
            else
            {
                foreach (var item in array)
                {
                    input = input.Replace(item, replaceStr);
                }
            }
            return input;
        }
        #endregion

        #region 将字符串劈开后接着劈开两次劈开
        /// <summary>
        /// 将字符串劈开后接着劈开两次劈开
        /// </summary>
        /// <param name="str"></param>
        /// <param name="rowSpeater"></param>
        /// <param name="colSpeater"></param>
        /// <param name="delRe"></param>
        /// <param name="trim"></param>
        /// <returns></returns>
        public static IEnumerable<IEnumerable<string>> GetStr2ArrayArray(string str, string[] rowSpeater, string[] colSpeater, bool delRe = true, bool trim = true)
        {
            List<IEnumerable<String>> list = new List<IEnumerable<string>>();
            IEnumerable<string> ss = GetStr2Array(str, rowSpeater, delRe, trim);
            foreach (var item in ss)
            {
                IEnumerable<string> temp = GetStr2Array(item, colSpeater, delRe, trim);
                list.Add(temp);
            }
            return list;
        }
        #endregion

        #region  把字符串按照分隔符转换成 List
        /// <summary>
        /// 把字符串按照分隔符转换成 List
        /// </summary>
        /// <param name="str">源字符串</param>
        /// <param name="speater">分隔符</param>
        /// <param name="toLower">是否转换为小写</param>
        /// <param name="delRe">是否去重复</param>
        /// <returns></returns>
        public static IEnumerable<string> GetStr2Array(string str, string[] speater, bool delRe = true,bool trim=true)
        {
            List<string> list = new List<string>();
            string[] ss = str.Split( speater , StringSplitOptions.RemoveEmptyEntries);
            if (trim)
            {
                ss = str.Split( speater , StringSplitOptions.RemoveEmptyEntries).Select(item=>item.Trim()).ToArray();
            }
            if (delRe)
            {
                return ss.Distinct();
            }
            else
            {
                return ss;
            }
        }
        #endregion

        #region 把字符串转 按照, 分割 换为数据
        /// <summary>
        /// 把字符串转 按照, 分割 换为数据
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static IEnumerable<string> GetStr2Array(string str, bool delRe = true, bool trim = true)
        {
            string[] arr = str.Split(new [] {"," }, StringSplitOptions.RemoveEmptyEntries);
            if (trim)
            {
                arr = arr.Select(item => item.Trim()).ToArray();
            }
            if (delRe)
            {
                return arr.Distinct();
            }
            else
            {
                return arr;
            }
        }
        #endregion

        #region 把 List<string> 按照分隔符组装成 string
        /// <summary>
        /// 把 List<string> 按照分隔符组装成 string
        /// </summary>
        /// <param name="list"></param>
        /// <param name="speater"></param>
        /// <returns></returns>
        public static string GetArray2Str<T>(IEnumerable<T> list, string speater)
        {
            return string.Join(speater, list.Select(item => item.ToString()));
        }

        #endregion

        #region 得到数组列表以逗号分隔的字符串
        /// <summary>
        /// 得到数组列表以逗号分隔的字符串
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public static string GetArray2Str<T>(IEnumerable<T> list)
        {
            return string.Join(",", list.Select(item => item.ToString()));
        }
        #endregion

        #region 得到数组列表以逗号分隔的字符串
        /// <summary>
        /// 得到数组列表以逗号分隔的字符串
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public static string GetDictionaryValue2Str<T1, T2>(Dictionary<T1, T2> list)
        {
            StringBuilder sb = new StringBuilder();
            foreach (KeyValuePair<T1, T2> kvp in list)
            {
                sb.Append(kvp.Value.ToString() + ",");
            }
            if (list.Count > 0)
            {
                return DelLastComma(sb.ToString());
            }
            else
            {
                return "";
            }
        }
        #endregion

        #region 删除最后结尾的一个逗号
        /// <summary>
        /// 删除最后结尾的一个逗号
        /// </summary>
        public static string DelLastComma(string str)
        {
            return str.Substring(0, str.LastIndexOf(","));
        }

        /// <summary>
        /// 删除最后结尾的指定字符后的字符
        /// </summary>
        public static string DelLastChar(string str, string strchar)
        {
            return str.Substring(0, str.LastIndexOf(strchar));
        }

        #endregion

        #region 转全角的函数(SBC case)
        /// <summary>
        /// 转全角的函数(SBC case)
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string ToSBC(string input)
        {
            //半角转全角：
            char[] c = input.ToCharArray();
            for (int i = 0; i < c.Length; i++)
            {
                if (c[i] == 32)
                {
                    c[i] = (char)12288;
                    continue;
                }
                if (c[i] < 127)
                    c[i] = (char)(c[i] + 65248);
            }
            return new string(c);
        }
        #endregion

        #region 转半角的函数(SBC case)
        /// <summary>
        ///  转半角的函数(SBC case)
        /// </summary>
        /// <param name="input">输入</param>
        /// <returns></returns>
        public static string ToDBC(string input)
        {
            char[] c = input.ToCharArray();
            for (int i = 0; i < c.Length; i++)
            {
                if (c[i] == 12288)
                {
                    c[i] = (char)32;
                    continue;
                }
                if (c[i] > 65280 && c[i] < 65375)
                    c[i] = (char)(c[i] - 65248);
            }
            return new string(c);
        }
        #endregion

        #region SQL注入检测

        /// <summary>
        /// SQL注入检测
        /// </summary>
        /// <param name="String"></param>
        /// <param name="IsDel"></param>
        /// <returns></returns>
        public static string SqlSafeString(string String, bool IsDel)
        {
            if (IsDel)
            {
                String = String.Replace("'", "");
                String = String.Replace("\"", "");
                return String;
            }
            String = String.Replace("'", "&#39;");
            String = String.Replace("\"", "&#34;");
            return String;
        }
        #endregion

        #region 获取正确的Id，如果不是正整数，返回0
        /// <summary>
        /// 获取正确的Id，如果不是正整数，返回0
        /// </summary>
        /// <param name="_value"></param>
        /// <returns>返回正确的整数ID，失败返回0</returns>
        public static int Str2Id(string _value)
        {
            if (IsNumberId(_value))
                return int.Parse(_value);
            else
                return 0;
        }
        #endregion

        #region 检查一个字符串是否是纯数字构成的，一般用于查询字符串参数的有效性验证。
        /// <summary>
        /// 检查一个字符串是否是纯数字构成的，一般用于查询字符串参数的有效性验证。(0除外)
        /// </summary>
        /// <param name="_value">需验证的字符串。。</param>
        /// <returns>是否合法的bool值。</returns>
        public static bool IsNumberId(string _value)
        {
            return QuickValidate("^[1-9]*[0-9]*$", _value);
        }
        #endregion

        #region 快速验证一个字符串是否符合指定的正则表达式。
        /// <summary>
        /// 快速验证一个字符串是否符合指定的正则表达式。
        /// </summary>
        /// <param name="_express">正则表达式的内容。</param>
        /// <param name="_value">需验证的字符串。</param>
        /// <returns>是否合法的bool值。</returns>
        public static bool QuickValidate(string _express, string _value)
        {
            if (_value == null) return false;
            Regex myRegex = new Regex(_express);
            if (_value.Length == 0)
            {
                return false;
            }
            return myRegex.IsMatch(_value);
        }
        #endregion

        #region 查询一个字符数组中是否有重复数据
        /// <summary>
        /// 查询一个字符数组中是否有重复数据
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static bool ArryRe(IEnumerable<string> input)
        {
            return input.Distinct().Count() != input.Count();
        }
        #endregion

        #region 查询数据是否为空
        /// <summary>
        /// 查询数据是否为空
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static bool IsNullOrEmpty(this string input)
        {
            return string.IsNullOrEmpty(input);
        }
        #endregion

        #region 加密成Base64
        /// <summary>
        /// 加密成Base64
        /// </summary>
        /// <param name="strJm"></param>
        /// <returns></returns>
        public static string EncryptBase64(this string strJm)
        {

            ASCIIEncoding ae = new ASCIIEncoding();
            byte[] bb1 = ae.GetBytes(strJm);
            string s2 = Convert.ToBase64String(bb1);
            return s2;
        }
        #endregion

        #region 根据Base64解密成string
        /// <summary>
        /// 根据Base64解密成string
        /// </summary>
        /// <param name="strJm"></param>
        /// <returns></returns>
        public static string DecryptBase64(this string strJm)
        {
            ASCIIEncoding ae = new ASCIIEncoding();
            byte[] b3 = Convert.FromBase64String(strJm);
            string s2 = ae.GetString(b3);
            return s2;
        }
        #endregion

    }

    #endregion
}
