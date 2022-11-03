using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StockOutLibrary.DataAccess;
using StockOutLibrary;
using StockOutLibrary.Models;
using System.Data;
using System.IO;
using System.Data;
using System.Linq;
using System.Web;
using ClosedXML.Excel;
using DocumentFormat.OpenXml.Spreadsheet;

namespace StockOutApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]

    public class StockOutController : ControllerBase
    {
        private readonly StockOutDataAccess _context;

        public StockOutController(StockOutDataAccess c)
            => _context = c;

        // GET: ShippingController
        [HttpGet]
        public IEnumerable<StockOutModel> GetAll()
                    => _context.stockouts.ToList();

        [HttpGet]
        [Route("[action]/SortByDate")]
        public IEnumerable<StockOutModel> SortByDate()
                    => _context.stockouts.ToList().OrderBy(c => c.out_date);

        [HttpPost]
        public ActionResult<StockOutModel> Create(StockOutModel stockout)
        {
            _context.stockouts.Add(stockout);
            _context.SaveChanges();

            return NoContent();
        }

        [HttpPost]
        [Route("[action]/GenerateExcel")]
        public FileResult Export()
        {
            StockOutModel model = new StockOutModel();
            DataTable dt = new DataTable("Grid");
            dt.Columns.AddRange(new DataColumn[5] { new DataColumn("stockout_id"),
                                            new DataColumn("product_id"),
                                            new DataColumn("stockout_qty"),
                                            new DataColumn("out_date"),
                                            new DataColumn("notes") });

            var stockouts = _context.stockouts.ToList();

            foreach (var stockout in stockouts)
            {
                dt.Rows.Add(stockout.stockout_id, stockout.product_id, stockout.stockout_qty, stockout.out_date,
                    stockout.notes);
            }

            using (XLWorkbook wb = new XLWorkbook())
            {
                wb.Worksheets.Add(dt);
                using (MemoryStream stream = new MemoryStream())
                {
                    wb.SaveAs(stream);
                    return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "StockOut.xlsx");
                }
            }
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, StockOutModel stockout)
        {
            if (id != stockout.product_id)
            {
                return BadRequest();
            }

            _context.Entry(stockout).State = EntityState.Modified;
            _context.SaveChanges();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var deleteStockOut = _context.stockouts.Find(id);
            if (deleteStockOut is null)
            {
                return NotFound();
            }

            _context.stockouts.Remove(deleteStockOut);
            _context.SaveChanges();

            return NoContent();
        }
    }
}
