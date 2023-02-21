using MathSoftModelLib;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;

namespace MathSoftCommonLib
{
    #region 代码生成器 辅助类型
    /// <summary>
    ///代码生成器 辅助类型
    /// </summary>
    public static class DataTableHelper
    {
        #region 将 数据列转换成 public datetime? Insertime 这种格式
        /// <summary>
        /// 将 数据列转换成 public datetime? Insertime 这种格式
        /// </summary>
        /// <param name="col"></param>
        /// <returns></returns>
        public static System.String DataColumnToString(DataColumn col)
        {
            string fomater = "public {0}{1} {2}  ";

            if (col.DataType.ToString().ToUpper().Contains("string".ToUpper()))
            {
                string a = col.DataType.ToString();
                string b = " ";
                string c = col.ColumnName;
                return string.Format(fomater, a, b, c) + " {get;set;}";
            }
            else if (col.AllowDBNull)
            {
                return string.Format(fomater, col.DataType.ToString(), "?", col.ColumnName) + " {get;set;}";
            }
            else
            {
                return string.Format(fomater, col.DataType.ToString(), string.Empty, col.ColumnName) + "  {get;set;} ";
            }
        }
        #endregion

        #region 将表转换成 类结构 public  class tablename{ .....；......；......；}
        /// <summary>
        /// 将表转换成 类结构 public  class tablename{ .....；
        /// ......；
        /// ......；}
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static System.String DataTableToString(DataTable dt)
        {
            string ret = string.Empty;
            List<string> list = new List<string>();
            foreach (DataColumn item in dt.Columns)
            {
                var strItem = DataTableHelper.DataColumnToString(item);
                list.Add(strItem);
            }
            ret = StringHelper.GetArray2Str<string>(list, "\n");

            ret = " public class  " + dt.TableName + "{\n" + ret + "\n}";

            return ret;
        }
        #endregion

        #region 将dt转化为泛型
        /// <summary>
        /// 将dt转化为泛型
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static List<T> ToList<T>(this DataTable dt) where T : new()
        {
            List<T> list = new List<T>();
            if (dt == null)
            {
                return list;
            }
            foreach (DataRow row in dt.Rows)
            {
                T t = new T();
                Type Ts = t.GetType();
                PropertyInfo[] properts = Ts.GetProperties();
                foreach (PropertyInfo item in properts)
                {
                    string name = item.Name;
                    if (dt.Columns.Contains(name))
                    {
                        #region Int16类型 包含Int16?
                        if (item.PropertyType == typeof(Int16))
                        {
                            item.SetValue(t, Int16.Parse(row[name].ToString()), null);
                            continue;
                        }

                        if (item.PropertyType == typeof(Int16?))
                        {
                            if (string.IsNullOrEmpty(row[name].ToString()))
                            {

                            }
                            else
                            {
                                item.SetValue(t, Int16.Parse(row[name].ToString()), null);
                            }
                            continue;
                        }
                        #endregion

                        #region int类型 包含int?
                        if (item.PropertyType == typeof(int))
                        {
                            item.SetValue(t, int.Parse(row[name].ToString()), null);
                            continue;
                        }

                        if (item.PropertyType == typeof(int?))
                        {
                            if (string.IsNullOrEmpty(row[name].ToString()))
                            {

                            }
                            else
                            {
                                item.SetValue(t, int.Parse(row[name].ToString()), null);
                            }
                            continue;
                        }
                        #endregion

                        #region Int32类型 包含Int32?
                        if (item.PropertyType == typeof(Int32))
                        {
                            item.SetValue(t, Int32.Parse(row[name].ToString()), null);
                            continue;
                        }

                        if (item.PropertyType == typeof(Int32?))
                        {
                            if (string.IsNullOrEmpty(row[name].ToString()))
                            {

                            }
                            else
                            {
                                item.SetValue(t, Int32.Parse(row[name].ToString()), null);
                            }
                            continue;
                        }
                        #endregion

                        #region float类型 包含float?
                        if (item.PropertyType == typeof(float))
                        {
                            item.SetValue(t, float.Parse(row[name].ToString()), null);
                            continue;
                        }

                        if (item.PropertyType == typeof(float?))
                        {
                            if (string.IsNullOrEmpty(row[name].ToString()))
                            {

                            }
                            else
                            {
                                item.SetValue(t, float.Parse(row[name].ToString()), null);
                            }
                            continue;
                        }
                        #endregion

                        #region decimal类型 包含decimal?
                        if (item.PropertyType == typeof(decimal))
                        {
                            item.SetValue(t, decimal.Parse(row[name].ToString()), null);

                            continue;
                        }

                        if (item.PropertyType == typeof(decimal?))
                        {
                            if (string.IsNullOrEmpty(row[name].ToString()))
                            {

                            }
                            else
                            {
                                item.SetValue(t, decimal.Parse(row[name].ToString()), null);
                            }

                            continue;
                        }
                        #endregion

                        #region double类型 包含double?
                        if (item.PropertyType == typeof(double))
                        {
                            item.SetValue(t, double.Parse(row[name].ToString()), null);

                            continue;
                        }

                        if (item.PropertyType == typeof(double?))
                        {
                            if (string.IsNullOrEmpty(row[name].ToString()))
                            {

                            }
                            else
                            {
                                item.SetValue(t, double.Parse(row[name].ToString()), null);
                            }

                            continue;
                        }
                        #endregion

                        #region DateTime类型 包含DateTime?
                        if (item.PropertyType == typeof(DateTime))
                        {
                            item.SetValue(t, DateTime.Parse(row[name].ToString()), null);

                            continue;
                        }

                        if (item.PropertyType == typeof(DateTime?))
                        {
                            if (string.IsNullOrEmpty(row[name].ToString()))
                            {

                            }
                            else
                            {
                                item.SetValue(t, Convert.ToDateTime(row[name]), null);
                            }

                            continue;
                        }
                        #endregion

                        #region Guid类型 包含Guid?
                        if (item.PropertyType == typeof(Guid))
                        {
                            item.SetValue(t, Guid.Parse(row[name].ToString()), null);

                            continue;
                        }

                        if (item.PropertyType == typeof(Guid?))
                        {
                            if (string.IsNullOrEmpty(row[name].ToString()))
                            {

                            }
                            else
                            {
                                item.SetValue(t, Guid.Parse(row[name].ToString()), null);
                            }

                            continue;
                        }
                        #endregion

                        #region string 类型
                        if (!string.IsNullOrEmpty(row[name].ToString()))
                        {
                            item.SetValue(t, row[name].ToString(), null);
                            continue;
                        }
                        #endregion
                    }
                }

                list.Add(t);
            }
            return list;
        }
        #endregion

        #region 对比两个datatable 比较列
        /// <summary>
        /// 对比两个datatable 比较列 
        /// 马良
        /// 2015年9月30日
        /// 如果返回值数量为0 则相同 如果>0 则不同
        /// </summary>
        /// <param name="dt1"></param>
        /// <param name="dt2"></param>
        /// <returns></returns>
        public static List<NameValueKeyPair> DiffTablesInCols(DataTable dt1, DataTable dt2)
        {
            List<NameValueKeyPair> listRet = new List<NameValueKeyPair>();

            List<string> list1 = new List<string>();
            foreach (DataColumn item in dt1.Columns)
            {
                list1.Add(item.ColumnName);
            }

            List<string> list2 = new List<string>();
            foreach (DataColumn item in dt2.Columns)
            {
                list1.Add(item.ColumnName);
            }

            //将两方面的整合
            List<string> list3 = list1.Union(list2).ToList();

            foreach (var item in list3)
            {
                NameValueKeyPair model = new NameValueKeyPair() { Name = item, };
                var exists1 = dt1.Columns.Contains(item);
                var exists2 = dt2.Columns.Contains(item);

                //都存在
                if (exists1 && exists2)
                {
                    if (dt1.Columns[item].DataType == dt2.Columns[item].DataType)
                    {
                    }
                    else
                    {
                        model.Value = string.Format("类型不同，左边表中为{0}，右边表中为{1}", dt1.Columns[item].DataType.ToString(),
                            dt2.Columns[item].DataType.ToString())
                            ;
                        listRet.Add(model);
                    }
                }
                else if (!exists1)
                {
                    model.Value = "左边表中没有，右边表中有";
                    listRet.Add(model);
                }
                else if (!exists2)
                {
                    model.Value = "左边表中有，右边表中没有";
                    listRet.Add(model);
                }
                else
                {
                    continue;
                }
            }
            return listRet;
        }
        #endregion

        //下面的方法来自北京南天软件的公共类库 目前没有验证 2015年9月30日 共 1 2 3 4 5 6 7七个方法
        //借款申请单
        //用这种方式获取dataset
        //XmlSerializer xmlLoanApplyInfo = new XmlSerializer(typeof(LoanApplyModel));
        //StringWriter sLoanApplyInfo = new StringWriter();
        //XmlTextWriter swtzd = new XmlTextWriter(sLoanApplyInfo);
        //xmlLoanApplyInfo.Serialize(swtzd, loanApplyInfo);
        //DataSet dsTzd = new DataSet();
        //dsTzd.ReadXml(new StringReader(sLoanApplyInfo.ToString()), XmlReadMode.Auto);

        #region 根据tablename,ds 得到insert的sql语句 1
        /// <summary>
        /// 根据tablename,ds 得到insert的sql语句
        /// </summary>
        /// <param name="tablename">数据库表名</param>
        /// <param name="ds">数据集合</param>
        /// <param name="tzdType">通知单类型</param>
        public static string AddSql(string tablename, DataSet ds)
        {
            string colname = "";
            string colvalue = "";
            string ls_colstr = "";
            string sql = "";




            for (int j = 0; j < ds.Tables[0].Rows.Count; j++)
            {
                colvalue = "";
                colname = "";

                for (int i = 0; i < ds.Tables[0].Columns.Count; i++)
                {
                    DataColumn column = ds.Tables[0].Columns[i];
                    if (column.AutoIncrement == true)
                        continue;
                    colname = colname + column.ColumnName + ",";

                    object lx = ds.Tables[0].Columns[i].DataType.ToString();
                    switch (lx.ToString())
                    {
                        case "System.String":
                            ls_colstr = "'" + ds.Tables[0].Rows[j][i].ToString() + "',";
                            break;
                        case "System.DateTime":
                            if (ds.Tables[0].Columns[i].ToString() == "")
                                ls_colstr = "null,";
                            else
                                ls_colstr = "'" + ds.Tables[0].Rows[j][i].ToString() + "',";
                            break;
                        case "System.Decimal":
                            if (ds.Tables[0].Rows[j][i].ToString() == "")
                                ls_colstr = "null,";
                            else
                                try
                                {
                                    ls_colstr = Convert.ToString(ds.Tables[0].Rows[j][i]) + ",";
                                }
                                catch (Exception e)
                                {
                                    throw e.GetBaseException();

                                }
                            break;
                        case "System.Int32":
                            if (ds.Tables[0].Rows[j][i].ToString() == "")
                                ls_colstr = "null,";
                            else
                                try
                                {
                                    ls_colstr = Convert.ToString(ds.Tables[0].Rows[j][i]) + ",";
                                }
                                catch (Exception ex)
                                {
                                    throw ex.GetBaseException();

                                }
                            break;
                        case "System.Byte[]":
                            ls_colstr = "null,";
                            break;

                    }
                    colvalue = colvalue + ls_colstr;
                }
                sql = sql + "insert into " + tablename + "(" + colname.Substring(0, colname.Length - 1) + ") values (" + colvalue.Substring(0, colvalue.Length - 1) + ");";
            }


            return sql;
        }
        #endregion

        #region 根据tablename,ds 得到Update的sql语句 2
        /// <summary>
        /// 根据tablename,ds 得到Update的sql语句
        /// </summary>
        /// <param name="tablename">数据库表名</param>
        /// <param name="ds">数据集合</param>
        public static string UpdateSql(string tablename, DataSet ds, string keyname)
        {
            string colname = "";
            string colvalue = "";
            string ls_colstr = "";
            string sql = "";


            for (int j = 0; j < ds.Tables[0].Rows.Count; j++)
            {
                for (int i = 0; i < ds.Tables[0].Columns.Count; i++)
                {
                    DataColumn column = ds.Tables[0].Columns[i];
                    if (column.AutoIncrement == true)
                        continue;
                    colname = ds.Tables[0].Columns[i].ColumnName + "=";

                    object lx = ds.Tables[0].Columns[i].DataType.ToString();
                    switch (lx.ToString())
                    {
                        case "System.String":
                            ls_colstr = "'" + ds.Tables[0].Rows[j][i].ToString() + "',";
                            break;
                        case "System.DateTime":
                            if (ds.Tables[0].Columns[i].ToString() == "")
                                ls_colstr = "null,";
                            else
                                ls_colstr = "'" + ds.Tables[0].Rows[j][i].ToString() + "',";
                            break;
                        case "System.Decimal":
                            if (ds.Tables[0].Rows[j][i].ToString() == "")
                                ls_colstr = "null,";
                            else
                                try
                                {
                                    ls_colstr = Convert.ToString(ds.Tables[0].Rows[j][i]) + ",";
                                }
                                catch (Exception e)
                                {
                                    throw e.GetBaseException();

                                }
                            break;
                        case "System.Int32":
                            if (ds.Tables[0].Rows[j][i].ToString() == "")
                                ls_colstr = "null,";
                            else
                                try
                                {
                                    ls_colstr = Convert.ToString(ds.Tables[0].Rows[j][i]) + ",";
                                }
                                catch (Exception ex)
                                {
                                    throw ex.GetBaseException();

                                }
                            break;
                        case "System.Byte[]":
                            //continue;
                            ls_colstr = "null,";
                            break;

                    }
                    colvalue = colvalue + colname + ls_colstr;
                }
                sql = sql + "update " + tablename + " set " + colvalue.Substring(0, colvalue.Length - 1) + " where " + keyname + "='" + ds.Tables[0].Rows[j][keyname].ToString().Trim() + "'";
            }

            return sql;
        }
        #endregion

        #region 同上 3
        public static string UpdateSql(string tablename, DataSet ds, string keyname1, string keyname2)
        {
            string colname = "";
            string colvalue = "";
            string ls_colstr = "";
            string sql = "";

            for (int j = 0; j < ds.Tables[0].Rows.Count; j++)
            {
                for (int i = 0; i < ds.Tables[0].Columns.Count; i++)
                {
                    colname = ds.Tables[0].Columns[i].ColumnName + "=";

                    object lx = ds.Tables[0].Columns[i].DataType.ToString();
                    switch (lx.ToString())
                    {
                        case "System.String":
                            ls_colstr = "'" + ds.Tables[0].Rows[j][i].ToString() + "',";
                            break;
                        case "System.DateTime":
                            if (ds.Tables[0].Columns[i].ToString() == "")
                                ls_colstr = "null,";
                            else
                                ls_colstr = "'" + ds.Tables[0].Rows[j][i].ToString() + "',";
                            break;
                        case "System.Decimal":
                            if (ds.Tables[0].Rows[j][i].ToString() == "")
                                ls_colstr = "null,";
                            else
                                try
                                {
                                    ls_colstr = Convert.ToString(ds.Tables[0].Rows[j][i]) + ",";
                                }
                                catch (Exception e)
                                {
                                    throw e.GetBaseException();

                                }
                            break;
                        case "System.Int32":
                            if (ds.Tables[0].Rows[j][i].ToString() == "")
                                ls_colstr = "null,";
                            else
                                try
                                {
                                    ls_colstr = Convert.ToString(ds.Tables[0].Rows[j][i]) + ",";
                                }
                                catch (Exception ex)
                                {
                                    throw ex.GetBaseException();

                                }
                            break;
                        case "System.Byte[]":
                            break;

                    }
                    colvalue = colvalue + colname + ls_colstr;
                }
                sql = sql + "update " + tablename + " set " + colvalue.Substring(0, colvalue.Length - 1) + " where " + keyname1 + "='" + ds.Tables[0].Rows[j][keyname1] + "' ";

                if (keyname2.ToString().Length > 0)
                    sql = sql + " and " + keyname2 + "='" + ds.Tables[0].Rows[j][keyname2] + "'";
            }

            return sql;

        }
        #endregion

        #region 同上  4
        //ds中存在作为条件的数据，要用旧的数据作为条件

        public static string UpdateSql(string tablename, DataSet ds, string olddata, string keyname1, string keyname2, string keyname3)
        {
            string colname = "";
            string colvalue = "";
            string ls_colstr = "";
            string sql = "";

            for (int j = 0; j < ds.Tables[0].Rows.Count; j++)
            {
                for (int i = 0; i < ds.Tables[0].Columns.Count; i++)
                {
                    colname = ds.Tables[0].Columns[i].ColumnName + "=";

                    object lx = ds.Tables[0].Columns[i].DataType.ToString();
                    switch (lx.ToString())
                    {
                        case "System.String":
                            ls_colstr = "'" + ds.Tables[0].Rows[j][i].ToString() + "',";
                            break;
                        case "System.DateTime":
                            if (ds.Tables[0].Columns[i].ToString() == "")
                                ls_colstr = "null,";
                            else
                                ls_colstr = "'" + ds.Tables[0].Rows[j][i].ToString() + "',";
                            break;
                        case "System.Decimal":
                            if (ds.Tables[0].Rows[j][i].ToString() == "")
                                ls_colstr = "null,";
                            else
                                try
                                {
                                    ls_colstr = Convert.ToString(ds.Tables[0].Rows[j][i]) + ",";
                                }
                                catch (Exception e)
                                {
                                    throw e.GetBaseException();

                                }
                            break;
                        case "System.Int32":
                            if (ds.Tables[0].Rows[j][i].ToString() == "")
                                ls_colstr = "null,";
                            else
                                try
                                {
                                    ls_colstr = Convert.ToString(ds.Tables[0].Rows[j][i]) + ",";
                                }
                                catch (Exception ex)
                                {
                                    throw ex.GetBaseException();

                                }
                            break;
                        case "System.Byte[]":
                            continue;
                         

                    }
                    colvalue = colvalue + colname + ls_colstr;
                }
                sql = sql + "update " + tablename + " set " + colvalue.Substring(0, colvalue.Length - 1) + " where " + keyname1 + "='" + ds.Tables[0].Rows[j][keyname1] + "' ";

                if (keyname2.ToString().Length > 0)
                    sql = sql + " and " + keyname2 + "='" + ds.Tables[0].Rows[j][keyname2] + "'";
                if (keyname3.Trim().Length > 0)
                {
                    sql = sql + " and " + keyname3 + " = '" + olddata + "'";
                }
            }

            return sql;

        }
        #endregion

        #region 同上 5
        public static string UpdateSql2(string tablename, DataSet ds, string olddata, string keyname1, string keyname2, string keyname3)
        {
            string colname = "";
            string ls_colstr = "";
            string sql = "";

            for (int j = 0; j < ds.Tables[0].Rows.Count; j++)
            {
                string colvalue = "";
                for (int i = 0; i < ds.Tables[0].Columns.Count; i++)
                {
                    colname = ds.Tables[0].Columns[i].ColumnName + "=";

                    object lx = ds.Tables[0].Columns[i].DataType.ToString();
                    switch (lx.ToString())
                    {
                        case "System.String":
                            ls_colstr = "'" + ds.Tables[0].Rows[j][i].ToString() + "',";
                            break;
                        case "System.DateTime":
                            if (ds.Tables[0].Columns[i].ToString() == "")
                                ls_colstr = "null,";
                            else
                                ls_colstr = "'" + ds.Tables[0].Rows[j][i].ToString() + "',";
                            break;
                        case "System.Decimal":
                            if (ds.Tables[0].Rows[j][i].ToString() == "")
                                ls_colstr = "null,";
                            else
                                try
                                {
                                    ls_colstr = Convert.ToString(ds.Tables[0].Rows[j][i]) + ",";
                                }
                                catch (Exception e)
                                {
                                    throw e.GetBaseException();

                                }
                            break;
                        case "System.Int32":
                            if (ds.Tables[0].Rows[j][i].ToString() == "")
                                ls_colstr = "null,";
                            else
                                try
                                {
                                    ls_colstr = Convert.ToString(ds.Tables[0].Rows[j][i]) + ",";
                                }
                                catch (Exception ex)
                                {
                                    throw ex.GetBaseException();

                                }
                            break;
                        case "System.Byte[]":
                            continue;
                    }
                    colvalue = colvalue + colname + ls_colstr;
                }
                sql = sql + "update " + tablename + " set " + colvalue.Substring(0, colvalue.Length - 1) + " where " + keyname1 + "='" + ds.Tables[0].Rows[j][keyname1] + "' ";

                if (keyname2.ToString().Length > 0)
                    sql = sql + " and " + keyname2 + "='" + ds.Tables[0].Rows[j][keyname2] + "'";
                if (keyname3.Trim().Length > 0)
                {
                    sql = sql + " and " + keyname3 + " = '" + olddata + "'";
                }
            }

            return sql;

        }
        #endregion

        #region 删除表中的记录 6
        /// <summary>
        /// 删除表中的记录
        /// </summary>
        /// <param name="tablename">数据库表名</param>
        /// <param name="ds">要删除的数据集合（只是关键字的集合就可以了）</param>
        public static string DeleteSql(string tablename, DataSet ds)
        {
            string colname = "";
            string colvalue = "";
            string ls_colstr = "";
            string sql = "";




            for (int j = 0; j < ds.Tables[0].Rows.Count; j++)
            {
                for (int i = 0; i < ds.Tables[0].Columns.Count; i++)
                {
                    colname = ds.Tables[0].Columns[i].ColumnName + "=";

                    object lx = ds.Tables[0].Columns[i].DataType.ToString();
                    switch (lx.ToString())
                    {
                        case "System.String":
                            ls_colstr = "'" + ds.Tables[0].Rows[j][i].ToString() + "'";
                            break;
                        case "System.DateTime":
                            if (ds.Tables[0].Columns[i].ToString() == "")
                                ls_colstr = "null,";
                            else
                                ls_colstr = "'" + ds.Tables[0].Rows[j][i].ToString() + "'";
                            break;
                        case "System.Decimal":
                            if (ds.Tables[0].Rows[j][i].ToString() == "")
                                ls_colstr = "null,";
                            else
                                try
                                {
                                    ls_colstr = Convert.ToString(ds.Tables[0].Rows[j][i]);
                                }
                                catch (Exception e)
                                {
                                    throw e.GetBaseException();

                                }
                            break;
                        case "System.Int32":
                            if (ds.Tables[0].Rows[j][i].ToString() == "")
                                ls_colstr = "null,";
                            else
                                try
                                {
                                    ls_colstr = Convert.ToString(ds.Tables[0].Rows[j][i]);
                                }
                                catch (Exception ex)
                                {
                                    throw ex.GetBaseException();

                                }
                            break;
                    }
                    colvalue = colvalue + colname + ls_colstr + " and ";
                }
                sql = sql + "delete " + tablename + " where  " + colvalue.Substring(0, colvalue.Length - 5);
            }

            return sql;
        }
        #endregion

        #region 得到的是sql查询中的where后面的语句 7
        /// <summary>
        /// 得到的是sql查询中的where后面的语句


        /// </summary>
        /// <param name="tablename">数据库表名</param>
        /// <param name="ds">要查询的数据集合（只是关键字的集合就可以了）</param>
        public static string SelectSql(string tablename, DataSet ds)
        {
            string colname = "";
            string colvalue = "";
            string ls_colstr = "";
            string sql = "";




            for (int j = 0; j < ds.Tables[0].Rows.Count; j++)
            {
                for (int i = 0; i < ds.Tables[0].Columns.Count; i++)
                {
                    colname = ds.Tables[0].Columns[i].ColumnName + "=";

                    object lx = ds.Tables[0].Columns[i].DataType.ToString();
                    switch (lx.ToString())
                    {
                        case "System.String":
                            ls_colstr = "'" + ds.Tables[0].Rows[j][i].ToString() + "'";
                            break;
                        case "System.DateTime":
                            if (ds.Tables[0].Columns[i].ToString() == "")
                                ls_colstr = "null,";
                            else
                                ls_colstr = "'" + ds.Tables[0].Rows[j][i].ToString() + "'";
                            break;
                        case "System.Decimal":
                            if (ds.Tables[0].Rows[j][i].ToString() == "")
                                ls_colstr = "null,";
                            else
                                try
                                {
                                    ls_colstr = Convert.ToString(ds.Tables[0].Rows[j][i]);
                                }
                                catch (Exception e)
                                {
                                    throw e.GetBaseException();

                                }
                            break;
                        case "System.Int32":
                            if (ds.Tables[0].Rows[j][i].ToString() == "")
                                ls_colstr = "null,";
                            else
                                try
                                {
                                    ls_colstr = Convert.ToString(ds.Tables[0].Rows[j][i]);
                                }
                                catch (Exception ex)
                                {
                                    throw ex.GetBaseException();

                                }
                            break;
                    }
                    colvalue = colvalue + colname + ls_colstr + " and ";
                }
                sql = colvalue.Substring(0, colvalue.Length - 5);
            }

            return sql;
        }

        #endregion

    }
    #endregion
}
