using System;
using Mogre;
using Seller;

namespace Buyer
{
    public class Buyer
    {
        // Constantes
        const int time = 10;

        // Attributs
        Mesh model;
        Vector2 position;

        // Constructeur
        public Buyer(Vector2 _position)
        {
            position = _position;
        }

        public void move(Vector2 speed)
        {
            position += speed * time;
        }

        public void display() { }
    }
}