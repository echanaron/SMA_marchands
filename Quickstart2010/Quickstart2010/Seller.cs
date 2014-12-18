using System;
using Mogre;

namespace Quickstart2010.Modules
{
    public class Seller
    {
        // Constantes
        const int initPrice = 10;
        const int initNbItems = 10;
//        Mesh model;

        // Attributs
        int id;
        int price;
        int money;
        int nbRemainingItems;
        public Vector2 position;

        // Constructeur
        public Seller(int id, Vector2 position, int price)
        {
            this.id = id;
            this.position = position;
            this.price = price;
            this.nbRemainingItems = initNbItems;
            this.money = 0;
        }

        public void display() { }

        /* Vend un article */
        public bool sell()
        {
            if (nbRemainingItems != 0)
                return false;

            nbRemainingItems--;
            money += price;
            return true;
        }

        /* Modifie le prix de vente du vendeur */
        public void changePrice(int newPrice)
        {
            price = newPrice;
        }

        /* Renvoie le prix du vendeur */
        public int getPrice()
        {
            return price;
        }
    }
}