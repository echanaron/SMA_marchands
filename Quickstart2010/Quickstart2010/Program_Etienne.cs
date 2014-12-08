using System;

using Mogre;

using Quickstart2010.Modules;
using Quickstart2010.States;

namespace Quickstart2010
{
    public class Program
    {
        // Constantes
        const int nbBuyers = 20;
        const int nbSellers = 4;

        // Attributs
        Buyer[] buyers;
        Seller[] sellers;

        /* Constructeur */
        public Program()
        {
            buyers = new Buyer[nbBuyers];
            sellers = new Seller[nbSellers];

            for (int i = 0; i < nbBuyers; i++)
                buyers[i] = new Buyer(i, new Vector2(0, 0));

            sellers[0] = new Seller(0, new Vector2(0, 0), 10);
            sellers[1] = new Seller(1, new Vector2(10, 0), 10);
            sellers[2] = new Seller(2, new Vector2(0, 10), 10);
            sellers[3] = new Seller(3, new Vector2(10, 10), 10);
        }

        /* Boucle principale de la simulation */
        public void run()
        {
            while (true)
            {
                // Choix aléatoire d'un marchand vers lequel se diriger pour les acheteurs immobiles ou arrivés à destination
                // Déplacement des acheteurs mobiles
                Vector2 direction = new Vector2(0, 0);
                Random random = new Random();
                for (int i = 0; i < nbBuyers - 1; i++)
                {
                    if (buyers[i].hasReachedDestination())
                    {
                        //                        Console.WriteLine("Client " + i + " : j'ai atteint ma destination");
                        Seller s = sellers[buyers[i].sellerCloseTo()];
                        buyers[i].buyTo(s);
                    }

                    if (!buyers[i].isMoving() || buyers[i].hasReachedDestination())
                    {
                        int i_seller = random.Next(0, nbSellers - 1);
                        buyers[i].headFor(sellers[i_seller].position);
                        //                        Console.WriteLine("Client " + i + " : je me dirige vers le vendeur " + i_seller);
                    }
                    else
                    {
                        buyers[i].move();
                    }
                }
            }
        }

        /* Main */
        static void Main()
        {
            Program p = new Program();

            p.run();
        }
    }
}