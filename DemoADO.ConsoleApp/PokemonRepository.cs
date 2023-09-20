﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoADO.ConsoleApp
{
    public class PokemonRepository
    {
        // Data Source=(localdb)\MSSQLLocalDB;User ID=sa;Password=********;Connect Timeout=60;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False
        // définir les informations de connection (server, database, user, password, ...)
        // string connectionString = @"server=(localdb)\MSSQLLocalDB;database=Pokemon;uid=sa;pwd=test1234";
        private string _connectionString = @"server=BSTORM\SQLEXPRESS;database=Pokemon;integrated security=true";
        public void AjouterPokemon(Pokemon pokemon)
        {
            string nomTable = "pokemon";
            using (SqlConnection connection = new SqlConnection(_connectionString))
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

        public Pokemon RecupPokemon(int id)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = connection.CreateCommand();
                command.CommandText = "SELECT * FROM POKEMON WHERE Id = @id";
                command.Parameters.AddWithValue("@id", id);
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

                    pokemon = new Pokemon(Resultid, nom, taille, poids, type1, type2);
                }
                else
                {
                    throw new KeyNotFoundException();
                }
                connection.Close();
                return pokemon;
            }
        }

        public List<Pokemon> RecupPokemons()
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = connection.CreateCommand();
                command.CommandText = "SELECT * FROM POKEMON";
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                List<Pokemon> pokemons = new List<Pokemon>();
                while (reader.Read())
                {
                    int Resultid = (int)reader["Id"];
                    string nom = (string)reader["Name"];
                    int taille = (int)reader["Height"];
                    decimal poids = (decimal)reader["Weight"];
                    int type1 = (int)reader["Type1ID"];
                    int? type2 = reader["Type2ID"] == DBNull.Value ? null : (int)reader["Type2ID"];

                    pokemons.Add(new Pokemon(Resultid, nom, taille, poids, type1, type2));
                }
                connection.Close();
                return pokemons;
            }
        }

        public bool ModifierPokemon(int id, Pokemon pokemon)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = connection.CreateCommand();
                command.CommandText = "UPDATE POKEMON " +
                                      "SET Name = @nom," +
                                          "Height = @taille," +
                                          "Weight = @poids," +
                                          "Type1Id = @type1," +
                                          "Type2Id = @type2 " +
                                      "WHERE Id = @id";

                command.Parameters.AddWithValue("@nom", pokemon.Nom);
                command.Parameters.AddWithValue("@taille", pokemon.Taille);
                command.Parameters.AddWithValue("@poids", pokemon.Poids);
                command.Parameters.AddWithValue("@type1", pokemon.Type1Id);
                command.Parameters.AddWithValue("@type2", pokemon.Type2Id is null ? DBNull.Value : pokemon.Type2Id);
                command.Parameters.AddWithValue("@id", id);

                connection.Open();

                int nbLignes = command.ExecuteNonQuery();

                connection.Close();

                //if(nbLignes == 1)
                //{
                //    return true;
                //}
                //else
                //{
                //    return false;
                //}

                return nbLignes == 1;
            }
        }

        public bool SupprimerPokemon(int id)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = connection.CreateCommand();
                command.CommandText = "DELETE Pokemon WHERE Id = @id";
                command.Parameters.AddWithValue("@id", id);
                connection.Open();
                int nbLignes = command.ExecuteNonQuery();
                connection.Close();
                return nbLignes == 1;
            }
        }
    }
}