namespace BankConsole;

public class Client : User
{
    public char TaxRegime { get; set; }

    public Client() {}

    public Client(int ID, string Name, string Email, decimal Balance, char TaxRegime) : base (ID, Name, Email, Balance)
    {
      this.TaxRegime = TaxRegime;
      setBalance(Balance);  
    }

    public override void setBalance(decimal amount)
    {
        base.setBalance(amount);

        if(TaxRegime.Equals('M'))
        {
            Balance += (amount * 0.02m);
        }
    }

    public override string ShowDate()
    {
        return base.ShowDate() + $", Régimen Fiscal: {this.TaxRegime}";
    }
}