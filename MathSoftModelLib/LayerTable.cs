using System.Collections.Generic;

namespace MathSoftModelLib
{
    #region LayerTable的基本类的描述
    /// <summary>
    /// LayerTable的基本类的描述
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class LayerTable<T>
    {
        public int code { get; set; }
        public string msg { get; set; }
        public int count { get; set; }
        public List<T> data { get; set; }
    }
    #endregion
}
