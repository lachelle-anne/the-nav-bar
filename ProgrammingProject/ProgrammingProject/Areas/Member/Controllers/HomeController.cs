using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ProgrammingProject.Data;

namespace ProgrammingProject.Areas.Member.Controllers
{
    public class HomeController : MemberBaseController
    {
        public HomeController(ApplicationDbContext context, IMapper mapper, UserManager<IdentityUser> userManager) : base(context, mapper, userManager)
        {

        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
