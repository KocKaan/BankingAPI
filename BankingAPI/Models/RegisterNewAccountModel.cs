using System;
using System.ComponentModel.DataAnnotations;

namespace BankingAPI.Models
{
    public class RegisterNewAccountModel
    {
        
        //DTO for AccountModel
        
        public string FirstName { get; set; }
        public string LastName { get; set; }
        // public string AccountName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        //public decimal CurrentAccountBalance { get; set; }
        public AccountType AccountType { get; set; }

        //generate account number here 
        //public string AccountNumberGenerated { get; set; }

        //storing the hash and salt of the Account Transaction pin
        //public byte[] PinHash { get; set; }
        //public byte[] PinSalt { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateLastUpdated { get; set; }

        [Required]
        [RegularExpression(@"^[0-9]{4}$",ErrorMessage ="Pin must not be more than 4 digits")]
        public string Pin { get; set; }

        [Required]
        [Compare("Pin", ErrorMessage = "Pins do not match")]
        public string ConfirmPin { get; set; }//compare pin and confirm pin

    }
}
