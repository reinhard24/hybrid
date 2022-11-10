using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Data;
using LoginLibrary.Models;
using LoginLibrary.DataAccess;
using Npgsql;

namespace LoginApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {


        //private readonly LoginDataAccess _context;

        //public LoginController(LoginDataAccess c)
        //    => _context = c;

        //[HttpPost]
        //[Route("[action]")]
        //public IActionResult Login(LoginModel login)
        //{
        //    var findLogin = _context.logins.Find(login.user_name);
        //    if (findLogin is null)
        //    {
        //        return NotFound();
        //    }
        //    else if (login != null && !string.IsNullOrWhiteSpace(login.user_name) && !string.IsNullOrWhiteSpace(login.user_password))
        //    {
        //        var result = _context.logins.ToList().Where(c => c.user_name == login.user_name);
        //        return Ok(result);
        //    }
        //    return NotFound();
        //}

        private NpgsqlConnection? conn;
        string connString = String.Format("Server=35.219.63.229;Port=5432;Database=hybridkitchen_db;User Id=postgres;Password=archsoft;");

        private readonly IConfiguration _configuration;

        public LoginController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpPost, Route("[action]", Name = "Login")]
        public UserDetailsModel Login(LoginModel users)
        {
            UserDetailsModel userdetails = new UserDetailsModel();
            userdetails.result = new Result();
            try
            {
                if (users != null && !string.IsNullOrWhiteSpace(users.user_name) && !string.IsNullOrWhiteSpace(users.user_password))
                {
                    conn = new NpgsqlConnection("Server=35.219.63.229;Port=5432;Database=hybridkitchen_db;User Id=postgres;Password=archsoft;");
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

                        if (intResult == 1)
                        {
                            //userdetails.user_id = dt.Rows[0]["user_id"].ToString();
                            //userdetails.user_name = dt.Rows[0]["user_name"].ToString();
                            //userdetails.email = dt.Rows[0]["email"].ToString();
                            //userdetails.user_password = dt.Rows[0]["user_password"].ToString();
                            //userdetails.phone = dt.Rows[0]["phone"].ToString();
                            //userdetails.address = dt.Rows[0]["address"].ToString();
                            //userdetails.role_id = dt.Rows[0]["role_id"].ToString();

                            userdetails.result.result = true;
                            userdetails.result.message = "success";
                        }
                        else
                        {
                            userdetails.result.result = false;
                            userdetails.result.message = "Invalid user";
                        }
                    }
                }
                else
                {
                    userdetails.result.result = false;
                    userdetails.result.message = "Please enter username and password";
                }
            }
            catch (Exception ex)
            {
                userdetails.result.result = false;
                userdetails.result.message = "Error occurred: " + ex.Message.ToString();
            }
            return userdetails;
        }
    }
}
