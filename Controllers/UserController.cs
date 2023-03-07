using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;
using Passenger.Data;
using Passenger.Models;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using System.Data.SqlClient;

namespace Passenger.Controllers
{
    public class UserController : Controller
    {
        SqlConnection con = new SqlConnection();
        SqlCommand com = new SqlCommand();

        void connectionString()
        {
            con.ConnectionString = "data source=CSSQLSERVER; database=cs414_s23_team3; user id=cs414_s23_team3; password=pcc1234;";
        }
        private readonly ApplicationDbContext _context;
        public UserController(ApplicationDbContext context)
        {
            _context = context;
        }
        //We might need the below code when we do an admin page
        // GET: Users 
        public async Task<IActionResult> Display()
        {
            return View(await _context.User.ToListAsync());
        }
        // GET: Users/Details/5
        public IActionResult Details(int id)
        {
            var user = _context.User.FirstOrDefault(u => u.user_id == id);

            var model = new UserModel
            {
                user_id = user.user_id,
                user_first_name = user.user_first_name,
                user_last_name = user.user_last_name,
                user_email_address = user.user_email_address,
                user_password = user.user_password
            };

            return View(model);
        }

        // GET: Users/Create
        public IActionResult Create()
        {
            UserModel user = new UserModel();
            return View(user);
        }

        // POST: Users/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
/*        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("user_id,user_first_name,user_last_name,user_email_address,user_password")] User user)
        {
            if (ModelState.IsValid)
            {
                _context.Add(user);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(user);
        }*/

        // GET: Users/Edit/5
        public IActionResult Edit(int id)
        {
            UserModel user = GetUser(id);
            return View(user);
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(UserModel user)
        {
            try
            {
                _context.User.Attach(user);
                _context.Entry(user).State = EntityState.Modified;
                _context.SaveChanges();
            }
            catch
            {

            }
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public IActionResult Create(UserModel model)
        {
            // Check if the model state is valid
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Create a new trip instance with the provided data and the user ID
            UserModel user = new UserModel
            {
                //user_id = model.user_id,
                user_first_name = model.user_first_name,
                user_last_name = model.user_last_name,
                user_email_address = model.user_email_address,
                user_password = model.user_password,
                user_gender = model.user_gender
            };
            connectionString();
            con.Open();
            com.Connection = con;
            com.CommandText = "INSERT INTO [User] (user_first_name, user_last_name, user_email_address, user_password, user_gender) VALUES (@user_first_name, @user_last_name, @user_email_address, @user_password, @user_gender)";
            //com.Parameters.AddWithValue("@user_id", user.user_id);
            com.Parameters.AddWithValue("@user_first_name", user.user_first_name);
            com.Parameters.AddWithValue("@user_last_name", user.user_last_name);
            com.Parameters.AddWithValue("@user_email_address", user.user_email_address);
            com.Parameters.AddWithValue("@user_password", user.user_password);
            com.Parameters.AddWithValue("@user_gender", user.user_gender);
            com.ExecuteNonQuery();
            con.Close();
            TempData["register"] = "User successfully registered";
            // Add the trip to the database
            //_context.User.Add(user);
            //_context.SaveChanges();
            //InsertUser(user);
            // Return a success response
            /* return View(trip);*/
            return RedirectToAction("Login", "Home");
            /*return View("Create", new CreateTripModel()); >>>> this is if the user make the multiple trip in the same time*/
        }


        // GET: Users/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.User
                .FirstOrDefaultAsync(m => m.user_id == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var user = await _context.User.FindAsync(id);
            _context.User.Remove(user);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Display));
        }

        private bool UserExists(int id)
        {
            return _context.User.Any(e => e.user_id == id);
        }
        private void InsertUser(UserModel user)
        {
            connectionString();
            con.Open();
            com.Connection = con;
            com.CommandText = "INSERT INTO [User] (user_id,user_first_name, user_last_name, user_email_address, user_password, user_gender) VALUES (@user_id, @user_first_name, @user_last_name, @user_email_address, @user_password, @user_gender)";
            com.Parameters.AddWithValue("@user_id", user.user_id);
            com.Parameters.AddWithValue("@user_first_name", user.user_first_name);
            com.Parameters.AddWithValue("@user_last_name", user.user_last_name);
            com.Parameters.AddWithValue("@user_email_address", user.user_email_address);
            com.Parameters.AddWithValue("@user_password", user.user_password);
            com.Parameters.AddWithValue("@user_gender", user.user_gender);
            com.ExecuteNonQuery();
            con.Close();
            TempData["register"] = "User successfully registered";
        }
        public UserModel GetUser(int id)
        {
            UserModel user = _context.User.Where(u => u.user_id == id).FirstOrDefault();
            return (user);
        }
    }
}
