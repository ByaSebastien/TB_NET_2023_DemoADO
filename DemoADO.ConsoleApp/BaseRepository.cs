using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoADO.ConsoleApp
{
    public abstract class BaseRepository<TKey, TEntity> : IBaseRepository<TKey, TEntity> where TEntity : class
    {
        // Data Source=(localdb)\MSSQLLocalDB;User ID=sa;Password=********;Connect Timeout=60;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False
        // définir les informations de connection (server, database, user, password, ...)
        // string connectionString = @"server=(localdb)\MSSQLLocalDB;database=Pokemon;uid=sa;pwd=test1234";
        protected string _connectionString = @"server=BSTORM\SQLEXPRESS;database=Pokemon;integrated security=true";
        protected string _tableName;
        protected string _columnIdName;

        public BaseRepository(string tableName,string columnIdName)
        {
            _tableName = tableName;
            _columnIdName = columnIdName;
        }

        protected abstract TEntity Convert(SqlDataReader reader);

        public abstract void Add(TEntity entity);

        public TEntity GetOne(TKey id)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = connection.CreateCommand();
                command.CommandText = $"SELECT * FROM {_tableName} WHERE {_columnIdName} = @id";
                command.Parameters.AddWithValue("@id", id);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                TEntity entity;
                if (reader.Read())
                {
                    entity = Convert(reader);
                }
                else
                {
                    throw new KeyNotFoundException();
                }
                connection.Close();
                return entity;
            }
        }

        public IEnumerable<TEntity> GetAll()
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = connection.CreateCommand();
                command.CommandText = $"SELECT * FROM {_tableName}";
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                List<TEntity> entities = new List<TEntity>();
                while (reader.Read())
                {
                    entities.Add(Convert(reader));
                }
                connection.Close();
                return entities;
            }
        }

        public abstract bool Update(TKey id, TEntity entity);

        public bool Delete(TKey id)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = connection.CreateCommand();
                command.CommandText = $"DELETE {_tableName} WHERE {_columnIdName} = @id";
                command.Parameters.AddWithValue("@id", id);
                connection.Open();
                int nbLignes = command.ExecuteNonQuery();
                connection.Close();
                return nbLignes == 1;
            }
        }
    }
}
