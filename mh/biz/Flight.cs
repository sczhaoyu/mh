using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace mh.biz
{
    public class Flight
    {
        int hwnd;
        int mapID;
        Execute ex;
        bool flag = false;
        bean.Axis toAxis;
        public Flight(int mapID, Execute ex, bean.Axis toAxis)
        {
            this.ex = ex;
            this.mapID = mapID;
            this.hwnd = ex.hwnd.ToInt32();
            this.toAxis = toAxis;
        }
        /// <summary>
        /// 飞到城市
        /// </summary>
        /// <returns></returns>
        public bool GoToCity()
        {
            List<bean.Goods> goods = mhxy.CallFunc.itBag(hwnd);
            for (int i = 0; i < goods.Count; i++)
            {
                if (goods[i].type == 673 && flag == false)
                {
                    string cmd = String.Format("32 09 {0} 00 00 00 00 00", StringUtil.FormatIntToHexStyle(goods[i].id, true).Trim());


                    int goodsID = goods[i].id;
                    registerFlight(goodsID);
                    //发送飞行棋指令
                    LoadDll.sendMsg((IntPtr)hwnd, cmd);
                    flag = true;


                }
                //System.Threading.Thread.Sleep(3000);
            }
            return false;
        }
        public void registerFlight(int goodsID)
        {
            string cmd = "";
            //注册读取飞行棋坐标信息
            ex.RegMsgCallBack(0x3B, new MsgCallBack(false, delegate (int msgType, byte[] data)
            {

                //3B 3A 00 0D
                if (data[3] == 0x0D)
                {
                    int mid = BitConverter.ToInt16(new byte[] { data[8], data[9] }, 0);
                    if (mapID == mid)
                    {
                        int point = readAxis(data, this.toAxis);
                        //发送飞行指令
                        cmd = "3B 07 0F {0} {1} 00";
                        cmd = String.Format(cmd, StringUtil.FormatIntToHexStyle(goodsID, true).Trim(), StringUtil.FormatIntToHexStyle(point, true).Trim());
                        LoadDll.sendMsg((IntPtr)hwnd, cmd);
                    }

                }

            }, true, null));
            //注册飞行棋使用信息
            ex.RegMsgCallBack(0x37, new MsgCallBack(false, delegate (int msgType, byte[] data)
            {
                //37 04 00 C6 D4 27 40
                if (data[1] == 0x04 && data[2] == 0x00)
                {
                    int gid = BitConverter.ToInt32(new byte[] { data[3], data[4], data[5], data[6] }, 0);
                    cmd = "33 05 {0} 00";
                    cmd = String.Format(cmd, StringUtil.FormatIntToHexStyle(gid, true).Trim());
                    //回应服务器已经到了
                    LoadDll.sendMsg((IntPtr)hwnd, cmd);
                }

            }, true, null));
        }
        /// <summary>
        /// 读取距离目标最近的点位
        /// </summary>
        /// <param name="data"></param>
        /// <param name="toAxis"></param>
        /// <returns></returns>
        public int readAxis(byte[] data, bean.Axis toAxis)
        {
            List<bean.Axis> list = new List<bean.Axis>();
            data = data.Skip(13).Take(data.Length - 13).ToArray();
            for (int i = 0; i < data.Length; i++)
            {
                bean.Axis a = new bean.Axis(toAxis.x, toAxis.y);
                a.x = BitConverter.ToInt16(new byte[] { data[i], data[i + 1] }, 0);
                a.y = BitConverter.ToInt16(new byte[] { data[i + 2], data[i + 3] }, 0);
                i += 3;
                list.Add(a);
            }
            //计算距离最近坐标点
            int idx = 0;
            int j = -1;
            for (int i = 0; i < list.Count; i++)
            {
                int x = System.Math.Abs(list[i].x - toAxis.x);
                int y = System.Math.Abs(list[i].y - toAxis.y);
                int tmp = (int)Math.Sqrt(x * x + y * y);
                if (j == -1)
                {
                    j = tmp;
                }
                if (tmp < j)
                {
                    idx = i;
                    j = tmp;
                }


            }
            return idx;
        }

    }
}
