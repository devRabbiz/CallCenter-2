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
    public partial class frmKhachHang : Form
    {
        CKinhDoanh _cKinhDoanh = new CKinhDoanh();

        public frmKhachHang()
        {
            InitializeComponent();
        }

        private void frmKhachHang_Load(object sender, EventArgs e)
        {
            gridControl.LevelTree.Nodes.Add("Chi Tiết Kiểm Tra Xác Minh", gridViewKTXM);
            gridControl.LevelTree.Nodes.Add("Chi Tiết Điều Chỉnh Biến Động", gridViewDCBD);
            gridControl.LevelTree.Nodes.Add("Chi Tiết Cắt Tạm/Hủy Danh Bộ", gridViewCHDB);
            gridControl.LevelTree.Nodes.Add("Chi Tiết Phiếu Hủy Danh Bộ", gridViewYeuCauCHDB);
            gridControl.LevelTree.Nodes.Add("Chi Tiết Thảo Thư Trả Lời", gridViewTTTL);
            gridControl.LevelTree.Nodes.Add("Chi Tiết Bấm Chì", gridViewBamChi);
            gridControl.LevelTree.Nodes.Add("Chi Tiết Đóng Nước", gridViewDongNuoc);
            gridControl.LevelTree.Nodes.Add("Chi Tiết Gian Lận", gridViewGianLan);
        }

        public void Search()
        {
            DataTable tb = new DataTable();
            string sql = "  SELECT * FROM ";
            sql += " (SELECT DANHBO,HOTEN, (SONHA+' '+ TENDUONG) as DIACHI,DIENTHOAI FROM TB_DULIEUKHACHHANG  ";
            sql += "   UNION ALL   ";
            sql += "  SELECT DANHBO,HOTEN, (SONHA+' '+ TENDUONG) as DIACHI,''  as DIENTHOAI FROM TB_DULIEUKHACHHANG_HUYDB  ) AS T  ";
            sql += " WHERE DANHBO IS NOT NULL ";

            string db = txtsearchDB.Text.Replace(" ", "").Replace("-", "");
            if (db != "")
            {
                sql += " AND DANHBO LIKE '%" + db + "%' ";
            }

            if (txtSearchDC.Text.Replace("-", "") != "")
            {
                sql += " AND DIACHI LIKE '%" + txtSearchDC.Text  + "%' ";
            }
            if (txtSearchDT.Text.Replace("-", "") != "")
            {
                sql += " AND DIENTHOAI LIKE '%" + txtSearchDT.Text  + "%' ";
            }

            tb = DAL.KhachHang.CKhachHang.getDataTable(sql);

            gSearch.DataSource = tb;
            Utilities.DataGridV.formatRows(gSearch);
            groupBox1.Text = "Tổng số " + tb.Rows.Count + " Khách Hàng !";

            if (tb.Rows.Count == 1)
            {
               string dbo = gSearch.Rows[0].Cells["DC_DANHBA"].Value + "";
                LoadThongTinDB(dbo.Replace(" ", ""));
            }
        }

        private void search(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                Search();
                gridControl.DataSource = _cKinhDoanh.GetTienTrinhByDanhBo(txtsearchDB.Text.Replace(" ", "").Replace("-", "")).Tables["Don"];
            }
        }

      
        TB_DULIEUKHACHHANG khachhang = null;
        void LoadThongTinDB(string sodanhbo)
        {
            if (sodanhbo.Length == 11)
            {
                khachhang = DAL.KhachHang.CKhachHang.finByDanhBo(sodanhbo);
                if (khachhang != null)
                {
                    rDanhBo.Text = khachhang.DANHBO;
                    LOTRINH.Text = khachhang.LOTRINH;
                    DOT.Text = khachhang.LOTRINH.Substring(1,2);
                    HOPDONG.Text = khachhang.HOPDONG;
                    HOTEN.Text = khachhang.HOTEN;
                    TENDUONG.Text =khachhang.SONHA + ' ' + khachhang.TENDUONG;
                    txtDienThoai.Text = khachhang.DIENTHOAI;
                    QUAN.Text = khachhang.QUAN + "."+khachhang.PHUONG;
                    txtHieuLuc.Text = String.Format("{0:00}", khachhang.KY) + "/" + khachhang.NAM;
                    GIABIEU.Text = khachhang.GIABIEU;
                    DINHMUC.Text = khachhang.DINHMUC;
                    NGAYGAN.ValueObject = khachhang.NGAYTHAY;
                    KIEMDINH.ValueObject = khachhang.NGAYKIEMDINH;
                    HIEUDH.Text = khachhang.HIEUDH;
                    CO.Text = khachhang.CODH;
                    CAP.Text = khachhang.CAP;
                    SOTHAN.Text = khachhang.SOTHANDH;
                    VITRI.Text = khachhang.VITRIDHN;
                    txtDMA.Text = khachhang.MADMA;
                    txtNhanVienDocSo.Text = CKhachHang.getNVDS(khachhang.LOTRINH.Substring(3, 2));
                    
                    loadghichu(khachhang.DANHBO);
                    loadHoaDon(khachhang.DANHBO);
                    loadDongNuoc(khachhang.DANHBO);
                }
                else
                {
                    TB_DULIEUKHACHHANG_HUYDB khachhanghuy = DAL.KhachHang.CKhachHang.finByDanhBoHuy(sodanhbo);
                    if (khachhanghuy != null)
                    {
                        rDanhBo.Text = khachhanghuy.DANHBO;
                        LOTRINH.Text = khachhanghuy.LOTRINH;
                        DOT.Text = khachhanghuy.DOT;
                        HOPDONG.Text = khachhanghuy.HOPDONG;
                        HOTEN.Text = khachhanghuy.HOTEN;
                        TENDUONG.Text = khachhanghuy.SONHA + " " +khachhanghuy.TENDUONG;
                         QUAN.Text = khachhanghuy.QUAN + "  " +khachhanghuy.PHUONG;
                        txtHieuLuc.Text = "Hết HL " + khachhanghuy.HIEULUCHUY;
                        GIABIEU.Text = khachhanghuy.GIABIEU;
                        DINHMUC.Text = khachhanghuy.DINHMUC;
                        NGAYGAN.ValueObject = khachhanghuy.NGAYTHAY;
                        KIEMDINH.ValueObject = khachhanghuy.NGAYKIEMDINH;
                        HIEUDH.Text = khachhanghuy.HIEUDH;
                        CO.Text = khachhanghuy.CODH;
                        CAP.Text = khachhanghuy.CAP;
                        SOTHAN.Text = khachhanghuy.SOTHANDH;
                        VITRI.Text = khachhanghuy.VITRIDHN;
                        //CHITHAN.Text = khachhanghuy.CHITHAN;
                        //CHIGOC.Text = khachhanghuy.CHIGOC;
                        //btCapNhatThongTin.Enabled = false;

                        loadghichu(khachhanghuy.DANHBO);
                        loadHoaDon(khachhanghuy.DANHBO);
                        loadDongNuoc(khachhanghuy.DANHBO);
                    }
                    else
                    {

                        MessageBox.Show(this, "Không Tìm Thấy Thông Tin !", "..: Thông Báo :..", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        //Refesh();
                    }
                }
            }
        }

        public void loadghichu(string danhbo)
        {
            lichsuGhiCHu.DataSource = DAL.KhachHang.CKhachHang.lisGhiChu(danhbo);
            for (int i = 0; i < lichsuGhiCHu.Rows.Count; i++)
            {
                if (i % 2 == 0)
                {
                    lichsuGhiCHu.Rows[i].DefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(245)))), ((int)(((byte)(217)))));
                }
                else
                {
                    lichsuGhiCHu.Rows[i].DefaultCellStyle.BackColor = System.Drawing.Color.White;
                }
            }
        }
        public void loadDongNuoc(string danhbo)
        {
            gDongNuoc.DataSource = DAL.KhachHang.CKhachHang.getDongMoiNuoc(danhbo);
            Utilities.DataGridV.formatRows(gDongNuoc);
        }
        public void loadHoaDon(string danhbo)
        {
            gHoaDon.DataSource = DAL.KhachHang.CKhachHang.getListHoaDonReport(danhbo, 12);
            Utilities.DataGridV.formatRows(gHoaDon);
        }

        private void gSearch_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                string db = gSearch.Rows[gSearch.CurrentRow.Index].Cells["DC_DANHBA"].Value + "";
                LoadThongTinDB(db.Replace(" ",""));

            }
            catch (Exception)
            {
                
            }
        }

        private void btHoSoGoc_Click(object sender, EventArgs e)
        {
            if (CKhachHang.findByHoSoGoc(rDanhBo.Text.Replace("-", "")) != null)
            {
                frmViewPdf F = new frmViewPdf(rDanhBo.Text.Replace("-", ""));
                F.ShowDialog();
            }
            else
            {
                MessageBox.Show(this, "Hồ sơ gốc chưa được cập nhật !", "..: Thông Báo :..", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btTiepNhanKN_Click(object sender, EventArgs e)
        {
            string db = rDanhBo.Text.Replace("-", "");
            frmTiepNhanKN f = new frmTiepNhanKN(db,"KH");
            f.ShowDialog();
        }

        private void btDongNuoc_Click(object sender, EventArgs e)
        {
            string url = "http://hp_g7/callcenter.aspx?add=" + this.TENDUONG.Text;
            frmWeb F = new frmWeb(url);            
            F.ShowDialog();
        }

        
  
    }
}