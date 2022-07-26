using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace BankConsole;

public static class Storage
{
    static string filePath = AppDomain.CurrentDomain.BaseDirectory + @"\users.json";

    public static void AddUser(User user)
    {
        string json = "", usersInFile = "";
        if(File.Exists(filePath))
        {
            usersInFile = File.ReadAllText(filePath);
        }

        var listUsers = JsonConvert.DeserializeObject<List<object>>(usersInFile);

        if(listUsers == null)
        {
            listUsers = new List<object>();
        }

        listUsers.Add(user);

        JsonSerializerSettings settings = new JsonSerializerSettings { Formatting = Formatting.Indented };

        json = JsonConvert.SerializeObject(listUsers, settings);

        File.WriteAllText(filePath, json);
    }

    public static string ValidateID(int ID)
    {
        string usersInFile = "";
        var listUsers = new List<User>();

        if(File.Exists(filePath))
        {
            usersInFile = File.ReadAllText(filePath);
        }

        var listObjects = JsonConvert.DeserializeObject<List<object>>(usersInFile);

        if(listObjects == null)
        {
            return "There are no users in the file";
        }

        foreach (object obj in listObjects)
        {
            User newUser;
            JObject user = (JObject)obj;

            if(user.ContainsKey("TaxRegime"))
            {
                newUser = user.ToObject<Client>();
            }
            else
            {
                newUser = user.ToObject<Employee>();
            }

            listUsers.Add(newUser);
        }

        try
        {
            var userToAdd = listUsers.Where(user => user.getID() == ID).Single();
            if(userToAdd != null)
            {   
                System.Console.WriteLine("El ID ingresado ya existe. Ingresa un ID válido.");
                return "Error";
            }
        }
        catch(InvalidOperationException)
        {
            
            return "Success";
        }

        return "Error";
    }

    public static List<User> getNewUsers()
    {
        string usersInFile = "";
        var listUsers = new List<User>();

        if(File.Exists(filePath))
        {
            usersInFile = File.ReadAllText(filePath);
        }

        var listObjects = JsonConvert.DeserializeObject<List<object>>(usersInFile);

        if(listObjects == null)
        {
            return listUsers;
        }

        foreach(object obj in listObjects)
        {
            User newUser;
            JObject user = (JObject)obj;

            if(user.ContainsKey("TaxRegime"))
            {
                newUser = user.ToObject<Client>();
            }
            else
            {
                newUser = user.ToObject<Employee>();
            }

            listUsers.Add(newUser);
        }

        var newUserList = listUsers.Where(user => user.getRegisterDate().Date.Equals(DateTime.Today)).ToList();

        return newUserList;
    }

    public static string DeleteUser(int ID)
    {
        string usersInFile = "";
        var listUsers = new List<User>();

        if(File.Exists(filePath))
        {
            usersInFile = File.ReadAllText(filePath);
        }

        var listObjects = JsonConvert.DeserializeObject<List<object>>(usersInFile);

        if(listObjects == null)
        {
            return "There are no users in the file";
        }

        foreach (object obj in listObjects)
        {
            User newUser;
            JObject user = (JObject)obj;

            if(user.ContainsKey("TaxRegime"))
            {
                newUser = user.ToObject<Client>();
            }
            else
            {
                newUser = user.ToObject<Employee>();
            }

            listUsers.Add(newUser);
        }

        try
        {
            var userToDelete = listUsers.Where(user => user.getID() == ID).Single();
            listUsers.Remove(userToDelete);
        }
        catch(InvalidOperationException)
        {
            System.Console.WriteLine("El ID ingresado no existe. Ingresa un ID válido.");
            return "Error";
        }
        
        JsonSerializerSettings settings = new JsonSerializerSettings { Formatting = Formatting.Indented };

        string json = JsonConvert.SerializeObject(listUsers, settings);

        File.WriteAllText(filePath, json);

        return "Success";
    }
}