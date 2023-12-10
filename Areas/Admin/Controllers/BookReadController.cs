using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using System.Xml;

namespace SellBookStore.Areas.Admin.Controllers
{
    public class BookReadController : Controller
    {
        // GET: Admin/BookRead
        public ActionResult Index(int masach)
        {
            String tensach = "";
            string xmlFilePath = Server.MapPath("~/App_Data/XML/books.xml");
            if (System.IO.File.Exists(xmlFilePath))
            {
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(xmlFilePath);
                XmlNodeList Nodes = xmlDoc.SelectNodes("//Book");
                foreach (XmlElement Node in Nodes)
                {
                    int BookID = int.Parse(Node.SelectSingleNode("BookID").InnerText);
                    if (BookID == masach)
                    {
                        tensach = Node.SelectSingleNode("FileBook").InnerText;
                    }
                }
            }
            ViewBag.NameFile = tensach;
            return View();
        }
    }
}