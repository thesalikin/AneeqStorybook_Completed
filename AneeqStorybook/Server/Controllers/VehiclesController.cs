using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AneeqStorybook.Server.Data;
using AneeqStorybook.Shared.Domain;
using AneeqStorybook.Server.IRepository;

namespace AneeqStorybook.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        //Refactored
        //private readonly ApplicationDbContext _context;
        private readonly IUnitOfWork _unitOfWork;

        //Refactored
        //public BooksController(ApplicationDbContext context)
        public BooksController(IUnitOfWork unitOfWork)
        {
            //Refactored
            //_context = context
            _unitOfWork = unitOfWork;
        }


        // GET: api/Books
        [HttpGet]
        //Refactored
        //public async Task<ActionResult<IEnumerable<Book>>> GetBooks()
        public async Task<IActionResult> GetBooks()
        {
            //Refactored
            //return await _context.Books.ToListAsync();
            var Books = await _unitOfWork.Books.GetAll(includes: q => q.Include(x => x.Author).Include(x => x.Title));
            return Ok(Books);
        }

        // GET: api/Books/5
        [HttpGet("{id}")]
        //Refactored
        //public async Task<ActionResult<Book>> GetBook(int id)
        public async Task<IActionResult> GetBook(int id)
        {
            //Refactored
            //var Book = await _context.Books.FindAsync(id);
            var Book = await _unitOfWork.Books.Get(q => q.Id == id);

            if (Book == null)
            {
                return NotFound();
            }

            //Refactored
            return Ok(Book);
        }

        // PUT: api/Books/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBook(int id, Book Book)
        {
            if (id != Book.Id)
            {
                return BadRequest();
            }

            //Refactored
            //_context.Entry(Book).State = EntityState.Modified;
            _unitOfWork.Books.Update(Book);

            try
            {
                //Refactored
                //await _context.SaveChangesAsync();
                await _unitOfWork.Save(HttpContext);
            }
            catch (DbUpdateConcurrencyException)
            {
                //Refactored
                //if (!BookExists(id))
                if (!await BookExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Books
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Book>> PostBook(Book Book)
        {
            //Refactored
            //_context.Books.Add(Book);
            //await _context.SaveChangesAsync();
            await _unitOfWork.Books.Insert(Book);
            await _unitOfWork.Save(HttpContext);

            return CreatedAtAction("GetBook", new { id = Book.Id }, Book);
        }

        // DELETE: api/Books/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBook(int id)
        {
            //Refactored
            //var Book = await _context.Books.FindAsync(id);
            var Book = await _unitOfWork.Books.Get(q => q.Id == id);
            if (Book == null)
            {
                return NotFound();
            }

            //Refactored
            //_context.Books.Remove(Book);
            //await _context.SaveChangesAsync();
            await _unitOfWork.Books.Delete(id);
            await _unitOfWork.Save(HttpContext);

            return NoContent();
        }

        //Refactored
        //private bool BookExists(int id)
        private async Task<bool> BookExists(int id)
        {
            //Refactored
            //return _context.Books.Any(e => e.Id == id);
            var Book = await _unitOfWork.Books.Get(q => q.Id == id);
            return Book != null;
        }
    }
}
