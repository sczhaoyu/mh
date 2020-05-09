using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;

namespace mh.model
{
    class MhxyConfig
    {
        //人物ID
        public int id;
        //宝宝ID
        public int bb_id;

        //人物红
        public int hp;
        //人物蓝
        public int mp;
        //人物是否自动加红蓝
        public int hp_mp_auto;

        //宝宝红
        public int bb_hp;
        //宝宝蓝
        public int bb_mp;
        //宝宝自动加蓝
        public int bb_hp_mp_auto;

        //人物技能指令
        public int skill = 0;
        //人物技能是否自动
        public int skill_auto;


        //宝宝技能指令
        public int bb_skill = 0;
        //宝宝技能是否自动
        public int bb_skill_auto;
        public static MhxyConfig GetID(int id)
        {
            MhxyConfig ret = new MhxyConfig();

            string sql = "select * from mhxy_cfg where  id=" + id.ToString();
            SQLiteConnection conn = SQLiteUtil.GetConn();
            conn.Open();
            DataSet ds = SQLiteHelper.ExecuteDataSet(conn, sql, null);
            conn.Close();

            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                ret.id = Convert.ToInt32(ds.Tables[0].Rows[i]["id"].ToString());
                ret.hp = Convert.ToInt32(ds.Tables[0].Rows[i]["hp"].ToString());
                ret.mp = Convert.ToInt32(ds.Tables[0].Rows[i]["mp"].ToString());
                ret.hp_mp_auto = Convert.ToInt32(ds.Tables[0].Rows[i]["hp_mp_auto"].ToString());
                ret.bb_hp = Convert.ToInt32(ds.Tables[0].Rows[i]["bb_hp"].ToString());
                ret.bb_mp = Convert.ToInt32(ds.Tables[0].Rows[i]["bb_mp"].ToString());
                ret.bb_hp_mp_auto = Convert.ToInt32(ds.Tables[0].Rows[i]["bb_hp_mp_auto"].ToString());
                ret.skill = Convert.ToInt32(ds.Tables[0].Rows[i]["skill"].ToString());
                ret.bb_skill = Convert.ToInt32(ds.Tables[0].Rows[i]["bb_skill"].ToString());
                ret.skill_auto = Convert.ToInt32(ds.Tables[0].Rows[i]["skill_auto"].ToString());
                ret.bb_skill_auto = Convert.ToInt32(ds.Tables[0].Rows[i]["bb_skill_auto"].ToString());



            }



            return ret;
        }
        public static void Save(MhxyConfig mc)
        {
            SQLiteConnection conn = SQLiteUtil.GetConn();
            conn.Open();
            string sql = "INSERT INTO  mhxy_cfg (id,hp,mp,hp_mp_auto,bb_hp,bb_mp,bb_hp_mp_auto,skill,skill_auto,bb_skill,bb_skill_auto) VALUES(@id,@hp,@mp,@hp_mp_auto,@bb_hp,@bb_mp,@bb_hp_mp_auto,@skill,@skill_auto,@bb_skill,@bb_skill_auto)";
            SQLiteParameter[] parameters = {

                              new SQLiteParameter("@id", DbType.Int32,4),
                              new SQLiteParameter("@hp", DbType.Int32,4),
                              new SQLiteParameter("@mp", DbType.Int32,4),
                              new SQLiteParameter("@hp_mp_auto", DbType.Int32,4),
                              new SQLiteParameter("@bb_hp", DbType.Int32,4),
                              new SQLiteParameter("@bb_mp", DbType.Int32,4),
                              new SQLiteParameter("@bb_hp_mp_auto", DbType.Int32,4),
                              new SQLiteParameter("@skill", DbType.Int32,4),
                              new SQLiteParameter("@skill_auto", DbType.Int32,4),
                              new SQLiteParameter("@bb_skill", DbType.Int32,4),
                              new SQLiteParameter("@bb_skill_auto", DbType.Int32,4),


                        };
            parameters[0].Value = mc.id;
            parameters[1].Value = mc.hp;
            parameters[2].Value = mc.mp;
            parameters[3].Value = mc.hp_mp_auto;
            parameters[4].Value = mc.bb_hp;
            parameters[5].Value = mc.bb_mp;
            parameters[6].Value = mc.bb_hp_mp_auto;
            parameters[7].Value = mc.skill;
            parameters[8].Value = mc.skill_auto;
            parameters[9].Value = mc.bb_skill;
            parameters[10].Value = mc.bb_skill_auto;

            SQLiteCommand cmd = SQLiteHelper.CreateCommand(conn, sql, parameters);
            cmd.ExecuteNonQuery();
            conn.Close();
        }
        public static void Delete(int id)
        {
            SQLiteConnection conn = SQLiteUtil.GetConn();
            conn.Open();
            string sql = "delete from mhxy_cfg  where id=@id";
            SQLiteParameter[] parameters = {
                              new SQLiteParameter("@id", DbType.Int32,4),
                        };
            parameters[0].Value = id;


            SQLiteCommand cmd = SQLiteHelper.CreateCommand(conn, sql, parameters);
            cmd.ExecuteNonQuery();
            conn.Close();
        }
    }
}
