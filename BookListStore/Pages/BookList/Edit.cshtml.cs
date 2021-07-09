using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookListStore.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BookListStore.Pages.BookList
{

    public class EditModel : PageModel
    {
        private readonly ApplicationDBContext _db;

        public EditModel(ApplicationDBContext db)
        {
            _db = db;
        }

        [BindProperty]
        public Book Book { set; get; }
        public async Task OnGet(int id)
        {
            Book = await _db.Book.FindAsync(id);
        }
        public async Task<IActionResult> OnPost()
        {
            if (ModelState.IsValid)
            {
                var bookFromDB = await _db.Book.FindAsync(Book.Id);
                bookFromDB.Name = Book.Name;
                bookFromDB.Author = Book.Author;
                bookFromDB.ISBN = Book.ISBN;

                await _db.SaveChangesAsync();
                return RedirectToPage("Index");

            }
            else
                return Page();
        }
    }
}
