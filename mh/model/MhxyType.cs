using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;

namespace mh.model
{
    class MhxyType
    {
        public int TypeId;
        public string prefix;
        public string Name;
        public int IsShow;
        public string filertRule;
        /// <summary>
        /// 获取全部数据
        /// </summary>
        /// <returns></returns>
        public static List<MhxyType> GetAll()
        {
            string sql = "select * from mhxy_type";
            SQLiteConnection conn = SQLiteUtil.GetConn();
            conn.Open();
            DataSet ds = SQLiteHelper.ExecuteDataSet(conn, sql, null);
            conn.Close();
            List<MhxyType> ret = new List<MhxyType>();
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                MhxyType mt = new MhxyType();
                mt.TypeId = Convert.ToInt32(ds.Tables[0].Rows[i]["type_id"].ToString());
                mt.Name = ds.Tables[0].Rows[i]["name"].ToString();
                mt.IsShow = Convert.ToInt32(ds.Tables[0].Rows[i]["is_show"].ToString());
                mt.prefix = ds.Tables[0].Rows[i]["prefix"].ToString();
                mt.filertRule = ds.Tables[0].Rows[i]["filert_rule"].ToString();
                ret.Add(mt);

            }
            return ret;
        }
        /// <summary>
        /// 修改状态
        /// </summary>
        /// <param name="typeId"></param>
        /// <param name="value"></param>
        /// <returns>返回影响行数</returns>
        public static int UpdateShow(int typeId, int value)
        {

            string sql = "update mhxy_type set is_show=" + value.ToString() + "  where type_id=" + typeId.ToString();
            SQLiteConnection conn = SQLiteUtil.GetConn();
            conn.Open();
            SQLiteCommand cmd = SQLiteHelper.CreateCommand(conn, sql);
            int ret = cmd.ExecuteNonQuery();
            conn.Close();
            return ret;
        }
        public static void Add(MhxyType mt)
        {
            SQLiteConnection conn = SQLiteUtil.GetConn();
            conn.Open();
            string sql = "INSERT INTO mhxy_type (type_id, name, is_show,prefix,filert_rule) VALUES(@type_id, @name, @is_show,@prefix,@filert_rule)";
            SQLiteParameter[] parameters = {
                              new SQLiteParameter("@type_id",DbType.Int32,4),
                              new SQLiteParameter("@name", DbType.String),
                              new SQLiteParameter("@is_show", DbType.Int32,4),
                              new SQLiteParameter("@prefix", DbType.String),
                              new SQLiteParameter("@filert_rule", DbType.String)

                        };
            parameters[0].Value = mt.TypeId;
            parameters[1].Value = mt.Name;
            parameters[2].Value = mt.IsShow;
            parameters[3].Value = mt.prefix;
            parameters[4].Value = mt.filertRule;
            SQLiteCommand cmd = SQLiteHelper.CreateCommand(conn, sql, parameters);
            cmd.ExecuteNonQuery();
            conn.Close();
        }
        public static bool CheckTypeId(int typeId)
        {
            SQLiteConnection conn = SQLiteUtil.GetConn();
            conn.Open();
            string sql = "select type_id from mhxy_type where type_id=" + typeId.ToString();
            DataSet ds = SQLiteHelper.ExecuteDataSet(conn, sql, null);
            conn.Close();
            DataTable dt = ds.Tables[0];
            int count=dt.Rows.Count;
            if (count>0)
            {
                return true;
            }
            return false;
        }
    }
}
