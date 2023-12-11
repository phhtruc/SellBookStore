using SellBookStore.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Xml;

namespace SellBookStore.Areas.Admin.Controllers
{
    public class UpdateController : Controller
    {
        // GET: Admin/Update
        public ActionResult Index(int masanpham)
        {
            Books book = new Books();
            XmlDocument xmlDoc = new XmlDocument();
            string xmlFilePath = Server.MapPath("~/App_Data/XML/books.xml");
            xmlDoc.Load(xmlFilePath);
            XmlNode bookNode = xmlDoc.SelectSingleNode("//Book[BookID=" + masanpham + "]");
            book.BookId = masanpham;
            book.Title = bookNode.SelectSingleNode("Title").InnerText;
            book.Author = bookNode.SelectSingleNode("Author").InnerText;
            book.Price = Decimal.Parse(bookNode.SelectSingleNode("Price").InnerText);
            book.Mota = bookNode.SelectSingleNode("mota").InnerText;
            book.Image = bookNode.SelectSingleNode("Image").InnerText;
            book.FileBook = bookNode.SelectSingleNode("FileBook").InnerText;
            return View(book);
        }
        [Route("SuaSach")]
        public ActionResult SuaSach(Books book, HttpPostedFileBase uploadhinh, string selectedValue, HttpPostedFileBase file)
        {
            //if (ModelState.IsValid)
            //{
            //    db.Update(book);
            //    db.SaveChanges();
            //    return RedirectToRoute(new { controller = "Books", action = "Index" });
            //}
            XmlDocument xmlDoc = new XmlDocument();
            string xmlFilePath = Server.MapPath("~/App_Data/XML/books.xml");
            if (System.IO.File.Exists(xmlFilePath))
            {
                try
                {
                    xmlDoc.Load(xmlFilePath);
                    XmlNode node = xmlDoc.SelectSingleNode("//Book[BookID='" + book.BookId.ToString() + "']");
                    XmlNodeList bookNodes = xmlDoc.SelectNodes("//Book");
                    if (node != null)
                    {
                        node.SelectSingleNode("Title").InnerText = book.Title;
                        node.SelectSingleNode("Author").InnerText = book.Author;
                        node.SelectSingleNode("Price").InnerText = book.Price.ToString();
                        node.SelectSingleNode("mota").InnerText = book.Mota;


                        if (uploadhinh != null && uploadhinh.ContentLength > 0)
                        {
                            string filename = "";
                            filename = "book" + uploadhinh.FileName;
                            string path = Path.Combine(Server.MapPath("~/Assets/images/browse-books"), filename);
                            uploadhinh.SaveAs(path);
                            node.SelectSingleNode("Image").InnerText = filename;
                        }
                        if (file != null && file.ContentLength > 0)
                        {
                            string filename = "";
                            filename = "bookfile" + file.FileName;
                            string path = Path.Combine(Server.MapPath("~/Assets/BooksFilePdf"), filename);
                            file.SaveAs(path);
                            node.SelectSingleNode("FileBook").InnerText = filename;
                        }

                        xmlDoc.Save(xmlFilePath);

                        return RedirectToRoute(new { controller = "Books", action = "Index" });
                    }
                }
                catch
                {


                    return RedirectToRoute(new { controller = "Update", action = "Index" });
                }

            }
            else
            {
                return RedirectToRoute(new { controller = "Books", action = "Index" });
            }

            return View();
        }
    }
}