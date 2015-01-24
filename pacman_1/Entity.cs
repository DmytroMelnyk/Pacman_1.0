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
    
    enum Direction
    {
        up, right, down, left
    }
    
    abstract class Entity
    {
        //fields

        protected char face;
        public int x;
        public int y;
        public Direction direction;
        public bool upMove, rightMove, downMove, leftMove;
                
        //methods

        public void Move() 
        {
            switch (direction)
            {
                case Direction.up: { this.y = this.y - 1; }
                    break;
                case Direction.right: { this.x = this.x + 1; }
                    break;
                case Direction.down: { this.y = this.y + 1; }
                    break;
                case Direction.left: { this.x = this.x - 1; }
                    break;
                default: { }
                    break;
            }
        }

    }

    class Pacman : Entity
    {
        
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

        //public override void Move()
        //{
        //    switch (direction)
        //    {
        //        case Direction.up: { this.y = this.y - 1; }
        //            break;
        //        case Direction.right: { this.x = this.x + 1;  }
        //            break;
        //        case Direction.down: { this.y = this.y + 1; }
        //            break;
        //        case Direction.left: { this.x = this.x - 1;  }
        //            break;
        //        default: { }
        //            break;
        //    }
        //}
    }

    class Ghost : Entity
    {
        // properties

        public char Face { get { return face; } }

        // constructors

        public Ghost() { face = 'G'; }
        public Ghost(int x, int y) : this()
        {
            this.x = x;
            this.y = y;
        }

        // methods

        //public override void Move()
        //{
        //    base.Move();
        //}
    }

    
}
