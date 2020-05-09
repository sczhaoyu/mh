using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace mh.biz
{
    /// <summary>
    /// 镖局任务
    /// </summary>
    public class BiaoJuTask
    {
        //交付镖的NPC
        public int NPC_ID = 0;
        //当前押镖次数
        public int count = 0;
        //当前窗口句柄
        public int hwnd;
        //运行状态
        public bool runStatus = false;
        private Thread th;
        //是否接镖
        public bool recvBiao = false;
        List<model.MhxyRouterRec> list;
        //当前移动任务
        public int idx = -1;

        //NPC对话界面打开
        public bool CallNPCOpen = false;
        //NPC 郑镖头对话
        public int BiaotouDialogLv = 0;
        //是否在押镖中
        public bool is_play = false;

        public bool recvNPCGoods = false;
        //默认长风镖局
        public int initMapID = 3;


        AutoMove am;
        Execute ex;

        //==========================================
        //镖的等级
        public int goodsLv = 3;
        //镖货的ID
        public int biaoID = 0;
        //押镖的现金方式1现金，2储备金
        public int moneyType = 1;
        private Mutex mxCheck = new Mutex();
        //============================================
        public BiaoJuTask(Execute e)
        {
            this.ex = e;
            this.hwnd = e.hwnd.ToInt32();
            registerFontCheck();
            registerReadTask();
        }
        /// <summary>
        /// 注册验证
        /// </summary>
        public void registerFontCheck()
        {

            //文字验证收到数据包
            ex.RegMsgCallBack(0x42, new MsgCallBack(true, delegate (int msgType, byte[] data)
            {
                string cmd = checkFont(data, msgType);
                if (cmd.Trim() != "")
                {
                    mxCheck.WaitOne();
                    Log.WriteLine("收到验证锁住对话");
                    new bean.DelaySend(hwnd, true, 4, 7, cmd, null, null).Run();
                }

            }, false, null));


            //文字验证服务器回复答案
            ex.RegMsgCallBack(0xFF, new MsgCallBack(true, delegate (int msgType, byte[] data)
            {
                if (data[1] == 0x08)
                {

                    //显示界面
                    mhxy.CallFunc.UIShowStatus(hwnd, true);
                    //隐藏验证框
                    mhxy.CallFunc.HideCheckWin(hwnd);
                    mxCheck.ReleaseMutex();
                    Log.WriteLine("验证回答正确解锁");
                }

            }, false, null));

            //服务器下发表情验证
            ex.RegMsgCallBack(0xFF, new MsgCallBack(true, delegate (int msgType, byte[] data)
            {
                //FF 0A 00 BC EC B2 E2 30 36 38 38 73 73
                if (data[1] == 0x0A && data[3] == 0xBC && data[4] == 0xEC && data[5] == 0xB2 && data[6] == 0xE2)
                {

                    string ebqCmd = Expression();

                    new bean.DelaySend(hwnd, true, 2, 4, ebqCmd, delegate ()
                    {
                        mxCheck.WaitOne();
                        Log.WriteLine("收到表情验证锁住对话");
                    },
                    delegate ()
                    {

                        mxCheck.ReleaseMutex();
                        Log.WriteLine("回答表情验证解锁");
                        //显示界面
                        mhxy.CallFunc.UIShowStatus(hwnd, true);
                        //隐藏验证框
                        mhxy.CallFunc.HideCheckWin(hwnd);

                    }).Run();
                }

            }, true, null));

        }
        /// <summary>
        /// 寻找镖银的ID
        /// </summary>
        /// <returns></returns>
        public int GetBiaoYinID()
        {
            int biaoID = mhxy.CallFunc.GetBagTypeID(hwnd, 3001);
            //镖货的种类ID
            if (biaoID == 0)
            {
                biaoID = mhxy.CallFunc.GetBagTypeID(hwnd, 3002);
            }
            if (biaoID == 0)
            {
                biaoID = mhxy.CallFunc.GetBagTypeID(hwnd, 3003);
            }
            if (biaoID == 0)
            {
                biaoID = mhxy.CallFunc.GetBagTypeID(hwnd, 3004);
            }
            return biaoID;
        }
        /// <summary>
        /// 读取任务
        /// </summary>
        public void registerReadTask()
        {
            //读取押镖任务
            ex.RegMsgCallBack(0xCE, new MsgCallBack(true, delegate (int msgType, byte[] data)
            {
                if (data[3] == 0x01 && data[4] == 0x08 && data[6] == 0xC9)
                {
                    int idx = StringUtil.FindByteLastOf(data, 0x0);
                    byte[] ret = data.Skip(idx + 1).Take(data.Length - idx).ToArray();
                    string name = StringUtil.GetChsFromHex(StringUtil.byteToHexStr(ret));
                    if (Global.biaoju_names.ContainsKey(name))
                    {
                        Log.WriteLine("押镖到NPC【{0}】", name);
                        //已经接到镖，读取送往的NPC
                        if (is_play == false)
                        {
                            int id = Global.biaoju_names[name];
                            //启动任务寻路
                            Run(3, true, id);
                            //设置为运行状态
                            is_play = true;
                        }

                    }
                }

            }, true, null));

            //读任务列表
            ex.RegMsgCallBack(0x81, new MsgCallBack(true, delegate (int msgType, byte[] data)
            {
                //判断是否已经接到任务
                //81 DE 00 50 20 4E 0A D5 E2 EF DA 
                if (data[3] == 0x50 && data[4] == 0x20 && data[5] == 0x4E && data[6] == 0x0A)
                {
                    string ret = StringUtil.GetByteHexData(data, 7);
                    int idx = ret.LastIndexOf("当天还可押送镖银");
                    //已经接到镖
                    if (idx > 0)
                    {
                        new bean.DelaySend(hwnd, true, 2, 4, "23 01 00", null, null).Run();
                    }
                }

            }, true, null));

            //调用交货确认
            ex.RegMsgCallBack(0xFA, new MsgCallBack(true, delegate (int msgType, byte[] data)
            {
                //判断交货确认
                //FA 0B 00 16 00 00 00 D6 00 00 00 00 00 00
                if (data[1] == 0x0B && data[3] == 0x16 && data[7] == 0xD6)
                {
                    sendDelivery(2);
                }
            }, true, null));


        }
        //启动押镖
        public void Run(int mapId, bool skill_random, int npcId)
        {
            ex.battle.skill_random = skill_random;
            NPC_ID = npcId;
            initMapID = mapId;
            Log.WriteLine("押镖寻路开始启动");
            task();

        }
        /// <summary>
        /// 停止押镖
        /// </summary>
        public void Stop()
        {
            runStatus = false;
        }
        /// <summary>
        /// 镖局寻路任务
        /// </summary>
        public void task()
        {
            new biz.Distance(ex, initMapID, NPC_ID, delegate ()
            {
                sendDelivery(1);
            }).Run();
        }


        /// <summary>
        /// 发货
        /// </summary>
        public void sendDelivery(int t)
        {
            if (t == 1)
            {
                biaoID = mhxy.CallFunc.GetBagTypeID(hwnd, 3001);
                //镖货的种类ID
                if (biaoID == 0)
                {
                    biaoID = mhxy.CallFunc.GetBagTypeID(hwnd, 3002);
                }
                if (biaoID == 0)
                {
                    biaoID = mhxy.CallFunc.GetBagTypeID(hwnd, 3003);
                }
                if (biaoID == 0)
                {
                    biaoID = mhxy.CallFunc.GetBagTypeID(hwnd, 3004);
                }
                //封包发货

                if (biaoID > 0)
                {
                    string cmd = String.Format("33 05 {0} 00", StringUtil.FormatIntToHexStyle(biaoID, true).Trim());
                    //先发包告诉是手动给予
                    LoadDll.sendMsg((IntPtr)hwnd, cmd);
                    System.Threading.Thread.Sleep(1500);

                    //物品ID1075273487->0F 5F 17 40
                    //FA 0C 16 00 00 00 D4 01 00 0F 5F 17 40 00
                    cmd = String.Format("FA 0C 16 00 00 00 D4 01 00 {0} 00", StringUtil.FormatIntToHexStyle(biaoID, true).Trim());
                    LoadDll.sendMsg((IntPtr)hwnd, cmd);
                    Log.WriteLine("发送货物给NPC:{0}", cmd);


                }
            }
            if (t == 2 && biaoID > 0)
            {
                string cmd = String.Format("89 18 {0} {1} 01 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00", StringUtil.FormatIntToHexStyle(NPC_ID, true).Trim(), StringUtil.FormatIntToHexStyle(biaoID, true).Trim());
                LoadDll.sendMsg((IntPtr)hwnd, cmd);
                Log.WriteLine("收到回应,发出确认：{0}", cmd);
                //交货结束
                is_play = false;
                System.Threading.Thread.Sleep(3000);
                toChangAn();
                Log.WriteLine("回到长安找郑镖头！");
            }

        }
        /// <summary>
        /// 回长安镖局
        /// </summary>
        public void toChangAn()
        {
            CallNPCOpen = false;
            //飞回长安
            new biz.Flight(1001, ex, new bean.Axis(468, 162)).GoToCity();
            System.Threading.Thread.Sleep(2000);
            //去郑镖头家里 接镖
            new biz.Distance(ex, 1001, 536871314, delegate ()
            {
                mhxy.CallFunc.CloseDia(hwnd);
                recvDelivery();
            }).Run();

        }

        /// <summary>
        /// 接货
        /// </summary>
        public void recvDelivery()
        {
            //注册,一级对话回复被打开  //FA 47 01 3E 00 00 00 05
            ex.RegMsgCallBack(0xFA, new MsgCallBack(false, delegate (int msgType, byte[] data)
            {
                CallNPCOpen = true;

                System.Threading.Thread.Sleep(1000);
                regBiao();
                bean.Axis ax = mhxy.CallFunc.ReadDialogOption(hwnd, goodsLv);
                mhxy.CallFunc.taskClick(hwnd, ax.x, ax.y);

            }, true, new Dictionary<int, int>() {
                { 3,0x3E},
                { 4,0x00},
                { 5,0x00},
                { 6,0x00},
                { 7,0x05}
            }));

            //打开郑镖头对话框
            while (!CallNPCOpen)
            {
                mxCheck.WaitOne();

                string cmd = mhxy.CallNPC.GetNpcDialog(536871314, 0, 0);
                LoadDll.sendMsg((IntPtr)hwnd, cmd);
                mxCheck.ReleaseMutex();
                Log.WriteLine("NPC郑镖头放锁");
                System.Threading.Thread.Sleep(4000);

            }
            //判断身上有没有货物，如果没有继续回调
            System.Threading.Thread.Sleep(20000);
            if (GetBiaoYinID() == 0)
            {
                recvDelivery();
            }
        }
        public void regBiao()
        {
            //注册,二级对话回复被打开
            //FA 47 01 3E 00 00 00 05
            ex.RegMsgCallBack(0xFA, new MsgCallBack(false, delegate (int msgType, byte[] data)
            {
                System.Threading.Thread.Sleep(1500);
                bean.Axis ax = mhxy.CallFunc.ReadDialogOption(hwnd, moneyType);
                mhxy.CallFunc.taskClick(hwnd, ax.x, ax.y);

            }, true, new Dictionary<int, int>() {
                { 3,0x3E},
                { 4,0x00},
                { 5,0x00},
                { 6,0x00},
                { 7,0x05}
            }));
        }
        //文字验证检查
        public static string checkFont(byte[] data, int t)
        {
            if (data.Length < 0x0D)
            {
                return "";
            }
            //获取到验证的数据类型
            int FontType = BitConverter.ToInt32(new byte[] { data[3], data[4], data[5], data[6] }, 0);

            string cmd = "";
            byte[] body;
            switch (FontType)
            {
                //一字成语验证
                case 525648:
                    body = data.Skip(7).Take(2).ToArray();
                    cmd = String.Format("42 07 50 06 02 00 {0} 00", StringUtil.FormatBytesToHex(body).Trim());
                    break;
                //四字成语排列
                case 524880:
                    //乱序8个字
                    body = data.Skip(7).Take(8).ToArray();
                    int[] sort = new int[4];
                    string font = StringUtil.GetChsFromHex(StringUtil.byteToHexStr(data.Skip(17).Take(data.Length - 17).ToArray()));
                    string sortFont = StringUtil.GetChsFromHex(StringUtil.byteToHexStr(body));
                    //查找顺序
                    for (int i = 0; i < sortFont.Length; i++)
                    {
                        for (int j = 0; j < font.Length; j++)
                        {
                            if (sortFont[i] == font[j])
                            {
                                //判断是否是起始位
                                if (j + 4 < font.Length - 1)
                                {
                                    string tmp = font.Substring(j, 4);
                                    if (checkFullFont(sortFont, tmp))
                                    {
                                        sort[0] = j;
                                        sort[1] = j + 1;
                                        sort[2] = j + 2;
                                        sort[3] = j + 3;
                                    }
                                }

                            }
                        }
                    }
                    Array.Sort(sort);
                    sortFont = font.Substring(sort[0], 1) + font.Substring(sort[1], 1) + font.Substring(sort[2], 1) + font.Substring(sort[3], 1);
                    sortFont = StringUtil.GetHexFromChs(sortFont);
                    sortFont = StringUtil.FormatHexStyle(sortFont);
                    cmd = String.Format("42 0D 50 03 08 00 {0} 00", sortFont.Trim());
                    break;

            }

            return cmd;

        }
        /// <summary>
        /// 检测是否包含全部文字
        /// </summary>
        /// <param name="sortFont"></param>
        /// <param name="ret"></param>
        /// <returns></returns>
        public static bool checkFullFont(string sortFont, string ret)
        {

            for (int i = 0; i < sortFont.Length; i++)
            {
                bool flag = false;
                for (int j = 0; j < ret.Length; j++)
                {
                    if (sortFont[i] == ret[j])
                    {
                        flag = true;
                    }
                }
                if (flag == false)
                {
                    return false;
                }
            }

            return true;


        }
        public void handle(byte[] data, int t)
        {

            int type = data[0];
            switch (type)
            {
                //主界面开启
                //case 0x81:
                //    CallNPCOpen = true;

                //   break;
                case 0xFA:
                    ////判断对话放行
                    //if (data[2] == 0x01 && data[3] == 0x3E && data[7] == 0x05)
                    //{
                    //    CallNPCOpen = true;
                    //}

                    break;
            }
        }

        public string Expression()
        {

            //生成一个随机数
            Random r = new Random();
            int sw = r.Next(1, 3);
            int r1 = r.Next(40, 220);
            int r2 = r.Next(40, 220);
            string cmdcheck = "";
            if (sw == 1)
            {
                cmdcheck = "42 04 50 05 {0} 00";
                cmdcheck = String.Format(cmdcheck, StringUtil.FormatIntToHexStyle(r1, false));
                // new bean.DelaySend(hwnd, true, 3, 6, cmdcheck).Run();
            }
            else
            {
                cmdcheck = "42 05 50 05 {0} {1} 00";
                cmdcheck = String.Format(cmdcheck, StringUtil.FormatIntToHexStyle(r1, false), StringUtil.FormatIntToHexStyle(r2, false));
            }
            return cmdcheck;
        }
        /// <summary>
        /// 获取汉字
        /// </summary>
        /// <param name="data"></param>
        /// <param name="offset"></param>
        /// <returns></returns>


    }
}
