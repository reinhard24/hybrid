using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StockAdjustmentLibrary.DataAccess;
using StockAdjustmentLibrary;
using StockAdjustmentLibrary.Models;
using System.Data;
using System.IO;
using System.Data;
using System.Linq;
using System.Web;
using ClosedXML.Excel;
using DocumentFormat.OpenXml.Spreadsheet;
using DocumentFormat.OpenXml.Office2010.Excel;
using DocumentFormat.OpenXml.Drawing.Charts;
using DataTable = System.Data.DataTable;

namespace StockAdjustmentApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]

    public class StockAdjustmentController : ControllerBase
    {
        private readonly StockAdjustmentDataAccess _context;

        public StockAdjustmentController(StockAdjustmentDataAccess c)
            => _context = c;

        // GET: ShippingController
        [HttpGet]
        public IEnumerable<StockAdjustmentModel> GetAll()
                    => _context.stockadjustments.ToList();

        [HttpGet]
        [Route("[action]/Search")]
        public IActionResult Get(int search)
        {
            var findStockIn = _context.stockadjustments.Find(search);
            if (findStockIn is null)
            {
                return NotFound();
            }

            var result = _context.stockadjustments.ToList().Where(c => c.stockadjusment_id == search);
            return Ok(result);
        }

        [HttpGet]
        [Route("[action]/SearchByDateRange")]
        public IActionResult Get(DateViewModel date)
        {
            
            var result = _context.stockadjustments.ToList().Where(c => c.adjusment_date >= date.startDate && c.adjusment_date <= date.endDate);
            return Ok(result);
        }

        //Test

        [HttpPost]
        [Route("[action]/GenerateExcel")]
        public FileResult Export()
        {
            StockAdjustmentModel model = new StockAdjustmentModel();
            DataTable dt = new DataTable("Grid");
            dt.Columns.AddRange(new DataColumn[2] { new DataColumn("stockadjusment_id"),
                                            new DataColumn("adjusment_date") });

            var stockadjustments = _context.stockadjustments.ToList();

            foreach (var stockadjustment in stockadjustments)
            {
                dt.Rows.Add(stockadjustment.stockadjusment_id, stockadjustment.adjusment_date);
            }

            using (XLWorkbook wb = new XLWorkbook())
            {
                wb.Worksheets.Add(dt);
                using (MemoryStream stream = new MemoryStream())
                {
                    wb.SaveAs(stream);
                    return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "StockAdjustment.xlsx");
                }
            }
        }

        //endTest

        [HttpPost]
        public ActionResult<StockAdjustmentModel> Create(StockAdjustmentModel stockadjustment)
        {
            _context.stockadjustments.Add(stockadjustment);
            _context.SaveChanges();

            return NoContent();
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, StockAdjustmentModel stockadjustment)
        {
            if (id != stockadjustment.stockadjusment_id)
            {
                return BadRequest();
            }

            _context.Entry(stockadjustment).State = EntityState.Modified;
            _context.SaveChanges();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var deleteStockAdjustment = _context.stockadjustments.Find(id);
            if (deleteStockAdjustment is null)
            {
                return NotFound();
            }

            _context.stockadjustments.Remove(deleteStockAdjustment);
            _context.SaveChanges();

            return NoContent();
        }
    }

    public class DateViewModel
    {
        public DateTime startDate { get; set; }
        public DateTime endDate { get; set; }
    }
}
