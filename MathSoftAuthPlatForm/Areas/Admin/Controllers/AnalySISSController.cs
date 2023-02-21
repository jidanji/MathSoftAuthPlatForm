using AutoMapper;
using Business;
using DataAccess;
using DTOModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MathSoftCommonLib;
using MathSoftModelLib;
using System.Linq.Expressions;
using System.IO;

namespace MAZIKONG.Areas.Admin.Controllers
{
    public class AnalySISSController : AdminBaseController
    {

        [Authorize]
        public ActionResult GetDetailExcel(string Name)
        {
            NPOI.HSSF.UserModel.HSSFWorkbook book = new NPOI.HSSF.UserModel.HSSFWorkbook();
            //添加一个sheet
            NPOI.SS.UserModel.ISheet sheet1 = book.CreateSheet("Sheet1");
            //给sheet1添加第一行的头部标题
            NPOI.SS.UserModel.IRow row1 = sheet1.CreateRow(0);
            row1.CreateCell(0).SetCellValue("序号");
            row1.CreateCell(1).SetCellValue("身份证号码");
            row1.CreateCell(2).SetCellValue("学生姓名");
            row1.CreateCell(3).SetCellValue("考生号");
            row1.CreateCell(4).SetCellValue("字典值");
            row1.CreateCell(5).SetCellValue("字典备注");
            row1.CreateCell(6).SetCellValue("站长");
            row1.CreateCell(7).SetCellValue("站长大区");
            row1.CreateCell(8).SetCellValue("站长区域");
            row1.CreateCell(9).SetCellValue("毕业学校");

            List<SSISDetail> list1 = new BLL_View_AnalySISS().GetDetail(Name);

            for (int i = 0; i < list1.Count; i++)
            {
                NPOI.SS.UserModel.IRow rowtemp = sheet1.CreateRow(i + 1);
                rowtemp.CreateCell(0).SetCellValue((i + 1).ToString());//序号
                rowtemp.CreateCell(1).SetCellValue(list1[i].IdCard);//学生身份证
                rowtemp.CreateCell(2).SetCellValue(list1[i].StudentName);//学生姓名
                rowtemp.CreateCell(3).SetCellValue(list1[i].StudentNumber);//考生号
                rowtemp.CreateCell(4).SetCellValue(list1[i].DictValue);//字典值
                rowtemp.CreateCell(5).SetCellValue(list1[i].DictRemark);//字典备注
                rowtemp.CreateCell(6).SetCellValue(list1[i].zhanzhang);//毕业学校
                rowtemp.CreateCell(7).SetCellValue(list1[i].zhanzhangdaqu);//缴费次数
                rowtemp.CreateCell(8).SetCellValue(list1[i].zhanzhangquyu);//推荐人
                rowtemp.CreateCell(9).SetCellValue(list1[i].GrandSchool);//毕业学校
            }


            // 写入到客户端 
            System.IO.MemoryStream ms = new System.IO.MemoryStream();
            book.Write(ms);
            ms.Seek(0, SeekOrigin.Begin);




            return File(ms, "application/vnd.ms-excel", Name + "辖区内人员信息表.xls");
        }
        [Authorize]
        public ActionResult Index()
        {
            int tsthh = 0;
            List<View_User_Menu> View_User_Menul = new BLL_View_User_Menu().Search(
                -1,
                -1,
                out tsthh,
                i => i.UserAccount == System.Web.HttpContext.Current.User.Identity.Name);
            List<UI_Math_Menuinfo> lllll = View_User_Menul.Select(item => Mapper.Map<UI_Math_Menuinfo>(item)).ToList();
            ViewBag.MenuList = lllll;
            return View();
        }
        [Authorize]
        public ActionResult GetData(int draw,
            int start,
            int length,
            string student_name_idcard,
            string alarm_type,
            string tuijianrentype,
            string tuijianren,
            string zhaoshengtype)
        {
            try
            {
                Expression<Func<View_AnalySISS, bool>> e1 = i => true;
                Expression<Func<View_AnalySISS, bool>> e2 = i => true;


                Expression<Func<View_AnalySISS, bool>> F6 = i => true;
                if (!string.IsNullOrEmpty(zhaoshengtype))
                {
                    F6 = i => i.ZSLB == zhaoshengtype;
                }


                Expression<Func<View_AnalySISS, bool>> F7 = i => true;

                if (!string.IsNullOrEmpty(student_name_idcard))
                {
                    e1 = i => i.XSC_IdCard.Contains(student_name_idcard);
                    e2 = i => i.JYT_StudentName.Contains(student_name_idcard);
                }

                Expression<Func<View_AnalySISS, bool>> e3 = i => true;
                if (!string.IsNullOrEmpty(alarm_type))
                {
                    int alarm_type1 = int.Parse(alarm_type);
                    if (alarm_type1 == 2)
                    {
                        //2项目
                        e3 = i => i.FeeItems == alarm_type1;
                        F7 = i => i.name_Check == 1;
                    }
                    else if (alarm_type1 != 1111)
                    {
                        e3 = i => i.FeeItems != 2;
                        F7 = i => true;
                    }

                }

                if (alarm_type == "1111")
                {
                    F7 = i => i.name_Check == 0;
                }

                //推荐人类型 
                Expression<Func<View_AnalySISS, bool>> e4 = i => true;
                if (!string.IsNullOrEmpty(tuijianrentype))
                {
                    int a1 = int.Parse(tuijianrentype);
                    e4 = i => i.tuijianrentype == a1;
                }

                int total = 0;
                //
                Expression<Func<View_AnalySISS, bool>> e5 = i => true;
                if (!string.IsNullOrEmpty(tuijianren))
                {
                    e5 = i => i.Resault_Who.Contains(tuijianren);
                }

                List<View_AnalySISS> list = new BLL_View_AnalySISS().Search(
                    start,
                    length,
                    out total,
                    (e1.Or(e2)).And(e3).And(e4).And(e5).And(F6).And(F7)
                    );
                List<Dto_View_AnalySISS> list1 = list.Select(item => Mapper.Map<Dto_View_AnalySISS>(item)).ToList();

                return new JsonResult
                {
                    Data =
                    new datatablesModel<Dto_View_AnalySISS>
                    {
                        data = list1,
                        draw = draw,
                        recordsFiltered = total,
                        recordsTotal = total
                    },
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet
                };
            }
            catch (Exception ex)
            {
                return new JsonResult
                {
                    Data =
                 new datatablesModel<Dto_View_AnalySISS>
                 {
                     data = new List<Dto_View_AnalySISS>() { },
                     draw = 0,
                     recordsFiltered = 0,
                     recordsTotal = 0,
                     remark = ex.ToString()
                 },
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet
                };
            }

        }

        [Authorize]
        public ActionResult GetExcel( string student_name_idcard, string alarm_type, string tuijianrentype, string tuijianren,
             string zhaoshengtype)
        {
            Expression<Func<View_AnalySISS, bool>> e1 = i => true;
            Expression<Func<View_AnalySISS, bool>> e2 = i => true;

            Expression<Func<View_AnalySISS, bool>> F6 = i => true;
            Expression<Func<View_AnalySISS, bool>> F7 = i => true;

            if (!string.IsNullOrEmpty(zhaoshengtype))
            {
                F6 = i => i.ZSLB == zhaoshengtype;
            }

            if (!string.IsNullOrEmpty(student_name_idcard))
            {
                e1 = i => i.XSC_IdCard.Contains(student_name_idcard);
                e2 = i => i.JYT_StudentName.Contains(student_name_idcard);
            }

            Expression<Func<View_AnalySISS, bool>> e3 = i => true;
            if (!string.IsNullOrEmpty(alarm_type))
            {
                int alarm_type1 = int.Parse(alarm_type);
                if (alarm_type1 == 2)
                {
                    //2项目
                    e3 = i => i.FeeItems == alarm_type1;
                    F7 = i => i.name_Check == 1;
                }
                else if (alarm_type1 != 1111)
                {
                    e3 = i => i.FeeItems != 2;
                    F7 = i => true;
                }

            }

            if (alarm_type == "1111")
            {
                F7 = i => i.name_Check == 0;
            }

            //推荐人类型 
            Expression<Func<View_AnalySISS, bool>> e4 = i => true;
            if (!string.IsNullOrEmpty(tuijianrentype))
            {
                int a1 = int.Parse(tuijianrentype);
                e4 = i => i.tuijianrentype == a1;
            }

            int total = 0;
            //
            Expression<Func<View_AnalySISS, bool>> e5 = i => true;
            if (!string.IsNullOrEmpty(tuijianren))
            {
                e5 = i => i.SYJD_Who.Contains(tuijianren) || i.ZS_Who.Contains(tuijianren);
            }

            List<View_AnalySISS> list = new BLL_View_AnalySISS().Search(
                -1,
                -1,
                out total,
                (e1.Or(e2)).And(e3).And(e4).And(e5).And(F6).And(F7)
                );
            List<Dto_View_AnalySISS> list1 = list.Select(item => Mapper.Map<Dto_View_AnalySISS>(item)).ToList();

            NPOI.HSSF.UserModel.HSSFWorkbook book = new NPOI.HSSF.UserModel.HSSFWorkbook();
            //添加一个sheet
            NPOI.SS.UserModel.ISheet sheet1 = book.CreateSheet("Sheet1");
            //给sheet1添加第一行的头部标题
            NPOI.SS.UserModel.IRow row1 = sheet1.CreateRow(0);
            row1.CreateCell(0).SetCellValue("序号");
            row1.CreateCell(1).SetCellValue("学生身份证");
            row1.CreateCell(2).SetCellValue("学生姓名");
            row1.CreateCell(3).SetCellValue("学生姓名差异");
            
            row1.CreateCell(4).SetCellValue("招生类别");
            row1.CreateCell(5).SetCellValue("学生性别");
            row1.CreateCell(6).SetCellValue("毕业学校");
            row1.CreateCell(7).SetCellValue("缴费次数(学费+住宿费)");
            row1.CreateCell(8).SetCellValue("推荐人");
            row1.CreateCell(9).SetCellValue("推荐人类型");
             

            for (int i = 0; i < list1.Count; i++)
            {
                NPOI.SS.UserModel.IRow rowtemp = sheet1.CreateRow(i + 1);
                rowtemp.CreateCell(0).SetCellValue((i + 1).ToString());//序号
                rowtemp.CreateCell(1).SetCellValue(list1[i].XSC_IdCard);//学生身份证
                rowtemp.CreateCell(2).SetCellValue(list1[i].XSC_StudentName);//学生姓名

                
                rowtemp.CreateCell(3).SetCellValue(list1[i].name_Check_Content);//学生姓名
                rowtemp.CreateCell(4).SetCellValue(list1[i].ZSLB);//

                rowtemp.CreateCell(5).SetCellValue(list1[i].JYT_StudentSex);//学生性别
                rowtemp.CreateCell(6).SetCellValue(list1[i].JYT_GrandSchool);//毕业学校
                rowtemp.CreateCell(7).SetCellValue(list1[i].FeeItems);//缴费次数

                rowtemp.CreateCell(8).SetCellValue(list1[i].Resault_Who);//推荐人
                rowtemp.CreateCell(9).SetCellValue(list1[i].tuijianren);//推荐人类型

                
            }


            List<MaliangUI> ret = new BLL_View_AnalySISS().Static(string.Empty);
            ////////////////////////////////////////////////////////////////////
            NPOI.SS.UserModel.ISheet sheet2 = book.CreateSheet("统计分析数据");
            //给sheet1添加第一行的头部标题
            NPOI.SS.UserModel.IRow row2 = sheet2.CreateRow(0);
            row2.CreateCell(0).SetCellValue("序号");
            row2.CreateCell(1).SetCellValue("部门");
            row2.CreateCell(2).SetCellValue("招生人员");
            row2.CreateCell(3).SetCellValue("辖区内命中人数");
            row2.CreateCell(4).SetCellValue("总命中人数");
            row2.CreateCell(5).SetCellValue("基数");
            row2.CreateCell(6).SetCellValue("命中率");
            

            for (int i = 0; i < ret.Count; i++)
            {
                NPOI.SS.UserModel.IRow rowtemp = sheet2.CreateRow(i + 1);
                rowtemp.CreateCell(0).SetCellValue((i + 1).ToString());//序号
                rowtemp.CreateCell(1).SetCellValue(ret[i].Math_Dept_Name);//学生身份证
                rowtemp.CreateCell(2).SetCellValue(ret[i].Name);//学生身份证
                rowtemp.CreateCell(3).SetCellValue(ret[i].Value3);//学生姓名
                rowtemp.CreateCell(4).SetCellValue(ret[i].Value);//学生姓名
                rowtemp.CreateCell(5).SetCellValue(ret[i].Value2);//学生性别
                rowtemp.CreateCell(6).SetCellValue(ret[i].rate);//学生性别

            }
            // 写入到客户端 
            System.IO.MemoryStream ms = new System.IO.MemoryStream();
            book.Write(ms);
            ms.Seek(0, SeekOrigin.Begin);




            return File(ms, "application/vnd.ms-excel", "数据分析结果导出.xls");


        }

        [Authorize]
        public ActionResult GetStaticData(string alarm_type)
        {
            BLL_View_AnalySISS bll = new BLL_View_AnalySISS();
            List<MaliangUI> ret = bll.Static(alarm_type);
            return new JsonResult() { Data =
                new
                {
                    draw = int.Parse(Request["draw"]),
                    recordsTotal = ret.Count,
                    recordsFiltered = ret.Count,
                    data = ret
                },JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
    }
}