using Microsoft.EntityFrameworkCore;
using BankAPI.Data;
using BankAPI.Data.BankModels;
using BankAPI.Data.DTOs;

namespace BankAPI.Services;

public class LoginService   {

    private readonly BankContext _context;
    public LoginService(BankContext context)    {
        _context = context;
    }

    public async Task<Administrator?> GetAdmin(AdminDto admin)  {
        return await _context.Administrators.
                    SingleOrDefaultAsync(x => x.Email == admin.Email && x.Pwd == admin.Pwd);
    }

    public async Task<Client?> GetClient(ClientDto client)  {
        var validEmail = await _context.Clients.SingleOrDefaultAsync(x => x.Email == client.Email);
        if(validEmail == null)  {
            return null;
        }
        else
            return validEmail;
    }
}