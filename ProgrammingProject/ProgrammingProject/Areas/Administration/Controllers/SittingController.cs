using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProgrammingProject.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProgrammingProject.Areas.Administration.Controllers
{
    public class SittingController : AdministrationBaseController
    {
        public SittingController(ApplicationDbContext context, IMapper mapper, UserManager<IdentityUser> userManager) : base(context, mapper, userManager)
        {
            
        }
        public async Task<IActionResult> Index()
        {
            return View(await _context.Sittings.ToListAsync());
        }
        public async Task<IActionResult> Details(int id)
        {
            var sitting = await _context.Sittings.FindAsync(id);
            if (sitting == null)
                return NotFound();
            var m = _mapper.Map<Data.Sitting>(sitting);
            return View(m);
        }
        public async Task<IActionResult> Create()
        {
            var s = new Models.Sitting.Sitting
            {
                SittingTypes = new SelectList(await _context.SittingTypes.ToArrayAsync(), "Id", "Name"),
                Restaurants = new SelectList(await _context.Restaurants.ToArrayAsync(), "Id", "Name")
            };
            return View(s);
        }

        [HttpPost]
        public async Task<IActionResult> Create([Bind("StartTime", "EndTime", "SittingName", "Capacity", "RestaurantId", "IsClosed", "SittingTypeId")] Models.Sitting.Sitting sitting)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var s = new Sitting
                    {
                        Id = sitting.Id,
                        Start = sitting.StartTime,
                        End = sitting.EndTime,
                        Name = sitting.SittingName,
                        Capacity = sitting.Capacity,
                        SittingTypeId = sitting.SittingTypeId,
                        IsClosed = sitting.IsClosed,
                        RestaurantId = sitting.RestaurantId
                    };
                    _context.Sittings.Add(s);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch
                {
                    ModelState.AddModelError("Exception", "Oops there was an error");
                }
            }
            sitting.SittingTypes = new SelectList(await _context.SittingTypes.ToArrayAsync());
            return View(sitting);
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var s = await _context.Sittings.FindAsync(id);
            if (s == null)
                return NotFound();
            var sitting = new Models.Sitting.Sitting
            {
                Id = s.Id,
                SittingName = s.Name,
                StartTime = s.Start,
                EndTime = s.End,
                Capacity = s.Capacity,
                IsClosed = s.IsClosed,
                RestaurantId = s.RestaurantId,
                SittingTypeId = s.SittingTypeId,
                SittingTypes = new SelectList(await _context.SittingTypes.ToListAsync(), nameof(SittingType.Id), nameof(SittingType.Name)),
                Restaurants = new SelectList(await _context.Restaurants.ToListAsync(), nameof(Restaurant.Id), nameof(Restaurant.Name))
            };
            return View(sitting);
        }
        
        [HttpPost]
        public async Task<IActionResult> Edit([Bind("Id", "StartTime", "EndTime", "SittingName", "Capacity", "RestaurantId", "IsClosed", "SittingTypeId")] Models.Sitting.Sitting sitting)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var p = new Sitting
                    {
                        Id = sitting.Id,
                        Start = sitting.StartTime,
                        End = sitting.EndTime,
                        Name = sitting.SittingName,
                        Capacity = sitting.Capacity,
                        SittingTypeId = sitting.SittingTypeId,
                        IsClosed = sitting.IsClosed,
                        RestaurantId = sitting.RestaurantId
                    };
                    if (p == null) 
                    { 
                        return NotFound();
                    }
                    _context.Sittings.Update(p);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception)
                {
                    ModelState.AddModelError("Exception", "Oops, there was an error");
                }
            }
            sitting.SittingTypes = new SelectList(await _context.SittingTypes.ToArrayAsync());
            sitting.Restaurants = new SelectList(await _context.Restaurants.ToArrayAsync());
            return View(sitting);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var sitting = await _context.Sittings
                .FirstOrDefaultAsync(m => m.Id == id);
            if (sitting == null)
            {
                return NotFound();
            }

            return View(sitting);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var sitting = await _context.Sittings.FindAsync(id);
            _context.Sittings.Remove(sitting);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SittingExists(int id)
        {
            return _context.Sittings.Any(e => e.Id == id);
        }
    }
}
