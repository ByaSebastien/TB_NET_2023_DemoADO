using DemoAdo;

StudentRepository repository = new();

repository.Update(new Student
{
    Id = 3,
    LastName = "Herssens",
    FirstName = "Caroline",
    Gender = 1,
    BirthDate = new DateTime(1983, 02, 05),
    IsGraduated = true,
});
//if(repository.Remove(2))
//{
//    Console.WriteLine("Suppression OK");
//}