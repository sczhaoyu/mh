namespace mh
{
    partial class FrmMenu
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmMenu));
            this.tbg = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.btn_update = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label4 = new System.Windows.Forms.Label();
            this.cbx_bb_skill = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cbx_skill = new System.Windows.Forms.ComboBox();
            this.cbx_skill_auto = new System.Windows.Forms.CheckBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.cbx_bb_hm = new System.Windows.Forms.CheckBox();
            this.txt_bb_mp = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txt_bb_hp = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.cbx_hm = new System.Windows.Forms.CheckBox();
            this.txt_mp = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txt_hp = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.label13 = new System.Windows.Forms.Label();
            this.txt_map_name = new System.Windows.Forms.TextBox();
            this.txt_right_y = new System.Windows.Forms.TextBox();
            this.txt_right_x = new System.Windows.Forms.TextBox();
            this.txt_left_y = new System.Windows.Forms.TextBox();
            this.txt_left_x = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.btn_add = new System.Windows.Forms.Button();
            this.lv_map = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.id = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.cmsd_map = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsm_delete = new System.Windows.Forms.ToolStripMenuItem();
            this.tbg.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.cmsd_map.SuspendLayout();
            this.SuspendLayout();
            // 
            // tbg
            // 
            this.tbg.Controls.Add(this.tabPage1);
            this.tbg.Controls.Add(this.tabPage2);
            this.tbg.Location = new System.Drawing.Point(12, 8);
            this.tbg.Name = "tbg";
            this.tbg.SelectedIndex = 0;
            this.tbg.Size = new System.Drawing.Size(587, 476);
            this.tbg.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.btn_update);
            this.tabPage1.Controls.Add(this.groupBox2);
            this.tabPage1.Controls.Add(this.groupBox1);
            this.tabPage1.Location = new System.Drawing.Point(4, 28);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(579, 444);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "战斗设置";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // btn_update
            // 
            this.btn_update.Location = new System.Drawing.Point(437, 365);
            this.btn_update.Name = "btn_update";
            this.btn_update.Size = new System.Drawing.Size(113, 44);
            this.btn_update.TabIndex = 6;
            this.btn_update.Text = "确认设置";
            this.btn_update.UseVisualStyleBackColor = true;
            this.btn_update.Click += new System.EventHandler(this.btn_update_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.cbx_bb_skill);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.cbx_skill);
            this.groupBox2.Controls.Add(this.cbx_skill_auto);
            this.groupBox2.Location = new System.Drawing.Point(19, 219);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(531, 102);
            this.groupBox2.TabIndex = 5;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "技能设置";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(234, 43);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 18);
            this.label4.TabIndex = 7;
            this.label4.Text = "宝宝:";
            // 
            // cbx_bb_skill
            // 
            this.cbx_bb_skill.FormattingEnabled = true;
            this.cbx_bb_skill.Location = new System.Drawing.Point(288, 40);
            this.cbx_bb_skill.Name = "cbx_bb_skill";
            this.cbx_bb_skill.Size = new System.Drawing.Size(121, 26);
            this.cbx_bb_skill.TabIndex = 6;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(20, 43);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 18);
            this.label3.TabIndex = 5;
            this.label3.Text = "人物:";
            // 
            // cbx_skill
            // 
            this.cbx_skill.FormattingEnabled = true;
            this.cbx_skill.Location = new System.Drawing.Point(79, 40);
            this.cbx_skill.Name = "cbx_skill";
            this.cbx_skill.Size = new System.Drawing.Size(121, 26);
            this.cbx_skill.TabIndex = 5;
            // 
            // cbx_skill_auto
            // 
            this.cbx_skill_auto.AutoSize = true;
            this.cbx_skill_auto.Checked = true;
            this.cbx_skill_auto.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbx_skill_auto.Location = new System.Drawing.Point(418, 44);
            this.cbx_skill_auto.Name = "cbx_skill_auto";
            this.cbx_skill_auto.Size = new System.Drawing.Size(106, 22);
            this.cbx_skill_auto.TabIndex = 4;
            this.cbx_skill_auto.Text = "自动释放";
            this.cbx_skill_auto.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.cbx_bb_hm);
            this.groupBox1.Controls.Add(this.txt_bb_mp);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.txt_bb_hp);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.cbx_hm);
            this.groupBox1.Controls.Add(this.txt_mp);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.txt_hp);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(19, 21);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(531, 157);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "蓝血补充";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(11, 101);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(62, 18);
            this.label8.TabIndex = 11;
            this.label8.Text = "宝宝=>";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(11, 35);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(62, 18);
            this.label7.TabIndex = 10;
            this.label7.Text = "人物=>";
            // 
            // cbx_bb_hm
            // 
            this.cbx_bb_hm.AutoSize = true;
            this.cbx_bb_hm.Checked = true;
            this.cbx_bb_hm.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbx_bb_hm.Location = new System.Drawing.Point(418, 102);
            this.cbx_bb_hm.Name = "cbx_bb_hm";
            this.cbx_bb_hm.Size = new System.Drawing.Size(106, 22);
            this.cbx_bb_hm.TabIndex = 9;
            this.cbx_bb_hm.Text = "自动补充";
            this.cbx_bb_hm.UseVisualStyleBackColor = true;
            // 
            // txt_bb_mp
            // 
            this.txt_bb_mp.Location = new System.Drawing.Point(288, 100);
            this.txt_bb_mp.Name = "txt_bb_mp";
            this.txt_bb_mp.Size = new System.Drawing.Size(103, 28);
            this.txt_bb_mp.TabIndex = 8;
            this.txt_bb_mp.Text = "100";
            this.txt_bb_mp.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txt_mp_KeyPress);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(255, 104);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(35, 18);
            this.label5.TabIndex = 7;
            this.label5.Text = "蓝:";
            // 
            // txt_bb_hp
            // 
            this.txt_bb_hp.Location = new System.Drawing.Point(126, 98);
            this.txt_bb_hp.Name = "txt_bb_hp";
            this.txt_bb_hp.Size = new System.Drawing.Size(103, 28);
            this.txt_bb_hp.TabIndex = 6;
            this.txt_bb_hp.Text = "100";
            this.txt_bb_hp.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txt_mp_KeyPress);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(94, 101);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(35, 18);
            this.label6.TabIndex = 5;
            this.label6.Text = "红:";
            // 
            // cbx_hm
            // 
            this.cbx_hm.AutoSize = true;
            this.cbx_hm.Checked = true;
            this.cbx_hm.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbx_hm.Location = new System.Drawing.Point(418, 38);
            this.cbx_hm.Name = "cbx_hm";
            this.cbx_hm.Size = new System.Drawing.Size(106, 22);
            this.cbx_hm.TabIndex = 4;
            this.cbx_hm.Text = "自动补充";
            this.cbx_hm.UseVisualStyleBackColor = true;
            // 
            // txt_mp
            // 
            this.txt_mp.Location = new System.Drawing.Point(288, 32);
            this.txt_mp.Name = "txt_mp";
            this.txt_mp.Size = new System.Drawing.Size(103, 28);
            this.txt_mp.TabIndex = 3;
            this.txt_mp.Text = "100";
            this.txt_mp.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txt_mp_KeyPress);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(255, 36);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(35, 18);
            this.label2.TabIndex = 2;
            this.label2.Text = "蓝:";
            // 
            // txt_hp
            // 
            this.txt_hp.Location = new System.Drawing.Point(126, 30);
            this.txt_hp.Name = "txt_hp";
            this.txt_hp.Size = new System.Drawing.Size(103, 28);
            this.txt_hp.TabIndex = 1;
            this.txt_hp.Text = "100";
            this.txt_hp.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txt_hp_KeyPress);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(94, 33);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 18);
            this.label1.TabIndex = 0;
            this.label1.Text = "红:";
            // 
            // tabPage2
            // 
            this.tabPage2.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.tabPage2.Controls.Add(this.label13);
            this.tabPage2.Controls.Add(this.txt_map_name);
            this.tabPage2.Controls.Add(this.txt_right_y);
            this.tabPage2.Controls.Add(this.txt_right_x);
            this.tabPage2.Controls.Add(this.txt_left_y);
            this.tabPage2.Controls.Add(this.txt_left_x);
            this.tabPage2.Controls.Add(this.label12);
            this.tabPage2.Controls.Add(this.label11);
            this.tabPage2.Controls.Add(this.label10);
            this.tabPage2.Controls.Add(this.label9);
            this.tabPage2.Controls.Add(this.btn_add);
            this.tabPage2.Controls.Add(this.lv_map);
            this.tabPage2.Location = new System.Drawing.Point(4, 28);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(579, 444);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "地图配置";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(414, 25);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(44, 18);
            this.label13.TabIndex = 11;
            this.label13.Text = "名称";
            // 
            // txt_map_name
            // 
            this.txt_map_name.Location = new System.Drawing.Point(473, 20);
            this.txt_map_name.Name = "txt_map_name";
            this.txt_map_name.Size = new System.Drawing.Size(100, 28);
            this.txt_map_name.TabIndex = 10;
            // 
            // txt_right_y
            // 
            this.txt_right_y.Location = new System.Drawing.Point(282, 80);
            this.txt_right_y.Name = "txt_right_y";
            this.txt_right_y.Size = new System.Drawing.Size(100, 28);
            this.txt_right_y.TabIndex = 9;
            this.txt_right_y.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txt_left_x_KeyPress);
            // 
            // txt_right_x
            // 
            this.txt_right_x.Location = new System.Drawing.Point(91, 80);
            this.txt_right_x.Name = "txt_right_x";
            this.txt_right_x.Size = new System.Drawing.Size(100, 28);
            this.txt_right_x.TabIndex = 8;
            this.txt_right_x.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txt_left_x_KeyPress);
            // 
            // txt_left_y
            // 
            this.txt_left_y.Location = new System.Drawing.Point(282, 20);
            this.txt_left_y.Name = "txt_left_y";
            this.txt_left_y.Size = new System.Drawing.Size(100, 28);
            this.txt_left_y.TabIndex = 7;
            this.txt_left_y.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txt_left_x_KeyPress);
            // 
            // txt_left_x
            // 
            this.txt_left_x.Location = new System.Drawing.Point(91, 20);
            this.txt_left_x.Name = "txt_left_x";
            this.txt_left_x.Size = new System.Drawing.Size(100, 28);
            this.txt_left_x.TabIndex = 6;
            this.txt_left_x.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txt_left_x_KeyPress);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(210, 85);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(53, 18);
            this.label12.TabIndex = 5;
            this.label12.Text = "右下Y";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(19, 85);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(53, 18);
            this.label11.TabIndex = 4;
            this.label11.Text = "右下X";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(210, 25);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(53, 18);
            this.label10.TabIndex = 3;
            this.label10.Text = "左上Y";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(19, 25);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(53, 18);
            this.label9.TabIndex = 2;
            this.label9.Text = "左上X";
            // 
            // btn_add
            // 
            this.btn_add.Location = new System.Drawing.Point(473, 75);
            this.btn_add.Name = "btn_add";
            this.btn_add.Size = new System.Drawing.Size(97, 39);
            this.btn_add.TabIndex = 1;
            this.btn_add.Text = "添加";
            this.btn_add.UseVisualStyleBackColor = true;
            this.btn_add.Click += new System.EventHandler(this.btn_add_Click);
            // 
            // lv_map
            // 
            this.lv_map.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader4,
            this.columnHeader5,
            this.id});
            this.lv_map.FullRowSelect = true;
            this.lv_map.Location = new System.Drawing.Point(3, 128);
            this.lv_map.Name = "lv_map";
            this.lv_map.Size = new System.Drawing.Size(570, 310);
            this.lv_map.TabIndex = 0;
            this.lv_map.UseCompatibleStateImageBehavior = false;
            this.lv_map.View = System.Windows.Forms.View.Details;
            this.lv_map.MouseClick += new System.Windows.Forms.MouseEventHandler(this.lv_map_MouseClick);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "名称";
            this.columnHeader1.Width = 84;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "左上X";
            this.columnHeader2.Width = 68;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "左上Y";
            this.columnHeader3.Width = 68;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "右下X";
            this.columnHeader4.Width = 68;
            // 
            // columnHeader5
            // 
            this.columnHeader5.Text = "右下Y";
            this.columnHeader5.Width = 68;
            // 
            // id
            // 
            this.id.Width = 1;
            // 
            // cmsd_map
            // 
            this.cmsd_map.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.cmsd_map.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsm_delete});
            this.cmsd_map.Name = "cmsd_map";
            this.cmsd_map.Size = new System.Drawing.Size(153, 32);
            // 
            // tsm_delete
            // 
            this.tsm_delete.Name = "tsm_delete";
            this.tsm_delete.Size = new System.Drawing.Size(152, 28);
            this.tsm_delete.Text = "删除此条";
            this.tsm_delete.Click += new System.EventHandler(this.tsm_delete_Click);
            // 
            // FrmMenu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(614, 505);
            this.Controls.Add(this.tbg);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmMenu";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "人物配置";
            this.Load += new System.EventHandler(this.FrmMenu_Load);
            this.tbg.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.cmsd_map.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tbg;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txt_hp;
        private System.Windows.Forms.TextBox txt_mp;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox cbx_hm;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.CheckBox cbx_skill_auto;
        private System.Windows.Forms.ComboBox cbx_skill;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cbx_bb_skill;
        private System.Windows.Forms.CheckBox cbx_bb_hm;
        private System.Windows.Forms.TextBox txt_bb_mp;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txt_bb_hp;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button btn_update;
        private System.Windows.Forms.ListView lv_map;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.ColumnHeader columnHeader5;
        private System.Windows.Forms.Button btn_add;
        private System.Windows.Forms.TextBox txt_right_y;
        private System.Windows.Forms.TextBox txt_right_x;
        private System.Windows.Forms.TextBox txt_left_y;
        private System.Windows.Forms.TextBox txt_left_x;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ContextMenuStrip cmsd_map;
        private System.Windows.Forms.ToolStripMenuItem tsm_delete;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox txt_map_name;
        private System.Windows.Forms.ColumnHeader id;
    }
}