using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;

namespace mh.model
{
    /// <summary>
    /// 地图出口
    /// </summary>
    class MhxyMapExit
    {
        //ID标识
        public int id;
        //地图ID
        public int map_id;
        //去往地图的ID
        public int to_map_id;
        //出口所在坐标x
        public int x;
        //出口所在坐标y
        public int y;
        public int wait_x;
        public int wait_y;
        //NPC的ID
        public int npc_id;
        //npc的选项
        public int call_npc_option;
        public string remarks;


        public MhxyMap myMap;
        public MhxyMap toMap;
        /// <summary>
        /// 获取全部数据
        /// </summary>
        /// <returns></returns>
        public static List<MhxyMapExit> GetAll()
        {
            List<MhxyMap> maps = MhxyMap.GetAll();
            List<MhxyMapExit> ret = new List<MhxyMapExit>();

            string sql = "select * from mhxy_map_exit";
            SQLiteConnection conn = SQLiteUtil.GetConn();
            conn.Open();
            DataSet ds = SQLiteHelper.ExecuteDataSet(conn, sql, null);
            conn.Close();

            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                MhxyMapExit mt = new MhxyMapExit();
                mt.id = Convert.ToInt32(ds.Tables[0].Rows[i]["id"].ToString());
                mt.map_id = Convert.ToInt32(ds.Tables[0].Rows[i]["map_id"].ToString());
                mt.to_map_id = Convert.ToInt32(ds.Tables[0].Rows[i]["to_map_id"].ToString());
                mt.x = Convert.ToInt32(ds.Tables[0].Rows[i]["x"].ToString());
                mt.y = Convert.ToInt32(ds.Tables[0].Rows[i]["y"].ToString());
                mt.wait_x = Convert.ToInt32(ds.Tables[0].Rows[i]["wait_x"].ToString());
                mt.wait_y = Convert.ToInt32(ds.Tables[0].Rows[i]["wait_y"].ToString());
                mt.remarks = ds.Tables[0].Rows[i]["remarks"].ToString();
                mt.npc_id = Convert.ToInt32(ds.Tables[0].Rows[i]["npc_id"].ToString());
                mt.call_npc_option = Convert.ToInt32(ds.Tables[0].Rows[i]["call_npc_option"].ToString());
                for (int j = 0; j < maps.Count; j++)
                {
                    if (mt.map_id==maps[j].id)
                    {
                        mt.myMap = maps[j];
                    }
                    if (mt.to_map_id == maps[j].id)
                    {
                        mt.toMap = maps[j];
                    }
                }
                ret.Add(mt);

            }

            return ret;
        }
    }
}
