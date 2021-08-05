using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ProgrammingProject.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProgrammingProject.Areas.Administration.Controllers
{
    [Area("Administration"), Authorize(Roles = "Admin")]
    public class AdministrationBaseController : Controller
    {
        protected readonly ApplicationDbContext _context;
        protected readonly IMapper _mapper;
        protected readonly UserManager<IdentityUser> _userManager;

        public AdministrationBaseController(ApplicationDbContext context, IMapper mapper, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _mapper = mapper;
            _userManager = userManager;
        }
        public ApplicationDbContext Context { get; }

        public IMapper Mapper { get; }
    }
}
 