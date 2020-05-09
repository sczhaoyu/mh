using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace mh
{
    class TableCheck
    {
        /// <summary>
        /// 检测数据库完整性
        /// </summary>
        static TableCheck()
        {
            checkDB();
        }
        public static void load() { }
        private static void checkDB()
        {
            //判断数据表mhxy_log是否存在
            if (!SQLiteUtil.ExitTable("mhxy_log"))
            {
                //创建表mhxy_log
                //id 标志列
                //ctime写入时间
                //pkg_type 消息方式
                //pkg_len 消息长度
                //msg_type 消息类别
                //msg_len 消息长度
                //body 消息内容
                createLogTable("mhxy_log");
            }
            //
            //判断数据表mhxy_type是否存在
            if (!SQLiteUtil.ExitTable("mhxy_type"))
            {
                //创建表mhxy_type
                //type_id 分类ID
                //name 名称
                SQLiteUtil.createTable("mhxy_type", "type_id INTEGER PRIMARY KEY  NOT NULL , name  TEXT NOT NULL,is_show INTEGER NOT NULL,prefix  TEXT NOT NULL,filert_rule INTEGER NOT NULL");
            }
            //判断数据表mhxy_cfg是否存在
            if (!SQLiteUtil.ExitTable("mhxy_cfg"))
            {
                 
                SQLiteUtil.createTable("mhxy_cfg", "id  INTEGER PRIMARY KEY NOT NULL, bb_id  INTEGER NOT NULL, hp  INTEGER NOT NULL, mp  INTEGER NOT NULL, hp_mp_auto  INTEGER NOT NULL, bb_hp  INTEGER NOT NULL, bb_mp  INTEGER NOT NULL, bb_hp_mp_auto  INTEGER NOT NULL, skill  INTEGER NOT NULL, skill_auto  INTEGER NOT NULL, bb_skill  INTEGER NOT NULL, bb_skill_auto  INTEGER NOT NULL");
            }

            //判断数据表mhxy_skill是否存在
            if (!SQLiteUtil.ExitTable("mhxy_skill"))
            {

               SQLiteUtil.createTable("mhxy_skill", "skill_id  INTEGER  PRIMARY KEY  NOT NULL, name  TEXT NOT NULL, skill_type  INTEGER NOT NULL, hp  INTEGER NOT NULL, mp  INTEGER NOT NULL, factions  INTEGER NOT NULL");
               
            }
            //判断数据表mhxy_map_region是否存在
            if (!SQLiteUtil.ExitTable("mhxy_map_region"))
            {

                SQLiteUtil.createTable("mhxy_map_region", "id INTEGER   PRIMARY KEY AUTOINCREMENT NOT NULL,map_id  INTEGER NOT NULL, name TEXT NOT NULL, x  INTEGER NOT NULL, y  INTEGER NOT NULL, max_x  INTEGER NOT NULL, max_y  INTEGER NOT NULL");
            }
         
            //判断数据表mhxy_map_exit是否存在
            if (!SQLiteUtil.ExitTable("mhxy_map_exit"))
            {

                SQLiteUtil.createTable("mhxy_map_exit", "id INTEGER   PRIMARY KEY AUTOINCREMENT NOT NULL,map_id  INTEGER NOT NULL,  x  INTEGER NOT NULL, y  INTEGER NOT NULL, wait_x  INTEGER NOT NULL,  wait_y  INTEGER NOT NULL,to_map_id  INTEGER NOT NULL, npc_id  INTEGER NOT NULL, call_npc_option  INTEGER NOT NULL, remarks TEXT NOT NULL");
            }
            //判断数据表mhxy_map_region是否存在
            if (!SQLiteUtil.ExitTable("mhxy_map"))
            {

                SQLiteUtil.createTable("mhxy_map", "id INTEGER   PRIMARY KEY AUTOINCREMENT NOT NULL ,map_no INTEGER  NOT NULL, name TEXT NOT NULL, parent_id  INTEGER NOT NULL");
            }
            //判断数据表mhxy_npc是否存在
            if (!SQLiteUtil.ExitTable("mhxy_npc"))
            {

                SQLiteUtil.createTable("mhxy_npc", "npc_id INTEGER   PRIMARY KEY  NOT NULL,map_id  INTEGER NOT NULL, name TEXT NOT NULL, x  INTEGER NOT NULL, y  INTEGER NOT NULL");
            }

            //判断数据表mhxy_npc是否存在
            if (!SQLiteUtil.ExitTable("mhxy_router_rec"))
            {

                SQLiteUtil.createTable("mhxy_router_rec", "id INTEGER   PRIMARY KEY  AUTOINCREMENT NOT NULL,map_id INTEGER NOT NULL, exit_id  INTEGER NOT NULL, sort INTEGER NOT NULL, is_type INTEGER NOT NULL, target_id INTEGER NOT NULL, remarks TEXT NOT NULL");
            }

            //判断数据表mhxy_npc是否存在
            if (!SQLiteUtil.ExitTable("mhxy_axis"))
            {

                SQLiteUtil.createTable("mhxy_axis", "id  INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL,flag_id  INTEGER NOT NULL, x  INTEGER NOT NULL, y  INTEGER NOT NULL, nx  INTEGER NOT NULL, ny  TEXT NOT NULL, type  INTEGER NOT NULL, remarks  TEXT NOT NULL");
            }
        }
        public static void createLogTable(string name)
        {
            SQLiteUtil.createTable(name, "id INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL ,ctime datetime  NOT NULL,pkg_type INTEGER NOT NULL, pkg_len  INTEGER NOT NULL,msg_type  INTEGER NOT NULL ,msg_len  INTEGER NOT NULL , body  TEXT NOT NULL");
        }
    }
}
