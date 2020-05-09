using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace mh.bean
{
    public class GamePlayer : Biology
    {
        public GamePlayer(biz.Execute e) : base(e) { }
        //气血检测线程
        public override void AddHPMP(IntPtr hwnd)
        {
            if (Global.mh_cfg[hwnd].hp_mp_auto == 0) { return; }

            bean.AttrsPanel attr = mhxy.CallFunc.ReadAttrsPanel(hwnd.ToInt32());

            if (attr.peopleMP < Global.mh_cfg[hwnd].mp)
            {
                Log.WriteLine("人物加满蓝");
                //蓝加满
                LoadDll.sendMsg(hwnd, "35 06 00 00 00 00 02 00");
            }
            if (attr.peopleHP < Global.mh_cfg[hwnd].hp)
            {
                //血加满
                Log.WriteLine("人物加满气血");
                LoadDll.sendMsg(hwnd, "35 06 00 00 00 00 01 00");
            }

        }
        public void updateAttr(byte[] data)
        {
            int t = data[0];
            int len = data[1];
            switch (t)
            {
                //属性最全数据
                case 0x20:
                    if (len < 145) { return; }
                    lv = data[114];
                    //名字处理
                    int idx = StringUtil.FindByte(data, 13, 0x00);
                    this.name = StringUtil.GetChsFromHex(StringUtil.byteToHexStr(data.Skip(13).Take(idx - 13).ToArray()));
                    hp = BitConverter.ToInt16(new byte[] { data[56], data[57] }, 0);
                    maxHP = BitConverter.ToInt16(new byte[] { data[58], data[59] }, 0);
                    mp = BitConverter.ToInt16(new byte[] { data[61], data[62] }, 0);
                    mp = BitConverter.ToInt16(new byte[] { data[61], data[62] }, 0);
                    maxMP = BitConverter.ToInt16(new byte[] { data[64], data[65] }, 0);

                    anger = BitConverter.ToInt16(new byte[] { data[66], data[67] }, 0);
                    exp = BitConverter.ToInt16(new byte[] { data[69], data[70], data[71], data[72] }, 0);
                    maxExp = BitConverter.ToInt16(new byte[] { data[142], data[143], data[144], data[145] }, 0);
                    break;
                //战斗中更新数据
                case 0x21:
                    if (len != 6 && len != 8) { return; }
                    int at = data[3];
                    switch (at)
                    {
                        //HP
                        //06血量 最后两位值
                        //21 03 00 06 47 01
                        case 0x06:
                            hp = BitConverter.ToInt16(new byte[] { data[4], data[5] }, 0);
                            break;
                        //最大气血
                        //21 03 00 08 50 01
                        case 0x08:
                            maxHP = BitConverter.ToInt16(new byte[] { data[4], data[5] }, 0);
                            break;
                        //MP 21 03 00 09 4F 00
                        case 0x09:
                            mp = BitConverter.ToInt16(new byte[] { data[4], data[5] }, 0);
                            break;
                        //最大魔法
                        //21 03 00 0A BA 00
                        case 0x0A:
                            maxMP = BitConverter.ToInt16(new byte[] { data[4], data[5] }, 0);
                            break;
                        //anger 21 03 00 0B 62 00
                        case 0x0B:
                            anger = BitConverter.ToInt16(new byte[] { data[4], data[5] }, 0);
                            break;
                        //exp  当前经验 后四位经验值 21 05 00 0D 28 12 00 00
                        case 0x0D:
                            exp = BitConverter.ToInt16(new byte[] { data[4], data[5], data[6], data[7] }, 0);
                            break;
                        //升级经验
                        // 21 05 00 25 E1 6F 00 00
                        case 0x25:
                            maxExp = BitConverter.ToInt16(new byte[] { data[4], data[5], data[6], data[7] }, 0);
                            break;
                    }
                    break;
            }
            sayHi();
        }
    }
}
