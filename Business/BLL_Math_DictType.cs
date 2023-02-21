using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

using DataAccess;

namespace Business
{
    public class BLL_Math_DictType
    {
        private MathRoleAuthorEntities context = null;
        private DbSet<Math_DictType> contextItem = null;
        public BLL_Math_DictType()
        {
            context = new MathRoleAuthorEntities();
            contextItem = context.Math_DictType;
        }


        public List<Math_DictType> Search(int PageIndex, int PageSize, out int total, Expression<Func<Math_DictType, bool>> where = null)
        {
            Expression<Func<Math_DictType, bool>> exp1 = where == null? item => true: where;
            total = contextItem.Count(exp1);
            if (PageIndex > 0)
            {
                return contextItem.Where(exp1).Skip((PageIndex - 1) * PageSize).Take(PageSize).ToList();
            }
            else
            {
                return contextItem.Where(exp1).ToList();
            }
        }
    }
}
