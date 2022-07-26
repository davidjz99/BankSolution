using MailKit.Net.Smtp;
using MimeKit;
using System.Text.RegularExpressions;

namespace BankConsole;

public static class EmailService
{
    //Métodos
    public static bool ValidateEmail(string email)
    {
        string emailFormat = "\\w+([-+.']\\w+)*@\\w+([-.]\\w+)*\\.\\w+([-.]\\w+)*";

        if(Regex.IsMatch(email, emailFormat))
        {
            if(Regex.Replace(email, emailFormat, String.Empty) .Length == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            return false;
        }

    }

    public static void SendMail()
    {
        var message = new MimeMessage();
        message.From.Add(new MailboxAddress ("David Juárez", "david99.mandar@gmail.com"));
        message.To.Add(new MailboxAddress ("Admin", "david99.recibir@gmail.com"));
        message.Subject = "BankConsole: Usuarios nuevos";

        message.Body = new TextPart("plain")
        {
            Text = getEmailText()
        };

        using (var client = new SmtpClient ())
        {
            client.Connect("smtp.gmail.com", 587, false);
            client.Authenticate("david99.mandar@gmail.com", "jvymelsqjkwrepjz");
            client.Send(message);
            client.Disconnect(true);
        }
    }

    private static string getEmailText()
    {
        List<User> newUsers = Storage.getNewUsers();

        if(newUsers.Count == 0)
        {
            return "No hay usuarios nuevos.";
        }

        string emailtext = "Usuarios agregados hoy:\n";

        foreach(User user in newUsers)
        {
            emailtext += "\t+ " + user.ShowDate() + "\n";
        }

        return emailtext;
    }
}