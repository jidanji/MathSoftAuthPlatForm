using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace MathSoftCommonLib
{
    public class SqlHelper : IDisposable
    {
        private SqlConnection con;

        private string _connString;

        SqlTransaction trans;// = new SqlTransaction();

        #region 构造函数
        public SqlHelper(string strCon)
        {
            _connString = strCon;
        }

        public SqlHelper()
        {
            _connString = ConfigurationManager.AppSettings["conString"];
        }

        #endregion

        #region open
        /// <summary>
        /// Open the database connection
        /// </summary>
        private void Open()
        {
            if (con == null)
            {
                con = new SqlConnection(_connString);

                try
                {
                    con.Open();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
        #endregion

        #region make parameter
        /// <summary>
        /// Make parameter of the stored procedure
        /// </summary>
        /// <param name="ParamName">Parameter's name</param>
        /// <param name="DbType">Parameter's type</param>
        /// <param name="Size">Parameter's size</param>
        /// <param name="Direction">Parameter's direction</param>
        /// <param name="Value">Parameter's value</param>
        /// <returns>Sql Parameter</returns>
        public SqlParameter MakeParam(string ParamName, SqlDbType DbType, int Size, ParameterDirection Direction, object Value)
        {
            SqlParameter param;

            if (Size > 0)
            {
                param = new SqlParameter(ParamName, DbType, Size);
            }
            else
            {
                param = new SqlParameter(ParamName, DbType);
            }

            param.Direction = Direction;

            if (!(Direction == ParameterDirection.Output && Value == null))
            {
                if (Value == null)
                {
                    param.Value = DBNull.Value;
                }
                else
                {
                    param.Value = Value;
                }
            }
            else
            {
                param.Value = DBNull.Value;
            }

            return param;
        }

        /// <summary>
        /// Make in parameter
        /// </summary>
        /// <param name="ParamName">Parameter's name</param>
        /// <param name="DbType">Parameter's type</param>
        /// <param name="Size">Parameter's size</param>
        /// <param name="Value">Parameter's value</param>
        /// <returns>Sql Parameter</returns>
        public SqlParameter MakeInParam(string ParamName, SqlDbType DbType, int Size, object Value)
        {
            return MakeParam(ParamName, DbType, Size, ParameterDirection.Input, Value);
        }

        /// <summary>
        /// Make out parameter
        /// </summary>
        /// <param name="ParamName">Parameter's name</param>
        /// <param name="DbType">Parameter's type</param>
        /// <param name="Size">Parameter's size</param>
        /// <returns>Sql Parameter</returns>
        public SqlParameter MakeOutParam(string ParamName, SqlDbType DbType, int Size)
        {
            return MakeParam(ParamName, DbType, Size, ParameterDirection.Output, null);
        }
        #endregion

        #region 获取数据集

        #region 存储过程获取数据集
        /// <summary>
        /// Get dataset by stored procedure
        /// </summary>
        /// <param name="procName">Stored procedure's name</param>
        /// <param name="prams">Stored procedure's parameters</param>
        /// <returns>The dataset that conforms to the condition</returns>
        public DataSet RunProcGetDataSet(string procName, SqlParameter[] prams)
        {
            SqlCommand cmd = CreateProcCommand(procName, prams);
            cmd.CommandTimeout = 0;
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();

            adapter.Fill(ds);

            Close();

            cmd = null;

            return ds;
        }

        /// <summary>
        /// Get dataset by stored procedure
        /// </summary>
        /// <param name="procName">Stored procedure's name</param>
        /// <param name="prams">Stored procedure's parameters</param>
        /// <returns>The dataset that conforms to the condition</returns>
        public DataSet RunProcGetDataSet(string procName, SqlParameter[] prams, System.Data.Common.DataTableMappingCollection dtmapC)
        {
            SqlCommand cmd = CreateProcCommand(procName, prams);
            cmd.CommandTimeout = 0;
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataSet dataset = new DataSet();
            for (int i = 0; i < dtmapC.Count; i++)
                adapter.TableMappings.Add(dtmapC[i].SourceTable, dtmapC[i].DataSetTable);
            adapter.Fill(dataset);

            Close();

            cmd = null;

            return dataset;
        }

        /// <summary>
        /// Get dataset by stored procedure
        /// </summary>
        /// <param name="procName">Stored procedure's name</param>
        /// <returns>The dataset that conforms to the condition</returns>
        public DataSet RunProcGetDataSet(string procName)
        {
            SqlCommand cmd = CreateProcCommand(procName, null);
            cmd.CommandTimeout = 0;
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataSet dataset = new DataSet();

            adapter.Fill(dataset);

            Close();

            cmd = null;

            return dataset;
        }

        #endregion

        #region SQL语句获取数据集

        /// <summary>
        /// Get dataset by the query string
        /// </summary>
        /// <param name="querystr">Query string</param>
        /// <returns>The dataset that conforms to the condition</returns>
        public DataSet ExeQueryGetDataSet(string querystr)
        {
            Open();

            SqlCommand cmd = new SqlCommand(querystr, con);
            cmd.CommandTimeout = 0;
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataSet dataset = new DataSet();

            adapter.Fill(dataset);

            Close();

            cmd = null;

            return dataset;
        }
        /// <summary>
        /// Get dataset by the query string and its parameters
        /// </summary>
        /// <param name="querystr">Querty string</param>
        /// <param name="prams">Parameters</param>
        /// <returns>The dataset that conforms to the condition</returns>
        public object GetSingle(string querystr, params   SqlParameter[] prams)
        {
            Open();

            SqlCommand cmd = new SqlCommand(querystr, con);
            cmd.CommandTimeout = 0;
            if (prams != null)
            {
                foreach (SqlParameter parameter in prams)
                {
                    cmd.Parameters.Add(parameter);
                }
            }

            object obj = cmd.ExecuteScalar();

            Close();

            cmd = null;

            return obj;
        }

        /// <summary>
        /// Get dataset by the query string and its parameters
        /// </summary>
        /// <param name="querystr">Querty string</param>
        /// <param name="prams">Parameters</param>
        /// <returns>The dataset that conforms to the condition</returns>
        public DataSet ExeQueryGetDataSet(string querystr, SqlParameter[] prams)
        {
            Open();

            SqlCommand cmd = new SqlCommand(querystr, con);
            cmd.CommandTimeout = 0;
            if (prams != null)
            {
                foreach (SqlParameter parameter in prams)
                {
                    cmd.Parameters.Add(parameter);
                }
            }

            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataSet dataset = new DataSet();

            adapter.Fill(dataset);

            Close();

            cmd = null;

            return dataset;
        }



        #endregion

        #endregion

        #region 获取行数据集

        #region 存储过程获取行数据集
        /// <summary>
        /// execute stored procedure return a data reader, with parameter
        /// </summary>
        /// <param name="procName"></param>
        /// <param name="prams"></param>
        /// <returns></returns>
        public SqlDataReader ExeProcGetReader(string procName, SqlParameter[] prams)
        {
            SqlCommand cmd = CreateProcCommand(procName, prams);
            cmd.CommandTimeout = 0;
            return cmd.ExecuteReader(CommandBehavior.CloseConnection);
        }

        /// <summary>
        /// execute stored procedure return a data reader
        /// </summary>
        /// <param name="procName"></param>
        /// <returns></returns>
        public SqlDataReader ExeProcGetReader(string procName)
        {
            SqlCommand cmd = CreateProcCommand(procName, null);
            cmd.CommandTimeout = 0;
            return cmd.ExecuteReader(CommandBehavior.CloseConnection);
        }
        #endregion

        #region SQL语句获取行数据集
        /// <summary>
        /// Execute a query string and return a SqlDataReader object
        /// </summary>
        /// <param name="querystr">Query string</param>
        /// <returns>SqlDataReader</returns>
        public SqlDataReader ExeQueryGetReader(string querystr)
        {
            Open();

            SqlCommand cmd = new SqlCommand(querystr, con);
            cmd.CommandTimeout = 0;

            return cmd.ExecuteReader();
        }

        public SqlDataReader ExeQueryGetReader(string querystr, SqlParameter[] prams)
        {
            Open();

            SqlCommand cmd = new SqlCommand(querystr, con);
            if (prams != null)
            {
                foreach (SqlParameter parameter in prams)
                {
                    cmd.Parameters.Add(parameter);
                }
            }

            cmd.CommandTimeout = 0;

            return cmd.ExecuteReader();
        }

        #endregion

        #endregion

        #region 执行数据库命令

        #region 执行存储过程
        /// <summary>
        /// function execute non query
        /// </summary>
        /// <param name="procName"> stored procedure name</param>
        /// <returns></returns>
        public void ExeProc(string procName)
        {
            SqlCommand cmd = CreateProcCommand(procName, null);
            cmd.CommandTimeout = 0;
            if (con.State == ConnectionState.Closed)
                con.Open();

            cmd.ExecuteNonQuery();

            this.Close();

            cmd = null;
        }

        public void ExeProcWithTrans(string procName)
        {
            SqlCommand cmd = CreateProcCommand(procName, null);
            cmd.CommandTimeout = 0;
            cmd.Transaction = trans;

            cmd.ExecuteNonQuery();

            cmd = null;
        }

        /// <summary>
        /// execute non query with parameters
        /// </summary>
        /// <param name="procName"></param>
        /// <param name="prams"></param>
        /// <returns></returns>
        public void ExeProc(string procName, SqlParameter[] prams)
        {
            SqlCommand cmd = CreateProcCommand(procName, prams);
            cmd.CommandTimeout = 0;
            if (con.State == ConnectionState.Closed)
                con.Open();

            cmd.ExecuteNonQuery();

            this.Close();

            cmd = null;
        }

        public void ExeProcWithTrans(string procName, SqlParameter[] prams)
        {
            SqlCommand cmd = CreateProcCommand(procName, prams);
            cmd.CommandTimeout = 0;
            cmd.Transaction = trans;

            cmd.ExecuteNonQuery();

            cmd = null;
        }

        /// <summary>
        /// Create command object
        /// </summary>
        /// <param name="procName">Stored procedure's name</param>
        /// <param name="prams">Parameters</param>
        /// <returns>SqlCommand object</returns>
        private SqlCommand CreateProcCommand(string procName, SqlParameter[] prams)
        {
            Open();

            SqlCommand cmd = new SqlCommand(procName, con);
            cmd.CommandTimeout = 0;
            cmd.CommandType = CommandType.StoredProcedure;

            if (prams != null)
            {
                foreach (SqlParameter parameter in prams)
                {
                    cmd.Parameters.Add(parameter);
                }
            }

            return cmd;
        }
        #endregion

        #region 执行SQL命令
        /// <summary>
        /// Execute a query string and return nothing
        /// </summary>
        /// <param name="querystr">Query string</param>
        public int ExeQuery(string querystr)
        {
            int iRe = 0;
            Open();

            SqlCommand cmd = new SqlCommand(querystr, con);
            cmd.CommandTimeout = 0;
            if (con.State == ConnectionState.Closed)
                con.Open();

            iRe = cmd.ExecuteNonQuery();

            cmd = null;

            con.Close();
            return iRe;
        }

        public void ExeQueryWithTrans(string querystr)
        {
            SqlCommand cmd = new SqlCommand(querystr, con);
            cmd.CommandTimeout = 0;
            cmd.Transaction = trans;

            cmd.ExecuteNonQuery();

            cmd = null;
        }

        /// <summary>
        /// 执行多条SQL语句，实现数据库事务。
        /// </summary>
        /// <param name="SQLStringList">多条SQL语句</param>		
        public int ExecuteSqlTran(List<String> SQLStringList)
        {
            using (SqlConnection conn = new SqlConnection(_connString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                SqlTransaction tx = conn.BeginTransaction();
                cmd.Transaction = tx;
                try
                {
                    int count = 0;
                    for (int n = 0; n < SQLStringList.Count; n++)
                    {
                        string strsql = SQLStringList[n];
                        if (strsql.Trim().Length > 1)
                        {
                            cmd.CommandText = strsql;
                            count += cmd.ExecuteNonQuery();
                        }
                    }
                    tx.Commit();
                    return count;
                }
                catch (Exception ex)
                {
                    tx.Rollback();
                    throw ex;
                }
            }
        }

        /// <summary>
        /// Execure a query string and return nothing
        /// </summary>
        /// <param name="querystr">Query string</param>
        /// <param name="prams">Parameters</param>
        public void ExeQuery(string querystr, SqlParameter[] prams)
        {
            Open();

            SqlCommand cmd = new SqlCommand(querystr, con);
            cmd.CommandTimeout = 0;
            if (prams != null)
            {
                foreach (SqlParameter parameter in prams)
                {
                    cmd.Parameters.Add(parameter);
                }
            }

            if (con.State == ConnectionState.Closed)
                con.Open();

            cmd.ExecuteNonQuery();
            //cmd.ExecuteReader();
            cmd = null;

            con.Close();
        }

        public void ExeQueryWithTrans(string querystr, SqlParameter[] prams)
        {
            SqlCommand cmd = new SqlCommand(querystr, con);
            cmd.CommandTimeout = 0;
            cmd.Transaction = trans;

            if (prams != null)
            {
                foreach (SqlParameter parameter in prams)
                {
                    cmd.Parameters.Add(parameter);
                }
            }

            cmd.ExecuteNonQuery();

            cmd = null;
        }
        #endregion

        #endregion

        #region 事务
        /// <summary>
        /// Begin a transaction operate
        /// </summary>
        public void BeginTrans()
        {
            this.Open();

            SqlTransaction st = con.BeginTransaction();
            trans = st;
        }

        /// <summary>
        /// Rollback a transaction operate
        /// </summary>
        public void RollBackTrans()
        {
            trans.Rollback();
            this.Close();
        }

        /// <summary>
        /// Commit a transaction operate
        /// </summary>
        public void CommitTrans()
        {
            trans.Commit();
            this.Close();
        }
        #endregion

        #region 关闭数据库连接
        /// <summary>
        /// close database connection
        /// </summary>
        public void Close()
        {
            if (con != null)
            {
                con.Close();
            }
        }
        #endregion

        #region 判断数据库的连接状态
        /// <summary>
        /// 判断数据库的连接状态
        /// </summary>
        /// <returns></returns>
        public string IsOpen()
        {
            if (con == null)
            {
                return "Close";
            }
            else
            {
                return "Open";
            }
        }
        #endregion

        #region 释放数据库的连接
        /// <summary>
        /// 释放数据库的连接
        /// </summary>
        public void Dispose()
        {
            if (con != null)
            {
                con.Dispose();

                con = null;
            }
        }
        #endregion

    }
}
