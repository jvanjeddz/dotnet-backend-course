using BankAPI.Data;
using BankAPI.Data.BankModels;
using BankAPI.Data.DTOs;
using Microsoft.EntityFrameworkCore;
namespace BankAPI.Services;

public class BankTransactionService
{
    private readonly BankContext _context;
    public BankTransactionService(BankContext context)
    {
        _context = context;
    }


    public async Task<IEnumerable<BankTransactionDtoOut>> GetAll()  {
        return await _context.BankTransactions.Select(a => new BankTransactionDtoOut    {
            Id = a.Id,
            Name = a.Account.Client.Name,
            transactionType = a.TransactionTypeNavigation.Name, 
            amount = a.Amount,
            externalAccount = a.ExternalAccount,
            regDate = a.RegDate
        }).ToListAsync();
    }

    public async Task<BankTransactionDtoOut?> GetDtoById(int id)    {
        return await _context.BankTransactions.
        Where(a => a.Id == id).
        Select(a => new BankTransactionDtoOut 
        {
            Id = a.Id,
            Name = a.Account.Client.Name,
            transactionType = a.TransactionTypeNavigation.Name, 
            amount = a.Amount,
            externalAccount = a.ExternalAccount,
            regDate = a.RegDate
        }).SingleOrDefaultAsync();
    }

}