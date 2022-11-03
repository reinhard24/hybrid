using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StockInLibrary.DataAccess;
using StockInLibrary;
using StockInLibrary.Models;
using System.Data;
using System.IO;
using System.Data;
using System.Linq;
using System.Web;
using ClosedXML.Excel;
using DocumentFormat.OpenXml.Spreadsheet;
using DocumentFormat.OpenXml.Office2010.Excel;


namespace ShippingApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]

    public class StockInController : ControllerBase
    {
        private readonly StockInDataAccess _context;

        public StockInController(StockInDataAccess c)
            => _context = c;

        // GET: ShippingController
        [HttpGet]
        public IEnumerable<StockInModel> GetAll()
                    => _context.stockins.ToList();

        [HttpGet]
        [Route("[action]/GetSearch")]
        public IActionResult Get(int id)
        {
            var findStockIn = _context.stockins.Find(id);
            if (findStockIn is null)
            {
                return NotFound();
            }

            var result = _context.stockins.ToList().Where(c => c.product_id == id);
            return Ok(result);
        }

        [HttpGet]
        [Route("[action]/SortByDate")]
        public IEnumerable<StockInModel> SortByDate()
                    => _context.stockins.ToList().OrderBy(c => c.entry_date);

        [HttpPost]
        public ActionResult<StockInModel> Create(StockInModel stockin)
        {
            _context.stockins.Add(stockin);
            _context.SaveChanges();

            return NoContent();
        }

        [HttpPost]
        [Route("[action]/GenerateExcel")]
        public FileResult Export()
        {
            StockInModel model = new StockInModel();
            DataTable dt = new DataTable("Grid");
            dt.Columns.AddRange(new DataColumn[5] { new DataColumn("stockin_id"),
                                            new DataColumn("product_id"),
                                            new DataColumn("stockin_qty"),
                                            new DataColumn("entry_date"),
                                            new DataColumn("notes") });

            var stockins = _context.stockins.ToList();

            foreach (var stockin in stockins)
            {
                dt.Rows.Add(stockin.stockin_id, stockin.product_id, stockin.stockin_qty, stockin.entry_date,
                    stockin.notes);
            }

            using (XLWorkbook wb = new XLWorkbook())
            {
                wb.Worksheets.Add(dt);
                using (MemoryStream stream = new MemoryStream())
                {
                    wb.SaveAs(stream);
                    return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "stockins.xlsx");
                }
            }
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, StockInModel stockin)
        {
            if (id != stockin.stockin_id)
            {
                return BadRequest();
            }

            _context.Entry(stockin).State = EntityState.Modified;
            _context.SaveChanges();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var deleteStockIn = _context.stockins.Find(id);
            if (deleteStockIn is null)
            {
                return NotFound();
            }

            _context.stockins.Remove(deleteStockIn);
            _context.SaveChanges();

            return NoContent();
        }
    }
}
