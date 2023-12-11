using SellBookStore.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Xml;

namespace SellBookStore.Controllers
{
    public class HomeController : Controller
    {
        private string connectionString = "Data Source=LAPTOP-9DPP351S;Initial Catalog=SellBookStore;Integrated Security=True";
        SellBookStoreContext db = new SellBookStoreContext();
        public ActionResult Index()
        {
            
            List<Books> booksList = LoadBooksFromXml();
            List<Customers> usersList = LoadUsersFromXml();
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
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    // Mở kết nối
                    connection.Open();

                    // Tạo SqlCommand để truy vấn dữ liệu từ cơ sở dữ liệu
                    string query = "SELECT * FROM Books";
                    SqlCommand command = new SqlCommand(query, connection);
                    // Tạo SqlDataAdapter để đọc dữ liệu từ cơ sở dữ liệu vào DataSet
                    SqlDataAdapter dataAdapter = new SqlDataAdapter(command);
                    DataSet dataSet = new DataSet();
                    DataSet dataSet1 = new DataSet();
                    dataAdapter.Fill(dataSet);

                    // Tạo XmlDocument để tạo tài liệu XML
                    XmlDocument xmlDocument = new XmlDocument();
                    // Tạo phần tử gốc của tài liệu XML
                    XmlElement rootElement = xmlDocument.CreateElement("ATB");
                    xmlDocument.AppendChild(rootElement);
                    // Duyệt qua các dòng trong DataSet và tạo các phần tử XML tương ứng
                    foreach (DataRow row in dataSet.Tables[0].Rows)
                    {
                        XmlElement rowElement = xmlDocument.CreateElement("Book");

                        // Duyệt qua các cột trong từng dòng
                        foreach (DataColumn column in dataSet.Tables[0].Columns)
                        {
                            XmlElement columnElement = xmlDocument.CreateElement(column.ColumnName);
                            columnElement.InnerText = row[column].ToString();
                            rowElement.AppendChild(columnElement);
                        }

                        rootElement.AppendChild(rowElement);

                        // Thêm một dòng trắng sau mỗi phần tử <Book>
                        xmlDocument.PreserveWhitespace = true;
                        XmlWhitespace whitespace = xmlDocument.CreateWhitespace("\n");
                        rootElement.AppendChild(whitespace);
                    }

                    // Lưu tài liệu XML vào đường dẫn cụ thể hoặc thực hiện các thao tác khác với XML
                    string xmlFilePathbook = Server.MapPath("~/App_Data/XML/books.xml");
                    xmlDocument.Save(xmlFilePathbook);
                    string xmlContent = System.IO.File.ReadAllText(xmlFilePathbook);
                    // Đóng kết nối
                    connection.Close();
                    ViewBag.xml = xmlContent;
                    return booksList;
                }
            }
            return booksList;
        }
        private List<Customers> LoadUsersFromXml()
        {
            List<Customers> usersList = new List<Customers>();

            string xmlFilePath = Server.MapPath("~/App_Data/XML/users.xml");
            if (System.IO.File.Exists(xmlFilePath))
            {

                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(xmlFilePath);

                XmlNodeList bookNodes = xmlDoc.SelectNodes("//Book");
                foreach (XmlElement bookNode in bookNodes)
                {

                    Customers user = new Customers();
                    user.CustomerId = int.Parse(bookNode.SelectSingleNode("CustomerID").InnerText);
                    user.FirstName = bookNode.SelectSingleNode("Title").InnerText;
                    user.LastName = bookNode.SelectSingleNode("Author").InnerText;
                    user.Email = (bookNode.SelectSingleNode("Email").InnerText);
                    user.Phone = bookNode.SelectSingleNode("Phone").InnerText;
                    user.Username = bookNode.SelectSingleNode("Username").InnerText;
                    user.Password = bookNode.SelectSingleNode("Password").InnerText;
                    user.RoleId = int.Parse(bookNode.SelectSingleNode("RoleID").InnerText);
                    usersList.Add(user);
                }
            }
            else
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    // Mở kết nối
                    connection.Open();

                    // Tạo SqlCommand để truy vấn dữ liệu từ cơ sở dữ liệu
                    string queryuser = "SELECT * FROM Customers";
                    SqlCommand command = new SqlCommand(queryuser, connection);
                    // Tạo SqlDataAdapter để đọc dữ liệu từ cơ sở dữ liệu vào DataSet
                    SqlDataAdapter dataAdapter = new SqlDataAdapter(command);
                    DataSet dataSet = new DataSet();
                    dataAdapter.Fill(dataSet);

                    // Tạo XmlDocument để tạo tài liệu XML
                    XmlDocument xmlDocument = new XmlDocument();
                    // Tạo phần tử gốc của tài liệu XML
                    XmlElement rootElement = xmlDocument.CreateElement("ATBUsers");
                    xmlDocument.AppendChild(rootElement);
                    // Duyệt qua các dòng trong DataSet và tạo các phần tử XML tương ứng
                    foreach (DataRow row in dataSet.Tables[0].Rows)
                    {
                        XmlElement rowElement = xmlDocument.CreateElement("User");

                        // Duyệt qua các cột trong từng dòng
                        foreach (DataColumn column in dataSet.Tables[0].Columns)
                        {
                            XmlElement columnElement = xmlDocument.CreateElement(column.ColumnName);
                            columnElement.InnerText = row[column].ToString();
                            rowElement.AppendChild(columnElement);
                        }

                        rootElement.AppendChild(rowElement);

                        // Thêm một dòng trắng sau mỗi phần tử <Book>
                        xmlDocument.PreserveWhitespace = true;
                        XmlWhitespace whitespace = xmlDocument.CreateWhitespace("\n");
                        rootElement.AppendChild(whitespace);
                    }
                    // Lưu tài liệu XML vào đường dẫn cụ thể hoặc thực hiện các thao tác khác với XML
                    string xmlFilePathuser = Server.MapPath("~/App_Data/XML/users.xml");
                    xmlDocument.Save(xmlFilePathuser);
                    string xmlContent = System.IO.File.ReadAllText(xmlFilePathuser);
                    // Đóng kết nối
                    connection.Close();
                    ViewBag.xml = xmlContent;
                    return usersList;
                }
            }
            return usersList;
        }
        private void resetFile()
        {
            string xmlFilePathuser = Server.MapPath("~/App_Data/XML/users.xml");
            string xmlFilePathbook = Server.MapPath("~/App_Data/XML/books.xml");
            if(System.IO.File.Exists(xmlFilePathuser)|| System.IO.File.Exists(xmlFilePathbook))
            {
                    try
                    {
                        // Xóa tệp XML
                        System.IO.File.Delete(xmlFilePathuser);
                        System.IO.File.Delete(xmlFilePathbook);
                    }
                    catch (Exception ex)
                    {
                    }
                }
            else
                {
                    ViewBag.Message = "Tệp XML không tồn tại.";
                }
            }
    }
}