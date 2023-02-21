using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace MathSoftCommonLib
{
    #region 数据库帮助类（只用于查询）
    /// <summary>
    /// 数据库帮助类（只用于查询）
    /// </summary>
    public static class DataBaseHelper
    {
        #region  获取该用户下所有的数据表
        /// <summary>
        /// 获取该用户下所有的数据表
        /// </summary>
        /// <param name="conString"></param>
        /// <returns></returns>
        public  static IEnumerable<string> GetTables(string conString)
        {
            List<string> list = new List<string>();
            string sql = @"  SELECT Name FROM  SysObjects  Where  XType in ('U' )
ORDER BY Name ";

            SqlHelper helper = new SqlHelper(conString);
            DataTable dt = helper.ExeQueryGetDataSet(sql).Tables[0];
            foreach (var item in dt.AsEnumerable())
            {
                list.Add(item.Field<string>("Name"));
            }
            return list;
        }
        #endregion

        #region 获取该用户下所有的视图
        /// <summary>
        /// 获取该用户下所有的视图
        /// </summary>
        /// <param name="conString"></param>
        /// <returns></returns>
        public static IEnumerable<string> GetViews(string conString)
        {
            List<string> list = new List<string>();
            string sql = @"  SELECT Name FROM  SysObjects  Where  XType in ('V')
ORDER BY Name ";

            SqlHelper helper = new SqlHelper(conString);
            DataTable dt = helper.ExeQueryGetDataSet(sql).Tables[0];
            foreach (var item in dt.AsEnumerable())
            {
                list.Add(item.Field<string>("Name"));
            }
            return list;
        }
        #endregion
    }
    #endregion


    //获取所有的存储过程和函数
}
