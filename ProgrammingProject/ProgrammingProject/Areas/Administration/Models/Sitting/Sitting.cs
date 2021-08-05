using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProgrammingProject.Areas.Administration.Models.Sitting
{
    public class Sitting
    {
        public int Id { get; set; }
        public string SittingName { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public int Capacity { get; set; }
        public SelectList Restaurants { get; set; }
        public int RestaurantId { get; set; }
        public int SittingTypeId { get; set; }
        public SelectList SittingTypes { get; set; }
        public bool IsClosed { get; set; }
    }
}
