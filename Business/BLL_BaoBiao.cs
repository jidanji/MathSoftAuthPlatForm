using DataAccess;
using DTOModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MathSoftCommonLib;
using MathSoftModelLib;

namespace Business
{
    #region 报表分析类
    /// <summary>
    /// 报表分析类
    /// </summary>
    public class BLL_BaoBiao
    {
        #region  对象的实例
        private MathRoleAuthorEntities context = null;
        #endregion

        #region 构造函数
        /// <summary>
        /// 构造函数
        /// </summary>
        public BLL_BaoBiao()
        {
            context = new MathRoleAuthorEntities();
        }
        #endregion

        #region 工作量报表
        /// <summary>
        /// 
        /// </summary>
        /// <param name="UserName">工作量报表</param>
        /// <param name="searchBegDate"></param>
        /// <param name="searchEndDate"></param>
        /// <param name="searchDept"></param>
        /// <param name="searchStaticType"></param>
        /// <returns></returns>
        public List<UIStatic> Search(
            string UserName,
            string searchBegDate,
            string searchEndDate,
            string searchDept,
            string searchStaticType)
        {
            string p1 = " and 1=1";
            string p2 = " and 1=1";
            string p3 = " and 1=1";
            //开始日期
            if (!string.IsNullOrEmpty(searchBegDate))
            {
                p1+=" and ShenPiRiQi>='" + searchBegDate + "'";
            }
            else
            {
                p1 += " and ShenPiRiQi>='" + DateTime.Now.Year + "-01-01" + "'";
            }


            //结束日期
            if (!string.IsNullOrEmpty(searchEndDate))
            {
                p2 += " and ShenPiRiQi<'" + DateTime.Parse(searchEndDate).AddDays(1).GetDateString() + "'";
            }
            else
            {
                p2 += " and ShenPiRiQi<'" + (DateTime.Now.Year + 1).ToString() + "-01-01" + "'";
            }

            Math_UserInfo userinfo = new BLL_User().GetSingleByUserAccount(UserName);
            if (userinfo.User_Area != 2)
            {
                p3 = " and  [CreateDeptId]='" + userinfo.User_Dept_Id.ToString() + "' ";
            }
            else
            {
                if (!string.IsNullOrEmpty(searchDept))
                {
                    p3 += " and  [CreateDeptId]='" + searchDept + "' ";
                }
            }

            String sql = string.Format(@"SELECT DateMonth ,
       [CreateDeptId]
      ,[UserDeptName]
      , sum([InfactFee]) as [InfactFee],
count(*) as PersonCount,
'##4##' as StaticType
from(
 SELECT convert(varchar({3}), ShenPiRiQi, 20) as DateMonth,
        [CreateDeptId]
       ,[UserDeptName]
       ,[InfactFee]
   FROM[MathRoleAuthor].[dbo].[Math_Work]
   where STATUS = 3 {0} {1} {2}) t1
     group by DateMonth,
          [CreateDeptId]
         ,[UserDeptName]
      order by[CreateDeptId], DateMonth", p1, p2, p3, searchStaticType);
            sql = sql.Replace("##4##", searchStaticType == "4" ? "按年" : "按月");
            List<UIStatic> ret = context.Database.SqlQuery<UIStatic>(sql, new List<object>().ToArray()).ToList();
            return ret;
        }
        #endregion

        #region 学生报表-按月统计
        /// <summary>
        /// 学生报表-按月统计
        /// </summary>
        /// <returns></returns>
        public List<UIZHaoShengBaobiao> GetZhaoShengBaoBiaoByMonth()
        {
            string sql = @"select   
  count(*) as TotalNumber,
  [InsertData]
  from (
SELECT  
       [UserDeptId]
      ,[UserDeptName],
   convert(varchar(7), [InsertTime], 20) as  [InsertData]
  FROM [MathRoleAuthor].[dbo].[Math_Student]) t
 group by   [InsertData]
 order by  [InsertData] desc";
            List<UIZHaoShengBaobiao> ret = context.Database.SqlQuery<UIZHaoShengBaobiao>(sql, new List<object>().ToArray()).ToList();
            return ret;
        }
        #endregion

        #region 学生报表-按照部门统计
        /// <summary>
        /// 学生报表-按照部门统计
        /// </summary>
        /// <returns></returns>
        public List<UIZHaoShengBaobiao> GetZhaoShengBaoBiao()
        {
            string sql = @"select  [UserDeptId]
      ,[UserDeptName],
  count(*) as TotalNumber
  
  from (
SELECT  
       [UserDeptId]
      ,[UserDeptName]
 

  FROM [MathRoleAuthor].[dbo].[Math_Student]) t
 
 group by  [UserDeptId],[UserDeptName]
 order by  TotalNumber desc";
            List<UIZHaoShengBaobiao> ret = context.Database.SqlQuery<UIZHaoShengBaobiao>(sql, new List<object>().ToArray()).ToList();
            return ret;
        }
        #endregion

        #region 学生报表-按照推荐人统计

        /// <summary>
        /// 学生报表-按照推荐人统计
        /// </summary>
        /// <returns></returns>
        public List<UIZHaoShengBaobiao> GetZhaoShengBaoBiaoByTuiJianRen()
        {
            string sql = @"select  UserName
      ,UserId,
  count(*) as TotalNumber,
  UserDeptName
  from (
SELECT  
       UserName,
      UserId,
      UserDeptName
  FROM [MathRoleAuthor].[dbo].[Math_Student]) t
 group by  UserId,UserName,UserDeptName
 order by  TotalNumber desc";
            List<UIZHaoShengBaobiao> ret = context.Database.SqlQuery<UIZHaoShengBaobiao>(sql, new List<object>().ToArray()).ToList();

            return ret;
        }
        #endregion

        #region 学生报表-按照专业统计
        /// <summary>
        /// 学生报表-按照专业统计
        /// </summary>
        /// <returns></returns>
        public List<UIZHaoShengBaobiao> GetZhaoShengBaoBiaoByZhuanYe()
        {
            string sql = @"Select count(*) TotalNumber,
[StudentZhuanYeId] ZhuanYeId,DictValue ZhuanYeName  from (
SELECT   t1. [StudentId]
      ,t1.[StudentOrderId]
      ,t1.[StudentName]
      ,t1.[StudentIDCard]
      ,t1.[StudentPhone]
      ,t1.[StudentType]
      ,t1.[StudentSchool]
      ,t1.[StudentAddress]
      ,t1.[UserId]
      ,t1.[UserPhone]
      ,t1.[UserName]
      ,t1.[UserDeptId]
      ,t1.[UserDeptName]
      ,t1.[InsertTime]
      ,t1.[UpdateTime]
      ,t1.[Remark]
       ,t1.[StudentZhuanYeId]
     ,t2.DictValue
  FROM  [Math_Student] t1
  join dbo.Math_Dict t2 on t1.[StudentZhuanYeId]=t2.DictId
  ) t3
  group by  [StudentZhuanYeId],DictValue
  order by TotalNumber desc
";


            List<UIZHaoShengBaobiao> ret = context.Database.SqlQuery<UIZHaoShengBaobiao>(sql, new List<object>().ToArray()).ToList();

            return ret;
        }
        #endregion

        #region 学生报表-按照性别统计
        /// <summary>
        /// 学生报表-按照性别统计
        /// </summary>
        /// <returns></returns>
        public List<UIZHaoShengBaobiao> GetZhaoShengBaoBiaoBySex()
        {
            string sql = @"select Sex,
  count(*) as TotalNumber
  from (
SELECT  
 Sex
  FROM [MathRoleAuthor].[dbo].[Math_Student]) t
 group by  Sex
 order by  TotalNumber desc";
            List<UIZHaoShengBaobiao> ret = context.Database.SqlQuery<UIZHaoShengBaobiao>(sql, new List<object>().ToArray()).ToList();
            return ret;
        }
        #endregion

        #region  学生报表-按照地区统计
        /// <summary>
        /// 学生报表-按照地区统计
        /// </summary>
        /// <returns></returns>
        public List<UIZHaoShengBaobiao> GetZhaoShengBaoBiaoByArea()
        {
            string sql = @"select Area,
  count(*) as TotalNumber
  from (
SELECT  
       Area
  FROM [MathRoleAuthor].[dbo].[Math_Student]) t
 group by  Area
 order by  TotalNumber desc";
            List<UIZHaoShengBaobiao> ret = context.Database.SqlQuery<UIZHaoShengBaobiao>(sql, new List<object>().ToArray()).ToList();
            return ret;
        }
        #endregion

        #region 学生报表-按照学生类别统计
        /// <summary>
        /// 学生报表-按照学生类别统计
        /// </summary>
        /// <returns></returns>
        public List<UIZHaoShengBaobiao> GetZhaoShengBaoBiaoByStudentType()
        {
            string sql = @"select StudentType,count(*) as TotalNumber
from (
SELECT  StudentType
  FROM [MathRoleAuthor].[dbo].[Math_Student]
  ) t
 group by  StudentType
 order by  TotalNumber desc
 ";
            List<UIZHaoShengBaobiao> ret = context.Database.SqlQuery<UIZHaoShengBaobiao>(sql, new List<object>().ToArray()).ToList();
            return ret;
        }
        #endregion
    }
    #endregion
}
