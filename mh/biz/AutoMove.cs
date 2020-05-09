using mh.mhxy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace mh.biz
{
    /// <summary>
    /// 自动移动
    /// </summary>
    public class AutoMove
    {
        biz.Execute ex = null;
        /// <summary>
        /// 移动类型
        /// 0指定移动到某一个点
        /// 1乱求跑
        /// </summary>
        public int moveType = 0;
        public int hwnd = 0;
        public int x = 0;
        public int y = 0;
        //移动线程
        private Thread moveThread;
        public bool status = true;
        private Mutex mutex = new Mutex();
        private int prveX = 0;
        private int prevY = 0;

        public int mapid = 0;
        public Mutex wayMx = new Mutex();

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="hwnd">句柄</param>
        /// <param name="moveType">移动方式</param>
        /// <param name="x">目地坐标X</param>
        /// <param name="y">目地坐标Y</param>
        public AutoMove(int hwnd, int moveType, int x, int y, int mapid, biz.Execute e)
        {
            this.hwnd = hwnd;
            this.moveType = moveType;
            this.x = x;
            this.y = y;
            this.ex = e;
            this.mapid = mapid;
        }
        public int getPID()
        {
            IntPtr pid = IntPtr.Zero;
            LoadDll.GetWindowThreadProcessId((IntPtr)this.hwnd, out pid);
            return pid.ToInt32();
        }
        /// <summary>
        /// 生成随机xy坐标
        /// 随机移动
        /// </summary>
        public void RandomMove()
        {

            int map_id = Global.addr.GetMapID(hwnd);
            if (Global.map_reg.ContainsKey(map_id) && Global.map_reg[map_id].Count > 0)
            {
                Random r = new Random();



                int idx = r.Next(0, Global.map_reg[map_id].Count);
                model.MhxyMapRegion map = Global.map_reg[map_id][idx];
                this.x = r.Next(map.x, map.max_x);
                this.y = r.Next(map.max_y, map.y);
                //call到目的地

                CallMoveXY(x, y);
            }



        }
        public void CallMoveXY(int mx, int my)
        {
            Log.WriteLine("前往坐标【{0}，{1}】", mx, my);
            //call到目的地
            mhxy.EventMsg me = new mhxy.EventMsg((IntPtr)hwnd, 100, 100, mx, my);
            mhxy.CallFunc.way(me);
        }
        public void ReSetMove(int mapId, int x, int y, int type)
        {
            //停止线程
            this.Stop();

            this.x = x;
            this.y = y;
            this.mapid = mapId;
            this.moveType = type;
            this.status = true;

            this.Run();
        }
        private void Move()
        {
            mutex.WaitOne();
            //随机移动标识
            bool randomMove = false;
            while (status)
            {
                wayMx.WaitOne();
                //跳过战斗状态,,地图差异不运行
                if (!ex.battle.battle)
                {
                    if (Global.addr.GetMapID(hwnd) != mapid)
                    {
                        Log.WriteLine("停止寻路，地图已经切换【{0}】", mapid);
                        this.status = false;
                    }
                    int[] xy = Global.addr.getPeopleXY(hwnd);
                    //状态判断，在战斗状态，跳过
                    switch (moveType)
                    {

                        case 0:
                            if (prveX != xy[0] && prevY != xy[1])
                            {
                                prveX = xy[0];
                                prevY = xy[1];

                                Log.WriteLine("move.....");

                            }
                            else
                            {

                                //停止状态，判断是否到达目的地
                                if (x == xy[0] && y == xy[1])
                                {
                                    //退出线程
                                    this.status = false;
                                    Log.WriteLine("到达目的地");
                                }
                                else
                                {
                                    Log.WriteLine("继续开车！");
                                    CallMoveXY(x, y);

                                }
                            }

                            break;
                        case 1:
                            //这里就是在当前地图瞎转悠
                            //第一次随机坐标
                            if (randomMove == false)
                            {
                                Log.WriteLine("初始化随机坐标");
                                randomMove = true;
                                RandomMove();
                            }
                            if (prveX != xy[0] && prevY != xy[1])
                            {
                                prveX = xy[0];
                                prevY = xy[1];
                                Log.WriteLine("移动中... ...");

                            }
                            else
                            {
                                Log.WriteLine("达到地址");

                                RandomMove();

                            }


                            break;
                    }

                }

                wayMx.ReleaseMutex();
                //1300毫秒检测一次
                System.Threading.Thread.Sleep(2000);

            }

            mutex.ReleaseMutex();
        }
        //启动执行
        public void Run()
        {
            status = true;
            moveThread = new Thread(new ThreadStart(Move));
            moveThread.Start();
        }
        //线程停止
        public void Stop()
        {
            this.status = false;
            mutex.WaitOne();
            mutex.ReleaseMutex();
        }
    }
}
