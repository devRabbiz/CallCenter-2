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
    public partial class frmWeb : Form
    {
        public frmWeb(string dc)
        {
            InitializeComponent();
            webKitBrowser1.Navigate(dc);
        }
    }
}
