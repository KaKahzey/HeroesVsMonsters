using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HeroesVsMonsters.Interfaces;

namespace HeroesVsMonsters.Models
{
    public class Dragonnet : Monstre, ICuir, IOr
    {
        public Dragonnet()
        {
            endurance += 1;
            or = GenererOr();
            cuir = GenererCuir();
        }


        public int GenererOr()
        {
            Random rdm = new Random();
            int quantite = rdm.Next(10, 50);
            return quantite;
        }

        public int GenererCuir()
        {
            Random rdm = new Random();
            int quantite = rdm.Next(1, 5);
            return quantite;
        }

        public override string ToString()
        {
            return "Le dragonnet";
        }
    }
}
