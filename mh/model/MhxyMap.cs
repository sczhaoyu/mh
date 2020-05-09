using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;

namespace mh.model
{
    class MhxyMap
    {
        //地图ID
        public int id;
        public int map_no;

        //地图名称
        public string name;
        //地图上级地图
        public int parent_id;


        /// <summary>
        /// 加载全部地图数据
        /// </summary>
        /// <returns></returns>
        public static List<MhxyMap> GetAll()
        {
            List<MhxyMap> ret = new List<MhxyMap>();

            string sql = "select * from mhxy_map";
            SQLiteConnection conn = SQLiteUtil.GetConn();
            conn.Open();
            DataSet ds = SQLiteHelper.ExecuteDataSet(conn, sql, null);
            conn.Close();

            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                MhxyMap mt = new MhxyMap();
                mt.id = Convert.ToInt32(ds.Tables[0].Rows[i]["id"].ToString());
                mt.map_no = Convert.ToInt32(ds.Tables[0].Rows[i]["map_no"].ToString());
                mt.name = ds.Tables[0].Rows[i]["name"].ToString();
                mt.parent_id = Convert.ToInt32(ds.Tables[0].Rows[i]["parent_id"].ToString());
                ret.Add(mt);

            }
            return ret;
        }
 
        ///// <summary>
        ///// 获取地图列表树
        ///// </summary>
        ///// <returns></returns>
        //public static List<MhxyMap> GetMapListTree()
        //{
        //    List<MhxyMap> maps = GetAll();
        //    //加载NPC
        //    List<MhxyNPC> npcs = MhxyNPC.GetAll();
        //    for (int i = 0; i < maps.Count; i++)
        //    {
        //        for (int j = 0; j < npcs.Count; j++)
        //        {
        //            if (maps[i].map_id == npcs[j].map_id)
        //            {
        //                maps[i].npcList.Add(npcs[j]);
        //            }
        //        }
        //    }

        //    //加载出口
        //    List<MhxyMapExit> exits = MhxyMapExit.GetAll();
        //    for (int i = 0; i < maps.Count; i++)
        //    {
        //        for (int j = 0; j < exits.Count; j++)
        //        {
        //            if (maps[i].map_id == exits[j].map_id)
        //            {
        //                maps[i].mapExtList.Add(exits[j]);
        //            }
        //        }
        //    }
        //    //顶级地图
        //    List<MhxyMap> parentMaps = new List<MhxyMap>();
        //    for (int i = 0; i < maps.Count; i++)
        //    {
        //        if (maps[i].parent_id == 0)
        //        {
        //            parentMaps.Add(maps[i]);
        //        }
        //    }
        //    //装配地图
        //    for (int i = 0; i < parentMaps.Count; i++)
        //    {
        //        loadMap(parentMaps[i], maps);
        //    }

        //    return parentMaps;
        //}
        ///// <summary>
        ///// 递归装载子地图
        ///// </summary>
        ///// <param name="maps">全部数据</param>
        ///// <param name="childrens">当前子节点数据</param>
        //public static void loadMap(MhxyMap map, List<MhxyMap> maps)
        //{
        //    Log.WriteLine("{0}，{1}", map.name, map.map_id);
        //    for (int i = 0; i < maps.Count; i++)
        //    {
        //        if (maps[i].parent_id == map.map_id)
        //        {
        //            //加入组
        //            map.childrensMap.Add(maps[i]);
        //            int idx = map.childrensMap.Count - 1;

        //            loadMap(map.childrensMap[idx], maps);
        //        }
        //    }
        //}
        ///// <summary>
        ///// 获取顶级地图ID
        ///// </summary>
        ///// <param name="maps">地图列表</param>
        ///// <param name="mapId">当前地图ID</param>
        ///// <returns></returns>
        //public static int GetTopMapId(List<MhxyMap> maps, int mapId)
        //{

        //    for (int i = 0; i < maps.Count; i++)
        //    {
        //        if (mapId == maps[i].map_id)
        //        {
        //            if (maps[i].parent_id > 0)
        //            {
        //                mapId = GetTopMapId(maps, maps[i].parent_id);
        //            }

        //        }
        //    }
        //    return mapId;
        //}
        ///// <summary>
        ///// 生成寻路的路径
        ///// </summary>
        ///// <param name="maps"></param>
        ///// <param name="mapId"></param>
        ///// <param name="toMapId"></param>
        ///// <returns></returns>
        //public static List<RouteAuxiliary> FindWayAuxiliary(List<MhxyMap> maps, int mapId, int toMapId)
        //{
        //    //获取当前地图的出口

        //    //判断哪个出口可以到达入口



        //    return null;
        //}
        ///// <summary>
        ///// 获得地图入口
        ///// </summary>
        ///// <param name="maps">包含顶级地图结构的值</param>
        ///// <param name="toMapId">去的地方</param>
        ///// <returns></returns>
        //public static MhxyMapExit GetMapExit(List<MhxyMap> maps, int toMapId)
        //{
        //    MhxyMapExit  ret = new  MhxyMapExit();
        //    for (int i = 0; i < maps.Count; i++)
        //    {
        //        //遍历出口查询
        //        for (int j = 0; j < maps[i].mapExtList.Count; j++)
        //        {
        //            //判断当前这个地图的出口是否包含去的出口
        //            if (toMapId == maps[i].mapExtList[j].to_map_id)
        //            {

        //               ret=maps[i].mapExtList[j];
        //                return ret;

        //            }
        //        }
        //        //判断是否还有子地图
        //        if (maps[i].childrensMap.Count > 0)
        //        {

        //            ret = GetMapExit(maps[i].childrensMap, toMapId);

        //        }

        //    }
        //    return ret;
        //}


    }


}
