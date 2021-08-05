using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProgrammingProject.Models.Reservation
{
    public class ReservationDetails
    {
        public int Id { get; set; }

        public int SittingId { get; set; }
        public Data.Sitting Sitting { get; set; }

        [Required, Display(Name = "Start"), DataType(DataType.DateTime)]
        public DateTime Start { get; set; }

        [Required, Display(Name = "End"), DataType(DataType.DateTime)]
        public DateTime End { get; set; }


        [Display(Name = "Guests")]
        public int Guests { get; set; }

        [Display(Name = "Special Notes")]
        public string Notes { get; set; }

        [Display(Name = "Booker")]
        public Data.Person Person { get; set; }
    }
}
