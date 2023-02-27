using DemoAdo;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;

ConfigurationBuilder builder = new();
builder.AddJsonFile("appsettings.json");
IConfiguration configuration = builder.Build();

SqlConnection connection = new(configuration.GetConnectionString("Main"));

StudentRepository studentRepo = new(connection);
SectionRespository sectionRepo = new(connection);

//Student? s  = studentRepo.GetById(3);

//Console.WriteLine(s.LastName);

foreach (var item in studentRepo.GetAllWithSection().ToList())
{
    Console.WriteLine(item.LastName);
    Console.WriteLine(item.Section?.Name);
}

//studentRepo.Add(new Student
//{
//    LastName = "Person",
//    FirstName = "Mike",
//    Gender = 1,
//    IsGraduated = true
//});


// sectionRepo.Add(new Section { Name = ".Net" });
// Section? s  = sectionRepo.GetById(1);

// Console.WriteLine(s.Name);


