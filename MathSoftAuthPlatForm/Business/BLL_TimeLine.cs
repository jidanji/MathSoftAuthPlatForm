using DataAccess;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace Business
{
    public class BLL_TimeLine: BaseMathRoleAuthorEntities
    {
        private DbSet<TimeLine> contextItem = null;

        public BLL_TimeLine():base()
        {
            contextItem = context.TimeLines;
        }

        /// <summary>
        /// 查询接口
        /// </summary>
        /// <param name="PageIndex"></param>
        /// <param name="PageSize"></param>
        /// <param name="total"></param>
        /// <param name="where"></param>
        /// <returns></returns>
        public List<TimeLine> Search(int PageIndex, int PageSize, out int total, Expression<Func<TimeLine, bool>> where = null)
        {
            Expression<Func<TimeLine, bool>> exp1 = where == null ? item => true : where;
            total = contextItem.Count(exp1);
            IQueryable<TimeLine> layList = contextItem.Where(exp1).OrderBy(item => item.OrderId);
            return PageIndex >= 0 ? layList.Skip(PageIndex).Take(PageSize).ToList() : layList.ToList();
        }

        public List<TimeLine> Update(List<TimeLine> list)
        {
            contextItem.RemoveRange(contextItem);
            context.SaveChanges();

            list.ForEach(item =>
            {
                item.TimelineId = Guid.NewGuid();
                item.InsertTime = DateTime.Now;
                item.LastUpdateTime = DateTime.Now;
            });
            list.ForEach(item =>
            {
                context.TimeLines.Add(item);
                context.SaveChanges();
            });
            return list;
        }

    }

}
