using System;
using Mogre;
using Buyer;

namespace Seller
{

    public class Seller
    {
        // Constantes
        const int initPrice = 10;
        const int initNbItems = 10;
        Mesh model;

        // Attributs
        int price;
        int nbRemainingItems;

        // Constructeur
        public Seller(int _price)
        {
            price = initPrice;
            nbRemainingItems = initNbItems;
        }

        public void display() { }
    }
}