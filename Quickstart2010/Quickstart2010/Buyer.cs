using System;
using Mogre;

namespace Quickstart2010.Modules
{
    public class Buyer
    {
        // Constantes
        const int dt = 1;

        // Attributs
//        Mesh model;
        int id;
        Vector2 position;
        Vector2 direction;
        Vector2 destination;
        int money;
        Seller betterSeller;

        /* Constructeur */
        public Buyer(int id, Vector2 position)
        {
            this.id = id;
            this.position = position;
            direction = new Vector2(0, 0);
            betterSeller = null;
        }

        /* Indique à l'acheteur de se diriger vers tel endroit */
        public void headFor(Vector2 destination)
        {
            this.destination = destination;
            this.direction = destination - this.position;
        }

        /* Déplace l'achteur suivant sa direction */
        public void move()
        {
            position += direction;
        }

        public void display() { }

        /* Renvoie l'état de l'acheteur (en mouvement ou non) */
        public bool isMoving()
        {
            return (direction.x != 0) && (direction.y != 0);
        }

        /* Indique si l'acheteur est arrivé ou non à destination */
        public bool hasReachedDestination()
        {
            return position == destination;
        }

        /* Transaction entre l'acheteur et un vendeur */
        public bool buyTo(Seller s)
        {
            if (money < s.getPrice())
                return false;

            s.sell();
            money -= s.getPrice();
            return true;
        }

        /* Fonction de detection du vendeur a cote ; -1 s'il n'y en a pas.
         * A completer. */
        public int sellerCloseTo()
        {
            if (true)
                return 0;

            return -1;
        }

        /* Renvoie le meilleur vendeur connu par l'acheteur */
        public Seller getBetterSeller()
        {
            return betterSeller;
        }

        /* Enregistre le vendeur comme meilleur vendeur connu */
        public void setBetterSeller(Seller s)
        {
            this.betterSeller = s;
        }

        /* L'acheteur échange son information sur le meilleur vendeur à un autre vendeur */
        public void exchangeData(Buyer b)
        {
            if (this.betterSeller != null)
            {
                this.betterSeller = b.getBetterSeller();
                return;
            }

            if (b.getBetterSeller().getPrice() < this.betterSeller.getPrice())
                this.betterSeller = b.getBetterSeller();
            else
                b.setBetterSeller(this.betterSeller);
        }
    }
}