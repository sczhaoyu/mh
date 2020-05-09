using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace mh
{
    public class LoadDll
    {
        [DllImport("kernel32.dll")]
        public static extern Boolean AllocConsole();
        [DllImport("kernel32.dll")]
        public static extern Boolean FreeConsole();


        [DllImport("user32.dll")]
        public static extern int GetWindowTextLength(IntPtr hWnd);
        [DllImport("User32.dll", CharSet = CharSet.Auto)]
        public static extern int GetWindowText(IntPtr hwnd, StringBuilder text, int length);


        //消息发送API
        [DllImport("User32.dll")]
        public static extern int PostMessageA(
            IntPtr hWnd,        // 信息发往的窗口的句柄
            int Msg,            // 消息ID
            int wParam,         // 参数1
            int lParam            // 参数2
        );
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern int SetWindowPos(IntPtr hWnd, int hWndInsertAfter, int x, int y, int Width, int Height, int flags);
        [DllImport("user32.dll ")]
        public static extern bool SetForegroundWindow(IntPtr hWnd);
        /// <summary>
        /// 获取窗体名称
        /// </summary>
        /// <param name="hwnd"></param>
        /// <returns></returns>
        public static string GetWindowsName(IntPtr hwnd)
        {
            int len = GetWindowTextLength(hwnd);
            StringBuilder windowName = new StringBuilder(len + 1);
            GetWindowText(hwnd, windowName, windowName.Capacity);
            return windowName.ToString();

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="hWnd">// 要修改内存的进程句柄</param>
        /// <param name="lpAddress"> 要修改内存的起始地址</param>
        /// <param name="dwSize">页区域大小</param>
        /// <param name="flNewProtect">新访问方式</param>
        /// <param name="lpflOldProtect">原访问方式 用于保存改变前的保护属性</param>
        /// <returns></returns>
        [DllImport("kernel32.dll")]
        public static extern int VirtualProtectEx(IntPtr hWnd, int lpAddress, Int32 dwSize, Int32 flNewProtect, ref Int32 lpflOldProtect);
        /// <summary>
        /// 获取窗体的句柄函数
        /// </summary>
        /// <param name="lpClassName">窗口类名</param>
        /// <param name="lpWindowName">窗口标题名</param>
        /// <returns>返回句柄</returns>
        [DllImport("user32.dll", EntryPoint = "FindWindow", SetLastError = true)]
        public static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        [DllImport("user32.dll", SetLastError = true)]
        public static extern IntPtr FindWindowEx(IntPtr parentHandle, IntPtr childAfter, string className, string windowTitle);

        

        //从指定内存中读取字节集数据
        [DllImportAttribute("kernel32.dll", EntryPoint = "ReadProcessMemory")]
        public static extern bool ReadProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, IntPtr lpBuffer, int nSize, IntPtr lpNumberOfBytesRead);

        //从指定内存中写入字节集数据
        [DllImportAttribute("kernel32.dll", EntryPoint = "WriteProcessMemory")]
        public static extern bool WriteProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, byte[] lpBuffer, int nSize, IntPtr lpNumberOfBytesWritten);

        //打开一个已存在的进程对象，并返回进程的句柄
        [DllImportAttribute("kernel32.dll", EntryPoint = "OpenProcess")]
        public static extern IntPtr OpenProcess(int dwDesiredAccess, bool bInheritHandle, int dwProcessId);

        //关闭一个内核对象。其中包括文件、文件映射、进程、线程、安全和同步对象等。
        [DllImport("kernel32.dll")]
        public static extern void CloseHandle(IntPtr hObject);

        [DllImport("User32.dll", CharSet = CharSet.Auto)]
        public static extern int GetWindowThreadProcessId(IntPtr hwnd, out IntPtr ID);
        /// <summary>
        /// 读取句柄中内存的值
        /// </summary>
        /// <param name="hwnd"></param>
        /// <param name="baseAddress"></param>
        /// <returns></returns>
        public static int ReadHwndMemoryValue(IntPtr hwnd, int baseAddress)
        {
            IntPtr pid = IntPtr.Zero;
            //获取窗口句柄
            LoadDll.GetWindowThreadProcessId(hwnd, out pid);
            int ret = LoadDll.ReadMemoryValue(pid.ToInt32(), baseAddress);
            return ret;
        }
        /// <summary>
        /// 读取浮点数据
        /// </summary>
        /// <param name="hwnd"></param>
        /// <param name="baseAddress"></param>
        /// <returns></returns>
        public static float ReadHwndMemoryFloatValue(IntPtr hwnd, int baseAddress)
        {
            IntPtr pid = IntPtr.Zero;
            //获取窗口句柄
            LoadDll.GetWindowThreadProcessId(hwnd, out pid);

            byte[] buffer = new byte[4];
            IntPtr byteAddress = Marshal.UnsafeAddrOfPinnedArrayElement(buffer, 0);
            IntPtr hProcess = LoadDll.OpenProcess(0x1F0FFF, false, pid.ToInt32());

            LoadDll.ReadProcessMemory(hProcess, (IntPtr)baseAddress, byteAddress, 4, IntPtr.Zero);
            float ret = BitConverter.ToSingle(buffer, 0);
            //关闭操作
            CloseHandle(hProcess);
            return ret;
        }
        /// <summary>
        /// 读取进程内存中的值
        /// </summary>
        /// <param name="pid"></param>
        /// <param name="baseAddress"></param>
        /// <returns></returns>
        public static int ReadMemoryValue(int pid, int baseAddress)
        {
            try
            {
                byte[] buffer = new byte[4];
                //获取缓冲区地址
                IntPtr byteAddress = Marshal.UnsafeAddrOfPinnedArrayElement(buffer, 0);
                //打开一个已存在的进程对象  0x1F0FFF 最高权限
                IntPtr hProcess = OpenProcess(0x1F0FFF, false, pid);
                //将制定内存中的值读入缓冲区
                ReadProcessMemory(hProcess, (IntPtr)baseAddress, byteAddress, 4, IntPtr.Zero);
                //关闭操作
                CloseHandle(hProcess);
                //从非托管内存中读取一个 32 位带符号整数。
                return Marshal.ReadInt32(byteAddress);
            }
            catch
            {
                return 0;
            }
        }
        public static byte[] intToBytes(int value)
        {
            byte[] src = new byte[4];
            src[3] = (byte)((value >> 24) & 0xFF);
            src[2] = (byte)((value >> 16) & 0xFF);
            src[1] = (byte)((value >> 8) & 0xFF);
            src[0] = (byte)(value & 0xFF);
            return src;
        }
        //将值写入指定内存地址中
        public static bool WriteMemoryHwndValue(IntPtr hwnd, int baseAddress, byte[] value)
        {
            bool ret = false;
            IntPtr pid = IntPtr.Zero;
            //获取窗口句柄
            LoadDll.GetWindowThreadProcessId(hwnd, out pid);
            try
            {
                //打开一个已存在的进程对象  0x1F0FFF 最高权限
                IntPtr hProcess = OpenProcess(0x1F0FFF, false, pid.ToInt32());
                //从指定内存中写入字节集数据
                ret = WriteProcessMemory(hProcess, (IntPtr)baseAddress, value, value.Length, IntPtr.Zero);
                //关闭操作
                CloseHandle(hProcess);
            }
            catch { }
            return ret;
        }
        /// <summary>
        /// 获取句柄进程，记得关闭
        /// </summary>
        /// <param name="hwnd"></param>
        /// <returns></returns>
        public static IntPtr GetHwndProcess(IntPtr hwnd)
        {

            IntPtr pid = IntPtr.Zero;
            //获取窗口句柄
            LoadDll.GetWindowThreadProcessId(hwnd, out pid);

            IntPtr hProcess = OpenProcess(0xFFFFF, false, pid.ToInt32());
            return hProcess;
        }

        //将值写入指定内存地址中
        public static bool WriteMemoryValue(int pid, int baseAddress, byte[] value)
        {
            bool ret = false;
            try
            {
                //打开一个已存在的进程对象  0x1F0FFF 最高权限
                IntPtr hProcess = OpenProcess(0x1F0FFF, false, pid);
                //从指定内存中写入字节集数据
                ret = WriteProcessMemory(hProcess, (IntPtr)baseAddress, value, value.Length, IntPtr.Zero);
                //关闭操作
                CloseHandle(hProcess);
            }
            catch { }
            return ret;
        }

        /// <summary>
        /// 读取窗口句柄中的值
        /// </summary>
        /// <param name="hwnd"></param>
        /// <param name="offset"></param>
        /// <returns></returns>
        public static int ReadHwndMemoryOffsetValue(IntPtr hwnd, params int[] offset)
        {
            IntPtr pid = IntPtr.Zero;
            //获取窗口句柄
            LoadDll.GetWindowThreadProcessId(hwnd, out pid);
            int ret = ReadMemoryOffsetValue(pid.ToInt32(), offset);
            return ret;
        }
        /// <summary>
        /// 读取偏移位置的值
        /// </summary>
        /// <param name="pid">进程ID</param>
        /// <param name="offset">偏移参数</param>
        /// <returns></returns>
        public static int ReadMemoryOffsetValue(int pid, params int[] offset)
        {
            int ret = 0;
            for (int i = 0; i < offset.Length; i++)

            {
                ret = ReadMemoryValue(pid, ret + offset[i]);

            }
            return ret;
        }


        //初始化dll函数
        [DllImport(@"mhxy.dll", EntryPoint = "init", SetLastError = true, CharSet = CharSet.Ansi, ExactSpelling = false, CallingConvention = CallingConvention.StdCall)]
        public extern static int initmhDll(int mh_hwnd, int hwnd);

        //发送数据包
        //[DllImport(@"mhxy.dll", EntryPoint = "sendMsg", SetLastError = true, CharSet = CharSet.Ansi, ExactSpelling = false, CallingConvention = CallingConvention.StdCall)]
        //public extern static int sendMsg(IntPtr hwnd, string body);
        public static bool sendMsg(IntPtr hwnd, string body)
        {
            byte[] b = StringUtil.strToToHexByte(body);
            return SendMsg(hwnd, b, b.Length);
        }
        /// <summary>
        /// 截获包类型
        /// </summary>
        /// <param name="subType">0指定包，1截获其他包</param>
        /// <returns></returns>
        [DllImport(@"mhxy.dll", EntryPoint = "subPkg", SetLastError = true, CharSet = CharSet.Ansi, ExactSpelling = false, CallingConvention = CallingConvention.StdCall)]
        public extern static int subPkg(int subType, IntPtr hwnd);

        [DllImport(@"mhxy.dll", EntryPoint = "MouseMsg", SetLastError = true, CharSet = CharSet.Ansi, ExactSpelling = false, CallingConvention = CallingConvention.StdCall)]
        public extern static int MouseMsg(int hwnd, int x, int y, int k, int c);

        /// <summary>
        /// 注入dll
        /// hwnd=0 传入打开进程句柄使用完成会关闭
        /// </summary>
        /// <param name="DllPath">全路径</param>
        /// <param name="hwnd">窗口句柄</param>
        /// <param name="hProcess">进程</param>
        /// <returns></returns>
        [DllImport(@"mhxy_kernel.dll", CallingConvention = CallingConvention.Cdecl)]
        public extern static int DoInjection(string DllPath, IntPtr hwnd, IntPtr hProcess);

        /// <summary>
        /// 跨进程取函数地址
        /// </summary>
        /// <param name="hwnd"></param>
        /// <param name="dllname"></param>
        /// <param name="lpProcName"></param>
        /// <returns></returns>
        [DllImport(@"mhxy_kernel.dll", CallingConvention = CallingConvention.Cdecl)]
        public extern static IntPtr GetHwndDllAddressEx(IntPtr hwnd, string dllname, string lpProcName);




        [DllImport(@"mhxy_kernel.dll", CallingConvention = CallingConvention.Cdecl)]
        public extern static void Free(IntPtr process, IntPtr remoteThread, IntPtr allocAddr);




        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1)]
        public class MhMsg
        {
            public MhMsg() { }
            public MhMsg(int type, int len, int hwnd, int addr, byte[] body)
            {
                this.type = type;
                this.len = len;
                this.addr = addr;
                this.body = body;
                this.hwnd = hwnd;
            }
            public int type;   //0收包，1发包，2替换包 消息类型
            public int len;    // 消息长度
            public int hwnd;   //发送消息的句柄
            public int addr;  //消息地址
            public byte[] body;//内容
        }
        /// <summary>
        /// 梦幻游戏函数地址
        /// </summary>
        /// 
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1)]
        public struct MhFuncAddrs
        {
            //dll起始点
            public IntPtr Benchmark;
            //远程初始化
            public IntPtr InitSystemRemoteThread;
            //设置通知回调地址
            public IntPtr NoticeCallBack;
            //注册过滤规则
            public IntPtr RegisterSendPkgRule;
            //设置消息回调
            public IntPtr SetMhMsgCallBack;
            //收包处理地址
            public IntPtr RecvMhxyPkgAddr;
            //发包处理地址
            public IntPtr ReplaceSendPkgByteAddr;
        }
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1)]
        public struct ProCallback
        {
            public IntPtr hwnd;
            public ResvMhMsg callBack;
        }
        //定义委托
        public delegate void ResvMhMsg(MhMsg m);

        [DllImport(@"mhxy_kernel.dll", CallingConvention = CallingConvention.Cdecl)]
        public extern static int InitSystem(string dllPath, int setMhMsgCallBack, int RecvMhxyPkgAddr, int ReplaceSendPkgByteAddr, IntPtr myHwnd, ResvMhMsg funcCallBack, IntPtr mhHwnd);

        [DllImport(@"mhxy_kernel.dll", CallingConvention = CallingConvention.Cdecl)]
        public extern static bool SendMsg(IntPtr hwnd, byte[] b, int len);


        public static int InitSystemMhxy(string path, IntPtr hwnd, IntPtr myHwnd, ResvMhMsg mhMsgCallBack)
        {
            //注入dll
            bool ret = util.WinApi.DoInjection(path, hwnd);
            
            if (!ret)
            {
                return 0;
            }
            //读取内存共享区函数基址
            string sdd = "mh_" + hwnd.ToInt32().ToString();
            util.ShareMemory recvHwnd = new util.ShareMemory(sdd, 4096);
            byte[] funcBytes = recvHwnd.Read(recvHwnd.lpBase.ToInt32(), Marshal.SizeOf(typeof(MhFuncAddrs)));
            MhFuncAddrs mf = (MhFuncAddrs)StringUtil.BytesToStruct(funcBytes, typeof(MhFuncAddrs));
            return InitSystem(path, mf.SetMhMsgCallBack.ToInt32(), mf.RecvMhxyPkgAddr.ToInt32(), mf.ReplaceSendPkgByteAddr.ToInt32(), myHwnd, mhMsgCallBack, hwnd);

        }
       

        /// <summary>
        /// 内存申请
        /// </summary>
        /// <param name="hwnd">窗口句柄</param>
        /// <param name="addr">地址</param>
        /// <param name="size">区域大小</param>
        /// <param name="attr">设置为PAGE_EXECUTE_READWRITE（0x40）时该内存页为可读可写可执行。</param>
        /// <param name="prevValue">内存原始属性类型保存地址</param>
        /// <returns></returns>
        public static int MemoryAlloc(IntPtr hwnd, int addr, int size, int attr, int prevValue)
        {
            IntPtr pid = IntPtr.Zero;
            //获取窗口句柄

            GetWindowThreadProcessId(hwnd, out pid);
            IntPtr hWnd = LoadDll.OpenProcess(0x1F0FFF, false, pid.ToInt32()); //获取进程句柄
            return VirtualProtectEx(hwnd, addr, 4096, 64, ref prevValue); //执行VirtualProtectEx函数
        }

        public static void InitSystemRemoteThread(string path, IntPtr hwnd, IntPtr myHwnd, ResvMhMsg mhMsgCallBack)
        {

            //注入dll
            bool ret = util.WinApi.DoInjection(path, hwnd);
            if (!ret)
            {
                return;
            }
            //读取内存共享区函数基址
            util.ShareMemory recvHwnd = new util.ShareMemory("mh_" + hwnd.ToInt32().ToString(), 4096);
            byte[] funcBytes = recvHwnd.Read(recvHwnd.lpBase.ToInt32(), Marshal.SizeOf(typeof(MhFuncAddrs)));



            MhFuncAddrs mf = (MhFuncAddrs)StringUtil.BytesToStruct(funcBytes, typeof(MhFuncAddrs));


            //获取远程进程的函数地址

            ProCallback pro = new ProCallback();
            pro.hwnd = myHwnd;
            pro.callBack = mhMsgCallBack;


            int size = Marshal.SizeOf(typeof(LoadDll.ProCallback));

            IntPtr process = LoadDll.GetHwndProcess(hwnd);
            IntPtr AllocAddr = util.WinApi.VirtualAllocEx(process, 0, size, util.WinApi.AllocationType.MEM_COMMIT, 0x04);


            byte[] data = StringUtil.StructToBytes(pro, size);
            LoadDll.WriteProcessMemory(process, AllocAddr, data, size, IntPtr.Zero);


            IntPtr hRemoteThread = util.WinApi.CreateRemoteThread(process, 0, 0, mf.InitSystemRemoteThread, AllocAddr, 0, 0);
            util.WinApi.WaitForSingleObject(hRemoteThread, 0xFFFFFFFF); //等待线程结束

            int remoteModule = 0;
            util.WinApi.GetExitCodeThread(hRemoteThread, ref remoteModule);
            LoadDll.Free(process, hRemoteThread, AllocAddr);
            Global.mh_func[hwnd] = mf;

        }
    }
}
