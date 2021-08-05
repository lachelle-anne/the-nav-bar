using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProgrammingProject.Models.Reservation
{
    public class Sitting
    {
        public int SittingId { get; set; }
        public SelectList SittingOptions { get; set; }

    }
}
