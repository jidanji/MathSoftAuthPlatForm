
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
            int toal = 0;
            Guid idj = Guid.Parse("0D930AF9-76A3-4AB8-A492-580216BA64CC");

            FormsAuthentication.SignOut();
            return View();
        }

        #region 根据用户名和密码验证用户
        /// <summary>
        /// 根据用户名和密码验证用户
        /// </summary>
        /// <param name="UserName"></param>
        /// <param name="UserPWD"></param>
        /// <returns></returns>
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
                    //Response.Write("客户端浏览器信息");
                    //Response.Write("<hr>");
                    //Response.Write("类型：" + b.Type + "<br>");
                    //Response.Write("名称：" + b.Browser + "<br>");
                    //Response.Write("版本：" + b.Version + "<br>");
                    //Response.Write("操作平台：" + b.Platform + "<br>");
                    //Response.Write("是否支持框架：" + b.Frames + "<br>");
                    //Response.Write("是否支持表格" + b.Tables + "<br>");
                    //Response.Write("是否支持COOKIES" + b.Cookies + "<br>");
                    //Response.Write("<hr>");





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

            finally
            {

            }
            return new JsonResult { Data = obj, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        #endregion

        #region 根据cookie验证当前用户的登录信息
        /// <summary>
        /// 根据cookie验证当前用户的登录信息
        /// </summary>
        /// <returns></returns>
        public ActionResult CheckCurrentLoginStatus()
        {
            string username = System.Web.HttpContext.Current.User.Identity.Name;


            if (string.IsNullOrWhiteSpace(username))
            {
                return new JsonResult
                {
                    Data = new UIModelData<string>
                    {
                        Data = string.Empty,
                        remark = "当前用户没有登录",
                        suc = false
                    },
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet
                };
            }
            else
            {
                int total = 0;
                List<Math_UserInfo> list = new BLL_User().Search(-1, -1, out total, i => i.UserAccount == username);
                if (list.Count() == 0)
                {
                    return new JsonResult
                    {
                        Data = new UIModelData<string>
                        {
                            Data = string.Empty,
                            remark = "当前用户没有登录",
                            suc = false
                        },
                        JsonRequestBehavior = JsonRequestBehavior.AllowGet
                    };
                }
                else
                {
                    Math_UserInfo modal = list.FirstOrDefault();
                    UIMathUserInfo modal1 = Mapper.Map<UIMathUserInfo>(modal);


                    return new JsonResult
                    {
                        Data = new UIModelData<UIMathUserInfo>
                        {
                            Data = modal1,
                            remark = string.Empty,
                            suc = true
                        },
                        JsonRequestBehavior = JsonRequestBehavior.AllowGet
                    };
                }
            }

        }
        #endregion
    }
}