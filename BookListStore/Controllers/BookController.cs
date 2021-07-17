using BookListStore.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookListStore.Controllers
{
    [Route("api/book")]
    [ApiController]
    public class BookController : Controller
    {
        private readonly ApplicationDBContext _db;
        public BookController(ApplicationDBContext db)
        {
            _db = db;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            return Json(new { data = _db.Book.ToList() });
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var bookFromDb = await _db.Book.FirstOrDefaultAsync(s => s.Id == id);
            if(bookFromDb == null)
            {
                return Json(new { success=false, message= "Error while Deleting."});
            }
            _db.Remove(bookFromDb);
            _ = _db.SaveChangesAsync();
            return Json(new { success = true, message = "Delete Successfully." });

        }
    }
}
