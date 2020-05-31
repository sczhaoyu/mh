using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
namespace mh.mhxy
{
    /// <summary>
    /// 梦幻西游地址管理
    /// </summary>
    class AddrManager
    {




        /// <summary>
        /// 人物ID地址
        /// </summary>
        public int PeopleID;
        public int BytesIndexOf(byte[] old, byte[] v)
        {
            for (int i = 0; i < old.Length; i++)
            {
                if (old[i] == v[0])
                {
                    bool ret = true;
                    for (int j = 0; j < v.Length; j++)
                    {
                        //防止下标越界
                        if (i + j >= old.Length)
                        {
                            return -1;
                        }
                        if (old[i + j] != v[j])
                        {
                            ret = false;
                        }

                    }
                    if (ret)
                    {
                        return i;

                    }
                }
            }
            return -1;
        }
        /// <summary>
        /// 更新梦幻西游基址
        /// </summary>
        /// <param name="hwnd"></param>
        public Addr loadAddr(int hwnd)
        {
            Addr addr = new Addr();
            //读取进程内存
            IntPtr pid = IntPtr.Zero;
            LoadDll.GetWindowThreadProcessId((IntPtr)hwnd, out pid);
            //如果(读内存(pid, 模块起址, AA, 7340032, 容器))

            byte[] buffer = new byte[7340032];
            //获取缓冲区地址
            int start = 0x11000000;
            IntPtr byteAddress = Marshal.UnsafeAddrOfPinnedArrayElement(buffer, 0);
            IntPtr hProcess = LoadDll.OpenProcess(0x1F0FFF, false, pid.ToInt32());
            bool ret = LoadDll.ReadProcessMemory(hProcess, new IntPtr(start), byteAddress, 7340032, IntPtr.Zero);
            if (ret)
            {
                //查找摊位基址

                int idx = BytesIndexOf(buffer, StringUtil.strToToHexByte("8B C6 8B 4C 24 38 64 89 0D 00 00 00 00 59 5F 5E 83 C4 38 C3"));
                if (idx > 0)
                {
                    addr.shop = BitConverter.ToInt32(buffer.Skip(idx + 65).Take(4).ToArray(), 0);

                }


                //搜索人物坐标基址
                idx = StringUtil.IndexOfBytes(buffer, "83 C8 01 A3 ?? ?? ?? ?? 83 EC 08 C7 44 24 14 00 00 00 00 B9 ?? ?? ?? ?? C7 44 24 04 00 00 00 00 C7 04 24 00 00 00 00");
                if (idx > 0)
                {
                     
                    addr.x = BitConverter.ToInt32(buffer.Skip(idx + 20).Take(4).ToArray(), 0);
                    addr.y = addr.x+4;
                    Log.WriteLine("人物X地址:{0}", StringUtil.IntToHex(addr.x));
                    Log.WriteLine("人物Y地址:{0}", StringUtil.IntToHex(addr.y));

                }
                //搜索地图基址
                idx = BytesIndexOf(buffer, new byte[] { 199, 68, 36, 52, 255, 255, 255, 255, 15, 90, 192, 131, 236, 8, 185 });
                if (idx > 0)
                {
                    addr.MapAddr = BitConverter.ToInt32(buffer.Skip(idx + 15).Take(4).ToArray(), 0);
                    Log.WriteLine("地图基址{0}", StringUtil.IntToHex(addr.MapAddr));
                }

                //搜索人物ID基址
                //idx = BytesIndexOf(buffer, StringUtil.strToToHexByte("89 44 24 10 55 85 C0 75 2A 8D 44 24 14 50"));
                //if (idx > 0)
                //{
                //    addr.PeopleID = BitConverter.ToInt32(buffer.Skip(idx - 4).Take(4).ToArray(), 0);
                //    Log.WriteLine("人物ID基址{0}", StringUtil.IntToHex(addr.PeopleID));
                //}


                //白鼠基址
                addr.bX = 0x11F01994;
                addr.bY = 0x11F01998;
                Log.WriteLine("白鼠基址{0}", StringUtil.IntToHex(addr.bX));

                //搜索蓝鼠基址
                idx = BytesIndexOf(buffer, StringUtil.strToToHexByte("8D 44 24 08 83 C4 04 50 8B 01"));
                if (idx > 0)
                {
                    addr.ls = BitConverter.ToInt32(buffer.Skip(idx + 15).Take(4).ToArray(), 0);
                    Log.WriteLine("蓝鼠基址{0}", StringUtil.IntToHex(addr.ls));

                }
                //搜索战斗基址
                idx = BytesIndexOf(buffer, new byte[] { 139, 76, 36, 4, 139, 84, 36, 8, 139, 4, 141 });
                if (idx > 0)
                {
                    addr.zd = BitConverter.ToInt32(buffer.Skip(idx + 11).Take(4).ToArray(), 0) + 96;
                    Log.WriteLine("战斗基址{0}", StringUtil.IntToHex(addr.zd));
                }

                //搜索窗口地址
                idx = BytesIndexOf(buffer, StringUtil.strToToHexByte("83 C4 04 85 C9 74 06 8B 01 56 FF 50 28"));
                if (idx > 0)
                {
                    addr.win = BitConverter.ToInt32(buffer.Skip(idx + 15).Take(4).ToArray(), 0);
                    Log.WriteLine("窗口地址{0}", StringUtil.IntToHex(addr.win));
                    addr.dialogue = LoadDll.ReadMemoryValue(pid.ToInt32(), addr.win);
                    addr.dialogue = LoadDll.ReadMemoryValue(pid.ToInt32(), addr.dialogue + 84);
                    addr.dialogue = LoadDll.ReadMemoryValue(pid.ToInt32(), addr.dialogue + 4);
                    addr.dialogue = LoadDll.ReadMemoryValue(pid.ToInt32(), addr.dialogue + 64);

                    addr.dialogue = LoadDll.ReadMemoryOffsetValue(pid.ToInt32(), addr.dialogue, 4);
                    Log.WriteLine("基址对话:{0}", StringUtil.IntToHex(addr.dialogue));

                }

                ////寻路HOOK  
                idx = BytesIndexOf(buffer, StringUtil.strToToHexByte("85 D2 0F 95 C0 0F B6 C0 50 6A 01 51 55 FF B6 14 01 00 00 8B CF FF B6 10 01 00 00"));
                if (idx > 0)
                {
                    addr.way = idx + start;
                    Log.WriteLine("寻路基址:{0}", StringUtil.IntToHex(addr.way));

                }
                //包开始地址
                idx = BytesIndexOf(buffer, StringUtil.strToToHexByte("7E 08 8A C2 B3 35 F6 EB"));
                if (idx > 0)
                {

                    addr.toPkg = start + idx + 1 + 27;
                    addr.pkgEnd = addr.toPkg - 41;
                    Log.WriteLine("转包基址:{0}", StringUtil.IntToHex(addr.toPkg));
                    Log.WriteLine("包止基址:{0}", StringUtil.IntToHex(addr.pkgEnd));
                }
                //明文地址
                idx = BytesIndexOf(buffer, StringUtil.strToToHexByte("8B 44 24 04 8A 4C 24 0C"));
                if (idx > 0)
                {
                    addr.msg = start + idx + 1 + 7;
                    Log.WriteLine("明文基址:{0}", StringUtil.IntToHex(addr.msg));
                }
            }
            LoadDll.CloseHandle(hProcess);
            return addr;
        }
    }
}
