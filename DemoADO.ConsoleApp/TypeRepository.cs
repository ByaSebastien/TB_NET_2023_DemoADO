using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoADO.ConsoleApp
{
    public class TypeRepository : BaseRepository<int,PokemonType>
    {
        public TypeRepository() : base("Type", "Id") { }

        protected override PokemonType Convert(SqlDataReader reader)
        {
            return new PokemonType()
            {
                Id = (int)reader["Id"],
                Nom = (string)reader["Name"]
            };
        }

        public override void Add(PokemonType type)
        {
            using(SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = connection.CreateCommand();
                cmd.CommandText = "INSERT INTO Type(Name)" +
                                  "OUTPUT INSERTED.ID " +
                                  "VALUES (@nom)";
                cmd.Parameters.AddWithValue("@nom",type.Nom);
                connection.Open();
                int? typeId  = (int)cmd.ExecuteScalar();
                if(typeId is null)
                {
                    throw new Exception("Echec");
                }
                else
                {
                    type.Id = typeId ?? 0;
                }
                connection.Close();
            }
        }

        public override bool Update(int id, PokemonType type)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = connection.CreateCommand();
                cmd.CommandText = "UPDATE Type " +
                                  "SET Name = @name " +
                                  "WHERE Id = @id";
                cmd.Parameters.AddWithValue("@nom", type.Nom);
                cmd.Parameters.AddWithValue("@id", id);
                connection.Open();
                
                int nbLignes = cmd.ExecuteNonQuery();

                connection.Close();

                return nbLignes == 1;
            }
        }
    }
}
