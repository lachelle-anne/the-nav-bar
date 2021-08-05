using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProgrammingProject.Data;

namespace ProgrammingProject.Areas.Member.Controllers
{
    public class ReservationController : MemberBaseController
    {
        public ReservationController(ApplicationDbContext context, IMapper mapper, UserManager<IdentityUser> userManager) : base(context, mapper, userManager)
        {

        }
        public async Task<IActionResult> Index()
        {
            List<Reservation> reservations = await _context.Reservations.ToListAsync();
            return View(reservations);
        }
    }
}
