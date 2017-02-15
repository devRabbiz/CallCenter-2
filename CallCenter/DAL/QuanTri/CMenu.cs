﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CallCenter.Database;

namespace CallCenter.DAL.QuanTri
{
    class CMenu : CCallCenter
    {
        public bool Them(Menu menu)
        {
            try
            {
                if (_db.Menus.Count() > 0)
                    menu.MaMenu = _db.Menus.Max(item => item.MaMenu) + 1;
                else
                    menu.MaMenu = 1;
                menu.CreateDate = DateTime.Now;
                menu.CreateBy = CNguoiDung.MaND;
                _db.Menus.InsertOnSubmit(menu);
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

        public bool Sua(Menu menu)
        {
            try
            {
                menu.ModifyDate = DateTime.Now;
                menu.ModifyBy = CNguoiDung.MaND;
                _db.SubmitChanges();
                return true;
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message, "Thông Báo", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                return false;
            }
        }

        public bool Xoa(Menu menu)
        {
            try
            {
                _db.Menus.DeleteOnSubmit(menu);
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

        public bool CheckExistByTenMenu(string TenMenu)
        {
            try
            {
                return _db.Menus.Any(item => item.TenMenu == TenMenu);
            }
            catch (Exception)
            {
                return false;
            }
        }

        public List<Menu> GetDS()
        {
            return _db.Menus.ToList();
        }

        public Menu GetByMaMenu(int MaMenu)
        {
            return _db.Menus.SingleOrDefault(item => item.MaMenu == MaMenu);
        }

        public Menu GetByTenMenu(string TenMenu)
        {
            return _db.Menus.SingleOrDefault(item => item.TenMenu == TenMenu);
        }
    }
}
