using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoADO.ConsoleApp
{
    public class Pokemon
    {
        public Pokemon() { }
        public Pokemon(int id, string nom, int taille, decimal poids, int type1Id, int? type2Id)
        {
            Id = id;
            Nom = nom;
            Taille = taille;
            Poids = poids;
            Type1Id = type1Id;
            Type2Id = type2Id;
        }

        public int Id { get; set; }
        public string Nom { get; set; }
        public int Taille { get; set; }
        public decimal Poids { get; set; }
        public int Type1Id { get; set; }
        public int? Type2Id { get; set;}
    }
}

//command.Parameters.AddWithValue("@id", id);
//command.Parameters.AddWithValue("@nom", nom);
//command.Parameters.AddWithValue("@taille", taille);
//command.Parameters.AddWithValue("@poids", poids);
//command.Parameters.AddWithValue("@type1", type1);
