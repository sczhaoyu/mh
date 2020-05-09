using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace mh.biz
{
    public class MsgCallBack
    {
        //消息回调函数
        public delegate void CallBack(int msgType, byte[] data);
        //true永久存活，false 只调用一次
        public bool type;
        public CallBack ck;
        //true 一步，false 同步
        public bool is_asynchronous;
        public int msgType;
        public byte[] data;
        //规则匹配
        private Dictionary<int, int> match = new Dictionary<int, int>();
        public MsgCallBack(bool type, CallBack ck, bool is_asynchronous, Dictionary<int, int> match)
        {
            this.type = type;
            this.ck = ck;
            this.is_asynchronous = is_asynchronous;
            this.match = match;
        }
        void asynchronous()
        {

            ck(msgType, data);
        }
        public void Run(int msgType, byte[] data)
        {
            this.msgType = msgType;
            this.data = data;
            Thread th = new Thread(new ThreadStart(asynchronous));
            th.Start();
        }
        public void CallFunc(int msgType, byte[] data)
        {
            //判断调用规则
            if (!matchDB(msgType, data))
            {
                return;
            }
            if (is_asynchronous)
            {

                //异步调用
                Run(msgType, data);
            }
            else
            {
                ck(msgType, data);
            }
        }
        //数据匹配
        public bool matchDB(int msgType, byte[] data)
        {
            if (match == null)
            {
                return true;
            }
            bool flag = true;
            foreach (var item in match)
            {
                if (data.Length < item.Key)
                {
                    return false;
                }
                if (data[item.Key] != item.Value)
                {
                    flag = false;
                }
            }
            return flag;

        }
    }
    public class Execute
    {

        //状态消息
        public Dictionary<int, string> statusList = new Dictionary<int, string>();
        //当前状态
        public int status;
        //消息类型映射到状态
        public Dictionary<int, int> msgTypeMapStatus = new Dictionary<int, int>();
        public IntPtr hwnd;
        //战斗信息
        public Battle battle = null;
        //自己属性
        public bean.GamePlayer mySelf;
        //宝宝属性
        public bean.Baby myBaby;
        //自动移动
        public biz.AutoMove am = null;
        //镖局人物
        public BiaoJuTask bjtask;
        //注册回调
        private Dictionary<int, List<MsgCallBack>> msgCallback = new Dictionary<int, List<MsgCallBack>>();

        
        /// <summary>
        /// 初始化函数
        /// </summary>
        /// <param name="MHHwnd">句柄</param>
        public Execute(IntPtr MHHwnd)
        {
            loadStatusList();
            this.hwnd = MHHwnd;
            this.battle = new Battle(this);
            myBaby = new bean.Baby(this);
            mySelf = new bean.GamePlayer(this);
            bjtask = new BiaoJuTask(this);
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
        
      
        /// <summary>
        /// 注册消息回调函数
        /// </summary>
        /// <param name="msgKey"></param>
        /// <param name="callback"></param>
        public void RegMsgCallBack(int msgKey, MsgCallBack callback)
        {
            if (!msgCallback.ContainsKey(msgKey))
            {
                msgCallback[msgKey] = new List<MsgCallBack>();
            }

            msgCallback[msgKey].Add(callback);
        }
        /// <summary>
        /// 动作处理器路由
        /// </summary>
        /// <param name="dataType">收发</param>
        /// <param name="revcData">数据</param>
        public void switchExecute(int dataType, byte[] revcData)
        {
            //运行注册后的回调
            if (msgCallback.ContainsKey(revcData[0]))
            {
                List<MsgCallBack> clist = msgCallback[revcData[0]];
                for (int i = 0; i < clist.Count; i++)
                {
                    //调用函数
                    clist[i].CallFunc(dataType, revcData);
                    //一次使用,保护路径
                    if (clist[i].type == false && clist[i].matchDB(dataType, revcData))
                    {

                        msgCallback[revcData[0]].RemoveAt(i);
                    }
                }
            }

            //数据类型
            int msgType = revcData[0];

            switch (msgType)
            {

                //战斗操作信息
                case 0x47:
                case 0x61:
                case 0x62:
                case 0x63:
                case 0x64:
                case 0x65:
                    battle.handle(dataType, msgType, revcData);
                    break;
                //人物处理
                case 0x20:
                case 0x21:
                    mySelf.updateAttr(revcData);
                    break;
                //宝宝属性处理
                case 0xD0:
                case 0xBB:
                case 0xB1:
                    myBaby.updateAttr(revcData);
                    break;
                case 0x81:
                case 0x89:
                case 0xFA:
                case 0x42:
                case 0xFF:
                case 0xCE:
                    bjtask.handle(revcData, dataType);
                    break;

            }
        }
        public void loadStatusList()
        {
            //定义状态信息
            statusList[0x00] = "移动";
            statusList[0x01] = "停止";
            statusList[0x02] = "战斗";
            msgTypeMapStatus[0x61] = 0x02;
        }
    }
}
