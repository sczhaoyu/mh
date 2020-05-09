using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;

namespace mh.model
{

    /// <summary>
    /// 地图区域过滤
    /// </summary>
    class MhxyMapRegion
    {
        //地图区域的四个点
        public int id;
        public int map_id;
        public string name;

        public int x;
        public int y;

        public int max_x;
        public int max_y;
        /// <summary>
        /// 区域间随机坐标轴
        /// </summary>
        /// <returns></returns>
        public int[] randomXY()
        {
            int[] ret = new int[2];
            Random r = new Random();
            ret[0] = r.Next(x, max_x);
            ret[1] = r.Next(y, max_y);
            return ret;
        }
        public static void Delete(int id) {
            SQLiteConnection conn = SQLiteUtil.GetConn();
            conn.Open();
            string sql = "delete from mhxy_map_region  where id=@id";
            SQLiteParameter[] parameters = {
                              new SQLiteParameter("@id", DbType.Int32,4),
                        };
            parameters[0].Value = id;


            SQLiteCommand cmd = SQLiteHelper.CreateCommand(conn, sql, parameters);
            cmd.ExecuteNonQuery();
            conn.Close();
        }
        /// <summary>
        /// 获得地图行走区域
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static List<MhxyMapRegion> GetMapID(int map_id)
        {
            List<MhxyMapRegion> ret = new List<MhxyMapRegion>();
            string sql = "select * from mhxy_map_region where map_id=" + map_id.ToString();
            SQLiteConnection conn = SQLiteUtil.GetConn();
            conn.Open();
            DataSet ds = SQLiteHelper.ExecuteDataSet(conn, sql, null);
            conn.Close();

            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                MhxyMapRegion m = new MhxyMapRegion();
                m.id = Convert.ToInt32(ds.Tables[0].Rows[i]["id"].ToString());
                m.map_id = Convert.ToInt32(ds.Tables[0].Rows[i]["map_id"].ToString());

                m.name = ds.Tables[0].Rows[i]["name"].ToString();
                m.x = Convert.ToInt32(ds.Tables[0].Rows[i]["x"].ToString());
                m.y = Convert.ToInt32(ds.Tables[0].Rows[i]["y"].ToString());
                m.max_x = Convert.ToInt32(ds.Tables[0].Rows[i]["max_x"].ToString());
                m.max_y = Convert.ToInt32(ds.Tables[0].Rows[i]["max_y"].ToString());

                ret.Add(m);

            }
            return ret;
        }
        /// <summary>
        /// 获取全部数据
        /// </summary>
        /// <returns></returns>
        public static List<MhxyMapRegion> GetMapAll()
        {
            List<MhxyMapRegion> ret = new List<MhxyMapRegion>();
            string sql = "select * from mhxy_map_region";
            SQLiteConnection conn = SQLiteUtil.GetConn();
            conn.Open();
            DataSet ds = SQLiteHelper.ExecuteDataSet(conn, sql, null);
            conn.Close();

            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                MhxyMapRegion m = new MhxyMapRegion();
                m.id = Convert.ToInt32(ds.Tables[0].Rows[i]["id"].ToString());
                m.map_id = Convert.ToInt32(ds.Tables[0].Rows[i]["map_id"].ToString());

                m.name = ds.Tables[0].Rows[i]["name"].ToString();
                m.x = Convert.ToInt32(ds.Tables[0].Rows[i]["x"].ToString());
                m.y = Convert.ToInt32(ds.Tables[0].Rows[i]["y"].ToString());
                m.max_x = Convert.ToInt32(ds.Tables[0].Rows[i]["max_x"].ToString());
                m.max_y = Convert.ToInt32(ds.Tables[0].Rows[i]["max_y"].ToString());

                ret.Add(m);

            }
            return ret;
        }
        public void Save()
        {

            SQLiteConnection conn = SQLiteUtil.GetConn();
            conn.Open();
            string sql = "INSERT INTO mhxy_map_region  (map_id,name,x,y,max_x,max_y) VALUES(@map_id,@name,@x,@y,@max_x,@max_y)";
            SQLiteParameter[] parameters = {
                              new SQLiteParameter("@map_id", DbType.Int32,4),
                              new SQLiteParameter("@name", DbType.String),
                              new SQLiteParameter("@x", DbType.Int32,4),
                              new SQLiteParameter("@y", DbType.Int32,4),
                              new SQLiteParameter("@max_x", DbType.Int32,4),
                              new SQLiteParameter("@max_y", DbType.Int32,4),
                        };
            parameters[0].Value = map_id;
            parameters[1].Value = name;
            parameters[2].Value = x;
            parameters[3].Value = y;
            parameters[4].Value = max_x;
            parameters[5].Value = max_y;

            SQLiteCommand cmd = SQLiteHelper.CreateCommand(conn, sql, parameters);
            cmd.ExecuteNonQuery();
            conn.Close();
        }
       
    }

}
