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
    [Area("Member"), Authorize(Roles ="Member")]
    public abstract class MemberBaseController : Controller
    {
        protected readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        protected readonly UserManager<IdentityUser> _userManager;

        public MemberBaseController(ApplicationDbContext context, IMapper mapper, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _mapper = mapper;
            _userManager = userManager;
        }
    }
}
