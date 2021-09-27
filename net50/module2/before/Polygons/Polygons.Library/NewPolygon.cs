using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Polygons.Library
{
    public class NewPolygon
    {
        public string Name { get; set; }
        public int Sides { get; set; }
        public int SideLength { get; set; }

        public int NewPolygonArea(int sides, int length)
        {
            NumberOfSides = sides;
            SideLength = length;
            return (sides * length);
        }

        public class PolygonType
        {
            public string Sides { get; set; }
            public int? Length { get; set; }
        }
    }
}
