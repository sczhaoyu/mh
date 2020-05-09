using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace mh.biz
{
    /// <summary>
    /// 路程设置
    /// </summary>
    class Distance
    {
        /// <summary>
        /// 完成调用
        /// </summary>
        public delegate void FinishCallBack();
        Thread th;
        public bool runStatus = true;
        public FinishCallBack callback = null;
        //行程路径
        List<model.MhxyRouterRec> list;
        //当前移动任务
        public int idx = 0;
        public int hwnd;
        public bool init = true;
        Execute ex;
        AutoMove am;
        /// <summary>
        ///开始地图和目标
        /// </summary>
        /// <param name="mapID"></param>
        /// <param name="targetId"></param>
        public Distance(Execute e, int mapID, int targetId, FinishCallBack callback)
        {
            list = model.MhxyRouterRec.GetRouterAll(mapID, targetId);
            this.hwnd = e.hwnd.ToInt32();
            this.ex = e;
            this.callback = callback;
            //判断当前地图所在位置，是否需要继续寻路
            for (int i = 0; i < list.Count; i++)
            {
                if (list[i].mapExit.toMap.map_no == Global.addr.GetMapID(hwnd))
                {
                    if (list.Count - 1 > i)
                    {
                        idx = i + 1;
                    }
                    else
                    {
                        idx = i;
                    }

                }
                //坐标偏移
                bean.Axis a = OffsetAxis(list[i].mapExit.wait_x, list[i].mapExit.wait_y, -1, 2, -2, 2);
                model.MhxyAxis ma = RandomAxis(list[i].mapExit.id, 1);
                if (ma != null)
                {
                    a = new bean.Axis(ma.x, ma.y);
                }
                list[i].mapExit.wait_x = a.x;
                list[i].mapExit.wait_y = a.y;

            }
            if (idx <= list.Count - 1)
            {
                Log.WriteLine("开始地图：{0}", list[idx].mapExit.myMap.name);
            }

        }
        /// <summary>
        /// 取出随机坐标
        /// </summary>
        /// <param name="flag_id"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public model.MhxyAxis RandomAxis(int flag_id, int type)
        {
            Random r = new Random();
            List<model.MhxyAxis> list = model.MhxyAxis.GetIDList(flag_id, type);
            if (list.Count > 0)
            {
                int idx = r.Next(0, list.Count - 1);
                return list[idx];
            }
            return null;
        }
        private void ExcuteTask()
        {

            //设置初始化信息移动
            am = new AutoMove(hwnd, 0, list[idx].mapExit.wait_x, list[idx].mapExit.wait_y, list[idx].mapExit.myMap.map_no, ex);

            while (runStatus)
            {
                if (init)
                {
                    am.Run();
                    init = false;
                }
                //检查是否到达该地图出口
                if (list[idx].exit_id > 0)
                {
                    int[] xy = Global.addr.getPeopleXY(hwnd);
                    //判断是否达到出口的地图
                    if (list[idx].mapExit.toMap.map_no == Global.addr.GetMapID(hwnd))
                    {

                        //判断是否还有任务存在，如果没有就结束当前线程
                        if (list.Count - 1 == idx)
                        {
                            runStatus = false;
                            am.Stop();
                        }
                        else
                        {
                            //还有剩下任务没有处理，继续处理
                            //移动下标位置
                            if (idx + 1 < list.Count - 1)
                            {
                                idx += 1;
                            }
                            else
                            {
                                idx = list.Count - 1;
                            }
                            //启用下一次寻路任务
                            int x = list[idx].mapExit.wait_x;
                            int y = list[idx].mapExit.wait_y;
                            am.ReSetMove(Global.addr.GetMapID(hwnd), x, y, 0);
                        }

                    }
                    else
                    {
                        //判断是否到达地址过出口
                        if (xy[0] == list[idx].mapExit.wait_x && xy[1] == list[idx].mapExit.wait_y)
                        {
                            if (list[idx].mapExit.npc_id == 0)
                            {
                                //调用过出口
                                mhxy.CallFunc.MhxyToExit(hwnd, list[idx].mapExit.x, list[idx].mapExit.y);
                            }
                            else
                            {
                                string cmd = mhxy.CallNPC.OpenDialog(list[idx].mapExit.npc_id);

                                //打开NPC的对话框
                                LoadDll.sendMsg((IntPtr)hwnd, cmd);
                                // NPC过出口 呼出NPC 注册收包选项
                                ex.RegMsgCallBack(0x81, new MsgCallBack(false, delegate (int msgType, byte[] data)
                                {
                                    //回调代码
                                    LoadDll.sendMsg((IntPtr)hwnd, String.Format("82 02 {0} 00", list[idx].mapExit.call_npc_option));
                                    System.Threading.Thread.Sleep(1500);
                                    mhxy.CallFunc.CloseDia(hwnd);

                                }, true, null));


                            }


                        }
                        else
                        {
                            //过口失败重新过去
                            if (am.status == false)
                            {

                                if (xy[0] != list[idx].mapExit.wait_x || xy[1] != list[idx].mapExit.wait_y)
                                {
                                    Log.WriteLine("过出口失败，重新定位出口！");
                                    int x = list[idx].mapExit.wait_x;
                                    int y = list[idx].mapExit.wait_y;
                                    am.ReSetMove(Global.addr.GetMapID(hwnd), x, y, 0);
                                }


                            }
                        }



                    }


                }
                System.Threading.Thread.Sleep(2000);
            }
            Log.WriteLine("已到达目的地图");
            if (list[idx].is_type == 0)
            {
                MoveNPC(list[idx].target_id);
            }
            else
            {
                if (callback != null)
                {
                    callback();
                }
            }


        }
        /// <summary>
        /// 坐标偏移
        /// </summary>
        /// <returns></returns>
        public bean.Axis OffsetAxis(int x, int y, int xs, int xe, int ys, int ye)
        {
            Random r = new Random();
            x = x + r.Next(xs, xe);
            y = y + r.Next(ys, ye);
            bean.Axis a = new bean.Axis(x, y);
            return a;
        }
        public void MoveNPC(int npcid)
        {

            //获取NPC坐标
            model.MhxyNPC npc = model.MhxyNPC.GetNPCID(npcid);
            if (npc == null || npc.x == 0 || npc.y == 0)
            {
                return;
            }
            //坐标偏移
            bean.Axis a = OffsetAxis(npc.x, npc.y, -3, 3, -4, 4);
            model.MhxyAxis ma = RandomAxis(npcid, 0);
            if (ma != null)
            {
                a = new bean.Axis(ma.x, ma.y);
            }


            npc.x = a.x;
            npc.y = a.y;
            am.ReSetMove(Global.addr.GetMapID(hwnd), npc.x, npc.y, 0);
            bool to = true;
            while (to)
            {
                int[] xy = Global.addr.getPeopleXY(hwnd);
                if (xy[0] == npc.x && xy[1] == npc.y)
                {
                    Log.WriteLine("已到达NPC所在坐标！");
                    to = false;
                    if (callback != null)
                    {
                        callback();
                    }

                }
                System.Threading.Thread.Sleep(1000);
            }
        }
        public void Run()
        {
            th = new Thread(new ThreadStart(ExcuteTask));
            th.Start();
        }
    }
}
