
using DataAccess;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Business
{
    public class BLL_View_User_Role: BaseMathRoleAuthorEntities
    {
        private DbSet<View_User_Role> contextItem = null;
        public BLL_View_User_Role():base()
        {
            contextItem = context.View_User_Role;
        }

        /// <summary>
        /// 分页或者不分页查询数据
        /// </summary>
        /// <param name="PageIndex"></param>
        /// <param name="PageSize"></param>
        /// <param name="total"></param>
        /// <param name="where"></param>
        /// <returns></returns>
        public List<View_User_Role> Search(int PageIndex, int PageSize, out int total, Expression<Func<View_User_Role, bool>> where = null)
        {
            Expression<Func<View_User_Role, bool>> exp1 = where == null ? item => true : where;
            total = contextItem.Count(exp1);
            IQueryable<View_User_Role> lazyList = contextItem.Where(exp1).OrderByDescending(i => i.RoleId);
            return (PageIndex >= 0) ? lazyList.Skip(PageIndex).Take(PageSize).ToList() : lazyList.ToList();
        }
    }
}

