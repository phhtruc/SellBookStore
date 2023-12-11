using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Xml;
using System.Data.SqlTypes;
using System.Xml.Linq;

namespace SellBookStore.Controllers
{
    public class ViewXMLController : Controller
    {
        private string connectionString = "Data Source=LAPTOP-9DPP351S;Initial Catalog=SellBookStore;Integrated Security=True";
        // GET: ViewXML
        public ActionResult Index()
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
                dataAdapter.Fill(dataSet);

                // Tạo XmlDocument để tạo tài liệu XML
                XmlDocument xmlDocument = new XmlDocument();
                // Tạo phần tử gốc của tài liệu XML
                XmlElement rootElement = xmlDocument.CreateElement("anh");
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
                string xmlFilePath = Server.MapPath("~/App_Data/XML/books.xml");
                xmlDocument.Save(xmlFilePath);
                string xmlContent = System.IO.File.ReadAllText(xmlFilePath);
                XDocument doc = XDocument.Load(xmlFilePath);
                // Đóng kết nối
                connection.Close();
                ViewBag.xml = xmlContent;
                return View(doc);
            }
        }
    }
}