using MySql.Data.MySqlClient;
class User
{
    public string name;
    public string email;
    public string documentNumber;
    
}

class PaymentApp
{
    static void Main()
    {
        while (true)
        {
            Console.WriteLine("Bienvenido a la aplicación de pago de servicios");
            Console.WriteLine("1. Registrarse");
            Console.WriteLine("2. Iniciar sesión");
            Console.WriteLine("3. Salir");

            int choice = int.Parse(Console.ReadLine());

            if (choice == 1)
            {
                Register();
            }
            else if (choice == 2)
            {
                Login();
            }
            else if (choice == 3)
            {
                break;
            }
            else
            {
                Console.WriteLine("Opción inválida, intente de nuevo");
            }
        }
    }

    public static void Register()
    {
        User user = new User();

        Console.WriteLine("Ingresa tu nombre:");
        user.name = Console.ReadLine();
        Console.WriteLine("Ingresa tu email:");
        user.email = Console.ReadLine();
        Console.WriteLine("Ingresa tu numero de documento:");
        user.documentNumber = Console.ReadLine();

        // Aquí se simula la conexión a la base de datos MySQL y la INSERCCION de los datos del usuario en una tabla
        string connStr = "server=localhost;user=root;database=mydb;port=3306;password=Parisina1;";
        MySqlConnection conn = new MySqlConnection(connStr);
        try
        {
            Console.WriteLine("Connecting to MySQL...");
            conn.Open();

            string sql = "INSERT INTO users (name, email, document_number) VALUES (@name, @email, @document_number)";
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@name", user.name);
            cmd.Parameters.AddWithValue("@email", user.email);
            cmd.Parameters.AddWithValue("@document_number", user.documentNumber);
            cmd.ExecuteNonQuery();

            Console.WriteLine("Usuario registrado en la base de datos");
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
        }

        conn.Close();
    }

    public static void Login()
    {
        User user = new User();

        Console.WriteLine("Ingresa tu email:");
        user.email = Console.ReadLine();
        Console.WriteLine("Ingresa tu numero de documento:");
        user.documentNumber = Console.ReadLine();
        // Aquí se simula la conexión a la base de datos MySQL y la BUSQUEDA de los datos del usuario en la tabla
        string connStr = "server=localhost;user=root;database=mydb;port=3306;password=Parisina1;";
        MySqlConnection conn = new MySqlConnection(connStr);
        try
        {
            Console.WriteLine("Conectando a MySQL...");
            conn.Open();

            string sql = "SELECT name FROM users WHERE email = @email AND document_number = @document_number";
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@email", user.email);
            cmd.Parameters.AddWithValue("@document_number", user.documentNumber);
            MySqlDataReader rdr = cmd.ExecuteReader();

            if (rdr.HasRows)
            {
                rdr.Read();
                string name = rdr["name"].ToString();
                Console.WriteLine("Bienvenido " + name + ", iniciaste sesión correctamente");
                ShowServices();
            }
            else
            {
                Console.WriteLine("Usuario no encontrado");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
        }

        conn.Close();
    }
    static void ShowServices()
    {
        Console.WriteLine("Selecciona un servicio para realizar el pago:");
        Console.WriteLine("1 - ANDE");
        Console.WriteLine("2 - TIGO");
        Console.WriteLine("3 - PERSONAL");

        int option = Convert.ToInt32(Console.ReadLine());

        switch (option)
        {
            case 1:
                RegisterPayment("ANDE");
                break;
            case 2:
                RegisterPayment("TIGO");
                break;
            case 3:
                RegisterPayment("PERSONAL");
                break;
            default:
                Console.WriteLine("Opción inválida");
                break;
        }
    }

    static void RegisterPayment(string service)
    {
        User user; 
        Console.WriteLine("Ingresa su numero de documento:");
        

        Console.WriteLine("Ingresa el monto a pagar en " + service);
        double amount = Convert.ToDouble(Console.ReadLine());
        
        Console.WriteLine("Pago registrado correctamente en " + service);
        Main();
    }
    public static bool VerifyDocumentNumber(string documentNumber)
    {
        string connStr = "server=localhost;user=root;database=mydb;port=3306;password=Parisina1;";
        MySqlConnection conn = new MySqlConnection(connStr);
        try
        {
            conn.Open();

            string sql = "SELECT COUNT(*) FROM valid_documents WHERE document_number = @document_number";
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@document_number", documentNumber);
            int count = Convert.ToInt32(cmd.ExecuteScalar());

            if (count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
            return false;
        }
        finally
        {
            conn.Close();
        }
    }
}

