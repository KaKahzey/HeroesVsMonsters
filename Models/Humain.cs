using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeroesVsMonsters.Models
{
    public class Humain : Hero
    {
        public Humain() : base()
        {
            force += 1;
            endurance += 1;
        }

        public override string ToString()
        {
            return "L'humain";
        }
    }
}
