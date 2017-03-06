using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CallCenter.DAL.KhachHang;
using CallCenter.Database;
using CallCenter.DAL.QuanTri;

namespace CallCenter.GUI.KhachHang
{
    public partial class frmTiepNhanKN : Form
    {
        AutoCompleteStringCollection namesCollection = new AutoCompleteStringCollection();
        string danhbo = "";
        string loaikh = "";
        public frmTiepNhanKN(string db, string loai)
        {
            danhbo = db;
            loaikh = loai;
            InitializeComponent();
            fLoad(loaikh);
            if ("KH".Equals(loaikh))
            {
                loadDanhBo();
            }

        }
        public void loadDanhBo()
        {

            TB_DULIEUKHACHHANG khachhang = DAL.KhachHang.CKhachHang.finByDanhBo(danhbo);
            if (khachhang != null)
            {
                txtSoDanhBo.Text = khachhang.DANHBO;
                txtTenKH.Text = khachhang.HOTEN;
                txtsonha.Text = khachhang.SONHA;
                txtDuong.Text = khachhang.TENDUONG;
                txtDienThoai.Text = khachhang.DIENTHOAI;
                cbPhuong.SelectedValue = khachhang.PHUONG;
                Quan.SelectedValue = khachhang.QUAN;

                //lDuong();
            }
        }
        public void lDuong()
        {
            DataTable table = CHeThongDuong.getQuanPhuong(this.txtDuong.Text);
            if (table.Rows.Count > 0)
            {
                this.cbPhuong.SelectedText = table.Rows[0][0].ToString();
                this.Quan.SelectedText = table.Rows[0][1].ToString();
            }
        }

        public void fLoad(string loaikh)
        {
            txtSoHoSo.Text = CTiepNhanDon.IdentityBienNhan();
            cbLoaiTiepNhan.DataSource = CTiepNhanDon.getLoaiTiepNhan(loaikh);
            cbLoaiTiepNhan.DisplayMember = "TenLoai";
            cbLoaiTiepNhan.ValueMember = "ID";

            try
            {
                List<TENDUONG> list = CHeThongDuong.getList();
                foreach (var item in list)
                {
                    namesCollection.Add(item.DUONG);
                }
                txtDuong.AutoCompleteMode = AutoCompleteMode.Suggest;
                txtDuong.AutoCompleteSource = AutoCompleteSource.CustomSource;
                txtDuong.AutoCompleteCustomSource = namesCollection;

                this.cbPhuong.DataSource = CHeThongDuong.getListPhuong();
                this.cbPhuong.DisplayMember = "Display";
                this.cbPhuong.ValueMember = "Value";
                Quan.DataSource = CHeThongDuong.getListQUAN();
                Quan.DisplayMember = "Display";
                Quan.ValueMember = "Value";



            }
            catch (Exception)
            {
            }

        }

        private void txtDuong_Leave(object sender, EventArgs e)
        {
            lDuong();
        }

        private void txtDuong_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13 || e.KeyChar == 9)
            {
                lDuong();
            }
        }

        private void cbPhuong_SelectedValueChanged(object sender, EventArgs e)
        {
            //try
            //{
            //    List<PHUONG> phuong = CHeThongDuong.ListPhuongByTenPhuong(this.cbPhuong.Text);
            //    if (phuong.Count > 0)
            //    {
            //        PHUONG p = phuong[0];
            //        Quan.SelectedValue = p.QUAN.MAQUAN;

            //    }
            //}
            //catch (Exception)
            //{

            //}
        }

        private void btTiepNhan_Click(object sender, EventArgs e)
        {
            TiepNhan tn = new TiepNhan();
            tn.LoaiTN = loaikh;
            tn.SoHoSo = this.txtSoHoSo.Text;
            tn.DanhBo = this.txtSoDanhBo.Text;
            tn.DienThoai = this.txtDienThoai.Text;
            tn.TenKH = this.txtTenKH.Text;
            tn.SoNha = this.txtsonha.Text;
            tn.TenDuong = this.txtDuong.Text;
            tn.Phuong = this.cbPhuong.SelectedValue + "";
            tn.Quan = this.Quan.SelectedValue + "";
            tn.LoaiHs = this.cbLoaiTiepNhan.SelectedValue + "";
            tn.NgayNhan = DateTime.Now;
            tn.GhiChu = this.txtGhiChu.Text;
            tn.CreateBy = CNguoiDung.HoTen + "";
            tn.CreateDate = DateTime.Now;
            if (CTiepNhanDon.Insert(tn))
            {

                MessageBox.Show(this, "Tiếp Nhận Thông Tin Thành Công!", "..: Thông Báo :..", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            else
            {
                MessageBox.Show(this, "Tiếp Nhận Thông Tin Thất Bại !", "..: Thông Báo :..", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


        }

        private void txtGhiChu_TextChanged(object sender, EventArgs e)
        {

        }
    }
}