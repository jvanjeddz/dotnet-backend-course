using BankAPI.Services;
using Microsoft.AspNetCore.Mvc;
using BankAPI.Data.BankModels;
using BankAPI.Data.DTOs;
namespace BankAPI.Controllers;
using Microsoft.AspNetCore.Authorization;

[Authorize]
[ApiController]
[Route("api/transactions")]
public class BankTransactionContoller : ControllerBase
{
    private readonly BankTransactionService bankTransactionService;
    private readonly AccountService accountService;
    private readonly ClientService clientService;
    public BankTransactionContoller(BankTransactionService bankTransactionService, AccountService accountService, ClientService clientService)
    {
        this.bankTransactionService = bankTransactionService;
        this.accountService = accountService;
        this.clientService = clientService;
    }
    [Authorize(Policy = "SuperAdmin")]
    [HttpGet("getall")]
    public async Task<IEnumerable<BankTransactionDtoOut>> Get() {
        return await bankTransactionService.GetAll();
    }

    [Authorize(Policy = "SuperAdmin")]
    [HttpGet("{id}")]
    public async Task<ActionResult<BankTransactionDtoOut>> GetById(int id)  {
        var transaction = await bankTransactionService.GetDtoById(id);

        if(transaction is null)
            return NotFound();
        
        return transaction;
    }

    [Authorize(Policy = "AuthClient")]
    [HttpGet("accounts/{id}")]
    public async Task<IEnumerable<AccountDtoOut>> GetAccounts(int id)   {
        var accountList = await accountService.GetAccountsByClientId(id);

        return accountList;
    }


    [Authorize(Policy = "AuthClient")]
    [HttpDelete("accounts/delete/{id}")]
    public async Task<IActionResult> DeleteAccount(int id)  {
        var accountToDelete = await accountService.GetById(id);

        if(accountToDelete is not null && accountToDelete.Balance.Equals(0))    {
            await accountService.Delete(id);
        }
        else if(accountToDelete is null)    {
            return AccountNotFound(id);
        }
        else if(!accountToDelete.Balance.Equals(0)) {
            return BadRequest(new {message = "La cuenta sigue disponiendo de dinero."});
        }

        return Ok();
    }

    public NotFoundObjectResult AccountNotFound(int id) {
        return NotFound(new { message = $"La cuenta con ID = {id} no existe."});
    }
}