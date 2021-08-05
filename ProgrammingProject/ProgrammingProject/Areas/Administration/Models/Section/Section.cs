using Microsoft.AspNetCore.Mvc.Rendering;
using ProgrammingProject.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProgrammingProject.Areas.Administration.Models.Section
{
    public class Section
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public SelectList Restaurants { get; set; }
        public int RestaurantId { get; set; }
    }
}
