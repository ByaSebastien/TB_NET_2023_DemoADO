using DemoAdo;

StudentRepository repository = new();

repository.Add(new Student
{
    LastName = "LY",
    FirstName = "Khun",
    Gender = 2,
    BirthDate = DateTime.Now,
    IsGraduated = false,
});