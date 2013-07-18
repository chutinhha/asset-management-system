using System;
using System.Collections.Generic;
//using System.Linq;
using System.Text;
using System.Data.SQLite;
using System.Data;
using System.Data.Common;
//SQLiteHelper ������ 
namespace IrRfidUHFDemo
{
    public class SQLiteHelper
    {
        /// <summary> 
        /// connectionString������Datasource=Test.db3;Pooling=true;FailIfMissing=false 
        /// </summary> 
        public static string connectionString = "Data Source=; Password=NAUARYSOAA";//{ get; set; }
        public static string sLastErr = "";

        //��ʼ������connectionStringΪ�գ����ڴ�SQLiteConnectionʵ�����ӵĲ���
        public SQLiteHelper(string dbPath)
        {
            connectionString = "Data Source=" + dbPath;
        }
        //��������һ����

        public static void CreateDB(string dbPath)
        {
            //����һ�����ݿ�.db�ļ�
            SQLiteConnection.CreateFile(dbPath);
            //��SQLiteConnection����
            using (SQLiteConnection connection = new SQLiteConnection("Data Source=" + dbPath))
            {
                //������
                connection.Open();
                //����һ��SQLiteCommandʵ�������ڶ�SQLite����ָ��
                using (SQLiteCommand command = new SQLiteCommand(connection))
                {
                    //����һ�������Ϊsql�﷨
                    command.CommandText = "Create Table Demo(id integer,name nvarchar(50))";
                    //ִ�����
                    command.ExecuteNonQuery();

                    command.CommandText = "insert into Demo(name)values('zhaoh')";
                    //ִ�����
                    command.ExecuteNonQuery();
                }
            }
        }

        //private static void PrepareCommand(SQLiteCommand cmd, SQLiteConnection conn,
        //string cmdText, params object[] p)
        //{
        //    if (conn.State != ConnectionState.Open)
        //        conn.Open();
        //    cmd.Parameters.Clear();
        //    cmd.Connection = conn;
        //    cmd.CommandText = cmdText;
        //    cmd.CommandType = CommandType.Text;
        //    cmd.CommandTimeout = 30;
        //    if (p != null)
        //    {
        //        foreach (object parm in p)
        //            cmd.Parameters.AddWithValue(string.Empty, parm);
        //    }
        //}
        private static void PrepareCommand(SQLiteCommand cmd, SQLiteConnection conn, SQLiteTransaction trans, 
            string cmdText, params object[] p)
        {
            if (conn.State != ConnectionState.Open)
                conn.Open();
            cmd.Parameters.Clear();
            cmd.Connection = conn;
            cmd.CommandText = cmdText;
            if (trans != null)
                cmd.Transaction = trans;
            cmd.CommandType = CommandType.Text;//cmdType; 
            cmd.CommandTimeout = 30;
            if (p != null)
            {
                 foreach (object parm in p)
                   cmd.Parameters.AddWithValue(string.Empty, parm);
            }
        }


        public static DataSet ExecuteQuery(string cmdText, params object[] p)
        {
            using (SQLiteConnection conn = new SQLiteConnection(connectionString))
            {
                using (SQLiteCommand command = new SQLiteCommand())
                {
                    DataSet ds = new DataSet();
                    PrepareCommand(command, conn, null,cmdText, p);
                    SQLiteDataAdapter da = new SQLiteDataAdapter(command);
                    da.Fill(ds);
                    return ds;
                }
            }
        }
        public static int ExecuteNonQuery(string cmdText, params object[] p)
        {
            using (SQLiteConnection conn = new SQLiteConnection(connectionString))
            {
                using (SQLiteCommand command = new SQLiteCommand())
                {
                    PrepareCommand(command, conn, null, cmdText, p);
                    return command.ExecuteNonQuery();
                }
            }
        }
        public static bool ExecuteNoQueryTran(List<String> listSql)
        {
            using (SQLiteConnection conn = new SQLiteConnection(connectionString))
            {
                sLastErr = "";
                conn.Open();
                SQLiteCommand cmd = new SQLiteCommand();
                cmd.Connection = conn;
                SQLiteTransaction tx = conn.BeginTransaction();
                cmd.Transaction = tx;
                cmd.CommandType = CommandType.Text;; 
                try
                {
                    for (int n = 0; n < listSql.Count; n++)
                    {
                        string strsql = listSql[n];
                        if (strsql.Trim().Length > 1)
                        {
                            sLastErr = strsql;
                            cmd.CommandText = strsql;
                            cmd.ExecuteNonQuery();
                        }
                    }
                    tx.Commit();
                    return true;
                }
                catch (SQLiteException e)
                {
                    tx.Rollback();
                    sLastErr = e.Message + sLastErr;
                    return false;
                }
            }
        }
        public static SQLiteDataReader ExecuteReader(string cmdText, params object[] p)
        {
            //using (SQLiteConnection conn = new SQLiteConnection(connectionString))
            //{
            //    using (SQLiteCommand command = new SQLiteCommand())
            //    {
            //        PrepareCommand(command, conn, null, cmdText, p);
            //        return command.ExecuteReader(CommandBehavior.CloseConnection);
            //    }
            //}
            SQLiteConnection conn = new SQLiteConnection(connectionString);
            SQLiteCommand command = new SQLiteCommand();
            PrepareCommand(command, conn, null, cmdText, p);
            return command.ExecuteReader(CommandBehavior.CloseConnection);

        }
        public static object ExecuteScalar(string cmdText, params object[] p)
        {
            using (SQLiteConnection conn = new SQLiteConnection(connectionString))
            {
                using (SQLiteCommand command = new SQLiteCommand())
                {
                    PrepareCommand(command, conn, null, cmdText, p);
                    return command.ExecuteScalar();
                }
            }
        }
        //�������������ds.Tables[0] ����sTblName�����ݿ������
        public static bool SqlBulkInsert(DataTable dt, string sTblName)
        {
            int affect = 0;
            SQLiteConnection conn = null;
            try
            {
                conn = new SQLiteConnection(connectionString);
                SQLiteCommand myCommand = new SQLiteCommand("select * from " + sTblName, conn);
                SQLiteDataAdapter myAdapter = new SQLiteDataAdapter(myCommand);
                SQLiteCommandBuilder myCommandBuilder = new SQLiteCommandBuilder(myAdapter);
                myAdapter.InsertCommand = myCommandBuilder.GetInsertCommand();
                foreach (DataRow dr in dt.Rows)
                {
                    if (dr.RowState != DataRowState.Added)
                    {
                        dr.SetAdded();
                    }
                }
                conn.Open();
                affect = myAdapter.Update(dt);
                conn.Close();
                return true;
            }
            catch (Exception e)
            {
                if(conn != null) conn.Close();
                sLastErr = e.Message;
            }
            return false;
        }
        //��������������ds.Tables[0] ����sTblName�����ݿ��������Ҫ��������
        public static bool SqlBulkUpdate(DataTable dt, string sTblName)
        {
            int affect = 0;
            SQLiteConnection conn = null;
            try
            {
                conn = new SQLiteConnection(connectionString);
                SQLiteCommand myCommand = new SQLiteCommand("select * from " + sTblName, conn);
                SQLiteDataAdapter myAdapter = new SQLiteDataAdapter(myCommand);
                SQLiteCommandBuilder myCommandBuilder = new SQLiteCommandBuilder(myAdapter);
                myAdapter.UpdateCommand = myCommandBuilder.GetUpdateCommand();
                conn.Open();
                affect = myAdapter.Update(dt);
                conn.Close();
                return true;
            }
            catch (Exception e)
            {
                if (conn != null) conn.Close();
                sLastErr = e.Message;
            }
            return false;
        }
    }
}


