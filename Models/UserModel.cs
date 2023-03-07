using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Passenger.Models
{
   public class UserModel
   {
      [Key]
      public int user_id { get; set; }
      public string user_first_name { get; set; }
      public string user_last_name { get; set; }
      public string user_email_address { get; set; }
      public string user_password { get; set; }
      public string user_gender { get; set; }

      
      public UserModel()
      {

      }

   }
}
