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
    public class AddBookController : Controller
    {
        // GET: Admin/AddBook
        SellBookStoreContext db = new SellBookStoreContext();
        public ActionResult Index()
        {
            var listcate = db.Catetory.ToList();
            ViewBag.ListCate = listcate;
            return View();
        }
        public ActionResult ThemSachMoi(Books book, HttpPostedFileBase uploadhinh, string selectedValue, HttpPostedFileBase file)
        {

            /*if (ModelState.IsValid)
            {

                if (uploadhinh != null && uploadhinh.ContentLength > 0)
                {
                    string filename = "";
                    filename = "book"  + uploadhinh.FileName;
                    string path = Path.Combine(Server.MapPath("~/Assets/images/browse-books"), filename);
                    uploadhinh.SaveAs(path);
                    book.Image = filename;
                }
                if (file != null && file.ContentLength > 0)
                {
                    string filename = "";
                    filename = "bookfile" + file.FileName;
                    string path = Path.Combine(Server.MapPath("~/Assets/BooksFilePdf"), filename);
                    file.SaveAs(path);
                    book.FileBook = filename;
                }
                book.CateId = int.Parse(selectedValue);
                db.Books.Add(book);
                db.SaveChanges();
                return RedirectToRoute(new { controller = "Books", action = "Index" });
            }*/
            try
            {

                string xmlFilePath = Server.MapPath("~/App_Data/XML/books.xml");
                if (ModelState.IsValid)
                {
                    XmlDocument xmlDoc = new XmlDocument();
                    xmlDoc.Load(xmlFilePath);
                    XmlElement bookElement = xmlDoc.CreateElement("Book");

                    XmlElement bookIdElement = xmlDoc.CreateElement("BookID");
                    bookIdElement.InnerText = GetNextUserId().ToString();
                    bookElement.AppendChild(bookIdElement);

                    XmlElement titleElement = xmlDoc.CreateElement("Title");
                    titleElement.InnerText = book.Title;
                    bookElement.AppendChild(titleElement);

                    XmlElement authorElement = xmlDoc.CreateElement("Author");
                    authorElement.InnerText = book.Author;
                    bookElement.AppendChild(authorElement);

                    XmlElement priceElement = xmlDoc.CreateElement("Price");
                    priceElement.InnerText = (book.Price).ToString();
                    bookElement.AppendChild(priceElement);



                    if (uploadhinh != null && uploadhinh.ContentLength > 0)
                    {
                        string filename = "";
                        filename = "book" + uploadhinh.FileName;
                        string path = Path.Combine(Server.MapPath("~/Assets/images/browse-books"), filename);
                        uploadhinh.SaveAs(path);
                        XmlElement imageElement = xmlDoc.CreateElement("Image");
                        imageElement.InnerText = filename;
                        bookElement.AppendChild(imageElement);
                    }

                    XmlElement cateIDElement = xmlDoc.CreateElement("CateID");
                    cateIDElement.InnerText = book.CateId.ToString();
                    bookElement.AppendChild(cateIDElement);

                    XmlElement motaElement = xmlDoc.CreateElement("mota");
                    motaElement.InnerText = book.Mota;
                    bookElement.AppendChild(motaElement);

                    if (file != null && file.ContentLength > 0)
                    {
                        string filename = "";
                        filename = "bookfile" + file.FileName;
                        string path = Path.Combine(Server.MapPath("~/Assets/BooksFilePdf"), filename);
                        file.SaveAs(path);
                        XmlElement fileElement = xmlDoc.CreateElement("Filebook");
                        fileElement.InnerText = filename;
                        bookElement.AppendChild(fileElement);
                    }
                    xmlDoc.DocumentElement?.AppendChild(bookElement);

                    xmlDoc.Save(xmlFilePath);
                    //db.Customers.Add(customers);
                    //db.SaveChanges();
                    //Session["UserName"] = customers.Username.ToString();
                    return RedirectToRoute(new { controller = "Books", action = "Index" });
                }
            }
            catch (Exception ex)
            {
                return RedirectToRoute(new { controller = "AddBook", action = "Index" });
            }
            return View();
        }
        private int GetNextUserId()
        {
            string xmlFilePath = Server.MapPath("~/App_Data/XML/books.xml");
            int maxId = 0;
            if (System.IO.File.Exists(xmlFilePath))
            {
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(xmlFilePath);
                XmlNodeList userNodes = xmlDoc.SelectNodes("//Book");
                foreach (XmlElement userNode in userNodes)
                {
                    int CustomerID = int.Parse(userNode.SelectSingleNode("BookID").InnerText);
                    if (CustomerID > maxId)
                    {
                        maxId = CustomerID;
                    }
                }
            }
            return maxId + 1;
        }
    }

}