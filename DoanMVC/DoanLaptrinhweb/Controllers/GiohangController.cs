using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DoanLaptrinhweb.Models;
namespace DoanLaptrinhweb.Controllers
{
    public class GiohangController : Controller
    {
        //Tao doi tuong data chua du lieu tu model dbbannhac da tao.
        QLbannhacEntities data = new QLbannhacEntities();
        //Lay gio hang
        public List<Giohang> Laygiohang()
        {
            List<Giohang> listGiohang = Session["Giohang"] as List<Giohang>;
            if (listGiohang == null)
            {
                listGiohang = new List<Giohang>();
                Session["Giohang"] = listGiohang;
            }
            return listGiohang;
        }
        public ActionResult ThemGiohang(int iMaNhac, string strURL)
        {
            List<Giohang> listGiohang = Laygiohang();
            Giohang sanpham = listGiohang.Find(n => n.iMaNhac == iMaNhac);
            if (sanpham == null)
            {
                sanpham = new Giohang(iMaNhac);
                listGiohang.Add(sanpham);
                return Redirect(strURL);
            }
            else
            {
                sanpham.iSoluong++;
                return Redirect(strURL);
            }
        }
        private int TongSoLuong()
        {
            int iTongSoLuong = 0;
            List<Giohang> listGiohang = Session["Giohang"] as List<Giohang>;
            if (listGiohang != null)
            {
                iTongSoLuong = listGiohang.Sum(n => n.iSoluong);
            }
            return iTongSoLuong;
        }
        private double TongTien()
        {
            double iTongTien = 0;
            List<Giohang> listGiohang = Session["Giohang"] as List<Giohang>;
            if (listGiohang != null)
            {
                iTongTien = listGiohang.Sum(n => n.dThanhtien);
            }
            return iTongTien;
        }
        public ActionResult GioHang()
        {
            List<Giohang> listGiohang = Laygiohang();
            if (listGiohang.Count == 0)
            {
                return RedirectToAction("Index", "MusicStore");
            }
            ViewBag.Tongsoluong = TongSoLuong();
            ViewBag.Tongtien = TongTien();
            return View(listGiohang);
        }
        public ActionResult GiohangPartial()
        {
            ViewBag.Tongsoluong = TongSoLuong();
            ViewBag.Tongtien = TongTien();
            return PartialView();
        }
        public ActionResult XoaGiohang(int iMaSP)
        {
            List<Giohang> listGiohang = Laygiohang();
            Giohang sanpham = listGiohang.SingleOrDefault(n => n.iMaNhac == iMaSP);
            if (sanpham != null)
            {
                listGiohang.RemoveAll(n => n.iMaNhac == iMaSP);
                return RedirectToAction("GioHang");
            }
            if (listGiohang.Count == 0)
            {
                return RedirectToAction("Index", "MusicStore");
            }
            return RedirectToAction("GioHang");
        }
        public ActionResult CapnhatGiohang(int iMaSP, FormCollection f)
        {
            List<Giohang> listGiohang = Laygiohang();
            Giohang sanpham = listGiohang.SingleOrDefault(n => n.iMaNhac == iMaSP);
            if (sanpham != null)
            {
                sanpham.iSoluong = int.Parse(f["txtSoluong"].ToString());
            }
            return RedirectToAction("Giohang");
        }
        public ActionResult XoaTatcaGiohang()
        {
            List<Giohang> listGiohang = Laygiohang();
            listGiohang.Clear();
            return RedirectToAction("Index", "MusicStore");
        }
        [HttpGet]
        public ActionResult DatHang()
        {
            if (Session["Taikhoan"] == null || Session["Taikhoan"].ToString() == "")
            {
                return RedirectToAction("Dangnhap", "Nguoidung");
            }
            if (Session["Giohang"] == null)
            {
                return RedirectToAction("Index", "MusicStore");
            }
            List<Giohang> listGiohang = Laygiohang();
            ViewBag.Tongsoluong = TongSoLuong();
            ViewBag.Tongtien = TongTien();

            return View(listGiohang);
        }
        public ActionResult DatHang(FormCollection collection)
        {
            //Them Don hang
            DonDatHang ddh = new DonDatHang();
            KhachHang kh = (KhachHang)Session["Taikhoan"];
            List<Giohang> gh = Laygiohang();
            ddh.MaKH = kh.MaKH;
            ddh.NgayDH = DateTime.Now;
            var ngaygiao = String.Format("{0:MM/dd/yyyy}", collection["Ngaygiao"]);    
            ddh.NgayGiao = DateTime.Parse(ngaygiao);
            ddh.TinhTrangGiaoHang = false;
            ddh.DaThanhToan = false;
            data.DonDatHangs.Add(ddh);
            data.SaveChanges();
            //Them chi tiet don hang
            foreach (var item in gh)
            {
                ChiTietDatHang ctdh = new ChiTietDatHang();
                ctdh.SoDH = ddh.SoDH;
                ctdh.MaNhac = item.iMaNhac;
                ctdh.SoLuong = item.iSoluong;
                ctdh.DonGia = (decimal)item.dDongia;
                data.ChiTietDatHangs.Add(ctdh);
            }
            data.SaveChanges();
            Session["Giohang"] = null;
            return RedirectToAction("Xacnhandonhang", "Giohang");

        }
        public ActionResult Xacnhandonhang()
        {
            return View();
        }
    }
}
        