using System;
using System.Collections.Generic;
using System.Text;

namespace CallCenter.Utilities
{
    class Doctien
    {
        public static string ReadGroup3(string G3)
        {
            string[] ReadDigit = new string[10] { " Không", " Một", " Hai", " Ba", " Bốn", " Năm", " Sáu", " Bảy", " Tám", " Chín" };
            string temp = "";
            if (G3 == "000") return "";
            temp = ReadDigit[int.Parse(G3[0].ToString())] + " Trăm";
            if (G3[1].ToString() == "0")
                if (G3[2].ToString() == "0") return temp;
                else
                {
                    temp += " Lẻ" + ReadDigit[int.Parse(G3[2].ToString())];
                    return temp;
                }
            else
                temp += ReadDigit[int.Parse(G3[1].ToString())] + " Mươi";

            if (G3[2].ToString() == "5") temp += " Lăm";
            else if (G3[2].ToString() != "0") temp += ReadDigit[int.Parse(G3[2].ToString())];
            return temp;


        }
        public static string ReadMoney(string Money)
        {
            string temp = "";
            try
            {
                
                while (Money.Length < 12)
                {
                    Money = "0" + Money;
                }
                string g1 = Money.Substring(0, 3);
                string g2 = Money.Substring(3, 3);
                string g3 = Money.Substring(6, 3);
                string g4 = Money.Substring(9, 3);
                if (g1 != "000")
                {
                    temp = ReadGroup3(g1);
                    temp += " Tỷ";
                }
                if (g2 != "000")
                {
                    temp += ReadGroup3(g2);
                    temp += " Triệu";
                }
                if (g3 != "000")
                {
                    temp += ReadGroup3(g3);
                    temp += " Ngàn";
                }
                temp = temp + ReadGroup3(g4);
                temp = temp.Replace("Một Mươi", "Mười");
                temp = temp.Trim();
                if (temp.IndexOf("Không Trăm") == 0)
                    temp = temp.Remove(0, 10);
                temp = temp.Trim();
                if (temp.IndexOf("Lẻ") == 0)
                    temp = temp.Remove(0, 2);
                temp = temp.Trim();
                temp = temp.Replace("Mươi Một", "Mươi Mốt");
                temp = temp.Trim();
                return temp.Substring(0, 1).ToUpper() + temp.Substring(1).ToLower() + " đồng ";

            }
            catch (Exception)
            {
                return "Không đồng";
            }


        }
    }
}
