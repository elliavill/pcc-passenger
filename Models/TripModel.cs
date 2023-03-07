using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Passenger.Models
{
    public class TripModel
    {
        [Key]
        [DatabaseGenerated(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity)]
        public int trip_id { get; set; }
        [Display(Name = "Pickup Location")]
        public string trip_pickup_location { get; set; }
        [Display(Name = "Destination")]
        public string trip_destination { get; set; }
        [Display(Name = "Fare")]
        public string trip_fare { get; set; }
        [Display(Name = "Status (Pending/In-Progress/Complete")]
        public string trip_status { get; set; }
        [Display(Name = "Distance")]
        public string trip_distance { get; set; }
        [Display(Name = "Pickup Time")]
        public string trip_start_time { get; set; }
        [Display(Name = "Dropoff Time")]
        public string trip_end_time { get; set; }
        [Display(Name = "Date")]
        public string trip_date { get; set; }
        [Display(Name = "Number of Seats")]
        public string trip_availability { get; set; }
        [Display(Name = "Payment Method")]
        public string payment_method { get; set; }
        
        [ForeignKey("Users")]
        [Display(Name = "Users")]
        public int user_id { get; set; }
        public virtual UserModel Users { get; set; }

        public TripModel()
        {

        }
    }
}
