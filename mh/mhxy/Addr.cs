using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace mh.mhxy
{
    /// <summary>
    /// 梦幻西游基础地址
    /// </summary>
    /// 
    class Addr
    {
        //public int PeopleID;//人物ID基址
        public int MapAddr;//地图地址
        public int x;//人物x地址
        public int y;//人物y地址
        public int bX;//白鼠x地址
        public int bY;//白鼠y地址
        public int ls;//兰鼠地址
        public int zd;//战斗地址
        public int win;//游戏UI窗口地址
        public int dialogue;//对话框地址
        public int way;//寻路基础地址
        public int Screen = 0x11DB4B7C;//屏幕大小基础地址
        public int shop;
        public int toPkg;//转包地址
        public int pkgEnd;//包终止地址
        public int msg;//明文地址
        public int hookWay;//寻路钩子


        public int GetPeopleID(int hwnd)
        {
            
            string str = LoadDll.GetWindowsName((IntPtr)hwnd).Replace(" ", "");
            int start = str.LastIndexOf("[") + 1;
            int end = str.LastIndexOf("]");
            str = str.Substring(start, end - start);
            return Convert.ToInt32(str);

        }
        /// <summary>
        /// 返回人物坐标
        /// 第一个是x
        /// 第二个是y
        /// </summary>
        /// <param name="hwnd"></param>
        /// <returns></returns>
        public int[] getPeopleXY(int hwnd)
        {
            int[] xy = new int[2];
            IntPtr pid = IntPtr.Zero;
            LoadDll.GetWindowThreadProcessId((IntPtr)hwnd, out pid);
            byte[] buffer = new byte[4];
            IntPtr byteAddress = Marshal.UnsafeAddrOfPinnedArrayElement(buffer, 0);
            IntPtr hProcess = LoadDll.OpenProcess(0x1F0FFF, false, pid.ToInt32());
            //读取x坐标
            bool ret = LoadDll.ReadProcessMemory(hProcess, new IntPtr(this.x), byteAddress, 4, IntPtr.Zero);
            double val = BitConverter.ToSingle(buffer, 0);
            xy[0] = (int)Math.Floor(val / 20);
            //读取y坐标
            int mapY = LoadDll.ReadMemoryOffsetValue(pid.ToInt32(), this.MapAddr, 0x60, 0x14);
            LoadDll.ReadProcessMemory(hProcess, new IntPtr(this.y), byteAddress, 4, IntPtr.Zero);
            val = BitConverter.ToSingle(buffer, 0);
            xy[1] = (int)Math.Floor((mapY - val) / 20);
            LoadDll.CloseHandle(hProcess);
            return xy;
        }
        /// <summary>
        /// 获得地图最大的XY坐标
        /// 第一个是x
        /// 第二个是y
        /// </summary>
        /// <param name="hwnd"></param>
        /// <returns></returns>
        public int[] GetMapMaxXY(int hwnd)
        {
            int[] xy = new int[2];
            IntPtr pid = IntPtr.Zero;
            LoadDll.GetWindowThreadProcessId((IntPtr)hwnd, out pid);
            byte[] buffer = new byte[4];
            IntPtr byteAddress = Marshal.UnsafeAddrOfPinnedArrayElement(buffer, 0);
            IntPtr hProcess = LoadDll.OpenProcess(0x1F0FFF, false, pid.ToInt32());
            //读取x坐标上限
            xy[0] = LoadDll.ReadMemoryOffsetValue(pid.ToInt32(), MapAddr, 0x60, 0x10);
            //读取y坐标上限
            xy[1] = LoadDll.ReadMemoryOffsetValue(pid.ToInt32(), MapAddr, 0x60, 0x14);
            //日狗的游戏算法*20
            xy[0] = (int)(xy[0] / 20);
            xy[1] = (int)(xy[1] / 20);
            //换算游戏坐标
            return xy;
        }
        //获取地图最大Y
        public int GetMapY(int hwnd)
        {
            return LoadDll.ReadHwndMemoryOffsetValue((IntPtr)hwnd, MapAddr, 0x60, 0x14);
        }
        /// <summary>
        /// 获取地图ID
        /// </summary>
        /// <param name="hwnd"></param>
        /// <returns></returns>
        public int GetMapID(int hwnd)
        {
            return LoadDll.ReadHwndMemoryOffsetValue((IntPtr)hwnd, MapAddr, 0x50, 0x0C);
        }
        /// <summary>
        /// 获取屏幕大小
        /// </summary>
        /// <param name="hwnd"></param>
        /// <returns></returns>
        public int[] GetMhxyScreen(int hwnd)
        {
            int[] ret = new int[2];

            ret[0] = LoadDll.ReadHwndMemoryValue((IntPtr)hwnd, Screen);
            ret[1] = LoadDll.ReadHwndMemoryValue((IntPtr)hwnd, Screen + 4);
            return ret;
        }


    }
}
