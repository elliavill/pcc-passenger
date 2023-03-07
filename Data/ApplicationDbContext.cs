using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Passenger.Models;

namespace Passenger.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Passenger.Models.UserModel> User { get; set; }
        public DbSet<Passenger.Models.TripModel> Trip { get; set; }
   }
}
