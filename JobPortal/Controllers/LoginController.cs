using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using JobPortal.Models;
using MySql.Data.MySqlClient;
using System.IO;
using JobPortal.Models;

namespace JobPortal.Controllers
{
    public class LoginController : Controller
    {
        public string MysqlConnection = "server=localhost;Database=Test01;Uid=root;Pwd=";

        public ActionResult Index(Login login)
        {
            MySqlConnection conn = new MySqlConnection(MysqlConnection);
            MySqlCommand cmd;
            conn.Open();

            var data = new UserData();
            var model = new List<UserData>();

            bool IsAccountAvailable = CheckAccountAvailability(login.Name, login.Password);
            if (IsAccountAvailable == true)
            {
                cmd = conn.CreateCommand();
                cmd.CommandText = "select * from ApplicationData";

                //cmd.Parameters.AddWithValue("@Name", login.Name);
                //cmd.Parameters.AddWithValue("@Password", login.Password);
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    //byte[] imgData;

                    while (reader.Read())
                    {
                        
                        data.UserName = reader["Name"].ToString();
                        data.Email = reader["Email"].ToString();
                        data.Website = reader["WebAddress"].ToString();
                        data.CoverLetter = reader["CoverLetter"].ToString();
                        
//                      data.File = (reader["Resume"]);

                        model.Add(data);
                    }
                }

            }
            return View("DataShow",model);
        }

        private bool CheckAccountAvailability(string Name, string Password)
        {
            MySqlConnection conn = new MySqlConnection(MysqlConnection);
            MySqlCommand cmd;
            conn.Open();

            try
            {
                cmd = conn.CreateCommand();
                cmd.CommandText = "select email from Registration where Name = @Name and Password = @Password";

                cmd.Parameters.AddWithValue("@Name", Name);
                cmd.Parameters.AddWithValue("@Password", Password);
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        if (reader.HasRows)
                        {
                            return true;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return false;
        }

    }
}
