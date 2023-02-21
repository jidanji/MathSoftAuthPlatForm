using System;

namespace DTOModel
{
    #region 财务处的模型
    /// <summary>
    /// 财务处的模型
    /// </summary>
    public class Dto_CWC
    {
        /// <summary>
        /// 主键Id
        /// </summary>
        public Guid CWCId { get; set; }

        /// <summary>
        /// 学生姓名
        /// </summary>
        public string StudentName { get; set; }

        /// <summary>
        /// 身份证号码
        /// </summary>
        public string IdCard { get; set; }

        /// <summary>
        /// 录入时间
        /// </summary>
        public DateTime InsertTime { get; set; }

        /// <summary>
        /// 最近一次更新时间
        /// </summary>
        public DateTime LastUpdateTime { get; set; }

        /// <summary>
        /// 数据来源
        /// </summary>
        public string DataFrom { get; set; }
    }
    #endregion
}
