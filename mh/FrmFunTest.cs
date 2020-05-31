using mh.mhxy;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace mh
{
    public partial class FrmFunTest : Form
    {
        public int hwnd;
        public biz.Execute ex;
        public FrmFunTest()
        {
            InitializeComponent();
        }
        public void toexit() {
            int x = Convert.ToInt32(txt_exit_x.Text.Trim());
            int y = Convert.ToInt32(txt_exit_y.Text.Trim());
            mhxy.CallFunc.MhxyToExit(hwnd, x, y);
        }
        private void button1_Click(object sender, EventArgs e)
        {

            Thread th = new Thread(toexit);
            th.Start();
        }

        private void txt_exit_x_KeyPress(object sender, KeyPressEventArgs e)
        {
            StringUtil.FilertTxtNumber(sender, e);
        }

        private void btn_biao_Click(object sender, EventArgs e)
        {
            int start = Convert.ToInt32(txt_start.Text.Trim());
            int end = Convert.ToInt32(txt_end.Text.Trim());
            ex.bjtask.Run(start, true, end);


        }

        private void btn_bag_Click(object sender, EventArgs e)
        {
            List<bean.Goods> list = mhxy.CallFunc.itBag(hwnd);
            for (int i = 0; i < list.Count; i++)
            {
                Log.WriteLine("位置：{0}物品编号：{1}—物品类别：{2}—物品数量：{3}", list[i].idx, list[i].id, list[i].type, list[i].count);
            }
        }

        private void btn_x_add_Click(object sender, EventArgs e)
        {
            txt_exit_x.Text = (Convert.ToInt32(txt_exit_x.Text.Trim()) + 1).ToString();
            reSetMouse();
        }

        private void btn_x_sub_Click(object sender, EventArgs e)
        {
            txt_exit_x.Text = (Convert.ToInt32(txt_exit_x.Text.Trim()) - 1).ToString();
            reSetMouse();
        }

        private void btn_y_add_Click(object sender, EventArgs e)
        {
            txt_exit_y.Text = (Convert.ToInt32(txt_exit_y.Text.Trim()) + 1).ToString();
            reSetMouse();
        }

        private void btn_y_sub_Click(object sender, EventArgs e)
        {
            txt_exit_y.Text = (Convert.ToInt32(txt_exit_y.Text.Trim()) - 1).ToString();
            reSetMouse();
        }
        public void reSetMouse()
        {

            int x = Convert.ToInt32(txt_exit_x.Text.Trim());
            int y = Convert.ToInt32(txt_exit_y.Text.Trim());

            int c_x = LoadDll.ReadHwndMemoryOffsetValue((IntPtr)hwnd, Global.addr.MapAddr, 0xCC);
            int c_y = LoadDll.ReadHwndMemoryOffsetValue((IntPtr)hwnd, Global.addr.MapAddr, 0xD0);

            //屏幕宽高
            int[] scr = Global.addr.GetMhxyScreen(hwnd);

            //目的地址坐标
            int s_x = x * 20 + 10;
            int s_y = Global.addr.GetMapY(hwnd) - ((y * 20) + 10);

            int x_min = c_x - scr[0] / 2;
            int x_max = c_x + scr[0] / 2;

            int y_min = c_y - scr[1] / 2;
            int y_max = c_y + scr[1] / 2;

            x = s_x - x_min;
            y = s_y - y_min;


            LoadDll.WriteMemoryHwndValue((IntPtr)hwnd, Global.addr.bX, BitConverter.GetBytes(x));
            LoadDll.WriteMemoryHwndValue((IntPtr)hwnd, new IntPtr(Global.addr.bX + 4).ToInt32(), BitConverter.GetBytes(y));
        }

        private void btn_show_ui_Click(object sender, EventArgs e)
        {
            mhxy.CallFunc.UIShowStatus(hwnd, true);
        }

        private void btn_changan_Click(object sender, EventArgs e)
        {
            ex.bjtask.toChangAn();
        }

        private void btn_movse_Click(object sender, EventArgs e)
        {
            int x = Convert.ToInt32(mouse_movX.Text.Trim());
            int y = Convert.ToInt32(mouse_movY.Text.Trim());
            CallFunc.MouseMove(hwnd,x,y);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            int[] xy = Global.addr.getPeopleXY(hwnd);
            txt_people_X.Text = xy[0].ToString();
            txt_people_Y.Text = xy[1].ToString();
        }
    }
}
