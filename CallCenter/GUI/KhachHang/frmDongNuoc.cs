using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CallCenter.GUI.KhachHang
{
    public partial class frmDongNuoc : Form
    {
        public frmDongNuoc()
        {
            InitializeComponent();
            webKitBrowser1.Navigate("http://192.168.1.8/View/dongnuoc.aspx");
        }
    }
}
