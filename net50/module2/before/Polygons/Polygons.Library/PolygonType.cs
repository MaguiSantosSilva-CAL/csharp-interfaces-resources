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

        public static PolygonType MakeNewShape(string userSelectedShapeName, int userSelectedNumberOfSides, int userSelectedSideLength)
        {
            userSelectedShapeName = userSelectedShapeName.ToLower();
            PolygonType userPolygon = new() { Sides = userSelectedNumberOfSides, Name = userSelectedShapeName, Length = userSelectedSideLength };
            
            if (userPolygon.Length != 0) {
                userPolygon.Area = Convert.ToInt32(userPolygon.Sides) * userPolygon.Length;
            }

            return userPolygon;
        }

        public int GetArea()
        {
            
             if (Length > 0 && Sides > 0)
             {
                 Area = Convert.ToInt32(Sides) * Length;
             }
             else
            {             
                Console.WriteLine("Polygon {0} requires positive values for both number of sides ({1}), and side length ({2})", Name, Sides, Length);
            }
            return Area;
        }

        public static bool PolygonExists(string polygonChoice, List<PolygonType> polygonTypeList)
        {
            var polygonName = polygonChoice.ToString();
                       
            var polygonFoundIndicator  = false;

            foreach (var polygon in polygonTypeList)
            {
                if (polygon.Name == polygonName)
                {
                   polygonFoundIndicator = true;
                   return polygonFoundIndicator;
                }

            }
            return polygonFoundIndicator;
        }

        public static void DisplayPolygon(string polygonType, dynamic polygon)
        {
            var lineNumber = 0;
            try
            {
                Console.WriteLine("{0} Number of Sides: {1}", polygonType, polygon.NumberOfSides);
                lineNumber++;
                Console.WriteLine("{0} Side Length: {1}", polygonType, polygon.SideLength);
                lineNumber++;
                Console.WriteLine("{0} Perimeter: {1}", polygonType, polygon.GetPerimeter());
                lineNumber++;
                Console.WriteLine("{0} Area: {1}", polygonType, Math.Round(polygon.GetArea(), 2));
                lineNumber++;
                Console.WriteLine();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception thrown while trying to process {0} at {1}:\n   {2}",
                    polygonType, lineNumber, ex.GetType().Name);
                Console.WriteLine();
            }
        }

    }
}
