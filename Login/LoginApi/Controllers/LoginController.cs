using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;
using LoginLibrary.Models;
using LoginLibrary.DataAccess;
using Npgsql;
using LoginLibrary;
using System.Xml.Linq;

namespace LoginApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LoginController : ControllerBase
    {
        private readonly LoginDataAccess _context;
        public LoginController(LoginDataAccess c)
            => _context = c;

        [HttpGet]
        public IActionResult Login([FromQuery] LoginViewModel users)
        {
            UserDetailsModel userdetails = new UserDetailsModel();
            userdetails.result = new Result();
            try
            {
                if (users != null && !string.IsNullOrWhiteSpace(users.user_name) && !string.IsNullOrWhiteSpace(users.user_password))
                {
                    NpgsqlConnection? conn = new NpgsqlConnection("Server=35.219.63.229;Port=5432;Database=hybridkitchen_db;User Id=postgres;Password=archsoft;");
                    using (conn)
                    {
                        conn.Open();
                        NpgsqlCommand cmd = new NpgsqlCommand("sp_login", conn);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("_user_name", users.user_name);
                        cmd.Parameters.AddWithValue("_user_password", users.user_password);
                        NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);

                        int intResult = (int)cmd.ExecuteScalar();
                        conn.Close();

                        if (intResult == 1) // Login successful
                        {
                            // Code if login is successful
                            return Ok("Successful");
                        }
                        else
                        {
                            // Code if login is not successful
                            return NotFound();
                        }
                    }
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                return NotFound();
            }
            return NotFound();
        }

        public class LoginViewModel
        {
            public string user_name { get; set; }
            public string user_password { get; set; }
        }
    }
}
