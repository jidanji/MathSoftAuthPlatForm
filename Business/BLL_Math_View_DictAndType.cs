using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

using DataAccess;

namespace Business
{
    public class BLL_Math_View_DictAndType
    {
        private MathRoleAuthorEntities context = null;
        private DbSet<Math_View_DictAndType> contextItem = null;

        public BLL_Math_View_DictAndType()
        {
            context = new MathRoleAuthorEntities();
            contextItem = context.Math_View_DictAndType;
        }

        public List<Math_View_DictAndType> Search(int PageIndex, int PageSize, out int total, Expression<Func<Math_View_DictAndType, bool>> where = null)
        {
            Expression<Func<Math_View_DictAndType, bool>> exp1 = where == null ? item => true : where;
            total = contextItem.Count(exp1);
            if (PageIndex >= 0)
            {
                return contextItem.Where(exp1).OrderByDescending(i => i.DictOrderId).Skip(PageIndex).Take(PageSize).ToList();
            }
            else
            {
                return contextItem.Where(exp1).OrderByDescending(i => i.DictOrderId).ToList();
            }
        }
    }
}
