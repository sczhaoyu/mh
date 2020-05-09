namespace mh
{
    partial class FrmFunTest
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmFunTest));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btn_x_sub = new System.Windows.Forms.Button();
            this.btn_y_sub = new System.Windows.Forms.Button();
            this.btn_y_add = new System.Windows.Forms.Button();
            this.btn_x_add = new System.Windows.Forms.Button();
            this.txt_exit_y = new System.Windows.Forms.TextBox();
            this.txt_exit_x = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.btn_biao = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txt_end = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txt_start = new System.Windows.Forms.TextBox();
            this.btn_bag = new System.Windows.Forms.Button();
            this.btn_show_ui = new System.Windows.Forms.Button();
            this.btn_changan = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btn_x_sub);
            this.groupBox1.Controls.Add(this.btn_y_sub);
            this.groupBox1.Controls.Add(this.btn_y_add);
            this.groupBox1.Controls.Add(this.btn_x_add);
            this.groupBox1.Controls.Add(this.txt_exit_y);
            this.groupBox1.Controls.Add(this.txt_exit_x);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.button1);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(296, 180);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "出口测试";
            // 
            // btn_x_sub
            // 
            this.btn_x_sub.Location = new System.Drawing.Point(229, 27);
            this.btn_x_sub.Name = "btn_x_sub";
            this.btn_x_sub.Size = new System.Drawing.Size(54, 30);
            this.btn_x_sub.TabIndex = 8;
            this.btn_x_sub.Text = "-";
            this.btn_x_sub.UseVisualStyleBackColor = true;
            this.btn_x_sub.Click += new System.EventHandler(this.btn_x_sub_Click);
            // 
            // btn_y_sub
            // 
            this.btn_y_sub.Location = new System.Drawing.Point(229, 76);
            this.btn_y_sub.Name = "btn_y_sub";
            this.btn_y_sub.Size = new System.Drawing.Size(54, 30);
            this.btn_y_sub.TabIndex = 7;
            this.btn_y_sub.Text = "-";
            this.btn_y_sub.UseVisualStyleBackColor = true;
            this.btn_y_sub.Click += new System.EventHandler(this.btn_y_sub_Click);
            // 
            // btn_y_add
            // 
            this.btn_y_add.Location = new System.Drawing.Point(169, 76);
            this.btn_y_add.Name = "btn_y_add";
            this.btn_y_add.Size = new System.Drawing.Size(54, 30);
            this.btn_y_add.TabIndex = 6;
            this.btn_y_add.Text = "+";
            this.btn_y_add.UseVisualStyleBackColor = true;
            this.btn_y_add.Click += new System.EventHandler(this.btn_y_add_Click);
            // 
            // btn_x_add
            // 
            this.btn_x_add.Location = new System.Drawing.Point(169, 26);
            this.btn_x_add.Name = "btn_x_add";
            this.btn_x_add.Size = new System.Drawing.Size(54, 30);
            this.btn_x_add.TabIndex = 5;
            this.btn_x_add.Text = "+";
            this.btn_x_add.UseVisualStyleBackColor = true;
            this.btn_x_add.Click += new System.EventHandler(this.btn_x_add_Click);
            // 
            // txt_exit_y
            // 
            this.txt_exit_y.Location = new System.Drawing.Point(50, 73);
            this.txt_exit_y.Name = "txt_exit_y";
            this.txt_exit_y.Size = new System.Drawing.Size(113, 28);
            this.txt_exit_y.TabIndex = 4;
            this.txt_exit_y.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txt_exit_x_KeyPress);
            // 
            // txt_exit_x
            // 
            this.txt_exit_x.Location = new System.Drawing.Point(50, 28);
            this.txt_exit_x.Name = "txt_exit_x";
            this.txt_exit_x.Size = new System.Drawing.Size(113, 28);
            this.txt_exit_x.TabIndex = 3;
            this.txt_exit_x.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txt_exit_x_KeyPress);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(18, 76);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(26, 18);
            this.label2.TabIndex = 2;
            this.label2.Text = "Y:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(17, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(26, 18);
            this.label1.TabIndex = 1;
            this.label1.Text = "X:";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(196, 132);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(87, 33);
            this.button1.TabIndex = 0;
            this.button1.Text = "出口";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // btn_biao
            // 
            this.btn_biao.Location = new System.Drawing.Point(186, 87);
            this.btn_biao.Name = "btn_biao";
            this.btn_biao.Size = new System.Drawing.Size(104, 35);
            this.btn_biao.TabIndex = 1;
            this.btn_biao.Text = "启动";
            this.btn_biao.UseVisualStyleBackColor = true;
            this.btn_biao.Click += new System.EventHandler(this.btn_biao_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btn_changan);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.txt_end);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.txt_start);
            this.groupBox2.Controls.Add(this.btn_biao);
            this.groupBox2.Location = new System.Drawing.Point(12, 269);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(296, 137);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "寻路测试";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 97);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 18);
            this.label4.TabIndex = 6;
            this.label4.Text = "目标:";
            // 
            // txt_end
            // 
            this.txt_end.Location = new System.Drawing.Point(65, 92);
            this.txt_end.Name = "txt_end";
            this.txt_end.Size = new System.Drawing.Size(113, 28);
            this.txt_end.TabIndex = 7;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 42);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 18);
            this.label3.TabIndex = 5;
            this.label3.Text = "起点:";
            // 
            // txt_start
            // 
            this.txt_start.Location = new System.Drawing.Point(65, 37);
            this.txt_start.Name = "txt_start";
            this.txt_start.Size = new System.Drawing.Size(113, 28);
            this.txt_start.TabIndex = 5;
            // 
            // btn_bag
            // 
            this.btn_bag.Location = new System.Drawing.Point(197, 436);
            this.btn_bag.Name = "btn_bag";
            this.btn_bag.Size = new System.Drawing.Size(90, 35);
            this.btn_bag.TabIndex = 3;
            this.btn_bag.Text = "背包";
            this.btn_bag.UseVisualStyleBackColor = true;
            this.btn_bag.Click += new System.EventHandler(this.btn_bag_Click);
            // 
            // btn_show_ui
            // 
            this.btn_show_ui.Location = new System.Drawing.Point(12, 436);
            this.btn_show_ui.Name = "btn_show_ui";
            this.btn_show_ui.Size = new System.Drawing.Size(103, 35);
            this.btn_show_ui.TabIndex = 4;
            this.btn_show_ui.Text = "界面显示";
            this.btn_show_ui.UseVisualStyleBackColor = true;
            this.btn_show_ui.Click += new System.EventHandler(this.btn_show_ui_Click);
            // 
            // btn_changan
            // 
            this.btn_changan.Location = new System.Drawing.Point(186, 35);
            this.btn_changan.Name = "btn_changan";
            this.btn_changan.Size = new System.Drawing.Size(104, 35);
            this.btn_changan.TabIndex = 8;
            this.btn_changan.Text = "长安";
            this.btn_changan.UseVisualStyleBackColor = true;
            this.btn_changan.Click += new System.EventHandler(this.btn_changan_Click);
            // 
            // FrmFunTest
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1045, 798);
            this.Controls.Add(this.btn_show_ui);
            this.Controls.Add(this.btn_bag);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FrmFunTest";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "功能测试";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txt_exit_y;
        private System.Windows.Forms.TextBox txt_exit_x;
        private System.Windows.Forms.Button btn_biao;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txt_start;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txt_end;
        private System.Windows.Forms.Button btn_bag;
        private System.Windows.Forms.Button btn_x_add;
        private System.Windows.Forms.Button btn_y_sub;
        private System.Windows.Forms.Button btn_y_add;
        private System.Windows.Forms.Button btn_x_sub;
        private System.Windows.Forms.Button btn_show_ui;
        private System.Windows.Forms.Button btn_changan;
    }
}