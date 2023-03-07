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
   public class TripController : Controller
   {
      /*
       * This function also retrieve the user_id from the session variable and use it to query the 'Trip' Table
       * It also read method of the crud operation. It lists all data from the Trip table
       */
      public IActionResult Display()
      {
         int userId = (int)HttpContext.Session.GetInt32("UserId");
         List<TripModel> trips = _context.Trip.ToList();
         // Retrieve the listTrip variable from the TempData object in HomeController
         List<TripModel> listTrip = TempData["ListTrip"] as List<TripModel>;

         String connectionString = "Data Source=CSSQLSERVER;Initial Catalog=cs414_s23_team3;User ID=cs414_s23_team3;Password=pcc1234";
         using (SqlConnection connection = new SqlConnection(connectionString))
         {
            connection.Open();
            String sql = "SELECT * FROM Trip WHERE user_id = @userId";
            using (SqlCommand command = new SqlCommand(sql, connection))
            {
               command.Parameters.AddWithValue("@userId", userId);
               using (var reader = command.ExecuteReader())
               {
                  while (reader.Read())
                  {
                     TripModel tripModel = new TripModel();
                     tripModel.trip_id = reader.GetInt32(0);
                     tripModel.trip_pickup_location = reader.GetString(1);
                     tripModel.trip_destination = reader.GetString(2);
                     /*tripModel.trip_fare = reader.GetString(3);
                     tripModel.trip_status = reader.GetString(4);
                     tripModel.trip_distance = reader.GetString(5);*/
                     tripModel.trip_start_time = reader.GetString(6);
                     tripModel.trip_end_time = reader.GetString(7);
                     tripModel.trip_date = reader.GetString(8);/*
                     tripModel.trip_availability = reader.GetString(9);
                     tripModel.payment_method = reader.GetString(10);*/
                     tripModel.user_id = reader.GetInt32(11);
                      /*listTrip.Add(tripModel);*/
                  }
               }
            }
         }
         return View(trips);
      }

      private readonly ApplicationDbContext _context;

      // Constructor
      public TripController(ApplicationDbContext context) // managed by dependency injectipm
      {
         _context = context;
      }

      public IActionResult Create()
      {
         TripModel Trip = new TripModel();
         return View(Trip);
      }

      public IActionResult Details(int id)
      {
         var trip = _context.Trip.FirstOrDefault(t => t.trip_id == id);
         if (trip == null)
         {
            return NotFound();
         }

         var model = new TripModel
         {
            trip_id = trip.trip_id,
            trip_destination = trip.trip_destination,
            trip_pickup_location = trip.trip_pickup_location,
            trip_fare = trip.trip_fare,
            trip_status = trip.trip_status,
            trip_distance = trip.trip_distance,
            trip_start_time = trip.trip_start_time,
            trip_end_time = trip.trip_end_time,
            trip_date = trip.trip_date,
            trip_availability = trip.trip_availability,
            payment_method = trip.payment_method,
            Users = trip.Users
         };

         return View(model);
      }


      public IActionResult Edit(int id)
      {
         TripModel trip = GetTrip(id);
         return View(trip);
      }

      [HttpPost]
      public IActionResult Edit(TripModel trip)
      {
         try
         {
            _context.Trip.Attach(trip);
            _context.Entry(trip).State = EntityState.Modified;
            _context.SaveChanges();
         }
         catch
         {

         }
         return RedirectToAction(nameof(Display));
      }

      [HttpPost]
      public IActionResult Create(TripModel model) {
         // Check if the model state is valid
         if (!ModelState.IsValid)
         {
            return BadRequest(ModelState);
         }
         
         // Get the currently logged-in user ID
         int userId = (int)HttpContext.Session.GetInt32("UserId");
         // Retrieve the listTrip variable from the TempData object in HomeController
         List<TripModel> listTrip = TempData["ListTrip"] as List<TripModel>;
         if (userId == 0)
         {
            return BadRequest("User ID is null");
         }

         // Create a new trip instance with the provided data and the user ID
         TripModel trip = new TripModel
         {
            trip_destination = model.trip_destination,
            trip_pickup_location = model.trip_pickup_location,
            trip_status = model.trip_status,
            trip_distance = model.trip_distance,
            trip_start_time = model.trip_start_time,
            trip_end_time = model.trip_end_time,
            trip_date = model.trip_date,
            trip_availability = model.trip_availability,
            payment_method = model.payment_method,
            user_id = Convert.ToInt32(model.user_id)
         };

         // Add the trip to the database
         _context.Trip.Add(trip);
         _context.SaveChanges();

         // Return a success response
         /* return View(trip);*/
         return RedirectToAction(nameof(Display));
         /*return View("Create", new CreateTripModel()); >>>> this is if the user make the multiple trip in the same time*/
      }

      public TripModel GetTrip(int id)
      {
         TripModel trip = _context.Trip.Where(u => u.trip_id == id).FirstOrDefault();
         return(trip);
      }
   }
}
