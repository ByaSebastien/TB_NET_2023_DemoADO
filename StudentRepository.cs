using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            command.Parameters.AddWithValue("BirthDate", s.BirthDate);
            command.Parameters.AddWithValue("Gender", s.Gender);
            command.Parameters.AddWithValue("IsGraduated", s.IsGraduated);
            //4 ouvrir la connection
            connection.Open();
            //5 exécuter la commande
            return (int)command.ExecuteScalar();
        }

        public void Update(Student s) 
        { 
        
        }

        public void Remove(int id) 
        { 

        }
    }
}
