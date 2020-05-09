using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace mh.mhxy
{
    /// <summary>
    /// 多级对话配置
    /// </summary>
    public class Dialog
    {
        public int npcId;
        public int lv;
        public int option;
        public string body;
        public Dialog dialog;
        //============================
        public string hexNpcId;
        public string hexLv;
        public string hexOption;

        public Dialog(int npcId, int lv, int option, string body)
        {
            this.npcId = npcId;
            this.lv = lv;
            this.option = option;
            this.body = body;
            //===========================================================
            hexNpcId = StringUtil.FormatIntToHexStyle(npcId, true).Trim();
            hexLv = StringUtil.FormatIntToHexStyle(lv, false).Trim();
            hexOption = StringUtil.FormatIntToHexStyle(option, false).Trim();
        }
        public Dialog Format(params object[] args)
        {
            this.body = String.Format(body, args);
            return this;
        }
    }

    /// <summary>
    /// NPC呼叫
    /// </summary>
    public class CallNPC
    {
        public static List<Dialog> dia = new List<Dialog>();

        /// <summary>
        /// 获取NPC的对话数据包
        /// </summary>
        /// <param name="npcId"></param>
        /// <param name="lv"></param>
        /// <param name="option"></param>
        /// <returns></returns>
        public static string GetNpcDialog(int npcId, int lv, int option)
        {
            for (int i = 0; i < dia.Count; i++)
            {
                if (dia[i].npcId == npcId && dia[i].lv == lv && dia[i].option == option)
                {
                    return dia[i].body;
                }
            }

            return "";
        }
        /// <summary>
        /// 打开一级对话
        /// </summary>
        /// <param name="npcID"></param>
        /// <returns></returns>
        public static string OpenDialog(int npcID)
        {
            return String.Format("80 09 {0} 00 00 00 00 00", StringUtil.FormatIntToHexStyle(npcID, true).Trim());
        }
        /// <summary>
        /// 初始化数据
        /// </summary>
        static CallNPC()
        {
            //程咬金的操作
            Dialog l = new Dialog(536871346, 0, 0, "80 09 {0} 00 00 00 00 00");
            dia.Add(l.Format(l.hexNpcId));
            //NPC给予
            l = new Dialog(l.npcId, 1, 3, "82 02 {0} 00");
            dia.Add(l.Format(l.hexOption));

            //桃源村长
            l = new Dialog(536872206, 0, 0, "80 09 {0} 00 00 00 00 00");
            dia.Add(l.Format(l.hexNpcId));


            //郑镖头的操作
            l = new Dialog(536871314, 0, 0, "80 09 {0} 00 00 00 00 00");
            dia.Add(l.Format(l.hexNpcId));
             //一级对话   
                                     
            l = new Dialog(l.npcId, 1, 1, "FA 0F 3E 00 00 00 05 {0} 00 00 00 {1} 01 00");
            dia.Add(l.Format(l.hexOption,l.hexNpcId));
            //二级对话确认接镖
            l = new Dialog(l.npcId, 2, 1, "FA 0F 3E 00 00 00 05 01 {0} 00 00 00 {1} 00");
            dia.Add(l.Format(l.hexOption, l.hexNpcId));
        }

    }
}
