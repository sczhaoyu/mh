using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;

namespace mh.model
{
    class MhxyNPC
    {
        //npc所在地图
        public int map_id;
        //npc的ID
        public int npc_id;
        //npc名称
        public string name;
        //所在坐标x
        public int x;
        //所在坐标y
        public int y;
        /// <summary>
        /// 获取全部NPC数据
        /// </summary>
        /// <returns></returns>
        public static List<MhxyNPC> GetAll()
        {
            List<MhxyNPC> ret = new List<MhxyNPC>();

            string sql = "select * from mhxy_npc";
            SQLiteConnection conn = SQLiteUtil.GetConn();
            conn.Open();
            DataSet ds = SQLiteHelper.ExecuteDataSet(conn, sql, null);
            conn.Close();

            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                MhxyNPC mt = new MhxyNPC();
                mt.map_id = Convert.ToInt32(ds.Tables[0].Rows[i]["map_id"].ToString());
                mt.name = ds.Tables[0].Rows[i]["name"].ToString();
                mt.npc_id = Convert.ToInt32(ds.Tables[0].Rows[i]["npc_id"].ToString());

                mt.x = Convert.ToInt32(ds.Tables[0].Rows[i]["x"].ToString());
                mt.y = Convert.ToInt32(ds.Tables[0].Rows[i]["y"].ToString());
                ret.Add(mt);

            }

            return ret;
        }
        public static MhxyNPC GetNPCID(int npc_id)
        {
            MhxyNPC mt = new MhxyNPC();

            string sql = "select * from mhxy_npc where npc_id=" + npc_id.ToString();
            SQLiteConnection conn = SQLiteUtil.GetConn();
            conn.Open();
            DataSet ds = SQLiteHelper.ExecuteDataSet(conn, sql, null);
            conn.Close();

            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {

                mt.map_id = Convert.ToInt32(ds.Tables[0].Rows[i]["map_id"].ToString());
                mt.name = ds.Tables[0].Rows[i]["name"].ToString();
                mt.npc_id = Convert.ToInt32(ds.Tables[0].Rows[i]["npc_id"].ToString());

                mt.x = Convert.ToInt32(ds.Tables[0].Rows[i]["x"].ToString());
                mt.y = Convert.ToInt32(ds.Tables[0].Rows[i]["y"].ToString());
            }

            return mt;
        }
    }
}
