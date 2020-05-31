using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace mh
{


    public partial class FrmMain : Form
    {
        bool revStatus = true;
        public static IntPtr current_mh = IntPtr.Zero;
        bool saveDB = false;
        //多线程安全
        static BlockingCollection<LoadDll.MhMsg> mhMsgQue = new BlockingCollection<LoadDll.MhMsg>(1000);
        Thread loopMsg;
        //显示过滤
        Dictionary<int, model.MhxyType> mapFilert = new Dictionary<int, model.MhxyType>();
        //业务处理器
        Dictionary<IntPtr, biz.Execute> mh_excute = new Dictionary<IntPtr, biz.Execute>();

        public FrmMain()
        {
            InitializeComponent();
        }
        /// <summary>
        /// 判断数据包是否显示
        /// </summary>
        /// <param name="data"></param>
        /// <param name="recvType"></param>
        /// <returns>默认显示，过滤已配置的数据包</returns>
        public bool FilertMsgData(byte[] data, int t)
        {

            int recvType = data[0];
            //存在处理判断规则
            if (mapFilert.ContainsKey(recvType))
            {
                model.MhxyType mt = mapFilert[recvType];
                if (mt.IsShow == 1)
                {
                    return false;
                }
                string[] rule = mt.filertRule.Split(']');
                //获取规则组
                for (int i = 0; i < rule.Length; i++)
                {
                    if (rule[i].Trim() == "")
                    {
                        continue;
                    }
                    //[7=48,6=49]
                    //取出条件匹配是否满足
                    string[] r = rule[i].Replace("[", "").Split(',');
                    bool flag = true;
                    //判断规则里面的条件
                    for (int k = 0; k < r.Length; k++)
                    {

                        string[] ret = r[k].Split('=');
                        int idx = Convert.ToInt32(ret[0]);
                        //处理下标越界
                        if (idx > data.Length - 1)
                        {
                            //中断当前循环，重置标志位
                            flag = true;
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
                        return false;
                    }
                }
            }


            return true;
        }
        /// <summary>
        /// 读取消息
        /// </summary>
        void loopReadMsg()
        {
            while (!mhMsgQue.IsCompleted)

            {
                var res = mhMsgQue.Take();

                recvMsgHandleDel<LoadDll.MhMsg> a = new recvMsgHandleDel<LoadDll.MhMsg>(recvMsgHandle);
                Invoke(a, res);
                //recvMsgHandle(res);
            }
        }
        //申明一个委托对象
        public delegate void recvMsgHandleDel<in T>(T t);
        /// <summary>
        /// 回复消息处理
        /// </summary>
        /// <param name="m"></param>
        public void recvMsgHandle(LoadDll.MhMsg m)
        {

            //来源窗口
            IntPtr formWind = (IntPtr)m.hwnd;
            //0收，1发
            int type = m.type;

            //取出数据长度大小
            int size = m.body.Length;

            byte[] data = m.body;

            //判断数据包是否显示
            bool ret = true;
            if (cbxFilert.Checked)
            {
                ret = FilertMsgData(data, type);
            }
            //处理回应的包
            mh_excute[formWind].switchExecute(type, data);

            model.MhxyLog ml = new model.MhxyLog(type, data);

            //判断是否入库保存
            if (saveDB)
            {
                //判断是否需要过滤
                if (cbx_db_filert.Checked == true && ret == false) { return; }
                ml.Save(Global.save_table);
            }

            //判断已知分类是否开启，开启后指显示已知分类
            if (cbxShowType.Checked && type != 1)
            {
                if (mapFilert.ContainsKey(data[0]) == false)
                {
                    return;
                }

            }
            if (!ret)
            {
                return;
            }
            //判断是否只显示当前
            if (cbx_current.Checked == true)
            {
                if (formWind.ToInt32() != current_mh.ToInt32())
                {
                    return;
                }
            }
            //判断是否只显示发包
            if (cbx_recv.Checked && type == 0)
            {
                return;
            }
            if (revStatus)
            {
                ListViewItem lt = new ListViewItem();
                //将数据库数据转变成ListView类型的一行数据
                lt.Text = (lv_pkg.Items.Count + 1).ToString();
                lt.SubItems.Add(data.Length.ToString());
                if (type == 0)
                {
                    lt.SubItems.Add("收");
                }
                if (type == 1)
                {
                    lt.SubItems.Add("发");
                }
                if (type == 2)
                {
                    lt.SubItems.Add("替");
                }

                lt.SubItems.Add(mhxy.Common.GetMHName(formWind.ToInt32()));
                lt.SubItems.Add(StringUtil.FormatHexStyle(StringUtil.byteToHexStr(data)));
                //将lt数据添加到listView1控件中
                lv_pkg.Items.Add(lt);
                lv_pkg.EnsureVisible(lv_pkg.Items.Count - 1);

            }

        }
        /// <summary>
        /// 重写窗体消息
        /// </summary>
        /// <param name="message"></param>
        protected override void WndProc(ref Message message)
        {
            switch (message.Msg)
            {
                case 74:

                    IntPtr addr = (IntPtr)message.LParam.ToInt32();
                    //来源窗口
                    IntPtr formWind = message.WParam;
                    //0收，1发
                    int type = StringUtil.GetPtrToInt(addr) != 0 ? 0 : 1;

                    //指针到整数
                    int dataPtr = StringUtil.GetPtrToInt(new IntPtr(addr.ToInt32() + 8));
                    //取出数据长度大小
                    int size = StringUtil.GetPtrToInt(new IntPtr(addr.ToInt32() + 4));
                    byte[] data = StringUtil.GetPtrData((IntPtr)dataPtr, size);
                    //判断数据包是否显示
                    bool ret = FilertMsgData(data, type);
                    //处理回应的包
                    mh_excute[formWind].switchExecute(type, data);

                    model.MhxyLog ml = new model.MhxyLog(type, data);

                    //判断是否入库保存
                    if (saveDB)
                    {
                        //判断是否需要过滤
                        if (cbx_db_filert.Checked == true && ret == false) { return; }
                        ml.Save(Global.save_table);
                    }

                    //判断已知分类是否开启，开启后指显示已知分类
                    if (cbxShowType.Checked && type != 1)
                    {
                        if (mapFilert.ContainsKey(data[0]) == false)
                        {
                            return;
                        }

                    }
                    if (!ret)
                    {
                        return;
                    }
                    //判断是否只显示当前
                    if (cbx_current.Checked == true)
                    {
                        if (formWind.ToInt32() != current_mh.ToInt32())
                        {
                            return;
                        }
                    }
                    //判断是否只显示发包
                    if (cbx_recv.Checked && type == 0)
                    {
                        return;
                    }
                    if (revStatus)
                    {
                        ListViewItem lt = new ListViewItem();
                        //将数据库数据转变成ListView类型的一行数据
                        lt.Text = (lv_pkg.Items.Count + 1).ToString();
                        lt.SubItems.Add(data.Length.ToString());
                        lt.SubItems.Add(type > 0 ? "发" : "收");
                        lt.SubItems.Add(mhxy.Common.GetMHName(formWind.ToInt32()));
                        lt.SubItems.Add(ml.GetBodyFormatHex());
                        //将lt数据添加到listView1控件中
                        lv_pkg.Items.Add(lt);
                        lv_pkg.EnsureVisible(lv_pkg.Items.Count - 1);

                    }

                    break;
            }
            base.WndProc(ref message);
        }

        private void FrmMain_Load(object sender, EventArgs e)
        {
            loopMsg = new Thread(loopReadMsg);
            Control.CheckForIllegalCrossThreadCalls = true;
            //开启控制台输出
            LoadDll.AllocConsole();
 

            loadType();
            lv_pkg.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
            //获得当前窗口句柄
            IntPtr hwnd = this.Handle;
            lab_current_win.Text += hwnd.ToString();


            loopMsg.Start();


        }
        private string HexStringToString(string hs, Encoding encode)
        {
            string strTemp = "";
            byte[] b = new byte[hs.Length / 2];
            for (int i = 0; i < hs.Length / 2; i++)
            {
                strTemp = hs.Substring(i * 2, 2);
                b[i] = Convert.ToByte(strTemp, 16);
            }
            //按照指定编码将字节数组变为字符串
            return encode.GetString(b);
        }

        public void tt()
        {

            //mhxy.CallFunc.ReadAttrsPanel(current_mh.ToInt32());
            mhxy.EventMsg me = new mhxy.EventMsg(current_mh, 100, 100, 121, 71);
            mhxy.CallFunc.way(me);

        }
        private void btn_send_Click(object sender, EventArgs e)
        {


            //Thread rt = new Thread(tt);
            //rt.Start();
            ////mhxy.CallFunc.ReadDialog(current_mh.ToInt32());
            //IntPtr hwnd = LoadDll.FindWindow("WSGAME", null);
            //initMhSystem(hwnd);
            //return;
            string txt = txtSend.Text.Trim();
            if (txt == "" || current_mh == IntPtr.Zero) { return; }
            LoadDll.sendMsg(current_mh, txt);
        }


        private void copyPkg_Click(object sender, EventArgs e)
        {

            string body = lv_pkg.SelectedItems[0].SubItems[4].Text;
            Clipboard.SetDataObject(body.Trim());
        }

        private void button1_Click(object sender, EventArgs e)
        {
            lv_pkg.Items.Clear();
        }

        private void btn_data_type_Click(object sender, EventArgs e)
        {
            if (btn_data_type.Text == "截获全部")
            {
                int ret = LoadDll.subPkg(1, current_mh);

                btn_data_type.Text = "截获指定";

            }
            else
            {


                int ret = LoadDll.subPkg(0, current_mh);
                btn_data_type.Text = "截获全部";

            }
        }

        private void FrmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void lv_pkg_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {

                if (lv_pkg.Items.Count == 0) return;
                cms.Show(lv_pkg, e.Location);//鼠标右键按下弹出菜单
            }
        }

        private void btn_stop_Click(object sender, EventArgs e)
        {
            revStatus = !revStatus;
            if (revStatus == false) { btn_stop.Text = "开始显示"; } else { btn_stop.Text = "停止显示"; }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            //判断数据表
            if (txtTable.Text.Trim() != "")
            {
                Global.save_table = txtTable.Text.Trim();
                if (!SQLiteUtil.ExitTable(Global.save_table))
                {
                    //创建表

                    TableCheck.createLogTable(Global.save_table);
                }
            }

            saveDB = !saveDB;
            if (saveDB)
            {
                btnSave.Text = "停止入库";
            }
            else
            {
                btnSave.Text = "开始入库";
            }

        }

        private void item_str_Click(object sender, EventArgs e)
        {
            FrmMsg frm = new FrmMsg();
            string body = lv_pkg.SelectedItems[0].SubItems[4].Text.Trim();
            frm.hexData = body;
            frm.Show();
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            FrmDataCfg fdc = new FrmDataCfg();
            fdc.frmMain = this;
            fdc.StartPosition = FormStartPosition.CenterScreen;
            fdc.ShowDialog();
        }
        /// <summary>
        /// 加载数据
        /// </summary>
        public void loadType()
        {
            List<model.MhxyType> ret = model.MhxyType.GetAll();
            //装配MAP
            for (int i = 0; i < ret.Count; i++)
            {
                mapFilert[ret[i].TypeId] = ret[i];
            }
        }

        private void btnLoadWin_Click(object sender, EventArgs e)
        {

            cbxWin.Items.Clear();

            Global.addr = null;
            Global.win_list = mhxy.Common.GetMHWindows();
            foreach (var item in Global.win_list)
            {
                Log.WriteLine("句柄[{0}]:{1}", item.Key, item.Value);
                string name = item.Value;
                if (name.Length > 14)
                {
                    name = mhxy.Common.GetMHName(item.Key.ToInt32());
                }
                cbxWin.Items.Add(new util.ComboxItem(name, item.Key.ToString()));
                //开始初始化
                // initMhxy(item.Key);
                initMhSystem(item.Key);
            }
            if (cbxWin.Items.Count <= 0)
            {
                return;
            }
            cbxWin.SelectedIndex = 0;
            string selectValue = ((util.ComboxItem)cbxWin.SelectedItem).Values;
            //设置第一个默认信息加载
            current_mh = new IntPtr(Convert.ToInt32(selectValue));
            lab_mh_win.Text = "梦幻窗口句柄:" + selectValue;

        }







        //委托实现
        public void CallBackHandle(LoadDll.MhMsg m)
        {
            //压入队列先进先出
            byte[] ret = StringUtil.GetPtrData((IntPtr)m.addr, m.len);
            m.body = ret;
            mhMsgQue.Add(m);

        }


        public LoadDll.ResvMhMsg mhMsgCallBack;

        /// <summary>
        /// 初始化游戏
        /// </summary>
        /// <param name="hwnd"></param>
        public void initMhSystem(IntPtr hwnd)
        {
           // string path = "E:\\mh_code\\mh\\mh\\bin\\x86\\Debug\\mhxy_kernel.dll";
            string path = Path.GetFullPath("mhxy_kernel.dll");

            //获取远程进程的函数地址
            mhMsgCallBack = new LoadDll.ResvMhMsg(CallBackHandle);

            //远程调用初始化
           //LoadDll.InitSystemRemoteThread(path, hwnd, this.Handle, mhMsgCallBack);


            //本地初始化
            LoadDll.InitSystemMhxy(path, hwnd, this.Handle, mhMsgCallBack);


            //跳过已经载入的窗口
            if (mh_excute.ContainsKey(hwnd) == false)
            {
                //启动任务处理
                mh_excute[hwnd] = new biz.Execute(hwnd);
            }
            IntPtr pid = IntPtr.Zero;
            //获取窗口句柄
            LoadDll.GetWindowThreadProcessId(hwnd, out pid);
            //放置修改前的保护方式
            Int32 prev = 0;
            //获取进程句柄
            IntPtr hWnd = LoadDll.OpenProcess(0x1F0FFF, false, pid.ToInt32());
            //执行VirtualProtectEx函数
            LoadDll.VirtualProtectEx(hWnd, 0x11000900, 4096, 64, ref prev);
            LoadDll.CloseHandle(hWnd);
            //更新基址
            if (Global.addr == null) { Global.addr = new mhxy.AddrManager().loadAddr(hwnd.ToInt32()); }

            //加载用户配置
            string wName = mhxy.Common.GetMHName(hwnd.ToInt32());
            if (wName != "mh")
            {
                //读取用户ID加载配置
                int peopleID = Global.addr.GetPeopleID(hwnd.ToInt32());
                Global.mh_cfg[hwnd] = model.MhxyConfig.GetID(peopleID);
                Log.WriteLine("配置加载：{0}", peopleID);
            }


        }
        /// <summary>
        /// 初始化游戏
        /// </summary>
        /// <param name="hwnd"></param>
        public void initMhxy(IntPtr hwnd)
        {
            //获取梦幻西游句柄
            btn_send.Enabled = true;
            //初始化基本信息
            LoadDll.initmhDll(hwnd.ToInt32(), this.Handle.ToInt32());
            //bug 需要两次
            LoadDll.subPkg(0, hwnd);
            LoadDll.subPkg(1, hwnd);




            //跳过已经载入的窗口
            if (mh_excute.ContainsKey(hwnd) == false)
            {
                //启动任务处理
                mh_excute[hwnd] = new biz.Execute(hwnd);
            }
            IntPtr pid = IntPtr.Zero;
            //获取窗口句柄
            LoadDll.GetWindowThreadProcessId(hwnd, out pid);
            //放置修改前的保护方式
            Int32 prev = 0;
            //获取进程句柄
            IntPtr hWnd = LoadDll.OpenProcess(0x1F0FFF, false, pid.ToInt32());
            //执行VirtualProtectEx函数
            LoadDll.VirtualProtectEx(hWnd, 0x11000900, 4096, 64, ref prev);
            LoadDll.CloseHandle(hWnd);
            //更新基址
            if (Global.addr == null) { Global.addr = new mhxy.AddrManager().loadAddr(hwnd.ToInt32()); }

            //加载用户配置
            string wName = mhxy.Common.GetMHName(hwnd.ToInt32());
            if (wName != "mh")
            {
                //读取用户ID加载配置
                int peopleID = Global.addr.GetPeopleID(hwnd.ToInt32());
                Global.mh_cfg[hwnd] = model.MhxyConfig.GetID(peopleID);
                Log.WriteLine("配置加载：{0}", peopleID);
            }

        }

        private void btnQh_Click(object sender, EventArgs e)
        {
            string selectValue = ((util.ComboxItem)cbxWin.SelectedItem).Values;
            //设置默认窗口
            current_mh = new IntPtr(Convert.ToInt32(selectValue));
            //切换到截获全部
            //bug 需要两次
            LoadDll.subPkg(0, current_mh);
            LoadDll.subPkg(1, current_mh);
            btn_data_type.Text = "截获指定";
            lab_mh_win.Text = "梦幻窗口句柄:" + selectValue;
        }

        private void btnAccountCfg_Click(object sender, EventArgs e)
        {
            if (current_mh == IntPtr.Zero)
            {
                MessageBox.Show("请打开游戏窗口");
                return;
            }
            int peopleID = Global.addr.GetPeopleID(current_mh.ToInt32());
            if (peopleID == 0)
            {
                MessageBox.Show("进入游戏后设置");
                return;
            }
            FrmMenu fm = new FrmMenu();
            fm.hwnd = current_mh;
            fm.people_id = peopleID;
            fm.map_id = Global.addr.GetMapID(current_mh.ToInt32());
            fm.ShowDialog();

        }

        private void cbx_auto_blam_CheckedChanged(object sender, EventArgs e)
        {

            if (!mh_excute.ContainsKey(current_mh))
            {


                cbx_auto_blam.Checked = false;
                return;
            }
            int map_id = Global.addr.GetMapID(current_mh.ToInt32());



            if (mh_excute[current_mh].am == null)
            {
                mh_excute[current_mh].am = new biz.AutoMove(current_mh.ToInt32(), 1, 163, 78, map_id, mh_excute[current_mh]);
            }
            if (cbx_auto_blam.Checked)
            {
                if (!Global.map_reg.ContainsKey(map_id) || Global.map_reg[map_id].Count == 0)
                {
                    cbx_auto_blam.Checked = false;
                    MessageBox.Show("请配置地图区域");
                    return;
                }

                mh_excute[current_mh].am.Run();
            }
            else
            {
                mh_excute[current_mh].am.Stop();
            }
        }

        private void btn_test_Click(object sender, EventArgs e)
        {
           
            FrmFunTest ft = new FrmFunTest();
            ft.hwnd = current_mh.ToInt32();
            if (ft.hwnd == 0)
            {
                MessageBox.Show("游戏未载入");
                return;
            }
            ft.ex = mh_excute[current_mh];
            ft.ShowDialog();
        }
 
    }
}
