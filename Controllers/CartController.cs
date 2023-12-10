using SellBookStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SellBookStore.Controllers
{
    public class CartController : Controller
    {
        // GET: Cart
        public ActionResult Index()
        {
            if (Session["UserName"] != null)
            {
                List<Books> booksList = Session["Cart"] as List<Books>;
                return View(booksList);
            }
            return RedirectToRoute(new { controller = "SignIn", action = "Index" });
        }
        public ActionResult RemoveCart(int masach)
        {
            if (Session["Cart"] != null)
            {
                var cart = (List<Books>)Session["Cart"];

                // Tìm sách cần xóa khỏi giỏ hàng
                var bookToRemove = cart.FirstOrDefault(b => b.BookId == masach);

                // Kiểm tra xem sách có tồn tại trong giỏ hàng không
                if (bookToRemove != null)
                {
                    // Xóa sách khỏi giỏ hàng
                    cart.Remove(bookToRemove);

                    // Cập nhật Session["Cart"]
                    Session["Cart"] = cart;
                }
            }
            return RedirectToRoute(new { controller = "Cart", action = "Index" });
        }
    }
}