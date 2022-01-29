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
    public class AuthorsController : ControllerBase
    {
        //Refactored
        //private readonly ApplicationDbContext _context;
        private readonly IUnitOfWork _unitOfWork;
        
        //Refactored
        //public AuthorsController(ApplicationDbContext context)
        public AuthorsController(IUnitOfWork unitOfWork)
        {
            //Refactored
            //_context = context
            _unitOfWork = unitOfWork;
        }
        
        
        // GET: api/Authors
        [HttpGet]
        //Refactored
        //public async Task<ActionResult<IEnumerable<Author>>> GetAuthors()
        public async Task<IActionResult> GetAuthors()
        {
            //Refactored
            //return await _context.Authors.ToListAsync();
            var Authors = await _unitOfWork.Authors.GetAll();
            return Ok(Authors);
        }

        // GET: api/Authors/5
        [HttpGet("{id}")]
        //Refactored
        //public async Task<ActionResult<Author>> GetAuthor(int id)
        public async Task<IActionResult> GetAuthor(int id)
        {
            //Refactored
            //var Author = await _context.Authors.FindAsync(id);
            var Author = await _unitOfWork.Authors.Get(q => q.Id == id);

            if (Author == null)
            {
                return NotFound();
            }

            //Refactored
            return Ok(Author);
        }

        // PUT: api/Authors/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAuthor(int id, Author Author)
        {
            if (id != Author.Id)
            {
                return BadRequest();
            }

            //Refactored
            //_context.Entry(Author).State = EntityState.Modified;
            _unitOfWork.Authors.Update(Author);

            try
            {
                //Refactored
                //await _context.SaveChangesAsync();
                await _unitOfWork.Save(HttpContext);
            }
            catch (DbUpdateConcurrencyException)
            {
                //Refactored
                //if (!AuthorExists(id))
                if (!await AuthorExists(id))
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

        // POST: api/Authors
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Author>> PostAuthor(Author Author)
        {
            //Refactored
            //_context.Authors.Add(Author);
            //await _context.SaveChangesAsync();
            await _unitOfWork.Authors.Insert(Author);
            await _unitOfWork.Save(HttpContext);

            return CreatedAtAction("GetAuthor", new { id = Author.Id }, Author);
        }

        // DELETE: api/Authors/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAuthor(int id)
        {
            //Refactored
            //var Author = await _context.Authors.FindAsync(id);
            var Author = await _unitOfWork.Authors.Get(q => q.Id == id);
            if (Author == null)
            {
                return NotFound();
            }

            //Refactored
            //_context.Authors.Remove(Author);
            //await _context.SaveChangesAsync();
            await _unitOfWork.Authors.Delete(id);
            await _unitOfWork.Save(HttpContext);

            return NoContent();
        }

        //Refactored
        //private bool AuthorExists(int id)
        private async Task<bool> AuthorExists(int id)
        {
            //Refactored
            //return _context.Authors.Any(e => e.Id == id);
            var Author = await _unitOfWork.Authors.Get(q => q.Id == id);
            return Author != null;
        }
    }
}
