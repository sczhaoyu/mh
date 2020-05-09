using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace mh
{
    public partial class FrmMenu : Form
    {
        public IntPtr hwnd = IntPtr.Zero;
        public int people_id = 0;
        model.MhxyConfig mhcfg = null;
        public int map_id = 38;
        public FrmMenu()
        {
            InitializeComponent();
        }

        private void FrmMenu_Load(object sender, EventArgs e)
        {
            //lv_map.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
            this.loadMap();
            //处理配置好的值
            if (Global.mh_cfg.ContainsKey(hwnd))
            {
                mhcfg = Global.mh_cfg[hwnd];
                //赋值开始
                txt_hp.Text = mhcfg.hp.ToString();
                txt_mp.Text = mhcfg.mp.ToString();
                cbx_hm.Checked = mhcfg.hp_mp_auto == 1;

                txt_bb_hp.Text = mhcfg.bb_hp.ToString();
                txt_bb_mp.Text = mhcfg.bb_mp.ToString();
                cbx_bb_hm.Checked = mhcfg.bb_hp_mp_auto == 1;
                cbx_skill_auto.Checked = mhcfg.skill_auto == 1;
            }
            else
            {
                mhcfg = new model.MhxyConfig();
            }
            if (people_id > 0)
            {

                mhcfg.id = people_id;
            }

            List<model.MhxySkill> skills = model.MhxySkill.GetAll();

            int people_idx = -1;
            int bb_idx = -1;
            for (int i = 0; i < skills.Count; i++)
            {
                model.MhxySkill s = skills[i];


              

                //人物技能
                if (s.SkillType == 0)
                {
                    cbx_skill.Items.Add(new util.ComboxItem(s.Name, s.SkillId.ToString()));

                    if (mhcfg.skill == s.SkillId) { people_idx = cbx_skill.Items.Count-1; }
                }
                //宝宝技能
                if (s.SkillType == 1)
                {
                    cbx_bb_skill.Items.Add(new util.ComboxItem(s.Name, s.SkillId.ToString()));
                    if (mhcfg.bb_skill == s.SkillId)
                    {
                        bb_idx = cbx_bb_skill.Items.Count - 1;
                    }

                }
                //共有技能
                if (s.SkillType == -1)
                {
                    cbx_skill.Items.Add(new util.ComboxItem(s.Name, s.SkillId.ToString()));
                    cbx_bb_skill.Items.Add(new util.ComboxItem(s.Name, s.SkillId.ToString()));

                }


            }
            cbx_bb_skill.SelectedIndex = 0;
            cbx_skill.SelectedIndex = 0;
            if (bb_idx > -1)
            {
                cbx_bb_skill.SelectedIndex = bb_idx;
            }
            if (people_idx > -1)
            {
                cbx_skill.SelectedIndex = people_idx;
            }

        }

        private void btn_update_Click(object sender, EventArgs e)
        {


            //人物自动加蓝血
            mhcfg.hp_mp_auto = 0;
            if (cbx_hm.Checked)
            {
                mhcfg.hp_mp_auto = 1;
            }

            mhcfg.hp = Convert.ToInt32(txt_hp.Text.Trim());
            mhcfg.mp = Convert.ToInt32(txt_mp.Text.Trim());
            //宝宝设置
            mhcfg.bb_hp_mp_auto = 0;
            if (cbx_bb_hm.Checked)
            {
                mhcfg.bb_hp_mp_auto = 1;
            }
            mhcfg.bb_hp = Convert.ToInt32(txt_bb_hp.Text.Trim());
            mhcfg.bb_mp = Convert.ToInt32(txt_bb_mp.Text.Trim());
            //===========================================================
            //人物技能设置
            mhcfg.bb_skill_auto = 0;
            mhcfg.skill_auto = 0;
            if (cbx_skill_auto.Checked)
            {
                mhcfg.bb_skill_auto = 1;
                mhcfg.skill_auto = 1;

                mhcfg.skill = Convert.ToInt32(((util.ComboxItem)cbx_skill.SelectedItem).Values);
                mhcfg.bb_skill = Convert.ToInt32(((util.ComboxItem)cbx_bb_skill.SelectedItem).Values);
            }
            //添加技能
            Global.mh_cfg[hwnd] = mhcfg;
            model.MhxyConfig.Delete(mhcfg.id);
            model.MhxyConfig.Save(mhcfg);
            MessageBox.Show("修改完成");
        }
        public void loadMap()
        {
            lv_map.Items.Clear();
            List<model.MhxyMapRegion> ret = model.MhxyMapRegion.GetMapID(map_id);
            //数据刷新

            Global.map_reg[map_id] = ret;


            for (int i = 0; i < ret.Count; i++)
            {
                ListViewItem lt = new ListViewItem();
                //将数据库数据转变成ListView类型的一行数据
                lt.Text = ret[i].name;
                lt.SubItems.Add(ret[i].x.ToString());
                lt.SubItems.Add(ret[i].y.ToString());
                lt.SubItems.Add(ret[i].max_x.ToString());
                lt.SubItems.Add(ret[i].max_y.ToString());
                lt.SubItems.Add(ret[i].id.ToString());
                //将lt数据添加到listView1控件中
                lv_map.Items.Add(lt);
                lv_map.EnsureVisible(lv_map.Items.Count - 1);
            }
        }
        private void btn_add_Click(object sender, EventArgs e)
        {
            model.MhxyMapRegion m = new model.MhxyMapRegion();
            m.x = Convert.ToInt32(txt_left_x.Text.Trim());
            m.y = Convert.ToInt32(txt_left_y.Text.Trim());

            m.max_x = Convert.ToInt32(txt_right_x.Text.Trim());

            m.max_y = Convert.ToInt32(txt_right_y.Text.Trim());

            m.name = txt_map_name.Text.Trim();
            m.map_id = map_id;
            if (m.max_x < m.x)
            {
                MessageBox.Show("右下X必须大于左上X");
                return;
            }
            if (m.y < m.max_y)
            {
                MessageBox.Show("左上Y须大于右下Y");
                return;
            }
            m.Save();

            loadMap();
            MessageBox.Show("保存成功");
        }

        private void lv_map_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {

                if (lv_map.Items.Count == 0) return;
                cmsd_map.Show(lv_map, e.Location);//鼠标右键按下弹出菜单
            }
        }

        private void tsm_delete_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(lv_map.SelectedItems[0].SubItems[5].Text);
            model.MhxyMapRegion.Delete(id);
            loadMap();
        }

        private void txt_hp_KeyPress(object sender, KeyPressEventArgs e)
        {
            filertTxtNumber(sender, e);
        }
        public void filertTxtNumber(object sender, KeyPressEventArgs e)
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

        private void txt_mp_KeyPress(object sender, KeyPressEventArgs e)
        {
            filertTxtNumber(sender, e);
        }

        private void txt_left_x_KeyPress(object sender, KeyPressEventArgs e)
        {
            filertTxtNumber(sender, e);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MessageBox.Show(cbx_skill.SelectedIndex.ToString());
        }
    }
}
