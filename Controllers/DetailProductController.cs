using SellBookStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Xml;

namespace SellBookStore.Controllers
{
    public class DetailProductController : Controller
    {
        // GET: DetailProduct
        SellBookStoreContext db = new SellBookStoreContext();
        public ActionResult Index(int bookId)
        {
            Books book = new Books();
            string xmlFilePath = Server.MapPath("~/App_Data/XML/books.xml");
            if (System.IO.File.Exists(xmlFilePath))
            {
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(xmlFilePath);
                XmlNodeList userNodes = xmlDoc.SelectNodes("//Book");
                foreach (XmlElement userNode in userNodes)
                {
                    int BookID = int.Parse(userNode.SelectSingleNode("BookID").InnerText);
                    if (bookId == BookID)
                    {
                        book.BookId = int.Parse(userNode.SelectSingleNode("BookID").InnerText);
                        book.Title = userNode.SelectSingleNode("Title").InnerText;
                        book.Author = userNode.SelectSingleNode("Author").InnerText;
                        book.Price = decimal.Parse(userNode.SelectSingleNode("Price").InnerText);
                        book.Image = userNode.SelectSingleNode("Image").InnerText;
                        book.Mota = userNode.SelectSingleNode("mota").InnerText;
                        book.FileBook = userNode.SelectSingleNode("FileBook").InnerText;
                    }
                }
            }
            return View(book);
        }
        public ActionResult AddToCart(int masach)
        {
            if (Session["UserName"] != null)
            {
                var book = db.Books.Find(masach);
                var cart = Session["Cart"] as List<Books> ?? new List<Books>();
                cart.Add(book);
                Session["Cart"] = cart;

                return RedirectToRoute(new { controller = "Cart", action = "Index" });
            }
            else
            {
                return RedirectToRoute(new { controller = "SignIn", action = "Index" });
            }
        }
        public ActionResult DownloadFile(int masach)
        {
            var book = db.Books.Find(masach);
            // Đường dẫn tới tệp tin trên máy chủ
            string filePath = Server.MapPath("~/Assets/BooksFilePdf/" + book.FileBook);
            string fileName = book.Title + ".pdf"; // Tên tệp tin khi tải về

            // Kiểm tra xem tệp tin có tồn tại không
            if (!System.IO.File.Exists(filePath))
            {
                return HttpNotFound();
            }

            // Đọc nội dung của tệp tin
            byte[] fileBytes = System.IO.File.ReadAllBytes(filePath);

            // Trả về một phản hồi để tải xuống tệp tin
            return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, fileName);
        }

    }
}