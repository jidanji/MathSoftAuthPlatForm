using System;

namespace MathSoftModelLib
{
    #region 定时任务类
    /// <summary>
    /// 定时任务类
    /// </summary>
    public class TimeTask
    {
        #region  时间点
        /// <summary>
        ///时间点
        /// </summary>
        public int Hhmmss { get; set; }
        #endregion

        #region 用横线分割的时间格式 10-00-00
        /// <summary>
        /// 用横线分割的时间格式 10-00-00
        /// </summary>
        public string Hhmmsshengxian
        {
            get
            {
                string res = Hhmmss.ToString().PadLeft(6, '0');
                return res.Substring(0, 2) + "-" + res.Substring(2, 2) + "-" + res.Substring(4, 2);
            }
        }
        #endregion

        #region 用:分割的时间格式 10-00-00
        /// <summary>
        /// 用:分割的时间格式 10-00-00
        /// </summary>
        public string HhmmssMaohao
        {
            get
            {
                string res = Hhmmss.ToString().PadLeft(6, '0');
                return res.Substring(0, 2) + ":" + res.Substring(2, 2) + ":" + res.Substring(4, 2);
            }
        }
        #endregion

        #region 任务id
        /// <summary>
        /// 任务id
        /// </summary>
        public string TaskId { get; set; }
        #endregion

        #region  任务名称
        /// <summary>
        /// 任务名称
        /// </summary>
        public String TaskName { get; set; }
        #endregion

        #region 状态ID
        /// <summary>
        /// 状态ID
        /// </summary>
        public int StatusId { get; set; }
        #endregion

        #region 状态名称
        /// <summary>
        /// 状态名称
        /// </summary>
        public string StatusName { get; set; }
        #endregion

        private bool _IsCurrentNode = false;

        #region 是否当前节点
        /// <summary>
        /// 是否当前节点 默认值是否
        /// </summary>
        public bool IsCurrentNode { get => _IsCurrentNode; set => _IsCurrentNode = value; }
        #endregion

    }
    #endregion

}
