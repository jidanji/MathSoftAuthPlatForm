using System;

namespace DTOModel
{
    #region 学生处的模型
    /// <summary>
    /// 学生处的模型
    /// </summary>
    public class Dto_XSC
    {
        public Guid XSCId { get; set; }
        public Guid IdCard { get; set; }
        public Guid StudentName { get; set; }
        public Guid InsertTime { get; set; }
        public Guid LastUpdateTime { get; set; }
        public string DataFrom { get; set; }
    }
    #endregion
}
