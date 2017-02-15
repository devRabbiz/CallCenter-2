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

namespace CallCenter.GUI.QuanTri
{
    public partial class frmNguoiDung : Form
    {
        CTo _cTo = new CTo();
        CNhom _cNhom = new CNhom();
        CNguoiDung _cNguoiDung = new CNguoiDung();
        CMenu _cMenu = new CMenu();
        CPhanQuyenNguoiDung _cPhanQuyenNguoiDung = new CPhanQuyenNguoiDung();
        int _selectedindex = -1;
        string _mnu = "mnuNguoiDung";
        BindingList<NguoiDung> _blNguoiDung;

        public frmNguoiDung()
        {
            InitializeComponent();
        }

        public void Clear()
        {
            _selectedindex = -1;
            txtHoTen.Text = "";
            txtTaiKhoan.Text = "";
            txtMatKhau.Text = "";
            chkPhoGiamDoc.Checked = false;
            chkDoi.Checked = false;
            chkToTruong.Checked = false;
            chkHanhThu.Checked = false;
            chkDongNuoc.Checked = false;
            chkVanPhong.Checked = false;
            if (CNguoiDung.Admin)
            {
                _blNguoiDung = new BindingList<NguoiDung>(_cNguoiDung.GetDS_Admin());
            }
            else
            {
                _blNguoiDung = new BindingList<NguoiDung>(_cNguoiDung.GetDSExceptMaND(CNguoiDung.MaND));
            }
            dgvNguoiDung.DataSource = _blNguoiDung;
        }

        private void frmNguoiDung_Load(object sender, EventArgs e)
        {
            if (CNguoiDung.Admin)
            {
                chkPhoGiamDoc.Visible = true;
                chkAn.Visible = true;
                _blNguoiDung = new BindingList<NguoiDung>(_cNguoiDung.GetDS_Admin());
            }
            else
            {
                chkPhoGiamDoc.Visible = false;
                chkAn.Visible = false;
                _blNguoiDung = new BindingList<NguoiDung>(_cNguoiDung.GetDSExceptMaND(CNguoiDung.MaND));
            }
            dgvNguoiDung.AutoGenerateColumns = false;

            cmbTo.DataSource = _cTo.GetDS();
            cmbTo.DisplayMember = "TenTo";
            cmbTo.ValueMember = "MaTo";
            //cmbTo.SelectedIndex = -1;

            cmbNhom.DataSource = _cNhom.GetDS();
            cmbNhom.DisplayMember = "TenNhom";
            cmbNhom.ValueMember = "MaNhom";
            //cmbNhom.SelectedIndex = -1;
            
            dgvNguoiDung.DataSource = _blNguoiDung;
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            if (CNguoiDung.CheckQuyen(_mnu, "Them"))
            {
                if (txtHoTen.Text.Trim() != "" && txtTaiKhoan.Text.Trim() != "" && txtMatKhau.Text.Trim() != "")
                {
                    NguoiDung nguoidung = new NguoiDung();
                    nguoidung.HoTen = txtHoTen.Text.Trim();
                    nguoidung.DienThoai = txtDienThoai.Text.Trim();
                    nguoidung.TaiKhoan = txtTaiKhoan.Text.Trim();
                    nguoidung.MatKhau = txtMatKhau.Text.Trim();
                    nguoidung.STT = _cNguoiDung.GetMaxSTT()+1;
                    if (!string.IsNullOrEmpty(txtNam.Text.Trim()))
                    {
                        nguoidung.NamVaoLam = int.Parse(txtNam.Text.Trim());
                        nguoidung.NgayPhepNamCu = nguoidung.NgayPhepNamMoi = 0;
                    }
                    if (cmbTo.SelectedIndex != -1)
                        nguoidung.MaTo = (int)cmbTo.SelectedValue;
                    if (cmbNhom.SelectedIndex != -1)
                        nguoidung.MaNhom = (int)cmbNhom.SelectedValue;
                    nguoidung.PhoGiamDoc = chkPhoGiamDoc.Checked;
                    nguoidung.An = chkAn.Checked;
                    nguoidung.Doi = chkDoi.Checked;
                    nguoidung.ToTruong = chkToTruong.Checked;
                    nguoidung.HanhThu = chkHanhThu.Checked;
                    nguoidung.DongNuoc = chkDongNuoc.Checked;
                    nguoidung.VanPhong = chkVanPhong.Checked;
                    nguoidung.ChamCong = chkChamCong.Checked;
                    ///tự động thêm quyền cho người mới
                    foreach (var item in _cMenu.GetDS())
                    {
                        PhanQuyenNguoiDung phanquyennguoidung = new PhanQuyenNguoiDung();
                        phanquyennguoidung.MaMenu = item.MaMenu;
                        phanquyennguoidung.MaND = nguoidung.MaND;
                        nguoidung.PhanQuyenNguoiDungs.Add(phanquyennguoidung);
                    }
                    if (_cNguoiDung.Them(nguoidung))
                    {
                        MessageBox.Show("Thành công", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        Clear();
                    }
                }
            }
            else
                MessageBox.Show("Bạn không có quyền Thêm Form này", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (CNguoiDung.CheckQuyen(_mnu, "Sua"))
            {
                if (_selectedindex != -1)
                {
                    NguoiDung nguoidung = _cNguoiDung.GetByMaND(int.Parse(dgvNguoiDung["MaND", _selectedindex].Value.ToString()));
                    nguoidung.HoTen = txtHoTen.Text.Trim();
                    nguoidung.DienThoai = txtDienThoai.Text.Trim();
                    nguoidung.TaiKhoan = txtTaiKhoan.Text.Trim();
                    nguoidung.MatKhau = txtMatKhau.Text.Trim();
                    if (!string.IsNullOrEmpty(txtNam.Text.Trim()))
                        nguoidung.NamVaoLam = int.Parse(txtNam.Text.Trim());
                    nguoidung.MaTo = (int)cmbTo.SelectedValue;
                    nguoidung.MaNhom = (int)cmbNhom.SelectedValue;
                    nguoidung.PhoGiamDoc = chkPhoGiamDoc.Checked;
                    nguoidung.An = chkAn.Checked;
                    nguoidung.Doi = chkDoi.Checked;
                    nguoidung.ToTruong = chkToTruong.Checked;
                    nguoidung.HanhThu = chkHanhThu.Checked;
                    nguoidung.DongNuoc = chkDongNuoc.Checked;
                    nguoidung.VanPhong = chkVanPhong.Checked;
                    nguoidung.ChamCong = chkChamCong.Checked;
                    _cNguoiDung.Sua(nguoidung);
                    DataTable dt = ((DataView)gridView.DataSource).Table;
                    foreach (DataRow item in dt.Rows)
                    {
                        PhanQuyenNguoiDung phanquyennguoidung = _cPhanQuyenNguoiDung.GetByMaMenuMaND(int.Parse(item["MaMenu"].ToString()), nguoidung.MaND);
                        if (phanquyennguoidung.Xem != bool.Parse(item["Xem"].ToString()) || phanquyennguoidung.Them != bool.Parse(item["Them"].ToString()) ||
                            phanquyennguoidung.Sua != bool.Parse(item["Sua"].ToString()) || phanquyennguoidung.Xoa != bool.Parse(item["Xoa"].ToString()) ||
                            phanquyennguoidung.QuanLy != bool.Parse(item["QuanLy"].ToString()))
                        {
                            phanquyennguoidung.Xem = bool.Parse(item["Xem"].ToString());
                            phanquyennguoidung.Them = bool.Parse(item["Them"].ToString());
                            phanquyennguoidung.Sua = bool.Parse(item["Sua"].ToString());
                            phanquyennguoidung.Xoa = bool.Parse(item["Xoa"].ToString());
                            phanquyennguoidung.QuanLy = bool.Parse(item["QuanLy"].ToString());
                            _cPhanQuyenNguoiDung.Sua(phanquyennguoidung);
                        }
                    }
                    Clear();
                    MessageBox.Show("Thành công", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
                MessageBox.Show("Bạn không có quyền Sửa Form này", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (CNguoiDung.CheckQuyen(_mnu, "Xoa"))
            {
                if (MessageBox.Show("Bạn có chắc chắn xóa?", "Xác nhận xóa", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                    if (_selectedindex != -1)
                    {
                        NguoiDung nguoidung = _cNguoiDung.GetByMaND(int.Parse(dgvNguoiDung["MaND", _selectedindex].Value.ToString()));
                        ///xóa quan hệ 1 nhiều
                        //_cPhanQuyenNguoiDung.Xoa(nguoidung.PhanQuyenNguoiDungs.ToList());
                        //_cNguoiDung.Xoa(nguoidung);
                        nguoidung.An = true;
                        _cNguoiDung.Sua(nguoidung);
                        Clear();
                        MessageBox.Show("Thành công", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                        MessageBox.Show("Lỗi, Vui lòng chọn Người Dùng cần xóa", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
                MessageBox.Show("Bạn không có quyền Xóa Form này", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void dgvNguoiDung_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                _selectedindex = e.RowIndex;
                txtHoTen.Text = dgvNguoiDung["HoTen", e.RowIndex].Value.ToString();
                if (dgvNguoiDung["DienThoai", e.RowIndex].Value != null)
                    txtDienThoai.Text = dgvNguoiDung["DienThoai", e.RowIndex].Value.ToString();
                txtTaiKhoan.Text = dgvNguoiDung["TaiKhoan", e.RowIndex].Value.ToString();
                txtMatKhau.Text = dgvNguoiDung["MatKhau", e.RowIndex].Value.ToString();
                if (dgvNguoiDung["NamVaoLam", e.RowIndex].Value != null)
                    txtNam.Text = dgvNguoiDung["NamVaoLam", e.RowIndex].Value.ToString();
                if (dgvNguoiDung["MaTo", e.RowIndex].Value != null)
                    cmbTo.SelectedValue = int.Parse(dgvNguoiDung["MaTo", e.RowIndex].Value.ToString());
                if (dgvNguoiDung["MaNhom", e.RowIndex].Value != null)
                    cmbNhom.SelectedValue = int.Parse(dgvNguoiDung["MaNhom", e.RowIndex].Value.ToString());
                chkPhoGiamDoc.Checked = bool.Parse(dgvNguoiDung["PhoGiamDoc", e.RowIndex].Value.ToString());
                chkAn.Checked = bool.Parse(dgvNguoiDung["An", e.RowIndex].Value.ToString());
                chkDoi.Checked = bool.Parse(dgvNguoiDung["Doi", e.RowIndex].Value.ToString());
                chkToTruong.Checked = bool.Parse(dgvNguoiDung["ToTruong", e.RowIndex].Value.ToString());
                chkHanhThu.Checked = bool.Parse(dgvNguoiDung["HanhThu", e.RowIndex].Value.ToString());
                chkDongNuoc.Checked = bool.Parse(dgvNguoiDung["DongNuoc", e.RowIndex].Value.ToString());
                chkVanPhong.Checked = bool.Parse(dgvNguoiDung["VanPhong", e.RowIndex].Value.ToString());
                chkChamCong.Checked = bool.Parse(dgvNguoiDung["ChamCong", e.RowIndex].Value.ToString());
                if (CNguoiDung.Admin)
                    gridControl.DataSource = _cPhanQuyenNguoiDung.GetDSByMaND(true, int.Parse(dgvNguoiDung["MaND", e.RowIndex].Value.ToString()));
                else
                    gridControl.DataSource = _cPhanQuyenNguoiDung.GetDSByMaND(false, int.Parse(dgvNguoiDung["MaND", e.RowIndex].Value.ToString()));
            }
            catch (Exception)
            {
            }

        }

        private void dgvNguoiDung_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            using (SolidBrush b = new SolidBrush(dgvNguoiDung.RowHeadersDefaultCellStyle.ForeColor))
            {
                e.Graphics.DrawString((e.RowIndex + 1).ToString(), e.InheritedRowStyle.Font, b, e.RowBounds.Location.X + 10, e.RowBounds.Location.Y + 4);
            }
        }

        private void dgvNguoiDung_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dgvNguoiDung.Columns[e.ColumnIndex].Name == "TenTo" && dgvNguoiDung["MaTo", e.RowIndex].Value != null)
                e.Value = _cTo.GetTenToByMaTo(int.Parse(dgvNguoiDung["MaTo", e.RowIndex].Value.ToString()));
            if (dgvNguoiDung.Columns[e.ColumnIndex].Name == "TenNhom" && dgvNguoiDung["MaNhom", e.RowIndex].Value != null)
                e.Value = _cNhom.GetTenNhomByMaNhom(int.Parse(dgvNguoiDung["MaNhom", e.RowIndex].Value.ToString()));
        }

        private void gridView_CellValueChanging(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.Column.Name == "ToanQuyen")
                if (bool.Parse(e.Value.ToString()))
                {
                    gridView.SetRowCellValue(e.RowHandle, gridView.Columns["Xem"], "True");
                    gridView.SetRowCellValue(e.RowHandle, gridView.Columns["Them"], "True");
                    gridView.SetRowCellValue(e.RowHandle, gridView.Columns["Sua"], "True");
                    gridView.SetRowCellValue(e.RowHandle, gridView.Columns["Xoa"], "True");
                }
                else
                {
                    gridView.SetRowCellValue(e.RowHandle, gridView.Columns["Xem"], "False");
                    gridView.SetRowCellValue(e.RowHandle, gridView.Columns["Them"], "False");
                    gridView.SetRowCellValue(e.RowHandle, gridView.Columns["Sua"], "False");
                    gridView.SetRowCellValue(e.RowHandle, gridView.Columns["Xoa"], "False");
                }
        }

        //private void dgvNguoiDung_KeyDown(object sender, KeyEventArgs e)
        //{
        //    if (e.KeyCode.Equals(Keys.Up))
        //    {
        //        moveUp();
        //    }
        //    if (e.KeyCode.Equals(Keys.Down))
        //    {
        //        moveDown();
        //    }
        //    e.Handled = true;
        //}

        //private void moveUp()
        //{
        //    if (dgvNguoiDung.RowCount > 0)
        //    {
        //        if (dgvNguoiDung.SelectedRows.Count > 0)
        //        {
        //            int rowCount = dgvNguoiDung.Rows.Count;
        //            int index = dgvNguoiDung.SelectedCells[0].OwningRow.Index;

        //            if (index == 0)
        //            {
        //                return;
        //            }
        //            //DataGridViewRowCollection rows = dgvNguoiDung.Rows;

        //            // remove the previous row and add it behind the selected row.
        //            //DataGridViewRow prevRow = rows[index - 1];
        //            //rows.Remove(prevRow);
        //            //prevRow.Frozen = false;
        //            //rows.Insert(index, prevRow);
        //            //dgvNguoiDung.ClearSelection();
        //            //dgvNguoiDung.Rows[index - 1].Selected = true;
        //            var itemToMoveUp = this._blNguoiDung[index];
        //            this._blNguoiDung.RemoveAt(index);
        //            this._blNguoiDung.Insert(index - 1, itemToMoveUp);
        //            dgvNguoiDung.ClearSelection();
        //            dgvNguoiDung.Rows[index - 1].Selected = true;
        //        }
        //    }
        //}

        //private void moveDown()
        //{
        //    if (dgvNguoiDung.RowCount > 0)
        //    {
        //        if (dgvNguoiDung.SelectedRows.Count > 0)
        //        {
        //            int rowCount = dgvNguoiDung.Rows.Count;
        //            int index = dgvNguoiDung.SelectedCells[0].OwningRow.Index;

        //            if (index == (rowCount - 2)) // include the header row
        //            {
        //                return;
        //            }
        //            //DataGridViewRowCollection rows = dgvNguoiDung.Rows;

        //            // remove the next row and add it in front of the selected row.
        //            //DataGridViewRow nextRow = rows[index + 1];
        //            //rows.Remove(nextRow);
        //            //nextRow.Frozen = false;
        //            //rows.Insert(index, nextRow);
        //            //dgvNguoiDung.ClearSelection();
        //            //dgvNguoiDung.Rows[index + 1].Selected = true;
        //            var itemToMoveDown = this._blNguoiDung[index];
        //            this._blNguoiDung.RemoveAt(index);
        //            this._blNguoiDung.Insert(index + 1, itemToMoveDown);
        //            dgvNguoiDung.ClearSelection();
        //            dgvNguoiDung.Rows[index + 1].Selected = true;
        //        }
        //    }
        //}

        int rowIndexFromMouseDown;
        DataGridViewRow rw;
        private void dgvNguoiDung_DragDrop(object sender, DragEventArgs e)
        {
            int rowIndexOfItemUnderMouseToDrop;
            Point clientPoint = dgvNguoiDung.PointToClient(new Point(e.X, e.Y));
            rowIndexOfItemUnderMouseToDrop = dgvNguoiDung.HitTest(clientPoint.X, clientPoint.Y).RowIndex;

            if (e.Effect == DragDropEffects.Move)
            {
                var item = this._blNguoiDung[rowIndexFromMouseDown];
                _blNguoiDung.RemoveAt(rowIndexFromMouseDown);
                _blNguoiDung.Insert(rowIndexOfItemUnderMouseToDrop,item);
                
                ///update STT dô database
                for (int i = 0; i < _blNguoiDung.Count; i++)
                {
                    _blNguoiDung[i].STT = i + 1;
                }
                _cNguoiDung.SubmitChanges();
            }
        }

        private void dgvNguoiDung_DragEnter(object sender, DragEventArgs e)
        {
            if (dgvNguoiDung.SelectedRows.Count > 0)
            {
                e.Effect = DragDropEffects.Move;
            }
        }

        private void dgvNguoiDung_MouseClick(object sender, MouseEventArgs e)
        {
            if (dgvNguoiDung.SelectedRows.Count == 1)
            {
                if (e.Button == MouseButtons.Left)
                {
                    rw = dgvNguoiDung.SelectedRows[0];
                    rowIndexFromMouseDown = dgvNguoiDung.SelectedRows[0].Index;
                    dgvNguoiDung.DoDragDrop(rw, DragDropEffects.Move);
                }
            }
        }
    }
}
