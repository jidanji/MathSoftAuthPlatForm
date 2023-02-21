using MathSoftModelLib;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;

namespace MathSoftCommonLib
{
    public static class ExcelHelper
    {
        #region 从Excel中取出数据
        /// <summary>
        /// 永远都不要做重复性的工作
        /// 从Excel中取出数据。这个类库需要驱动程序。在个程序的驱动程序位于马良的希捷硬盘
        /// H:\软件\windows软件\开发工具（7）\office开发\驱动程序office2007下分别安装64位或者32位
        /// 
        /// </summary>
        /// <param name="fileName">文件名称</param>
        /// <param name="sheetName">Sheet1页面名称</param>
        /// <returns>datatable</returns>
        public static DataTable TransExcelToTable(string fileName, string sheetName)
        {
            string connFrom = "Provider=Microsoft.Ace.OleDB.12.0;Data Source=" + fileName + ";Extended Properties='Excel 12.0;HDR=YES;IMEX=1';";

            DataTable dt1 = new DataTable();
            using (OleDbConnection thisconnection = new OleDbConnection(connFrom))
            {
                thisconnection.Open();
                string Sql = "select * from [" + sheetName + "$]";
                OleDbDataAdapter mycommand = new OleDbDataAdapter(Sql, thisconnection);
                dt1 = new DataTable();
                mycommand.Fill(dt1);
                thisconnection.Close();
            }
            return dt1;
        }
        #endregion

        #region 获取一张excel中所有合理的table

        /// <summary>
        /// 获取一张excel中所有合理的table
        /// </summary>
        /// <param name="SynonymList">同义词列表</param>
        /// <param name="mustbeCols">必须含有的列</param>
        /// <param name="needCols">需要含有的列</param>
        /// <param name="path">文件的路径</param>
        /// <returns></returns>
        public static List<DataTable> GetTables(
            List<Synonym> SynonymList,
            List<string> mustbeCols,
            List<string> needCols,
            string path = null)
        {
            if (mustbeCols == null)
            {
                mustbeCols = new List<string>();
            }
            if (needCols == null)
            {
                needCols = new List<string>();
            }
            List<DataTable> list = new List<DataTable>();
            string SourceExcelPath = string.Empty;
            if (string.IsNullOrEmpty(path))
            {
                path = @"C:\Users\maliang63\Documents\WeChat Files\maliang19860121\FileStorage\File\2020-01\数据样本\财务缴费表 (2).xlsx";
            }
            string StrConn = "Provider=Microsoft.ACE.OLEDB.12.0;" + " Data Source=" + path + ";" + "Extended Properties='Excel 12.0;HDR=Yes;IMEX=1'";//路径的正确性
            OleDbConnection con = new OleDbConnection(StrConn);
            con.Open();//打开连接

            System.Data.DataTable SheetNames = con.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, new object[] { null, null, null, "Table" });//获取列表名称
            string[] TableNames = new string[SheetNames.Rows.Count];
            for (int k = 0; k < SheetNames.Rows.Count; ++k)
            {
                TableNames[k] = SheetNames.Rows[SheetNames.Rows.Count - k - 1]["TABLE_NAME"].ToString();//遍历
            }
            DataSet ds = new DataSet();
            foreach (string item in TableNames)
            {
                DataSet ds1 = new DataSet();
                OleDbDataAdapter myCommand = null;
                string strExcel = string.Format("select * from [{0}]", item);
                myCommand = new OleDbDataAdapter(strExcel, StrConn);

                myCommand.Fill(ds1, item);
                var dt = ds1.Tables[0];
                foreach (DataRow row in dt.Rows)
                {
                    ReplaceRowData(row);
                    if (IsRowEmpty(row))
                    {
                        row.Delete();
                    }
                }
                //解析同义词
                //dataDt.Columns["Name"].ColumnName = "Name_Old";
                var Columns = dt.Columns;
                foreach (DataColumn item1 in Columns)
                {
                    string cc = item1.ColumnName;
                    if (SynonymList.Select(cv => cv.Name).Contains(cc))
                    {
                        break;
                    }
                    else
                    {
                        var newColName = GetColNameBySynonymName(cc, SynonymList);
                        if (string.IsNullOrEmpty(newColName))
                        {
                        }
                        else
                        {
                            item1.ColumnName = newColName;
                        }
                    }

                }

                if (dt.Rows.Count > 0)
                {
                    if (funcmustBeCols(mustbeCols, dt))
                    {
                        funcNeedCols(needCols, dt);
                        list.Add(dt.Copy());
                    }
                }
            }
            return list;
        }
        #endregion

        #region  判断数据行是全部为空吗？
        /// <summary>
        /// 判断数据行是全部为空吗？
        /// </summary>
        /// <param name="row"></param>
        /// <returns></returns>
        public static bool IsRowEmpty(DataRow row)
        {
            bool ret = true;
            var cellCount = row.ItemArray.Count();
            for (int i = 0; i < cellCount; i++)
            {
                var sth = row.ItemArray[i].ToString();
                if (string.IsNullOrEmpty(sth))
                {
                    ret = ret & true;
                }
                else
                {
                    ret = ret & false;
                }
            }
            return ret;
        }


        #endregion

        #region 替换string类型的一些字符
        /// <summary>
        /// 替换string类型的一些字符
        /// </summary>
        /// <param name="row"></param>
        public static void ReplaceRowData(DataRow row)
        {

            var cellCount = row.ItemArray.Count();
            for (int i = 0; i < cellCount; i++)
            {
                if (row.ItemArray[i].GetType() == typeof(string))
                {

                    string resource = row.ItemArray[i].ToString();
                    resource = resource.Replace("`", "");
                    //想对row赋值就要这样写。
                    row.SetField<string>(i, resource);
                }
            }
        }
        #endregion

        #region 根据同义词获取列名
        /// <summary>
        /// 根据同义词获取列名
        /// </summary>
        /// <param name="name"></param>
        /// <param name="SynonymList"></param>
        /// <returns></returns>
        public static string GetColNameBySynonymName(string name, List<Synonym> SynonymList)
        {
            foreach (Synonym item in SynonymList)
            {
                if (item.SynonymNames.Contains(name))
                {
                    return item.Name;
                }
            }
            return string.Empty;
        }
        #endregion

        #region 在一张表里是否含有必须的列
        /// <summary>
        /// 在一张表里是否含有必须的列
        /// </summary>
        /// <param name="cols"></param>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static bool funcmustBeCols(List<string> cols, DataTable dt)
        {
            bool ret = true;
            foreach (string item in cols)
            {
                ret = ret && dt.Columns.Contains(item);
            }
            return ret;
        }
        #endregion

        #region 为表添加必要性的列
        /// <summary>
        /// 为表添加必要性的列
        /// </summary>
        /// <param name="cols"></param>
        /// <param name="dt"></param>
        public static void funcNeedCols(List<string> cols, DataTable dt)
        {
            foreach (string item in cols)
            {
                if (dt.Columns.Contains(item))
                {
                }
                else
                {
                    dt.Columns.Add(new DataColumn() { ColumnName = item, Caption = item, DataType = typeof(string) });
                }
            }
        }
        #endregion
    }
}
