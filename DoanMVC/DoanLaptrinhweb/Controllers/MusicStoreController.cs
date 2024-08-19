using DoanLaptrinhweb.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace DoanLaptrinhweb.Controllers
{
    public class MusicStoreController : Controller
    {
        //Tao 1 doi tuong chua toan bo so CSDL tu dbQLbannhac
        QLbannhacEntities data = new QLbannhacEntities();

        private List<Nhac> Laynhacmoi(int count)
        {
            //Sap xep giam dan  theo NgayCapNhat, lay count dong dau
            return data.Nhacs.OrderByDescending(a => a.NgayCapNhat).Take(count).ToList();
        }
        public ActionResult Index()
        {
            //Lay 10 bai nhac moi nhat
            var nhacmoi = Laynhacmoi(10);
            return View(nhacmoi);
        }
        public ActionResult Dangnhac()
        {
            var Dangnhac = from Dn in data.Dangnhacs select Dn;
            return PartialView(Dangnhac);
        }
        public ActionResult SPTheodangnhac(int id)
        {
            var Nhac = from s in data.Nhacs where s.MaDangnhac == id select s;
            return View(Nhac);
        }
        public ActionResult Details(int id)
        {
            var Nhac = from s in data.Nhacs
                       where s.MaNhac == id
                       select s;
            return View(Nhac.Single());
        }
    }
}