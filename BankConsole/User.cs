using Newtonsoft.Json;

namespace BankConsole;

public class User
{
    //Atributos
    [JsonProperty]
    protected int ID { get; set; }
    [JsonProperty]
    protected string Name { get; set; }
    [JsonProperty]
    protected string Email { get; set; }
    [JsonProperty]
    protected decimal Balance { get; set; }
    [JsonProperty]
    protected DateTime RegisterDate { get; set; }

    //Constructores    
    public User() {}

    public User(int ID, string Name, string Email, decimal Balance)
    {
        this.ID = ID;
        this.Name = Name;
        this.Email = Email;
        this.RegisterDate = DateTime.Now;
    }

    //Métodos
    public int getID()
    {
        return ID;
    }

    public DateTime getRegisterDate()
    {
        return RegisterDate;
    }

    public virtual void setBalance(decimal amount)
    {
        decimal quantity = 0;

        if(amount > 0){
            quantity = amount;
        }
        else{
            quantity = 0;
        }

        this.Balance += quantity;
    }

    public virtual string ShowDate()
    {
        return $"ID: {this.ID}, Nombre: {this.Name}, Correo: {this.Email}, Saldo: {this.Balance}, Fecha de registro: {this.RegisterDate.ToShortDateString()}";
    }

    public string ShowDate(string initialMessage)
    {
        return $"{initialMessage}\nNombre: {this.Name}, Correo: {this.Email}, Saldo: {this.Balance}, Fecha de registro: {this.RegisterDate.ToShortDateString()}";
    }
}