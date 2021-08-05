using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ProgrammingProject.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProgrammingProject.Areas.Administration.Controllers
{
    public class HomeController : AdministrationBaseController
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
