using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeroesVsMonsters.Models
{
    internal class Des
    {

        

        public int Min { 
            get
            {
                return 1;
            }
        }

        public int Max { 
            get
            {
                return 7;
            }
        }
        public int Lancer4D6()
        {
            Random rdm = new Random();
            int resultat = 0;
            List<int> resultats = new List<int>();
            int petit = int.MaxValue;
            int compte = -1;

            for (int i = 1; i < 5; i++)
            {
                resultats.Add(rdm.Next(Min, Max));
            }
            foreach (int e in resultats)
            {
                if (e < petit)
                {
                    petit = e;
                }
            }

            foreach (int e in resultats)
            {
                if (e != petit)
                {
                    resultat += e;
                }
                else
                {
                    compte += 1;
                }
            }
            if (compte > 0 && compte < 4)
            {
                resultat += compte * petit;
            }
            return resultat;

        }
    }
}
