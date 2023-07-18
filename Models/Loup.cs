using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HeroesVsMonsters.Interfaces;

namespace HeroesVsMonsters.Models
{
    public class Loup : Monstre, ICuir
    {
        public Loup()
        {
            cuir = GenererCuir();
        }


        public int GenererCuir()
        {
            Random rdm = new Random();
            int quantite = rdm.Next(1, 5);
            return quantite;
        }

        public override string ToString()
        {
            return "Le loup";
        }
    }
}
