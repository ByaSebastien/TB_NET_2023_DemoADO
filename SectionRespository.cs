using Dapper;
using System.Data.SqlClient;

namespace DemoAdo
{
    public class SectionRespository
    {
        private readonly SqlConnection connection;

        public SectionRespository(SqlConnection connection) // injection de dépendance par ctor
        {
            this.connection = connection;
        }

        public IEnumerable<Section> GetAll() {
            return connection.Query<Section>("SELECT * FROM Section");
        }

        public Section? GetById(int id)
        {
            return connection.QueryFirstOrDefault<Section>("SELECT * FROM Section WHERE Id = @Id", new { id });
        }

        public int Add(Section s)
        {
            return connection.ExecuteScalar<int>(@"INSERT INTO Section OUTPUT INSERTED.Id VALUES(@Name)", s);
        }

        public bool Update(Section s)
        {
            return connection.Execute(@"UPDATE Section SET Name = @Name WHERE Id = @Id", s) != 0;
        }

        public bool Delete(int id)
        {
            return connection.Execute("DELETE FROM Section WHERE Id = @Id", new { id }) != 0;
        }
    }
}
