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
    public class PatronsController : ControllerBase
    {



        //Refactored
        //private readonly ApplicationDbContext _context;
        private readonly IUnitOfWork _unitOfWork;

        //Refactored
        //public PatronsController(ApplicationDbContext context)
        public PatronsController(IUnitOfWork unitOfWork)
        {
            //Refactored
            //_context = context
            _unitOfWork = unitOfWork;

        }


        // GET: api/Patrons
        [HttpGet]
        //Refactored
        //public async Task<ActionResult<IEnumerable<Patron>>> GetPatrons()
        public async Task<IActionResult> GetPatrons()
        {
            //Refactored
            //return await _context.Patrons.ToListAsync();
            var Patrons = await _unitOfWork.Patrons.GetAll();
            return Ok(Patrons);
        }

        // GET: api/Patrons/5
        [HttpGet("{id}")]
        //Refactored
        //public async Task<ActionResult<Patron>> GetPatron(int id)
        public async Task<IActionResult> GetPatron(int id)
        {
            //Refactored
            //var Patron = await _context.Patrons.FindAsync(id);
            var Patron = await _unitOfWork.Patrons.Get(q => q.Id == id);

            if (Patron == null)
            {
                return NotFound();
            }

            //Refactored
            return Ok(Patron);
        }

        // PUT: api/Patrons/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPatron(int id, Patron Patron)
        {
            if (id != Patron.Id)
            {
                return BadRequest();
            }

            //Refactored
            //_context.Entry(Patron).State = EntityState.Modified;
            _unitOfWork.Patrons.Update(Patron);

            try
            {
                //Refactored
                //await _context.SaveChangesAsync();
                await _unitOfWork.Save(HttpContext);
            }
            catch (DbUpdateConcurrencyException)
            {
                //Refactored
                //if (!PatronExists(id))
                if (!await PatronExists(id))
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

        // POST: api/Patrons
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Patron>> PostPatron(Patron Patron)
        {
            //Refactored
            //_context.Patrons.Add(Patron);
            //await _context.SaveChangesAsync();
            await _unitOfWork.Patrons.Insert(Patron);
            await _unitOfWork.Save(HttpContext);

            return CreatedAtAction("GetPatron", new { id = Patron.Id }, Patron);
        }

        // DELETE: api/Patrons/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePatron(int id)
        {
            //Refactored
            //var Patron = await _context.Patrons.FindAsync(id);
            var Patron = await _unitOfWork.Patrons.Get(q => q.Id == id);
            if (Patron == null)
            {
                return NotFound();
            }

            //Refactored
            //_context.Patrons.Remove(Patron);
            //await _context.SaveChangesAsync();
            await _unitOfWork.Patrons.Delete(id);
            await _unitOfWork.Save(HttpContext);

            return NoContent();
        }

        //Refactored
        //private bool PatronExists(int id)
        private async Task<bool> PatronExists(int id)
        {
            //Refactored
            //return _context.Patrons.Any(e => e.Id == id);
            var Patron = await _unitOfWork.Patrons.Get(q => q.Id == id);
            return Patron != null;
        }

    }
}
