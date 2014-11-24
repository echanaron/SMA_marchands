using System;
using Mogre;

namespace Quickstart2010.Modules
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
        public Vector2 position;

        // Constructeur
        public Seller(Vector2 _position, int _price)
        {
            position = _position;
            price = initPrice;
            nbRemainingItems = initNbItems;
        }

        public void display() { }
    }
}