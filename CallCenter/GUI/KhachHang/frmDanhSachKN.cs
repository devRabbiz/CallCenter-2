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
using CallCenter.DAL;

namespace CallCenter.GUI.KhachHang
{
    public partial class frmDanhSachKN : Form
    {

        public frmDanhSachKN()
        {
            InitializeComponent();
            dateTuNgay.ValueObject = DateTime.Now.Date;
            dateDenNgay.ValueObject = DateTime.Now.Date;
            pLoad();
        }

        public void pLoad()
        {
            string sql = " SELECT tn.SoHoSo,DienThoai,DanhBo,lt.TenLoai,NgayNhan, GhiChu,CreateBy,ChuyenHS,NgayXuLy,KetQuaXuLy,NhanVienXuLy,TenKH,(SoNha + ' ' + TenDuong ) as DiaChi ";
            sql += "   FROM TiepNhan tn, LoaiTiepNhan lt ";
            sql += "   WHERE tn.LoaiHs=lt.ID  ";
            sql += " AND CONVERT(DATE,NgayNhan,103) BETWEEN CONVERT(DATE,'" + Utilities.DateToString.NgayVN(dateTuNgay.Value.Date) + "',103) AND CONVERT(DATE,'" + Utilities.DateToString.NgayVN(dateDenNgay.Value.Date) + "',103) ";
            
            if(ckChuaChuyen.Checked)
                sql += " AND (ChuyenHS is NULL OR ChuyenHS='False')";
            else if(ckChuaXuLy.Checked)
                sql += " AND (ChuyenHS is not null AND NgayXuLy is null) ";
            else if (ckChuaXuLy.Checked)
                sql += " AND NgayXuLy IS NOT NULL ";

            dataGrid.DataSource = CCallCenter.getDataTable(sql);
           // format();
        }


        private void check(object sender, EventArgs e)
        {
            pLoad();
        }

        void format()
        {
            for (int i = 0; i < dataGrid.Rows.Count; i++)
            {

                if ("Flase".Equals(this.dataGrid.Rows[i].Cells["ChuyenHS"].Value + ""))
                {
                    dataGrid.Rows[i].DefaultCellStyle.BackColor = System.Drawing.Color.PaleGreen;
                }               
                else if ("True".Equals(this.dataGrid.Rows[i].Cells["ChuyenHS"].Value + "") && "".Equals(this.dataGrid.Rows[i].Cells["NgayXuLy"].Value + ""))
                {
                    dataGrid.Rows[i].DefaultCellStyle.BackColor = System.Drawing.Color.Cyan;
                }
                else if ("".Equals(this.dataGrid.Rows[i].Cells["NgayXuLy"].Value + ""))
                {
                    dataGrid.Rows[i].DefaultCellStyle.BackColor = System.Drawing.Color.GreenYellow;
                }
                
            } 
        }

        private void dateDenNgay_ValueChanged(object sender, EventArgs e)
        {
            pLoad();
        }

        private void dateTuNgay_ValueChanged(object sender, EventArgs e)
        {
            pLoad();
        }

        private void btChuyenHS_Click(object sender, EventArgs e)
        {
            string listDanhBa = "";
            int flag = 0;
            for (int i = 0; i < dataGrid.Rows.Count; i++)
            {
                if ("True".Equals(this.dataGrid.Rows[i].Cells["checkChon"].Value + ""))
                {
                    flag++;
                    listDanhBa += ("'" + (this.dataGrid.Rows[i].Cells["sohoso"].Value + "").Replace(" ", "") + "',");
                }
            }
            MessageBox.Show(this, listDanhBa.Remove(listDanhBa.Length - 1, 1));

        }

        private void dataGrid_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            format();
        }

        private void btCapNhat_Click(object sender, EventArgs e)
        {
            string sql = "UPDATE TiepNhan SET ChuyenHS='True',NgayChuyen=GETDATE(),NgayXuLy=GETDATE(),KetQuaXuLy=N'" + txtKetQuaXL.Text + "',NhanVienXuLy='" + CNguoiDung.HoTen + "'  WHERE SoHoSo='" + txtSoHoSo.Text + "'";
            if (CCallCenter.ExecuteCommand_(sql) > 0)
            { MessageBox.Show(this, "Cập Nhật Xử Lý Thành Công !", "..: Thông Báo :..", MessageBoxButtons.OK, MessageBoxIcon.Information); pLoad(); }
            else
                MessageBox.Show(this, "Cập Nhật Xử Lý Thất Bại !", "..: Thông Báo :..", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void dataGrid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                string sohoso = dataGrid.Rows[e.RowIndex].Cells["sohoso"].Value + "";
                string DanhBo = dataGrid.Rows[e.RowIndex].Cells["DanhBo"].Value + "";
                string DienThoai = dataGrid.Rows[e.RowIndex].Cells["DienThoai"].Value + "";
                string TenLoai = dataGrid.Rows[e.RowIndex].Cells["TenLoai"].Value + "";
                string TenKH = dataGrid.Rows[e.RowIndex].Cells["TenKH"].Value + "";
                string DiaChi = dataGrid.Rows[e.RowIndex].Cells["DiaChi"].Value + "";
                string GhiChu = dataGrid.Rows[e.RowIndex].Cells["GhiChu"].Value + "";
                string Ngaytn = dataGrid.Rows[e.RowIndex].Cells["NgayNhan"].Value + "";
                txtSoHoSo.Text = sohoso;
                txtSoDanhBo.Text = DanhBo;
                txtDienThoai.Text = DienThoai;
                txtTenKH.Text = TenKH;
                txtDuong.Text = DiaChi;
                txtGhiChu.Text = GhiChu;
                txtLoaiTiepNhan.Text = TenLoai;
                dateNgaytn.ValueObject = Ngaytn;
            }
            catch (Exception)
            {

            }
        }

      

    }
}
