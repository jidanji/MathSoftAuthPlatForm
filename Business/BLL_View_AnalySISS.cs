using DataAccess;
using DTOModel;
using MathSoftModelLib;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace Business
{
    public class BLL_View_AnalySISS
    {
        private MathRoleAuthorEntities context = null;
        private DbSet<View_AnalySISS> contextItem = null;
        public BLL_View_AnalySISS()
        {
            context = new MathRoleAuthorEntities();
            contextItem = context.View_AnalySISS;
        }

        public List<View_AnalySISS> Search(int PageIndex, int PageSize, out int total, Expression<Func<View_AnalySISS, bool>> where = null)
        {
            Expression<Func<View_AnalySISS, bool>> exp1 = where == null ? item => true : where;

            total = contextItem.Count(exp1);
            return PageIndex >= 0 ?
            contextItem.Where(exp1).OrderByDescending(item => item.ZS_Who).Skip(PageIndex).Take(PageSize).ToList()
            : contextItem.Where(exp1).OrderByDescending(item => item.ZS_Who).ToList();
        }

        public List<View_AnalySISS> GetAll()
        {
            return contextItem.ToList();
        }

        //        public List<MaliangUI> Static(string alarm_type)
        //        {
        //            string sql = @" 
        //    select base.*,t2.Math_Dept_Name from (
        // select ISNULL(t1.[ZS_Who],'自然生源') as Name ,count(*)as Value from (SELECT  [JYT_Id]
        //      ,[JYT_StudentName]
        //      ,[JYT_IdCard]
        //      ,[JYT_StudentSex]
        //      ,[JYT_GrandSchool]
        //      ,[JYT_InsertTime]
        //      ,[JYT_LastUpdateTime]
        //      ,[JYT_DataFrom]
        //      ,[XSC_IdCard]
        //      ,[ZS_IdCard]
        //      ,[SYJD_GrandSchool]
        //      ,[SYJD_Who]
        //      ,[CWC_IdCard]
        //      ,[ZS_Who]
        //  FROM [MathRoleAuthor].[dbo].[View_AnalySISS] base 
        //  where SYJD_GrandSchool is  null and FeeItems=2 ) as t1
        //  group by t1.[ZS_Who])
        //  base 
        //  left join dbo.Math_UserInfo t1 on base.Name=t1.UserName
        //  left join dbo.Math_Deptinfo t2 on t1.User_Dept_Id=t2.Math_Dept_Id

        //  order by Math_Dept_Name desc
        //";
        //            List<MaliangUI> ret = context.Database.SqlQuery<MaliangUI>(sql, new List<object>().ToArray()).ToList();

        //            string sql2 = @"SELECT 
        //       [UserName] as Name,count(*)
        //as Value        
        //  FROM [MathRoleAuthor].[dbo].[Math_Student]
        //  group by [UserName]";


        //            List<MaliangUI> ret2 = context.Database.SqlQuery<MaliangUI>(sql2, new List<object>().ToArray()).ToList();

        //            foreach(MaliangUI item in ret)
        //            {
        //              var res= ret2.FirstOrDefault(subitem => item.Name == subitem.Name);
        //                if (res == null)
        //                {
        //                    item.rate = "0%";
        //                    item.Value2 = 0;
        //                }
        //                else
        //                {
        //                    if (res.Value == 0)
        //                    {
        //                        item.rate = "0%";
        //                        item.Value2 = 0;
        //                    }
        //                    else
        //                    {
        //                        item.Value2 = res.Value;
        //                        item.rate = Math.Round((decimal)item.Value / res.Value*100, 2).ToString()+"%";  
        //                    }
        //                }

        //            }

        //         var s1=  ret.Sum(item => item.Value);
        //            var s2 = ret.Sum(item => item.Value2);

        //            var s3 = "0";
        //            if (s2 != 0)
        //            {
        //                s3 = Math.Round((decimal)s1 / s2 * 100, 2).ToString() + "%";
        //            }

        //            List<MaliangUI> ret3 = new List<MaliangUI>();
        //            ret3.Add(new MaliangUI { Name = "全体人员", rate = s3, Value = s1, Value2 = s2 });

        //            List<MaliangUI> retAppennd = context.Database.SqlQuery<MaliangUI>(@"select base.UserName Name,base.Math_Dept_Name,count(*)  as Value2 ,'0%'as rate,0 as Value from (
        //SELECT t1.*, t4.Math_Dept_Name
        //  FROM[MathRoleAuthor].[dbo].[Math_Student] t1
        //left   join[MathRoleAuthor].[dbo].[Math_XSC] t2 on t1.[StudentIDCard] = t2.[IdCard]
        // left join dbo.Math_UserInfo t3 on t1.[UserName] = t3.UserName
        //  left join dbo.Math_Deptinfo t4 on t3.User_Dept_Id = t4.Math_Dept_Id
        //where t2.[IdCard] is null)

        //base
        //group by UserName,Math_Dept_Name", new List<object>().ToArray()).ToList();

        //            List<MaliangUI> googo = new List<MaliangUI>();
        //            googo.AddRange(ret);
        //            foreach (MaliangUI item1 in retAppennd)
        //            {
        //               int count= ret.Count(d => d.Name == item1.Name);
        //                if (count > 0)
        //                {

        //                }
        //                else
        //                {
        //                    googo.Add(item1);
        //                }

        //            }

        //            googo = googo.OrderByDescending(item => item.Math_Dept_Name).ToList();
        //            ret3.AddRange(googo);

        //            return ret3;
        //        }

        public List<MaliangUI> Static(string alarm_type)
        {
            string sql0 = @"  SELECT   [zhanzhang] Name,count(*)as Value
  FROM (
SELECT     ZhanZhangStaticNew.*
FROM     Math_Student     INNER JOIN ZhanZhangStaticNew
                       ON ZhanZhangStaticNew.IdCard = Math_Student.StudentIDCard
                       and Math_Student.UserName=ZhanZhangStaticNew.zhanzhang
 
                       )
                      t1
                       where   DictValue
 is not null 
  group by [zhanzhang]
   ";

            List<MaliangUI> xiaqulist = context.Database.SqlQuery<MaliangUI>(sql0, new List<object>().ToArray()).ToList();



            string sql = @" 
      select base.*,t2.Math_Dept_Name from (
 select   Name ,count(*)as Value from (SELECT  [JYT_Id]
      ,[JYT_StudentName]
      ,[JYT_IdCard]
      ,[JYT_StudentSex]
      ,[JYT_GrandSchool]
      ,[JYT_InsertTime]
      ,[JYT_LastUpdateTime]
      ,[JYT_DataFrom]
      ,[XSC_IdCard]
      ,[ZS_IdCard]
      ,[SYJD_GrandSchool]
      ,[SYJD_Who]
      ,[CWC_IdCard]
      ,[ZS_Who],
case when syjd_grandschool is null and  zs_who is not null then  zs_who
 when syjd_grandschool is not null and  syjd_who is not null then  syjd_grandschool
else '自然生源' end 
 as Name
  FROM [MathRoleAuthor].[dbo].[View_AnalySISS] base 
  where  FeeItems=2 and name_Check=1 ) as t1
  group by t1.Name)
  base 
  left join dbo.Math_UserInfo t1 on base.Name=t1.UserName
  left join dbo.Math_Deptinfo t2 on t1.User_Dept_Id=t2.Math_Dept_Id

  order by Math_Dept_Name desc
";
            List<MaliangUI> ret = context.Database.SqlQuery<MaliangUI>(sql, new List<object>().ToArray()).ToList();

            string sql2 = @"SELECT 
       [UserName] as Name,count(*)
as Value        
  FROM [MathRoleAuthor].[dbo].[Math_Student]
  group by [UserName]";


            List<MaliangUI> ret2 = context.Database.SqlQuery<MaliangUI>(sql2, new List<object>().ToArray()).ToList();

            foreach (MaliangUI item in ret)
            {
                var res = ret2.FirstOrDefault(subitem => item.Name == subitem.Name);
                if (res == null)
                {
                    item.rate = "0%";
                    item.Value2 = 0;
                }
                else
                {
                    if (res.Value == 0)
                    {
                        item.rate = "0%";
                        item.Value2 = 0;
                    }
                    else
                    {
                        item.Value2 = res.Value;
                        item.rate = Math.Round((decimal)item.Value / res.Value * 100, 2).ToString() + "%";
                    }
                }

            }

            var s1 = ret.Sum(item => item.Value);
            var s2 = ret.Sum(item => item.Value2);

            var s3 = "0";
            if (s2 != 0)
            {
                s3 = Math.Round((decimal)s1 / s2 * 100, 2).ToString() + "%";
            }

            List<MaliangUI> ret3 = new List<MaliangUI>();
            ret3.Add(new MaliangUI { Name = "全体人员", rate = s3, Value = s1, Value2 = s2 });

            List<MaliangUI> retAppennd = context.Database.SqlQuery<MaliangUI>(@"select base.UserName Name,base.Math_Dept_Name,count(*)  as Value2 ,'0%'as rate,0 as Value from (
SELECT t1.*, t4.Math_Dept_Name
  FROM[MathRoleAuthor].[dbo].[Math_Student] t1
left   join[MathRoleAuthor].[dbo].[Math_XSC] t2 on t1.[StudentIDCard] = t2.[IdCard]
 left join dbo.Math_UserInfo t3 on t1.[UserName] = t3.UserName
  left join dbo.Math_Deptinfo t4 on t3.User_Dept_Id = t4.Math_Dept_Id
where t2.[IdCard] is null)
 
base
group by UserName,Math_Dept_Name", new List<object>().ToArray()).ToList();

            List<MaliangUI> googo = new List<MaliangUI>();
            googo.AddRange(ret);
            foreach (MaliangUI item1 in retAppennd)
            {
                int count = ret.Count(d => d.Name == item1.Name);
                if (count > 0)
                {

                }
                else
                {
                    googo.Add(item1);
                }

            }

            googo = googo.OrderByDescending(item => item.Math_Dept_Name).ToList();
            ret3.AddRange(googo);

            //到了最后我们补充辖区内的数据
            foreach (MaliangUI item in ret3)
            {
                MaliangUI target = xiaqulist.FirstOrDefault(sub => sub.Name == item.Name);
                if (target != null)
                {
                    item.Value3 = target.Value.ToString();
                }
                else
                {
                    item.Value3 = "_";
                }

            }
            return ret3;
        }

        public List<SSISDetail> GetDetail(string zhanzhang)
        {
            string sql = string.Format(@" SELECT  *
  FROM (
SELECT     ZhanZhangStaticNew.*
FROM     Math_Student     INNER JOIN ZhanZhangStaticNew
                       ON ZhanZhangStaticNew.IdCard = Math_Student.StudentIDCard
                       and Math_Student.UserName=ZhanZhangStaticNew.zhanzhang
 
                       )
                      t1
                       where   DictValue
 is not null and 
 zhanzhang='{0}'", zhanzhang);

            List<SSISDetail> xiaqulist = context.Database.SqlQuery<SSISDetail>(sql, new List<object>().ToArray()).ToList();
            return xiaqulist;
        }
    }

   
}
