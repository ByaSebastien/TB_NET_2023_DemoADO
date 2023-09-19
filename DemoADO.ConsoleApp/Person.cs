using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoADO.ConsoleApp
{
    internal class Person
    {
        // public string Name;
        // public string FirstName { get; set; }

        private string _firstName;

        public string FirstName
        {
            get { return _firstName; }
            set { _firstName = value; }
        }

        public string LastName { get; set; }

        public DateTime BirthDate { get; set; }

        public string FullName { get { return FirstName + LastName; } }
        public int AgeApprox { get { return DateTime.Now.Year - BirthDate.Year; } }


        public void SePresenter()
        {
            Console.WriteLine($"Bonjour, je m'appelle {FullName} et j'ai +ou- {AgeApprox} ans");
        }
    }
}
