using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeroesVsMonsters.Models
{
    public class Nain : Hero
    {
        public Nain() : base()
        {
            endurance += 2;
        }

        public override string ToString()
        {
            return "Le nain";
        }
    }
}
