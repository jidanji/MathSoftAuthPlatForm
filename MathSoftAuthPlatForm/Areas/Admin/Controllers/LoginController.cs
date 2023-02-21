
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
            var list = new BLL_Math_Dict().Search(-1, -1, out toal, i => i.DictTypeId == idj);
            var m1 = list.FirstOrDefault();
            ViewBag.important = m1 == null ? "欢迎使用" : m1.DictRemark;

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

            finally
            {

            }
            return new JsonResult { Data = obj, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
    }
}