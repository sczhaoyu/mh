using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace mh
{
    public partial class FrmEnhanceTest : Form
    {
        public FrmEnhanceTest()
        {
            InitializeComponent();
        }

        private void FrmEnhanceTest_Load(object sender, EventArgs e)
        {
               //开启控制台输出
            LoadDll.AllocConsole();
            mhxy.MHKernel mk = new mhxy.MHKernel();
            mk.Init();
        }
    }
}
