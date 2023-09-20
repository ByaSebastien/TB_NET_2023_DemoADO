using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoADO.ConsoleApp
{
    public class PokemonType
    {
        public PokemonType()
        {

        }

        public PokemonType(string nom)
        {
            Nom = nom;
        }

        public PokemonType(int? id, string nom) : this(nom)
        {
            Id = id ?? 0;
        }

        public int Id { get; set; }
        public string Nom { get; set; }


    }
}
