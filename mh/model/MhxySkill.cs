using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;

namespace mh.model
{
    class MhxySkill
    {
        //技能代码
        public int SkillId;
        //技能名称
        public string Name;
        //技能类型 0人物 ，1宝宝
        public int SkillType;
        //技能所属门派
        public int Factions;
        public int hp;
        public int mp;
        /// <summary>
        /// 获取全部数据
        /// </summary>
        /// <returns></returns>
        public static List<MhxySkill> GetAll()
        {
            List<MhxySkill> ret = new List<MhxySkill>();
            string sql = "select * from mhxy_skill";
            SQLiteConnection conn = SQLiteUtil.GetConn();
            conn.Open();
            DataSet ds = SQLiteHelper.ExecuteDataSet(conn, sql, null);
            conn.Close();

            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                MhxySkill ms = new MhxySkill();
                ms.SkillId = Convert.ToInt32(ds.Tables[0].Rows[i]["skill_id"].ToString());
                ms.Name = ds.Tables[0].Rows[i]["name"].ToString();
                ms.SkillType = Convert.ToInt32(ds.Tables[0].Rows[i]["skill_type"].ToString());
                ms.Factions = Convert.ToInt32(ds.Tables[0].Rows[i]["factions"].ToString());
                ms.hp = Convert.ToInt32(ds.Tables[0].Rows[i]["hp"].ToString());
                ms.mp = Convert.ToInt32(ds.Tables[0].Rows[i]["mp"].ToString());
                ret.Add(ms);

            }

            return ret;
        }
    }
}
