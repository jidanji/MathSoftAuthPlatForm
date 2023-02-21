using DataAccess;
using DTOModel;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
 

namespace Business
{
 public   class BLL_View_ZhanZhangStatic
    {
        private MathRoleAuthorEntities context = null;
        private DbSet<View_ZhanZhangStatic> contextItem = null;
        public BLL_View_ZhanZhangStatic()
        {
            context = new MathRoleAuthorEntities();
            contextItem = context.View_ZhanZhangStatic;
        }

        /// <summary>
        /// 分页或者不分页查询数据
        /// </summary>
        /// <param name="PageIndex"></param>
        /// <param name="PageSize"></param>
        /// <param name="total"></param>
        /// <param name="where"></param>
        /// <returns></returns>
        public List<View_ZhanZhangStatic> Search(int PageIndex, int PageSize, out int total, Expression<Func<View_ZhanZhangStatic, bool>> where = null)
        {
            List<View_ZhanZhangStatic> list = new List<View_ZhanZhangStatic>();
            Expression<Func<View_ZhanZhangStatic, bool>> exp1 = where == null ? item => true : where;

            total = contextItem.Count(exp1);
            if (PageIndex >= 0)
            {
                list = contextItem.Where(exp1).OrderByDescending(i => i.XSCId).Skip(PageIndex).Take(PageSize).ToList();
            }
            else
            {
                list = contextItem.Where(exp1).OrderByDescending(i => i.XSCId).ToList();
            }

            return list;
        }

        public List<StaticUI> Static()
        {
            string sql2 = @"select base.*,t2.Math_Dept_Name from (
SELECT  count(*) Value,ZhanZhangDaQu,ZhanZhang
  FROM  dbo.View_ZhanZhangStatic
  where 
  ZhanZhangDaQu	 is not null 
  and  ZhanZhang is not null 
  group by ZhanZhangDaQu,ZhanZhang
  ) base     left join dbo.Math_UserInfo t1 on base.ZhanZhang=t1.UserName
  left join dbo.Math_Deptinfo t2 on t1.User_Dept_Id=t2.Math_Dept_Id
  order by Math_Dept_Name,ZhanZhang";

            List<StaticUI> ret2 = context.Database.SqlQuery<StaticUI>(sql2, new List<object>().ToArray()).ToList();
            return ret2;

        }

        public void Start()
        {
            clearTable();
            int total = 0;
            List<View_ZhanZhangStatic> list = Search(-1, -1, out total);
            foreach (View_ZhanZhangStatic item in list)
            {
                context.ZhanZhangStaticNews.Add(new  ZhanZhangStaticNew 
                {
                    DictRemark = item.DictRemark,
                    DictValue = item.DictValue,
                    GrandSchool = item.GrandSchool,
                    IdCard = item.IdCard,
                    StudentName = item.StudentName,
                    StudentNumber = item.StudentNumber,
                    XSCId = item.XSCId,
                    zhanzhang = item.zhanzhang,
                    zhanzhangdaqu = item.zhanzhangdaqu,
                    zhanzhangquyu = item.zhanzhangquyu
                });
            }
            context.SaveChanges();
        }

        public void clearTable()
        {
            context.Database.ExecuteSqlCommand("truncate table [dbo].[ZhanZhangStaticNew]", new object[] { });
        }
    }
}
