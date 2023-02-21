using System;

namespace DTOModel
{
    #region 财务处的模型
    /// <summary>
    /// 财务处的模型
    /// </summary>
    public class Dto_CWC
    {
        public Guid CWCId { get; set; }
        public string StudentName { get; set; }
        public string IdCard { get; set; }
        public DateTime InsertTime { get; set; }
        public DateTime LastUpdateTime { get; set; }
        public string DataFrom { get; set; }
    }
    #endregion
}
