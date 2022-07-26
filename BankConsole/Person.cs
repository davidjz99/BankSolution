namespace BankConsole;

public abstract class Person
{
    public abstract string getName();

    public string getCountry()
    {
        return "México";
    }
}

public interface IPerson
{
    string GetName();
    string getCountry();
}