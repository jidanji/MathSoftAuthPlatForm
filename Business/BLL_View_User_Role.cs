
using DataAccess;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace Business
{
    public class BLL_View_User_Role
    {
        private MathRoleAuthorEntities context = null;
        private DbSet<View_User_Role> contextItem = null;
        public BLL_View_User_Role()
        {
            context = new MathRoleAuthorEntities();
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
            if (PageIndex >= 0)
            {
                return contextItem.Where(exp1).OrderByDescending(i => i.RoleId).Skip(PageIndex).Take(PageSize).ToList();
            }
            else
            {
                return contextItem.Where(exp1).OrderByDescending(i => i.RoleId).ToList();
            }
        }
    }
}

