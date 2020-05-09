using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;

namespace mh.model
{
    public class MhxyAxis
    {
        public int id;
        //类型ID 冗余
        public int flag_id;
        //原坐标
        public int x;
        public int y;
        //新坐标
        public int nx;
        public int ny;
        //0-NPCID,1-出口ID
        public int type;
        //备注
        public string remarks;

        public static List<MhxyAxis> GetAll()
        {
            List<MhxyAxis> ret = new List<MhxyAxis>();
            string sql = "select * from mhxy_axis";
            SQLiteConnection conn = SQLiteUtil.GetConn();
            conn.Open();
            DataSet ds = SQLiteHelper.ExecuteDataSet(conn, sql, null);
            conn.Close();

            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                MhxyAxis ms = new MhxyAxis();
                ms.id = Convert.ToInt32(ds.Tables[0].Rows[i]["id"].ToString());
                ms.flag_id = Convert.ToInt32(ds.Tables[0].Rows[i]["flag_id"].ToString());
                ms.x = Convert.ToInt32(ds.Tables[0].Rows[i]["x"].ToString());
                ms.y = Convert.ToInt32(ds.Tables[0].Rows[i]["y"].ToString());
                ms.nx = Convert.ToInt32(ds.Tables[0].Rows[i]["nx"].ToString());
                ms.ny = Convert.ToInt32(ds.Tables[0].Rows[i]["ny"].ToString());
                ms.remarks = ds.Tables[0].Rows[i]["remarks"].ToString();
                ret.Add(ms);

            }

            return ret;
        }
        public static List<MhxyAxis> GetIDList(int flag_id, int type)
        {
            List<MhxyAxis> ret = new List<MhxyAxis>();
            string sql = "select * from mhxy_axis where flag_id=" + flag_id.ToString() + " and type=" + type.ToString();
            SQLiteConnection conn = SQLiteUtil.GetConn();
            conn.Open();
            DataSet ds = SQLiteHelper.ExecuteDataSet(conn, sql, null);
            conn.Close();

            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                MhxyAxis ms = new MhxyAxis();
                ms.id = Convert.ToInt32(ds.Tables[0].Rows[i]["id"].ToString());
                ms.flag_id = Convert.ToInt32(ds.Tables[0].Rows[i]["flag_id"].ToString());
                ms.x = Convert.ToInt32(ds.Tables[0].Rows[i]["x"].ToString());
                ms.y = Convert.ToInt32(ds.Tables[0].Rows[i]["y"].ToString());
                ms.nx = Convert.ToInt32(ds.Tables[0].Rows[i]["nx"].ToString());
                ms.ny = Convert.ToInt32(ds.Tables[0].Rows[i]["ny"].ToString());
                ms.remarks = ds.Tables[0].Rows[i]["remarks"].ToString();
                ret.Add(ms);

            }

            return ret;
        }
    }
}
