using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CallCenter.DAL.KhachHang;

namespace CallCenter.GUI.KhachHang
{
    public partial class frmTiepNhanKN : Form
    {
        public frmTiepNhanKN()
        {
            InitializeComponent();
            fLoad();
        }
        public void fLoad()
        {
            txtSoHoSo.Text = CKhachHang.IdentityBienNhan();
        
        }
    }
}
