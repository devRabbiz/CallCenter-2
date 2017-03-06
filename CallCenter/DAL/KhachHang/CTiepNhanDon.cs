using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CallCenter.Database;
using System.Data.SqlClient;
using System.Data;
using System.Collections;
using CallCenter.Utilities;

namespace CallCenter.DAL.KhachHang
{
    class CTiepNhanDon
    {
        static dbCallCenterDataContext db = new dbCallCenterDataContext();

        public static List<LoaiTiepNhan> getLoaiTiepNhan(string loaikh)
        {
            var data = from tn in db.LoaiTiepNhans where tn.LoaiKH==loaikh select tn;
            ArrayList list = new ArrayList();
            //list.Add(new AddValueCombox("  Chọn Loại  ", ""));
            //foreach (var a in data)
            //{
            //    list.Add(new AddValueCombox(a.TenLoai, a.ID+""));
            //}
            return data.ToList();
        }

        public static string IdentityBienNhan()
        {
            string loaihs = "CT";
            string year = DateTime.Now.Year.ToString().Substring(2);
            string kytumacdinh = year + loaihs;


            string id = kytumacdinh + "000001";
            try
            {

                String_Indentity.String_Indentity obj = new String_Indentity.String_Indentity();
                dbCallCenterDataContext db = new dbCallCenterDataContext();
                db.Connection.Open();
                string sql = " SELECT MAX(SoHoSo) as 'SoHoSo' FROM TiepNhan    ORDER BY SoHoSo DESC";
                SqlDataAdapter adapter = new SqlDataAdapter(sql, db.Connection.ConnectionString);
                DataTable table = new DataTable();
                adapter.Fill(table);
                if (table.Rows.Count > 0)
                {
                    if (table.Rows[0][0].ToString().Trim().Substring(0, 2).Equals(year))
                    {
                        int number = 1;

                        id = obj.ID(kytumacdinh, table.Rows[0][0].ToString().Trim(), "000000", number) + "";
                    }
                    else
                    {
                        id = obj.ID(year + loaihs, year + loaihs + "000000", "000000") + "";
                    }
                }
                else
                {
                    id = obj.ID(kytumacdinh, table.Rows[0][0].ToString().Trim(), "000000") + "";
                }

                db.Connection.Close();

            }
            catch (Exception)
            {

            }

            return id;

        }

        public static bool Insert(TiepNhan tn)
        {
            try
            {
                db.TiepNhans.InsertOnSubmit(tn);
                db.SubmitChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static TiepNhan finbyMaTN(string mabn)
        {
            var query = from biennhan in db.TiepNhans where biennhan.SoHoSo == mabn select biennhan;
            return query.SingleOrDefault();
        }
        
        public static bool Update()
        {
            try
            {
                db.SubmitChanges();
                return true;
            }
            catch (Exception ex)
            {
            }
            return false;
        }
    }
}