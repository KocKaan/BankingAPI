using System;
using System.Linq;
using BankingAPI.DAL;
using BankingAPI.Models;
using BankingAPI.Services.Interfaces;
using BankingAPI.Utils;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace BankingAPI.Services
{
    public class TransactionService: ITransactionService
    {
        private BankDbContext _dbContext;
        ILogger<TransactionService> _logger;
        private AppSettings _settings;
        private static string _ourBankSettlementaccount;

        public TransactionService(BankDbContext dbContext, ILogger<TransactionService> logger, IOptions<AppSettings> settings)
        {
            _dbContext = dbContext;
            _logger = logger;
            _settings = settings.Value;
            _ourBankSettlementaccount = _settings.OurBankSettlementAccount;
        }

        public Response CreateNewTransaction(Transaction transaction)
        {
            Response response = new Response();
            _dbContext.Transactions.Add(transaction);
            _dbContext.SaveChanges();
            response.ResponseCode = "00";
            response.ResponseMessage = "Transaction created successfully";
            response.Data = null;

            return response;
        }

        public Response FindTransactionByDate(DateTime date)
        {
            var transaction = _dbContext.Transactions.Where(x => x.TransactionDate == date).ToList();
            response.ResponseCode = "00";
            response.ResponseMessage = "Transaction created successfully";
            response.Data = null;


        }

        public Response MakeDeposit(string AccountNumber, decimal Amount, string TransactionPin)
        {
            throw new NotImplementedException();
        }

        public Response MakeFundsTransfer(string FromAccount, string ToAccount, decimal Amount, string TransactionPin)
        {
            throw new NotImplementedException();
        }

        public Response MakeWithdrawal(string AccountNumber, decimal Amount, string TransactionPin)
        {
            throw new NotImplementedException();
        }
    }
}
