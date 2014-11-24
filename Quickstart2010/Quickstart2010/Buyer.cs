using System;
using Mogre;

namespace Quickstart2010.Modules
{
    public class Buyer
    {
        // Constantes
        const int dt = 1;

        // Attributs
        Mesh model;
        Vector2 position;
        Vector2 direction;

        // Constructeur
        public Buyer(Vector2 _position)
        {
            position = _position;
            direction = new Vector2(0, 0);
        }

        public void goTo(Vector2 destination)
        {
            direction = destination - position;
        }

        public void move()
        {
            position += direction;
        }

        public void display() { }

        public bool isMoving()
        {
            return (direction.x != 0) && (direction.y != 0);
        }
    }
}