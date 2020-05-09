namespace mh
{
    partial class FrmMsg
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmMsg));
            this.rtbBody = new System.Windows.Forms.RichTextBox();
            this.btnstr = new System.Windows.Forms.Button();
            this.rtbRet = new System.Windows.Forms.RichTextBox();
            this.txtBit = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cbx_type = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtEnd = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtOffsetNum = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.cbxToType = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtToRet = new System.Windows.Forms.TextBox();
            this.btn_toByte = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.txtNum = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.txtIntRet = new System.Windows.Forms.TextBox();
            this.txt16ToInt = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.txt16 = new System.Windows.Forms.TextBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // rtbBody
            // 
            this.rtbBody.Location = new System.Drawing.Point(12, 12);
            this.rtbBody.Name = "rtbBody";
            this.rtbBody.Size = new System.Drawing.Size(749, 215);
            this.rtbBody.TabIndex = 0;
            this.rtbBody.Text = "";
            // 
            // btnstr
            // 
            this.btnstr.Location = new System.Drawing.Point(620, 346);
            this.btnstr.Name = "btnstr";
            this.btnstr.Size = new System.Drawing.Size(132, 35);
            this.btnstr.TabIndex = 2;
            this.btnstr.Text = "转换";
            this.btnstr.UseVisualStyleBackColor = true;
            this.btnstr.Click += new System.EventHandler(this.btnstr_Click);
            // 
            // rtbRet
            // 
            this.rtbRet.Location = new System.Drawing.Point(12, 402);
            this.rtbRet.Name = "rtbRet";
            this.rtbRet.Size = new System.Drawing.Size(749, 198);
            this.rtbRet.TabIndex = 6;
            this.rtbRet.Text = "";
            // 
            // txtBit
            // 
            this.txtBit.Location = new System.Drawing.Point(134, 254);
            this.txtBit.Multiline = true;
            this.txtBit.Name = "txtBit";
            this.txtBit.Size = new System.Drawing.Size(95, 32);
            this.txtBit.TabIndex = 7;
            this.txtBit.Text = "0";
            this.txtBit.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtBit_KeyPress);
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(20, 257);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(122, 27);
            this.label1.TabIndex = 8;
            this.label1.Text = "偏移位数:";
            // 
            // cbx_type
            // 
            this.cbx_type.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbx_type.FormattingEnabled = true;
            this.cbx_type.Items.AddRange(new object[] {
            "普通方式",
            "递增方式"});
            this.cbx_type.Location = new System.Drawing.Point(360, 258);
            this.cbx_type.Name = "cbx_type";
            this.cbx_type.Size = new System.Drawing.Size(109, 26);
            this.cbx_type.TabIndex = 9;
            this.cbx_type.SelectedIndexChanged += new System.EventHandler(this.cbx_type_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(245, 258);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(122, 27);
            this.label2.TabIndex = 10;
            this.label2.Text = "偏移方式:";
            // 
            // txtEnd
            // 
            this.txtEnd.Location = new System.Drawing.Point(360, 351);
            this.txtEnd.Multiline = true;
            this.txtEnd.Name = "txtEnd";
            this.txtEnd.Size = new System.Drawing.Size(109, 32);
            this.txtEnd.TabIndex = 11;
            this.txtEnd.Text = "0";
            this.txtEnd.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtEnd_KeyPress);
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.Location = new System.Drawing.Point(245, 354);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(122, 27);
            this.label3.TabIndex = 12;
            this.label3.Text = "结束位置:";
            // 
            // txtOffsetNum
            // 
            this.txtOffsetNum.Enabled = false;
            this.txtOffsetNum.Location = new System.Drawing.Point(134, 351);
            this.txtOffsetNum.Multiline = true;
            this.txtOffsetNum.Name = "txtOffsetNum";
            this.txtOffsetNum.Size = new System.Drawing.Size(95, 32);
            this.txtOffsetNum.TabIndex = 13;
            this.txtOffsetNum.Text = "1";
            this.txtOffsetNum.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtOffset_KeyPress);
            // 
            // label4
            // 
            this.label4.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label4.Location = new System.Drawing.Point(20, 351);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(122, 27);
            this.label4.TabIndex = 14;
            this.label4.Text = "偏移数量:";
            // 
            // cbxToType
            // 
            this.cbxToType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxToType.FormattingEnabled = true;
            this.cbxToType.Items.AddRange(new object[] {
            "字符串",
            "10进制"});
            this.cbxToType.Location = new System.Drawing.Point(620, 257);
            this.cbxToType.Name = "cbxToType";
            this.cbxToType.Size = new System.Drawing.Size(132, 26);
            this.cbxToType.TabIndex = 15;
            // 
            // label5
            // 
            this.label5.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label5.Location = new System.Drawing.Point(503, 257);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(122, 27);
            this.label5.TabIndex = 16;
            this.label5.Text = "转换方式:";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtToRet);
            this.groupBox1.Controls.Add(this.btn_toByte);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.txtNum);
            this.groupBox1.Location = new System.Drawing.Point(12, 619);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(749, 79);
            this.groupBox1.TabIndex = 17;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "整数转16进制字节";
            // 
            // txtToRet
            // 
            this.txtToRet.Location = new System.Drawing.Point(171, 27);
            this.txtToRet.Name = "txtToRet";
            this.txtToRet.Size = new System.Drawing.Size(474, 28);
            this.txtToRet.TabIndex = 3;
            // 
            // btn_toByte
            // 
            this.btn_toByte.Location = new System.Drawing.Point(651, 27);
            this.btn_toByte.Name = "btn_toByte";
            this.btn_toByte.Size = new System.Drawing.Size(89, 31);
            this.btn_toByte.TabIndex = 2;
            this.btn_toByte.Text = "转换";
            this.btn_toByte.UseVisualStyleBackColor = true;
            this.btn_toByte.Click += new System.EventHandler(this.btn_toByte_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(9, 33);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(44, 18);
            this.label6.TabIndex = 1;
            this.label6.Text = "整数";
            // 
            // txtNum
            // 
            this.txtNum.Location = new System.Drawing.Point(59, 27);
            this.txtNum.Name = "txtNum";
            this.txtNum.Size = new System.Drawing.Size(106, 28);
            this.txtNum.TabIndex = 0;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.txtIntRet);
            this.groupBox2.Controls.Add(this.txt16ToInt);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.txt16);
            this.groupBox2.Location = new System.Drawing.Point(12, 714);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(749, 79);
            this.groupBox2.TabIndex = 18;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "16进制字节转整数";
            // 
            // txtIntRet
            // 
            this.txtIntRet.Location = new System.Drawing.Point(171, 27);
            this.txtIntRet.Name = "txtIntRet";
            this.txtIntRet.Size = new System.Drawing.Size(474, 28);
            this.txtIntRet.TabIndex = 3;
            // 
            // txt16ToInt
            // 
            this.txt16ToInt.Location = new System.Drawing.Point(651, 27);
            this.txt16ToInt.Name = "txt16ToInt";
            this.txt16ToInt.Size = new System.Drawing.Size(89, 31);
            this.txt16ToInt.TabIndex = 2;
            this.txt16ToInt.Text = "转换";
            this.txt16ToInt.UseVisualStyleBackColor = true;
            this.txt16ToInt.Click += new System.EventHandler(this.txt16ToInt_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(9, 33);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(44, 18);
            this.label7.TabIndex = 1;
            this.label7.Text = "字节";
            // 
            // txt16
            // 
            this.txt16.Location = new System.Drawing.Point(59, 27);
            this.txt16.Name = "txt16";
            this.txt16.Size = new System.Drawing.Size(106, 28);
            this.txt16.TabIndex = 0;
            // 
            // FrmMsg
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(777, 910);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.cbxToType);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txtOffsetNum);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtBit);
            this.Controls.Add(this.cbx_type);
            this.Controls.Add(this.txtEnd);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.rtbRet);
            this.Controls.Add(this.btnstr);
            this.Controls.Add(this.rtbBody);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimizeBox = false;
            this.Name = "FrmMsg";
            this.Text = "消息查看";
            this.Load += new System.EventHandler(this.FrmMsg_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RichTextBox rtbBody;
        private System.Windows.Forms.Button btnstr;
        private System.Windows.Forms.RichTextBox rtbRet;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtBit;
        private System.Windows.Forms.ComboBox cbx_type;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtEnd;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtOffsetNum;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cbxToType;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btn_toByte;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtNum;
        private System.Windows.Forms.TextBox txtToRet;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox txtIntRet;
        private System.Windows.Forms.Button txt16ToInt;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txt16;
    }
}