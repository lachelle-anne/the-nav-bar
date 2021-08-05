using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ProgrammingProject.Data
{
    public class Sitting

        //instance of the sitting
    {
        public int Id { get; set; }

        public SittingType SittingType { get; set; }

        public int SittingTypeId { get; set; }

        [Required]
        public string Name { get; set; }

        [Required, Display(Name = "Start"), DataType(DataType.DateTime)]
        public DateTime Start { get; set; }

        [Required, Display(Name = "End"), DataType(DataType.DateTime)]
        public DateTime End { get; set; }

        public int Capacity { get; set; }

        public string Details { get => $"{Start.ToString("ddd d MMM")} - {Start.ToString("hh:mm tt")} to {End.ToString("hh:mm tt")}"; }

        public Restaurant Restaurant { get; set; }
        public int RestaurantId { get; set; }

        public bool IsClosed { get; set; }


    }
}
