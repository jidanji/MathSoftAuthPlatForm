
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
    public class BLL_Math_View_DictAndType: BaseMathRoleAuthorEntities
    {
        private DbSet<Math_View_DictAndType> contextItem = null;
        public BLL_Math_View_DictAndType():base()
        {
            contextItem = context.Math_View_DictAndType;
        }

        public List<Math_View_DictAndType> Search(int PageIndex, int PageSize, out int total, Expression<Func<Math_View_DictAndType, bool>> where = null)
        {
            Expression<Func<Math_View_DictAndType, bool>> exp1 = where == null ? item => true : where;
            total = contextItem.Count(exp1);
            IQueryable<Math_View_DictAndType> lazyList = contextItem.Where(exp1).OrderByDescending(i => i.DictOrderId);
            return (PageIndex >= 0) ? lazyList.Skip(PageIndex).Take(PageSize).ToList() : lazyList.ToList();
        }
    }
}
