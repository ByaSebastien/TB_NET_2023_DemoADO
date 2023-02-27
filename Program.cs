using DemoAdo;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;

ConfigurationBuilder builder = new ConfigurationBuilder();
builder.AddJsonFile("appsettings.json");
IConfiguration configuration = builder.Build();

SqlConnection connection = new SqlConnection(configuration.GetConnectionString("Main"));

StudentRepository repository = new(connection);
SectionRespository repository2 = new(connection);

//repository.Add(new Student
//{
//    LastName = "Morre",
//    FirstName = "Thierry",
//    Gender = 2,
//    BirthDate = null,
//    IsGraduated = true,
//});
//if (repository.Remove(6))
//{
//    Console.WriteLine("Suppression OK");
//}

//Student? student = repository.GetById(3);
//Console.WriteLine(student.FirstName);
//Console.WriteLine(student.LastName);
//Console.WriteLine(student.Gender);
//Console.WriteLine(student.BirthDate);
//Console.WriteLine(student.IsGraduated);

foreach (Student student in repository.GetAll())
{
    Console.WriteLine(student.FirstName);
    Console.WriteLine(student.LastName);
    Console.WriteLine(student.Gender);
    Console.WriteLine(student.BirthDate);
    Console.WriteLine(student.IsGraduated);
}
