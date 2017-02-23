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
        public frmDongNuoc(string dc)
        {
            InitializeComponent();
            webKitBrowser1.Navigate("http://hp_g7/callcenter.aspx?add=" + dc);
        }
    }
}
