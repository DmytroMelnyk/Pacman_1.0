using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace pacman_1
{
    class Map
    {
        //fields

        XDocument doc;
        List<StringBuilder> map;
        int rows, columns, counters;
        char fishka = 'f', wall = '#';
        public bool isWinGame; 

        Pacman pacman;
        Ghost[] ghosts;

        //constructors

        public Map(string path, int ghosts)
        {
            doc = XDocument.Load(path);
            CreateMap(doc);
            rows = this.map.Count;
            columns = this.map[0].Length;

            //CountCounters();
            CreateEntities(ghosts);
        }

        // properties

        public int Rows
        {
            get
            {
                return rows;
            }
        }

        public int Columns 
        {
            get
            {
                return columns;
            }
        }

        //methods

        void CreateMap(XDocument xDoc)
        {
            map = new List<StringBuilder>();
            foreach (XElement el in xDoc.Elements("main").Elements("r"))
            {
                map.Add(new StringBuilder((string)el));
            }
        }

        void CountCounters()
        {
            for (int i = 0; i < map.Count; i++)
            {
                for (int k = 0; k < map[i].Length; i++)
                {
                    if (map[i][k] == fishka)
                    {
                        counters++;
                    }
                }
            }
        }

        void CreateEntities(int numbOfGhosts)
        {
            this.pacman = new Pacman(Columns / 2 - 1, Rows - 1);
            this.ghosts = new Ghost[numbOfGhosts];
            for (int i = 0; i < numbOfGhosts; i++)
            {
                this.ghosts[i] = new Ghost();
            }
        }

        public void ChangeMap()
        {
            // запамятовування символу, де буде встановлений пакман;
            char currentChar = map[pacman.y][pacman.x];
            // збирання фішок
            if (currentChar == fishka)
            {
                counters--;
                if (counters == 0)
                {
                    isWinGame = true;
                }
            }
            // заміна попереднього рядку карти на рядок з символом пакмана
            map[pacman.y][pacman.x] = pacman.Face;
            // відображення карти
            ShowMap();
            // запамятовування попередніх координат пакмана
            int firstPacmanX = pacman.x, firtsPacmanY = pacman.y;
            // зміна координат пакмана в залежності від напрямку руху із зупинкою об стінку
            ChangePacmanDirection();
            if ((pacman.direction == Direction.right) & (map[pacman.y][pacman.x + 1] != wall)) pacman.Move();
            if ((pacman.direction == Direction.up) & (map[pacman.y - 1][pacman.x] != wall)) pacman.Move();
            if ((pacman.direction == Direction.left) & (map[pacman.y][pacman.x - 1] != wall)) pacman.Move();
            if ((pacman.direction == Direction.down) & (map[pacman.y + 1][pacman.x] != wall)) pacman.Move();
            // зміна символу карти де був встановлений пакман на попередній символ currentChar
            map[pacman.y][firstPacmanX] = ' ';
            map[firtsPacmanY] = map[pacman.y];
            
        }

        void ShowMap()
        {
            Console.Clear();
            int i;
            for (i = 0; i < Rows - 1; i++)
            {
                Console.WriteLine(map[i]);
            }
            Console.Write(map[i]);
        }

        public void ChangePacmanDirection()
        {
            if (Console.KeyAvailable)
            {
                switch (Console.ReadKey().Key)
                {
                    case ConsoleKey.UpArrow: 
                        {
                            if (map[pacman.y - 1][pacman.x] != wall) pacman.direction = Direction.up; 
                        }
                        break;
                    case ConsoleKey.RightArrow: 
                        {
                            if (map[pacman.y][pacman.x + 1] != wall) pacman.direction = Direction.right; 
                        }
                        break;
                    case ConsoleKey.DownArrow: 
                        {
                            if (map[pacman.y + 1][pacman.x] != wall) pacman.direction = Direction.down; 
                        }
                        break;
                    case ConsoleKey.LeftArrow: 
                        {
                            if (map[pacman.y][pacman.x - 1] != wall) pacman.direction = Direction.left; 
                        }
                        break;
                }
            }
        }

    }
}
