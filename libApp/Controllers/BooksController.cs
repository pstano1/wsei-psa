using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using libApp.Data;
using libApp.Models;
using libApp.ViewModels;
using Microsoft.AspNetCore.Authorization;

namespace libApp.Controllers
{
    [Authorize]
    public class BooksController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BooksController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Books
        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
              return _context.Book != null ? 
                          View(await _context.Book.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.Book'  is null.");
        }

        // GET: Books/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = _context.Book
                .Include(b => b.BookCustomers)
                .ThenInclude(bc => bc.Customer)
                .FirstOrDefault(m => m.Id == id);

            if (book == null)
            {
                return NotFound();
            }

            var viewModel = new BookDetailsViewModel
            {
                Book = book,
                BC = book.BookCustomers.ToList(),
                Customers = book.BookCustomers.Select(bc => bc.Customer).ToList()
            };

            return View(viewModel);
        }

        // GET: Books/Create
        public IActionResult Create()
        {
            var genres = _context.Genre.ToList();
            var authors = _context.Author.ToList();
            var viewModel = new BookFormViewModel
            {
                Genres = genres,
                Authors = authors
            };

            return View("Create", viewModel);
        }

        // GET: Books/Reserve
        public async Task<IActionResult> Reserve(int? id)
        {
            if (id == null || _context.Book == null)
            {
                return NotFound();
            }

            var book = await _context.Book
                .FirstOrDefaultAsync(m => m.Id == id);
            if (book == null)
            {
                return NotFound();
            }

            var customers = _context.Customer.ToList();
            var viewModel = new ReserveBookViewModel
            {
                Book = book,
                Customers = customers,
                BC = new BookCustomer()
            };

            return View("Reserve", viewModel);
        }

        [HttpPost]
        public IActionResult Reserve(int id, [Bind("ID,CustomerId,BookId")] BookCustomer bc)
        {
            if (bc.ID == 0)
            {
                bc.BorrowDate = DateTime.Now;
                bc.BookId = id;
                _context.BookCustomer.Add(bc);
                var bookInDb2 = _context.Book.SingleOrDefault(b => b.Id == id);
                bookInDb2.AvailableQuantity = bookInDb2.AvailableQuantity - 1;
            }
            else
            {
                var bookCustomer = _context.BookCustomer.SingleOrDefault(b => b.ID == bc.ID);
                bookCustomer.ReturnDate = DateTime.Now;
                var bookInDb = _context.Book.SingleOrDefault(b => b.Id == id);
                bookInDb.AvailableQuantity = bookInDb.AvailableQuantity + 1;
            }

            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateException e)
            {
                Console.Write("kurwa");
                Console.WriteLine(e);
            }

            return RedirectToAction("Index", "Books");
        }

        // POST: Books/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,Genre,Quantity,AvailableQuantity")] Book book)
        {
            if (ModelState.IsValid)
            {
                _context.Add(book);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(book);
        }

        [HttpPost]
        public IActionResult Save(Book book)
        {
            if (book.Id == 0)
            {
                _context.Book.Add(book);
            }
            else
            {
                var bookInDb = _context.Book.SingleOrDefault(b => b.Id == book.Id);
                bookInDb.Title = book.Title;
                bookInDb.Genre = book.Genre;
                bookInDb.Quantity = book.Quantity;
                bookInDb.AuthorID = book.AuthorID;
                bookInDb.AvailableQuantity = book.AvailableQuantity;
            }

            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateException e)
            {
                Console.WriteLine(e);
            }

            return RedirectToAction("Index", "Books");
        }

        // GET: Books/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Book == null)
            {
                return NotFound();
            }

            var book = await _context.Book.FindAsync(id);
            if (book == null)
            {
                return NotFound();
            }

            var genres = _context.Genre.ToList();
            var authors = _context.Author.ToList();
            var viewModel = new BookFormViewModel
            {
                Book = book,
                Genres = genres,
                Authors = authors
            };

            return View("Create", viewModel);
        }

        // POST: Books/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Genre,Author,Quantity,AvailableQuantity")] Book book)
        {
            if (id != book.Id)
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
                    if (!BookExists(book.Id))
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

        // GET: Books/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Book == null)
            {
                return NotFound();
            }

            var book = await _context.Book
                .FirstOrDefaultAsync(m => m.Id == id);
            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }

        // POST: Books/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Book == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Book'  is null.");
            }
            var book = await _context.Book.FindAsync(id);
            if (book != null)
            {
                _context.Book.Remove(book);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BookExists(int id)
        {
          return (_context.Book?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
