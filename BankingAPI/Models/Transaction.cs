using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BankingAPI.Models
{
    [Table("Transactions")]
    public class Transaction
    {
        [Key]
        public int Id { get; set; }

        //This will be created every instance of this class
        public string TransactionUniqueReference { get; set; }
        public decimal TransactionAmount { get; set; }
        public TranStatus TransactionStatus { get; set; }

        //depends on the TransactionsStatus
        public bool IsSuccessful => TransactionStatus.Equals(TranStatus.Success);
        public string TransactionSourceAccount { get; set; }
        public string TransactionDestinationAccount { get; set; }
        public string TransactionParticulars { get; set; }
        public TranType TransactionType { get; set; }
        public DateTime TransactionDate { get; set; }

        public Transaction()
        {
            TransactionUniqueReference = $"{Guid.NewGuid().ToString().Replace("-", "").Substring(1, 27)}";
        }
    }

    public enum TranStatus
    {
        Failed,
        Success,
        Error
    }

    public enum TranType
    {
        Deposit,
        Withdrawal,
        Transfer
    }
}
