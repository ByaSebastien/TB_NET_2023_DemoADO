using System.Data;
using System.Data.SqlClient;

namespace DemoAdo
{
    public class StudentRepository
    {
        private readonly SqlConnection connection;

        public StudentRepository(SqlConnection connection) // injection de dépendance par ctor
        {
            this.connection = connection;
        }

        public IEnumerable<Student> GetAll()
        {
            using SqlCommand command = connection.CreateCommand();

            command.CommandText = "SELECT * FROM Student";

            connection.Open();
            using SqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                yield return new Student
                {
                    Id = (int)reader["Id"],
                    LastName = (string)reader["LastName"],
                    FirstName = (string)reader["FirstName"],
                    Gender = (int)reader["Gender"],
                    IsGraduated = (bool)reader["IsGraduated"],
                    BirthDate = reader["BirthDate"] as DateTime?
                };
            }
            connection.Close();
        }

        public Student? GetById(int id)
        {
            using SqlCommand command = connection.CreateCommand();

            command.CommandText = "SELECT * FROM Student WHERE Id = @Id";
            command.Parameters.AddWithValue("Id", id);

            connection.Open();
            using SqlDataReader reader = command.ExecuteReader();
            if(reader.Read())
            {
                return new Student
                {
                    Id = (int)reader["Id"],
                    LastName = (string)reader["LastName"],
                    FirstName = (string)reader["FirstName"],
                    Gender = (int)reader["Gender"],
                    IsGraduated = (bool)reader["IsGraduated"],
                    BirthDate = reader["BirthDate"] as DateTime?
                };
            }
            connection.Close();
            return null;
        }

        public int Add(Student s)
        {
            //2 creer une commande
            using SqlCommand command = connection.CreateCommand();
            //3 créer la requète
            command.CommandText = @"
                INSERT INTO Student 
                (LastName, FirstName, BirthDate, Gender, IsGraduated)
                OUTPUT INSERTED.Id VALUES
                (@LastName, @FirstName, @BirthDate, @Gender, @IsGraduated)
            ";
            command.Parameters.AddWithValue("LastName", s.LastName);
            command.Parameters.AddWithValue("FirstName", s.FirstName);
            command.Parameters.AddWithValue("BirthDate", s.BirthDate ?? (object)DBNull.Value);
            command.Parameters.AddWithValue("Gender", s.Gender);
            command.Parameters.AddWithValue("IsGraduated", s.IsGraduated);
            //4 ouvrir la connection
            connection.Open();
            //5 exécuter la commande
            int id = (int)command.ExecuteScalar();
            connection.Close();
            return id;
        }

        public bool Update(Student s) 
        {
            using SqlCommand command = connection.CreateCommand();

            command.CommandText = @"
                UPDATE Student
                SET
                LastName = @LastName,
                FirstName = @FirstName,
                BirthDate = @BirthDate,
                Gender = @Gender,
                IsGraduated = @IsGraduated
                WHERE Id = @Id
            ";

            command.Parameters.AddWithValue("LastName", s.LastName);
            command.Parameters.AddWithValue("FirstName", s.FirstName);
            command.Parameters.AddWithValue("BirthDate", s.BirthDate ?? (object)DBNull.Value);
            command.Parameters.AddWithValue("Gender", s.Gender);
            command.Parameters.AddWithValue("IsGraduated", s.IsGraduated);
            command.Parameters.AddWithValue("Id", s.Id);

            connection.Open();
            bool result = command.ExecuteNonQuery() != 0;
            connection.Close();
            return result;
        }

        public bool Remove(int id) 
        { 
            using SqlCommand command = connection.CreateCommand();

            command.CommandText = "DELETE FROM Student WHERE Id = @id";
            command.Parameters.AddWithValue("id", id);

            connection.Open();
            bool result = command.ExecuteNonQuery() != 0;
            connection.Close();
            return result;
        }
    }
}
