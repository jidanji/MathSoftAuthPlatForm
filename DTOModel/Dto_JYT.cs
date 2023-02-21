using System;

namespace DTOModel
{
    #region 教育厅的模型
    /// <summary>
    /// 教育厅的模型
    /// </summary>
    public class Dto_JYT
    {
        public Guid JYTId { get; set; }
        public string StudentName { get; set; }
        public string IdCard { get; set; }
        public string StudentSex { get; set; }
        public string GrandSchool { get; set; }
        public DateTime InsertTime { get; set; }
        public DateTime LastUpdateTime { get; set; }
        public string DataFrom { get; set; }
    }
    #endregion
}
