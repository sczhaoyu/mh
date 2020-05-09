using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace mh.mhxy
{
    /// <summary>
    /// 游戏函数CALL
    /// </summary>
    class CallFunc
    {
        /// <summary>
        /// 读取人物面板属性
        /// </summary>
        /// <param name="hwnd"></param>
        public static bean.AttrsPanel ReadAttrsPanel(int hwnd)
        {
            IntPtr wnd = (IntPtr)hwnd;
            bean.AttrsPanel attr = new bean.AttrsPanel();
            int offset = 0;

            for (int i = 0; i < 100; i++)
            {
                int addr = LoadDll.ReadHwndMemoryOffsetValue(wnd, Global.addr.win, 0x68, 0x8, 0x68, offset, 0x00);
                int x = LoadDll.ReadHwndMemoryOffsetValue(wnd, Global.addr.win, 0x68, 0x8, 0x68, offset, 0x8);
                int y = LoadDll.ReadHwndMemoryOffsetValue(wnd, Global.addr.win, 0x68, 0x8, 0x68, offset, 0xc);
                //人物气血
                if (x == 582 && y == 5 && attr.peopleHP == 0)
                {
                    attr.peopleHP = LoadDll.ReadHwndMemoryOffsetValue(wnd, Global.addr.win, 0x68, 0x8, 0x68, offset, 0xF4) / 10;

                }
                //人物蓝
                if (x == 582 && y == 17 && attr.peopleMP == 0)
                {
                    attr.peopleMP = LoadDll.ReadHwndMemoryOffsetValue(wnd, Global.addr.win, 0x68, 0x8, 0x68, offset, 0xF4) / 10;

                }
                //人物怒气
                if (x == 582 && y == 29 && attr.anger == 0)
                {
                    attr.anger = LoadDll.ReadHwndMemoryOffsetValue(wnd, Global.addr.win, 0x68, 0x8, 0x68, offset, 0xF4) / 10;

                }
                if (x == 582 && y == 41 && attr.peopleExp == 0)
                {
                    attr.peopleExp = LoadDll.ReadHwndMemoryOffsetValue(wnd, Global.addr.win, 0x68, 0x8, 0x68, offset, 0xF4) / 10;

                }
                //宠物血
                if (x == 462 && y == 5 && attr.bbHP == 0)
                {
                    attr.bbHP = LoadDll.ReadHwndMemoryOffsetValue(wnd, Global.addr.win, 0x68, 0x8, 0x68, offset, 0xF4) / 10;

                }
                //宠物蓝
                if (x == 462 && y == 17 && attr.bbMP == 0)
                {
                    attr.bbMP = LoadDll.ReadHwndMemoryOffsetValue(wnd, Global.addr.win, 0x68, 0x8, 0x68, offset, 0xF4) / 10;

                }
                //宝宝经验
                if (x == 462 && y == 29 && attr.bbExp == 0)
                {
                    attr.bbExp = LoadDll.ReadHwndMemoryOffsetValue(wnd, Global.addr.win, 0x68, 0x8, 0x68, offset, 0xF4) / 10;

                }
                offset += 4;
            }

            return attr;
        }
        /// <summary>
        /// 读取对话选项
        /// </summary>
        /// <param name="hwnd">句柄</param>
        /// <param name="option">第几个选项</param>
        /// <returns></returns>
        public static bean.Axis ReadDialogOption(int hwnd, int option)
        {
            bean.Axis a = null;
            int idx = 0;
            Random r = new Random();
            for (int i = 0; i < 20; i++)
            {
                //获取对话框的x,y起点
                int x = LoadDll.ReadHwndMemoryOffsetValue((IntPtr)hwnd, Global.addr.win, 0x68, 0x8, 0x68, i * 4, 0x08);
                int y = LoadDll.ReadHwndMemoryOffsetValue((IntPtr)hwnd, Global.addr.win, 0x68, 0x8, 0x68, i * 4, 0x0C);
                int type = LoadDll.ReadHwndMemoryOffsetValue((IntPtr)hwnd, Global.addr.win, 0x68, 0x8, 0x68, i * 4, 0x18);
                int show = LoadDll.ReadHwndMemoryOffsetValue((IntPtr)hwnd, Global.addr.win, 0x68, 0x8, 0x68, i * 4, 0x28);
                if (type != 550 || show == 1)
                {
                    continue;
                }
                for (int n = 0; n < 10; n++)
                {

                    int tz = LoadDll.ReadHwndMemoryOffsetValue((IntPtr)hwnd, Global.addr.win, 0x68, 0x8, 0x68, i * 4, 0x68, 56 + n * 4, 0x00);

                    int nx = LoadDll.ReadHwndMemoryOffsetValue((IntPtr)hwnd, Global.addr.win, 0x68, 0x8, 0x68, i * 4, 0x68, 56 + n * 4, 0x08);
                    if (nx == 30)
                    {
                        idx++;
                        if (idx == option)
                        {

                            int ny = LoadDll.ReadHwndMemoryOffsetValue((IntPtr)hwnd, Global.addr.win, 0x68, 0x8, 0x68, i * 4, 0x68, 56 + n * 4, 0x0C);
                            x = nx + x + r.Next(10, 20);
                            y = ny + y;
                            a = new bean.Axis(x, y);
                        }

                    }
                }


            }
            return a;

        }
        public static void CallDialog(int hwnd, int option, int type)
        {

            //获取对话框的x,y起点
            int x = LoadDll.ReadHwndMemoryOffsetValue((IntPtr)hwnd, Global.addr.win, 0x68, 0x8, 0x68, 0, 0x8);
            int y = LoadDll.ReadHwndMemoryOffsetValue((IntPtr)hwnd, Global.addr.win, 0x68, 0x8, 0x68, 0, 0xC);

            //点击点位的x,y
            double nx = x + 86;
            double ny = y + 48;

            clickWin(hwnd, (int)nx, (int)ny);


        }

        /// <summary>
        /// 隐藏商铺
        /// </summary>
        /// <param name="hwnd"></param>
        public static void HideShop(int hwnd)
        {

            LoadDll.WriteMemoryHwndValue((IntPtr)hwnd, Global.addr.shop, BitConverter.GetBytes(46));

        }
        public static void HidePlayer(int hwnd)
        {
            LoadDll.WriteMemoryHwndValue((IntPtr)hwnd, Global.addr.shop, BitConverter.GetBytes(122));
        }
        //获取物品类型的ID
        public static int GetBagTypeID(int hwnd, int type)
        {
            List<bean.Goods> gs = itBag(hwnd);
            for (int i = 0; i < gs.Count; i++)
            {
                if (gs[i].type == type)
                {
                    return gs[i].id;
                }
            }
            return 0;
        }
        public static List<bean.Goods> itBag(int hwnd)
        {
            List<bean.Goods> goods = new List<bean.Goods>();
            int addr = readBag(hwnd)[0];
            for (int i = 0; i < 20; i++)
            {
                int addrFlag = LoadDll.ReadHwndMemoryValue((IntPtr)hwnd, addr + (i + 1) * 12);
                addrFlag = LoadDll.ReadHwndMemoryValue((IntPtr)hwnd, addrFlag + 0x40);
                addrFlag = LoadDll.ReadHwndMemoryValue((IntPtr)hwnd, addrFlag + 0x8);
                addrFlag = LoadDll.ReadHwndMemoryValue((IntPtr)hwnd, addrFlag + 0x8);
                addrFlag = LoadDll.ReadHwndMemoryValue((IntPtr)hwnd, addrFlag + 0x14);
                addrFlag = ReadOffset(hwnd, addrFlag);
                addrFlag = LoadDll.ReadHwndMemoryValue((IntPtr)hwnd, addrFlag + 0xC);

                int addrType = LoadDll.ReadHwndMemoryValue((IntPtr)hwnd, addrFlag + 0x24);
                int addrNum = LoadDll.ReadHwndMemoryValue((IntPtr)hwnd, addrFlag + 0x30);
                int addrID = LoadDll.ReadHwndMemoryValue((IntPtr)hwnd, addrFlag + 0x3C);


                int id = LoadDll.ReadHwndMemoryValue((IntPtr)hwnd, addrID + 0x8);
                int type = LoadDll.ReadHwndMemoryValue((IntPtr)hwnd, addrType + 0x8);
                int count = LoadDll.ReadHwndMemoryValue((IntPtr)hwnd, addrNum + 0x8);

                if (id > 0)
                {
                    goods.Add(new bean.Goods(id, type, count, i));
                }
            }
            return goods;
        }
        public static int ReadOffset(int hwnd, int addr)
        {
            int offset = 1364;
            for (int i = 0; i < 2; i++)
            {
                if (i == 1)
                {
                    offset = 92;
                }
                int addr1 = LoadDll.ReadHwndMemoryValue((IntPtr)hwnd, addr + offset);
                int addr2 = LoadDll.ReadHwndMemoryValue((IntPtr)hwnd, addr1 + 12);
                addr2 = LoadDll.ReadHwndMemoryValue((IntPtr)hwnd, addr2 + 60);
                addr2 = LoadDll.ReadHwndMemoryValue((IntPtr)hwnd, addr2 + 8);
                if (addr2 > 1000000000 && addr2 < 1200000000)
                {
                    return addr1;
                }
            }
            return 0;
        }
        public static int[] readBag(int hwnd)
        {
            int[] xy = new int[2];
            //临时地址
            int tmpPtr = LoadDll.ReadHwndMemoryValue((IntPtr)hwnd, Global.addr.win);
            tmpPtr = LoadDll.ReadHwndMemoryValue((IntPtr)hwnd, tmpPtr + 0x68);
            tmpPtr = LoadDll.ReadHwndMemoryValue((IntPtr)hwnd, tmpPtr + 0x8);
            tmpPtr = LoadDll.ReadHwndMemoryValue((IntPtr)hwnd, tmpPtr + 0x68);

            for (int i = 0; i < 20; i++)
            {
                int addr = LoadDll.ReadHwndMemoryValue((IntPtr)hwnd, tmpPtr + (i * 4));
                int x = LoadDll.ReadHwndMemoryValue((IntPtr)hwnd, addr + 0xF4);
                int y = LoadDll.ReadHwndMemoryValue((IntPtr)hwnd, addr + 0xF8);
                int z = LoadDll.ReadHwndMemoryValue((IntPtr)hwnd, tmpPtr + 40);
                if (x == 544 && y == 333)
                {
                    int addr2 = LoadDll.ReadHwndMemoryValue((IntPtr)hwnd, addr + 0x68);
                    for (int k = 0; k < 20; k++)
                    {

                        int addr3 = LoadDll.ReadHwndMemoryValue((IntPtr)hwnd, (addr2 + (k * 4)));
                        x = LoadDll.ReadHwndMemoryValue((IntPtr)hwnd, addr3 + 0x08);
                        y = LoadDll.ReadHwndMemoryValue((IntPtr)hwnd, addr3 + 0x0C);
                        if (x == 261 && y == 54)
                        {
                            //===================================
                            for (int j = 0; j < 2; j++)
                            {
                                if (j == 1)
                                {
                                    k = k + 1;
                                }
                                int addr4 = LoadDll.ReadHwndMemoryValue((IntPtr)hwnd, addr2 + k * 4);
                                addr4 = LoadDll.ReadHwndMemoryValue((IntPtr)hwnd, addr4 + 0x68);
                                if (j == 1)
                                {
                                    addr4 = addr4 + 8;
                                }
                                addr4 = LoadDll.ReadHwndMemoryValue((IntPtr)hwnd, addr4);
                                addr4 = LoadDll.ReadHwndMemoryValue((IntPtr)hwnd, addr4 + 0x68);
                                addr4 = LoadDll.ReadHwndMemoryValue((IntPtr)hwnd, addr4);
                                addr4 = LoadDll.ReadHwndMemoryValue((IntPtr)hwnd, addr4 + 0x68);
                                for (int p = 0; p < 100; p++)
                                {
                                    int addr5 = LoadDll.ReadHwndMemoryValue((IntPtr)hwnd, addr4 + (p * 4) + 144);
                                    x = LoadDll.ReadHwndMemoryValue((IntPtr)hwnd, addr5 + 0x8);
                                    y = LoadDll.ReadHwndMemoryValue((IntPtr)hwnd, addr5 + 0xC);
                                    if (x == 1 && y == 1)
                                    {
                                        xy[j] = addr4 + p * 4 + 144 - 12;

                                        break;
                                    }
                                }


                            }
                            break;

                            //=============================================

                        }

                    }

                }

            }
            return xy;

        }

        /// <summary>
        /// 关闭对话
        /// </summary>
        /// <param name="hwnd"></param>
        public static void CloseDia(int hwnd)
        {

            for (int i = 0; i < 10; i++)
            {
                int show = LoadDll.ReadHwndMemoryOffsetValue((IntPtr)hwnd, Global.addr.win, 0x68, 0x8, 0x68, (i * 4), 0x28);
                int type = LoadDll.ReadHwndMemoryOffsetValue((IntPtr)hwnd, Global.addr.win, 0x68, 0x8, 0x68, (i * 4), 0x18);
                if (show == 0)
                {

                    if (type == 641 || type == 579 || type == 465 || type == 544 || type == 550 || type == 278 || type == 370 || type == 290 || type == 641 || type == 581 || type == 544 || type == 425 || type == 360 || type == 362 || type == 190 || type == 500 || type == 560 || type == 410)
                    {
                        int addr = LoadDll.ReadHwndMemoryOffsetValue((IntPtr)hwnd, Global.addr.win, 0x68, 0x8, 0x68, (i * 4)) + 0x28;
                        LoadDll.WriteMemoryHwndValue((IntPtr)hwnd, addr, BitConverter.GetBytes(1));
                    }

                }
            }



        }
        /// <summary>
        /// 隐藏或者显示全部UI
        /// </summary>
        /// <param name="hwnd"></param>
        public static void UIShowStatus(int hwnd, bool show)
        {
            int UI = LoadDll.ReadHwndMemoryOffsetValue((IntPtr)hwnd, Global.addr.win, 0x68, 0x8) + 0x28;
            //0显示 1隐藏
            int s = show == true ? 0 : 1;
            show = LoadDll.WriteMemoryHwndValue((IntPtr)hwnd, UI, BitConverter.GetBytes(s));

        }
        /// <summary>
        /// 隐藏验证窗口
        /// </summary>
        /// <param name="hwnd"></param>
        public static void HideCheckWin(int hwnd)
        {
            int tmpPtr = LoadDll.ReadHwndMemoryOffsetValue((IntPtr)hwnd, Global.addr.win, 0x68, 0x04, 0x68);
            tmpPtr = LoadDll.ReadHwndMemoryValue((IntPtr)hwnd, tmpPtr) + 0x28;

            LoadDll.WriteMemoryHwndValue((IntPtr)hwnd, tmpPtr, BitConverter.GetBytes(1));
        }
        public static void clickPeople(int hwnd)
        {
            //防止小杂碎检测
            Random r = new Random();
            int ry = r.Next(-12, -5);

            IntPtr pid = IntPtr.Zero;
            LoadDll.GetWindowThreadProcessId((IntPtr)hwnd, out pid);
            //第一步先定位一个临时地址，也就是4小人黑框的地址
            int tmpPtr = LoadDll.ReadMemoryOffsetValue(pid.ToInt32(), Global.addr.win, 0x68, 0x04, 0x68);
            //判断界面是否打开
            int no = LoadDll.ReadMemoryOffsetValue(pid.ToInt32(), tmpPtr, 0x28);
            if (no != 0) { return; }

            bool find = false;
            // 黑框的X起点坐标
            int x = LoadDll.ReadMemoryOffsetValue(pid.ToInt32(), tmpPtr, 0x08);
            //黑框的Y起点坐标
            int y = LoadDll.ReadMemoryOffsetValue(pid.ToInt32(), tmpPtr, 0x0c);

            //伪基址 黑框向下一层继续读地址
            int wPtr = LoadDll.ReadMemoryOffsetValue(pid.ToInt32(), tmpPtr, 0x54);

            //二级基址 找到第一个小人方向所在的地址
            int twoPtr = LoadDll.ReadMemoryOffsetValue(pid.ToInt32(), new IntPtr(wPtr + 0x04).ToInt32(), 0x8C, 0x28, 0x04, 0x14);

            //用第一个地址向下继续读 C 30 如果读到1证明这个小人面对你，获取小人坐标
            if (LoadDll.ReadMemoryOffsetValue(pid.ToInt32(), twoPtr + 0x0c, 0x30) == 1)
            {
                x += 50;
                y = y + LoadDll.ReadMemoryOffsetValue(pid.ToInt32(), tmpPtr, 0x54, 0x4, 0xc) + ry;
                find = true;
            }

            //找到第二个小人方向所在的地址
            twoPtr = LoadDll.ReadMemoryOffsetValue(pid.ToInt32(), new IntPtr(wPtr + 0x08).ToInt32(), 0x8C, 0x28, 0x04, 0x14);

            // 用第二个地址向下继续读 C 30 如果读到1证明这个小人面对你，获取小人坐标
            if (LoadDll.ReadMemoryOffsetValue(pid.ToInt32(), twoPtr + 0x0c, 0x30) == 1)
            {
                x += 140;
                y = y + LoadDll.ReadMemoryOffsetValue(pid.ToInt32(), tmpPtr, 0x54, 0x08, 0xC) + ry;
                find = true;

            }

            //找到第三个小人方向所在的地址
            twoPtr = LoadDll.ReadMemoryOffsetValue(pid.ToInt32(), new IntPtr(wPtr + 0x0C).ToInt32(), 0x8C, 0x28, 0x04, 0x14);
            //用第三个地址向下继续读 C 30 如果读到1证明这个小人面对你，获取小人坐标
            if (LoadDll.ReadMemoryOffsetValue(pid.ToInt32(), twoPtr + 0x0c, 0x30) == 1)
            {
                x += 230;
                y = y + LoadDll.ReadMemoryOffsetValue(pid.ToInt32(), tmpPtr, 0x54, 0x0C, 0xC) + ry;
                find = true;

            }

            //  找到第四个小人方向所在的地址
            twoPtr = LoadDll.ReadMemoryOffsetValue(pid.ToInt32(), new IntPtr(wPtr + 0x10).ToInt32(), 0x8C, 0x28, 0x04, 0x14);
            if (LoadDll.ReadMemoryOffsetValue(pid.ToInt32(), twoPtr + 0x0c, 0x30) == 1)
            {
                x += 320;
                y = y + LoadDll.ReadMemoryOffsetValue(pid.ToInt32(), tmpPtr, 0x54, 0x10, 0xC) + ry;
                find = true;


            }
            if (find == true)
            {
                clickWin(hwnd, x, y);
                Log.WriteLine("点击小人");
                find = false;
            }



        }
        public static uint MAKELONG(ushort x, ushort y)
        {
            return ((((uint)x) << 16) | y); //low order WORD 是指标的x位置； high order WORD是y位置.
        }
        /// <summary>
        /// 任务点击
        /// </summary>
        /// <param name="hwnd"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public static void taskClick(int hwnd, int x, int y)
        {
            Global.mouseMx.WaitOne();
            //获取窗口焦点
            LoadDll.SetForegroundWindow((IntPtr)hwnd);
            //设置窗口显示状态
            LoadDll.SetWindowPos((IntPtr)hwnd, -2, 0, 0, 0, 0, 3);
            System.Threading.Thread.Sleep(1500);
            MouseMove(hwnd, x, y);
            // 写入单击事件
            LoadDll.WriteMemoryHwndValue((IntPtr)hwnd, new IntPtr(Global.addr.bX - 8).ToInt32(), BitConverter.GetBytes(1));
            System.Threading.Thread.Sleep(80);
            LoadDll.WriteMemoryHwndValue((IntPtr)hwnd, new IntPtr(Global.addr.bX - 8).ToInt32(), BitConverter.GetBytes(0));
            Global.mouseMx.ReleaseMutex();

        }
        /// <summary>
        /// 鼠标移动
        /// </summary>
        /// <param name="hwnd"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public static void MouseMove(int hwnd, int x, int y)
        {
            while (true)
            {
                int lx = LoadDll.ReadHwndMemoryOffsetValue((IntPtr)hwnd, Global.addr.ls, 0x64);
                int ly = LoadDll.ReadHwndMemoryOffsetValue((IntPtr)hwnd, Global.addr.ls, 0x68);
                if ((lx == x && y == ly) || (Math.Abs(x - lx) <= 1 && Math.Abs(ly - y) <= 1))
                {
                    break;
                }

                double xx = (x - lx) * 0.92;
                double yy = (y - ly) * 0.92;

                int dx = LoadDll.ReadHwndMemoryOffsetValue((IntPtr)hwnd, Global.addr.bX);
                int dy = LoadDll.ReadHwndMemoryOffsetValue((IntPtr)hwnd, new IntPtr(Global.addr.bX + 4).ToInt32());

                int x1 = (int)xx + dx;
                int y1 = (int)yy + dy;
                LoadDll.WriteMemoryHwndValue((IntPtr)hwnd, Global.addr.bX, BitConverter.GetBytes(x1));
                LoadDll.WriteMemoryHwndValue((IntPtr)hwnd, new IntPtr(Global.addr.bX + 4).ToInt32(), BitConverter.GetBytes(y1));
                System.Threading.Thread.Sleep(30);
            }


        }

        public static void clickWin(int hwnd, int x, int y)
        {
            IntPtr wnd = (IntPtr)hwnd;

            //int lx = LoadDll.ReadHwndMemoryOffsetValue(wnd, Global.addr.ls, 0x64);
            //int ly = LoadDll.ReadHwndMemoryOffsetValue(wnd, Global.addr.ls, 0x68);

            //double xx = (x - lx) * 0.92;
            //double yy = (y - ly) * 0.92;

            //int dx = LoadDll.ReadHwndMemoryOffsetValue(wnd, Global.addr.bX);
            //int dy = LoadDll.ReadHwndMemoryOffsetValue(wnd, new IntPtr(Global.addr.bX + 4).ToInt32());

            //int x1 = (int)xx + dx;
            //int y1 = (int)yy + dy;
            MouseMove(hwnd, x, y);

            //LoadDll.WriteMemoryHwndValue(wnd, Global.addr.bX, BitConverter.GetBytes(x1));
            //LoadDll.WriteMemoryHwndValue(wnd, new IntPtr(Global.addr.bX + 4).ToInt32(), BitConverter.GetBytes(y1));


            // 写入单击事件
            LoadDll.WriteMemoryHwndValue(wnd, new IntPtr(Global.addr.bX - 8).ToInt32(), BitConverter.GetBytes(1));
            System.Threading.Thread.Sleep(80);
            LoadDll.WriteMemoryHwndValue(wnd, new IntPtr(Global.addr.bX - 8).ToInt32(), BitConverter.GetBytes(0));

        }
        /// <summary>
        /// 寻路call
        /// </summary>
        /// <param name="em"></param>
        public static void way(mhxy.EventMsg em)
        {


            IntPtr pid = IntPtr.Zero;
            LoadDll.GetWindowThreadProcessId(em.hwnd, out pid);
            int map = Global.addr.GetMapY(em.hwnd.ToInt32());
            //==================
            em.posX = em.posX * 20;
            em.posY = map - em.posY * 20 - 10;
            //=======================
            int wayPos = Global.addr.way;

            long dwTempAddr = 0x11000900 - wayPos;
            dwTempAddr = 0xFFFFFFFF - dwTempAddr - 0x20;
           
            string szHookString = "85 C9 0F 95 C0 0F B6 C0 50 6A 01 51 55 FF B6 14 01 00 00 8B CF FF B6 10 01 00 00";
            szHookString = szHookString + "B8"; 
            szHookString = szHookString + StringUtil.FormatIntToHex(Global.addr.GetPeopleID(em.hwnd.ToInt32()));
             
          
            szHookString = szHookString + " 39 46 0C 75 12 3E C7 44 24 08 ";
            szHookString = szHookString + StringUtil.FormatIntToHex(em.posX);
            szHookString = szHookString + " 3E C7 44 24 0C ";
            szHookString = szHookString + StringUtil.FormatIntToHex(em.posY);
            szHookString = szHookString + " E9 ";
           
            szHookString = szHookString + StringUtil.FormatIntToHex((int)dwTempAddr);
            szHookString = szHookString + " 00 00 00 00";
            szHookString = szHookString.Replace(" ", "");
           
            //==================================================
            byte[] data = StringUtil.strToToHexByte(szHookString);
            bool success = LoadDll.WriteMemoryValue(pid.ToInt32(), 0x11000900, data);
            //==================================================
            szHookString = "E9 ";
            dwTempAddr = 0x11000900 - wayPos - 0x5;
            szHookString = szHookString + StringUtil.FormatIntToHex((int)dwTempAddr);
            szHookString = szHookString.Replace(" ", "");
          
            data = StringUtil.strToToHexByte(szHookString);
            success = LoadDll.WriteMemoryValue(pid.ToInt32(), wayPos, data);
            //===================================================================
            //隐藏UI
            //UIShowStatus(em.hwnd.ToInt32(), false);
            ////隐藏玩家和摊位
            //HidePlayer(em.hwnd.ToInt32());
            //HideShop(em.hwnd.ToInt32());
            //CloseDia(em.hwnd.ToInt32());
            //System.Threading.Thread.Sleep(100);
            ////调用鼠标触发call
            //clickWin(em.hwnd.ToInt32(), em.mX, em.mY);
            //System.Threading.Thread.Sleep(300);
            //UIShowStatus(em.hwnd.ToInt32(), true);
            System.Threading.Thread.Sleep(5000);
            //==================================================================
            //复位call代码
            data = StringUtil.strToToHexByte("85D20F95C0");
            success = LoadDll.WriteMemoryValue(pid.ToInt32(), wayPos, data);

        }
        /// <summary>
        /// 游戏过出口
        /// </summary>
        /// <param name="hwnd">句柄</param>
        /// <param name="x">出口X</param>
        /// <param name="y">出口Y</param>
        /// <returns></returns>
        public static int[] MhxyToExit(int hwnd, int x, int y)
        {
            int[] ret = new int[2];

            int mapY = Global.addr.GetMapY(hwnd);
            //屏幕宽高
            int[] scr = Global.addr.GetMhxyScreen(hwnd);

            //目的地址坐标
            int s_x = x * 20 + 10;
            int s_y = mapY - ((y * 20) + 10);

            //////计算两点的距离
            //int[] xy = Global.addr.getPeopleXY(hwnd);
            ////换算为原始坐标
            //int p_x = xy[0] * 20 + 10;
            //int p_y = mapY - ((xy[1] * 20) + 10);
            //拿到渲染的中心点，计算显示区域
            int c_x = LoadDll.ReadHwndMemoryOffsetValue((IntPtr)hwnd, Global.addr.MapAddr, 0xCC);
            int c_y = LoadDll.ReadHwndMemoryOffsetValue((IntPtr)hwnd, Global.addr.MapAddr, 0xD0);

            int x_min = c_x - scr[0] / 2;
            int x_max = c_x + scr[0] / 2;

            int y_min = c_y - scr[1] / 2;
            int y_max = c_y + scr[1] / 2;
            // Log.WriteLine("屏幕区间坐标【{0},{1}】，【{2},{3}】", x_min, y_min, x_max, y_max);
            //鼠标点击该坐标
            Log.WriteLine("过出口【{0},{1}】", x, y);
            // Log.WriteLine("当前【{0},{1}】，目标地址【{2},{3}】", p_x, p_y, s_x, s_y);

            //关闭对话框
            CloseDia(hwnd);
            UIShowStatus(hwnd, false);
            System.Threading.Thread.Sleep(300);
            CallFunc.clickWin(hwnd, s_x - x_min, s_y - y_min);
            System.Threading.Thread.Sleep(200);
            UIShowStatus(hwnd, true);
            return ret;
        }
    }
}
