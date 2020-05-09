using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;

namespace mh.model
{
    class MhxyRouterRec
    {
        //ID标识
        public int id = 0;
        //当前地图的ID
        public int map_id = 0;
        //步骤
        public int sort = 0;
        //出口ID
        public int exit_id;
        //0是NPC,1到达某地图
        public int is_type = 0;
        //NPC的ID或者目标地图ID
        public int target_id = 0;
        //备注 
        public string remarks = "";

        public MhxyMapExit mapExit;
        public MhxyNPC npc;
        /// <summary>
        /// 获取任务全部数据
        /// </summary>
        /// <param name="mapId"></param>
        /// <param name="targetId"></param>
        /// <returns></returns>
        public static List<MhxyRouterRec> GetRouterAll(int mapId, int targetId)
        {
            List<MhxyMapExit> exlist = MhxyMapExit.GetAll();
            List<MhxyNPC> npcList = MhxyNPC.GetAll();
            List<MhxyRouterRec> ret = new List<MhxyRouterRec>();

            string sql = "select * from mhxy_router_rec where map_id=" + mapId.ToString() + " and target_id=" + targetId.ToString() + " order by sort asc";
            SQLiteConnection conn = SQLiteUtil.GetConn();
            conn.Open();
            DataSet ds = SQLiteHelper.ExecuteDataSet(conn, sql, null);
            conn.Close();

            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                MhxyRouterRec mt = new MhxyRouterRec();
                mt.id = Convert.ToInt32(ds.Tables[0].Rows[i]["id"].ToString());
                mt.map_id = Convert.ToInt32(ds.Tables[0].Rows[i]["map_id"].ToString());
                mt.exit_id = Convert.ToInt32(ds.Tables[0].Rows[i]["exit_id"].ToString());
                mt.sort = Convert.ToInt32(ds.Tables[0].Rows[i]["sort"].ToString());
                mt.is_type = Convert.ToInt32(ds.Tables[0].Rows[i]["is_type"].ToString());
                mt.target_id = Convert.ToInt32(ds.Tables[0].Rows[i]["target_id"].ToString());
                mt.remarks = ds.Tables[0].Rows[i]["remarks"].ToString();
                //装载出口信息
                for (int k = 0; k < exlist.Count; k++)
                {
                    if (mt.exit_id == exlist[k].id)
                    {
                        mt.mapExit = exlist[k];
                    }
                   
                }
                //装载NPC
                if (mt.is_type == 0)
                {

                    for (int k = 0; k < npcList.Count; k++)
                    {
                        if (mt.target_id == npcList[k].npc_id) {
                            mt.npc = npcList[k];
                        }
                    }
                }
                ret.Add(mt);

            }

            return ret;
        }
    }
}
