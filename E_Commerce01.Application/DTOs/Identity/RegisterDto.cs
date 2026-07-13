using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce01.Application.DTOs.Identity
{
    public class RegisterDto
    {
        [Required(ErrorMessage = "Email is Required"), EmailAddress]
        public string Email { get; set; } = default!;
        [Required(ErrorMessage = "Password is Required")]
        public string Password { get; set; } = default!;
        [Required(ErrorMessage = "DisplayName is Required")]
        public string DisplayName { get; set; } = default!;
        [Required(ErrorMessage = "UserNaem is Required")]
        public string UserName { get; set; } = default!;
        public string PhoneNumber { get; set; } = default!;
    }
}
