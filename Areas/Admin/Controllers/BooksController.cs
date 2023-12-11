using SellBookStore.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Xml;
using System.Xml.Linq;

namespace SellBookStore.Areas.Admin.Controllers
{
    public class BooksController : Controller
    {
        // GET: Admin/Books
        SellBookStoreContext db = new SellBookStoreContext();
        public ActionResult Index()
        {
            List<Books> booksList = LoadBooksFromXml();
            var listCate = db.Catetory.ToList();
            ViewBag.Cate = listCate;
            return View(booksList);
        }
        private List<Books> LoadBooksFromXml()
        {
            List<Books> booksList = new List<Books>();

            string xmlFilePath = Server.MapPath("~/App_Data/XML/books.xml");

            if (System.IO.File.Exists(xmlFilePath))
            {

                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(xmlFilePath);

                XmlNodeList bookNodes = xmlDoc.SelectNodes("//Book");
                foreach (XmlElement bookNode in bookNodes)
                {

                    Books book = new Books();
                    book.BookId = int.Parse(bookNode.SelectSingleNode("BookID").InnerText);
                    book.Title = bookNode.SelectSingleNode("Title").InnerText;
                    book.Author = bookNode.SelectSingleNode("Author").InnerText;
                    book.Price = decimal.Parse(bookNode.SelectSingleNode("Price").InnerText);
                    book.Image = bookNode.SelectSingleNode("Image").InnerText;
                    book.Mota = bookNode.SelectSingleNode("mota").InnerText;
                    book.FileBook = bookNode.SelectSingleNode("FileBook").InnerText;
                    booksList.Add(book);
                }
            }
            else
            {
                ViewBag.XmlContent = "File not found.";
            }

            return booksList;
        }
        public ActionResult XoaSach(int masp)
        {
            //Books book = db.Books.Find(masp);
            //if (book != null)
            //{
            //    db.Books.Remove(book);
            //    db.SaveChanges();
            //    return RedirectToRoute(new { controller = "Books", action = "Index" });
            //}
            //else
            //{
            //    return RedirectToRoute(new { controller = "Admin", action = "Index" });
            //}

            // Tạo XDocument và tải tài liệu XML
            XmlDocument xmlDoc = new XmlDocument();
            string xmlFilePath = Server.MapPath("~/App_Data/XML/books.xml");
            if (System.IO.File.Exists(xmlFilePath))
            {
                xmlDoc.Load(xmlFilePath);
                XmlNode node = xmlDoc.SelectSingleNode("//Book[BookID='" + masp.ToString() + "']");
                if (node != null)
                {
                    node.ParentNode.RemoveChild(node);
                    xmlDoc.Save(xmlFilePath);
                    return RedirectToRoute(new { controller = "Books", action = "Index" });
                }
            }
            else
            {
                return RedirectToRoute(new { controller = "Books", action = "Index" });
            }
            return RedirectToRoute(new { controller = "Books", action = "Index" });
        }
    }
}