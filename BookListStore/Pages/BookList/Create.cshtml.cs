using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookListStore.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BookListStore.Pages.BookList
{
    public class CreateModel : PageModel
    {
        private readonly ApplicationDBContext _db;

        public CreateModel(ApplicationDBContext db)
        {
            _db = db;
        }

        [BindProperty]
        public Book Book { set; get; }
        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPost()
        {
            if (ModelState.IsValid)
            {
                await _db.Book.AddAsync(Book);
                await _db.SaveChangesAsync();
                return RedirectToPage("Index");

            }
            else
            {
                return Page();
            }
        }
    }
}
