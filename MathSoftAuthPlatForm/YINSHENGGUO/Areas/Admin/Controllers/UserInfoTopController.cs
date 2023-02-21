using AutoMapper;
using Business;
using DataAccess;
using DTOModel;
using MathSoftCommonLib;
using MathSoftModelLib;
using MAZIKONG.App_Start;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Web.Mvc;

namespace MAZIKONG.Areas.Admin.Controllers
{
    public class UserInfoTopController : AdminBaseController
    {
        [PreLoadMenuList]
        [Authorize]
        public ActionResult Index()
        {
            //这个是部门信息的
            int total = 0;
            List<Math_Deptinfo> list = new BLL_Math_Deptinfo().Search(-1, -1, out total, i => true);
            List<UI_Math_Deptinfo> list1 = new List<UI_Math_Deptinfo>();
            foreach (Math_Deptinfo item in list)
            {
                list1.Add(Mapper.Map<UI_Math_Deptinfo>(item));
            }
            ViewBag.DeptList = list1;

            //下面是角色信息的

            int total1 = 0;
            List<Math_RoleInfo> listrole = new BLL_Role().Search(-1, -1, out total1, i => true);
            List<UIRoleModel> listUIrole = new List<UIRoleModel>();
            foreach (Math_RoleInfo item in listrole)
            {
                listUIrole.Add(Mapper.Map<UIRoleModel>(item));
            }
            ViewBag.RoleList = listUIrole;

            return View();
        }

        [Authorize]
        public ActionResult GetData(
            int draw,
            int start,
            int length,
            string UserName,
            string  UserAccount,
            string User_Dept_Id,
            string UserPhone)
        {

            Expression<Func<Math_UserInfo, bool>> where1 = i => true;
            if (!string.IsNullOrWhiteSpace(UserName))
            {
                where1 = i => i.UserName.Contains(UserName);
            }

            Expression<Func<Math_UserInfo, bool>> where2 = i => true;

            if (!string.IsNullOrEmpty(UserAccount))
            {
                where2 = i => i.UserAccount.Contains(UserAccount);
            }

            Expression<Func<Math_UserInfo, bool>> where3 = i => true;
            if (!string.IsNullOrEmpty(User_Dept_Id))
            {

                Guid sth = Guid.Parse(User_Dept_Id);
                where3 = i => i.User_Dept_Id == sth;
            }

            Expression<Func<Math_UserInfo, bool>> where4 = i => true;
            if (!string.IsNullOrEmpty(UserPhone))
            {
                where4 = i => i.UserPhone.Contains(UserPhone);
            }
            
            BLL_User bLL_Role = new BLL_User();
            int total = 0;
            List<Math_UserInfo> list = bLL_Role.Search(start, length, out total, where1.And(where2).And(where3).And(where4));

            List<UIMathUserInfo> listUIRoleModel = new List<UIMathUserInfo>();
            foreach (Math_UserInfo item in list)
            {
                UIMathUserInfo uIRoleModel = Mapper.Map<UIMathUserInfo>(item);
                uIRoleModel.Math_Dept_Name = item.Math_Deptinfo.Math_Dept_Name;

                List<UIRoleModel> roleList = new List<UIRoleModel>();
                foreach (Math_User_Role_Select selectModel in item.Math_User_Role_Select)
                {
                    roleList.Add(Mapper.Map<UIRoleModel>(selectModel.Math_RoleInfo));
                }
                uIRoleModel.ListRole = roleList;
                listUIRoleModel.Add(uIRoleModel);
            }
            datatablesModel<UIMathUserInfo> datatablesModel = new datatablesModel<UIMathUserInfo>()
            {
                data = listUIRoleModel,
                draw = draw,
                recordsFiltered = total,
                recordsTotal = total
            };
            return new JsonResult() { Data = datatablesModel, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }


        [Authorize]
        public ActionResult AddUser(
string UserName,
string UserAccount,
string UserPhone,
string UserTel,
string UserPwd,
Guid User_Dept_Id,
string User_Sex,
int User_Area,
String RoleIds
)
        {

            MathRoleAuthorEntities entities = new MathRoleAuthorEntities();
            BLL_User bLL_Role = new BLL_User();
            UIModelData<Math_UserInfo> uIModelData = bLL_Role.Add(UserName, UserAccount, UserPhone,
                UserTel, UserPwd, User_Dept_Id,
                User_Sex,
                User_Area,
                  RoleIds,
                i => i.UserAccount == UserAccount
                );
            UIModelData<UIMathUserInfo> model = new UIModelData<UIMathUserInfo>
            {
                Data = Mapper.Map<UIMathUserInfo>(uIModelData.Data),
                remark = uIModelData.remark,
                status = uIModelData.status,
                suc = uIModelData.suc
            };
            return new JsonResult() { Data = model, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        [Authorize]
        public ActionResult UpdateUser(Guid UserId, string UserName,
string UserAccount,
string UserPhone,
string UserTel,
string UserPwd,
Guid User_Dept_Id,
string User_Sex,
int User_Area,
string RoleIds)
        {

            MathRoleAuthorEntities entities = new MathRoleAuthorEntities();
            BLL_User bLL_Role = new BLL_User();
            UIModelData<Math_UserInfo> uIModelData = bLL_Role.Update(UserId, UserName, UserAccount, UserPhone, UserTel,
                UserPwd, User_Dept_Id, User_Sex, User_Area, RoleIds,
                i => i.UserId != UserId && i.UserAccount == UserAccount);

            UIModelData<UIMathUserInfo> model = new UIModelData<UIMathUserInfo>
            {
                Data = Mapper.Map<UIMathUserInfo>(uIModelData.Data),
                remark = uIModelData.remark,
                status = uIModelData.status,
                suc = uIModelData.suc

            };
            return new JsonResult() { Data = model, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        [Authorize]
        public ActionResult DeleteUser(Guid UserId)
        {
            UIModelData<string> model = null;
            try
            {
                BLL_User bll = new  BLL_User ();
                bll.Delete(UserId);
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
        public ActionResult DeleteUserBatch(string UserIds)
        {
            List<Guid> ids = JsonHelper.DeserializeObject<List<Guid>>(UserIds);
            ids.ForEach(item =>
            {
                try
                {
                    //删除学生
                    BLL_Math_Student bLL_Math_Student = new BLL_Math_Student();
                    bLL_Math_Student.DeleteByTeacher(item);

                    //删除授权关系
                    BLL_Math_User_Role_Select bLL_Math_User_Role_Select = new BLL_Math_User_Role_Select();
                    bLL_Math_User_Role_Select.DeleteRelationShipByUserId(item);

                    BLL_User bll = new BLL_User();
                    bll.Delete(item);
                }
                catch (Exception ex)
                {
                }

            });

            UIModelData<string> model1 = new UIModelData<string>()
            {
                remark = "成功删除" + ids.Count() + "条记录",
                suc = true,
                Data = string.Empty
            };
            return new JsonResult() { Data = model1, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        [Authorize]
        public ActionResult ChangePWDIndex()
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
        public ActionResult ChangePWD(string oldPWD, string newPWD)
        {
            UIModelData<string> model1 = null;
            string UserName = System.Web.HttpContext.Current.User.Identity.Name;
            BLL_User bLL_User = new BLL_User();
            bool ret = bLL_User.ValidUser(UserName, oldPWD);

            if (ret)
            {
                MathRoleAuthorEntities context = new MathRoleAuthorEntities();
                DbSet<Math_UserInfo> contextItem = context.Math_UserInfo;
                Math_UserInfo model = contextItem.FirstOrDefault(item => item.UserAccount == UserName);
                model.UserPwd = newPWD;
                model.UserUpdateTime = DateTime.Now;
                context.SaveChanges();

                model1 = new UIModelData<string>()
                {
                    remark = "修改成功",
                    suc = true,
                    Data = string.Empty
                };
            }
            else
            {
                model1 = new UIModelData<string>()
                {
                    remark = "修改失败,旧密码错误",
                    suc = false,
                    Data = string.Empty
                };
            }
            return new JsonResult { Data = model1, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
    }
}