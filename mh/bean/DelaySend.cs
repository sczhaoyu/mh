using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace mh.bean
{
    public class DelaySend
    {
        public delegate void SendCallBack();
        //随机延迟
        bool random;
        int s;
        int e;
        //发送的命令
        string cmd;
        int hwnd;
        SendCallBack prevFunc;
        SendCallBack lastFunc;
        /// <summary>
        /// 延迟发送
        /// </summary>
        /// <param name="r">是否随机延迟</param>
        /// <param name="s">随机开始</param>
        /// <param name="e">随机结束</param>
        /// <param name="c">命令</param>
        public DelaySend(int hwnd, bool r, int s, int e, string c, SendCallBack prevFunc, SendCallBack lastFunc)
        {
            this.random = r;
            this.s = s;
            this.e = e;
            this.cmd = c;
            this.hwnd = hwnd;
            this.prevFunc = prevFunc;
            this.lastFunc = lastFunc;
        }
        //延迟发送
        void delaySend()
        {
            if (prevFunc != null)
            {
                this.prevFunc();
            }
            if (random)
            {
                Random r = new Random();
                int m = r.Next(s, e);
                System.Threading.Thread.Sleep(m * 1000);
            }
            else
            {
                System.Threading.Thread.Sleep(s * 1000);
            }
            LoadDll.sendMsg((IntPtr)hwnd, cmd);
            if (lastFunc != null)
            {
                this.lastFunc();
            }
            Log.WriteLine("延迟发送报文：{0}", cmd);
        }
        public void Run()
        {
            Thread th = new Thread(new ThreadStart(delaySend));
            th.Start();
        }

    }
}
