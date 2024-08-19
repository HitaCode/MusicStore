using DoanLaptrinhweb.Models;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using Microsoft.Win32.SafeHandles;
using Microsoft.Ajax.Utilities;
using System.Drawing.Text;
using System.Web.UI.WebControls;

namespace DoanLaptrinhweb.Controllers
{
    public class AdminController : Controller
    {
        QLbannhacEntities data = new QLbannhacEntities();
        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Nhac(int ?page)
        {
            int pageNumber = (page ?? 1);
            int pageSize = 7;
            //return View(data.Nhacs.ToList());
            return View(data.Nhacs.ToList().OrderBy(n => n.MaNhac).ToPagedList(pageNumber, pageSize));
        }
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(FormCollection collection)
        {
            var tendn = collection["username"];
            var matkhau = collection["password"];
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
                Admin ad = data.Admins.SingleOrDefault(n => n.UserAdmin == tendn && n.PassAdmin == matkhau);
                if (ad != null)
                {
                    Session["Taikhoanadmin"] = ad;
                    return RedirectToAction("Index", "Admin");
                }
                else
                    ViewBag.Thongbao = "Tên đăng nhập hoặc mật khẩu không đúng";
            }
            return View();
            
        }
        [HttpGet]
        public ActionResult ThemmoiNhac()
        {
            ViewBag.MaDangnhac = new SelectList(data.Dangnhacs.ToList().OrderBy(n => n.TenDangnhac), "MaDangnhac", "TenDangnhac");
            return View();
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult ThemmoiNhac(Nhac nhac, HttpPostedFileBase fileUpload)
        {
            ViewBag.MaDangnhac = new SelectList(data.Dangnhacs.ToList().OrderBy(n => n.TenDangnhac), "MaDangnhac", "TenDangnhac");
            if (fileUpload == null)
            {
                ViewBag.Thongbao = "Vui lòng chọn ảnh bìa";
                return View();
            }
            else
            {
                if (ModelState.IsValid)
                {
                    var fileName = Path.GetFileName(fileUpload.FileName);
                    var path = Path.Combine(Server.MapPath("~/Hinhsanpham"), fileName);
                  
                    ViewBag.MaDangnhac = new SelectList(data.Dangnhacs.ToList().OrderBy(n => n.TenDangnhac), "MaDangnhac", "TenDangnhac");
                    if (ModelState.IsValid)
                    {
                        data.Nhacs.Add(nhac);
                        data.SaveChanges();
                    }
                    if (System.IO.File.Exists(path))
                        ViewBag.Thongbao = "Hình ảnh đã tồn tại";
                    else
                    {
                        fileUpload.SaveAs(path);
                    }

                }
                return RedirectToAction("Nhac");
            }
          
        }
        public ActionResult Chitietnhac (int id)
        {
            Nhac nhac = data.Nhacs.SingleOrDefault(n => n.MaNhac == id);
            ViewBag.MaNhac = nhac.MaNhac;
            if (nhac == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(nhac);
        }
        [HttpGet]
        public ActionResult Xoanhac(int id)
        {
            Nhac nhac = data.Nhacs.SingleOrDefault(n => n.MaNhac == id);
            ViewBag.MaNhac = nhac.MaNhac;
            if (nhac == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(nhac);
        }
        [HttpPost,ActionName("Xoanhac")]
        public ActionResult Xacnhanxoa(int id)
        {
            Nhac nhac = data.Nhacs.SingleOrDefault(n => n.MaNhac == id);
            ViewBag.MaNhac = nhac.MaNhac;
            if (nhac == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            data.Nhacs.Remove(nhac);
            data.SaveChanges();
            return RedirectToAction("Nhac");
        }
        [HttpGet]
        public ActionResult Suanhac(int id)
        {
            Nhac nhac = data.Nhacs.SingleOrDefault(n => n.MaNhac == id);
            if (nhac == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            ViewBag.MaDangnhac = new SelectList(data.Dangnhacs.ToList().OrderBy(n => n.TenDangnhac), "MaDangnhac", "TenDangnhac");
            return View(nhac);

        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Suanhac(Nhac nhac, FormCollection f, HttpPostedFileBase fileUpload)
        {

            if (ModelState.IsValid)
            {
                if (fileUpload != null)
                {
                    var fileName = Path.GetFileName(fileUpload.FileName);
                    var path = Path.Combine(Server.MapPath("~/Hinhsanpham"), fileName);
                    path = Path.Combine(Server.MapPath("~/Hinhsanpham"), f["oldimage"]);
                    if (System.IO.File.Exists(path))
                    {
                        System.IO.File.Delete(path);
                        fileUpload.SaveAs(path);
                        nhac.AnhBia = fileName;
                    }
                }
                else
                {
                    nhac.AnhBia = f["oldimage"];
                }
                    data.Entry(nhac).State = System.Data.Entity.EntityState.Modified;
                    data.SaveChanges();
                return RedirectToAction("Nhac");
                }
            return View(nhac);
            }
        public ActionResult Taikhoan(int? page)
        {
            int pageNumber = (page ?? 1);
            int pageSize = 7;
            return View(data.KhachHangs.ToList().OrderBy(n => n.MaKH).ToPagedList(pageNumber, pageSize));
        }
        public ActionResult ThemmoiKH()
        {
            return View();
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult ThemmoiKH(KhachHang khachhang)
        {
             data.KhachHangs.Add(khachhang);
             data.SaveChanges();
          
         return RedirectToAction("Taikhoan");
        }
        public ActionResult ChitietKH(int id)
        {
            KhachHang khachhang = data.KhachHangs.SingleOrDefault(n => n.MaKH == id);
            ViewBag.MaKH = khachhang.MaKH;
            if (khachhang == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(khachhang);
        }
        public ActionResult XoaKH(int id)
        {
            KhachHang khachhang = data.KhachHangs.SingleOrDefault(n => n.MaKH == id);
            ViewBag.MaKH = khachhang.MaKH;
            if (khachhang == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(khachhang);
        }
        [HttpPost, ActionName("XoaKH")]
        public ActionResult XacnhanxoaKH(int id)
        {
            KhachHang khachhang = data.KhachHangs.SingleOrDefault(n => n.MaKH == id);
            ViewBag.MaKH = khachhang.MaKH;
            if (khachhang == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            data.KhachHangs.Remove(khachhang);
            data.SaveChanges();
            return RedirectToAction("Taikhoan");
        }
        [HttpGet]
        public ActionResult SuaKH(int id)
        {
            KhachHang khachhang = data.KhachHangs.SingleOrDefault(n => n.MaKH == id);
            if (khachhang == null)
            {
                Response.StatusCode = 404;
                return null;
            }

            return View(khachhang);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult SuaKH(KhachHang khachHang, FormCollection f)
        {
            if (ModelState.IsValid)
            {
                data.Entry(khachHang).State = System.Data.Entity.EntityState.Modified;
                data.SaveChanges();
            }
            ViewBag.MaKH = khachHang.MaKH;
            return RedirectToAction("Taikhoan");
        }
        public ActionResult Donhang(int? page)
        {
            int pageNumber = (page ?? 1);
            int pageSize = 7;
            return View(data.DonDatHangs.ToList().OrderBy(n => n.SoDH).ToPagedList(pageNumber, pageSize));
        }
        public ActionResult ThemDH()
        {
            ViewBag.MaKH = new SelectList(data.KhachHangs.ToList().OrderBy(n => n.HoTen), "MaKH", "HoTen");
            return View();
        }
        [HttpPost]
        [ValidateInput(false)]

        public ActionResult ThemDH(DonDatHang dondathang)
        {
            ViewBag.MaKH = new SelectList(data.KhachHangs.ToList().OrderBy(n => n.HoTen), "MaKH", "HoTen");
            data.DonDatHangs.Add(dondathang);
            data.SaveChanges();

            return RedirectToAction("Donhang");
        }
        public ActionResult ChitietDH(int id)
        {
            DonDatHang dondathang = data.DonDatHangs.SingleOrDefault(n => n.SoDH == id);
            ViewBag.SoDH = dondathang.SoDH;
            if (dondathang == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(dondathang);
        }
        public ActionResult XoaDH(int id)
        {
            DonDatHang dondathang = data.DonDatHangs.SingleOrDefault(n => n.SoDH == id);
            ViewBag.SoDH = dondathang.SoDH;
            if (dondathang == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(dondathang);
        }
        [HttpPost, ActionName("XoaDH")]
        public ActionResult XacnhanxoaDH(int id)
        {
            DonDatHang dondathang = data.DonDatHangs.SingleOrDefault(n => n.SoDH == id);
            ViewBag.SoDH = dondathang.SoDH;
            if (dondathang == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            data.DonDatHangs.Remove(dondathang);
            data.SaveChanges();
            return RedirectToAction("Donhang");
        }
        [HttpGet]
        public ActionResult SuaDH(int id)
        {
            DonDatHang dondathang = data.DonDatHangs.SingleOrDefault(n => n.SoDH == id);
            if (dondathang == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            ViewBag.MaKH = new SelectList(data.KhachHangs.ToList().OrderBy(n => n.HoTen), "MaKH", "HoTen");
            return View(dondathang);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult SuaDH(DonDatHang dondathang, FormCollection f)
        {
            if (ModelState.IsValid)
            {
                data.Entry(dondathang).State = System.Data.Entity.EntityState.Modified;
                data.SaveChanges();
            }
            ViewBag.MaKH = new SelectList(data.KhachHangs.ToList().OrderBy(n => n.HoTen), "MaKH", "HoTen");
            ViewBag.SoDH = dondathang.SoDH;
            return RedirectToAction("Donhang");
        }
    }
}


   
 
