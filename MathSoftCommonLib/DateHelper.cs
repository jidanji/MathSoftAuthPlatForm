using System;
using System.Collections.Generic;
using System.Linq;


namespace MathSoftCommonLib
{
    #region 时间帮助类
    /// <summary>
    /// 时间帮助类
    /// </summary>
    public static class DateHelper
    {
        #region 将日期时间格式转换为int类型 yyyyMMddHHmmss
        /// <summary>
        /// 将日期时间格式转换为int类型 yyyyMMddHHmmss
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static int GetDateTimeInt(this DateTime input)
        {
            String formate = "yyyyMMddHHmmss";
            return Convert.ToInt32(input.ToString(formate));
        }
        #endregion

        #region 将日期格式转换为int类型 yyyyMMddHHmmss
        /// <summary>
        /// 将日期格式转换为int类型 yyyyMMddHHmmss
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static int GetDateInt(this DateTime input)
        {
            String formate = "yyyyMMdd";
            return Convert.ToInt32(input.ToString(formate));
        }
        #endregion

        #region 将时间格式转换为int类型 HHmmss
        /// <summary>
        /// 将时间格式转换为int类型 HHmmss
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static int GetTimeInt(this DateTime input)
        {
            String formate = "HHmmss";
            return Convert.ToInt32(input.ToString(formate));
        }
        #endregion

        #region 将日期转换为特定格式(年)
        /// <summary>
        /// 将日期转换为特定格式(年)
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string GetDateYearString(this DateTime input)
        {
            return input.ToString(string.Format("yyyy"));
        }
        #endregion

        #region 将日期转换为特定格式(年月)
        /// <summary>
        /// 将日期转换为特定格式
        /// </summary>
        /// <param name="input"></param>
        /// <param name="speater"></param>
        /// <returns></returns>
        public static string GetDateYearMonthString(this DateTime input, string speater)
        {
            return input.ToString(string.Format("yyyy{0}MM", speater));
        }


        /// <summary>
        /// 将日期转换为特定格式
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string GetDateYearMonthString(this DateTime input)
        {
            return input.ToString(string.Format("yyyy{0}MM", "-"));
        }
        #endregion

        #region 将日期转换为特定格式
        /// <summary>
        /// 将日期转换为特定格式
        /// </summary>
        /// <param name="input"></param>
        /// <param name="speater"></param>
        /// <returns></returns>
        public static string GetDateString(this DateTime input, string speater)
        {
            return input.ToString(string.Format("yyyy{0}MM{0}dd", speater));
        }


        /// <summary>
        /// 将日期转换为特定格式
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string GetDateString(this DateTime input)
        {
            return input.ToString(string.Format("yyyy{0}MM{0}dd", "-"));
        }
        #endregion

        #region 将日期时间转换为特定格式
        /// <summary>
        /// 将日期时间转换为特定格式
        /// </summary>
        /// <param name="input"></param>
        /// <param name="speater1"></param>
        /// <param name="speater2"></param>
        /// <returns></returns>
        public static string GetDateTimeString(this DateTime input, string speater1, string speater2)
        {
            return input.ToString(string.Format("yyyy{0}MM{0}dd HH{1}mm{1}ss", speater1, speater2));
        }
        /// <summary>
        ///  将日期时间转换为特定格式
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string GetDateTimeString(this DateTime input)
        {
            return input.ToString(string.Format("yyyy{0}MM{0}dd HH{1}mm{1}ss", "-", ":"));
        }
        #endregion

        #region 获取日期
        /// <summary>
        /// 获取日期
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static DateTime GetDate(this DateTime input)
        {
            return input.Date;
        }
        #endregion

        #region 获取日期+小时
        /// <summary>
        /// 获取日期+小时 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static DateTime GetDateAndHour(this DateTime input)
        {
            DateTime dt = new DateTime(input.Year, input.Month, input.Day,
                input.Hour, 0, 0);
            return dt;
        }
        #endregion

        #region 获取日期+小时+分钟
        /// <summary>
        /// 获取日期+小时+分钟
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static DateTime GetDateAndHourMin(this DateTime input)
        {
            DateTime dt = new DateTime(input.Year, input.Month, input.Day,
                input.Hour, input.Minute, 0);
            return dt;
        }
        #endregion

        #region 获取日期+小时+分钟+秒钟
        /// <summary>
        /// 获取日期+小时+分钟+秒钟
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static DateTime GetDateAndHourMinSec(this DateTime input)
        {
            DateTime dt = new DateTime(input.Year, input.Month, input.Day,
                input.Hour, input.Minute, input.Second);
            return dt;
        }
        #endregion

        #region 最近本周
        /// <summary>
        /// 最近本周
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static DatePair GetWeek(this DateTime input)
        {
            int DayOfWeekint = (int)input.Date.DayOfWeek;
            var a = input.Date.AddDays((-1) * DayOfWeekint + 1);
            var b = input.Date.AddDays((7 - DayOfWeekint) + 1);
            DatePair model = new DatePair() { BegDate = a, EndTDate = b };
            return model;
        }
        #endregion

        #region 最近当天
        /// <summary>
        /// 最近当天
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static DatePair GetDayArea(this DateTime input)
        {
            DatePair ret = new DatePair();

            ret.BegDate = input.Date;

            ret.EndTDate = input.Date.AddDays(1);
            return ret;
        }
        #endregion

        #region 最近两天
        /// <summary>
        /// 最近两天
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static DatePair Get2DayArea(this DateTime input)
        {
            DatePair ret = new DatePair();

            ret.BegDate = input.Date.AddDays(-1);

            ret.EndTDate = input.Date.AddDays(1);
            return ret;
        }
        #endregion

        #region 最近当月的
        /// <summary>
        /// 最近当月的
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static DatePair GetMonthArea(this DateTime input)
        {
            DatePair ret = new DatePair();

            ret.BegDate = new DateTime(input.Year, input.Month, 1);

            ret.EndTDate = ret.BegDate.AddMonths(1);
            return ret;
        }
        #endregion

        #region 最近季度的
        /// <summary>
        /// 最近季度的
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static DatePair GetQuarterArea(this DateTime input)
        {
            DatePair ret = new DatePair();

            int month = input.Month;
            if (month >= 1 & month <= 3)
            {
                ret.BegDate = new DateTime(input.Year, 1, 1);
                ret.EndTDate = new DateTime(input.Year, 4, 1);
            }

            else if (month >= 4 & month <= 6)
            {
                ret.BegDate = new DateTime(input.Year, 4, 1);
                ret.EndTDate = new DateTime(input.Year, 7, 1);
            }

            else if (month >= 7 & month <= 9)
            {
                ret.BegDate = new DateTime(input.Year, 7, 1);
                ret.EndTDate = new DateTime(input.Year, 10, 1);
            }
            else
            {
                ret.BegDate = new DateTime(input.Year, 10, 1);
                ret.EndTDate = new DateTime(input.Year, 1, 1).AddYears(1);
            }
            return ret;
        }
        #endregion

        #region 最近本年的
        /// <summary>
        /// 最近本年的
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static DatePair GetYearArea(this DateTime input)
        {
            DatePair ret = new DatePair() { BegDate = new DateTime(input.Year, 1, 1), EndTDate = new DateTime(input.Year + 1, 1, 1) };

            return ret;
        }
        #endregion

        #region 根据区间找到年的区间列表
        /// <summary>
        /// 根据区间找到年的区间列表
        /// </summary>
        /// <param name="begTime"></param>
        /// <param name="endTime"></param>
        /// <returns></returns>
        public static List<DatePair> GetYearsList(DateTime begTime, DateTime endTime)
        {
            List<DatePair> list = new List<DatePair>();
            int begYear = begTime.Year;
            int endYear = endTime.Year;
            IEnumerable<int> listYear = Enumerable.Range(begYear, endYear - begYear + 1);
            foreach (var item in listYear)
            {
                DatePair pair = new DatePair() { BegDate = new DateTime(item, 1, 1), EndTDate = new DateTime(item, 1, 1).AddYears(1) };
                list.Add(pair);
            }
            return list;
        }
        #endregion

        #region 根据区间找到月的区间列表
        /// <summary>
        /// 根据区间找到月的区间列表
        /// </summary>
        /// <param name="begTime"></param>
        /// <param name="endTime"></param>
        /// <returns></returns>
        public static List<DatePair> GetMonthList(DateTime begTime, DateTime endTime)
        {
            List<DatePair> list = new List<DatePair>();
            DateTime begTime1 = new DateTime(begTime.Year, begTime.Month, 1);
            DateTime endDate1 = new DateTime(endTime.Year, endTime.Month, 1);
            for (DateTime a = begTime1; a <= endDate1; a = a.AddMonths(1))
            {
                DatePair pair = new DatePair() { BegDate = a, EndTDate = a.AddMonths(1) };
                list.Add(pair);
            }
            return list;
        }
        #endregion

        #region 根据区间找到周的区间列表 如果不到周的就按照周围日期补齐
        /// <summary>
        /// 根据区间找到周的区间列表 如果不到周的就按照周围日期补齐
        /// </summary>
        /// <param name="begTime"></param>
        /// <param name="endTime"></param>
        /// <returns></returns>
        public static List<DatePair> GetWeekList(DateTime begTime, DateTime endTime)
        {
            List<DatePair> list = new List<DatePair>();
            int DayOfWeekint1 = (int)begTime.Date.DayOfWeek;
            int DayOfWeekint2 = (int)endTime.Date.DayOfWeek;
            var beg = begTime.Date.AddDays((-1) * DayOfWeekint1 + 1);
            var end = endTime.Date.AddDays((7 - DayOfWeekint2) + 1);
            for (var a = beg; a < end; a = a.AddDays(7))
            {
                DatePair obj = new DatePair() { BegDate = beg, EndTDate = beg.AddDays(6) };
                list.Add(obj);
            }
            return list;
        }

        #endregion

        #region 根据区间找到天的区间列表
        /// <summary>
        /// 根据区间找到天的区间列表
        /// </summary>
        /// <param name="begTime"></param>
        /// <param name="endTime"></param>
        /// <returns></returns>
        public static List<DatePair> GetDayList(DateTime begTime, DateTime endTime)
        {
            List<DatePair> list = new List<DatePair>();
            DateTime begDate = begTime.Date;
            DateTime endDate = endTime.Date;
            for (DateTime a = begDate; a <= endDate; a = a.AddDays(1))
            {
                DatePair pair = new DatePair() { BegDate = a, EndTDate = a.AddDays(1) };
                list.Add(pair);
            };
            return list;
        }
        #endregion

        #region 获取当前日期是星期几？转化为中文
        /// <summary>
        /// 获取当前日期是星期几？转化为中文
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string GetCurrentWeek(this DateTime input)
        {
            string week = string.Empty;
            switch ((int)input.DayOfWeek)
            {
                case 0:
                    week = "星期日";
                    break;
                case 1:
                    week = "星期一";
                    break;
                case 2:
                    week = "星期二";
                    break;
                case 3:
                    week = "星期三";
                    break;
                case 4:
                    week = "星期四";
                    break;
                case 5:
                    week = "星期五";
                    break;
                default:
                    week = "星期六";
                    break;
            }
            return week;
        }
        #endregion

        #region 时间区段列表
        /// <summary>
        /// 时间区段列表
        /// </summary>
        public enum DataArea
        {
            /// <summary>
            /// 近一周
            /// </summary>
            Week,
            /// <summary>
            /// 近两天
            /// </summary>
            Day2,
            /// <summary>
            /// 当天
            /// </summary>
            Day,
            /// <summary>
            /// 当月
            /// </summary>
            Month,
            /// <summary>
            /// 当季
            /// </summary>
            Quarter,
            /// <summary>
            /// 当年
            /// </summary>
            Year
        }
        #endregion

        #region 日期对
        /// <summary>
        /// 日期对
        /// </summary>
        public class DatePair
        {
            #region 开始时间
            /// <summary>
            /// 开始时间
            /// </summary>
            public DateTime BegDate
            {
                get;
                set;
            }
            #endregion


            #region  结束时间
            /// <summary>
            /// 当使用这个日期时，请注意，请使用小于 非小于等于，结束时间
            /// </summary>
            public DateTime EndTDate
            {
                get;
                set;
            }
            #endregion

        }

        #endregion

        #region 跨度优先级
        /// <summary>
        /// 跨度优先级
        /// </summary>
        public enum DatePriority : int
        {
            PreAndBaseLine,
            PreAndAfter,
            BothBaseLine,
            BaseLineAndAfter
        }
        #endregion

        #endregion
    }

}
