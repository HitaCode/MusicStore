using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Hosting;
using System.Web.Mvc;
using DoanLaptrinhweb.Models;

namespace DoanMVC.Controllers
{
    public class NguoidungController : Controller
    {
        QLbannhacEntities data = new QLbannhacEntities();
        // GET: Nguoidung
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult Dangky()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Dangky(FormCollection collection, KhachHang Kh)
        {
            var hoten = collection["HotenKH"];
            var tendn = collection["TenDN"];

            var matkhau = collection["Matkhau"];

            var matkhaunhaplai = collection["Matkhaunhaplai"];

            var diachi = collection["Diachi"];

            var email = collection["Email"];

            var dienthoai = collection["Dienthoai"];
            var ngaysinh = String.Format("{0:MM/dd/yyyy}", collection["Ngaysinh"]);
            if (String.IsNullOrEmpty(hoten))
            {
                ViewData["Loi1"] = "Họ tên khách hàng không được để trống";
            }
            else if (String.IsNullOrEmpty(tendn))
            {
                ViewData["Loi2"] = "Phải nhập tên đăng nhập";
            }
            else if (String.IsNullOrEmpty(matkhau))
            {
                ViewData["Loi3"] = "Phải nhập mật khẩu";
            }
            else if (String.IsNullOrEmpty(matkhaunhaplai))
            {
                ViewData["Loi4"] = "Phải nhập lại mật khẩu";
            }
            if (String.IsNullOrEmpty(email))
            {
                ViewData["Loi5"] = "Email không được bỏ trống";
            }
            if (String.IsNullOrEmpty(dienthoai))
            {
                ViewData["Loi1"] = "Phải nhập điện thoại";
            }
            else
            {
                Kh.HoTen = hoten;
                Kh.TaiKhoan = tendn;
                Kh.MatKhau = matkhau;
                Kh.Email = email;
                Kh.DiachiKH = diachi;
                Kh.DienThoaiKH = dienthoai;
                Kh.NgaySinh = DateTime.Parse(ngaysinh);
                data.KhachHangs.Add(Kh);
                data.SaveChanges();
                return RedirectToAction("Dangnhap");
            }
            return this.Dangky();
        }
        [HttpGet]
        public ActionResult Dangnhap()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Dangnhap(FormCollection collection)
        {
            // Gán các giá trị người dùng nhập liệu cho các biến
            var tendn = collection["TenDN"];
            var matkhau = collection["Matkhau"];
            if (String.IsNullOrEmpty(tendn))
            {
                ViewData["Loi1"] = "Phải nhập tên đăng nhập";
            }
            else if (String.IsNullOrEmpty(matkhau))
                {
                ViewData["Loi2"] = "Phải nhập mật khẩu";
                }
                else
                {
                     //Gần giá trị cho đối tượng được tạo mới (kh)
                     KhachHang Kh = data.KhachHangs.SingleOrDefault(n => n.TaiKhoan == tendn && n.MatKhau == matkhau);
                     if (Kh != null)
                     {
                         // ViewBag.Thongbao = "Chúc mừng đăng nhập thành công";
                         Session["TaiKhoan"] = Kh;
                         Session["Hoten"] = Kh.HoTen;
                    return RedirectToAction("Index", "MusicStore");

                }
                     else
                         ViewBag.Thongbao = "Tên đăng nhập hoặc mật khẩu không đúng";
            }
            return this.Dangnhap();
         

        }
    }
}