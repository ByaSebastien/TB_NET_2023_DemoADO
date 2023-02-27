namespace DemoAdo
{
    public class Student
    {
        public int Id { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public DateTime? BirthDate { get; set; }
        public int Gender { get; set; } // 1 fille 2 garcon
        public bool IsGraduated { get; set; }

    }
}
