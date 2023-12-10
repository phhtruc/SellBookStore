using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SellBookStore.Areas.Admin.Controllers
{
    public class BooksController : Controller
    {
        // GET: Admin/Books
        public ActionResult Index()
        {
            return View();
        }
    }
}