using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProgrammingProject.Models.Reservation
{
    public class ReservationPerson
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

        public SelectList Titles { get; set; }
        [Display(Name = "Title")]
        public int TitleId { get; set; }

        [Required, StringLength(32, MinimumLength = 2, ErrorMessage = "Given Name must be within the character limit")]
        [Display(Name = "Given Name", Description = "First Name of the Person")]
        public string FirstName { get; set; }

        [Required, StringLength(32, MinimumLength = 2, ErrorMessage = "Last Name must be within the character limit")]
        [Display(Name = "Last Name", Description = "Last Name of the Person")]
        public string LastName { get; set; }

        [DataType(DataType.PhoneNumber), Required]
        [Phone(ErrorMessage = "Invalid Phone Number"), StringLength(10)]
        public string PhoneNumber { get; set; }

        [DataType(DataType.EmailAddress), Required]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; }

    }
}
