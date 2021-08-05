using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProgrammingProject.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;

namespace ProgrammingProject.Controllers
{
    public class PersonController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly UserManager<IdentityUser> _userManager;
        public PersonController(ApplicationDbContext context, IMapper mapper, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _mapper = mapper;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {


            if (User.Identity.IsAuthenticated)
            {
                ClaimsPrincipal cp = User;
                IdentityUser iu = await _userManager.FindByNameAsync(User.Identity.Name);

                if (!User.IsInRole("Admin"))
                {
                    await _userManager.AddToRoleAsync(iu, "Admin");
                }
            }

            return View(await _context.People.ToListAsync());
        }



        public async Task<IActionResult> Details(int id)
        {
            var person = await _context.People.FindAsync(id);
            if (person == null)
                return NotFound();
            var m = _mapper.Map<Models.Person.Person>(person);
            return View(m);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var m = new Models.Person.Person
            {
                Titles = new SelectList(await _context.Titles.ToListAsync(), "Id", "Name")
            };
            return View(m);
        }

        [HttpPost]
        public async Task<IActionResult> Create([Bind("Id, TitleId, FirstName,LastName,PhoneNumber,Email")] Models.Person.Person m)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var p = new Person
                    {
                        TitleId = m.TitleId,
                        FirstName = m.FirstName,
                        LastName = m.LastName,
                        PhoneNumber= m.PhoneNumber,
                        Email = m.Email
                    };
                    _context.People.Add(p);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception)
                {
                    ModelState.AddModelError("Exception", "Oops, there was an error");
                }
            }
            m.Titles = new SelectList(await _context.Titles.ToListAsync(), "Id", "Name", m.TitleId);
            return View(m);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var p = await _context.People.FindAsync(id);
            if (p == null)
                return NotFound();
            var m = _mapper.Map<Models.Person.Person>(p);
            m.Titles = new SelectList(await _context.Titles.ToListAsync(), nameof(Title.Id), nameof(Title.Name));
            return View(m);
        }

        [HttpPost]
        public async Task<IActionResult> Edit([Bind("Id, TitleId, FirstName,LastName, PhoneNumber, Email")] Models.Person.Person m)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var p = await _context.People.FindAsync(m.Id);
                    if (p == null)
                        return NotFound();

                    p = _mapper.Map<Person>(m);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception)
                {
                    ModelState.AddModelError("Exception", "Oops, there was an error");
                }
            }
            m.Titles = new SelectList(await _context.Titles.ToListAsync(), "Id", "Name", m.TitleId);
            return View(m);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var person = await _context.People
                                    .Include(p => p.Title)
                                    .FirstOrDefaultAsync(p => p.Id == id);
            if (person == null)
                return NotFound();
            var m = _mapper.Map<Models.Person.Person>(person);
            m.TitleId = person.Title.Id;
            return View(m);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(Models.Person.Person m)
        {
            try
            {
                var person = await _context.People
                        .Include(p => p.Title)
                        .FirstOrDefaultAsync(p => p.Id == m.Id);
                if (person == null)
                    return NotFound();
                _context.People.Remove(person);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {
                ModelState.AddModelError("Exception", "Oops, you broke my code");
            }
            return View(m);
        }

    }
}

