using System;

namespace DTOModel
{
    public class UI_TimeLine
    {
        /// <summary>
        /// 客户端ID，为了临时变量的需求
        /// </summary>
        public string TimelineClientId { get; set; }
        /// <summary>
        /// ID
        /// </summary>
        public Guid TimelineId { get; set; }
        /// <summary>
        /// 排序字段
        /// </summary>
        public int OrderId { get; set; }

        /// <summary>
        /// 种类
        /// </summary>
        public string category { get; set; }
        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime BegTime { get; set; }
        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime EndTime { get; set; }
        /// <summary>
        /// 录入时间
        /// </summary>
        public DateTime InsertTime { get; set; }
        /// <summary>
        /// 最近一次更新时间
        /// </summary>
        public DateTime LastUpdateTime { get; set; }
        /// <summary>
        /// 时间集合
        /// </summary>
        public DateTime[] DateRange
        {
            get;
            set;
        }
    }

}
