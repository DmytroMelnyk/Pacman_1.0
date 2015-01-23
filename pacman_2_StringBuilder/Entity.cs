using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pacman_1
{
    struct Point
    {
        public int X { get; set; }
        public int Y { get; set; }
    }
    
    abstract class Entity
    {
        //fields

        protected char face;
        protected int x;
        protected int y;
                
        //methods

    }

    class Pacman
    {
        // fields

        char face;
        public int x;
        public int y;
        public Direction direction;

        // properties

        public char Face { get { return face; } }

        // constructors

        public Pacman() { face = 'P'; }
        public Pacman(int x, int y) : this()
        {
            this.x = x;
            this.y = y;
        }

        // methods

        public void Move()
        {
            switch (direction)
            {
                case Direction.up: { this.y = this.y - 1; }
                    break;
                case Direction.right: { this.x = this.x + 1;  }
                    break;
                case Direction.down: { this.y = this.y + 1; }
                    break;
                case Direction.left: { this.x = this.x - 1;  }
                    break;
                default: { }
                    break;
            }
        }
    }

    class Ghost
    {

    }

    enum Direction
    {
        up, right, down, left
    }
}
