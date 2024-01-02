using Microsoft.AspNetCore.Mvc;
using BankAPI.Services;
using BankAPI.Data.BankModels;

namespace BankAPI.Controllers;

[ApiController]
[Route("[Controller]")]
public class AccountController: ControllerBase  {
    private readonly AccountService _service;
    public AccountController(AccountService service) {
        _service = service;
    }

    [HttpGet]
    public IEnumerable<Account> Get()    {
        return _service.GetAll();
    }

    [HttpGet("{id}")]
    public ActionResult<Account> GetById(int id)    {
        var account = _service.GetById(id);

        if (account is null)
            return NotFound();
        
        return account;
    }

    [HttpPost]
    public IActionResult Create(int id, Account account)  {

        //var newAccount = _service.Create(account);

        try
            {
            var newAccount = _service.Create(account);
            return CreatedAtAction(nameof(GetById), new { id = newAccount.Id}, newAccount);
            }
        catch (ArgumentException)
            {
            return BadRequest();
            }
    }

    [HttpPut("{id}")]
    public IActionResult Update(int id, Account account) {

        if (id != account.Id)
            return BadRequest();

    try
        {
            _service.Update(id, account);
            return NoContent();
        }
    catch (ArgumentException)
        {
            return BadRequest();
        }

    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id) {
        var accountToDelete = _service.GetById(id);

        if(accountToDelete is not null)  {
            _service.Delete(id);
            return Ok();
        }
        else    {
            return NotFound();
        }
    }
}