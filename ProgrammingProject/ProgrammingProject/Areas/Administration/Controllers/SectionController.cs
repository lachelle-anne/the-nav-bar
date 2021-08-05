using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProgrammingProject.Areas.Administration.Models.Section;
using ProgrammingProject.Data;

namespace ProgrammingProject.Areas.Administration.Controllers
{
    public class SectionController : AdministrationBaseController
    {
        public SectionController(ApplicationDbContext context, IMapper mapper, UserManager<IdentityUser> userManager) : base(context, mapper, userManager)
        {

        }
        public async Task<IActionResult> Index()
        {
            var sections = await _context.Sections.ToListAsync();
            return View(sections);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var r = new Models.Section.Section
            {
                Restaurants = new SelectList(await _context.Restaurants.ToListAsync(), "Id", "Name")
            };
            return View(r);
        }

        [HttpPost]
        public async Task<IActionResult> Create([Bind("Name", "RestaurantId")] Models.Section.Section s)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var section = new Data.Section
                    {
                        Name = s.Name,
                        RestaurantId = s.RestaurantId
                    };
                    _context.Sections.Add(section);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception)
                {
                    ModelState.AddModelError("Exception", "Oops! There was an Error!");
                }
            }
            s.Restaurants = new SelectList(await _context.Restaurants.ToListAsync(), "Id", "Name");
            return View(s);
        }
        public async Task<IActionResult> Delete([Bind("Id", "Name", "RestaurantId")] Data.Section s)
        {
            var section = await _context.Sections.FirstOrDefaultAsync(m=>m.Id == s.Id);
             _context.Sections.Remove(section);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Edit([Bind("Id", "Name", "RestaurantId")] Data.Section s)
        {

            var section = await _context.Sections.FirstOrDefaultAsync(m => m.Id == s.Id);
            Models.Section.Section modelSection = new Models.Section.Section
            {
                Id = section.Id,
                Name = section.Name,
                Restaurants = new SelectList(await _context.Restaurants.ToListAsync(), "Id", "Name")
            };
            return View(modelSection);
        }

        [HttpPost]
        public async Task<IActionResult> Edit([Bind("Id", "Name", "RestaurantId")] Models.Section.Section s)
        {
            if (ModelState.IsValid)
            {
                if(s.RestaurantId == 0)
                {

                }
                try
                {
                    var m = new Data.Section
                    {
                        Id = s.Id,
                        Name = s.Name,
                        RestaurantId = s.RestaurantId
                    };
                    _context.Sections.Update(m);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception)
                {
                    ModelState.AddModelError("Exception", "Oops there was an error");
                }
            }
            s.Restaurants = new SelectList(await _context.Restaurants.ToListAsync(), "Id", "Name", s.RestaurantId);
            return View(s);

        }
    }
}
