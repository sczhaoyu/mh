namespace mh
{
    partial class FrmMain
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmMain));
            this.txtSend = new System.Windows.Forms.TextBox();
            this.btn_send = new System.Windows.Forms.Button();
            this.lab_current_win = new System.Windows.Forms.Label();
            this.lab_mh_win = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.lv_pkg = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.cms = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.copyPkg = new System.Windows.Forms.ToolStripMenuItem();
            this.item_str = new System.Windows.Forms.ToolStripMenuItem();
            this.button1 = new System.Windows.Forms.Button();
            this.btn_data_type = new System.Windows.Forms.Button();
            this.btn_stop = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.cbx_db_filert = new System.Windows.Forms.CheckBox();
            this.btnLoad = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.txtTable = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cbxFilert = new System.Windows.Forms.CheckBox();
            this.cbxShowType = new System.Windows.Forms.CheckBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.btnQh = new System.Windows.Forms.Button();
            this.btnLoadWin = new System.Windows.Forms.Button();
            this.cbxWin = new System.Windows.Forms.ComboBox();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.cbx_auto_hm = new System.Windows.Forms.CheckBox();
            this.checkBox7 = new System.Windows.Forms.CheckBox();
            this.checkBox6 = new System.Windows.Forms.CheckBox();
            this.checkBox5 = new System.Windows.Forms.CheckBox();
            this.btnAccountCfg = new System.Windows.Forms.Button();
            this.checkBox4 = new System.Windows.Forms.CheckBox();
            this.button3 = new System.Windows.Forms.Button();
            this.checkBox3 = new System.Windows.Forms.CheckBox();
            this.checkBox2 = new System.Windows.Forms.CheckBox();
            this.cbx_auto_blam = new System.Windows.Forms.CheckBox();
            this.cbx_current = new System.Windows.Forms.CheckBox();
            this.cbx_recv = new System.Windows.Forms.CheckBox();
            this.btn_test = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.cms.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtSend
            // 
            this.txtSend.Location = new System.Drawing.Point(318, 955);
            this.txtSend.Multiline = true;
            this.txtSend.Name = "txtSend";
            this.txtSend.Size = new System.Drawing.Size(855, 42);
            this.txtSend.TabIndex = 0;
            // 
            // btn_send
            // 
            this.btn_send.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btn_send.Location = new System.Drawing.Point(1179, 955);
            this.btn_send.Name = "btn_send";
            this.btn_send.Size = new System.Drawing.Size(141, 42);
            this.btn_send.TabIndex = 1;
            this.btn_send.Text = "发包";
            this.btn_send.UseVisualStyleBackColor = true;
            this.btn_send.Click += new System.EventHandler(this.btn_send_Click);
            // 
            // lab_current_win
            // 
            this.lab_current_win.AutoSize = true;
            this.lab_current_win.Location = new System.Drawing.Point(22, 41);
            this.lab_current_win.Name = "lab_current_win";
            this.lab_current_win.Size = new System.Drawing.Size(125, 18);
            this.lab_current_win.TabIndex = 2;
            this.lab_current_win.Text = "当前窗口句柄:";
            // 
            // lab_mh_win
            // 
            this.lab_mh_win.AutoSize = true;
            this.lab_mh_win.Location = new System.Drawing.Point(22, 89);
            this.lab_mh_win.Name = "lab_mh_win";
            this.lab_mh_win.Size = new System.Drawing.Size(125, 18);
            this.lab_mh_win.TabIndex = 3;
            this.lab_mh_win.Text = "梦幻窗口句柄:";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lab_current_win);
            this.groupBox1.Controls.Add(this.lab_mh_win);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(264, 141);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "窗口信息";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.lv_pkg);
            this.groupBox2.Location = new System.Drawing.Point(318, 19);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(1002, 830);
            this.groupBox2.TabIndex = 5;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "数据报文";
            // 
            // lv_pkg
            // 
            this.lv_pkg.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader5,
            this.columnHeader4});
            this.lv_pkg.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lv_pkg.FullRowSelect = true;
            this.lv_pkg.GridLines = true;
            this.lv_pkg.Location = new System.Drawing.Point(3, 24);
            this.lv_pkg.Name = "lv_pkg";
            this.lv_pkg.Size = new System.Drawing.Size(996, 803);
            this.lv_pkg.TabIndex = 10;
            this.lv_pkg.UseCompatibleStateImageBehavior = false;
            this.lv_pkg.View = System.Windows.Forms.View.Details;
            this.lv_pkg.MouseClick += new System.Windows.Forms.MouseEventHandler(this.lv_pkg_MouseClick);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "计数";
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "长度";
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "类型";
            // 
            // columnHeader5
            // 
            this.columnHeader5.Text = "窗口";
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "内容";
            // 
            // cms
            // 
            this.cms.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.cms.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.copyPkg,
            this.item_str});
            this.cms.Name = "cms";
            this.cms.Size = new System.Drawing.Size(153, 60);
            // 
            // copyPkg
            // 
            this.copyPkg.Name = "copyPkg";
            this.copyPkg.Size = new System.Drawing.Size(152, 28);
            this.copyPkg.Text = "复制报文";
            this.copyPkg.Click += new System.EventHandler(this.copyPkg_Click);
            // 
            // item_str
            // 
            this.item_str.Name = "item_str";
            this.item_str.Size = new System.Drawing.Size(152, 28);
            this.item_str.Text = "查看报文";
            this.item_str.Click += new System.EventHandler(this.item_str_Click);
            // 
            // button1
            // 
            this.button1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.button1.Location = new System.Drawing.Point(515, 892);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(141, 42);
            this.button1.TabIndex = 6;
            this.button1.Text = "清空记录";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // btn_data_type
            // 
            this.btn_data_type.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btn_data_type.Location = new System.Drawing.Point(711, 892);
            this.btn_data_type.Name = "btn_data_type";
            this.btn_data_type.Size = new System.Drawing.Size(141, 42);
            this.btn_data_type.TabIndex = 8;
            this.btn_data_type.Text = "截获指定";
            this.btn_data_type.UseVisualStyleBackColor = true;
            this.btn_data_type.Click += new System.EventHandler(this.btn_data_type_Click);
            // 
            // btn_stop
            // 
            this.btn_stop.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btn_stop.Location = new System.Drawing.Point(319, 892);
            this.btn_stop.Name = "btn_stop";
            this.btn_stop.Size = new System.Drawing.Size(141, 42);
            this.btn_stop.TabIndex = 9;
            this.btn_stop.Text = "停止显示";
            this.btn_stop.UseVisualStyleBackColor = true;
            this.btn_stop.Click += new System.EventHandler(this.btn_stop_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.cbx_db_filert);
            this.groupBox3.Controls.Add(this.btnLoad);
            this.groupBox3.Controls.Add(this.btnSave);
            this.groupBox3.Controls.Add(this.txtTable);
            this.groupBox3.Controls.Add(this.label1);
            this.groupBox3.Location = new System.Drawing.Point(12, 190);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(264, 204);
            this.groupBox3.TabIndex = 10;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "数据存储";
            // 
            // cbx_db_filert
            // 
            this.cbx_db_filert.AutoSize = true;
            this.cbx_db_filert.Location = new System.Drawing.Point(146, 91);
            this.cbx_db_filert.Name = "cbx_db_filert";
            this.cbx_db_filert.Size = new System.Drawing.Size(106, 22);
            this.cbx_db_filert.TabIndex = 5;
            this.cbx_db_filert.Text = "入库过滤";
            this.cbx_db_filert.UseVisualStyleBackColor = true;
            // 
            // btnLoad
            // 
            this.btnLoad.Location = new System.Drawing.Point(146, 133);
            this.btnLoad.Name = "btnLoad";
            this.btnLoad.Size = new System.Drawing.Size(97, 42);
            this.btnLoad.TabIndex = 4;
            this.btnLoad.Text = "分类管理";
            this.btnLoad.UseVisualStyleBackColor = true;
            this.btnLoad.Click += new System.EventHandler(this.btnLoad_Click);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(9, 133);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(97, 42);
            this.btnSave.TabIndex = 3;
            this.btnSave.Text = "开始入库";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // txtTable
            // 
            this.txtTable.Location = new System.Drawing.Point(74, 35);
            this.txtTable.Name = "txtTable";
            this.txtTable.Size = new System.Drawing.Size(169, 28);
            this.txtTable.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 40);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 18);
            this.label1.TabIndex = 0;
            this.label1.Text = "表名:";
            // 
            // cbxFilert
            // 
            this.cbxFilert.AutoSize = true;
            this.cbxFilert.Checked = true;
            this.cbxFilert.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbxFilert.Location = new System.Drawing.Point(1211, 917);
            this.cbxFilert.Name = "cbxFilert";
            this.cbxFilert.Size = new System.Drawing.Size(106, 22);
            this.cbxFilert.TabIndex = 11;
            this.cbxFilert.Text = "消息过滤";
            this.cbxFilert.UseVisualStyleBackColor = true;
            // 
            // cbxShowType
            // 
            this.cbxShowType.AutoSize = true;
            this.cbxShowType.Location = new System.Drawing.Point(1067, 917);
            this.cbxShowType.Name = "cbxShowType";
            this.cbxShowType.Size = new System.Drawing.Size(106, 22);
            this.cbxShowType.TabIndex = 12;
            this.cbxShowType.Text = "已知分类";
            this.cbxShowType.UseVisualStyleBackColor = true;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.btnQh);
            this.groupBox4.Controls.Add(this.btnLoadWin);
            this.groupBox4.Controls.Add(this.cbxWin);
            this.groupBox4.Location = new System.Drawing.Point(12, 437);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(264, 169);
            this.groupBox4.TabIndex = 13;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "窗口信息";
            // 
            // btnQh
            // 
            this.btnQh.Location = new System.Drawing.Point(159, 100);
            this.btnQh.Name = "btnQh";
            this.btnQh.Size = new System.Drawing.Size(97, 37);
            this.btnQh.TabIndex = 2;
            this.btnQh.Text = "确认切换";
            this.btnQh.UseVisualStyleBackColor = true;
            this.btnQh.Click += new System.EventHandler(this.btnQh_Click);
            // 
            // btnLoadWin
            // 
            this.btnLoadWin.Location = new System.Drawing.Point(9, 100);
            this.btnLoadWin.Name = "btnLoadWin";
            this.btnLoadWin.Size = new System.Drawing.Size(102, 37);
            this.btnLoadWin.TabIndex = 1;
            this.btnLoadWin.Text = "载入窗口";
            this.btnLoadWin.UseVisualStyleBackColor = true;
            this.btnLoadWin.Click += new System.EventHandler(this.btnLoadWin_Click);
            // 
            // cbxWin
            // 
            this.cbxWin.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxWin.FormattingEnabled = true;
            this.cbxWin.Location = new System.Drawing.Point(6, 47);
            this.cbxWin.Name = "cbxWin";
            this.cbxWin.Size = new System.Drawing.Size(252, 26);
            this.cbxWin.TabIndex = 0;
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.cbx_auto_hm);
            this.groupBox5.Controls.Add(this.checkBox7);
            this.groupBox5.Controls.Add(this.checkBox6);
            this.groupBox5.Controls.Add(this.checkBox5);
            this.groupBox5.Controls.Add(this.btnAccountCfg);
            this.groupBox5.Controls.Add(this.checkBox4);
            this.groupBox5.Controls.Add(this.button3);
            this.groupBox5.Controls.Add(this.checkBox3);
            this.groupBox5.Controls.Add(this.checkBox2);
            this.groupBox5.Controls.Add(this.cbx_auto_blam);
            this.groupBox5.Location = new System.Drawing.Point(12, 666);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(264, 331);
            this.groupBox5.TabIndex = 14;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "功能设置";
            // 
            // cbx_auto_hm
            // 
            this.cbx_auto_hm.AutoSize = true;
            this.cbx_auto_hm.Location = new System.Drawing.Point(9, 207);
            this.cbx_auto_hm.Name = "cbx_auto_hm";
            this.cbx_auto_hm.Size = new System.Drawing.Size(106, 22);
            this.cbx_auto_hm.TabIndex = 9;
            this.cbx_auto_hm.Text = "蓝血补充";
            this.cbx_auto_hm.UseVisualStyleBackColor = true;
            // 
            // checkBox7
            // 
            this.checkBox7.AutoSize = true;
            this.checkBox7.Location = new System.Drawing.Point(137, 150);
            this.checkBox7.Name = "checkBox7";
            this.checkBox7.Size = new System.Drawing.Size(106, 22);
            this.checkBox7.TabIndex = 8;
            this.checkBox7.Text = "自动封妖";
            this.checkBox7.UseVisualStyleBackColor = true;
            // 
            // checkBox6
            // 
            this.checkBox6.AutoSize = true;
            this.checkBox6.Location = new System.Drawing.Point(137, 93);
            this.checkBox6.Name = "checkBox6";
            this.checkBox6.Size = new System.Drawing.Size(106, 22);
            this.checkBox6.TabIndex = 7;
            this.checkBox6.Text = "自动抓鬼";
            this.checkBox6.UseVisualStyleBackColor = true;
            // 
            // checkBox5
            // 
            this.checkBox5.AutoSize = true;
            this.checkBox5.Location = new System.Drawing.Point(137, 207);
            this.checkBox5.Name = "checkBox5";
            this.checkBox5.Size = new System.Drawing.Size(106, 22);
            this.checkBox5.TabIndex = 6;
            this.checkBox5.Text = "五开组队";
            this.checkBox5.UseVisualStyleBackColor = true;
            // 
            // btnAccountCfg
            // 
            this.btnAccountCfg.Location = new System.Drawing.Point(9, 282);
            this.btnAccountCfg.Name = "btnAccountCfg";
            this.btnAccountCfg.Size = new System.Drawing.Size(106, 38);
            this.btnAccountCfg.TabIndex = 5;
            this.btnAccountCfg.Text = "账号设置";
            this.btnAccountCfg.UseVisualStyleBackColor = true;
            this.btnAccountCfg.Click += new System.EventHandler(this.btnAccountCfg_Click);
            // 
            // checkBox4
            // 
            this.checkBox4.AutoSize = true;
            this.checkBox4.Location = new System.Drawing.Point(137, 36);
            this.checkBox4.Name = "checkBox4";
            this.checkBox4.Size = new System.Drawing.Size(106, 22);
            this.checkBox4.TabIndex = 4;
            this.checkBox4.Text = "战斗协同";
            this.checkBox4.UseVisualStyleBackColor = true;
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(159, 282);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(84, 38);
            this.button3.TabIndex = 3;
            this.button3.Text = "确定";
            this.button3.UseVisualStyleBackColor = true;
            // 
            // checkBox3
            // 
            this.checkBox3.AutoSize = true;
            this.checkBox3.Location = new System.Drawing.Point(9, 150);
            this.checkBox3.Name = "checkBox3";
            this.checkBox3.Size = new System.Drawing.Size(106, 22);
            this.checkBox3.TabIndex = 2;
            this.checkBox3.Text = "自动师门";
            this.checkBox3.UseVisualStyleBackColor = true;
            // 
            // checkBox2
            // 
            this.checkBox2.AutoSize = true;
            this.checkBox2.Checked = true;
            this.checkBox2.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox2.Location = new System.Drawing.Point(9, 93);
            this.checkBox2.Name = "checkBox2";
            this.checkBox2.Size = new System.Drawing.Size(88, 22);
            this.checkBox2.TabIndex = 1;
            this.checkBox2.Text = "抓宝宝";
            this.checkBox2.UseVisualStyleBackColor = true;
            // 
            // cbx_auto_blam
            // 
            this.cbx_auto_blam.AutoSize = true;
            this.cbx_auto_blam.Location = new System.Drawing.Point(9, 36);
            this.cbx_auto_blam.Name = "cbx_auto_blam";
            this.cbx_auto_blam.Size = new System.Drawing.Size(106, 22);
            this.cbx_auto_blam.TabIndex = 0;
            this.cbx_auto_blam.Text = "自动乱走";
            this.cbx_auto_blam.UseVisualStyleBackColor = true;
            this.cbx_auto_blam.CheckedChanged += new System.EventHandler(this.cbx_auto_blam_CheckedChanged);
            // 
            // cbx_current
            // 
            this.cbx_current.AutoSize = true;
            this.cbx_current.Location = new System.Drawing.Point(1067, 873);
            this.cbx_current.Name = "cbx_current";
            this.cbx_current.Size = new System.Drawing.Size(106, 22);
            this.cbx_current.TabIndex = 15;
            this.cbx_current.Text = "显示当前";
            this.cbx_current.UseVisualStyleBackColor = true;
            // 
            // cbx_recv
            // 
            this.cbx_recv.AutoSize = true;
            this.cbx_recv.Location = new System.Drawing.Point(1211, 873);
            this.cbx_recv.Name = "cbx_recv";
            this.cbx_recv.Size = new System.Drawing.Size(106, 22);
            this.cbx_recv.TabIndex = 16;
            this.cbx_recv.Text = "过滤收包";
            this.cbx_recv.UseVisualStyleBackColor = true;
            // 
            // btn_test
            // 
            this.btn_test.Location = new System.Drawing.Point(907, 892);
            this.btn_test.Name = "btn_test";
            this.btn_test.Size = new System.Drawing.Size(123, 42);
            this.btn_test.TabIndex = 17;
            this.btn_test.Text = "功能测试";
            this.btn_test.UseVisualStyleBackColor = true;
            this.btn_test.Click += new System.EventHandler(this.btn_test_Click);
            // 
            // FrmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.ClientSize = new System.Drawing.Size(1371, 1040);
            this.Controls.Add(this.btn_test);
            this.Controls.Add(this.cbx_recv);
            this.Controls.Add(this.cbx_current);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.cbxShowType);
            this.Controls.Add(this.cbxFilert);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.btn_stop);
            this.Controls.Add(this.btn_data_type);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btn_send);
            this.Controls.Add(this.txtSend);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FrmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "梦幻调试";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmMain_FormClosing);
            this.Load += new System.EventHandler(this.FrmMain_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.cms.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtSend;
        private System.Windows.Forms.Button btn_send;
        private System.Windows.Forms.Label lab_current_win;
        private System.Windows.Forms.Label lab_mh_win;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ContextMenuStrip cms;
        private System.Windows.Forms.ToolStripMenuItem copyPkg;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button btn_data_type;
        private System.Windows.Forms.Button btn_stop;
        private System.Windows.Forms.ListView lv_pkg;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.TextBox txtTable;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnLoad;
        private System.Windows.Forms.ToolStripMenuItem item_str;
        private System.Windows.Forms.CheckBox cbxFilert;
        private System.Windows.Forms.CheckBox cbx_db_filert;
        private System.Windows.Forms.CheckBox cbxShowType;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.ComboBox cbxWin;
        private System.Windows.Forms.Button btnLoadWin;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.Button btnQh;
        private System.Windows.Forms.CheckBox cbx_auto_blam;
        private System.Windows.Forms.CheckBox checkBox2;
        private System.Windows.Forms.CheckBox checkBox3;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.CheckBox checkBox4;
        private System.Windows.Forms.Button btnAccountCfg;
        private System.Windows.Forms.CheckBox checkBox5;
        private System.Windows.Forms.CheckBox checkBox6;
        private System.Windows.Forms.CheckBox checkBox7;
        private System.Windows.Forms.CheckBox cbx_auto_hm;
        private System.Windows.Forms.ColumnHeader columnHeader5;
        private System.Windows.Forms.CheckBox cbx_current;
        private System.Windows.Forms.CheckBox cbx_recv;
        private System.Windows.Forms.Button btn_test;
    }
}

