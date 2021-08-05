using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProgrammingProject.Data
{
    public class Person
    {
        public int Id { get; set; }
        public Title Title { get; set; }
        public int TitleId { get; set; }
        [Required, StringLength(32, MinimumLength = 2, ErrorMessage = "First Name must be within the character limit")]
        public string FirstName { get; set; }
        [Required, StringLength(32, MinimumLength = 2, ErrorMessage = "Last Name must be within the character limit")]
        public string LastName { get; set; }
        [DataType(DataType.EmailAddress), Required]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; }

        [DataType(DataType.PhoneNumber), Required, StringLength(10)]
        [Phone(ErrorMessage = "Invalid Phone Number")]
        public string PhoneNumber { get; set; }

        [MaxLength(450)]
        public string UserId { get; set; }
    }
}
