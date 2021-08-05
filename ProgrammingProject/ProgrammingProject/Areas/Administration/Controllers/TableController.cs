using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProgrammingProject.Areas.Administration.Models.Table;
using ProgrammingProject.Data;

namespace ProgrammingProject.Areas.Administration.Controllers
{
    public class TableController : AdministrationBaseController
    {
        public TableController(ApplicationDbContext context, IMapper mapper, UserManager<IdentityUser> userManager) : base(context, mapper, userManager)
        {
                
        }
        
        public async Task<IActionResult> Index(int id)
        {
            var tables = await _context.Tables.Where(m => m.SectionId == id).ToListAsync();
            List<Models.Table.Table> modelTables = new List<Models.Table.Table>();
            foreach (var t in tables)
            {
                Models.Table.Table table = new Models.Table.Table
                {
                    Id = t.Id,
                    TableNumber = t.TableNumber,
                    Seats = t.Seats,
                    SectionId = id
                };
                modelTables.Add(table);
            }
            TempData["SectionId"] = id;
            return View(modelTables);
        }

        [HttpGet]
        public IActionResult Create()
        {
            int sectionId = int.Parse(TempData["SectionId"].ToString());
            var table = new Models.Table.Table
            {
                SectionId = sectionId
            };
            return View(table);
        }
        [HttpPost]
        public async Task<IActionResult> Create([Bind("Id", "TableNumber", "Seats", "SectionId")] Models.Table.Table t)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var table = new Data.Table
                    {
                        Id = t.Id,
                        TableNumber = t.TableNumber,
                        Seats = t.Seats,
                        SectionId = t.SectionId
                    };
                    _context.Tables.Add(table);
                    await _context.SaveChangesAsync();
                    return RedirectToAction("Index", "Table", new {area = "Administration", Id=t.SectionId});
                }
                catch (Exception)
                {
                    ModelState.AddModelError("Exception", "Oops, there was an error");
                }
            }
            var tables = new SelectList(await _context.Tables.Where(m => m.SectionId == t.SectionId).ToListAsync());
            return View(tables);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var table = await _context.Tables.FirstOrDefaultAsync(t => t.Id == id);
            return View(table);
        }

        [HttpPost]
        public async Task<IActionResult> Edit([Bind("Id", "TableNumber", "Seats", "SectionId")] Models.Table.Table t)
        {
            if (ModelState.IsValid) 
            {
                try
                {
                    Data.Table table = new Data.Table
                    {
                        Id = t.Id,
                        TableNumber = t.TableNumber,
                        Seats = t.Seats,
                        SectionId = t.SectionId
                    };
                    _context.Tables.Update(table);
                    await _context.SaveChangesAsync();
                    return RedirectToAction("Index", "Table", new { area = "Administration", Id = t.SectionId });
                }
                catch (Exception)
                {

                    ModelState.AddModelError("Exception", "Oops, there was an error");
                }
            }
            var tables = new SelectList(await _context.Tables.Where(m => m.SectionId == t.SectionId).ToListAsync());
            return View(tables);


        }
        public async Task<IActionResult> Delete(int id, int sId)
        {
            var table = await _context.Tables.FirstOrDefaultAsync(m => m.Id == id);
            if(table != null)
            {
                _context.Tables.Remove(table);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "Table", new { area = "Administration", Id = sId });
            }
            var tables = new SelectList(await _context.Tables.Where(m => m.SectionId == sId).ToListAsync());
            return View(tables);
        }
    }
}
