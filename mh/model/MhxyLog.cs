using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;

namespace mh.model
{
    class MhxyLog
    {
        public DateTime ctime;
        public int pkgType;
        public int pkgLen;
        public int msgType;
        public int msgLen;
        public byte[] body;
        /// <summary>
        /// 初始化信息
        /// </summary>
        /// <param name="pkgType">消息类型</param>
        /// <param name="data">数据</param>
        public MhxyLog(int pkgType, byte[] data)
        {
            this.ctime = DateTime.Now;
            this.pkgType = pkgType;
            this.pkgLen = data.Length;
            this.msgType = data[0];
            if (pkgType == 0)
            {
                this.msgLen = BitConverter.ToInt16(new byte[] { data[1], data[2] }, 0);
            }
            else
            {
                this.msgLen = BitConverter.ToInt16(new byte[] { data[1], data[data.Length - 1] }, 0);
            }

            this.body = data;
        }
        /// <summary>
        /// 获得一个格式化后的16进制字符串
        /// </summary>
        /// <returns></returns>
        public string GetBodyFormatHex()
        {
            return StringUtil.FormatBytesToHex(this.body);
        }
        /// <summary>
        /// 获取消息内容
        /// </summary>
        /// <returns></returns>
        public string GetBodyByte()
        {
            return StringUtil.byteToHexStr(this.body);
        }

        /// <summary>
        /// 保存数据日志
        /// </summary>
        /// <param name="tableName">保存表名称</param>
        /// <param name="m"></param>
        public void Save(string tableName)
        {
            SQLiteConnection conn = SQLiteUtil.GetConn();
            conn.Open();
            string sql = "INSERT INTO " + tableName + " (ctime,pkg_type, pkg_len, msg_type, msg_len, body) VALUES(@ctime,@pkg_type, @pkg_len, @msg_type, @msg_len, @body)";
            SQLiteParameter[] parameters = {
                              new SQLiteParameter("@ctime", DbType.DateTime2),
                              new SQLiteParameter("@pkg_type", DbType.String),
                              new SQLiteParameter("@pkg_len", DbType.Int32,4),
                              new SQLiteParameter("@msg_type", DbType.Int32,4),
                              new SQLiteParameter("@msg_len", DbType.Int32,4),
                              new SQLiteParameter("@body", DbType.String)
                        };
            parameters[0].Value = DateTime.Now;
            parameters[1].Value = this.pkgType;
            parameters[2].Value = this.pkgLen;
            parameters[3].Value = this.msgType;
            parameters[4].Value = this.msgLen;
            parameters[5].Value = this.GetBodyByte();
            SQLiteCommand cmd = SQLiteHelper.CreateCommand(conn, sql, parameters);
            cmd.ExecuteNonQuery();
            conn.Close();

        }
    }
}
