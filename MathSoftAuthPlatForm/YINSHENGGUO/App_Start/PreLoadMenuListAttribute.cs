using AutoMapper;
using Business;
using DataAccess;
using DTOModel;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace MAZIKONG.App_Start
{
    #region  预加载菜单选项
    /// <summary>
    /// 预加载菜单选项
    /// </summary>
    public class PreLoadMenuListAttribute : FilterAttribute, IResultFilter
    {
        public void OnResultExecuted(ResultExecutedContext filterContext)
        {
        }

        public void OnResultExecuting(ResultExecutingContext filterContext)
        {
            int menuCount = 0;
            string loginName = System.Web.HttpContext.Current.User.Identity.Name;
            List<View_User_Menu> menuList = new BLL_View_User_Menu().Search(-1, -1, out menuCount, i => i.UserAccount == loginName);
            List<UI_Math_Menuinfo> menuListDTO = menuList.Select(item => Mapper.Map<UI_Math_Menuinfo>(item)).ToList();
            filterContext.Controller.ViewBag.MenuList = menuListDTO;
        }
    }
    #endregion
}
