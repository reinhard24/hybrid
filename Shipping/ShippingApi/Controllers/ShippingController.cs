using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShippingLibrary;
using ShippingLibrary.DataAccess;
using ShippingLibrary.Models;

namespace ShippingApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]

    public class ShippingController : ControllerBase
    {
        private readonly ShippingDataAccess _context;

        public ShippingController(ShippingDataAccess c)
            => _context = c;

        // GET: ShippingController
        [HttpGet]
        public IEnumerable<ShippingModel> GetAll()
                    => _context.Shipping.ToList();

        [HttpPost]
        public ActionResult<ShippingModel> Create(ShippingModel shipping)
        {
            _context.Shipping.Add(shipping);
            _context.SaveChanges();

            return NoContent();
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, ShippingModel shipping)
        {
            if (id != shipping.shipping_id)
            {
                return BadRequest();
            }

            _context.Entry(shipping).State = EntityState.Modified;
            _context.SaveChanges();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var deleteShipping = _context.Shipping.Find(id);
            if (deleteShipping is null)
            {
                return NotFound();
            }

            _context.Shipping.Remove(deleteShipping);
            _context.SaveChanges();

            return NoContent();
        }
    }
}
