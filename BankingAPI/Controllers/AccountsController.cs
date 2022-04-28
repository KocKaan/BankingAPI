using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using AutoMapper;
using BankingAPI.Models;
using BankingAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace BankingAPI.Controllers
{
    [ApiController]
    [Route("api/v3/[controller]")]
    public class AccountsController: ControllerBase
    {
    
        private IAccountService _accountService;

        IMapper _mapper;

        
    
        public AccountsController(AccountService accountService, IMapper mapper)
        {
            _accountService = accountService;
            _mapper = mapper;

        }

        //register new account
        [HttpPost]
        [Route("register_new_account")]
        public IActionResult RegisterNewAccount([FromBody] RegisterNewAccountModel newAccount)
        {
            if (!ModelState.IsValid) return BadRequest(newAccount);

            var account = _mapper.Map<Account>(newAccount);
            // we do this because new account wont have encrypted pin
            return Ok(_accountService.Create(account, newAccount.Pin, newAccount.ConfirmPin));

        }

        [HttpGet]
        [Route("get_all_accounts")]
        public IActionResult GetAllAccounts()
        {
            var allAccounts = _accountService.GetAllAccounts();
            var getCleanedAccounts = _mapper.Map<IList<GetAccountModel>>(allAccounts);
            return Ok(getCleanedAccounts);
        }

        [HttpPost]
        [Route("authenticate")]
        public IActionResult Authenticate([FromBody] AuthenticateModel model)
        {
            if (!ModelState.IsValid) return BadRequest(model);

            return Ok(_accountService.Authenticate(model.AccountNumber, model.Pin));

        }

        [HttpGet]
        [Route("get_by_account_number")]
        public IActionResult GetByAccountNumber(string AccountNumber)
        {
            if (!Regex.IsMatch(AccountNumber, @"^[0][1-9]/d{9}$|[1-9]\d{9}$")) return BadRequest("Account Number must be 10 digit");

            var account = _accountService.GetByAccountNumber(AccountNumber);

            var cleanAccount = _mapper.Map<GetAccountModel>(account);
            return Ok(cleanAccount);
        }

        [HttpGet]
        [Route("get_account_by_id")]
        public IActionResult GetByAccountById(int id)
        {
            
            var account = _accountService.GetById(id);

            var cleanAccount = _mapper.Map<GetAccountModel>(account);
            return Ok(cleanAccount);
        }

        [HttpPut]
        [Route("update_account")]
        public IActionResult UpdateAccount([FromBody] UpdateAccountModel model, string Pin)
        {
            if (!ModelState.IsValid) return BadRequest(model);

            var account = _mapper.Map<Account>(model);

            _accountService.Update(account, model.Pin);
            return Ok();
        }
    }
}
