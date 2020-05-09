using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace mh
{
    public partial class FrmMsg : Form
    {
        public string hexData = "";
        private byte[] hex;
        private int offsetBit = 0;
        public FrmMsg()
        {
            InitializeComponent();
        }

        private void FrmMsg_Load(object sender, EventArgs e)
        {
            rtbBody.Text = hexData;
            cbx_type.SelectedIndex = 0;
            cbxToType.SelectedIndex = 0;
        }

        private void btnstr_Click(object sender, EventArgs e)
        {

            //结束位置
            int endIdx = Convert.ToInt32(txtEnd.Text.Trim());
            if (txtEnd.Text.Trim() != "")
            {
                endIdx = Convert.ToInt32(txtEnd.Text.Trim());
            }
            //偏移位置
            int offsetBit = 0;
            if (txtBit.Text.Trim() != "")
            {
                offsetBit = Convert.ToInt32(txtBit.Text.Trim());
            }
            //偏移量
            int offsetNum = 0;
            if (txtOffsetNum.Text.Trim() != "")
            {
                offsetNum = Convert.ToInt32(txtOffsetNum.Text.Trim());
            }
            if (txtBit.Text != "")
            {
                //转换为二进制
                byte[] b = StringUtil.strToToHexByte(rtbBody.Text);
                //默认普通方式

                if (cbx_type.Text == "递增方式" && txtOffsetNum.Text.Trim() != "")
                {
                    offsetBit = offsetBit + offsetNum;
                    txtBit.Text = offsetBit.ToString();
                }

                if (endIdx > 0 && endIdx > offsetBit)
                {
                    b = b.Skip(offsetBit).Take(endIdx - offsetBit).ToArray();
                }
                //默认截取到最后
                else
                {
                    b = b.Skip(offsetBit).ToArray();
                }
                //转换为字符串
                if (cbxToType.Text == "字符串")
                {
                    string tmp = StringUtil.byteToHexStr(b);
                    rtbRet.Text = StringUtil.GetChsFromHex(tmp);
                }
                //转换为10进制
                if (cbxToType.Text == "10进制")
                {
                    string tmp = "";
                    for (int i = 0; i < b.Length; i++)
                    {
                        int ret = Convert.ToInt32(b[i]);
                        tmp += ret.ToString() + " ";
                    }

                    rtbRet.Text = tmp;

                }
            }
        }

        private void txtBit_KeyPress(object sender, KeyPressEventArgs e)
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

        private void txtEnd_KeyPress(object sender, KeyPressEventArgs e)
        {
            txtBit_KeyPress(sender, e);
        }

        private void txtOffset_KeyPress(object sender, KeyPressEventArgs e)
        {
            txtBit_KeyPress(sender, e);
        }

        private void cbx_type_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbx_type.Text == "递增方式")
            {
                txtOffsetNum.Enabled = true;
            }
            else
            {
                txtOffsetNum.Enabled = false;
            }
        }

        private void btn_toByte_Click(object sender, EventArgs e)
        {
            int t = Convert.ToInt32(txtNum.Text.Trim());
            byte[] data = BitConverter.GetBytes(t);//将int32转换为字节数组
            StringBuilder line = new StringBuilder();
            for (int i = 0; i < data.Length; i++)
            {
                if (data[i] < 16)
                {
                    line.Append("0" + String.Format("{0:X}", data[i]) + " ");
                }
                else
                {
                    line.Append(String.Format("{0:X}", data[i]) + " ");

                }
            }

            txtToRet.Text = line.ToString();

        }

        private void txt16ToInt_Click(object sender, EventArgs e)
        {
           
            string[] ret = Regex.Split(txt16.Text.Trim(), " ", RegexOptions.IgnoreCase);
            byte[] val = new byte[4];
            for (int i = 0; i < ret.Length; i++)
            {
                int tmp = Convert.ToInt32(ret[i], 16);
                val[i] = Convert.ToByte(tmp);
            }
            txtIntRet.Text = BitConverter.ToInt32(val, 0).ToString();
        }
    }
}
