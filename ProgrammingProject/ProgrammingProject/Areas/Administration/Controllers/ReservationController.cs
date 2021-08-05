using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProgrammingProject.Data;

namespace ProgrammingProject.Areas.Administration.Controllers
{
    public class ReservationController : AdministrationBaseController
    {
        public ReservationController(ApplicationDbContext context, IMapper mapper, UserManager<IdentityUser> userManager) : base(context, mapper, userManager)
        {

        }
        public async Task<IActionResult> Index()
        {
            var reservations = await _context.Reservations.Where(r => r.Start > DateTime.Now && r.Start < DateTime.Now.AddDays(7)).ToListAsync();
            using (_context) {
                foreach (var reservation in reservations)
                {
                    reservation.Booker = _context.People.Where(p => p.Id == reservation.BookerId).FirstOrDefault();
                    reservation.Status = _context.ReservationStatuses.Where(s => s.Id == reservation.StatusId).FirstOrDefault();
                    reservation.Sitting = _context.Sittings.Where(s => s.Id == reservation.SittingId).FirstOrDefault();
                }
                _context.Dispose();
            }
            return View(reservations);
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var reservation = await _context.Reservations.Where(r => r.Id == id).FirstOrDefaultAsync();
            if(reservation == null)
            {
                return NotFound();
            }
            using (_context)
            {
                reservation.Booker = _context.People.Where(p => p.Id == reservation.BookerId).FirstOrDefault();
                reservation.Sitting = _context.Sittings.Where(s => s.Id == reservation.SittingId).FirstOrDefault();
                reservation.Status = _context.ReservationStatuses.Where(s => s.Id == reservation.StatusId).FirstOrDefault();
                _context.Dispose();
            }
            return View(reservation);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var reservation = await _context.Reservations.Where(r => r.Id == id).FirstOrDefaultAsync();
            if(reservation == null)
            {
                return NotFound();
            }
            try
            {
                _context.Reservations.Remove(reservation);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch(Exception)
            {
                ModelState.AddModelError("Exception", "Oops there was an issue");
            }
            return RedirectToAction("Index", "Reservation", new { area = "Administration" });
        }
    }
}
    