using Polygons.Library;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace Polygons
{
    public class Program : Access
    {
        public static void Login()
        {
            // Console.WriteLine(access.GetType());  // same as  Console.WriteLine(accessType);
            if (!UserIsLoggedIn) Console.WriteLine("Please log in to continue.");

            Console.Write("Username: ");            var username = Console.ReadLine();
            Console.Write("Password: ");            var password = GetHiddenConsoleInput();
            Console.WriteLine();

            var builder = new SqlConnectionStringBuilder(GetConnectionString())
            {
                ConnectionString = "server=(local);user id=polygonSys;password=bareminimum;"
            };
            
            var sqlConnection = new SqlConnection(builder.ConnectionString);

            if (sqlConnection.State == ConnectionState.Closed)
            {
                try
                {
                    sqlConnection.Open();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.WriteLine("Connection State {0}", sqlConnection.State);
                }
            }

            using (sqlConnection)
            {
                var userInformationFromDatabase = string.Empty;
                //var cmd = new SqlCommand("SELECT information from polygon.users where username = @Username and pwd = CONVERT(binary,@Password);", sqlConnection);
                var cmd = new SqlCommand("exec usp_userLoginSuccessful @Username, @password", sqlConnection);

                //cmd.Parameters.AddWithValue("@Username", username); 
                //cmd.Parameters.AddWithValue("@Password", password); //This didn't work because C# passes UTF16 string which converts differently into binary; Latin1 is UTF8.

                cmd.Parameters.AddWithValue("@Username", username);
                cmd.Parameters.Add("@Password", SqlDbType.NVarChar);   //VarChar is UTF8
                cmd.Parameters["@Password"].Value = password;

                SqlDataReader sqlDataReader;
                var connectionSuccessful = false;

                try
                {
                    sqlDataReader = cmd.ExecuteReader();
                    connectionSuccessful = true;

                    var access = new Access() { SessionID = Access.GetNext() };
                    Console.WriteLine("Session ID: " + access.SessionID);

                    while (sqlDataReader.Read())
                    {
                        UserIsLoggedIn = true;

                        userInformationFromDatabase = sqlDataReader.IsDBNull(0) ? "No Results" : sqlDataReader.GetString(0);
                        
                        cmd.Dispose();
                        //var userLoginScript = new SqlCommand("exec usp_userLoginSuccessful @Username, @password;", sqlConnection);
                        //userLoginScript.Parameters.AddWithValue("@Username", username);
                        //userLoginScript.Parameters.Add("@Password", SqlDbType.VarChar);
                        //userLoginScript.Parameters["@Password"].Value = password;

                        //userLoginScript.ExecuteReader();

                    }

                    sqlDataReader.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                        

                if (UserIsLoggedIn)
                {
                    Console.WriteLine("Login Successful");
                    Console.WriteLine("Your Information:");
                    Console.WriteLine(userInformationFromDatabase);
                }
                else
                {
                    Console.WriteLine("Login Failed for " + username + " ({0})", password);
                    Console.WriteLine(cmd.CommandText);
                    Console.WriteLine();                    
                    if (!connectionSuccessful) Console.WriteLine(cmd.ExecuteNonQuery());

                }
            }

            Console.WriteLine("Connection State {0}", sqlConnection.State);
        }

        private static string GetHiddenConsoleInput()
        {
            var input = new StringBuilder();
            while (true)
            {
                var key = Console.ReadKey(true);
                if (key.Key == ConsoleKey.Enter) break;
                if (key.Key == ConsoleKey.Backspace && input.Length > 0) input.Remove(input.Length - 1, 1);
                else if (key.Key != ConsoleKey.Backspace) input.Append(key.KeyChar);
            }
            return input.ToString();
        }

        private static string GetConnectionString()
        {
            // To avoid storing the connection string in your code,
            // you can retrieve it from a configuration file.
            return "Server=//localhost/./CALALONPC473" +
                   "Initial Catalog=maguiss";
        }

        public static void Main(string[] args)
        {
            var polygonTypes = new List<PolygonType>()
            {
                new PolygonType() {Sides = "4", Name = "square"},
                new PolygonType() {Sides = "3", Name = "triangle"},
                new PolygonType() {Sides = "8", Name = "octagon"}
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
            string? polygonSideLength = Console.ReadLine();

            var polygonLengthOverride = String.IsNullOrWhiteSpace(polygonSideLength);
            if (polygonLengthOverride)
            {
                polygonSideLength = "5";
                Console.WriteLine("Invalid or null side length provided. {0} assigned as side length.", polygonSideLength);
            }

            var polygonChoiceOverride = String.IsNullOrWhiteSpace(polygonChoice);

            if (polygonChoiceOverride)
            {
                polygonChoice = "square";
                Console.WriteLine("No value provided. '{0}' assigned as polygon type", polygonChoice);
            }

            var polygonRecognised = CheckPolygon(polygonChoice, polygonTypes);

            if (!polygonRecognised)
            {
                Console.Write("How many sides? ");
                string ? polygonSides = Console.ReadLine();
                if (!String.IsNullOrWhiteSpace(polygonSides))
                {
                    new PolygonType() { Name = polygonChoice, Sides = polygonSides };
                    PolygonType polygonNew = new() { Name = polygonChoice, Sides = polygonSides };
                    CheckPolygon(polygonNew.Name, polygonTypes);
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
                    sideCount = polygon.Sides;
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

    public class PolygonType 
    {
        public string Sides { get; set; }
        public string Name { get; set; }
        public int? Length { get; set; }
    }



} //Namespace
