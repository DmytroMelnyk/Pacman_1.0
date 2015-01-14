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
        int rows, columns;
        
        //constructors

        public Map(string path)
        {
            doc = XDocument.Load(path);
            CreateMap(doc);
            rows = this.map.Count;
            columns = this.map[0].Length;
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

        public void ShowMap()
        {
            Console.Clear();
            int i;
            for (i = 0; i < Rows - 1; i++)
            {
                Console.WriteLine(map[i]);
            }
            Console.Write(map[i]);
        }

    }
}
