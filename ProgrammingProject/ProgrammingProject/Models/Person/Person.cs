using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProgrammingProject.Models.Person
{
    public class Person
    {
        public int Id { get; set; }
        public SelectList Titles { get; set; }
        [Display(Name ="Title")]
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
