using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Intrinsics.Arm;
using System.Text;
using System.Threading.Tasks;

namespace HeroesVsMonsters.Models
{
    public class Plateau : Personnage
    {
        public Plateau()
        {
            Terrain = new Personnage?[15,15];
            Terrain = RemplirPlateau(Terrain);
        }

        public Personnage?[,] Terrain { get; set; }

        public Personnage?[,] RemplirPlateau(Personnage?[,] plateau)
        {
            int i, j;
            for (i = 0; i < plateau.GetLength(0); i++)
            {
                for (j = 0; j < plateau.GetLength(1); j++)
                {
                    plateau[i, j] = null;
                }
            }
            return plateau;
        }

        public void AfficherPlateau(int[] positionEnnemy)
        {
            string displayedMatrix = "";
            int i, j;
            for (i = 0; i < Terrain.GetLength(0); i++)
            {
                for (j = 0; j < Terrain.GetLength(1); j++)
                {
                    if (Terrain[i, j] is Humain)
                    {
                        displayedMatrix += "[H]";
                    }
                    else if (Terrain[i, j] is Nain)
                    {
                        displayedMatrix += "[N]";
                    }
                    else if(Terrain[i, j] is Monstre && positionEnnemy[0] == i && positionEnnemy[1] == j)
                    {
                        if(Terrain[i, j] is Loup)
                        {
                            displayedMatrix += "[L]";
                        }
                        else if (Terrain[i, j] is Orque)
                        {
                            displayedMatrix += "[O]";
                        }
                        else
                        {
                            displayedMatrix += "[D]";
                        }
                    }
                    else
                    {
                        displayedMatrix += "[ ]";
                    }
                }
                displayedMatrix += "\n";
            }
            displayedMatrix += "Shorewood, forêt enchantée du pays de Stormwall";
            Console.WriteLine(displayedMatrix);
        }
        public void PlacerPersonnages(Hero hero, List<Monstre> monstres, Personnage[,] terrain)
        {
            Random rdm = new Random();
            int rdmRow, rdmColumn;
            bool positionOccupee = true;

                positionOccupee = true;
                while (positionOccupee)
                {
                    rdmRow = rdm.Next(0, 6);
                    rdmColumn = rdm.Next(0, 15);
                    if (terrain[rdmRow, rdmColumn] is null)
                    {
                        positionOccupee = false;
                        terrain[rdmRow, rdmColumn] = hero;
                    }
                }
            foreach(Monstre m in monstres)
            {
                positionOccupee = true;
                while (positionOccupee)
                {
                    rdmRow = rdm.Next(7, 15);
                    rdmColumn = rdm.Next(0, 15);
                    if (terrain[rdmRow, rdmColumn] is null)
                    {
                        positionOccupee = false;
                        terrain[rdmRow, rdmColumn] = m;
                    }
                }
            }
        }

        public static int[] TrouverPersonnage(Hero hero, Personnage[,] terrain, int[] position)
        {
            int i, j;
            for (i = 0; i < terrain.GetLength(0); i++)
            {
                for (j = 0; j < terrain.GetLength(1); j++)
                {
                    if (terrain[i, j] == hero)
                    {
                        position[0] = i;
                        position[1] = j;
                    }
                }
            }
            return position;
        }
        public void DeplacerPersonnage(Hero hero, List<Monstre> monstres, Personnage[,] terrain, int[] positionEnnemy, int[] position, ref bool enCours)
        {
            bool combatTrouve = false;
            int i, j, row, column;
            bool caillouSurLaRoute = false;
            Random rdm = new Random();
            int chance;
            
            while (!combatTrouve)
            {
                chance = rdm.Next(1, 20);
                Console.WriteLine("Vers quelle direction souhaitez-vous aller? z/q/s/d");
                string direction = Console.ReadLine();
                if(direction == "z")
                {
                    DeplacerHaut(hero, terrain, position);
                    Console.Clear();
                    AfficherPlateau(positionEnnemy);
                }
                else if(direction == "q")
                {
                    DeplacerGauche(hero, terrain, position);
                    Console.Clear();
                    AfficherPlateau(positionEnnemy);
                }
                else if(direction == "s")
                {
                    DeplacerBas(hero, terrain, position);
                    Console.Clear();
                    AfficherPlateau(positionEnnemy);
                }
                else if(direction == "d")
                {
                    DeplacerDroite(hero, terrain, position);
                    Console.Clear();
                    AfficherPlateau(positionEnnemy);
                }
                if(chance < 5)
                {
                    Console.WriteLine("Il y a un caillou sur la route, aie aie aie la cheville! \n -2 pv");
                    hero.PV -= 2;
                    if(hero.PV <= 0)
                    {
                        enCours = false;
                        break;
                    }
                }
                TrouverEnnemy(hero, terrain, positionEnnemy, position);
                if (positionEnnemy[0] != 0 || positionEnnemy[1] != 0)
                {
                    combatTrouve = true;

                }
            }

        }
        public void DeplacerHaut(Hero hero, Personnage[,] terrain, int[] position)
        {
            position = TrouverPersonnage(hero, terrain, position);
            if (position[0] != 0 && terrain[position[0] - 1, position[1]] == null)
            {
                terrain[position[0] - 1, position[1]] = terrain[position[0], position[1]];
                terrain[position[0], position[1]] = null;
            }
            else
            {
                Console.WriteLine("UN MUR");
            }
        }

        public void DeplacerGauche(Hero hero, Personnage[,] terrain, int[] position)
        {
            position = TrouverPersonnage(hero, terrain, position);
            if (position[1] != 0 && terrain[position[0], position[1] - 1] == null)
            {
                terrain[position[0], position[1] - 1] = terrain[position[0], position[1]];
                terrain[position[0], position[1]] = null;
            }
            else
            {
                Console.WriteLine("UN MUR");
            }
        }

        public void DeplacerBas(Hero hero, Personnage[,] terrain, int[] position)
        {
            position = TrouverPersonnage(hero, terrain, position);
            if (position[0] != terrain.GetLength(0) - 1 && terrain[position[0] + 1, position[1]] == null)
            {
                terrain[position[0] + 1, position[1]] = terrain[position[0], position[1]];
                terrain[position[0], position[1]] = null;
            }
            else
            {
                Console.WriteLine("UN MUR");
            }
        }

        public void DeplacerDroite(Hero hero, Personnage[,] terrain, int[] position)
        {
            position = TrouverPersonnage(hero, terrain, position);
            if (position[1] != terrain.GetLength(1) - 1 && terrain[position[0], position[1] + 1] == null)
            {
                terrain[position[0], position[1] + 1] = terrain[position[0], position[1]];
                terrain[position[0], position[1]] = null;
            }
            else
            {
                Console.WriteLine("UN MUR");
            }
        }

        public void TrouverEnnemy(Hero hero, Personnage[,] terrain, int[] positionEnnemy, int[] position)
        {
            position = TrouverPersonnage(hero, terrain, position);
            positionEnnemy[0] = 0;
            positionEnnemy[1] = 0;
            bool verifG, verifH, verifB, verifD;
            verifG = position[1] == 0 ? false : true;
            verifH = position[0] == 0 ? false : true;
            verifB = position[0] == (terrain.GetLength(0) - 1) ? false : true;
            verifD = position[1] == (terrain.GetLength(1) - 1) ? false : true;
            

            if (verifH && terrain[position[0] - 1, position[1]] is Monstre)
            {
                positionEnnemy[0] = position[0] - 1;
                positionEnnemy[1] = position[1];
                Console.Clear();
                AfficherPlateau(positionEnnemy);
                Console.WriteLine("Un monstre est au dessus du héros! FIGHTEUU");
                Console.ReadLine();
            }
            else if (verifG && terrain[position[0], position[1] - 1] is Monstre)
            {
                positionEnnemy[0] = position[0];
                positionEnnemy[1] = position[1] - 1;
                Console.Clear();
                AfficherPlateau(positionEnnemy);
                Console.WriteLine("Un monstre est à gauche du héros! FIGHTEUU");
                Console.ReadLine();
            }
            else if (verifB && terrain[position[0] + 1, position[1]] is Monstre)
            {
                positionEnnemy[0] = position[0] + 1;
                positionEnnemy[1] = position[1];
                Console.Clear();
                AfficherPlateau(positionEnnemy);
                Console.WriteLine("Un monstre est en dessous du héros! FIGHTEUU");
                Console.ReadLine();
            }
            else if (verifD && terrain[position[0], position[1] + 1] is Monstre)
            {
                positionEnnemy[0] = position[0];
                positionEnnemy[1] = position[1] + 1;
                Console.Clear();
                AfficherPlateau(positionEnnemy);
                Console.WriteLine("Un monstre est à droite du héros! FIGHTEUU");
                Console.ReadLine();
            }

        }

        public void Rejouer(ref bool jouer)
        {
            Console.WriteLine("Voulez-vous rejouer? y/n");
            string entree = Console.ReadLine();
            if(entree == "n") 
            {
                jouer = false;
            }
            else
            {
                Console.WriteLine("Dans le doute on zy go agane");
            }
            
        }
    }
}
