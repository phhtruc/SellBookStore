using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SellBookStore.Controllers
{
    public class XMLtoSQLController : Controller
    {
        // GET: XMLtoSQL
        private string connectionString = "Data Source=LAPTOP-9DPP351S;Initial Catalog=SellBookStore;Integrated Security=True";
        public ActionResult Index()
        {
            return View();
        }
        private int SaveToSql()
        {
            List<Customers> users = convertListUsers();
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    foreach (var user in users)
                    {
                        // Kiểm tra xem người dùng có tồn tại trong cơ sở dữ liệu hay không
                        string checkUserQuery = "SELECT COUNT(*) FROM Customers WHERE Username = @Username";
                        using (SqlCommand checkUserCommand = new SqlCommand(checkUserQuery, connection))
                        {
                            checkUserCommand.Parameters.AddWithValue("@Username", user.Username);

                            int userCount = (int)checkUserCommand.ExecuteScalar();

                            if (userCount > 0)
                            {
                                // Nếu người dùng tồn tại, thực hiện lệnh UPDATE
                                string updateQuery = "UPDATE Customers SET FirstName = @FirstName, LastName = @LastName, Email = @Email, Phone = @Phone, RoleID = @RoleID, Password = @Password WHERE Username = @Username";

                                using (SqlCommand command = new SqlCommand(updateQuery, connection))
                                {
                                    command.Parameters.AddWithValue("@FirstName", user.FirstName);
                                    command.Parameters.AddWithValue("@LastName", user.LastName);
                                    command.Parameters.AddWithValue("@Email", user.Email);
                                    command.Parameters.AddWithValue("@Phone", user.Phone);
                                    command.Parameters.AddWithValue("@RoleID", user.RoleId);
                                    command.Parameters.AddWithValue("@Username", user.Username);
                                    command.Parameters.AddWithValue("@Password", user.Password);
                                    // Thêm các tham số khác tùy thuộc vào cấu trúc bảng SQL của bạn

                                    command.ExecuteNonQuery();
                                }
                            }
                            else
                            {
                                string query = "INSERT INTO Customers (FirstName, LastName,Email,Phone,RoleID,Username,Password) " +
                                "VALUES (@FirstName, @LastName,@Email,@Phone,@RoleID,@Username,@Password)";

                                using (SqlCommand insertcommand = new SqlCommand(query, connection))
                                {
                                    insertcommand.Parameters.AddWithValue("@FirstName", user.FirstName);
                                    insertcommand.Parameters.AddWithValue("@LastName", user.LastName);
                                    insertcommand.Parameters.AddWithValue("@Email", user.Email);
                                    insertcommand.Parameters.AddWithValue("@Phone", user.Phone);
                                    insertcommand.Parameters.AddWithValue("@RoleID", user.RoleId);
                                    insertcommand.Parameters.AddWithValue("@Username", user.Username);
                                    insertcommand.Parameters.AddWithValue("@Password", user.Password);
                                    insertcommand.ExecuteNonQuery();
                                    // Thêm các tham số khác tùy thuộc vào cấu trúc bảng SQL của bạn
                                }
                            }
                        }
                    }
                }
                return 1;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }
        private List<Customers> convertListUsers()
        {
            List<Customers> userlist = new List<Customers>();

            string xmlFilePath = Server.MapPath("~/App_Data/XML/users.xml");
            if (System.IO.File.Exists(xmlFilePath))
            {

                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(xmlFilePath);

                XmlNodeList bookNodes = xmlDoc.SelectNodes("//User");
                foreach (XmlElement bookNode in bookNodes)
                {

                    Customers user = new Customers();
                    user.FirstName = bookNode.SelectSingleNode("FirstName").InnerText;
                    user.LastName = bookNode.SelectSingleNode("LastName").InnerText;
                    user.Phone = bookNode.SelectSingleNode("Phone").InnerText;
                    user.Email = bookNode.SelectSingleNode("Email").InnerText;
                    user.Username = bookNode.SelectSingleNode("Username").InnerText;
                    user.Password = bookNode.SelectSingleNode("Password").InnerText;
                    user.RoleId = int.Parse(bookNode.SelectSingleNode("RoleID").InnerText);
                    userlist.Add(user);
                }
            }
            return userlist;
        }
    }
}