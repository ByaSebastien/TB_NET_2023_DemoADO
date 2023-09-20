using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoADO.ConsoleApp
{
    public class TypeRepository
    {
        private string _connectionString = @"server=BSTORM\SQLEXPRESS;database=Pokemon;integrated security=true";

        public void Add(PokemonType type)
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
    }
}
