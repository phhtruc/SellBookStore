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
    public class SignUpController : Controller
    {
        SellBookStoreContext db = new SellBookStoreContext();
        // GET: SignUp
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult AddAccount(Customers customers)
        {
            string xmlFilePath = Server.MapPath("~/App_Data/XML/users.xml");
            if (ModelState.IsValid)
            {
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(xmlFilePath);
                XmlElement userElement = xmlDoc.CreateElement("User");

                XmlElement customerIdElement = xmlDoc.CreateElement("CustomerID");
                customerIdElement.InnerText = GetNextUserId().ToString();
                userElement.AppendChild(customerIdElement);

                XmlElement firstNameElement = xmlDoc.CreateElement("FirstName");
                firstNameElement.InnerText = customers.FirstName;
                userElement.AppendChild(firstNameElement);

                XmlElement lastNameElement = xmlDoc.CreateElement("LastName");
                lastNameElement.InnerText = customers.LastName;
                userElement.AppendChild(lastNameElement);

                XmlElement roleElement = xmlDoc.CreateElement("RoleID");
                roleElement.InnerText = (customers.RoleId).ToString();
                userElement.AppendChild(roleElement);

                XmlElement emailElement = xmlDoc.CreateElement("Email");
                emailElement.InnerText = customers.Email;
                userElement.AppendChild(emailElement);

                XmlElement phoneElement = xmlDoc.CreateElement("Phone");
                phoneElement.InnerText = customers.Phone;
                userElement.AppendChild(phoneElement);

                XmlElement usernameElement = xmlDoc.CreateElement("Username");
                usernameElement.InnerText = customers.Username;
                userElement.AppendChild(usernameElement);

                XmlElement passwordElement = xmlDoc.CreateElement("Password");
                passwordElement.InnerText = customers.Password;
                userElement.AppendChild(passwordElement);

                xmlDoc.DocumentElement?.AppendChild(userElement);

                xmlDoc.Save(xmlFilePath);
                //db.Customers.Add(customers);
                //db.SaveChanges();
                //Session["UserName"] = customers.Username.ToString();
                return RedirectToRoute(new { controller = "SignIn", action = "Index" });
            }
            return View();
        }
        private int GetNextUserId()
        {
            string xmlFilePath = Server.MapPath("~/App_Data/XML/users.xml");
            int maxId = 0;
            if (System.IO.File.Exists(xmlFilePath))
            {
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(xmlFilePath);
                XmlNodeList userNodes = xmlDoc.SelectNodes("//User");
                foreach (XmlElement userNode in userNodes)
                {
                    int CustomerID = int.Parse(userNode.SelectSingleNode("CustomerID").InnerText);
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