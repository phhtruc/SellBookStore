using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SellBookStore.Areas.Admin.Controllers
{
    public class BookReadController : Controller
    {
        // GET: Admin/BookRead
        public ActionResult Index()
        {
            return View();
        }
    }
}