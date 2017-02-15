﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CallCenter.DAL.QuanTri;
using CallCenter.Database;

namespace CallCenter.GUI.HeThong
{
    public partial class frmDoiMatKhau : Form
    {
        CNguoiDung _cNguoiDung = new CNguoiDung();

        public frmDoiMatKhau()
        {
            InitializeComponent();
        }

        private void btnThayDoi_Click(object sender, EventArgs e)
        {
            if (txtMatKhauCu.Text.Trim() != "" && txtMatKhauMoi.Text.Trim() != "" && txtXNMatKhauMoi.Text.Trim() != "")
                if (txtMatKhauMoi.Text.Trim() == txtXNMatKhauMoi.Text.Trim())
                {
                    NguoiDung nguoidung = _cNguoiDung.GetByMaND(CNguoiDung.MaND);
                    nguoidung.MatKhau = txtMatKhauMoi.Text.Trim();
                    if (_cNguoiDung.Sua(nguoidung))
                    {
                        MessageBox.Show("Thành công", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtMatKhauCu.Text = "";
                        txtMatKhauMoi.Text = "";
                        txtXNMatKhauMoi.Text = "";
                    }
                }
                else
                    MessageBox.Show("Xác nhận Mật khẩu không giống nhau", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else
                MessageBox.Show("Chưa nhập đủ thông tin", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void frmDoiMatKhau_Load(object sender, EventArgs e)
        {

        }
    }
}
