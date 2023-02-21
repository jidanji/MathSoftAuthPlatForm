using DataAccess;
using DTOModel;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Business
{
    public class BLL_ZhanZhangStaticNew
    {
        private MathRoleAuthorEntities context = null;
        private DbSet<ZhanZhangStaticNew> contextItem = null;
        public BLL_ZhanZhangStaticNew()
        {

            context = new MathRoleAuthorEntities();
            contextItem = context.ZhanZhangStaticNews;
        }

        public List<ZhanZhangStaticNew> Search(int PageIndex, int PageSize, out int total, Expression<Func<ZhanZhangStaticNew, bool>> where = null)
        {
            List<ZhanZhangStaticNew> list = new List<ZhanZhangStaticNew>();
            Expression<Func<ZhanZhangStaticNew, bool>> exp1 = where == null ? item => true : where;

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
    }
}
