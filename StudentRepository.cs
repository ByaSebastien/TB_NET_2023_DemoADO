using System.Data.SqlClient;

namespace DemoAdo
{
    public class StudentRepository
    {
        private string _connectionString = @"
            server=(localdb)\MSSQLLocalDB;
            database=DemoADO;
            integrated security=true;
            trusted_connection=true
        ";

        public List<Student> GetAll()
        {
            return null;
        }

        public Student GetById(int id)
        {
            return null;
        }

        public int Add(Student s)
        {
            //1 chercher ou créer une connection
            using SqlConnection connection = new SqlConnection(_connectionString);
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
            return (int)command.ExecuteScalar();
        }

        public bool Update(Student s) 
        {
            using SqlConnection connection = new SqlConnection(_connectionString);
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
            return command.ExecuteNonQuery() != 0;
        }

        public bool Remove(int id) 
        { 
            using SqlConnection connection = new SqlConnection(_connectionString);
            using SqlCommand command = connection.CreateCommand();

            command.CommandText = "DELETE FROM Student WHERE Id = @id";
            command.Parameters.AddWithValue("id", id);

            connection.Open();
            return command.ExecuteNonQuery() != 0;
        }
    }
}
