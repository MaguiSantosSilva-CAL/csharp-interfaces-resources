using Polygons.Library;
using System;
using System.Linq;
using System.Collections.Generic;

namespace Polygons
{
    public class Program : Access
    {
        public static void Main(string[] args)
        {
            var polygonType = new List<PolygonType>()
            {
                new PolygonType() {Sides = 4, Name = "square"},
                new PolygonType() {Sides = 3, Name = "triangle"},
                new PolygonType() {Sides = 8, Name = "octagon"}
            };
                       
            Login();
            while (!UserIsLoggedIn)
            {
                Login();
            }

            Console.WriteLine("" +
                "*/*/*/*/ I <3 P O L Y G O N S */*/*/*/" +
                "");
            Console.Write("Polygon type:");
            string? polygonChoice = Console.ReadLine();

            Console.Write("Side Length:");
            string? sideLengthInput = Console.ReadLine();

            var polygonLengthOverride = String.IsNullOrWhiteSpace(sideLengthInput);
            var polygonSideLength = polygonLengthOverride ? 5 : Convert.ToInt32(sideLengthInput);

            if (polygonLengthOverride)
            {               
                Console.WriteLine("Invalid or null side length provided. {0} assigned as side length.", polygonSideLength);
            }

            var polygonChoiceOverride = String.IsNullOrWhiteSpace(polygonChoice);

            if (polygonChoiceOverride)
            {
                polygonChoice = "square";
                Console.WriteLine("No value provided. '{0}' assigned as polygon type", polygonChoice);
            }

            var polygonRecognised = CheckPolygon(polygonChoice, polygonType);

            if (!polygonRecognised)
            {
                Console.Write("How many sides? ");
                string? polygonSides = Console.ReadLine();
                if (!String.IsNullOrWhiteSpace(polygonSides))
                {
                    var polygonNew = new PolygonType() { Name = polygonChoice, Sides = Convert.ToInt32(polygonSides) };
                    CheckPolygon(polygonNew.Name, polygonType);
                    Console.WriteLine("Sides: {0}, Length: {1}, Area: {2}",polygonNew.Sides, polygonNew.Length, polygonNew.GetArea());

                }
            }


            //var square = new Square(5);
            //DisplayPolygon("Square", square);

            //var triangle = new Triangle(5);
            //DisplayPolygon("Triangle", triangle);

            //var octagon = new Octagon(5);
            //DisplayPolygon("Octagon", octagon);
        }

        public static bool CheckPolygon(string polygonChoice, List<PolygonType> polygonType)
        {
            polygonChoice = polygonChoice.ToLower();

            var sideCount = "an unknown number";
            var polygonChoiceNotes = " is unknown";

            var polygonFoundIndicator = false;

            foreach (var polygon in polygonType)
            {
                if (polygon.Name == polygonChoice)
                {
                    polygonChoiceNotes = "";
                    polygonFoundIndicator = true;
                }

                if (polygonFoundIndicator)
                {
                    var polygonDynamic = polygon.Name.ToLower();
                    DisplayPolygon(polygon.Name, polygonDynamic);
                }
            }

            Console.WriteLine("Your polygon ({0}){1} has {2} sides", arg0 : polygonChoice, arg1 : polygonChoiceNotes,
                arg2 : sideCount);

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
            } catch (Exception ex) {
                Console.WriteLine("Exception thrown while trying to process {0} at {1}:\n   {2}",
                    polygonType, lineNumber, ex.GetType().Name);
                Console.WriteLine();
            }
        }
    } // Program





} //Namespace
