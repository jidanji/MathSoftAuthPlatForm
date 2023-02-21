
using AutoMapper;
using Business;
using DataAccess;
using DTOModel;
using MathSoftModelLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.Security;

namespace MAZIKONG.Areas.Admin.Controllers
{
    public class LoginController : AdminBaseController
    {
        // GET: Admin/Login
        public ActionResult Index()
        {
            var b = Request.Browser;
            ViewBag.BrowserType = b.Type;
            return View();
        }
        public ActionResult ValidUser(String UserName, string UserPWD)
        {
            UIModelData<String> obj = null;
            try
            {
                bool ret = new BLL_User().ValidUser(UserName, UserPWD);
                String remark = string.Empty;
                if (!ret)
                {
                    remark = "登录失败，账号或者密码错误";
                }
                else
                {
                    var b = Request.Browser;
                    new BLL_BrowserInfo().Add(b.Browser, b.Version, UserName
                        , b.Type, b.Platform,
                        b.Frames.ToString(),
                        b.Tables.ToString(),
                        b.Cookies.ToString()
                         );
                    FormsAuthentication.SetAuthCookie(UserName, false);
                    int tsthh = 0;

                    BLL_View_User_Menu bLL_View_User_Menu = new BLL_View_User_Menu();

                    List<View_User_Menu> View_User_Menul = bLL_View_User_Menu.Search(-1, -1, out tsthh, i => i.UserAccount == UserName);
                    List<UI_Math_Menuinfo> lllll = View_User_Menul.Select(item => Mapper.Map<UI_Math_Menuinfo>(item)).ToList();
                    var model = lllll.OrderByDescending(item => item.MenuOrderBy).FirstOrDefault();
                    remark = model == null ? string.Empty : model.MenuURL;
                }
                obj = new UIModelData<string>
                {
                    Data = remark,
                    remark = remark,
                    suc = ret
                };
            }

            catch (Exception ex)
            {
                obj = new UIModelData<string>
                {
                    Data = ex.ToString(),
                    remark = ex.ToString(),
                    suc = false
                };
            }
            return new JsonResult { Data = obj, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
    }
}