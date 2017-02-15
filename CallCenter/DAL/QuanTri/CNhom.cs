using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CallCenter.Database;

namespace CallCenter.DAL.QuanTri
{
    class CNhom : CCallCenter
    {
        public bool Them(Nhom nhom)
        {
            try
            {
                if (_db.Nhoms.Count() > 0)
                    nhom.MaNhom = _db.Nhoms.Max(item => item.MaNhom) + 1;
                else
                    nhom.MaNhom = 1;
                nhom.CreateDate = DateTime.Now;
                nhom.CreateBy = CNguoiDung.MaND;
                _db.Nhoms.InsertOnSubmit(nhom);
                _db.SubmitChanges();
                return true;
            }
            catch (Exception ex)
            {
                Refresh();
                System.Windows.Forms.MessageBox.Show(ex.Message, "Thông Báo", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                return false;
            }
        }

        public bool Sua(Nhom nhom)
        {
            try
            {
                nhom.ModifyDate = DateTime.Now;
                nhom.ModifyBy = CNguoiDung.MaND;
                _db.SubmitChanges();
                return true;
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message, "Thông Báo", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                return false;
            }
        }

        public bool Xoa(Nhom nhom)
        {
            try
            {
                _db.Nhoms.DeleteOnSubmit(nhom);
                _db.SubmitChanges();
                return true;
            }
            catch (Exception ex)
            {
                Refresh();
                System.Windows.Forms.MessageBox.Show(ex.Message, "Thông Báo", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                return false;
            }
        }

        public List<Nhom> GetDS()
        {
            return _db.Nhoms.ToList();
        }

        public Nhom GetByMaNhom(int MaTT_Nhom)
        {
            return _db.Nhoms.SingleOrDefault(item => item.MaNhom == MaTT_Nhom);
        }

        public string GetTenNhomByMaNhom(int MaTT_Nhom)
        {
            return _db.Nhoms.SingleOrDefault(item => item.MaNhom == MaTT_Nhom).TenNhom;
        }
    }
}
