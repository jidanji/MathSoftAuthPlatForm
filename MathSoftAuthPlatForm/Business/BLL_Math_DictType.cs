
using DataAccess;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace Business
{
    public class BLL_Math_DictType: BaseMathRoleAuthorEntities
    {
        private DbSet<Math_DictType> contextItem = null;
        public BLL_Math_DictType():base()
        {
            contextItem = context.Math_DictType;
        }

        public List<Math_DictType> Search(int PageIndex, int PageSize, out int total, Expression<Func<Math_DictType, bool>> where = null)
        {
            Expression<Func<Math_DictType, bool>> exp1 = where == null ? item => true : where;
            total = contextItem.Count(exp1);
            IQueryable<Math_DictType> lazyList = contextItem.Where(exp1);
            return PageIndex > 0 ? lazyList.Skip((PageIndex - 1) * PageSize).Take(PageSize).ToList() : lazyList.ToList();
        }
    }
}
