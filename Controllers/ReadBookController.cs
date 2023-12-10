using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SellBookStore.Controllers
{
    public class ReadBookController : Controller
    {
        // GET: ReadBook
        public ActionResult Index()
        {
            return View();
        }
    }
}