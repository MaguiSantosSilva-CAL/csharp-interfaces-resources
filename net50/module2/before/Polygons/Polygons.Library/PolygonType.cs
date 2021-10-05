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

        public void makeNewShape(string userSelectedShapeName, int userSelectedNumberOfSides)
        {
            var polygonType = new List<PolygonType>()
            {
                new PolygonType() {Sides = userSelectedNumberOfSides, Name = userSelectedShapeName}                
            };
        }

        public int GetArea()
        {
            Area = (Convert.ToInt32(Sides) * Length);
            return Area;
        }

    }
}
