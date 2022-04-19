using System;
namespace BankingAPI.Models
{
    public class Transaction
    {
        public Transaction()
        {
            [Key]
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string AccountNumberGenerated { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateLastUpdated { get; set; }
        public decimal CurrentAccountBalance { get; set; }
        //cummulative
        [JsonIgnore]
        public byte[] PinStoredHash { get; set; }
        [JsonIgnore]
        public byte[] PinStoredSalt { get; set; }
    }
    }
}
