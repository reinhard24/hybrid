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
    public class LoginModel
    {
        public string user_name { get; set; }
        public string user_password { get; set; }
    }

    [Keyless]
    public class Result
    {
        public bool result { get; set; }
        public string message { get; set; }
    }
}
