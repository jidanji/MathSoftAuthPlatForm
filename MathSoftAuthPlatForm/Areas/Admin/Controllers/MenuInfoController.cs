using AutoMapper;
using Business;
using DataAccess;
using DTOModel;
using MathSoftCommonLib;
using MathSoftModelLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;

namespace MAZIKONG.Areas.Admin.Controllers
{
    public class MenuInfoController : AdminBaseController
    {
        [Authorize]
        public ActionResult Index()
        {
            string userName = System.Web.HttpContext.Current.User.Identity.Name;
            int tsthh = 0;
            BLL_View_User_Menu bLL_View_User_Menu = new BLL_View_User_Menu();
            List<View_User_Menu> View_User_Menul = bLL_View_User_Menu.Search(-1, -1, out tsthh, i => i.UserAccount == userName);
            List<UI_Math_Menuinfo> lllll = new List<UI_Math_Menuinfo>();
            foreach (View_User_Menu item in View_User_Menul)
            {
                UI_Math_Menuinfo mmmm = Mapper.Map<UI_Math_Menuinfo>(item);
                lllll.Add(mmmm);
            }

            ViewBag.MenuList = lllll;
            return View();
        }

        [Authorize]
        public ActionResult GetData(int draw, int start, int length, string MenuTitle)
        {
            Expression<Func<Math_MenuInfo, bool>> where = i => true;
            if (!string.IsNullOrWhiteSpace(MenuTitle))
            {
                where = i => i.MenuTitle.Contains(MenuTitle);
            }


            BLL_Math_MenuInfo bLL_Menuinfo = new BLL_Math_MenuInfo();
            int total = 0;
            List<Math_MenuInfo> list = bLL_Menuinfo.Search(start, length, out total, where);

            List<UI_Math_Menuinfo> listUIRoleModel = new List<UI_Math_Menuinfo>();
            foreach (Math_MenuInfo item in list)
            {
                UI_Math_Menuinfo uIRoleModel = Mapper.Map<UI_Math_Menuinfo>(item);
                listUIRoleModel.Add(uIRoleModel);
            }
            datatablesModel<UI_Math_Menuinfo> datatablesModel = new datatablesModel<UI_Math_Menuinfo>()
            {
                data = listUIRoleModel,
                draw = draw,
                recordsFiltered = total,
                recordsTotal = total
            };

            return new JsonResult() { Data = datatablesModel, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        [Authorize]
        public ActionResult AddMenu(string MenuTitle, string MenuURL, string MenuRemark, string MenuIcon,int MenuOrderBy)
        {

            MathRoleAuthorEntities entities = new MathRoleAuthorEntities();
            BLL_Math_MenuInfo bLL_Role = new BLL_Math_MenuInfo();
            UIModelData<Math_MenuInfo> uIModelData = bLL_Role.Add(MenuTitle, MenuURL, MenuRemark, MenuIcon, MenuOrderBy,i => i.MenuTitle == MenuTitle);
            UIModelData<UI_Math_Menuinfo> model = new UIModelData<UI_Math_Menuinfo>
            {
                Data = Mapper.Map<UI_Math_Menuinfo>(uIModelData.Data),
                remark = uIModelData.remark,
                status = uIModelData.status,
                suc = uIModelData.suc
            };
            return new JsonResult() { Data = model, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        [Authorize]
        public ActionResult UpdateMenu(Guid MenuId, string MenuTitle, string MenuURL, string MenuRemark, string MenuIcon,int MenuOrderBy)
        {

            MathRoleAuthorEntities entities = new MathRoleAuthorEntities();
            BLL_Math_MenuInfo bLL_Role = new BLL_Math_MenuInfo();
            UIModelData<Math_MenuInfo> uIModelData = bLL_Role.Update(MenuId, MenuTitle, MenuURL, MenuRemark, MenuIcon, MenuOrderBy,
                i => i.MenuId != MenuId && i.MenuTitle == MenuTitle);

            UIModelData<UI_Math_Menuinfo> model = new UIModelData<UI_Math_Menuinfo>
            {
                Data = Mapper.Map<UI_Math_Menuinfo>(uIModelData.Data),
                remark = uIModelData.remark,
                status = uIModelData.status,
                suc = uIModelData.suc
            };
            return new JsonResult() { Data = model, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        [Authorize]
        public ActionResult DeleteMenu(Guid MenuId)
        {
            UIModelData<string> model = null;
            try
            {
                BLL_Math_MenuInfo bll = new BLL_Math_MenuInfo();
                bll.Delete(MenuId);
                model = new UIModelData<string>()
                {
                    Data = string.Empty,
                    remark = OpCommonString.DeleteSuccess,
                    suc = true
                };
            }

            catch (Exception ex)
            {
                model = new UIModelData<string>()
                {
                    Data = string.Empty,
                    remark = "删除失败" + ex.Message,
                    suc = false
                };
            }
            return new JsonResult() { Data = model, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        [Authorize]
        public ActionResult DeleteMenuBatch(string MenuIds)
        {
            List<Guid> ids = JsonHelper.DeserializeObject<List<Guid>>(MenuIds);
            foreach (Guid item in ids)
            {
                BLL_Math_MenuInfo bll = new BLL_Math_MenuInfo();
                bll.Delete(item);
            }

            UIModelData<string> model1 = new UIModelData<string>()
            {
                remark = "成功删除" + ids.Count() + "条记录",
                suc = true,
                Data = string.Empty
            };
            return new JsonResult() { Data = model1, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
    }
}