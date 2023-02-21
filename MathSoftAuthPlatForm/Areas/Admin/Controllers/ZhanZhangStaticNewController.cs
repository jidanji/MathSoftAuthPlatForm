using AutoMapper;
using Business;
using DataAccess;
using DTOModel;
using MathSoftCommonLib;
using MathSoftModelLib;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;

namespace MAZIKONG.Areas.Admin.Controllers
{
    public class ZhanZhangStaticNewController : Controller
    {
        [Authorize]
        // GET: Admin/ZhanZhangStatic
        public ActionResult Index()
        {
            string userName = System.Web.HttpContext.Current.User.Identity.Name;
            int tsthh = 0;
            BLL_View_User_Menu bLL_View_User_Menu = new BLL_View_User_Menu();
            List<View_User_Menu> View_User_Menul = bLL_View_User_Menu.Search(-1, -1, out tsthh, i => i.UserAccount == userName);
            List<UI_Math_Menuinfo> lllll = View_User_Menul.Select(item => Mapper.Map<UI_Math_Menuinfo>(item)).ToList();
            ViewBag.MenuList = lllll;



            int total = 0;

            //用户相关的信息
            List<Math_UserInfo> list = new BLL_User().Search(-1, -1, out total, i => true);
            List<UIMathUserInfo> list1 = list.Select(item => Mapper.Map<UIMathUserInfo>(item)).ToList();

            ViewBag.UserList = list1;

            //功能相关的信息
            List<Math_MenuInfo> menulist = new BLL_Math_MenuInfo().Search(-1, -1, out total, i => true);
            List<UI_Math_Menuinfo> uimenulist = menulist.Select(item => Mapper.Map<UI_Math_Menuinfo>(item)).ToList();

            ViewBag.MenuList1 = uimenulist;

            return View();
        }




        [Authorize]
        public ActionResult GetData(int draw,
    int start,
    int length,
   string ispipei,
   string ZhanZhangDaQu, 
   string studentInfo
            )
        {
            Expression<Func<ZhanZhangStaticNew, bool>> e1 = i => true;

            Expression<Func<ZhanZhangStaticNew, bool>> e2 = i => true;

            Expression<Func<ZhanZhangStaticNew, bool>> e3 = i => true;
            if (!string.IsNullOrEmpty(ZhanZhangDaQu))
            {
                e3 = i => i.zhanzhangdaqu == ZhanZhangDaQu;
            }

            Expression<Func<ZhanZhangStaticNew, bool>> e4 = i => true;
            if (!string.IsNullOrEmpty(studentInfo))
            {
                e4 = i => i.StudentName.Contains(studentInfo) || i.IdCard.Contains(studentInfo);
            }
            if (!string.IsNullOrEmpty(ispipei))
            {
                if (ispipei == "0")
                {
                    e1 = i => (string.IsNullOrEmpty(i.zhanzhangdaqu)) || (string.IsNullOrEmpty(i.zhanzhangdaqu));
                }
                else
                {
                    e1 = i => (!string.IsNullOrEmpty(i.zhanzhangdaqu)) && (!string.IsNullOrEmpty(i.zhanzhangdaqu));
                }
            }
            int total = 0;
            List<ZhanZhangStaticNew> list = new BLL_ZhanZhangStaticNew().Search(
                start,
                length,
               out total,
               e1.And(e2).And(e3).And(e4)
                );
            List<DTO_View_ZhanZhangStatic> list1 = list.Select(item => Mapper.Map<DTO_View_ZhanZhangStatic>(item)).ToList();
            return new JsonResult
            {
                Data =
                new datatablesModel<DTO_View_ZhanZhangStatic>
                {
                    data = list1,
                    draw = draw,
                    recordsFiltered = total,
                    recordsTotal = total
                },
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }

        [Authorize]
        public ActionResult GetExcel(string ispipei,
   string ZhanZhangDaQu, string
studentInfo)
        {
            Expression<Func<View_ZhanZhangStatic, bool>> e1 = i => true;

            Expression<Func<View_ZhanZhangStatic, bool>> e2 = i => true;

            Expression<Func<View_ZhanZhangStatic, bool>> e3 = i => true;
            if (!string.IsNullOrEmpty(ZhanZhangDaQu))
            {
                e3 = i => i.zhanzhangdaqu == ZhanZhangDaQu;
            }

            Expression<Func<View_ZhanZhangStatic, bool>> e4 = i => true;
            if (!string.IsNullOrEmpty(studentInfo))
            {
                e4 = i => i.StudentName.Contains(studentInfo) || i.IdCard.Contains(studentInfo);
            }
            if (!string.IsNullOrEmpty(ispipei))
            {
                if (ispipei == "0")
                {
                    e1 = i => (string.IsNullOrEmpty(i.zhanzhang)) || (string.IsNullOrEmpty(i.zhanzhang));
                }
                else
                {
                    e1 = i => (!string.IsNullOrEmpty(i.zhanzhang)) && (!string.IsNullOrEmpty(i.zhanzhang));
                }
            }

      





            int total = 0;
            List<View_ZhanZhangStatic> list = new BLL_View_ZhanZhangStatic().Search(
                -1,
                -1,
                out total,
                e1.And(e2).And(e3).And(e4)
                );
            List<DTO_View_ZhanZhangStatic> list1 = list.Select(item => Mapper.Map<DTO_View_ZhanZhangStatic>(item)).ToList();

            NPOI.HSSF.UserModel.HSSFWorkbook book = new NPOI.HSSF.UserModel.HSSFWorkbook();
            //添加一个sheet
            NPOI.SS.UserModel.ISheet sheet1 = book.CreateSheet("Sheet1");
            //给sheet1添加第一行的头部标题
            NPOI.SS.UserModel.IRow row1 = sheet1.CreateRow(0);
            row1.CreateCell(0).SetCellValue("序号");
            row1.CreateCell(1).SetCellValue("学生身份证");
            row1.CreateCell(2).SetCellValue("学生姓名");
            row1.CreateCell(3).SetCellValue("毕业学校");
            row1.CreateCell(4).SetCellValue("字典值");

            row1.CreateCell(5).SetCellValue("站长大区");
            row1.CreateCell(6).SetCellValue("站长区域");
            row1.CreateCell(7).SetCellValue("站长");



            for (int i = 0; i < list1.Count; i++)
            {
                NPOI.SS.UserModel.IRow rowtemp = sheet1.CreateRow(i + 1);
                rowtemp.CreateCell(0).SetCellValue((i + 1).ToString());//序号
                rowtemp.CreateCell(1).SetCellValue(list1[i].IdCard);//学生身份证
                rowtemp.CreateCell(2).SetCellValue(list1[i].StudentName);//学生姓名
                rowtemp.CreateCell(3).SetCellValue(list1[i].GrandSchool);//学生姓名


                rowtemp.CreateCell(4).SetCellValue(list1[i].DictValue);//学生姓名
                rowtemp.CreateCell(5).SetCellValue(list1[i].zhanzhangdaqu);//

                rowtemp.CreateCell(6).SetCellValue(list1[i].zhanzhangquyu);//学生性别
                rowtemp.CreateCell(7).SetCellValue(list1[i].zhanzhang);//毕业学校
              




            }


            List<StaticUI> ret = new BLL_View_ZhanZhangStatic().Static();
            ////////////////////////////////////////////////////////////////////
            NPOI.SS.UserModel.ISheet sheet2 = book.CreateSheet("统计分析数据");
            //给sheet1添加第一行的头部标题
            NPOI.SS.UserModel.IRow row2 = sheet2.CreateRow(0);
            row2.CreateCell(0).SetCellValue("序号");
            row2.CreateCell(1).SetCellValue("站长区域");
            row2.CreateCell(2).SetCellValue("站长");
            row2.CreateCell(3).SetCellValue("部门");
            row2.CreateCell(4).SetCellValue("命中人数");


            for (int i = 0; i < ret.Count; i++)
            {
                NPOI.SS.UserModel.IRow rowtemp = sheet2.CreateRow(i + 1);
                rowtemp.CreateCell(0).SetCellValue((i + 1).ToString());//序号
                rowtemp.CreateCell(1).SetCellValue(ret[i].ZhanZhangDaQu);//学生身份证
                rowtemp.CreateCell(2).SetCellValue(ret[i].ZhanZhang);//学生身份证
                rowtemp.CreateCell(3).SetCellValue(ret[i].Math_Dept_Name);//学生姓名
                rowtemp.CreateCell(4).SetCellValue(ret[i].Value);//学生性别


            }
            // 写入到客户端 
            System.IO.MemoryStream ms = new System.IO.MemoryStream();
            book.Write(ms);
            ms.Seek(0, SeekOrigin.Begin);




            return File(ms, "application/vnd.ms-excel", "站长结果导出.xls");


        }

        [Authorize]
        public ActionResult GetStaticData()
        {
            BLL_View_ZhanZhangStatic bll = new BLL_View_ZhanZhangStatic();
            List<StaticUI> ret = bll.Static();
            return new JsonResult()
            {
                Data =
                new
                {
                    draw = int.Parse(Request["draw"]),
                    recordsTotal = ret.Count,
                    recordsFiltered = ret.Count,
                    data = ret
                },
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }

        [Authorize]
        public ActionResult start() {

            try
            {
                new BLL_View_ZhanZhangStatic().Start();
                return new JsonResult() { Data = 1000, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
            catch (Exception ex)
            {
                return new JsonResult() { Data = ex.ToString(), JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
        }
    }
}