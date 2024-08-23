using Microsoft.AspNetCore.Mvc;
using Library.Models;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Text;
using Azure;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

[Authorize]
public class BooksController : Controller
{
    private readonly LibraryContext _context;

    public BooksController(LibraryContext context)
    {
        _context = context;
    }

    //Action is used to display a list of all books.
    public async Task<IActionResult> Index()
    {
        return View(await _context.Books.ToListAsync());
    }
    //Returns an empty view named "Create".
    //This action is used to display a form for creating a new book.
    public IActionResult Create()
    {
        return View();
    }

    //Receives a Book object from the HTTP POST request.
    //Validates the incoming data using model validation.
    //If valid, adds the book to the database and redirects to the book list.
    //If invalid, returns the Create view with the provided data for correction.
    [HttpPost]
    public async Task<IActionResult> Create([Bind("BookID,Title,Author")] Book book)
    {
        if (ModelState.IsValid)
        {
            _context.Add(book);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(book);
    }

    //Checks if the id is valid.
    //Retrieves the book from the database using FindAsync - 
    //If the book is found, passes it to the "Edit" view.
    //If the book is not found, returns a NotFound result.
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var book = await _context.Books.FindAsync(id);
        if (book == null)
        {
            return NotFound();
        }
        return View(book);
    }


    //Retrieves the book to be updated
    //Validates the incoming data
    //Updates the book
    //If the book is not found during the update
    [HttpPost]
    public async Task<IActionResult> Edit(int id, [Bind("BookID,Title,Author")] Book book)
    {
        if (id != book.BookID)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(book);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BookExists(book.BookID))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return RedirectToAction(nameof(Index));
        }
        return View(book);
    }
    //Delete Action retrieves the book to be deleted and displays a confirmation view.
    public IActionResult Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var book = _context.Books.FirstOrDefault(m => m.BookID == id);
        if (book == null)
        {
            return NotFound();
        }

        return View(book);
    }
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public IActionResult DeleteConfirmed(int id)
    {
        var book = _context.Issue.Find(id);
        _context.Issue.Remove(book);
        _context.SaveChanges();
        return RedirectToAction(nameof(Index));

    }
        //Private function that checks if the Book exists in the Database
        private bool BookExists(int id)
    {
        return _context.Books.Any(e => e.BookID == id);
    }
}
