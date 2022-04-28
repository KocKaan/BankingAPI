﻿using System;
using System.ComponentModel.DataAnnotations;

namespace BankingAPI.Models
{
    public class AuthenticateModel
    {
        public AuthenticateModel()
        {
        }

        [Required]
        [RegularExpression(@"^[0][1-9]/d{9}$|[1-9]\d{9}$")]
        public string AccountNumber { get; set; }

        [Required]
        public string Pin { get; set; }


    }
}
