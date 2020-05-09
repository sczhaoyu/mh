using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Text;

namespace mh
{
    class SQLiteUtil
    {
        static SQLiteUtil()
        {
            if (!File.Exists(dbf))
            {
                CreateNewDatabase(dbf);
            }

            //初始化调用
            TableCheck.load();
        }
        ////sql链接
        //private static SQLiteConnection conn = null;
        //数据文件地址
        private static string dbf = "mhxy.db";
        /// <summary>
        /// 创建空数据库
        /// </summary>
        /// <param name="name"></param>
        public static void CreateNewDatabase(string name)
        {
            SQLiteConnection.CreateFile(name);
        }

        //获得链接
        public static SQLiteConnection GetConn()
        {

            return new SQLiteConnection("Data Source=" + dbf + ";Version=3;");
        }
        /// <summary>
        /// 创建数据表
        /// </summary>
        /// <param name="name"></param>
        public static void createTable(string name, string fileds)
        {
            string sql = "create table " + name + " (" + fileds + ")";
            SQLiteConnection conn = GetConn();
            conn.Open();
            SQLiteCommand cmd = SQLiteHelper.CreateCommand(conn, sql);
            cmd.ExecuteNonQuery();
            conn.Close();
        }
        /// <summary>
        /// 查询表名称是否存在
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static bool ExitTable(string name)
        {
            SQLiteConnection conn = GetConn();
            string sql = "select count(*)  from sqlite_master  where type ='table' and name ='" + name + "'";
            conn.Open();
            DataSet ds = SQLiteHelper.ExecuteDataSet(GetConn(), sql, null);

            conn.Close();
            DataTable dt = ds.Tables[0];
            string count = ds.Tables[0].Rows[0][0].ToString();
            if (count != "0")
            {
                return true;
            }
            return false;

        }
    }
}
