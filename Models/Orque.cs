using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HeroesVsMonsters.Interfaces;

namespace HeroesVsMonsters.Models
{
    public class Orque : Monstre, IOr
    {
        public Orque()
        {
            force += 1;
            or = GenererOr();
        }

        public int GenererOr()
        {
            Random rdm = new Random();
            int quantite = rdm.Next(10, 50);
            return quantite;
        }

        public override string ToString()
        {
            return "L'orque";
        }
    }
    
}
