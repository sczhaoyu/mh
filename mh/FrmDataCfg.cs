using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace mh
{
    public partial class FrmDataCfg : Form
    {
        public FrmMain frmMain;
        public FrmDataCfg()
        {
            InitializeComponent();
        }

        private void FrmDataCfg_Load(object sender, EventArgs e)
        {
          
            lv_type.CheckBoxes = true;
          
            loadDB();

        }
        //加载数据
        private void loadDB()
        {

            List<model.MhxyType> ret = model.MhxyType.GetAll();
            for (int i = 0; i < ret.Count; i++)
            {

                ListViewItem lt = new ListViewItem();
                //将数据库数据转变成ListView类型的一行数据
                lt.Text = ret[i].prefix;
                lt.SubItems.Add(ret[i].Name);
               
                lt.SubItems.Add(ret[i].IsShow == 0 ? "过滤" : "屏蔽");
                if (ret[i].IsShow == 0)
                {
                    lt.Checked = true;
                }
                lt.SubItems.Add(ret[i].filertRule);
                lt.SubItems.Add(ret[i].TypeId.ToString());
                //将lt数据添加到listView1控件中
                lv_type.Items.Add(lt);

            }

            lv_type.ItemChecked += lv_typeCheckChange;
        }



        private void lv_typeCheckChange(object sender, ItemCheckedEventArgs e)
        {
            int idx = e.Item.Index;
            int k = e.Item.Checked == true ? 0 : 1;
            string id = lv_type.Items[idx].SubItems[4].Text.ToString();
            model.MhxyType.UpdateShow(Convert.ToInt32(id), k);
            frmMain.loadType();

        }

        private void btn_add_Click(object sender, EventArgs e)
        {


            if (txtId.Text.Trim() == "" || txtName.Text.Trim() == "") { return; }



            model.MhxyType mt = new model.MhxyType();
            mt.TypeId = Convert.ToInt32(txtId.Text.Trim());
            mt.Name = txtName.Text.Trim();
            mt.IsShow = cbxShow.Checked == true ? 1 : 0;
            mt.prefix = StringUtil.FormatIntToHexStyle(mt.TypeId,false);
            mt.filertRule = txt_rule.Text.Trim();
            bool exit = model.MhxyType.CheckTypeId(mt.TypeId);
            if (exit)
            {
                MessageBox.Show("已添加！");
                return;
            }
            model.MhxyType.Add(mt);
            lv_type.Items.Clear();
            lv_type.ItemChecked -= lv_typeCheckChange;
            loadDB();
            frmMain.loadType();
        }

        private void txtId_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 0x20) e.KeyChar = (char)0;  //禁止空格键
            if ((e.KeyChar == 0x2D) && (((TextBox)sender).Text.Length == 0)) return;   //处理负数
            if (e.KeyChar > 0x20)
            {
                try
                {
                    double.Parse(((TextBox)sender).Text + e.KeyChar.ToString());
                }
                catch
                {
                    e.KeyChar = (char)0;   //处理非法字符
                }
            }
        }


    }
}
