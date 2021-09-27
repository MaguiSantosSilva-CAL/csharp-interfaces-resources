using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Polygons.Library
{
    public class PolygonType
    {
        public string Name { get; set; }
        public int Sides { get; set; }
        public int Length { get; set; }
        public int Area { get; set; }

        public int GetArea()
        {
            Area = (Convert.ToInt32(Sides) * Length);
            return Area;
        }

    }
}
