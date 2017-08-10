using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using JobPortal.Models;
using System.IO;
using System.Windows.Forms;
using MySql.Data;
using MySql.Data.MySqlClient;


namespace JobPortal.Controllers
{
    public class IndexController : Controller
    {
        public string MysqlConnection = "server=localhost;Database=Test01;Uid=root;Pwd=";
        
        public ActionResult Index()
        {
            return View();
        }



        [HttpPost]
        public ActionResult Submit(IEnumerable<HttpPostedFileBase> File, Index index)
        {
            byte[] file;
            using (var stream = new FileStream(index.FileName, FileMode.Open, FileAccess.Read))
            {
                using (var reader = new BinaryReader(stream))
                {
                    file = reader.ReadBytes((int)stream.Length);
                }
            }

            bool emailCheck = CheckEmailAvailability(index.Email);
            if (emailCheck == false)
            {

                MySqlConnection conn = new MySqlConnection(MysqlConnection);
                MySqlCommand cmd;
                conn.Open();

                try
                {
                    cmd = conn.CreateCommand();
                    cmd.CommandText = "Insert into ApplicationData(Name, Email, WebAddress, CoverLetter, Resume, Working) values(@Name, @Email, @WebAddress, @CoverLetter, @Resume, @Working)";
                    cmd.Parameters.AddWithValue("@Name", index.UserName);
                    cmd.Parameters.AddWithValue("@Email", index.Email);
                    cmd.Parameters.AddWithValue("@WebAddress", index.Website);
                    cmd.Parameters.AddWithValue("@CoverLetter", index.CoverLetter);
                    cmd.Parameters.AddWithValue("@Resume", file);
                    cmd.Parameters.AddWithValue("@Working", index.WorkWithUs);
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
            return RedirectToAction("Index", "Home");
        }

        private bool CheckEmailAvailability(string email)
        {
            MySqlConnection conn = new MySqlConnection(MysqlConnection);
            MySqlCommand cmd;
            conn.Open();

            try
            {
                cmd = conn.CreateCommand();
                cmd.CommandText = "select email from ApplicationData where Email = @Email";

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