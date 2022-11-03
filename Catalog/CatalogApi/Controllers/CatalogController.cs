using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CatalogLibrary.DataAccess;
using CatalogLibrary;
using CatalogLibrary.Models;

namespace CatalogApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]

    public class CatalogController : ControllerBase
    {
        private readonly CatalogDataAccess _context;

        public CatalogController(CatalogDataAccess c)
            => _context = c;

        // GET: CatalogController
        [HttpGet]
        public IEnumerable<CatalogModel> GetAll()
                    => _context.Catalog.ToList();

        [HttpGet]
        [Route("[action]/SortAZ")]
        public IEnumerable<CatalogModel> SortAZ()
                    => _context.Catalog.ToList().OrderBy(c => c.catalog_name);

        [HttpGet]
        [Route("[action]/SortByQty")]
        public IEnumerable<CatalogModel> SortByQty()
                    => _context.Catalog.ToList().OrderBy(c => c.product_qty);

        [HttpGet]
        [Route("[action]/GetId")]
        public IActionResult Get(int id)
        {
            var findCatalog = _context.Catalog.Find(id);
            if (findCatalog is null)
            {
                return NotFound();
            }

            var result = _context.Catalog.FirstOrDefault(c => c.catalog_id == id);
            return Ok(result);
        }

        [HttpGet]
        [Route("[action]/GetName")]
        public IActionResult Get(string name)
        {
            var findCatalog = _context.Catalog.Include(name);
            if (findCatalog is null)
            {
                return NotFound();
            }

            var result = _context.Catalog.FirstOrDefault(c => c.catalog_name == name);
            return Ok(result);
        }

        [HttpPost]
        public ActionResult<CatalogModel> Create(CatalogModel catalog)
        {
            _context.Catalog.Add(catalog);
            _context.SaveChanges();

            return NoContent();
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, CatalogModel catalog)
        {
            if (id != catalog.catalog_id)
            {
                return BadRequest();
            }

            _context.Entry(catalog).State = EntityState.Modified;
            _context.SaveChanges();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var deleteCatalog = _context.Catalog.Find(id);
            if (deleteCatalog is null)
            {
                return NotFound();
            }

            _context.Catalog.Remove(deleteCatalog);
            _context.SaveChanges();

            return NoContent();
        }
    }
}
