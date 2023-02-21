using MathSoftModelLib;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MathSoftCommonLib
{
    #region 定时任务帮助类
    /// <summary>
    /// 定时任务帮助类
    /// </summary>
    public class TaskHelper
    {
        #region 获取当前任务
        /// <summary>
        /// 获取当前任务
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public List<TimeTask> GetCurrentTask(List<TimeTask> list)
        {
            List<TimeTask> ret = new List<TimeTask>();
            DateTime now = DateTime.Now;
            int timeNow = now.GetTimeInt();

            List<int> timeList = list.Where(item => item.Hhmmss <= timeNow).Select(item => item.Hhmmss).OrderByDescending(item => item).ToList<int>();

            if (timeList.Count() == 0)
            {
                return ret;
            }
            else
            {
                int lastTime = timeList.FirstOrDefault();
                return list.Where(item => item.Hhmmss == lastTime).ToList();
            }
        }
        #endregion

        #region  获取历史任务
        /// <summary>
        /// 获取历史任务
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public List<TimeTask> GetHisTask(List<TimeTask> list)
        {
            List<TimeTask> ret = new List<TimeTask>();
            DateTime now = DateTime.Now;
            int timeNow = now.GetTimeInt();
            return list.Where(item => item.Hhmmss <= timeNow).Select(item => item).ToList();
        }
        #endregion

        #region 获取未来任务
        /// <summary>
        /// 获取未来任务
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public List<TimeTask> GetFutureTask(List<TimeTask> list)
        {
            List<TimeTask> ret = new List<TimeTask>();
            DateTime now = DateTime.Now;
            int timeNow = now.GetTimeInt();
            return list.Where(item => item.Hhmmss > timeNow).Select(item => item).ToList();
        }
        #endregion
    }
    #endregion
}
