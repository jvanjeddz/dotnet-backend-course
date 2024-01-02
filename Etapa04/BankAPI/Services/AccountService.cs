using BankAPI.Data;
using BankAPI.Data.BankModels;

namespace BankAPI.Services;

public class AccountService  {
    private readonly BankContext _context;

    public AccountService(BankContext context)  {
        _context = context;
    }

    public IEnumerable<Account> GetAll() {
        return _context.Accounts.ToList();
    }

    public Account? GetById(int id)  {
        return _context.Accounts.Find(id);
    }

    public Account Create(Account newAccount)  {

        var clientExists = _context.Clients.Any(c => c.Id == newAccount.ClientId);

        if (!clientExists)
        {
            throw new ArgumentException();
        }

        _context.Accounts.Add(newAccount);
        _context.SaveChanges();

        return newAccount;
    }


    public void Update(int id, Account account)   {
        var existingAccount = GetById(id);

        var clientExists = _context.Clients.Any(c => c.Id == account.ClientId);

        if (!clientExists)
        {
            throw new ArgumentException();
        }

        if(existingAccount is not null)  {
            existingAccount.AccountType =  account.AccountType;
            existingAccount.ClientId = account.ClientId;
            existingAccount.Balance = account.Balance;
            
            _context.SaveChanges();
        }

    }

    public void Delete(int id)  {
        var AccountToDelete = GetById(id);

        if(AccountToDelete is not null)  {
            _context.Accounts.Remove(AccountToDelete);
            _context.SaveChanges();
        }
    }

}