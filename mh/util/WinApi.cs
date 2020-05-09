using System;
using System.Collections.Generic;
using System.IO;
using System.IO.MemoryMappedFiles;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Text;

namespace mh.util
{
    public class WinApi
    {
        [DllImport("kernel32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        public static extern IntPtr CreateFileMapping(
              IntPtr hFile,
              IntPtr lpFileMappingAttributes,
              FileMapProtection flProtect,
              uint dwMaximumSizeHigh,
              uint dwMaximumSizeLow,
              string lpName);


        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern IntPtr MapViewOfFile(
            IntPtr hFileMappingObject,
            FileMapAccess dwDesiredAccess,
            UInt32 dwFileOffsetHigh,
            UInt32 dwFileOffsetLow,
            UIntPtr dwNumberOfBytesToMap);

        [DllImport("Kernel32.dll", CharSet = CharSet.Auto)]
        public static extern bool UnmapViewOfFile(IntPtr pvBaseAddress);

        [DllImport("Kernel32.dll", CharSet = CharSet.Auto)]
        public static extern bool CloseHandle(IntPtr handle);

        [DllImport("Kernel32.dll")]
        public static extern int WaitForSingleObject(IntPtr hHandle, uint dwMilliseconds);
        [DllImport("Kernel32.dll", CharSet = CharSet.Auto)]
        public static extern bool ReleaseMutex(IntPtr handle);


        [DllImport("Kernel32.dll")]
        public static extern IntPtr GetProcAddress(IntPtr hwnd, string lpname);
        [DllImport("Kernel32.dll")]
        public static extern int CreateMutexA(
           LPSECURITY_ATTRIBUTES lpMutexAttributes, // 指向安全属性的指针
            bool bInitialOwner, // 初始化互斥对象的所有者
            string lpName // 指向互斥对象名的指针);
            );

        [DllImport("Kernel32.dll")]
        public static extern int OpenMutexA(
                DWDesiredAccess dwDesiredAccess, // MUTEX_ALL_ACCESS 请求对互斥体的完全访问 MUTEX_MODIFY_STATE 允许使用 ReleaseMutex 函数 SYNCHRONIZE 允许互斥体对象同步使用
                bool bInheritHandle, //如希望子进程能够继承句柄，则为TRUE
                string lpName //要打开对象的名字
          );
        [DllImport("Kernel32.dll")]
        public static extern IntPtr VirtualAllocEx(
            IntPtr hwnd, //申请内存所在的进程句柄。
            int lpaddress, //保留页面的内存地址；一般用NULL自动分配 。
            int size, //欲分配的内存大小，字节单位；注意实际分 配的内存大小是页内存大小的整数倍
            AllocationType flAllocationType,
            int flProtect//操作权限
            );
        [DllImport("Kernel32.dll")]
        public static extern IntPtr GetModuleHandleA(string name);
        [DllImport("kernel32.dll")]
        public static extern IntPtr LoadLibrary(string lpFileName);
        [DllImport("Kernel32.dll")]
        public extern static IntPtr LoadLibraryA(string path);

        [DllImport("Kernel32.dll")]
        public static extern IntPtr CreateRemoteThread(IntPtr hwnd, int attrib, int size, IntPtr address, IntPtr par, int flags, int threadid);
        [DllImport("kernel32.dll")]//在其它进程中释放申请的虚拟内存空间
        public static extern bool VirtualFreeEx(
                                  IntPtr hProcess,//目标进程的句柄,该句柄必须拥有PROCESS_VM_OPERATION的权限
                                  IntPtr lpAddress,//指向要释放的虚拟内存空间首地址的指针
                                  int dwSize,
                                  uint dwFreeType//释放类型
        );
        [DllImport("Kernel32.dll")]
        public static extern int GetExitCodeThread(
            IntPtr hThread,                  // in,想获取退出代码的一个线程的句柄     
            ref int lpExitCode               //out,用于装载线程退出代码的一个长整数变量。如线程尚未中断，则设为常数STILL_ACTIVE
        );

        public static bool DoInjection(string DllPath, IntPtr hwnd)
        {
            IntPtr hProcess = LoadDll.GetHwndProcess(hwnd);


            byte[] dllBytes = System.Text.Encoding.Default.GetBytes(DllPath);

            int BufSize = dllBytes.Length;
            IntPtr AllocAddr = VirtualAllocEx(hProcess, 0, BufSize, AllocationType.MEM_COMMIT, 0x04);
            LoadDll.WriteProcessMemory(hProcess, AllocAddr, dllBytes, BufSize, IntPtr.Zero);

            IntPtr pfnStartAddr = GetProcAddress(GetModuleHandleA("kernel32.dll"), "LoadLibraryA");

            IntPtr hRemoteThread = CreateRemoteThread(hProcess, 0, 0, pfnStartAddr, AllocAddr, 0, 0);

            // 等待远线程结束
            WaitForSingleObject(hRemoteThread, 0xFFFFFFFF);
            // 取DLL在目标进程的句柄
            int remoteModule = 0;
            GetExitCodeThread(hRemoteThread, ref remoteModule);

            LoadDll.Free(hProcess, hRemoteThread, AllocAddr);
            return hRemoteThread.ToInt32() > 0;

        }
        /// <summary>
        /// 申请内存
        /// </summary>
        /// <param name="hwnd"></param>
        /// <returns></returns>
        public static IntPtr MallocMemory(IntPtr hwnd, int size)
        {
            IntPtr pid = IntPtr.Zero;
            //获取窗口句柄
            LoadDll.GetWindowThreadProcessId(hwnd, out pid);

            //获取进程句柄
            IntPtr prohWnd = LoadDll.OpenProcess(0x1F0FFF, false, pid.ToInt32());
            if (prohWnd != IntPtr.Zero)
            {
                //执行VirtualProtectEx函数

                IntPtr addr = VirtualAllocEx(prohWnd, 0, size, AllocationType.MEM_COMMIT, 64);
                LoadDll.CloseHandle(prohWnd);
                return addr;

            }
            return IntPtr.Zero;
        }
        [Flags]
        public enum AllocationType : uint
        {

            MEM_COMMIT = 0x00001000,
            MEM_RESERVE = 0x2000,
            MEM_DECOMMIT = 0x4000,
            MEM_RELEASE = 0x8000,
            MEM_FREE = 0x10000,
            MEM_PRIVATE = 0x20000,
            MEM_MAPPED = 0x40000,
            MEM_RESET = 0x80000,
            MEM_TOP_DOWN = 0x100000,
            MEM_WRITE_WATCH = 0x200000,
            MEM_PHYSICAL = 0x400000,
            MEM_ROTATE = 0x800000,
            MEM_DIFFERENT_IMAGE_BASE_OK = 0x800000,
            MEM_RESET_UNDO = 0x1000000,
            MEM_LARGE_PAGES = 0x20000000,
            MEM_4MB_PAGES = 0x80000000
        }

        public enum WaitForSingle : uint
        {

            IGNORE = 0,
            INFINITE = 0xFFFFFFFF,

        }

        [Flags]
        public enum DWDesiredAccess : uint
        {
            MUTEX_ALL_ACCESS = 2031617,
            MUTEX_MODIFY_STATE = 1
        }
        [Flags]
        public enum FileMapProtection : uint
        {
            PageReadonly = 0x02,
            PageReadWrite = 0x04,
            PageWriteCopy = 0x08,
            PageExecuteRead = 0x20,
            PageExecuteReadWrite = 0x40,
            SectionCommit = 0x8000000,
            SectionImage = 0x1000000,
            SectionNoCache = 0x10000000,
            SectionReserve = 0x4000000,
        }
        [Flags]
        public enum FileMapAccess : uint
        {
            FileMapCopy = 0x0001,
            FileMapWrite = 0x0002,
            FileMapRead = 0x0004,
            FileMapAllAccess = 0x001f,
            FileMapExecute = 0x0020,
        }
        public class LPSECURITY_ATTRIBUTES
        {
            public int nLength = 0;
            public int lpSecurityDescriptor = 0;
            public int bInheritHandle = 0;
        }
    }
}
