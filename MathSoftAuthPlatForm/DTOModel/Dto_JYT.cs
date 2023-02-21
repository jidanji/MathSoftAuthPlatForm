using System;

namespace DTOModel
{
    #region 教育厅的模型
    /// <summary>
    /// 教育厅的模型
    /// </summary>
    public class Dto_JYT
    {
        /// <summary>
        /// 主键Id
        /// </summary>
        public Guid JYTId { get; set; }

        /// <summary>
        /// 学生姓名
        /// </summary>
        public string StudentName { get; set; }

        /// <summary>
        /// 身份证号码
        /// </summary>
        public string IdCard { get; set; }

        /// <summary>
        /// 学生性别
        /// </summary>
        public string StudentSex { get; set; }
        public string GrandSchool { get; set; }
        public DateTime InsertTime { get; set; }
        public DateTime LastUpdateTime { get; set; }
        public string DataFrom { get; set; }
    }
    #endregion
}
