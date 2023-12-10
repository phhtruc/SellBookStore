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
    public class SignInController : Controller
    {
        SellBookStoreContext db = new SellBookStoreContext();
        // GET: SignIn
        public ActionResult Index()
        {
            if (Session["UserName"] == null)
            {
                return View();
            }
            else
            {
                return RedirectToRoute(new { controller = "Home", action = "Index" });
            }
        }
        [HttpPost]
        public ActionResult ActionSignIn(Customers customer)
        {
            string xmlFilePath = Server.MapPath("~/App_Data/XML/users.xml");
            if (System.IO.File.Exists(xmlFilePath))
            {
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(xmlFilePath);
                XmlNodeList userNodes = xmlDoc.SelectNodes("//User");
                foreach (XmlElement userNode in userNodes)
                {
                    int RoleId = int.Parse(userNode.SelectSingleNode("RoleID").InnerText);
                    String Email = userNode.SelectSingleNode("Email").InnerText;
                    String Password = userNode.SelectSingleNode("Password").InnerText;
                    if (customer.Email.Equals(Email) && customer.Password.Equals(Password))
                    {

                        var route = new RouteValueDictionary();
                        if (RoleId == 1)
                        {
                            Session["UserName"] = userNode.SelectSingleNode("Username").ToString();
                            route = new RouteValueDictionary {
                                    { "controller", "Admin/Admin" },
                                    { "action", "Index" }, // Thêm đối tượng user vào route values
                                };
                        }
                        else
                        {
                            Session["UserName"] = userNode.SelectSingleNode("Username").ToString();
                            route = new RouteValueDictionary {
                                { "controller", "Home" },
                                    { "action", "Index" },
                                    { "user", userNode } // Thêm đối tượng user vào route values
                                };
                        };
                        return RedirectToRoute(route);

                    }
                }
                return RedirectToRoute(new { controller = "SignIn", action = "Index" });
            }
            /* (Session["UserName"] == null)
            {
                var u = db.Customers.Where(x => x.Email.Equals(customer.Email) && x.Password.Equals(customer.Password)).FirstOrDefault();
                if (u != null)
                {
                    var route = new RouteValueDictionary();
                    if (u.RoleId==1)
                    {
                        Session["UserName"] = u.Username.ToString();
                            route = new RouteValueDictionary {
                            { "controller", "Admin/Admin" },
                            { "action", "Index" }, // Thêm đối tượng user vào route values
                        };
                    }
                    else
                    {
                        Session["UserName"] = u.Username.ToString();
                        route = new RouteValueDictionary {
                        { "controller", "Home" },
                        { "action", "Index" },
                        { "user", u } // Thêm đối tượng user vào route values
                        };
                    
                    };

                    return RedirectToRoute(route);
                }
            }*/
            return View();
        }
        public ActionResult Logout()
        {
            Session.Clear();
            Session.Abandon();
            return RedirectToAction("Index", "Home");
        }
    }
}