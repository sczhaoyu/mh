using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace mh.mhxy
{
    /// <summary>
    /// 梦幻内核
    /// </summary>
    public class MHKernel
    {
        int hwnd;
        util.ShareMemory recvHwnd;
        util.ShareMemory sendHwnd;
        int shareSize = 8096;
        util.WinMutex sendHC;
        util.WinMutex recvHC;
        bool play = true;
        //过滤规则
        Dictionary<int, string> ruleMap = new Dictionary<int, string>();
        Dictionary<int, Dictionary<int, byte[]>> ruleValues = new Dictionary<int, Dictionary<int, byte[]>>();

        public bool Init()
        {

            //string t = "[3=08,4=00]={48,32,62,78}[3=07,4=00]={48,32,62,78}";

            //RegisterRule(0x80, t);
            //byte[] b = new byte[] { 0x80, 0x00, 0x00, 0x08, 0x00 };

            ////   byte [] ret=  matchRule(b);
            //return false;
            hwnd = LoadDll.FindWindow("WSGAME", null).ToInt32();
            Global.addr = new mhxy.AddrManager().loadAddr(hwnd);
            //汇编指令
            byte[] code = { 104, 0, 0, 0, 0, 104, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 87, 191, 0, 0, 46, 0, 141, 127, 1, 136, 12, 16, 131, 63, 0, 117, 10, 232, 128, 0, 0, 0, 131, 63, 0, 116, 77, 131, 127, 5, 0, 117, 11, 232, 70, 0, 0, 0, 131, 127, 5, 0, 116, 60, 133, 192, 117, 13, 96, 106, 255, 255, 55, 185, 54, 17, 159, 117, 255, 209, 97, 139, 95, 5, 136, 76, 24, 4, 131, 248, 3, 114, 31, 15, 183, 90, 1, 131, 195, 2, 57, 216, 114, 20, 139, 95, 5, 199, 3, 1, 0, 0, 0, 96, 255, 55, 187, 30, 17, 159, 117, 255, 211, 97, 95, 194, 12, 0, 96, 141, 79, 9, 81, 106, 0, 106, 2, 186, 11, 75, 159, 117, 255, 210, 104, 0, 32, 0, 0, 49, 210, 82, 82, 106, 2, 80, 186, 193, 24, 159, 117, 255, 210, 95, 87, 137, 71, 5, 97, 195, 96, 141, 79, 21, 81, 106, 0, 104, 1, 0, 31, 0, 186, 239, 239, 160, 117, 255, 210, 95, 87, 137, 7, 97, 195, 49, 192, 232, 0, 0, 0, 0, 89, 139, 121, 39, 133, 255, 116, 19, 49, 219, 102, 187, 0, 4, 137, 7, 137, 4, 59, 141, 60, 95, 137, 7, 137, 4, 59, 95, 91, 89, 194, 8, 0, 104, 0, 0, 0, 0, 104, 0, 0, 0, 0, 104, 0, 0, 0, 0, 104, 0, 0, 0, 0, 104, 0, 0, 0, 0, 104, 0, 0, 0, 0, 104, 0, 0, 0, 0, 104, 0, 0, 0, 0, 131, 124, 36, 24, 0, 117, 12, 232, 131, 0, 0, 0, 131, 124, 36, 24, 0, 116, 115, 131, 124, 36, 28, 0, 117, 12, 232, 161, 0, 0, 0, 131, 124, 36, 28, 0, 116, 96, 96, 106, 255, 255, 116, 36, 60, 185, 54, 17, 159, 117, 255, 209, 97, 49, 200, 139, 76, 36, 24, 133, 210, 117, 17, 60, 241, 117, 13, 15, 182, 89, 5, 131, 195, 2, 137, 95, 16, 198, 1, 1, 49, 219, 102, 187, 0, 4, 128, 57, 1, 15, 68, 68, 17, 4, 141, 12, 25, 137, 68, 17, 4, 137, 17, 141, 12, 25, 128, 57, 1, 15, 68, 68, 17, 4, 141, 12, 25, 137, 68, 17, 4, 96, 255, 116, 36, 60, 187, 30, 17, 159, 117, 255, 211, 97, 131, 196, 32, 95, 91, 89, 194, 8, 0, 96, 141, 76, 36, 48, 81, 106, 0, 106, 2, 187, 11, 75, 159, 117, 255, 211, 104, 0, 32, 0, 0, 49, 219, 83, 83, 106, 2, 80, 186, 193, 24, 159, 117, 255, 210, 137, 68, 36, 60, 139, 76, 36, 32, 137, 65, 210, 97, 195, 96, 141, 76, 36, 36, 81, 106, 0, 104, 1, 0, 31, 0, 186, 239, 239, 160, 117, 255, 210, 137, 68, 36, 64, 139, 76, 36, 32, 137, 65, 186, 97, 195, 0, 0, 0, 0, 0, 0 };

            string hexHwnd = StringUtil.IntToHex(hwnd);
            recvHwnd = new util.ShareMemory("MYs" + hexHwnd, shareSize);
            sendHwnd = new util.ShareMemory("MYf" + hexHwnd, shareSize);
            recvHC = new util.WinMutex("HCs" + hexHwnd);
            sendHC = new util.WinMutex("HCf" + hexHwnd);
            Start();
            code = StringUtil.ReplaceBytes(code, 11, 12, GetByteEnd(recvHwnd.HwndName, new byte[] { 0 }));

            code = StringUtil.ReplaceBytes(code, 23, 12, GetByteEnd(recvHC.Name, new byte[] { 0 }));

            byte[] jj = GetByteEnd(sendHwnd.HwndName, new byte[] { 0 });
            byte[] kk = jj.Skip(jj.Length - 4).Take(4).ToArray();
            //=====================================
            code = StringUtil.ReplaceBytes(code, 261, 4, kk);
            kk = jj.Skip(4).Take(4).ToArray();
            code = StringUtil.ReplaceBytes(code, 266, 4, kk);
            kk = jj.Skip(0).Take(4).ToArray();
            code = StringUtil.ReplaceBytes(code, 271, 4, kk);

            jj = GetByteEnd(sendHC.Name, new byte[] { 0 });
            kk = jj.Skip(jj.Length - 4).Take(4).ToArray();
            code = StringUtil.ReplaceBytes(code, 276, 4, kk);
            kk = jj.Skip(4).Take(4).ToArray();
            code = StringUtil.ReplaceBytes(code, 281, 4, kk);
            kk = jj.Skip(0).Take(4).ToArray();
            code = StringUtil.ReplaceBytes(code, 286, 4, kk);
            //====

            int addr = (int)util.WinApi.MallocMemory((IntPtr)hwnd, 4096);
            code = StringUtil.ReplaceBytes(code, 37, 4, BitConverter.GetBytes(addr));

            IntPtr kernelHwnd = util.WinApi.GetModuleHandleA("kernel32.dll");
            int[] moduleAddrs = new int[5];
            moduleAddrs[0] = (int)util.WinApi.GetProcAddress(kernelHwnd, "WaitForSingleObject");
            moduleAddrs[1] = (int)util.WinApi.GetProcAddress(kernelHwnd, "ReleaseMutex");
            moduleAddrs[2] = (int)util.WinApi.GetProcAddress(kernelHwnd, "OpenFileMappingA");
            moduleAddrs[3] = (int)util.WinApi.GetProcAddress(kernelHwnd, "MapViewOfFile");
            moduleAddrs[4] = (int)util.WinApi.GetProcAddress(kernelHwnd, "OpenMutexA");
            int[] oldAddrs = new int[5] { 1973358902, 1973358878, 1973373707, 1973360833, 1973481455 };
            int n = 0;
            int flag = 0;//标志位
            for (int i = 0; i < 10; i++)
            {
                if (flag == 5) { flag = 0; }
                kk = BitConverter.GetBytes(oldAddrs[flag]);
                jj = BitConverter.GetBytes(moduleAddrs[flag]);
                n = n + 1;
                n = StringUtil.FindBytes(code, kk, n);
                code = StringUtil.ReplaceBytes(code, n + 1, 4, jj);
                flag++;
            }


            LoadDll.WriteMemoryHwndValue((IntPtr)hwnd, addr, code);

            code = StringUtil.AppendBytes(new byte[] { 104 }, BitConverter.GetBytes(addr + 34), new byte[] { 195 });

            LoadDll.WriteMemoryHwndValue((IntPtr)hwnd, Global.addr.msg, code);

            code = StringUtil.AppendBytes(new byte[] { 104 }, BitConverter.GetBytes(addr + 209), new byte[] { 195 });
            LoadDll.WriteMemoryHwndValue((IntPtr)hwnd, Global.addr.pkgEnd, code);

            code = StringUtil.AppendBytes(new byte[] { 104 }, BitConverter.GetBytes(addr + 249), new byte[] { 195 });
            LoadDll.WriteMemoryHwndValue((IntPtr)hwnd, Global.addr.toPkg, code);



            return false;
        }
        /// <summary>
        /// 注册过滤规则
        /// </summary>
        /// <param name="head"></param>
        /// <param name="rule"></param>
        public void RegisterRule(int head, string rule)
        {

            if (ruleMap.ContainsKey(head))
            {
                ruleMap[head] += rule;
            }
            else
            {
                ruleMap[head] = rule;
            }
            //重新生成规则
            string[] values = ruleMap[head].Split('}');
            for (int i = 0; i < values.Length; i++)
            {
                int s = values[i].IndexOf('{');
                if (s > -1)
                {
                    values[i].Substring(s, values[i].Length);
                }
            }

        }
        /// <summary>
        /// 规则匹配
        /// </summary>
        /// <param name="head"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public int matchRule(byte[] data)
        {
            int head = data[0];
            if (ruleMap.ContainsKey(head))
            {
                //处理得到规则
                string[] rule = ruleMap[head].Split('}');

                for (int i = 0; i < rule.Length; i++)
                {
                    int s = rule[i].IndexOf('[');
                    int e = rule[i].IndexOf(']');

                    if (s > -1 && e > -1)
                    {

                        rule[i] = rule[i].Substring(s, e + 1);
                    }
                }

                //获取规则组
                for (int i = 0; i < rule.Length; i++)
                {
                    if (rule[i].Trim() == "")
                    {
                        continue;
                    }
                    //[7=48,6=49]
                    //取出条件匹配是否满足
                    string[] r = rule[i].Replace("[", "").Replace("]", "").Split(',');
                    bool flag = true;
                    //判断规则里面的条件
                    for (int k = 0; k < r.Length; k++)
                    {

                        string[] ret = r[k].Split('=');
                        int idx = Convert.ToInt32(ret[0]);
                        //处理下标越界
                        if (idx > data.Length - 1)
                        {
                            //中断当前循环，重置标志位,组数据不匹配
                            flag = false;
                            break;
                        }
                        int val = Convert.ToInt32(ret[1], 16);
                        //判断配置条件
                        if (data[idx] != val)
                        {
                            flag = false;
                        }
                    }
                    //已经匹配到不显示的规则
                    if (flag)
                    {
                        return i;
                    }
                }

            }

            return -1;
        }
        //截获的收包线程
        public void ReadLoop()
        {
            while (play)
            {

                util.WinApi.WaitForSingleObject((IntPtr)recvHC.MutexHANDLE, 0);

                IntPtr sv = new IntPtr(recvHwnd.lpBase.ToInt32() + 5);
                int len = StringUtil.GetPtrToInt(sv);
                int read = BitConverter.ToInt32(recvHwnd.Read(recvHwnd.lpBase.ToInt32(), 4), 0);
                if (len != 0 && read == 1)
                {
                    len = len & 65535;
                    len = len + 3;
                    byte[] b = recvHwnd.Read(new IntPtr(recvHwnd.lpBase.ToInt32() + 4).ToInt32(), len);
                    // Console.WriteLine("收包:{0}", StringUtil.FormatBytesToHex(b));

                    recvHwnd.Write(recvHwnd.lpBase, BitConverter.GetBytes(0));
                }
                util.WinApi.ReleaseMutex((IntPtr)recvHC.MutexHANDLE);

            }
        }
        /// <summary>
        /// 拦截发包线程
        /// </summary>
        public void WriteLoop()
        {

            while (play)
            {
                util.WinApi.WaitForSingleObject((IntPtr)sendHC.MutexHANDLE, 0);
                int edx = StringUtil.GetPtrToInt(new IntPtr(sendHwnd.lpBase.ToInt32() + 1024));
                int len = 0;

                if (edx > 257)
                {

                    len = StringUtil.GetPtrToInt(new IntPtr(sendHwnd.lpBase.ToInt32() + 1029));
                    len = len & 65535;
                    if (edx > len)
                    {
                        len += 3;
                        byte[] b = recvHwnd.Read(new IntPtr(sendHwnd.lpBase.ToInt32() + 1028).ToInt32(), len);
                        Console.WriteLine("发包:{0}", StringUtil.FormatBytesToHex(b));
                    }

                }
                //正常发包地址
                if (edx >= 2 && edx < 260)
                {
                    len = StringUtil.GetPtrToInt(new IntPtr(sendHwnd.lpBase.ToInt32() + 1029));
                    len = len & 255;
                    if (edx > len)
                    {
                        len += 2;

                        byte[] b = recvHwnd.Read(new IntPtr(sendHwnd.lpBase.ToInt32() + 1028).ToInt32(), len);
                        Console.WriteLine("发包1:{0}", StringUtil.FormatBytesToHex(b));
                    }

                }
                //寻找过滤特征
                if (edx > 2)
                {
                    len = edx + 1;
                    byte[] b = recvHwnd.Read(new IntPtr(sendHwnd.lpBase.ToInt32() + 1028).ToInt32(), len);
                    //匹配规则
                    // matchRule(b);
                    if (b[0] == 0x80)
                    {
                         //sendHwnd.Write(new IntPtr(sendHwnd.lpBase.ToInt32() + 2052), new byte[] { 0x09, 0x0C, 0x05, 0x00, 0x20, 0x00, 0x00, 0x00, 0x00, 0x00 });
                        // sendHwnd.Write(new IntPtr(sendHwnd.lpBase.ToInt32() + 2048), BitConverter.GetBytes(1));
                        // 写到内存(数据[i].假封包1, 发共地址 ＋ 2052, )
                        // 写到内存(1, 发共地址 ＋ 2048, 4)
                         //Console.WriteLine("发包2:{0}", StringUtil.FormatBytesToHex(b));

                    }

                }
                //读取篡改的封包

                edx = StringUtil.GetPtrToInt(new IntPtr(sendHwnd.lpBase.ToInt32() + 3072));
                if (edx > 0)
                {
                    Console.WriteLine("篡改包:{0}", edx);
                }
                sendHwnd.Write(new IntPtr(sendHwnd.lpBase.ToInt32() + 4), BitConverter.GetBytes(0));
                util.WinApi.ReleaseMutex((IntPtr)sendHC.MutexHANDLE);
            }


        }
        public void Start()
        {
            Thread read = new Thread(ReadLoop);
            Thread write = new Thread(WriteLoop);
            read.Start();
            write.Start();
        }
        public void writeByte(byte[] code)
        {
            string ret = "";
            for (int i = 0; i < code.Length; i++)
            {
                ret += code[i].ToString() + ",";
            }
            Console.WriteLine(ret);
        }
        public byte[] GetByteEnd(string name, byte[] b)
        {
            byte[] nameByte = System.Text.Encoding.Default.GetBytes(name);
            byte[] ret = new byte[nameByte.Length + b.Length];
            nameByte.CopyTo(ret, 0);
            b.CopyTo(ret, nameByte.Length);
            return ret;
        }
    }
}
