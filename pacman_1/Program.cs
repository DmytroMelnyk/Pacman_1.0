using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace pacman_1
{
    class Program
    {
        static void Main(string[] args)
        {
            //create map
            // second commit
            // third commit
            Map map = new Map("H:/VS2012Proj/PACMAN/pacman_1/maps/map_1.xml", 2);
            SetConsoleWindow(map);

            while (true)
            {
                Thread.Sleep(250);
                map.ChangeMap();
                if (map.isWinGame)
                {
                    Console.Clear();
                    Console.WriteLine("YOU WIN!!!");
                    break;
                }
                if (map.isGameOver)
                {
                    Console.Clear();
                    Console.WriteLine("GAME OVER!!!");
                    break;
                }
            }

            Console.ReadLine();
        }

        // change size of console window to the size of choosed map 
        static void SetConsoleWindow(Map map)
        {
            Console.SetBufferSize(100, map.Rows);
            Console.SetWindowPosition(0, 0);
            Console.SetWindowSize(map.Columns, map.Rows);
        }
        
    }
}
