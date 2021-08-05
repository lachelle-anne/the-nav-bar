using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProgrammingProject.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Mvc.ViewFeatures.Infrastructure;

namespace ProgrammingProject.Controllers
{

    public class ReservationController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;

        public ReservationController(ApplicationDbContext context, UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpGet]
        public async Task<IActionResult> Sitting()
        {
            var sittings = await _context.Sittings.Where(s => s.End > DateTime.Now && s.Start < DateTime.Now.AddDays(14) && s.IsClosed == false).ToArrayAsync();
            var m = new Models.Reservation.Sitting
            {
                SittingOptions = new SelectList(sittings, "Id", "Details")
            };     
            return View(m);
        }

        //Start time/date data has been checked and confirmed in this class
        [HttpGet]
        public IActionResult ReservationDetails(int sittingId)
        {
            var sitting = _context.Sittings.FirstOrDefault(s => s.Id == sittingId);
            if (sitting == null)
            {
                return NotFound();
            }

            var m = new Models.Reservation.ReservationDetails
            {
                SittingId = sittingId,
                Sitting = sitting,
                Start = sitting.Start
            };
            return View(m); 
        }

        //Start time/date data has been checked and confirmed in this class
        public IActionResult IsLoggedIn([Bind("SittingId", "Notes", "Guests", "Start")] Models.Reservation.Reservation r)
        {
            TempData["reservation"] = JsonConvert.SerializeObject(r);
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Information", "Reservation", new {area = ""});
            }
            else
            { 
                return RedirectToAction("GetBooker", "Reservation", new {area = ""});
            }
        }

        //for the guest, this class below is where the start time is not transferring - 
        //could this be because it needs to be retrieved through reservation?
        [HttpGet]
        public async Task<IActionResult> GetBooker()
        {
            //View data confirming reservation data from reservationdetails
            Models.Reservation.Reservation reservation = JsonConvert.DeserializeObject<Models.Reservation.Reservation>(TempData["reservation"].ToString());

            var rp = new Models.Reservation.ReservationPerson
            {
                Start = reservation.Start,
                Guests = reservation.Guests,
                Notes = reservation.Notes,
                SittingId = reservation.SittingId,
                Titles = new SelectList(await _context.Titles.ToArrayAsync(), "Id", "Name")
            };

            return View(rp);
        }

        [HttpGet]
        public async Task<IActionResult> Information()
        {
            Models.Reservation.Reservation reservation = JsonConvert.DeserializeObject<Models.Reservation.Reservation>(TempData["reservation"].ToString());
            if (ModelState.IsValid)
            {
                try
                {
                    var user = await _userManager.GetUserAsync(User);
                    var person = await _context.People.Where(p => p.UserId == user.Id).FirstOrDefaultAsync();

                    var r = new Reservation
                    {
                        SittingId = reservation.SittingId,
                        BookerId = person.Id,
                        Start = reservation.Start,
                        Guests= reservation.Guests,
                        Notes= reservation.Notes,
                        SourceId= (await _context.ReservationSources.Where(s=>s.Source=="Online").FirstOrDefaultAsync()).Id,
                        StatusId = 1,
                    };
                    
                    _context.Reservations.Add(r);
                    await _context.SaveChangesAsync();
                    return RedirectToAction("Confirmation", "Reservation", new { id = r.Id });
                }
                catch (Exception)
                {
                    ModelState.AddModelError("Exception", "Your booking could not be made, try again");
                }
            }
            return RedirectToAction("Sitting", "Reservation", new {area=""});
        }

        [HttpGet]
        public List<Models.Reservation.ReservationDetails> API_ReservationList()
        {
            var reservations = _context.Reservations.ToListAsync().Result;
            List<Models.Reservation.ReservationDetails> reservationList = new List<Models.Reservation.ReservationDetails>();


            using (_context)
            {
                foreach (var r in reservations)
                {
                    var reservation = new Models.Reservation.ReservationDetails
                    {
                        Id = r.Id,
                        Start = r.Start,
                        Notes = r.Notes,
                        Guests = r.Guests,
                        SittingId = r.SittingId,
                        Person = _context.People.FirstOrDefault(p => p.Id == r.BookerId),
                        Sitting = _context.Sittings.FirstOrDefault(s => s.Id == r.SittingId)

                    };
                    reservationList.Add(reservation);
                }
                _context.Dispose();
            }
            return reservationList;
        }

        [HttpPost]
        public IActionResult GuestInformation([Bind("SittingId", "Guests", "Notes", "Start", "TitleId", "FirstName", "LastName", "Email", "PhoneNumber")] Models.Reservation.ReservationPerson rp)
        {

            var person = _context.People.Where(p => p.Email == rp.Email).FirstOrDefault();
                if(person == null)
                {
                    person = new Person
                    {

                        TitleId = rp.TitleId,
                        FirstName = rp.FirstName,
                        LastName = rp.LastName,
                        Email = rp.Email,
                        PhoneNumber = rp.PhoneNumber
                    };
                    _context.People.Add(person);
                    _context.SaveChangesAsync();
                }

                try
                {
                    var reservation = new Reservation
                    {
                        SittingId = rp.SittingId,
                        BookerId = person.Id,
                        Start = rp.Start,
                        Guests = rp.Guests,
                        Notes = rp.Notes,
                        SourceId = (_context.ReservationSources.Where(s => s.Source == "Online").FirstOrDefaultAsync()).Id,
                        StatusId = 1,
                    };
                    _context.Reservations.Add(reservation);
                    _context.SaveChangesAsync();
                    TempData["reservation"] = JsonConvert.SerializeObject(reservation);
                    return RedirectToAction("Confirmation", "Reservation", new { area = "" });
                }
                catch
                {
                    ModelState.AddModelError("Exception", "Oops, we couldnt add your reservation");
                }
            return RedirectToAction("Sitting", "Reservation", new { area = "" });
        }


        [HttpGet]
        public async Task<IActionResult> Confirmation(int id)
        {

            var reservation = await _context.Reservations.Include(r => r.Booker).FirstOrDefaultAsync(r => r.Id == id);

            var r = new Models.Reservation.Reservation
            {
                Id = reservation.Id,
                Start = reservation.Start,
                Guests = reservation.Guests
            };

            return View(r);
        }
    }
}

