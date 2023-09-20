using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoADO.ConsoleApp
{
    public class RappelGenerique<T> where T : class
    {
        public void Afficher(T entity)
        {
            Console.WriteLine(entity);
        }
    }
}
