using AutoMapper;
using Business;
using DataAccess;
using DTOModel;
using MathSoftCommonLib;
using MathSoftModelLib;
using MAZIKONG.App_Start;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;

namespace MAZIKONG.Areas.Admin.Controllers
{
    public class HomeController : AdminBaseController
    {
        [PreLoadMenuList]
        [Authorize]
        public ActionResult Index()
        {
            return View();
        }

        [Authorize]
        public ActionResult Index2()
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


            //这个是部门信息的

            Math_UserInfo userinfo = new BLL_User().GetSingleByUserAccount(System.Web.HttpContext.Current.User.Identity.Name);

            List<UI_Math_Deptinfo> list1 = new List<UI_Math_Deptinfo>();
            if (userinfo.User_Area != 2)
            {
                //本部门
                list1.Add(Mapper.Map<UI_Math_Deptinfo>(userinfo.Math_Deptinfo));
            }
            else
            {
                int total = 0;
                List<Math_Deptinfo> list = new BLL_Math_Deptinfo().Search(-1, -1, out total, i => true);
                foreach (Math_Deptinfo item in list)
                {
                    list1.Add(Mapper.Map<UI_Math_Deptinfo>(item));
                }
            }
           
            ViewBag.DeptList = list1;

            return View();
        }

        [Authorize]
        public ActionResult Index3()
        {
            string userName = System.Web.HttpContext.Current.User.Identity.Name;
            int tsthh = 0;
            BLL_View_User_Menu bLL_View_User_Menu = new BLL_View_User_Menu();
            List<View_User_Menu> View_User_Menul = bLL_View_User_Menu.Search(-1, -1, out tsthh, i => i.UserAccount == userName);
            List<UI_Math_Menuinfo> lllll = View_User_Menul.Select(item => Mapper.Map<UI_Math_Menuinfo>(item)).ToList();
            ViewBag.MenuList = lllll;


            //这个是部门信息的
            int total = 0;
            List<Math_Deptinfo> list = new BLL_Math_Deptinfo().Search(-1, -1, out total, i => true);
            List<UI_Math_Deptinfo> list1 = list.Select(item => Mapper.Map<UI_Math_Deptinfo>(item)).ToList();
            ViewBag.DeptList = list1;
            return View();
        }

        [Authorize]
        public ActionResult AddMathWork(string WorkName, string JiaotongFangshi, string Target, string ShoolName, string PointWhoNumber, string PointWho, int Fee, DateTime BegTime, DateTime EndTime, string Remark)
        {
            BLL_Dict bLL_Dcit = new BLL_Dict();
            var dict1 = bLL_Dcit.Dict;

            Math_UserInfo userinfo = new BLL_User().GetSingleByUserAccount(System.Web.HttpContext.Current.User.Identity.Name);



            Math_Work work = new Math_Work()
            {
                WorkId = Guid.NewGuid(),
                WorkName = WorkName,
                JiaotongFangshi = JiaotongFangshi,
                Target = Target,
                ShoolName = ShoolName,
                PointWhoNumber = PointWhoNumber,
                PointWho = PointWho,
                Fee = Fee,
                BegTime = BegTime,
                EndTime = EndTime,
                Remark = Remark,
                Status = 1,
                CreateDeptId = userinfo.Math_Deptinfo.Math_Dept_Id,
                UserName = userinfo.UserName,
                UserAccount = System.Web.HttpContext.Current.User.Identity.Name,
                UserTelNumber = userinfo.UserPhone,
                UserDeptName = userinfo.Math_Deptinfo.Math_Dept_Name,
                ShenPiRiQi = DateTime.Now,
                CreateTime = DateTime.Now,
            };
            MathRoleAuthorEntities entities = new MathRoleAuthorEntities();
            BLL_Math_Work bLL_Role = new BLL_Math_Work();
            UIModelData<Math_Work> uIModelData = bLL_Role.Add(work, i => true);

            BLL_Math_Work_History bll_Math_Work_History = new BLL_Math_Work_History();
            bll_Math_Work_History.Add(work.WorkId, userinfo.UserName, userinfo.UserPhone, dict1[1], dict1[1],
                i => false
                );
            UIModelData<UIMath_Work> model = new UIModelData<UIMath_Work>()
            {
                Data = Mapper.Map<UIMath_Work>(uIModelData.Data),
                remark = uIModelData.remark,
                status = uIModelData.status,
                suc = uIModelData.suc

            };
            return new JsonResult() { Data = model, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        [Authorize]
        public ActionResult UpdateMathWork(Guid WorkId, string WorkName, string JiaotongFangshi, string Target, string ShoolName, string PointWhoNumber, string PointWho, int Fee, DateTime BegTime, DateTime EndTime, string Remark)
        {
            Math_UserInfo userinfo = new BLL_User().GetSingleByUserAccount(System.Web.HttpContext.Current.User.Identity.Name);

            MathRoleAuthorEntities entities = new MathRoleAuthorEntities();
            BLL_Math_Work bLL_Role = new BLL_Math_Work();
            UIModelData<Math_Work> uIModelData = bLL_Role.Update
           (WorkId,
           WorkName,
           JiaotongFangshi,
           Target,
           ShoolName,
           PointWhoNumber,
           PointWho,
           Fee,
           BegTime,
           EndTime,
           Remark,
           i => true);


            BLL_Math_Work_History bll_Math_Work_History = new BLL_Math_Work_History();
            bll_Math_Work_History.Add(WorkId, userinfo.UserName, userinfo.UserPhone, "修改", "修改",
                i => false
                );

            UIModelData<UIMath_Work> model = new UIModelData<UIMath_Work>()
            {
                Data = Mapper.Map<UIMath_Work>(uIModelData.Data),
                remark = uIModelData.remark,
                status = uIModelData.status,
                suc = uIModelData.suc
            };
            return new JsonResult() { Data = model, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        [Authorize]
        public ActionResult GetData(int draw, int start, int length,
string searchBegDate,
string searchEndDate,
string searchStatus
)
        {
            Expression<Func<Math_Work, bool>> where = i => true;
            where = i => i.UserAccount == System.Web.HttpContext.Current.User.Identity.Name;


            Expression<Func<Math_Work, bool>> where1 = i => true;
            if (!string.IsNullOrEmpty(searchBegDate))
            {
                DateTime beg = DateTime.Parse(searchBegDate);
                where1 = i => i.ShenPiRiQi >= beg;
            }


            Expression<Func<Math_Work, bool>> where2 = i => true;
            if (!string.IsNullOrEmpty(searchEndDate))
            {
                DateTime end = DateTime.Parse(searchEndDate).AddDays(1);
                where2 = i => i.ShenPiRiQi < end;
            }

            Expression<Func<Math_Work, bool>> where3 = i => true;
            if (!string.IsNullOrEmpty(searchStatus))
            {
                int s = int.Parse(searchStatus);
                where3 = i => i.Status == s;
            }

            BLL_Math_Work bLL_Role = new BLL_Math_Work();
            int total = 0;
            List<Math_Work> list = bLL_Role.Search(start, length, out total, where.And(where1).And(where2).And(where3));

            List<UIMath_Work> listUIRoleModel = new List<UIMath_Work>();
            foreach (Math_Work item in list)
            {
                UIMath_Work uIRoleModel = Mapper.Map<UIMath_Work>(item);

                foreach (Math_Work_History hisItem in item.Math_Work_History.OrderBy(i => i.WorkHisSeq))
                {
                    uIRoleModel.HisList.Add(Mapper.Map<UIMath_Work_History>(hisItem));
                }

                foreach (AttachedMent hisItem in item.AttachedMents.OrderByDescending(i => i.AttchSeq))
                {
                    uIRoleModel.AttachedMentList.Add(Mapper.Map<UI_AttachedMent>(hisItem));
                }

                listUIRoleModel.Add(uIRoleModel);

            }
            datatablesModel<UIMath_Work> datatablesModel = new datatablesModel<UIMath_Work>()
            {
                data = listUIRoleModel,
                draw = draw,
                recordsFiltered = total,
                recordsTotal = total
            };
            return new JsonResult() { Data = datatablesModel, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        [Authorize]
        public ActionResult GetData2(int draw, int start, int length,string searchBegDate,string searchEndDate,string searchStatus,string searchDept)
        {
            Math_UserInfo userinfo = new BLL_User().GetSingleByUserAccount(System.Web.HttpContext.Current.User.Identity.Name);


            Expression<Func<Math_Work, bool>> where1 = i => true;
            where1 = i => i.Status != 1;

            Expression<Func<Math_Work, bool>> where2 = i => true;
            if (userinfo.User_Area != 2)
            {
                where2 = i => i.CreateDeptId == userinfo.User_Dept_Id;
            }
            else
            {
                if (!string.IsNullOrEmpty(searchDept))
                {
                    Guid depteid = Guid.Parse(searchDept);
                    where2 = i => i.CreateDeptId == depteid;
                }
                else
                {

                }
            }

            ///////////////////////////////////////////////////////////////// 7 8 9
            Expression<Func<Math_Work, bool>> where7 = i => true;
            if (!string.IsNullOrEmpty(searchBegDate))
            {
                DateTime beg = DateTime.Parse(searchBegDate);
                where7 = i => i.ShenPiRiQi >= beg;
            }


            Expression<Func<Math_Work, bool>> where8 = i => true;
            if (!string.IsNullOrEmpty(searchEndDate))
            {
                DateTime end = DateTime.Parse(searchEndDate).AddDays(1);
                where8 = i => i.ShenPiRiQi < end;
            }

            Expression<Func<Math_Work, bool>> where9 = i => true;
            if (!string.IsNullOrEmpty(searchStatus))
            {
                int s = int.Parse(searchStatus);
                where9 = i => i.Status == s;
            }
            ///////////////////////////////////////////////////////////

            BLL_Math_Work bLL_Role = new BLL_Math_Work();
            int total = 0;
            List<Math_Work> list = bLL_Role.Search(start, length, out total, where1.And(where2)
                .And(where7)
                .And(where8)
                .And(where9)
                );

            List<UIMath_Work> listUIRoleModel = new List<UIMath_Work>();
            foreach (Math_Work item in list)
            {
                UIMath_Work uIRoleModel = Mapper.Map<UIMath_Work>(item);

                foreach (Math_Work_History hisItem in item.Math_Work_History.OrderBy(i => i.WorkHisSeq))
                {
                    uIRoleModel.HisList.Add(Mapper.Map<UIMath_Work_History>(hisItem));
                }

                foreach (AttachedMent hisItem in item.AttachedMents.OrderByDescending(i => i.AttchSeq))
                {
                    uIRoleModel.AttachedMentList.Add(Mapper.Map<UI_AttachedMent>(hisItem));
                }

                listUIRoleModel.Add(uIRoleModel);

            }
            datatablesModel<UIMath_Work> datatablesModel = new datatablesModel<UIMath_Work>()
            {
                data = listUIRoleModel,
                draw = draw,
                recordsFiltered = total,
                recordsTotal = total
            };
            return new JsonResult() { Data = datatablesModel, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        [Authorize]
        public ActionResult GetData3(int draw, int start, int length,
string searchBegDate,
string searchEndDate,
string searchStatus,
string searchDept)
        {
            Math_UserInfo userinfo = new BLL_User().GetSingleByUserAccount(System.Web.HttpContext.Current.User.Identity.Name);


            Expression<Func<Math_Work, bool>> where1 = i => true;
            where1 = i => i.Status == -3 || i.Status == 3 || i.Status == -4;


            ///////////////////////////////////////////////////////////////// 7 8 9
            Expression<Func<Math_Work, bool>> where7 = i => true;
            if (!string.IsNullOrEmpty(searchBegDate))
            {
                DateTime beg = DateTime.Parse(searchBegDate);
                where7 = i => i.ShenPiRiQi >= beg;
            }


            Expression<Func<Math_Work, bool>> where8 = i => true;
            if (!string.IsNullOrEmpty(searchEndDate))
            {
                DateTime end = DateTime.Parse(searchEndDate).AddDays(1);
                where8 = i => i.ShenPiRiQi < end;
            }

            Expression<Func<Math_Work, bool>> where9 = i => true;
            if (!string.IsNullOrEmpty(searchStatus))
            {
                int s = int.Parse(searchStatus);
                where9 = i => i.Status == s;
            }


            Expression<Func<Math_Work, bool>> where10 = i => true;
            if (!string.IsNullOrEmpty(searchDept))
            {
                Guid searchDeptId = Guid.Parse(searchDept);
                where10 = i => i.CreateDeptId == searchDeptId;
            }


            ///////////////////////////////////////////////////////////






            BLL_Math_Work bLL_Role = new BLL_Math_Work();
            int total = 0;
            List<Math_Work> list = bLL_Role.Search(start, length, out total, where1
                .And(where7)
                .And(where8)
                .And(where9)
                .And(where10));

            List<UIMath_Work> listUIRoleModel = new List<UIMath_Work>();
            foreach (Math_Work item in list)
            {
                UIMath_Work uIRoleModel = Mapper.Map<UIMath_Work>(item);

                foreach (Math_Work_History hisItem in item.Math_Work_History.OrderBy(i => i.WorkHisSeq))
                {
                    uIRoleModel.HisList.Add(Mapper.Map<UIMath_Work_History>(hisItem));
                }

                foreach (AttachedMent hisItem in item.AttachedMents.OrderByDescending(i => i.AttchSeq))
                {
                    uIRoleModel.AttachedMentList.Add(Mapper.Map<UI_AttachedMent>(hisItem));
                }

                listUIRoleModel.Add(uIRoleModel);

            }
            datatablesModel<UIMath_Work> datatablesModel = new datatablesModel<UIMath_Work>()
            {
                data = listUIRoleModel,
                draw = draw,
                recordsFiltered = total,
                recordsTotal = total
            };
            return new JsonResult() { Data = datatablesModel, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }


        [Authorize]
        public ActionResult GetData4(int draw, int start, int length,
            string searchBegDate,
string searchEndDate,
string searchDept
            )
        {
            Math_UserInfo userinfo = new BLL_User().GetSingleByUserAccount(System.Web.HttpContext.Current.User.Identity.Name);


            Expression<Func<Math_Work, bool>> where1 = i => true;
            where1 = i => i.Status == 3;

            Expression<Func<Math_Work, bool>> where2 = i => true;
            if (userinfo.User_Area != 2)
            {
                where2 = i => i.CreateDeptId == userinfo.User_Dept_Id;
            }
            else
            {
                if (!string.IsNullOrEmpty(searchDept))
                {
                    Guid depteid = Guid.Parse(searchDept);
                    where2 = i => i.CreateDeptId == depteid;
                }
            }

            ///////////////////////////////////////////////////////////////// 7 8 9
            Expression<Func<Math_Work, bool>> where7 = i => true;
            if (!string.IsNullOrEmpty(searchBegDate))
            {
                DateTime beg = DateTime.Parse(searchBegDate);
                where7 = i => i.ShenPiRiQi >= beg;
            }


            Expression<Func<Math_Work, bool>> where8 = i => true;
            if (!string.IsNullOrEmpty(searchEndDate))
            {
                DateTime end = DateTime.Parse(searchEndDate).AddDays(1);
                where8 = i => i.ShenPiRiQi < end;
            }
            ///////////////////////////////////////////////////////////




            BLL_Math_Work bLL_Role = new BLL_Math_Work();
            int total = 0;
            List<Math_Work> list = bLL_Role.Search(start, length, out total, where1
                .And(where2)
                .And(where7)
                .And(where8));

            List<UIMath_Work> listUIRoleModel = new List<UIMath_Work>();
            foreach (Math_Work item in list)
            {
                UIMath_Work uIRoleModel = Mapper.Map<UIMath_Work>(item);

                foreach (Math_Work_History hisItem in item.Math_Work_History.OrderBy(i => i.WorkHisSeq))
                {
                    uIRoleModel.HisList.Add(Mapper.Map<UIMath_Work_History>(hisItem));
                }

                foreach (AttachedMent hisItem in item.AttachedMents.OrderByDescending(i => i.AttchSeq))
                {
                    uIRoleModel.AttachedMentList.Add(Mapper.Map<UI_AttachedMent>(hisItem));
                }

                listUIRoleModel.Add(uIRoleModel);

            }
            datatablesModel<UIMath_Work> datatablesModel = new datatablesModel<UIMath_Work>()
            {
                data = listUIRoleModel,
                draw = draw,
                recordsFiltered = total,
                recordsTotal = total
            };
            return new JsonResult() { Data = datatablesModel, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        [Authorize]
        public ActionResult DeleteMathWork(Guid WorkId)
        {
            UIModelData<string> model = null;
            try
            {
                BLL_Math_Work bll = new BLL_Math_Work();
                bll.Delete(WorkId);
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
        public ActionResult Send2SHengPi(Guid WorkId)
        {
            UIModelData<string> model = null;

            BLL_Math_Work bll = new BLL_Math_Work();
            bll.Send2SHengPi(WorkId);
            model = new UIModelData<string>()
            {
                Data = string.Empty,
                remark = "提交成功",
                suc = true
            };

            Math_UserInfo userinfo = new BLL_User().GetSingleByUserAccount(System.Web.HttpContext.Current.User.Identity.Name);
            BLL_Math_Work_History bll_Math_Work_History = new BLL_Math_Work_History();
            bll_Math_Work_History.Add(WorkId, userinfo.UserName, userinfo.UserPhone, "提交部门审批", "提交部门审批",
                i => false
                );

            return new JsonResult() { Data = model, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }


        [Authorize]
        public ActionResult Send3SHengPi(Guid SendWorkId, int InfactFee)
        {
            UIModelData<string> model = null;

            BLL_Math_Work bll = new BLL_Math_Work();
            bll.Send3SHengPi(SendWorkId, InfactFee);
            model = new UIModelData<string>()
            {
                Data = string.Empty,
                remark = "提交成功",
                suc = true
            };

            Math_UserInfo userinfo = new BLL_User().GetSingleByUserAccount(System.Web.HttpContext.Current.User.Identity.Name);
            BLL_Math_Work_History bll_Math_Work_History = new BLL_Math_Work_History();
            bll_Math_Work_History.Add(SendWorkId, userinfo.UserName, userinfo.UserPhone, "提交招办审批", "提交招办审批",
                i => false
                );

            return new JsonResult() { Data = model, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        [Authorize]
        public ActionResult ShenPi2SHengPi(Guid WorkId, int Status, string Remark)
        {
            UIModelData<string> model = null;

            BLL_Math_Work bll = new BLL_Math_Work();
            bll.ShenPi2SHengPi(WorkId, Status);
            model = new UIModelData<string>()
            {
                Data = string.Empty,
                remark = "提交成功",
                suc = true
            };

            Math_UserInfo userinfo = new BLL_User().GetSingleByUserAccount(System.Web.HttpContext.Current.User.Identity.Name);
            BLL_Math_Work_History bll_Math_Work_History = new BLL_Math_Work_History();
            bll_Math_Work_History.Add(WorkId, userinfo.UserName, userinfo.UserPhone, new BLL_Dict().Dict[Status], Remark,
                i => false
                );

            return new JsonResult() { Data = model, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        [Authorize]
        public ActionResult ShenPi3SHengPi(Guid WorkId, int Status, string Remark)
        {
            UIModelData<string> model = null;

            BLL_Math_Work bll = new BLL_Math_Work();
            bll.ShenPi2SHengPi(WorkId, Status);
            model = new UIModelData<string>()
            {
                Data = string.Empty,
                remark = "提交成功",
                suc = true
            };

            Math_UserInfo userinfo = new BLL_User().GetSingleByUserAccount(System.Web.HttpContext.Current.User.Identity.Name);
            BLL_Math_Work_History bll_Math_Work_History = new BLL_Math_Work_History();
            if (Status == 2)
            {
                bll_Math_Work_History.Add(WorkId, userinfo.UserName, userinfo.UserPhone, "驳回", Remark,
            i => false
            );
            }
            else
            {
                bll_Math_Work_History.Add(WorkId, userinfo.UserName, userinfo.UserPhone, new BLL_Dict().Dict[Status], Remark,
            i => false
            );
            }
        

            return new JsonResult() { Data = model, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        [Authorize]
        public ActionResult UpLoadFile(string AttchedType,
Guid FileWorkId,
string AttchedRemark)
        {



            string extention = string.Empty;

            string savefileName = string.Empty;
            string sourceUrl = string.Empty;
            HttpPostedFileBase file = Request.Files[0];
            extention = file.FileName.Split('.').Last();
            savefileName = DateTime.Now.ToString("yyyyMMddHHmmss") + new Random().Next(10000, 99999).ToString() + "." + extention;
            sourceUrl = "/Admin/Source/" + savefileName;

            string desurl = "/Admin/Upload/" + savefileName;
            file.SaveAs(Server.MapPath(sourceUrl));
            #region 马良对数据进行压缩
            BitMapCompressor.Compress(new Bitmap(Server.MapPath(sourceUrl)),Server.MapPath(desurl), 30);
            #endregion


            BLL_AttachedMent bll = new BLL_AttachedMent();
            UIModelData<AttachedMent> uIModelData = bll.Add(FileWorkId, desurl, AttchedType, AttchedRemark, i => false);

            UIModelData<UI_AttachedMent> model = new UIModelData<UI_AttachedMent>()
            {
                Data = Mapper.Map<UI_AttachedMent>(uIModelData.Data),
                remark = uIModelData.remark,
                status = uIModelData.status,
                suc = uIModelData.suc
            };
            return new JsonResult() { Data = model, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
    }
}