using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace ProgrammingProject.Data
{
    public class Reservation
    {

        public int Id { get; set; }

        public Sitting Sitting { get; set; }

        public int SittingId { get; set; }

        [Required, Display(Name = "Start"), DataType(DataType.DateTime)]
        public DateTime Start { get; set; }

        [Required, Display(Name = "End"), DataType(DataType.DateTime)]
        public DateTime End { get; set; }

        [Required]
        public int Guests { get; set; }

        public string Notes { get; set; }

        public ReservationSource Source { get; set; }
        public int SourceId { get; set; }

        public ReservationStatus Status { get; set; }
        public int StatusId { get; set; }

        //FK - Data.Person.Id
        public Person Booker { get; set; }
        public int BookerId { get; set; }


    }
}
