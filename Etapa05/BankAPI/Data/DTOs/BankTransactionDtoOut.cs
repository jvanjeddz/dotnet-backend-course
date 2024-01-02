namespace BankAPI.Data.DTOs;

public class BankTransactionDtoOut
{
    public int Id {get; set;}
    
    public string Name {get; set;} = null!;

    public string transactionType {get; set;} = null!;

    public decimal amount {get; set;}
    
    public int? externalAccount {get; set;} = null!;

    public DateTime regDate {get; set;}

}