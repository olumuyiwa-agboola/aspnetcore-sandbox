﻿using System.ComponentModel.DataAnnotations;

namespace VerticalSliceArchitecture.API.Models
{
    public class LoginRequest
    {
        [Required]
        public string? Username{ get; set; }

        [Required]
        public string? Password{ get; set; }
    };
}
