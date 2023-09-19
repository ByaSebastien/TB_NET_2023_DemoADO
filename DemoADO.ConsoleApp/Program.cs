using System.Data;
using System.Data.SqlClient;

namespace DemoADO.ConsoleApp
{
    internal class Program
    {
        // Data Source=(localdb)\MSSQLLocalDB;User ID=sa;Password=********;Connect Timeout=60;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False
        // définir les informations de connection (server, database, user, password, ...)
        // string connectionString = @"server=(localdb)\MSSQLLocalDB;database=Pokemon;uid=sa;pwd=test1234";
        static string connectionString = @"server=(localdb)\MSSQLLocalDB;database=Pokemon;integrated security=true";
        static void Main(params string[] args)
        {
            #region exemples
            // pour SQL Server utiliser une SqlConnection (OracleConnection, MySqlConnection, ...)
            //using (SqlConnection connection = new SqlConnection(connectionString))
            //{
            //    // 1. ouvrir la connection
            //    connection.Open();

            //    // 2. Créer une commande (une commande contient la requete qui sera exécutée)
            //    SqlCommand command = connection.CreateCommand();
            //    //command.CommandText = "SELECT * FROM ...";
            //    //command.CommandText = "INSERT INTO ...";
            //    //command.CommandText = "DELETE FROM ...";
            //    //command.CommandText = "UPDATE ... SET ...";

            //    //// si on veut éxécuter une procedure stockée
            //    //command.CommandText = "SP_MyStoredProcedure";
            //    //command.CommandType = CommandType.StoredProcedure;

            //    // command.CommandText = "DELETE FROM [Type] WHERE [Name] LIKE '%e%'";

            //    command.CommandText = "SELECT * FROM [Pokemon] ORDER BY [Name]";

            //    // 3. Exécuter la commande
            //    SqlDataReader reader = command.ExecuteReader(); // le reader se place sur la ligne -1
            //    // reader.Read(); // Se place sur la ligne suivante (ligne 0)
            //    // reader.Read(); // ligne 1
            //    // reader.Read(); // ligne 2

            //    // tant que il reste des lignes à parcourir je me place sur la ligne suivante
            //    while (reader.Read())
            //    {
            //        Console.WriteLine($"Nom: {reader["Name"]}");
            //        decimal p = (decimal)reader["Weight"];
            //        double h = (int)reader["height"];

            //        string? description = reader["Description"] as string;
            //        Console.WriteLine(description);

            //        Console.WriteLine($"Poids: {p}");
            //        Console.WriteLine($"Taille: {h}");
            //        double bmi = (double)p / Math.Pow(h / 100, 2);
            //        Console.WriteLine($"BMI: {bmi}");
            //        Console.WriteLine("------------------");
            //    }

            //    // récupére la première valeur que l'on va récupérer
            //    // string value = (string)command.ExecuteScalar();

            //    // récupére le nombre de lignes modifiées
            //    // int value = command.ExecuteNonQuery();

            //    // Console.WriteLine(value);

            //    // à la fin
            //    // connection.Close(); // facultatif car la connection est fermée qd l'objet est disposé
            //} // à la fin des accolade l'objet est automatiquement disposé
            #endregion

            #region exercice (Créer une application qui demande à l'utilisateur d'entrer les données d'un pokemon et les sauvegarder en db)
            Console.WriteLine("Entrer le numero de votre pokemon");
            int id = int.Parse(Console.ReadLine());

            Console.WriteLine("Entrer le nom de votre pokemon");
            string nom = Console.ReadLine();

            Console.WriteLine("Entrer le taille de votre pokemon");
            int taille = int.Parse(Console.ReadLine());

            Console.WriteLine("Entrer le poids de votre pokemon");
            decimal poids = decimal.Parse(Console.ReadLine());

            Console.WriteLine("Entrer le type 1 de votre pokemon");
            int type1 = int.Parse(Console.ReadLine());

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = connection.CreateCommand();

                // ATTENTION INJECTION SQL !!!!
                command.CommandText = @$"INSERT INTO [Pokemon]
                    (Id, [Name], [Height], [Weight], [Type1Id])
                    VALUES ({id}, '{nom}', {taille}, {poids}, {type1})";

                command.ExecuteNonQuery();
            } 
            #endregion
        }
    }
}