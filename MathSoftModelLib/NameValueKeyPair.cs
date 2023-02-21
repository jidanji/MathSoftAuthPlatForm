using System;

namespace MathSoftModelLib
{
    public class MaliangUI {
        public string Math_Dept_Name { get; set; }
 
        public string Name { get; set; }
        public int Value { get; set; }

        public int Value2 { get; set; }

        public string rate { get; set; }

        public string Value3 { get; set; }
    }
    #region 键值对的集合
    /// <summary>
    /// 键值对的集合
    /// </summary>
    public class NameValueKeyPair
    {
        #region 名称
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }
        #endregion

        #region 取值
        /// <summary>
        /// 取值
        /// </summary>
        public string Value { get; set; }

        public override string ToString()
        {
            return String.Format("{0}------>{1}", Name, Value.ToString());
        }
        public string ToString(string Split)
        {
            return String.Format("{0}" + Split + "{1}", Name, Value.ToString());
        }
        #endregion

    }
    #endregion

    #region 拓展集合
    public class NameValueKeyPairExtention : NameValueKeyPair
    {
        #region 备注
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark
        {
            get;
            set;
        }
        #endregion

        #region  Id
        private string _Id = Guid.NewGuid().ToString();


        /// <summary>
        /// Id
        /// </summary>
        public string Id
        {
            get { return _Id; }
            set { _Id = value; }
        }
        #endregion'

        #region 父节点id
        private string _PId;

        /// <summary>
        ///父节点id
        /// </summary>
        public string PId
        {
            get { return _PId; }
            set { _PId = value; }
        }
        #endregion

        #region 孩子节点
        /// <summary>
        /// 孩子节点
        /// </summary>
        public dynamic Children
        {
            get;
            set;
        }
        #endregion
    }
    #endregion

}
