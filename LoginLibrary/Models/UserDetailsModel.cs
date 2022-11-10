using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoginLibrary.Models
{
    [Keyless]
    public class UserDetailsModel
    {
        public int? user_id { get; set; }
        public string? user_name { get; set; }
        public string? email { get; set; }
        public string? user_password { get; set; }
        public string? phone { get; set; }
        public string? address { get; set; }
        public int? role_id { get; set; }
        public Result result { get; set; }
    }
}
