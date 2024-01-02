namespace BankConsole;

public abstract class Person    {
    public abstract string getName();

    public string getCountry()    {
        return "Mexico";
    }
}

public interface IPerson    {
    string getName();

    string getCountry();
}