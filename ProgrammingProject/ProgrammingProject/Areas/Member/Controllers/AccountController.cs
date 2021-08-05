using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProgrammingProject.Data;

namespace ProgrammingProject.Areas.Member.Controllers
{
    public class AccountController : MemberBaseController
    {
        public AccountController(ApplicationDbContext context, IMapper mapper, UserManager<IdentityUser> userManager) : base(context, mapper,userManager)
        {

        }

        public async Task<IActionResult> Create()
        {
            var m = new Models.Account.Create
            {
                Titles = new SelectList(await _context.Titles.ToListAsync(), "Id", "Name")
            };
            return View(m);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Models.Account.Create m)
        {
            if (ModelState.IsValid)
            {
                var user = new IdentityUser { UserName = m.Email, Email = m.Email, EmailConfirmed = true };
                var result = await _userManager.CreateAsync(user, m.Password);
                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, "Member");

                    var p = new Person
                    {
                        TitleId = m.TitleId,
                        FirstName = m.FirstName,
                        LastName = m.LastName,
                        PhoneNumber = m.PhoneNumber,
                        Email = m.Email,
                        UserId = user.Id
                    };

                    _context.People.Add(p);
                    await _context.SaveChangesAsync();
                    return RedirectToAction("Index", "Home", new { area = "Member" });
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
                m.Titles = new SelectList(await _context.Titles.ToArrayAsync(), "Id", "Name");
            }
            return View(m);
        }
    }
}
