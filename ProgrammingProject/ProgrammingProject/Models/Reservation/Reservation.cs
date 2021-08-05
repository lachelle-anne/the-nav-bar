using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProgrammingProject.Models.Reservation
{
    public class Reservation
    {
        public int Id { get; set; }

        [Required, Display(Name = "Start"), DataType(DataType.DateTime)]
        public DateTime Start { get; set; }

        [Required, Display(Name = "End"), DataType(DataType.DateTime)]
        public DateTime End { get; set; }

        [Display(Name = "Guests")]
        public int Guests { get; set; }

        [Display(Name = "Special Notes")]
        public string Notes { get; set; }

        public SelectList Sittings { get; set; }
        public int SittingId { get; set; }

        [Display(Name ="Booker")]
        public Person.Person Person { get; set; }
    }
}
