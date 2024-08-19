using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DoanLaptrinhweb.Models
{
    public class Giohang
    {
        QLbannhacEntities data = new QLbannhacEntities();
        public int iMaNhac { set; get; }
        public string sTenNhac { set; get; }
        public string sAnhBia { set; get; }
        public Double dDongia { set; get; }
        public int iSoluong { set; get; }
        public Double dThanhtien
        {
            get { return iSoluong * dDongia; }
        }
        //Khoi tao gio hang theo MaNhac duoc chuyen vao voi Soluong mac dinh la 1
        public Giohang(int MaNhac)
        {
            iMaNhac = MaNhac;
            Nhac nhac = data.Nhacs.Single(n => n.MaNhac == iMaNhac);
            sTenNhac = nhac.TenNhac;
            sAnhBia = nhac.AnhBia;
            dDongia = double.Parse(nhac.GiaBan.ToString());
            iSoluong = 1;
        }
    }
}