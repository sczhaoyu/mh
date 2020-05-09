using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace mh.util
{
    class ShareMemory
    {
        public IntPtr hMapFile = IntPtr.Zero;
        public IntPtr lpBase = IntPtr.Zero;
        public String HwndName;
        int Size = 0;
        public ShareMemory(string name, int size)
        {
            CreateShare(name, size);
        }
        /// <summary>
        /// 创建内存共享区
        /// </summary>
        /// <param name="name"></param>
        /// <param name="size"></param>
        public void CreateShare(string name, int size)
        {
            string body = "创建共享区失败！";

            hMapFile = util.WinApi.CreateFileMapping(IntPtr.Zero, IntPtr.Zero, util.WinApi.FileMapProtection.PageReadWrite, 0, (uint)size, name);
            if (hMapFile != IntPtr.Zero)
            {
                lpBase = util.WinApi.MapViewOfFile(hMapFile, util.WinApi.FileMapAccess.FileMapAllAccess, 0, 0, new UIntPtr((uint)size));
                if (lpBase != IntPtr.Zero)
                {
                    body = String.Format("【共享区：{0}创建完成】", name);
                    HwndName = name;
                    this.Size = size;
                }
            }
            Console.WriteLine(body);

        }
        //读取数据
        public byte[] Read(int addr,int size)
        {
            byte[] ret = new byte[size];
            Marshal.Copy((IntPtr)addr, ret, 0, ret.Length);
            return ret;
        }

        //将数据写入共享内存中
        public void Write(IntPtr addr, byte[] data)
        {
            Marshal.Copy(data, 0, addr, data.Length);

        }
        public void Close()
        {
            util.WinApi.UnmapViewOfFile(lpBase);
            util.WinApi.CloseHandle(hMapFile);
        }
    }

    class WinMutex
    {
        //互斥对象
        public int LockHANDLE;
        //互斥句柄
        public int MutexHANDLE;
        public string Name;
        public WinMutex(string mx_name)
        {
          
            LockHANDLE = WinApi.CreateMutexA(null, false, mx_name);
            MutexHANDLE = WinApi.OpenMutexA(WinApi.DWDesiredAccess.MUTEX_ALL_ACCESS, false, mx_name);
            this.Name = mx_name;

        }
    }
}
