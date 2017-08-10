using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using JobPortal.Models;
using MySql.Data.MySqlClient;

namespace JobPortal.Controllers
{
    public class RegistrationController : Controller
    {
        public string MysqlConnection = "server=localhost;Database=Test01;Uid=root;Pwd=";
        [HttpPost]
        public ActionResult Index(RegistrationModel register)
        {
            MySqlConnection conn = new MySqlConnection(MysqlConnection);
            MySqlCommand cmd;
            conn.Open();

            bool IsEmailAvailable = CheckEmailAvailability(register.Email);

            if (IsEmailAvailable == false)
            {
                try
                {
                    cmd = conn.CreateCommand();
                    cmd.CommandText = "Insert into Registration(Name, Email, Password, Repassword) values(@Name, @Email, @Password, @Repassword)";
                    cmd.Parameters.AddWithValue("@Name", register.Name);
                    cmd.Parameters.AddWithValue("@Email", register.Email);
                    cmd.Parameters.AddWithValue("@Password", register.Password);
                    cmd.Parameters.AddWithValue("@Repassword", register.RePassword);
                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }

                finally
                {
                    conn.Close();
                }
            }

            else
            {
                TempData["Message"] = "Email id is already registred..!";
            }
            return RedirectToAction("Registration", "Home");
        }

        private bool CheckEmailAvailability(string email)
        {
            MySqlConnection conn = new MySqlConnection(MysqlConnection);
            MySqlCommand cmd;
            conn.Open();

            try
            {
                cmd = conn.CreateCommand();
                cmd.CommandText = "select email from Registration where Email = @Email";

                cmd.Parameters.AddWithValue("@Email", email);
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
