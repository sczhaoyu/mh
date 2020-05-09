using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace mh.bean
{
    public class Baby : Biology
    {
        public Baby(biz.Execute e) : base(e) { }
        public override void AddHPMP(IntPtr hwnd)
        {
            if (Global.mh_cfg[hwnd].bb_hp_mp_auto == 0) { return; }
            bean.AttrsPanel attr = mhxy.CallFunc.ReadAttrsPanel(hwnd.ToInt32());
            if (attr.bbHP == 0 || id <= 0)
            {
                return;
            }
            if (attr.bbHP < Global.mh_cfg[hwnd].bb_hp)
            {

                byte[] b = BitConverter.GetBytes(id);
                LoadDll.sendMsg(hwnd, String.Format("35 06 {0} 01 00", StringUtil.FormatBytesToHex(b).Trim()));
                Log.WriteLine("宝宝满血");
            }
            if (attr.bbMP < Global.mh_cfg[hwnd].bb_mp)
            {

                byte[] b = BitConverter.GetBytes(id);
                LoadDll.sendMsg(hwnd, String.Format("35 06 {0} 02 00", StringUtil.FormatBytesToHex(b).Trim()));
                Log.WriteLine("宝宝满蓝");
            }
        }

        public void updateAttr(byte[] data)
        {
            int t = data[0];
            int len = data[1];
            switch (t)
            {
                //读取宝宝ID
                case 0xD0:
                    // D0 05 1D 81 FC 20 00
                    this.id = BitConverter.ToInt32(new byte[] { data[2], data[3], data[4], data[5] }, 0);
                    break;
                //9,10宝宝当前血量
                //BB 09 00 24 15 1D 20 01 FB 00 00 00
                //第八位是气血上限03，01是剩余血量
                //BB 09 00 24 15 1D 20 03 31 01 00 00
                case 0xBB:
                    if (len != 0x09) { return; }
                    if (data[7] == 0x01)
                    {
                        //更新气血值
                        hp = BitConverter.ToInt16(new byte[] { data[8], data[9] }, 0);
                    }
                    if (data[7] == 0x03)
                    {
                        //更新气血值
                        maxHP = BitConverter.ToInt16(new byte[] { data[8], data[9] }, 0);
                    }
                    break;

                //宝宝全部属性,30是等级,67,68最大气血值,69,70宝宝魔法,28,29气血,61,62,63,64宝宝总经验
                //注意下标减去1
                //B1 6D 00 24 15 1D 20 C0 EA 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 F7 07 0D F2 00 00 00 85 00 00 00 99 00 00 00 43 00 00 00 21 00 00 00 61 2B 00 22 00 5D 00 20 00 1C 00 00 00 1D 1A 00 00 31 01 00 00 85 00 00 00 00 00 00 00 48 26 00 00 31 01 00 00 7D 13 00 00 04 58 00 00 00 41 00 00 00 00 00 00 58 00 00 00 58 00 00 00 00
                case 0xB1:
                    if (len != 0x6D) { return; }

                    this.id = BitConverter.ToInt32(new byte[] { data[3], data[4], data[5], data[6] }, 0);

                    this.lv = data[29];

                    this.hp = BitConverter.ToInt16(new byte[] { data[30], data[31] }, 0);

                    this.mp = BitConverter.ToInt16(new byte[] { data[34], data[35] }, 0);

                    this.maxMP = BitConverter.ToInt16(new byte[] { data[71], data[72] }, 0);

                    this.exp = BitConverter.ToInt32(new byte[] { data[63], data[64], data[65], data[66] }, 0);

                    this.maxHP = BitConverter.ToInt16(new byte[] { data[83], data[84] }, 0);

                    int idx = StringUtil.FindByte(data, 7, 0x00);

                    this.name = StringUtil.GetChsFromHex(StringUtil.byteToHexStr(data.Skip(7).Take(idx - 7).ToArray()));

                    break;
            }
            sayHi();
        }
    }
}
