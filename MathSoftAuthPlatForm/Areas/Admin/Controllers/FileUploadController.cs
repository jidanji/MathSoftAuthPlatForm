using AutoMapper;
using Business;
using DataAccess;
using DTOModel;
using MathSoftModelLib;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using MathSoftCommonLib;
using System.Data;

namespace MAZIKONG.Areas.Admin.Controllers
{
    public class FileUploadController : AdminBaseController
    {
        [Authorize]
        public ActionResult Index()
        {

            int tsthh = 0;
            BLL_View_User_Menu bLL_View_User_Menu = new BLL_View_User_Menu();
            List<View_User_Menu> View_User_Menul = bLL_View_User_Menu.Search(-1,
                -1,
                out tsthh,
                i => i.UserAccount == System.Web.HttpContext.Current.User.Identity.Name);
            List<UI_Math_Menuinfo> lllll = View_User_Menul.Select(item => Mapper.Map<UI_Math_Menuinfo>(item)).ToList();
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
        public ActionResult UpLoadExcel(string id)
        {
            try
            {
                BLL_Math_FileUpload bll = new BLL_Math_FileUpload();
                var file = Request.Files[0]; //获取选中文件  
                var filecombin = file.FileName.Split('.');
                if (file == null || String.IsNullOrEmpty(file.FileName) || file.ContentLength == 0 || filecombin.Length < 2)
                {
                    return Json(new
                    {
                        fileid = 0,
                        src = "",
                        name = "",
                        msg = "上传出错 请检查文件名 或 文件内容"
                    });
                }
                //定义本地路径位置
                string local = "Upload\\";
                string filePathName = string.Empty;
                string localPath = Path.Combine(HttpRuntime.AppDomainAppPath, local);

                var tmpName = Server.MapPath("~/Upload/");
                //var tmp = file.FileName;
                var tmp = id + "." + file.FileName.Split(new char[] { '.' })[1];

                var tmpIndex = 0;
                //判断是否存在相同文件名的文件 相同累加1继续判断
                while (System.IO.File.Exists(tmpName + tmp))
                {
                    tmp = filecombin[0] + "_" + ++tmpIndex + "." + filecombin[1];
                }

                //不带路径的最终文件名
                filePathName = tmp;

                if (!System.IO.Directory.Exists(localPath))
                    System.IO.Directory.CreateDirectory(localPath);
                string localURL = Path.Combine(local, filePathName);
                string p1 = Path.Combine(localPath, filePathName);
                file.SaveAs(p1);   //保存图片（文件夹）

                bll.Add(new Math_FileUpload
                {
                    FileName = tmp,
                    FilePath = p1,
                    FileType = id,
                    UploadId = Guid.NewGuid(),
                    UploadTime = DateTime.Now,
                });

                BLL_Math_Dict bLL_Math_Dict = new BLL_Math_Dict();
                int total = 0;
                var tew = Guid.Parse(ConfigManagerHelper.GetValueByKey("TongYiCi"));
                List<Math_Dict> dictList = bLL_Math_Dict.Search(-1,
                    -1,
                    out total,
                    i => i.DictTypeId == tew);

                List<Synonym> SynonymList = new List<Synonym>();
                foreach (Math_Dict item in dictList)
                {
                    SynonymList.Add(new Synonym() { Name = item.DictValue, SynonymNames = item.DictRemark.Split(new char[] { ';', ',', '，', '；' }) });
                }

                List<DataTable> dtList = new List<DataTable>();

                switch (id)
                {
                    #region  教育厅的数据
                    case "JYT":
                        dtList = ExcelHelper.GetTables(SynonymList,
                            new List<string> { "IdCard", "StudentName", "ZSLB" },
                            new List<string> { "StudentSex", "GrandSchool" }, p1);
                        BLL_Math_JYT bLL_Math_JYT = new BLL_Math_JYT();
                        bLL_Math_JYT.clearTable();
                        foreach (DataTable table in dtList)
                        {
                            foreach (DataRow row in table.Rows)
                            {
                                if (row.RowState == DataRowState.Deleted)
                                {
                                    continue;
                                }
                                string dfg = row["IdCard"].ToString();
                                bLL_Math_JYT.Add(row["StudentName"].ToString(),
                                    row["IdCard"].ToString(),
                                     row["StudentSex"].ToString(),
                                     row["GrandSchool"].ToString(),
                                      DateTime.Now,
                                      DateTime.Now,
                                      table.TableName,
                                     row["ZSLB"].ToString().Contains("单招") ? "单招" : "统招",
                                      sg => sg.IdCard == dfg
                                    );
                            }
                        }
                        break;
                    #endregion

                    #region 教育厅更新学校用
                    case "JYTGX":
                        dtList = ExcelHelper.GetTables(SynonymList,
                           new List<string> { "IdCard", "StudentName", "GrandSchool" },
                           new List<string> { "A5", "A7" }, p1);

                        List<DataRow> rowsSth = new List<DataRow>();

                        foreach (DataTable table in dtList)
                        {
                            foreach (DataRow row in table.Rows)
                            {
                                if (row.RowState == DataRowState.Deleted)
                                {
                                    continue;
                                }
                                rowsSth.Add(row);
                            }
                        }
                        foreach (DataRow row in rowsSth)
                        {
                            string dfg = row["IdCard"].ToString();
                            MathRoleAuthorEntities mathRoleAuthorEntities = new MathRoleAuthorEntities();
                            Math_JYT modal = mathRoleAuthorEntities.Math_JYT.FirstOrDefault(i => i.IdCard == dfg);
                            if (modal != null)
                            {
                                if (modal.GrandSchool == "其他学校")
                                {
                                    modal.GrandSchool = row["A5"].ToString() + "|" + row["GrandSchool"].ToString() + "|" + row["A7"].ToString();
                                    mathRoleAuthorEntities.SaveChanges();
                                }
                            }
                        }
                        break;
                    #endregion

                    #region  财务处 1、财务处错误数据 2、财务的数据
                    case "CWCERR":
                        dtList = ExcelHelper.GetTables(SynonymList,
                         new List<string> { "IdCard", "StudentName" },
                         new List<string> { }, p1);

                        List<DataRow> rows1 = new List<DataRow>();

                        foreach (DataTable table in dtList)
                        {
                            foreach (DataRow row in table.Rows)
                            {
                                if (row.RowState == DataRowState.Deleted)
                                {
                                    continue;
                                }
                                rows1.Add(row);
                            }
                        }
                        foreach (DataRow row in rows1)
                        {
                            string dfg = row["IdCard"].ToString();
                            new BLL_Math_CWC().update(dfg);
                        }
                        break;

                    case "CWC":
                        dtList = ExcelHelper.GetTables(SynonymList,
                          new List<string> { "IdCard", "StudentName", "FeeType" },
                          new List<string> { }, p1);
                        BLL_Math_CWC bLL_Math_CWC = new BLL_Math_CWC();
                        bLL_Math_CWC.clearTable();

                        List<DataRow> rows = new List<DataRow>();

                        foreach (DataTable table in dtList)
                        {
                            foreach (DataRow row in table.Rows)
                            {
                                if (row.RowState == DataRowState.Deleted)
                                {
                                    continue;
                                }
                                rows.Add(row);
                            }
                        }
                        foreach (DataRow row in rows)
                        {
                            string dfg = row["IdCard"].ToString();
                            int cc = (rows.Any(item => item["FeeType"].ToString().Contains("住宿费") && item["IdCard"].ToString() == dfg) ? 1 : 0)
                                  + (rows.Any(item => item["FeeType"].ToString().Contains("学费") && item["IdCard"].ToString() == dfg) ? 1 : 0);

                            if (rows.Any(item => item["FeeType"].ToString().Contains("学宿费") && item["IdCard"].ToString() == dfg))
                            {
                                cc = 2;
                            }

                            bLL_Math_CWC.Add(row["StudentName"].ToString(),
                                row["IdCard"].ToString(),
                                  DateTime.Now,
                                  DateTime.Now,
                                  "sheet",
                                  cc,
                                  sg => sg.IdCard == dfg
                                );
                        }
                        break;
                    #endregion

                    #region  生源基地
                    case "SYJD":
                        dtList = ExcelHelper.GetTables(SynonymList,
                        new List<string> { "GrandSchool", "Who" },
                        new List<string> { }, p1);
                        BLL_Math_SYJD bLL_Math_SYJD = new BLL_Math_SYJD();
                        bLL_Math_SYJD.clearTable();
                        foreach (DataTable table in dtList)
                        {
                            foreach (DataRow row in table.Rows)
                            {
                                if (row.RowState == DataRowState.Deleted)
                                {
                                    continue;
                                }
                                bLL_Math_SYJD.Add(row["GrandSchool"].ToString(),
                                    row["Who"].ToString(),
                                      DateTime.Now,
                                      DateTime.Now,
                                      table.TableName
                                    );
                            }
                        }
                        break;
                    #endregion

                    #region  学生处
                    case "XSC":
                        dtList = ExcelHelper.GetTables(SynonymList,
                         new List<string> { "IdCard", "StudentName", "StudentNumber" },
                         new List<string> { }, p1);
                        BLL_Math_XSC bLL_Math_XSC = new BLL_Math_XSC();
                        bLL_Math_XSC.clearTable();
                        foreach (DataTable table in dtList)
                        {
                            foreach (DataRow row in table.Rows)
                            {
                                if (row.RowState == DataRowState.Deleted)
                                {
                                    continue;
                                }
                                string dfg = row["IdCard"].ToString();
                                bLL_Math_XSC.Add(row["StudentName"].ToString(),
                                    row["IdCard"].ToString(),
                                      DateTime.Now,
                                      DateTime.Now,
                                      table.TableName,
                                      row["StudentNumber"].ToString(),
                                      sg => sg.IdCard == dfg
                                    );
                            }
                        }
                        break;
                    #endregion

                    #region 站长区域
                    case "ZZ":
                        dtList = ExcelHelper.GetTables(SynonymList,
                                         new List<string> { "zhanzhangdaqu", "zhanzhangquyu", "zhanzhang" },
                                         new List<string> { }, p1);
                        BLL_Math_ZZ bLL_Math_ZZ = new BLL_Math_ZZ();
                        bLL_Math_ZZ.clearTable();
                        foreach (DataTable table in dtList)
                        {
                            foreach (DataRow row in table.Rows)
                            {
                                if (row.RowState == DataRowState.Deleted)
                                {
                                    continue;
                                }
                                bLL_Math_ZZ.Add(row["zhanzhang"].ToString(), row["zhanzhangdaqu"].ToString(), row["zhanzhangquyu"].ToString(), i => false);
                            }
                        }
                        break;

                        #endregion
                }
                return Json(new
                {
                    src = localURL.Trim().Replace("\\", "|"),
                    name = Path.GetFileNameWithoutExtension(file.FileName),
                    suc = true
                });
            }
            catch (Exception ex)
            {
                return Json(new
                {
                    src = "",
                    name = "",   // 获取文件名不含后缀名
                    msg = "上传出错" + ex.Message,
                    suc = false

                });
            }
        }

        [Authorize]
        public ActionResult GetData()
        {
            int total = 0;
            List<Math_FileUpload> list = new BLL_Math_FileUpload().Search(-1, -1, out total);
            List<Dto_Math_FileUpload> l1 = list.Select(item => Mapper.Map<Dto_Math_FileUpload>(item)).ToList();

            UIRowsData<Dto_Math_FileUpload> datatablesModel = new UIRowsData<Dto_Math_FileUpload>()
            {
                remark = string.Empty,
                rows = l1,
                status = 1,
                suc = true,
                total = total,
                A1 = new BLL_Math_JYT().GetTotal(),
                A2 = new BLL_Math_CWC().GetTotal(),
                A3 = new BLL_Math_XSC().GetTotal(),
                A4 = new BLL_Math_SYJD().GetTotal(),
                A5=new BLL_Math_ZZ().GetTotal(),
            };
            return new JsonResult() { Data = datatablesModel, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
    }
}