using System.Data;
using System.Data.SqlClient;
using System.Security.Cryptography.X509Certificates;

namespace DemoADO.ConsoleApp
{
    internal class Program
    {

        static PokemonRepository pokemonRepository = new PokemonRepository();
        static TypeRepository typeRepository = new TypeRepository();
        
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

            //Console.WriteLine("Quel pokemon cherchez-vous?");
            //int id = int.Parse(Console.ReadLine());
            //Pokemon pokemon = RecupPokemon(id);
            //Console.WriteLine($"{pokemon.Id} : {pokemon.Nom}");

            //List<Pokemon> pokemonList = RecupPokemons();

            //pokemonList.ForEach(pokemon =>
            //{
            //    Console.WriteLine($"{pokemon.Id} : {pokemon.Nom}");
            //});

            //List<Pokemon> pokemons = pokemonList.Where(p => p.Type1Id == 1).ToList();

            //foreach(Pokemon pokemon in pokemons)
            //{
            //    Console.WriteLine($"{pokemon.Id} : {pokemon.Nom} {pokemon.Type1Id}");
            //}

            //Console.WriteLine("Quel pokemon cherchez-vous?");
            //int id = int.Parse(Console.ReadLine());
            //Pokemon pokemon = pokemonRepository.RecupFullPokemon(id);
            //Console.WriteLine($"{pokemon.Id} : {pokemon.Nom} type : {pokemon.Type1.Nom}");

            #endregion

            #region Update

            //Pokemon pokemon = new Pokemon()
            //{
            //    Nom = "Insécateur",
            //    Taille = 10,
            //    Poids = 10,
            //    Type1Id = 2,
            //    Type2Id = null
            //};

            //if(pokemonRepository.ModifierPokemon(2, pokemon))
            //{
            //    Console.WriteLine("Ok");
            //}
            //else
            //{
            //    Console.WriteLine("Ko");
            //}

            #endregion

            #region Delete

            //if (pokemonRepository.SupprimerPokemon(2))
            //{
            //    Console.WriteLine("Ok");
            //}
            //else
            //{
            //    Console.WriteLine("Ko");
            //}

            #endregion

            #region Ajout multiple

            //PokemonType type1 = new PokemonType("Sol");
            //typeRepository.Add(type1);
            //Pokemon pokemon = new Pokemon()
            //{
            //    Id = 2,
            //    Nom = "Taupiqueur",
            //    Taille = 1,
            //    Poids = 1,
            //    Type1Id = type1.Id
            //};
            //pokemonRepository.AjouterPokemon(pokemon);

            #endregion

            
            Pokemon pokemon = new Pokemon()
            {
                Id = 4,
                Nom = "Arcanin",
                Taille = 5,
                Poids = 6,
                Type1Id = 2,
            };

            pokemonRepository.Add(pokemon);

            List<Pokemon> pokemons = pokemonRepository.GetAll().ToList();

            foreach(Pokemon p in  pokemons)
            {
                Console.WriteLine($"{p.Id} : {p.Nom}");
            }

            pokemonRepository.Delete(2);

            pokemons = pokemonRepository.GetAll().ToList();

            foreach (Pokemon p in pokemons)
            {
                Console.WriteLine($"{p.Id} : {p.Nom}");
            }
        }
    }
}