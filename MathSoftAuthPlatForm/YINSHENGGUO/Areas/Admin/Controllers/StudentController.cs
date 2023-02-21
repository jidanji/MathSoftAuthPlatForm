using AutoMapper;
using Business;
using DataAccess;
using DTOModel;
using MathSoftCommonLib;
using MathSoftModelLib;
using MAZIKONG.App_Start;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Web.Mvc;

namespace MAZIKONG.Areas.Admin.Controllers
{
    public class StudentController : AdminBaseController
    {
        [PreLoadMenuList]
        [Authorize]
        public ActionResult Index()
        {
            int subtotal = 0;
            Guid ZhuanYeDictId = Guid.Parse(ConfigurationManager.AppSettings["ZhuanYeDictId"]);
            List<Math_Dict> listDict = new BLL_Math_Dict().Search(
                -1,
                -1,
                out subtotal,
                i => i.DictTypeId == ZhuanYeDictId);
            ViewBag.DictList = listDict;
            UI_SysSetting sysSetting = new BLL_SysSetting().GetStauts();
            ViewBag.UI_SysSetting = sysSetting;
            return View();
        }

        [Authorize]
        public ActionResult DeleteStudent(Guid StudentId)
        {
            try
            {
                Math_Student modal = new BLL_Math_Student().Delete(StudentId);
                UIStudent uIStudent = Mapper.Map<UIStudent>(modal);
                string str1 = JsonHelper.SerializeObject(uIStudent);
                new BLL_DeleteLog().Add(new DeleteLog { DeleteLogId = Guid.NewGuid(), DeleteContent = str1 });
                return new JsonResult() { Data = new { remark = "", suc = true }, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
            catch (Exception ex)
            {
                return new JsonResult() { Data = new { remark = ex.Message, suc = false }, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }

        }

        [PreLoadMenuList]
        [Authorize]
        public ActionResult BaoBiao()
        {
            int subtotal = 0;
            Guid ZhuanYeDictId = Guid.Parse(ConfigurationManager.AppSettings["ZhuanYeDictId"]);
            List<Math_Dict> listDict = new BLL_Math_Dict().Search(-1, -1, out subtotal, i => i.DictTypeId == ZhuanYeDictId);
            ViewBag.DictList = listDict;
            return View();
        }


        [Authorize]
        public ActionResult GetData(int draw, int start, int length,
            string BegDate,
            string EndDate,
            string StudentName,
            string StudentIDCard,
            string IsIn,
            string IsBaodao
            )
        {
            Expression<Func<Math_Student, bool>> where100000 = i => true;
            if (!string.IsNullOrWhiteSpace(IsIn))
            {
                if (IsIn == "1")
                {
                    where100000 = i => i.IsIn == IsIn;
                }
                else
                {
                    where100000 = i => i.IsIn == "" || i.IsIn == null;
                }
            }


            Expression<Func<Math_Student, bool>> where1000001 = i => true;
            if (!string.IsNullOrWhiteSpace(IsBaodao))
            {
                if (IsBaodao == "1")
                {
                    where1000001 = i => i.IsBaodao == IsBaodao;
                }
                else
                {
                    where1000001 = i => i.IsBaodao == "" || i.IsBaodao == null;
                }

            }

            Expression<Func<Math_Student, bool>> where = i => true;
            if (!string.IsNullOrWhiteSpace(BegDate))
            {
                var ca = DateTime.Parse(BegDate);
                where = i => i.InsertTime >= ca;
            }


            Expression<Func<Math_Student, bool>> where1 = i => true;
            if (!string.IsNullOrWhiteSpace(EndDate))
            {
                var ca = DateTime.Parse(EndDate).AddDays(1);
                where1 = i => i.InsertTime < ca;
            }


            Expression<Func<Math_Student, bool>> where2 = i => true;
            if (!string.IsNullOrWhiteSpace(StudentName))
            {

                where2 = i => i.StudentName.Contains(StudentName);
            }

            Expression<Func<Math_Student, bool>> where3 = i => true;
            if (!string.IsNullOrWhiteSpace(StudentIDCard))
            {

                where3 = i => i.StudentIDCard.Contains(StudentIDCard);
            }

            Guid uid = new BLL_User().GetSingleByUserAccount(System.Web.HttpContext.Current.User.Identity.Name)
                .UserId;
            Expression<Func<Math_Student, bool>> where4 = i => true;
            where4 = i => i.UserId == uid;


            int total = 0;
           
            List<Math_Student> list = new BLL_Math_Student().Search(start, length, out total,
                where1
                .And(where2)
                .And(where3)
                .And(where4)
                .And(where100000)
                .And(where1000001)
                );

            List<UIStudent> listUIStudent = new List<UIStudent>();
            foreach (Math_Student item in list)
            {
                UIStudent uIStudent = Mapper.Map<UIStudent>(item);
                uIStudent.StudentZhuanYeValue = item.Math_Dict.DictValue;
                listUIStudent.Add(uIStudent);
            }
            datatablesModel<UIStudent> datatablesModel = new datatablesModel<UIStudent>()
            {
                data = listUIStudent,
                draw = draw,
                recordsFiltered = total,
                recordsTotal = total
            };
            return new JsonResult() { Data = datatablesModel, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }


        [Authorize]
        public ActionResult OutExcel
            (
           string BegDate,
           string EndDate,
           string StudentName,
           string StudentIDCard,
           string IsIn,
           string IsBaodao
           )
        {
            Expression<Func<Math_Student, bool>> where100000 = i => true;
            if (!string.IsNullOrWhiteSpace(IsIn))
            {
                if (IsIn == "1")
                {
                    where100000 = i => i.IsIn == IsIn;
                }
                else
                {
                    where100000 = i => i.IsIn == "" || i.IsIn == null;
                }

            }

            Expression<Func<Math_Student, bool>> where1000001 = i => true;
            if (!string.IsNullOrWhiteSpace(IsBaodao))
            {
                if (IsBaodao == "1")
                {
                    where1000001 = i => i.IsBaodao == IsBaodao;
                }
                else
                {
                    where1000001 = i => i.IsBaodao == "" || i.IsBaodao == null;
                }

            }

            Expression<Func<Math_Student, bool>> where = i => true;
            if (!string.IsNullOrWhiteSpace(BegDate))
            {
                var ca = DateTime.Parse(BegDate);
                where = i => i.InsertTime >= ca;
            }


            Expression<Func<Math_Student, bool>> where1 = i => true;
            if (!string.IsNullOrWhiteSpace(EndDate))
            {
                var ca = DateTime.Parse(EndDate).AddDays(1);
                where1 = i => i.InsertTime < ca;
            }


            Expression<Func<Math_Student, bool>> where2 = i => true;
            if (!string.IsNullOrWhiteSpace(StudentName))
            {

                where2 = i => i.StudentName.Contains(StudentName);
            }

            Expression<Func<Math_Student, bool>> where3 = i => true;
            if (!string.IsNullOrWhiteSpace(StudentIDCard))
            {

                where3 = i => i.StudentIDCard.Contains(StudentIDCard);
            }

            Guid uid = new BLL_User().GetSingleByUserAccount(System.Web.HttpContext.Current.User.Identity.Name).UserId;
            Expression<Func<Math_Student, bool>> where4 = i => true;
            where4 = i => i.UserId == uid;


            int total = 0;
            BLL_Math_Student bll = new BLL_Math_Student();
            List<Math_Student> list = bll.Search
                (-1, -1, out total,
                where
                .And(where1)
                .And(where2)
                .And(where3)
                .And(where4)
                .And(where100000)
                .And(where1000001)
                );

            List<UIStudent> listUIStudent = new List<UIStudent>();
            foreach (Math_Student item in list)
            {
                UIStudent uIStudent = Mapper.Map<UIStudent>(item);
                uIStudent.StudentZhuanYeValue = item.Math_Dict.DictValue;


                listUIStudent.Add(uIStudent);
            }

            //创建Excel文件的对象
            NPOI.HSSF.UserModel.HSSFWorkbook book = new NPOI.HSSF.UserModel.HSSFWorkbook();
            //添加一个sheet
            NPOI.SS.UserModel.ISheet sheet1 = book.CreateSheet("Sheet1");

            //给sheet1添加第一行的头部标题
            NPOI.SS.UserModel.IRow row1 = sheet1.CreateRow(0);
            row1.CreateCell(0).SetCellValue("序号");
            row1.CreateCell(1).SetCellValue("学生姓名");
            row1.CreateCell(2).SetCellValue("身份证");
            row1.CreateCell(3).SetCellValue("联系方式");
            row1.CreateCell(4).SetCellValue("学生性质");
            row1.CreateCell(5).SetCellValue("专业");
            row1.CreateCell(6).SetCellValue("家庭住址");
            row1.CreateCell(7).SetCellValue("推荐人");
            row1.CreateCell(8).SetCellValue("推荐人部门");
            row1.CreateCell(9).SetCellValue("录入时间");
            row1.CreateCell(10).SetCellValue("毕业学校");
            row1.CreateCell(11).SetCellValue("录取情况");
            row1.CreateCell(11).SetCellValue("报道情况");

            for (int i = 0; i < listUIStudent.Count; i++)
            {
                NPOI.SS.UserModel.IRow rowtemp = sheet1.CreateRow(i + 1);
                rowtemp.CreateCell(0).SetCellValue((i + 1).ToString());//序号
                rowtemp.CreateCell(1).SetCellValue(listUIStudent[i].StudentName);//学生姓名
                rowtemp.CreateCell(2).SetCellValue(listUIStudent[i].StudentIDCard);//身份证
                rowtemp.CreateCell(3).SetCellValue(listUIStudent[i].StudentPhone);//联系方式
                rowtemp.CreateCell(4).SetCellValue(listUIStudent[i].StudentType);//学生性质
                rowtemp.CreateCell(5).SetCellValue(listUIStudent[i].StudentZhuanYeValue);//专业
                rowtemp.CreateCell(6).SetCellValue(listUIStudent[i].StudentAddress);//家庭住址
                rowtemp.CreateCell(7).SetCellValue(listUIStudent[i].UserName);//推荐人
                rowtemp.CreateCell(8).SetCellValue(listUIStudent[i].UserDeptName);//推荐人部门
                rowtemp.CreateCell(9).SetCellValue(listUIStudent[i].InsertTime.GetDateTimeString());//录入日期

                rowtemp.CreateCell(10).SetCellValue(listUIStudent[i].StudentSchool);//毕业学校

                rowtemp.CreateCell(11).SetCellValue(string.IsNullOrEmpty(listUIStudent[i].IsIn)? "录取失败":"录取成功");//毕业学校

                rowtemp.CreateCell(11).SetCellValue(string.IsNullOrEmpty(listUIStudent[i].IsIn) ? "未报道" : "来报道");//毕业学校
            }
            // 写入到客户端 
            System.IO.MemoryStream ms = new System.IO.MemoryStream();
            book.Write(ms);
            ms.Seek(0, SeekOrigin.Begin);
            return File(ms, "application/vnd.ms-excel", "我的招生.xls");
        }


        [Authorize]
        public ActionResult OutExcelReport
         (
        string BegDate,
        string EndDate,
        string StudentName
        )
        {
            //这个逻辑很复杂，需要导出全量的报表。目前只是导出了一部分的报表。

            Expression<Func<Math_Student, bool>> where = i => true;
            if (!string.IsNullOrWhiteSpace(BegDate))
            {
                var ca = DateTime.Parse(BegDate);
                where = i => i.InsertTime >= ca;
            }


            Expression<Func<Math_Student, bool>> where1 = i => true;
            if (!string.IsNullOrWhiteSpace(EndDate))
            {
                var ca = DateTime.Parse(EndDate).AddDays(1);
                where1 = i => i.InsertTime < ca;
            }


            Expression<Func<Math_Student, bool>> where2 = i => true;
            if (!string.IsNullOrWhiteSpace(StudentName))
            {

                where2 = i => i.StudentName.Contains(StudentName)|| i.StudentIDCard.Contains(StudentName);
            }

            Expression<Func<Math_Student, bool>> where3 = i => true;
          


            Expression<Func<Math_Student, bool>> where4 = i => true;



            int total = 0;
            BLL_Math_Student bll = new BLL_Math_Student();
            List<Math_Student> list = bll.Search
                (-1, -1, out total,
                where
                .And(where1)
                .And(where2)
                .And(where3)
                .And(where4)
                );

            List<UIStudent> listUIStudent = new List<UIStudent>();
            foreach (Math_Student item in list)
            {
                UIStudent uIStudent = Mapper.Map<UIStudent>(item);
                uIStudent.StudentZhuanYeValue = item.Math_Dict.DictValue;


                listUIStudent.Add(uIStudent);
            }

            //创建Excel文件的对象
            NPOI.HSSF.UserModel.HSSFWorkbook book = new NPOI.HSSF.UserModel.HSSFWorkbook();

            #region 基础信息表
            //添加一个sheet 基础信息表
            TableBase(listUIStudent, book,"基础信息表");
            #endregion

            #region 单招
            //添加一个sheet 单招
            TableBase(listUIStudent.Where(item=>item.StudentType== "单招").ToList(), book, "单招");
            #endregion

            #region 统招
            //添加一个sheet 统招
            TableBase(listUIStudent.Where(i=>i.StudentType== "统招").ToList(), book, "统招");
            #endregion

            #region 中专
            //添加一个sheet 基础信息表
            TableBase(listUIStudent.Where(i=>i.StudentType== "中专").ToList(), book, "中专");
            #endregion

            #region 五年一贯制
            //添加一个sheet 基础信息表
            TableBase(listUIStudent.Where(i => i.StudentType == "五年一贯制").ToList(), book, "五年一贯制");
            #endregion

            #region 按日期统计
            NPOI.SS.UserModel.ISheet 按日期统计 = book.CreateSheet("按日期统计");

            NPOI.SS.UserModel.IRow row2 = 按日期统计.CreateRow(0);
            row2.CreateCell(0).SetCellValue("序号");
            row2.CreateCell(1).SetCellValue("月份");
            row2.CreateCell(2).SetCellValue("招生总数");

            BLL_BaoBiao bLL_BaoBiao = new BLL_BaoBiao();
            var sth = bLL_BaoBiao.GetZhaoShengBaoBiaoByMonth();




            for (int i = 0; i < sth.Count; i++)
            {
                NPOI.SS.UserModel.IRow rowtemp = 按日期统计.CreateRow(i + 1);
                rowtemp.CreateCell(0).SetCellValue((i + 1).ToString());//序号
                rowtemp.CreateCell(1).SetCellValue(sth[i].InsertData);//月份
                rowtemp.CreateCell(2).SetCellValue(sth[i].TotalNumber);//招生总数
            }
            #endregion

            #region  按部门统计
            NPOI.SS.UserModel.ISheet 按部门统计 = book.CreateSheet("按部门统计");

            NPOI.SS.UserModel.IRow row按部门统计 = 按部门统计.CreateRow(0);
            row按部门统计.CreateCell(0).SetCellValue("序号");
            row按部门统计.CreateCell(1).SetCellValue("部门名称");
            row按部门统计.CreateCell(2).SetCellValue("招生总人数");

            var sth1 = bLL_BaoBiao.GetZhaoShengBaoBiao();

            for (int i = 0; i < sth1.Count; i++)
            {
                NPOI.SS.UserModel.IRow rowtemp = 按部门统计.CreateRow(i + 1);
                rowtemp.CreateCell(0).SetCellValue((i + 1).ToString());//序号
                rowtemp.CreateCell(1).SetCellValue(sth1[i].UserDeptName);//学生姓名
                rowtemp.CreateCell(2).SetCellValue(sth1[i].TotalNumber);//身份证

            }

            #endregion

            #region  按推荐人统计
            NPOI.SS.UserModel.ISheet 按推荐人统计 = book.CreateSheet("按推荐人统计");
            NPOI.SS.UserModel.IRow row按推荐人统计 = 按推荐人统计.CreateRow(0);
            row按推荐人统计.CreateCell(0).SetCellValue("序号");
            row按推荐人统计.CreateCell(1).SetCellValue("推荐人部门");
            row按推荐人统计.CreateCell(2).SetCellValue("推荐人");
            row按推荐人统计.CreateCell(3).SetCellValue("录取人数");
            row按推荐人统计.CreateCell(4).SetCellValue("招生总数");
            row按推荐人统计.CreateCell(5).SetCellValue("录取率");

            var sth2 = bLL_BaoBiao.GetZhaoShengBaoBiaoByTuiJianRen();

            for (int i = 0; i < sth2.Count; i++)
            {
                NPOI.SS.UserModel.IRow rowtemp = 按推荐人统计.CreateRow(i + 1);
                rowtemp.CreateCell(0).SetCellValue((i + 1).ToString());//序号
                rowtemp.CreateCell(1).SetCellValue(sth2[i].UserDeptName);//学生姓名
                rowtemp.CreateCell(2).SetCellValue(sth2[i].UserName);//学生姓名

                rowtemp.CreateCell(3).SetCellValue(sth2[i].intotal);//身份证
                rowtemp.CreateCell(4).SetCellValue(sth2[i].TotalNumber);//身份证
                rowtemp.CreateCell(5).SetCellValue(sth2[i].rate+"%");//身份证
            }
            #endregion

            #region 按专业统计
            NPOI.SS.UserModel.ISheet 按专业统计 = book.CreateSheet("按专业统计");

            NPOI.SS.UserModel.IRow row按专业统计 = 按专业统计.CreateRow(0);
            row按专业统计.CreateCell(0).SetCellValue("序号");
            row按专业统计.CreateCell(1).SetCellValue("专业");
            row按专业统计.CreateCell(2).SetCellValue("招生总数");

            var zhuanyelist = bLL_BaoBiao.GetZhaoShengBaoBiaoByZhuanYe();
            for (int i = 0; i < zhuanyelist.Count; i++)
            {
                NPOI.SS.UserModel.IRow rowtemp = 按专业统计.CreateRow(i + 1);
                rowtemp.CreateCell(0).SetCellValue((i + 1).ToString());//序号
                rowtemp.CreateCell(1).SetCellValue(zhuanyelist[i].ZhuanYeName);//学生姓名
                rowtemp.CreateCell(2).SetCellValue(zhuanyelist[i].TotalNumber);//身份证
            }
            #endregion

            #region  按性别统计
            NPOI.SS.UserModel.ISheet 按性别统计 = book.CreateSheet("按性别统计");



            NPOI.SS.UserModel.IRow row按性别统计 = 按性别统计.CreateRow(0);
            row按性别统计.CreateCell(0).SetCellValue("序号");
            row按性别统计.CreateCell(1).SetCellValue("性别");
            row按性别统计.CreateCell(2).SetCellValue("招生总数");

            var sexlist = bLL_BaoBiao.GetZhaoShengBaoBiaoBySex();

            for (int i = 0; i < sexlist.Count; i++)
            {
                NPOI.SS.UserModel.IRow rowtemp = 按性别统计.CreateRow(i + 1);
                rowtemp.CreateCell(0).SetCellValue((i + 1).ToString());//序号
                rowtemp.CreateCell(1).SetCellValue(sexlist[i].Sex);//学生姓名
                rowtemp.CreateCell(2).SetCellValue(sexlist[i].TotalNumber);//身份证

            }

            #endregion

            #region  按地区统计
            NPOI.SS.UserModel.ISheet 按地区统计 = book.CreateSheet("按地区统计");



            NPOI.SS.UserModel.IRow row按地区统计 = 按地区统计.CreateRow(0);
            row按地区统计.CreateCell(0).SetCellValue("序号");
            row按地区统计.CreateCell(1).SetCellValue("地区");
            row按地区统计.CreateCell(2).SetCellValue("招生总数");

            var diqulist = bLL_BaoBiao.GetZhaoShengBaoBiaoByArea();


            for (int i = 0; i < diqulist.Count; i++)
            {
                NPOI.SS.UserModel.IRow rowtemp = 按地区统计.CreateRow(i + 1);
                rowtemp.CreateCell(0).SetCellValue((i + 1).ToString());//序号
                rowtemp.CreateCell(1).SetCellValue(diqulist[i].Area);//学生姓名
                rowtemp.CreateCell(2).SetCellValue(diqulist[i].TotalNumber);//身份证
            }

            #endregion

            #region 按学生性质统计
            NPOI.SS.UserModel.ISheet 按学生性质统计 = book.CreateSheet("按学生性质统计");

            NPOI.SS.UserModel.IRow row按学生性质统计 = 按学生性质统计.CreateRow(0);
            row按学生性质统计.CreateCell(0).SetCellValue("序号");
            row按学生性质统计.CreateCell(1).SetCellValue("学生性质");
            row按学生性质统计.CreateCell(2).SetCellValue("招生总数");

            var xueshengxingzhilist = bLL_BaoBiao.GetZhaoShengBaoBiaoByStudentType();


            for (int i = 0; i < xueshengxingzhilist.Count; i++)
            {
                NPOI.SS.UserModel.IRow rowtemp = 按学生性质统计.CreateRow(i + 1);
                rowtemp.CreateCell(0).SetCellValue((i + 1).ToString());//序号
                rowtemp.CreateCell(1).SetCellValue(xueshengxingzhilist[i].StudentType);//学生姓名
                rowtemp.CreateCell(2).SetCellValue(xueshengxingzhilist[i].TotalNumber);//身份证
            }
            #endregion

            // 写入到客户端 
            System.IO.MemoryStream ms = new System.IO.MemoryStream();
            book.Write(ms);
            ms.Seek(0, SeekOrigin.Begin);
            return File(ms, "application/vnd.ms-excel", "招生统计表.xls");
        }

        private static void TableBase(List<UIStudent> listUIStudent, NPOI.HSSF.UserModel.HSSFWorkbook book,string tabName)
        {
            NPOI.SS.UserModel.ISheet 基础信息表 = book.CreateSheet(tabName);
            NPOI.SS.UserModel.IRow row1 = 基础信息表.CreateRow(0);
            row1.CreateCell(0).SetCellValue("序号");
            row1.CreateCell(1).SetCellValue("学生姓名");
            row1.CreateCell(2).SetCellValue("身份证");
            row1.CreateCell(3).SetCellValue("联系方式");
            row1.CreateCell(4).SetCellValue("学生性质");
            row1.CreateCell(5).SetCellValue("专业");
            row1.CreateCell(6).SetCellValue("家庭住址");
            row1.CreateCell(7).SetCellValue("推荐人");
            row1.CreateCell(8).SetCellValue("推荐人部门");
            row1.CreateCell(9).SetCellValue("录入时间");
            row1.CreateCell(10).SetCellValue("录取情况");
            row1.CreateCell(11).SetCellValue("报道情况");

            for (int i = 0; i < listUIStudent.Count; i++)
            {
                NPOI.SS.UserModel.IRow rowtemp = 基础信息表.CreateRow(i + 1);
                rowtemp.CreateCell(0).SetCellValue((i + 1).ToString());//序号
                rowtemp.CreateCell(1).SetCellValue(listUIStudent[i].StudentName);//学生姓名
                rowtemp.CreateCell(2).SetCellValue(listUIStudent[i].StudentIDCard);//身份证
                rowtemp.CreateCell(3).SetCellValue(listUIStudent[i].StudentPhone);//联系方式
                rowtemp.CreateCell(4).SetCellValue(listUIStudent[i].StudentType);//学生性质
                rowtemp.CreateCell(5).SetCellValue(listUIStudent[i].StudentZhuanYeValue);//专业
                rowtemp.CreateCell(6).SetCellValue(listUIStudent[i].StudentAddress);//家庭住址
                rowtemp.CreateCell(7).SetCellValue(listUIStudent[i].UserName);//推荐人
                rowtemp.CreateCell(8).SetCellValue(listUIStudent[i].UserDeptName);//推荐人部门
                rowtemp.CreateCell(9).SetCellValue(listUIStudent[i].InsertTime.GetDateTimeString());//录入日期
                rowtemp.CreateCell(10).SetCellValue(string.IsNullOrEmpty( listUIStudent[i].IsIn)? "录取失败":"录取成功");//录入日期
                rowtemp.CreateCell(11).SetCellValue(string.IsNullOrEmpty(listUIStudent[i].IsBaodao) ? "未报道" : "来报道");//录入日期
            }
        }

        [Authorize]
        public ActionResult GetData1(int draw, int start, int length,
           string BegDate,
           string EndDate,
           string StudentName,
           string tuijianren,
           string IsIn,
           string IsBaodao
           )
        {

            Expression<Func<Math_Student, bool>> where100000 = i => true;
            if (!string.IsNullOrWhiteSpace(IsIn))
            {
                if (IsIn == "1")
                {
                    where100000 = i => i.IsIn == IsIn;
                }
                else {
                    where100000 = i => i.IsIn == ""|| i.IsIn==null;
                }

            }

            Expression<Func<Math_Student, bool>> where1000001 = i => true;
            if (!string.IsNullOrWhiteSpace(IsBaodao))
            {
                if (IsBaodao == "1")
                {
                    where1000001 = i => i.IsBaodao == IsBaodao;
                }
                else
                {
                    where1000001 = i => i.IsBaodao == "" || i.IsBaodao == null;
                }

            }

            Expression<Func<Math_Student, bool>> where = i => true;
            if (!string.IsNullOrWhiteSpace(BegDate))
            {
                var ca = DateTime.Parse(BegDate);
                where = i => i.InsertTime >= ca;
            }


            Expression<Func<Math_Student, bool>> where1 = i => true;
            if (!string.IsNullOrWhiteSpace(EndDate))
            {
                var ca = DateTime.Parse(EndDate).AddDays(1);
                where1 = i => i.InsertTime < ca;
            }


            Expression<Func<Math_Student, bool>> where2 = i => true;
            if (!string.IsNullOrWhiteSpace(StudentName))
            {

                where2 = i => i.StudentName.Contains(StudentName)||i.StudentIDCard.Contains(StudentName);
            }

            Expression<Func<Math_Student, bool>> where3 = i => true;
            if (!string.IsNullOrWhiteSpace(tuijianren))
            {

                where3 = i => i.UserName.Contains(tuijianren);
            }

            Guid uid = new BLL_User().GetSingleByUserAccount(System.Web.HttpContext.Current.User.Identity.Name).UserId;
            Expression<Func<Math_Student, bool>> where4 = i => true;
            int total = 0;
            BLL_Math_Student bll = new BLL_Math_Student();
            List<Math_Student> list = bll.Search
                (start, length, out total,
                where
                .And(where1)
                .And(where2)
                .And(where3)
                .And(where4)
                .And(where100000)
                .And(where1000001)
                );

            List<UIStudent> listUIStudent = new List<UIStudent>();
            foreach (Math_Student item in list)
            {
                UIStudent uIStudent = Mapper.Map<UIStudent>(item);
                uIStudent.StudentZhuanYeValue = item.Math_Dict.DictValue;
               

                listUIStudent.Add(uIStudent);
            }
            datatablesModel<UIStudent> datatablesModel = new datatablesModel<UIStudent>()
            {
                data = listUIStudent,
                draw = draw,
                recordsFiltered = total,
                recordsTotal = total
            };
            return new JsonResult() { Data = datatablesModel, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        [Authorize]
        public ActionResult AddStudent(
            string StudentName,
            string StudentIDCard,
            string StudentPhone,
            string StudentType,
            string StudentSchool,
            Guid? StudentZhuanYeId,
            string StudentAddress,
            string Remark,
            string Sex,
            string Area
            )
        {
            string userName = System.Web.HttpContext.Current.User.Identity.Name;
            Math_UserInfo userInfo = new BLL_User().GetSingleByUserAccount(userName);
            Math_Deptinfo deptInfo = userInfo.Math_Deptinfo;


            Math_Student student = new Math_Student()
            {
                InsertTime = DateTime.Now,
                UpdateTime = DateTime.Now,
                StudentId = Guid.NewGuid(),
                UserId = userInfo.UserId,
                UserName = userInfo.UserName,
                UserDeptId = deptInfo.Math_Dept_Id,
                UserDeptName = deptInfo.Math_Dept_Name,
                UserPhone = userInfo.UserPhone,
                StudentIDCard = StudentIDCard,
                StudentPhone = StudentPhone,
                StudentType = StudentType,
                StudentSchool = StudentSchool,
                StudentZhuanYeId = StudentZhuanYeId,
                StudentName = StudentName,
                Remark = Remark,
                Sex= Sex,
                Area= Area

            };
            MathRoleAuthorEntities entities = new MathRoleAuthorEntities();
            BLL_Math_Student bLL_Math_Student = new BLL_Math_Student();
            if (StudentZhuanYeId == null)
            {
                StudentZhuanYeId = Guid.Parse("181CFBF4-B388-48D1-AE98-9ED7F47E3335");
            }
            UIModelData<Math_Student> uIModelData = bLL_Math_Student.Add
                 (StudentName,
                StudentIDCard,
                 StudentPhone,
                 StudentType,
                 StudentSchool,
                 userInfo.UserId,
                 StudentZhuanYeId.Value,
                 StudentAddress,
                 Remark,
                 Sex,
                 Area,
                userInfo.UserName,
              userInfo.UserPhone,
                deptInfo.Math_Dept_Id,
                 deptInfo.Math_Dept_Name,
                 i => i.StudentIDCard == StudentIDCard&&i.StudentType==StudentType);


            UIModelData<UIStudent> model = new UIModelData<UIStudent>()
            {
                Data = Mapper.Map<UIStudent>(uIModelData.Data),
                remark = uIModelData.remark,
                status = uIModelData.status,
                suc = uIModelData.suc
            };
            return new JsonResult() { Data = model, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

    }
}