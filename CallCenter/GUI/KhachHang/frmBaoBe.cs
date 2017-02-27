using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CallCenter.DAL.QuanTri;
using CallCenter.Database;
using CallCenter.DAL.KhachHang;

namespace CallCenter.GUI.KhachHang
{
    public partial class frmBaoBe : Form
    {


        public frmBaoBe()
        {
            InitializeComponent();
        }

        private void btToaDo_Click(object sender, EventArgs e)
        {
            string url = "http://hp_g7/callcenter.aspx?add=" + this.TENDUONG.Text;
            frmWeb F = new frmWeb(url);
            F.ShowDialog();
        }

    }
}