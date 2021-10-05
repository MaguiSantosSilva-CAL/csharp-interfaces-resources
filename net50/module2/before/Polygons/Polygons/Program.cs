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
            _ = TryCleanse(
                inputValues: Console.ReadLine(),
                "square",
                cleansedInput: out string polygonName);
                        
            var polygonRecognised = CheckPolygon(polygonName, polygonType);

            Console.Write("Side Length:");
            _ = TryCleanse(Console.ReadLine(), 5, out var polygonSideLength);
                        
            if (!polygonRecognised)
            {
                Console.Write("How many sides? ");
                
                _ = TryCleanse<int>(Console.ReadLine(), 6, out var polygonSideCount);
                                
                var polygonNew = new PolygonType() { Name = polygonName, Sides = polygonSideCount, Length = polygonSideLength};
                CheckPolygon(polygonNew.Name, polygonType);
                Console.WriteLine("Sides: {0}, Length: {1}, Area: {2}",polygonNew.Sides, polygonNew.Length, polygonNew.GetArea());
            }

            //var square = new Square(5);
            //DisplayPolygon("Square", square);

            //var triangle = new Triangle(5);
            //DisplayPolygon("Triangle", triangle);

            //var octagon = new Octagon(5);
            //DisplayPolygon("Octagon", octagon);
        }

        public static bool TryCleanse<T>(string inputValues, T defaultChoice, out T? cleansedInput)
        {
            cleansedInput = default;
            
            if (inputValues is null)
            {
                Console.Write("Value was not provided (null): expected type or value of input");
                return false;
            }

            #region 
            //Type calculatedInputType = inputValues.GetType();
            //Type overrideInputType   = defaultChoice.GetType();
                
            //if (!Equals(calculatedInputType, overrideInputType))
            //{
            //    Console.WriteLine("{0},{1}", calculatedInputType,overrideInputType);
            //    return inputValues;
            //}
            #endregion

            if (defaultChoice is null)
            {
                Console.Write("Parameter missing: input type is not as expected but a default value has not been provided for this eventuality");
                return false;                
            }

            Console.WriteLine("Type of T: " + typeof(T));
            
            try
            {
                //cleansedInput = inputValues;
            }
            catch
            {
                return false;
            }

            return true;
            
            #region
            //dynamic outputValue;
            //bool overrideInput;

            //switch (expectedInputType.ToLower())
            //{
            //    case "string":
            //        overrideInput = String.IsNullOrWhiteSpace(inputValues);
            //        return overrideInput;

            //    case "int":
            //        overrideInput = int.TryParse(inputValues, out int parsedInt);
            //        outputValue = overrideInput ? defaultChoice : parsedInt;
            //        return outputValue;

            //    case "bool":
            //        overrideInput = bool.TryParse(inputValues, out bool inputValuesAsBool);
            //        Console.WriteLine(overrideInput);
            //        outputValue = overrideInput ? inputValuesAsBool : defaultChoice;
            //        return outputValue;

            //    default:
            //        outputValue = "Parameters not known: input Type";
            //        return outputValue;
            //}
            #endregion

        }

        public static bool CheckPolygon(string polygonChoice, List<PolygonType> polygonTypeList)
        {
            dynamic polygonType = polygonChoice;

            try
            {
                polygonType.GetArea();
            }
            catch
            {
                Console.WriteLine("Polygone {0} is of unknown type.", Convert.ToString(polygonType).ToLower());
            }

            polygonChoice = polygonChoice.ToLower();

            var sideCount = "an unknown number";
            var polygonChoiceNotes = " is unknown";

            var polygonFoundIndicator = false;

            foreach (var polygon in polygonTypeList)
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
