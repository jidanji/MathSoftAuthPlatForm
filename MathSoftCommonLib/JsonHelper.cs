using System;

namespace MathSoftCommonLib
{
    #region  Json帮助类
    /// <summary>
    /// Json帮助类
    /// </summary>
    public static class JsonHelper
    {
        #region 序列化一个对象
        /// <summary>
        /// 序列化一个对象
        /// </summary>
        /// <param name="o"></param>
        /// <returns></returns>
        public static String SerializeObject(object o)
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(o);
        }
        #endregion

        #region 反序列化一个对象
        /// <summary>
        /// 反序列化一个对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="input"></param>
        /// <returns></returns>
        public static T DeserializeObject<T>(string input)
        {
            return Newtonsoft.Json.JsonConvert.DeserializeObject<T>(input);
        }
        #endregion

        #region 将json字符串转换为foreach的可枚举对象 调用方式 直接 foreach( var item in thismethod  ){}
        /// <summary>
        /// 将json字符串转换为foreach的可枚举对象 调用方式 直接 foreach( var item in thismethod  ){}
        /// </summary>
        /// <param name="data">无树形结构的数据字符串</param>
        /// <returns></returns>
        public static Newtonsoft.Json.Linq.JObject GetForeachObj(string data)
        {
            return Newtonsoft.Json.JsonConvert.DeserializeObject(data) as Newtonsoft.Json.Linq.JObject;
        }
        #endregion
    }
    #endregion
}

