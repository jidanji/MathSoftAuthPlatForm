using AutoMapper;
using Business;
using DataAccess;
using DTOModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;

using MathSoftModelLib;
using MathSoftCommonLib;

namespace MAZIKONG.Areas.Admin.Controllers
{
    public class ExcelController : AdminBaseController
    {
        [Authorize]
        public ActionResult Index()
        {
            return View();
        }

        [Authorize]
        public ActionResult GetData(string searchBegDate, string searchEndDate, string searchStatus)
        {

            //创建Excel文件的对象
            NPOI.HSSF.UserModel.HSSFWorkbook book = new NPOI.HSSF.UserModel.HSSFWorkbook();
            //添加一个sheet
            NPOI.SS.UserModel.ISheet sheet1 = book.CreateSheet("Sheet1");

            //给sheet1添加第一行的头部标题
            NPOI.SS.UserModel.IRow row1 = sheet1.CreateRow(0);
            row1.CreateCell(0).SetCellValue("序号");
            row1.CreateCell(1).SetCellValue("申请人");
            row1.CreateCell(2).SetCellValue("事件");
            row1.CreateCell(3).SetCellValue("部门");
            row1.CreateCell(4).SetCellValue("电话");
            row1.CreateCell(5).SetCellValue("计划费用(元)");
            row1.CreateCell(6).SetCellValue("实际费用(元)");
            row1.CreateCell(7).SetCellValue("发起日期");
            row1.CreateCell(8).SetCellValue("审批日期");
            row1.CreateCell(9).SetCellValue("状态");
            #region 马良隐藏
            /////////////////////////////////////////////////////////////////////////////////////////////////////////

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
            List<Math_Work> list = bLL_Role.Search(-1, -1, out total, where.And(where1).And(where2).And(where3));

            List<UIMath_Work> listUIRoleModel = new List<UIMath_Work>();
            foreach (Math_Work item in list)
            {
                UIMath_Work uIRoleModel = Mapper.Map<UIMath_Work>(item);
                listUIRoleModel.Add(uIRoleModel);
            }

            /////////////////////////////////////////////////////////////////////////////////////////////////////////
            #endregion
            //将数据逐步写入sheet1各个行
            for (int i = 0; i < list.Count; i++)
            {
                NPOI.SS.UserModel.IRow rowtemp = sheet1.CreateRow(i + 1);
                rowtemp.CreateCell(0).SetCellValue((i + 1).ToString());//序号
                rowtemp.CreateCell(1).SetCellValue(list[i].UserName);//申请人
                rowtemp.CreateCell(2).SetCellValue(list[i].WorkName);//事件
                rowtemp.CreateCell(3).SetCellValue(list[i].UserDeptName);//部门
                rowtemp.CreateCell(4).SetCellValue(list[i].UserTelNumber);//电话
                rowtemp.CreateCell(5).SetCellValue(list[i].Fee.ToString());//计划费用
                rowtemp.CreateCell(6).SetCellValue(list[i].InfactFee.ToString());//实际费用
                rowtemp.CreateCell(7).SetCellValue(list[i].CreateTime.Value.GetDateString());//发起日期
                rowtemp.CreateCell(8).SetCellValue(list[i].ShenPiRiQi.Value.GetDateString());//审批日期
                rowtemp.CreateCell(9).SetCellValue(new BLL_Dcit().Dict[list[i].Status.Value]);
            }
            // 写入到客户端 
            System.IO.MemoryStream ms = new System.IO.MemoryStream();
            book.Write(ms);
            ms.Seek(0, SeekOrigin.Begin);
            return File(ms, "application/vnd.ms-excel", "我的出差申请.xls");
        }



        [Authorize]
        public ActionResult GetData2(string searchBegDate,string searchEndDate,string searchStatus,string searchDept)
        {
            //创建Excel文件的对象
            NPOI.HSSF.UserModel.HSSFWorkbook book = new NPOI.HSSF.UserModel.HSSFWorkbook();
            //添加一个sheet
            NPOI.SS.UserModel.ISheet sheet1 = book.CreateSheet("Sheet1");

            //给sheet1添加第一行的头部标题
            NPOI.SS.UserModel.IRow row1 = sheet1.CreateRow(0);
            row1.CreateCell(0).SetCellValue("序号");
            row1.CreateCell(1).SetCellValue("申请人");
            row1.CreateCell(2).SetCellValue("事件");
            row1.CreateCell(3).SetCellValue("部门");
            row1.CreateCell(4).SetCellValue("电话");
            row1.CreateCell(5).SetCellValue("计划费用(元)");
            row1.CreateCell(6).SetCellValue("实际费用(元)");
            row1.CreateCell(7).SetCellValue("发起日期");
            row1.CreateCell(8).SetCellValue("审批日期");
            row1.CreateCell(9).SetCellValue("状态");
            #region 马良隐藏

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
            List<Math_Work> list = bLL_Role.Search(-1, -1, out total, where1.And(where2)
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

            #endregion
            //将数据逐步写入sheet1各个行
            for (int i = 0; i < list.Count; i++)
            {
                NPOI.SS.UserModel.IRow rowtemp = sheet1.CreateRow(i + 1);
                rowtemp.CreateCell(0).SetCellValue((i + 1).ToString());//序号
                rowtemp.CreateCell(1).SetCellValue(list[i].UserName);//申请人
                rowtemp.CreateCell(2).SetCellValue(list[i].WorkName);//事件
                rowtemp.CreateCell(3).SetCellValue(list[i].UserDeptName);//部门
                rowtemp.CreateCell(4).SetCellValue(list[i].UserTelNumber);//电话
                rowtemp.CreateCell(5).SetCellValue(list[i].Fee.ToString());//计划费用
                rowtemp.CreateCell(6).SetCellValue(list[i].InfactFee.ToString());//实际费用
                rowtemp.CreateCell(7).SetCellValue(list[i].CreateTime.Value.GetDateString());//发起日期
                rowtemp.CreateCell(8).SetCellValue(list[i].ShenPiRiQi.Value.GetDateString());//审批日期
                rowtemp.CreateCell(9).SetCellValue(new BLL_Dcit().Dict[list[i].Status.Value]);
            }
            // 写入到客户端 
            System.IO.MemoryStream ms = new System.IO.MemoryStream();
            book.Write(ms);
            ms.Seek(0, SeekOrigin.Begin);
            return File(ms, "application/vnd.ms-excel", "部门审批信息.xls");
        }

        [Authorize]
        public ActionResult GetData3(string searchBegDate,string searchEndDate,string searchStatus,string searchDept)
        {
            //创建Excel文件的对象
            NPOI.HSSF.UserModel.HSSFWorkbook book = new NPOI.HSSF.UserModel.HSSFWorkbook();
            //添加一个sheet
            NPOI.SS.UserModel.ISheet sheet1 = book.CreateSheet("Sheet1");

            //给sheet1添加第一行的头部标题
            NPOI.SS.UserModel.IRow row1 = sheet1.CreateRow(0);
            row1.CreateCell(0).SetCellValue("序号");
            row1.CreateCell(1).SetCellValue("申请人");
            row1.CreateCell(2).SetCellValue("事件");
            row1.CreateCell(3).SetCellValue("部门");
            row1.CreateCell(4).SetCellValue("电话");
            row1.CreateCell(5).SetCellValue("计划费用(元)");
            row1.CreateCell(6).SetCellValue("实际费用(元)");
            row1.CreateCell(7).SetCellValue("发起日期");
            row1.CreateCell(8).SetCellValue("审批日期");
            row1.CreateCell(9).SetCellValue("状态");
            #region 马良隐藏
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
            List<Math_Work> list = bLL_Role.Search(-1, -1, out total, where1
                .And(where7)
                .And(where8)
                .And(where9)
                .And(where10));

            List<UIMath_Work> listUIRoleModel = new List<UIMath_Work>();
            foreach (Math_Work item in list)
            {
                UIMath_Work uIRoleModel = Mapper.Map<UIMath_Work>(item);

                 

                listUIRoleModel.Add(uIRoleModel);

            }

            #endregion
            //将数据逐步写入sheet1各个行
            for (int i = 0; i < list.Count; i++)
            {
                NPOI.SS.UserModel.IRow rowtemp = sheet1.CreateRow(i + 1);
                rowtemp.CreateCell(0).SetCellValue((i + 1).ToString());//序号
                rowtemp.CreateCell(1).SetCellValue(list[i].UserName);//申请人
                rowtemp.CreateCell(2).SetCellValue(list[i].WorkName);//事件
                rowtemp.CreateCell(3).SetCellValue(list[i].UserDeptName);//部门
                rowtemp.CreateCell(4).SetCellValue(list[i].UserTelNumber);//电话
                rowtemp.CreateCell(5).SetCellValue(list[i].Fee.ToString());//计划费用
                rowtemp.CreateCell(6).SetCellValue(list[i].InfactFee.ToString());//实际费用
                rowtemp.CreateCell(7).SetCellValue(list[i].CreateTime.Value.GetDateString());//发起日期
                rowtemp.CreateCell(8).SetCellValue(list[i].ShenPiRiQi.Value.GetDateString());//审批日期
                rowtemp.CreateCell(9).SetCellValue(new BLL_Dcit().Dict[list[i].Status.Value]);
            }
            // 写入到客户端 
            System.IO.MemoryStream ms = new System.IO.MemoryStream();
            book.Write(ms);
            ms.Seek(0, SeekOrigin.Begin);
            return File(ms, "application/vnd.ms-excel", "招办审批信息.xls");
        }
        [Authorize]
        public ActionResult GetData4(string searchBegDate,string searchEndDate,string searchDept,string searchStaticType)
        {
            //创建Excel文件的对象
            NPOI.HSSF.UserModel.HSSFWorkbook book = new NPOI.HSSF.UserModel.HSSFWorkbook();
            //添加一个sheet
            NPOI.SS.UserModel.ISheet sheet1 = book.CreateSheet("Sheet1");

            //给sheet1添加第一行的头部标题
            NPOI.SS.UserModel.IRow row1 = sheet1.CreateRow(0);
            row1.CreateCell(0).SetCellValue("序号");
            row1.CreateCell(1).SetCellValue("申请人");
            row1.CreateCell(2).SetCellValue("事件");
            row1.CreateCell(3).SetCellValue("部门");
            row1.CreateCell(4).SetCellValue("电话");
            row1.CreateCell(5).SetCellValue("计划费用(元)");
            row1.CreateCell(6).SetCellValue("实际费用(元)");
            row1.CreateCell(7).SetCellValue("发起日期");
            row1.CreateCell(8).SetCellValue("审批日期");
            row1.CreateCell(9).SetCellValue("状态");
            #region 马良隐藏
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
            List<Math_Work> list = bLL_Role.Search(-1, -1, out total, where1
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

            #endregion
            //将数据逐步写入sheet1各个行
            for (int i = 0; i < list.Count; i++)
            {
                NPOI.SS.UserModel.IRow rowtemp = sheet1.CreateRow(i + 1);
                rowtemp.CreateCell(0).SetCellValue((i + 1).ToString());//序号
                rowtemp.CreateCell(1).SetCellValue(list[i].UserName);//申请人
                rowtemp.CreateCell(2).SetCellValue(list[i].WorkName);//事件
                rowtemp.CreateCell(3).SetCellValue(list[i].UserDeptName);//部门
                rowtemp.CreateCell(4).SetCellValue(list[i].UserTelNumber);//电话
                rowtemp.CreateCell(5).SetCellValue(list[i].Fee.ToString());//计划费用
                rowtemp.CreateCell(6).SetCellValue(list[i].InfactFee.ToString());//实际费用
                rowtemp.CreateCell(7).SetCellValue(list[i].CreateTime.Value.GetDateString());//发起日期
                rowtemp.CreateCell(8).SetCellValue(list[i].ShenPiRiQi.Value.GetDateString());//审批日期
                rowtemp.CreateCell(9).SetCellValue(new BLL_Dcit().Dict[list[i].Status.Value]);
            }
            // 写入到客户端 
            System.IO.MemoryStream ms = new System.IO.MemoryStream();
            book.Write(ms);
            ms.Seek(0, SeekOrigin.Begin);
            return File(ms, "application/vnd.ms-excel", "财务审阅基础信息.xls");
        }

        [Authorize]
        public ActionResult GetData5(string searchBegDate,string searchEndDate,string searchDept,string searchStaticType)

        {
            //创建Excel文件的对象
            NPOI.HSSF.UserModel.HSSFWorkbook book = new NPOI.HSSF.UserModel.HSSFWorkbook();
            //添加一个sheet
            NPOI.SS.UserModel.ISheet sheet1 = book.CreateSheet("Sheet1");

                    

            //给sheet1添加第一行的头部标题
            NPOI.SS.UserModel.IRow row1 = sheet1.CreateRow(0);
            row1.CreateCell(0).SetCellValue("序号");
            row1.CreateCell(1).SetCellValue("部门");
            row1.CreateCell(2).SetCellValue("统计方式");
            row1.CreateCell(3).SetCellValue("日期");
            row1.CreateCell(4).SetCellValue("总出差人数");
            row1.CreateCell(5).SetCellValue("总费用");
             
            #region 马良隐藏
            string userName = System.Web.HttpContext.Current.User.Identity.Name;



            BLL_BaoBiao bll = new BLL_BaoBiao();
            List<UIStatic> list = bll.Search(userName, searchBegDate, searchEndDate, searchDept, searchStaticType);

            #endregion
            //将数据逐步写入sheet1各个行
            for (int i = 0; i < list.Count; i++)
            {
                NPOI.SS.UserModel.IRow rowtemp = sheet1.CreateRow(i + 1);
                rowtemp.CreateCell(0).SetCellValue((i + 1).ToString());//序号
                rowtemp.CreateCell(1).SetCellValue(list[i].UserDeptName);//部门
                rowtemp.CreateCell(2).SetCellValue(list[i].StaticType);//统计方式
                rowtemp.CreateCell(3).SetCellValue(list[i].DateMonth);//日期
                rowtemp.CreateCell(4).SetCellValue(list[i].PersonCount);//总出差人数
                rowtemp.CreateCell(5).SetCellValue(list[i].InfactFee);//总费用
            }
            // 写入到客户端 
            System.IO.MemoryStream ms = new System.IO.MemoryStream();
            book.Write(ms);
            ms.Seek(0, SeekOrigin.Begin);
            return File(ms, "application/vnd.ms-excel", "财务审阅统计报表.xls");
        }
    }
}