namespace DemoADO.ConsoleApp
{
    internal class Program
    {
        //delegate void MonTypeDeFonction();
        //delegate void MonAutreTypeDeFonction(int nb);
        //delegate bool Condition(int nb);
        static void Main(params string[] args)
        {
            Person p = new Person();
            p.FirstName = "Khun";
            p.LastName = "Ly";
            p.BirthDate = new DateTime(1982, 5, 6);
            p.SePresenter();


            int number = 42;

            Action<int> maFonction = (int nb) => { Console.WriteLine(nb); };
            Action<int> maFonction2 = MyFunction;

            maFonction2(42);
            MyFunction(42);

            int? nb = ChercherPremier(
                new int[] { 1, 3, 7, 43, 9, 6, 42 }, 
                nb => nb > 14 && nb % 2 == 0
            );
            Console.WriteLine(nb);

        }

        static void MyFunction(int nb)
        {
            Console.WriteLine(nb);
        }

        static bool EstPair(int nb)
        {
            return nb % 2 == 0;
        }

        static bool EstImpair(int nb)
        {
            return nb % 2 == 1;
        }

        static int? ChercherPremierPair(int[] nombres)
        {
            foreach (int n in nombres)
            {
                if(EstPair(n))
                {
                    return n;
                }
            }
            return null;
        }

        static int? ChercherPremierImpair(int[] nombres)
        {
            foreach (int n in nombres)
            {
                if (EstImpair(n))
                {
                    return n;
                }
            }
            return null;
        }

        static int? ChercherPremier(int[] nombres, Func<int, bool> condition)
        {
            foreach (int n in nombres)
            {
                if (condition(n))
                {
                    return n;
                }
            }
            return null;
        }
    }
}