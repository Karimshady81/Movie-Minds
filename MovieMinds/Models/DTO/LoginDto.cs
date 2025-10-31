﻿using System.ComponentModel.DataAnnotations;

namespace MovieMinds.Models.DTO
{
    public class LoginDto
    {
        [Required(ErrorMessage = "Email or Username is required")]
        public string EmailOrUsername { get; set; } = string.Empty;

        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        public string Password { get; set; } = string.Empty;

        public bool RememberMe { get; set; }

        public string LockOut { get; set; } = string.Empty;
    }
}
