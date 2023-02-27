using Dapper;
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
            return connection.Query<Student>("SELECT * FROM Student");

            //using SqlCommand command = connection.CreateCommand();

            //command.CommandText = "SELECT * FROM Student";

            //connection.Open();

            //using SqlDataReader reader = command.ExecuteReader();

            //while (reader.Read())
            //{
            //    yield return new Student
            //    {
            //        Id = (int)reader["Id"],
            //        LastName = (string)reader["LastName"],
            //        FirstName = (string)reader["FirstName"],
            //        Gender = (int)reader["Gender"],
            //        IsGraduated = (bool)reader["IsGraduated"],
            //        BirthDate = reader["BirthDate"] as DateTime?,
            //        SectionId = reader["SectionId"] as int?,
            //    };
            //}
            //connection.Close();
        }

        public IEnumerable<Student> GetAllWithSection()
        {
            return connection.Query<Student, Section, Student>(@"
                SELECT * FROM Student st 
                LEFT JOIN Section se ON se.Id = st.SectionId
            ", (st, se) => {
                st.Section = se;
                return st;
            }, "SectionId");
        }

        public Student? GetById(int id)
        {
            return connection.QueryFirstOrDefault<Student>("SELECT * FROM Student WHERE Id = @Id", new { Id = id });

            //using SqlCommand command = connection.CreateCommand();

            //command.CommandText = "SELECT * FROM Student WHERE Id = @Id";
            //command.Parameters.AddWithValue("Id", id);

            //connection.Open();
            //using SqlDataReader reader = command.ExecuteReader();
            //if(reader.Read())
            //{
            //    return new Student
            //    {
            //        Id = (int)reader["Id"],
            //        LastName = (string)reader["LastName"],
            //        FirstName = (string)reader["FirstName"],
            //        Gender = (int)reader["Gender"],
            //        IsGraduated = (bool)reader["IsGraduated"],
            //        BirthDate = reader["BirthDate"] as DateTime?,
            //        SectionId = reader["SectionId"] as int?,
            //    };
            //}
            //connection.Close();
            //return null;
        }

        public int Add(Student s)
        {
            return connection.ExecuteScalar<int>(@"
                INSERT INTO Student 
                (LastName, FirstName, BirthDate, Gender, IsGraduated, SectionId)
                OUTPUT INSERTED.Id VALUES
                (@LastName, @FirstName, @BirthDate, @Gender, @IsGraduated, @SectionId)
            ", s);
            ////2 creer une commande
            //using SqlCommand command = connection.CreateCommand();
            ////3 créer la requète
            //command.CommandText = @"
            //    INSERT INTO Student 
            //    (LastName, FirstName, BirthDate, Gender, IsGraduated, SectionId)
            //    OUTPUT INSERTED.Id VALUES
            //    (@LastName, @FirstName, @BirthDate, @Gender, @IsGraduated, @SectionId)
            //";
            //command.Parameters.AddWithValue("LastName", s.LastName);
            //command.Parameters.AddWithValue("FirstName", s.FirstName);
            //command.Parameters.AddWithValue("BirthDate", s.BirthDate ?? (object)DBNull.Value);
            //command.Parameters.AddWithValue("Gender", s.Gender);
            //command.Parameters.AddWithValue("IsGraduated", s.IsGraduated);
            //command.Parameters.AddWithValue("SectionId", s.SectionId ?? (object)DBNull.Value);
            ////4 ouvrir la connection
            //connection.Open();
            ////5 exécuter la commande
            //int id = (int)command.ExecuteScalar();
            //connection.Close();
            //return id;
        }

        public bool Update(Student s) 
        {
            return connection.Execute(@"
                UPDATE Student
                SET
                LastName = @LastName,
                FirstName = @FirstName,
                BirthDate = @BirthDate,
                Gender = @Gender,
                IsGraduated = @IsGraduated,
                Section = @SectionId
                WHERE Id = @Id
            ", s) != 0;
            //using SqlCommand command = connection.CreateCommand();

            //command.CommandText = @"
            //    UPDATE Student
            //    SET
            //    LastName = @LastName,
            //    FirstName = @FirstName,
            //    BirthDate = @BirthDate,
            //    Gender = @Gender,
            //    IsGraduated = @IsGraduated,
            //    Section = @SectionId
            //    WHERE Id = @Id
            //";

            //command.Parameters.AddWithValue("LastName", s.LastName);
            //command.Parameters.AddWithValue("FirstName", s.FirstName);
            //command.Parameters.AddWithValue("BirthDate", s.BirthDate ?? (object)DBNull.Value);
            //command.Parameters.AddWithValue("Gender", s.Gender);
            //command.Parameters.AddWithValue("IsGraduated", s.IsGraduated);
            //command.Parameters.AddWithValue("SectionId", s.SectionId ?? (object)DBNull.Value);
            //command.Parameters.AddWithValue("Id", s.Id);

            //connection.Open();
            //bool result = command.ExecuteNonQuery() != 0;
            //connection.Close();
            //return result;
        }

        public bool Remove(int id) 
        {
            return connection.Execute(@"DELETE FROM Student WHERE Id = @id", new { Id = id }) != 0;
            //using SqlCommand command = connection.CreateCommand();

            //command.CommandText = "DELETE FROM Student WHERE Id = @id";
            //command.Parameters.AddWithValue("id", id);

            //connection.Open();
            //bool result = command.ExecuteNonQuery() != 0;
            //connection.Close();
            //return result;
        }
    }
}
