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
	    Buyer[]  buyers;
	    Seller[] sellers;
	
	    /* Constructeur */
	    public Program()
	    {
		    buyers  = new Buyer[nbBuyers];
		    sellers = new Seller[nbSellers];
		
		    for (int i=0; i<nbBuyers; i++)
			    buyers[i] = new Buyer(new Vector2(0, 0));
		
		    sellers[0] = new Seller(new Vector2(0, 0), 10);
		    sellers[1] = new Seller(new Vector2(10, 0), 10);
		    sellers[2] = new Seller(new Vector2(0, 10), 10);
		    sellers[3] = new Seller(new Vector2(10, 10), 10);
	    }
	
	    /* Boucle principale de la simulation */
	    public void run()
	    {
            while (true)
            {
                // Choix aléatoire d'un marchand vers lequel se diriger pour les acheteurs immobiles
                // Déplacement des acheteurs mobiles
                Vector2 direction = new Vector2(0, 0);
                Random random = new Random();
                for (int i = 0; i < nbBuyers - 1; i++)
                {
                    if (!buyers[i].isMoving())
                    {
                        int j;
                        buyers[i].goTo(sellers[j = random.Next(0, nbSellers - 1)].position);
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
//            Console.WriteLine("*************************************************************");
		    Program p = new Program();
		    
		    p.run();
        }
    }
}