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
    public class BLL_View_User_Menu: BaseMathRoleAuthorEntities
    {
        private DbSet<View_User_Menu> contextItem = null;
        public BLL_View_User_Menu() : base()
        {
            contextItem = context.View_User_Menu;
        }

        /// <summary>
        /// 分页或者不分页查询数据
        /// </summary>
        /// <param name="PageIndex"></param>
        /// <param name="PageSize"></param>
        /// <param name="total"></param>
        /// <param name="where"></param>
        /// <returns></returns>
        public List<View_User_Menu> Search(int PageIndex, int PageSize, out int total, Expression<Func<View_User_Menu, bool>> where = null)
        {
            List<View_User_Menu> list = new List<View_User_Menu>();
            Expression<Func<View_User_Menu, bool>> exp1 = where == null ? item => true : where;

            total = contextItem.Count(exp1);
            IQueryable<View_User_Menu> lazyList = contextItem.Where(exp1).OrderByDescending(i => i.MenuOrderBy);
            list = (PageIndex >= 0) ? lazyList.Skip(PageIndex).Take(PageSize).ToList() : lazyList.ToList();

            //menu去重复
            var MenuIds = list.Select(item => item.MenuId).Distinct().ToList();
            List<View_User_Menu> listRet = new List<View_User_Menu>();
            MenuIds.ForEach(menu => { listRet.Add(list.Where(item => item.MenuId == menu).First()); });

            return listRet;
        }
    }
}
