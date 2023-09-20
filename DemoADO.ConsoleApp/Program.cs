using System.Data;
using System.Data.SqlClient;
using System.Security.Cryptography.X509Certificates;

namespace DemoADO.ConsoleApp
{
    internal class Program
    {
        // Data Source=(localdb)\MSSQLLocalDB;User ID=sa;Password=********;Connect Timeout=60;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False
        // définir les informations de connection (server, database, user, password, ...)
        // string connectionString = @"server=(localdb)\MSSQLLocalDB;database=Pokemon;uid=sa;pwd=test1234";
        static string connectionString = @"server=BSTORM\SQLEXPRESS;database=Pokemon;integrated security=true";
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
            //Console.WriteLine("Entrer le numero de votre pokemon");
            //int id = int.Parse(Console.ReadLine());

            //Console.WriteLine("Entrer le nom de votre pokemon");
            //string nom = Console.ReadLine();

            //Console.WriteLine("Entrer le taille de votre pokemon");
            //int taille = int.Parse(Console.ReadLine());

            //Console.WriteLine("Entrer le poids de votre pokemon");
            //decimal poids = decimal.Parse(Console.ReadLine());

            //Console.WriteLine("Entrer le type 1 de votre pokemon");
            //int type1 = int.Parse(Console.ReadLine());

            //Console.WriteLine("As-t'il un deuxième type ? (y/n)");
            //string choix = Console.ReadLine();

            //int? type2 = null;
            //if(choix.ToLower() == "y")
            //{
            //    Console.WriteLine("Entrer le type 2 de votre pokemon");
            //    type2 = int.Parse(Console.ReadLine());
            //}


            //Pokemon newPokemon = new Pokemon(id,nom,taille,poids,type1,type2);

            //AjouterPokemon(newPokemon);


            #endregion

            #region Demo lecture Pokemon

            Console.WriteLine("Quel pokemon cherchez-vous?");
            int id = int.Parse(Console.ReadLine());
            Pokemon pokemon = RecupPokemon(id);
            Console.WriteLine($"{pokemon.Id} : {pokemon.Nom}");

            #endregion
        }

        public static void AjouterPokemon(Pokemon pokemon)
        {
            string nomTable = "pokemon";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = connection.CreateCommand();

                // ATTENTION INJECTION SQL !!!!
                command.CommandText = @$"INSERT INTO [{nomTable}]
                    (Id, [Name], [Height], [Weight], [Type1Id], [Type2Id])
                    VALUES (@id, @nom, @taille, @poids, @type1, @type2)";

                //SqlParameter sqlParameter = command.CreateParameter();
                //sqlParameter.ParameterName = "@id";
                //sqlParameter.Value = id;
                //command.Parameters.Add(sqlParameter);

                command.Parameters.AddWithValue("@id", pokemon.Id);
                command.Parameters.AddWithValue("@nom", pokemon.Nom);
                command.Parameters.AddWithValue("@taille", pokemon.Taille);
                command.Parameters.AddWithValue("@poids", pokemon.Poids);
                command.Parameters.AddWithValue("@type1", pokemon.Type1Id);
                command.Parameters.AddWithValue("@type2", pokemon.Type2Id is null ? DBNull.Value : pokemon.Type2Id);

                int nbLignes = command.ExecuteNonQuery();
                if (nbLignes == 1)
                {
                    Console.WriteLine("Succes");
                }
                else
                {
                    Console.WriteLine("Erreur");
                }
                connection.Close();
            }
        }
        public static Pokemon RecupPokemon(int id)
        {
            using(SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = connection.CreateCommand();
                command.CommandText = "SELECT * FROM POKEMON WHERE Id = @id";
                command.Parameters.AddWithValue ("@id", id);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                Pokemon pokemon;
                if (reader.Read())
                {
                    int Resultid = (int)reader["Id"];
                    string nom = (string)reader["Name"];
                    int taille = (int)reader["Height"];
                    decimal poids = (decimal)reader["Weight"];
                    int type1 = (int)reader["Type1ID"];
                    int? type2 = reader["Type2ID"] == DBNull.Value ? null : (int)reader["Type2ID"];

                    pokemon = new Pokemon(Resultid,nom,taille,poids,type1,type2);
                }
                else
                {
                    throw new KeyNotFoundException();
                }
                connection.Close();
                return pokemon;
            }
        }
    }
}