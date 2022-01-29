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
    public class ReservationsController : ControllerBase
    {
        //Refactored
        //private readonly ApplicationDbContext _context;
        private readonly IUnitOfWork _unitOfWork;

        //Refactored
        //public ReservationsController(ApplicationDbContext context)
        public ReservationsController(IUnitOfWork unitOfWork)
        {
            //Refactored
            //_context = context
            _unitOfWork = unitOfWork;
        }


        // GET: api/Reservations
        [HttpGet]
        //Refactored
        //public async Task<ActionResult<IEnumerable<Reservation>>> GetReservations()
        public async Task<IActionResult> GetReservations()
        {
            //Refactored
            //return await _context.Reservations.ToListAsync();
            var Reservations = await _unitOfWork.Reservations.GetAll(includes: q => q.Include(x => x.Book).Include(x => x.Patron));
            return Ok(Reservations);
        }

        // GET: api/Reservations/5
        [HttpGet("{id}")]
        //Refactored
        //public async Task<ActionResult<Reservation>> GetReservation(int id)
        public async Task<IActionResult> GetReservation(int id)
        {
            //Refactored
            //var Reservation = await _context.Reservations.FindAsync(id);
            var Reservation = await _unitOfWork.Reservations.Get(q => q.Id == id);

            if (Reservation == null)
            {
                return NotFound();
            }

            //Refactored
            return Ok(Reservation);
        }

        // PUT: api/Reservations/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutReservation(int id, Reservation Reservation)
        {
            if (id != Reservation.Id)
            {
                return BadRequest();
            }

            //Refactored
            //_context.Entry(Reservation).State = EntityState.Modified;
            _unitOfWork.Reservations.Update(Reservation);

            try
            {
                //Refactored
                //await _context.SaveChangesAsync();
                await _unitOfWork.Save(HttpContext);
            }
            catch (DbUpdateConcurrencyException)
            {
                //Refactored
                //if (!ReservationExists(id))
                if (!await ReservationExists(id))
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

        // POST: api/Reservations
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Reservation>> PostReservation(Reservation Reservation)
        {
            //Refactored
            //_context.Reservations.Add(Reservation);
            //await _context.SaveChangesAsync();
            await _unitOfWork.Reservations.Insert(Reservation);
            await _unitOfWork.Save(HttpContext);

            return CreatedAtAction("GetReservation", new { id = Reservation.Id }, Reservation);
        }

        // DELETE: api/Reservations/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteReservation(int id)
        {
            //Refactored
            //var Reservation = await _context.Reservations.FindAsync(id);
            var Reservation = await _unitOfWork.Reservations.Get(q => q.Id == id);
            if (Reservation == null)
            {
                return NotFound();
            }

            //Refactored
            //_context.Reservations.Remove(Reservation);
            //await _context.SaveChangesAsync();
            await _unitOfWork.Reservations.Delete(id);
            await _unitOfWork.Save(HttpContext);

            return NoContent();
        }

        //Refactored
        //private bool ReservationExists(int id)
        private async Task<bool> ReservationExists(int id)
        {
            //Refactored
            //return _context.Reservations.Any(e => e.Id == id);
            var Reservation = await _unitOfWork.Reservations.Get(q => q.Id == id);
            return Reservation != null;
        }
    }
}
