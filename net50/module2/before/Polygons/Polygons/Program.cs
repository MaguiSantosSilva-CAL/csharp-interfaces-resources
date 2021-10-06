using Polygons.Library;
using System;
using System.Linq;
using System.Diagnostics;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;

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

                        
            Console.Write("Polygon name:");
            _ = TryCleanse(
                inputValues: Console.ReadLine(),
                "square",
                cleansedInput: out string? polygonName);
                        

            Console.Write("Side Length:");
            _ = TryCleanse(
                Console.ReadLine(),
                5,
                out int polygonSideLength);

            int polygonSideCount;
            
            if (!PolygonType.PolygonExists(polygonName, polygonType))
            {
                Console.Write("How many sides? ");

                _ = TryCleanse<int>(
                    Console.ReadLine(),
                    6,
                    out polygonSideCount);
            }
            else
            {
                polygonSideCount = 0; //TODO
            }

            PolygonType userPolygon = PolygonType.MakeNewShape(polygonName, polygonSideCount, polygonSideLength);   
            string polygonChoiceNotes = "(unknown)";

            if (PolygonType.PolygonExists(userPolygon.Name, polygonType))
            {
                polygonChoiceNotes = "";
                PolygonType.DisplayPolygon(userPolygon.Name, polygonName);
            }
                                    
            var outputText = Convert.ToString(DateTime.Now) + ";\t" + Convert.ToString(userPolygon.Sides) + ";\t" + Convert.ToString(userPolygon.Length) + ";\t" + Convert.ToString(userPolygon.GetArea()) + ";\t" + Username + ";\t" + userPolygon.Name + polygonChoiceNotes + ";\n";

            Console.WriteLine(outputText);

            var logFilePath = "C:\\Users\\maguiss\\Documents\\GitHub\\csharp-interfaces-resources\\net50\\module2\\before\\Polygons\\Output.log";
            
            if (!File.Exists(logFilePath))
            {
                Console.WriteLine("Log created at: {0}", logFilePath);
                File.AppendAllText(logFilePath,"Timestamp\t\tSides\tLength\tArea\tUser\tPolygon\n");
            }
            
            File.AppendAllText(logFilePath, outputText);
            
            foreach (var line in File.ReadAllLines(logFilePath))
            {
                Console.WriteLine(line);
            };


            Console.ReadKey(true);
            Console.Clear();
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

            var type = typeof(T);
            var converter = TypeDescriptor.GetConverter(type);

            if (converter != null && converter.IsValid(inputValues))
            {
                cleansedInput = (T?)converter.ConvertFromString(inputValues);
                return true;
            }
            else
            {
                cleansedInput = defaultChoice;
            }

            return false;
            
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

        

      
    } // Program





} //Namespace
