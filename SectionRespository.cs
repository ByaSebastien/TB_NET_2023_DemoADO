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
    }
}
