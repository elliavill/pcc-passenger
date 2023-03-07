using Microsoft.AspNetCore.Mvc;
using Passenger.Models;
using System.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Data.SqlTypes;
using Microsoft.AspNetCore.Http;

namespace Passenger.Controllers
{
   public class HomeController : Controller
   {
      SqlConnection con = new SqlConnection();
      SqlCommand com = new SqlCommand();
      private SqlDataReader dr;

      // GET: Account
      [HttpGet]
      public ActionResult Login()
      {
         return View();
      }

      void connectionString()
      {
         con.ConnectionString = "data source=CSSQLSERVER; database=cs414_s23_team3; user id=cs414_s23_team3; password=pcc1234;";
      }

      public IActionResult Privacy()
      {
         return View();
      }

      public ActionResult Map()
      {
          return View();
      }

      [HttpPost]
      public ActionResult Verify(UserModel user)
      {
         try
         {
            connectionString();
            con.Open();
            com.Connection = con;
            com.CommandText = "select * from [User] where user_email_address='" + user.user_email_address + "' and user_password='" + user.user_password + "'";
            dr = com.ExecuteReader();
            if (dr.HasRows)
            {
               dr.Read();
               // store the the logged-in user id
               int userId = (int)dr["user_id"];
               HttpContext.Session.SetInt32("UserId", userId);
               TempData["message"] = "Login successful.";
               con.Close();
               return RedirectToAction("Display", "Trip");
            }
            else
            {
               con.Close();
               return View("Error");
            }
         }
         catch (Exception ex)
         {
            Console.WriteLine(ex.Message);
            return View("Error");
         }
      }
   }
}

