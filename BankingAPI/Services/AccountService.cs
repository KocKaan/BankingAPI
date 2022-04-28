using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BankingAPI.DAL;
using BankingAPI.Models;

namespace BankingAPI.Services
{
    public class AccountService : IAccountService
    {
        private BankDbContext _dbContext;

        public AccountService(BankDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Account Authenticate(string AccountNumber, string Pin)
        {
            //does account exist for that numeber

            var account = _dbContext.Accounts.Where(x => x.AccountNumberGenerated == AccountNumber).SingleOrDefault();
            if (account == null)
            {
                return null;
            }

            //verify pinshash
            if (!VerifyPinHash(Pin, account.PinHash, account.PinSalt))
            {
                return null;
            }

            return account;
        }

        private static bool VerifyPinHash(string Pin, byte[] pinHash, byte[] pinSalt)
        {
            if (string.IsNullOrWhiteSpace(Pin)) throw new ArgumentException("Pin missing");

            using (var hmac = new System.Security.Cryptography.HMACSHA512(pinSalt))
            {
                var computedPinHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(Pin));
                for (int i = 0; i < computedPinHash.Length; i++)
                {
                    if (computedPinHash[i] != pinHash[i]) return false;
                }
            }
            return true;
        }

        public Account Create(Account account, string Pin, string confirmPin)
        {
            //Create new Account
            if (_dbContext.Accounts.Any(x => x.Email == account.Email)) throw new ApplicationException("Account already exists");

            if (!Pin.Equals(confirmPin)) throw new ArgumentException("Pins do not match");

            //hashing and encyption the pin
            //out is used to state that the parameter passed must be modified by the method.
            byte[] pinHash, pinSalt;
            CreatePinHash(Pin, out pinHash, out pinSalt);

            account.PinHash = pinHash;
            account.PinSalt = pinSalt;

            _dbContext.Accounts.Add(account);
            _dbContext.SaveChanges();

            return account;
        }

        private static void CreatePinHash(string pin, out byte[] pinHash, out byte[] pinSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                pinSalt = hmac.Key;
                pinHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(pin));
            }
        }

        public void Delete(int Id)
        {
            var account = _dbContext.Accounts.Find(Id);
            if (account != null)
            {
                _dbContext.Accounts.Remove(account);
                _dbContext.SaveChanges();
            }
        }

        public IEnumerable<Account> GetAllAccounts()
        {
            return _dbContext.Accounts.ToList();
        }

        public Account GetByAccountNumber(string AccountNumber)
        {
            var account = _dbContext.Accounts.Where(x => x.AccountNumberGenerated == AccountNumber).FirstOrDefault();

            if (account == null) return null;

            return account;
        }

        public Account GetById(int Id)
        {
            var account = _dbContext.Accounts.Where(x => x.Id == Id).FirstOrDefault();
            if (account == null) return null;

            return account;
        }

        public void Update(Account account, string Pin = null)
        {
            var accountToBeUpdated = _dbContext.Accounts.Where(x => x.Email == account.Email).SingleOrDefault();
            if (accountToBeUpdated == null) throw new ApplicationException("Application doesnt exist");

            //change email
            if (!string.IsNullOrEmpty(account.Email))
            {

                if (_dbContext.Accounts.Any(x => x.Email == account.Email)) throw new ApplicationException("Email " + account.Email + " has been taken");

                accountToBeUpdated.Email = account.Email;
            }

            //change phone number
            if (!string.IsNullOrEmpty(account.PhoneNumber))
            {

                if (_dbContext.Accounts.Any(x => x.PhoneNumber == account.PhoneNumber)) throw new ApplicationException("Phone number " + account.PhoneNumber + " has been taken");

                accountToBeUpdated.PhoneNumber = account.PhoneNumber;
            }

            //change pin
            if (!string.IsNullOrWhiteSpace(Pin))
            {
                byte[] pinHash, pinSalt;
                CreatePinHash(Pin, out pinHash, out pinSalt);

                accountToBeUpdated.PinHash = pinHash;
                accountToBeUpdated.PinSalt = pinSalt;

            }

            _dbContext.Accounts.Update(accountToBeUpdated);
            _dbContext.SaveChanges();
        }
    }
}
