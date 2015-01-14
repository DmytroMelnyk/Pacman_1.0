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
            
            Map map = new Map("H:/VS2012Proj/PACMAN/pacman_1/maps/map_2.xml");
            SetConsoleWindow(map);
            
            

            //start game
            
            //game

            while (true)
            {
                if (!Console.KeyAvailable)
                {

                    Thread.Sleep(200);
                    map.ShowMap();
                }
                else
                {
                    break;
                }
            }
            
            

            Console.ReadLine();
        }

        static void SetConsoleWindow(Map map)
        {
            Console.SetBufferSize(100, map.Rows);
            Console.SetWindowPosition(0, 0);
            Console.SetWindowSize(map.Columns, map.Rows);
        }
        
    }
}
