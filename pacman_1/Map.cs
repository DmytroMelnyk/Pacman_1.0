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
        List<string> map;
        int rows, columns, counters;
        char fishka = 'f', wall = '#';
        public bool isWinGame, isGameOver; 

        Pacman pacman;
        Ghost[] ghosts;

        //constructors

        public Map(string path, int ghosts)
        {
            doc = XDocument.Load(path);
            CreateMap(doc);
            rows = this.map.Count;
            columns = this.map[0].Length;

            CountCounters();
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
            map = new List<string>();
            foreach (XElement el in xDoc.Elements("main").Elements("r"))
            {
                map.Add((string)el);
            }
        }

        void CountCounters()
        {
            StringBuilder row = new StringBuilder();
            foreach (string str in map)
            {
                row = new StringBuilder(str);
                for (int i = 0; i < row.Length; i++)
                {
                    if (row[i] == fishka)
                    {
                        counters++;
                    }
                }
            }
        }

        void CreateEntities(int numbOfGhosts)
        {
            // створення пакмана посередині самого нижнього рядка карти
            this.pacman = new Pacman(Columns / 2 - 1, Rows - 1);
            
            //створення привидів в точках другого зверху рядка ( y = 1 ) карти, 
            //точка Х задається рандомно в межах ширини карти
            Random randomGhostX = new Random();
            this.ghosts = new Ghost[numbOfGhosts];
            for (int i = 0; i < numbOfGhosts; i++)
            {
                this.ghosts[i] = new Ghost(randomGhostX.Next(1, Columns-1), 1);
            }
        }

        public void ChangeMap()
        {
            // запамятовування символу, де буде встановлений пакман;
            StringBuilder currentString = new StringBuilder(map[pacman.y]);
            char currentChar = currentString[pacman.x];

            // збирання фішок
            if (currentChar == fishka)
            {
                counters--;
                if (counters == 0)
                {
                    isWinGame = true;
                }
            }

            // закінчення гри при зіькненні пакмана з одним із привидів
            foreach (Ghost ghost in ghosts)
            {
                if (currentString[pacman.x] == ghost.Face)
                {
                    isGameOver = true;
                } 
            }

            // заміна попереднього рядку карти на рядок з символом пакмана
            currentString[pacman.x] = pacman.Face;
            map[pacman.y] = currentString.ToString();

            //заміна символу карти символом привида у відповідній позиції кожного привида
            ChangeMapWithGhost();

            // відображення карти
            ShowMap();

            // запамятовування попередніх координат пакмана
            int firstPacmanX = pacman.x, firtsPacmanY = pacman.y;

            // зміна координат пакмана в залежності від напрямку руху із зупинкою об стінку
            ChangePacmanDirection();
            if ((pacman.direction == Direction.right) & (currentString[pacman.x + 1] != wall)) pacman.Move();
            StringBuilder UpString = new StringBuilder(map[pacman.y - 1]);
            if ((pacman.direction == Direction.up) & (UpString[pacman.x] != wall)) pacman.Move();
            if ((pacman.direction == Direction.left) & (currentString[pacman.x - 1] != wall)) pacman.Move();
            StringBuilder DownString = new StringBuilder(map[pacman.y + 1]);
            if ((pacman.direction == Direction.down) & (DownString[pacman.x] != wall)) pacman.Move();
            
            //зміна координат привида
            

            // зміна символу карти де був встановлений пакман на попередній символ currentChar
            currentString[firstPacmanX] = ' ';

            //currentString[firstPacmanX] = currentChar; для привидів
            map[firtsPacmanY] = currentString.ToString();
            
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

        void ChangePacmanDirection()
        {
            StringBuilder currentString = new StringBuilder(map[pacman.y]);
            if (Console.KeyAvailable)
            {
                switch (Console.ReadKey().Key)
                {
                    case ConsoleKey.UpArrow: 
                        {
                            currentString = new StringBuilder(map[pacman.y - 1]);
                            if (currentString[pacman.x] != wall) pacman.direction = Direction.up; 
                        }
                        break;
                    case ConsoleKey.RightArrow: 
                        {
                            if(currentString[pacman.x + 1] != wall) pacman.direction = Direction.right; 
                        }
                        break;
                    case ConsoleKey.DownArrow: 
                        {
                            currentString = new StringBuilder(map[pacman.y + 1]);
                            if (currentString[pacman.x] != wall) pacman.direction = Direction.down; 
                        }
                        break;
                    case ConsoleKey.LeftArrow: 
                        {
                            if (currentString[pacman.x - 1] != wall) pacman.direction = Direction.left; 
                        }
                        break;
                }
            }
        }

        void ChangeMapWithGhost()
        {
            StringBuilder[] mapGhostString = new StringBuilder[ghosts.Length];
            for (int i = 0; i < ghosts.Length; i++)
            {
                mapGhostString[i] = new StringBuilder(map[ghosts[i].y]);
                mapGhostString[i][ghosts[i].x] = ghosts[i].Face;
                map[ghosts[i].y] = mapGhostString[i].ToString();
            }
        }

        void ChangeGhostDirection()
        {
            Random randomDirection = new Random();
            foreach (Ghost ghost in ghosts)
            {
                ghost.direction = (Direction)randomDirection.Next(4);
                ghost.Move();
            }

        }

        
    }
}
