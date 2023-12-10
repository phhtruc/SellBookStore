using SellBookStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SellBookStore.Controllers
{
    public class SanPhamTheoLoaiController : Controller
    {
        SellBookStoreContext db = new SellBookStoreContext();

        // GET: SanPhamTheoLoai
        public ActionResult Index(int maloai)
        {
            List<Books> listsanpham = db.Books.Where(x => x.CateId == maloai).OrderBy(x => x.Title).ToList();
            return View(listsanpham);
        }
    }
}