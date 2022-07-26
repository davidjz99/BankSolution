using BankConsole;

if (args.Length == 0)
{
    EmailService.SendMail();
}
else
{
    showMenu();
}

void showMenu()
{
    Console.Clear();
    Console.WriteLine("Selecciona una opción: ");
    Console.WriteLine("1 - Crear un usuario nuevo.");
    Console.WriteLine("2 - Eliminar un usuario existente.");
    Console.WriteLine("3 - salir.");

    int option = 0;
    do{
        string input = Console.ReadLine();

        if(!int.TryParse(input, out option))
        {
            System.Console.WriteLine("Debes ingresar un número (1, 2 o 3).");
        }
        else if(option <= 0 || option > 3)
        {
            System.Console.WriteLine("Debes ingresar un número válido (1, 2 o 3).");
        }
    }while (option <= 0 || option > 3);

    switch (option)
    {
        case 1:
            createUser();
            break;
        case 2:
            deleteUser();
            break;
        case 3:
            Environment.Exit(0);
            break;
    }
}

void createUser()
{
    int ID = 0;
    bool idValidation = true;
    string result = "";

    decimal balance = 0;
    bool balanceValidation = true;

    char userType;
    bool userValidation = true;

    Console.Clear();
    System.Console.WriteLine("Ingresa la información del usuario: ");

    //Validar ID
    do
    {
        System.Console.Write("ID: ");
        string input = Console.ReadLine();

        if(!int.TryParse(input, out ID))
        {
            idValidation = false;
            System.Console.WriteLine("Debes ingresar un valor numérico.");
        }
        else if(ID <= 0)
        {
            idValidation = false;
            System.Console.WriteLine("Debes ingresar un número válido (entero positivo mayor a 0).");
        }
        else
        {
            result = Storage.ValidateID(ID);

            if(result.Equals("Success"))
            {
                idValidation = true;
            }
        }
    } while(idValidation == false || result.Equals("Error"));

    System.Console.Write("Nombre: ");
    string name = Console.ReadLine();

    //Validar Email
    bool checkEmail = true;
    string email = "";
    do
    {
        System.Console.Write("Email: ");
        email = Console.ReadLine();

        checkEmail = EmailService.ValidateEmail(email);

        if(!checkEmail)
        {
            System.Console.WriteLine("Correo inválido. Prueba de nuevo");
        }
    } while(!checkEmail);
    

    //Validar Saldo
    do
    {
        System.Console.Write("Saldo: ");
        string input = Console.ReadLine();

        if(!decimal.TryParse(input, out balance))
        {
            balanceValidation = false;
            System.Console.WriteLine("Debes ingresar un valor numérico.");
        }
        else if(balance <= 0)
        {
            System.Console.WriteLine("Debes ingresar un saldo válido (mayor a 0).");
        }
        else
        {
            balanceValidation = true;
        }
    } while(balanceValidation == false || balance <= 0);

    //Validar tipo de usuario
    do
    {
        System.Console.Write("Escriba 'c' si el usuario es Cliente, 'e'  si es Empleado: ");
        string input = Console.ReadLine();

        if(!char.TryParse(input, out userType))
        {
            userValidation = false;
            System.Console.WriteLine("Debes ingresar un único caracter ('c' o 'e').");
        }
        else if(userType.Equals('c') || userType.Equals('e'))
        {
            userValidation = true;
        }
        else
        {
            userValidation = false;
            System.Console.WriteLine("Caracter inválido. Seleccion 'c' para Cliente, o 'e' para Empleado.");
        }
    } while(userValidation == false);
    
    User newUser;

    if(userType.Equals('c'))
    {
        System.Console.Write("Regimen Fiscal: ");
        char TaxRegime = char.Parse(Console.ReadLine());

        newUser = new Client(ID, name, email, balance, TaxRegime);
    }
    else
    {
        System.Console.Write("Departamento: ");
        string department = Console.ReadLine();

        newUser = new Employee(ID, name, email, balance, department);
    }

    Storage.AddUser(newUser);

    System.Console.WriteLine("Usuario creado.");
    Thread.Sleep(2000);
    showMenu();
}

void deleteUser()
{
    int ID = 0;
    bool idValidation = true;
    string result = "";
    int i = 0;

    Console.Clear();

    do
    {
        
        if(i > 0)
        {
            System.Console.Write("ID: ");
        }
        else
        {
            System.Console.Write("Ingresa el ID del usuario a eliminar: ");
            i = 1;
        }
        
        string stID = Console.ReadLine();

        if(!int.TryParse(stID, out ID))
        {
            idValidation = false;
            System.Console.WriteLine("Ingresa un valor numérico.");
        }
        else if(ID <= 0)
        {
            idValidation = false;
            System.Console.WriteLine("El numero debe ser un entero positivo mayor a 0.");
        }
        else
        {
            result = Storage.DeleteUser(ID);

            if(result.Equals("Success"))
            {
                idValidation = true;
                System.Console.Write("Usuario eliminado.");
                Thread.Sleep(2000);
                showMenu();
            }
        }
    } while(!idValidation || result.Equals("Error"));
}