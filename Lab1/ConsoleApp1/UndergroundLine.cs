using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace ConsoleApp1
{
    public class UndergroundLine
    {
        public string Name { get; set; }

        public List<Station> Stations { get; set; }

        public int SomeProp { get; set; }

        public UndergroundLine(string name, List<Station> stations, int prop)
        {
            this.Name = name;
            this.Stations = stations;
            this.SomeProp = prop;
        }

        public UndergroundLine()
        {
            this.Name = "default";
            this.Stations = new List<Station>();
            this.SomeProp = 0;
        }

        public override string ToString()
        {
            string str = null;

            foreach (var item in Stations)
            {
                str += item.ToString() + "\n";
            }
            return Name + "\n"+ str;
        }
    }
}
