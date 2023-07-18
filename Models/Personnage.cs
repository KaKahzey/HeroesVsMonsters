using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Runtime.Intrinsics.Arm;
using System.Text;
using System.Threading.Tasks;

namespace HeroesVsMonsters.Models
{
    public class Personnage
    {
        Des de = new Des();

        public Personnage()
        {
            endurance = de.Lancer4D6();
            force = de.Lancer4D6();
            pVMax = GenererPV();
            pV = GenererPV();
        }

        protected int endurance;
        protected int force;
        protected int pV;
        protected int pVMax;
        protected int or;
        protected int cuir;

        public int Or
        {
            get { return or; }
        }
        public int Cuir
        {
            get { return cuir; }
        }
        public int Endurance
        {
            get { return endurance; }
        }

        public int Force
        {
            get { return force; }
        }

        public int PV
        {
            get { return pV; }
            set
            {
                if (value <= pVMax)
                {
                    pV = value;
                }
                else
                {
                    pV = pVMax;
                }
            }
        }

        public int GenererPV()
        {
            if (endurance < 5)
            {
                return endurance - 1;
            }
            else if (endurance < 10)
            {
                return endurance;
            }
            else if (endurance < 15)
            {
                return endurance + 1;
            }
            else
            {
                return endurance + 2;
            }
        }

        public int Frappe(Personnage ennemy)
        {
            Random rdm = new Random();
            int degats = rdm.Next(1, 4);
            bool coupCritique = false;
            if (Force < 5)
            {
                degats += - 1;
            }
            else if (Force < 10)
            {
                degats += 0;
            }
            else if (Force < 15)
            {
                degats += 1;
            }
            else
            {
                degats += 2;
            }
            if(rdm.Next(1, 100) < 20)
            {
                coupCritique = true;
                degats *= 100 / 50;
            }
            ennemy.PV -= degats;
            if(ennemy.PV <= 0 && ennemy is Monstre)
            {
                Console.WriteLine($"{ennemy.ToString()} est mort!");
                if ((ennemy.GetType()).ToString() == "Le loup")
                {
                    this.cuir += ennemy.Cuir;
                    Console.WriteLine($"{this.ToString()} a tué {ennemy.ToString()}. \n + {ennemy.Cuir} peaux");
                }
                else if ((ennemy.GetType()).ToString() == "L'orque")
                {
                    this.or += ennemy.Or;
                    Console.WriteLine($"{this.ToString()} a tué {ennemy.ToString()}. \n + {ennemy.Or} pièces d'or");
                }
                else
                {
                    this.cuir += ennemy.Cuir;
                    this.or += ennemy.Or;
                    Console.WriteLine($"{this.ToString()} a tué {ennemy.ToString()}. \n + {ennemy.Cuir} peaux \n + {ennemy.Or} pièces d'or");
                }
                PV = pVMax;
                return 1;
            }
            else if(ennemy.PV <= 0)
            {
                Console.WriteLine($"{this.ToString()} a tué {ennemy.ToString()}.");
                Console.WriteLine("Game Over");
                return 2;
            }
            if(this is Hero)
            {
                Console.ForegroundColor = ConsoleColor.Green;
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
            }
            if (coupCritique)
            {
                Console.WriteLine("Whoawzaa coup critique!");
            }
                Console.WriteLine($"{this.ToString()} frappe {ennemy.ToString()} pour {degats} points de dégats, {ennemy.PV} PV restant");
            Console.ForegroundColor = ConsoleColor.White;
            return 0;

        }

        public static void DemanderPersonnages(ref bool humain, ref bool nain)
        {

            Console.WriteLine("Souhaitez-vous être un humain ou un nain? h/n");
            do
            {
                string entree = Console.ReadLine();
                if (entree == "h")
                {
                    humain = true;
                    Console.WriteLine("Je suis humain :)");
                    Console.ReadLine();
                    break;
                }
                else if (entree== "n")
                {
                    nain = true;
                    Console.WriteLine("PAR MA BARBE!");
                    Console.ReadLine();
                    break;
                }
            }
            while (true);
        }
        public static Hero CreerPersonnages(bool humain, bool nain, List<Monstre> monstres)
        {
            int i;
            Random rdm = new Random();
            int nbre = rdm.Next(1, 10);
            
            for (i = 0; i <= nbre; i++)
            {
                if (rdm.Next(1, 4) == 1)
                {
                    monstres.Add(new Loup());
                }
                else if (rdm.Next(1, 4) == 2)
                {
                    monstres.Add(new Orque());
                }
                else
                {
                    monstres.Add(new Dragonnet());
                }
            }
            if (humain)
            {
                Humain LeGarsDansLOTR = new Humain();
                return LeGarsDansLOTR;
            }
                Nain PetitRoux = new Nain();
                return PetitRoux;
        }

        public static void Fight(Hero hero,List<Monstre> monstres, Plateau plateau, Personnage[,] terrain, int[] positionEnnemy, int[] position, ref bool enCours)
        {
            Monstre monstre = (Monstre)terrain[positionEnnemy[0], positionEnnemy[1]];
            int monstreDansListe = monstres.IndexOf(monstre);
            position = Plateau.TrouverPersonnage(hero, terrain, position);
            int resultat = 0;

            while (resultat == 0)
            {
                resultat = hero.Frappe(monstres[monstreDansListe]);
                if (resultat == 1)
                {
                    terrain[positionEnnemy[0], positionEnnemy[1]] = null;
                    monstres.Remove(monstre);
                    Console.WriteLine("Appuyez sur une touche pour continuer");
                    Console.ReadLine();
                    if (monstres.Count() == 0)
                    {
                        Console.WriteLine($"Tous les monstres sont morts!! Vous finissez la partie avec \n {hero.Or} pièces d'or \n {hero.Cuir} peaux!");
                        enCours = false;
                    }
                    break;
                }
                resultat = monstres[monstreDansListe].Frappe(hero);
                if (resultat == 2)
                {
                    enCours = false;
                    Console.WriteLine("Appuyez sur une touche pour continuer");
                    Console.ReadLine();
                    break;
                }
            }
            
            positionEnnemy[0] = 0;
            positionEnnemy[1] = 0;
        }

        public static void Partie(Hero hero, List<Monstre> monstres, Plateau plateau, int[] positionEnnemy, int[] position)
        {
            bool enCours = true;
            while (enCours)
            {
                Console.Clear();
                plateau.AfficherPlateau(positionEnnemy);
                plateau.DeplacerPersonnage(hero, monstres, plateau.Terrain, positionEnnemy, position, ref enCours);
                if (enCours == false)
                {
                    Console.WriteLine("Le héros a bobo la cheville.. Il est donc mort.");
                    break;
                }
                Fight(hero, monstres,plateau,  plateau.Terrain, positionEnnemy, position, ref enCours);
            }
        }
        


    }
}
