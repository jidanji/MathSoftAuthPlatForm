using MathSoftModelLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace MathSoftCommonLib
{
    public class ObjectHelper
    {
        #region 获取对象信息
        /// <summary>
        /// 获取对象信息
        /// </summary>
        /// <param name="o"></param>
        /// <returns></returns>
        public static List<NameValueKeyPair> GetDetailsList(object o)
        {
            List<NameValueKeyPair> list = new List<NameValueKeyPair>();
            Type Ts = o.GetType();
            PropertyInfo[] properts = Ts.GetProperties();

            foreach (PropertyInfo item in properts)
            {
                var value = item.GetValue(o, null).ToString();
                var key = item.Name;
                NameValueKeyPair paire = new NameValueKeyPair() { Name = key, Value = value };
                list.Add(paire);
            }
            return list;
        }
        #endregion

        #region 获取对象信息
        /// <summary>
        /// 获取对象信息
        /// </summary>
        /// <param name="o"></param>
        /// <returns></returns>
        public static String GetDetailsString(object o, string RowSpliter = "\n", string eleSpliter = "->")
        {
            List<NameValueKeyPair> list = new List<NameValueKeyPair>();
            Type Ts = o.GetType();
            PropertyInfo[] properts = Ts.GetProperties();

            foreach (PropertyInfo item in properts)
            {
                var value = item.GetValue(o, null).ToString();
                var key = item.Name;
                NameValueKeyPair paire = new NameValueKeyPair() { Name = key, Value = value };
                list.Add(paire);
            }
            string strRet = StringHelper.GetArray2Str<string>(list.Select(item => item.ToString(eleSpliter)), RowSpliter);

            return strRet;
        }

        #endregion

    }
}
