using System;

namespace DTOModel
{
    #region  生源基地的模型
    /// <summary>
    /// 生源基地的模型
    /// </summary>
    public class Dto_SYJD
    {
        public Guid SYJDId { get; set; }
        public string GrandSchool { get; set; }
        public string Who { get; set; }
        public DateTime InsertTime { get; set; }
        public DateTime LastUpdateTime { get; set; }
        public string DataFrom { get; set; }
    }
    #endregion
}
