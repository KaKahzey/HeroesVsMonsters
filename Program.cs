using HeroesVsMonsters.Models;
using System.Collections.Generic;

namespace HeroesVsMonsters
{
    internal class Program
    {
        static void Main(string[] args)
        {
            bool jouer = true, humain = false, nain = false;
            int[] position = new int[2];
            int[] positionEnnemy = {0, 0};
            

            while (jouer)
            {
                List<Monstre> monstres = new List<Monstre>();
                Plateau plateau = new Plateau();
                
                Console.WriteLine("Bienvenue sur Heores Versus Monsters!");
                Personnage.DemanderPersonnages(ref humain, ref nain);
                Hero hero = Personnage.CreerPersonnages(humain, nain, monstres);
                plateau.PlacerPersonnages(hero, monstres, plateau.Terrain);

                Personnage.Partie(hero, monstres, plateau, positionEnnemy, position);

                plateau.Rejouer(ref jouer);

            }
        }
    }
}