using DataAccess;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace Business
{
    public class BLL_View_User_Menu
    {
        private MathRoleAuthorEntities context = null;
        private DbSet<View_User_Menu> contextItem = null;
        public BLL_View_User_Menu()
        {
            context = new MathRoleAuthorEntities();
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
            if (PageIndex >= 0)
            {
                list = contextItem.Where(exp1).OrderByDescending(i => i.MenuOrderBy).Skip(PageIndex).Take(PageSize).ToList();
            }
            else
            {
                list = contextItem.Where(exp1).OrderByDescending(i => i.MenuOrderBy).ToList();
            }

            foreach (var item in list) {
                if (item.PageType == 2)
                {
                    item.MenuURL = "/Admin/OuterPage/Index?url=" + HttpUtility.UrlEncode(item.MenuURL, System.Text.Encoding.GetEncoding(936)) ;
                }
            }
            //下面是menu去重复的过程。
            var MenuIds = list.Select(item => item.MenuId).Distinct().ToList();
            List<View_User_Menu> listRet = new List<View_User_Menu>();
            MenuIds.ForEach(menu => { listRet.Add(list.Where(item => item.MenuId == menu).First()); });
            return listRet;
        }
    }
}
