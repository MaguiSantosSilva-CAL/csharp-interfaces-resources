using System;
using System.Data.SqlClient;
using System.Text;
using System.Data;

namespace Polygons
{
    public class Access
    {
        private static bool userIsLoggedIn;
        public static bool UserIsLoggedIn { get => userIsLoggedIn; set => userIsLoggedIn = value; }

        private static int sessionID;
        public int SessionID { get; internal set; }

        private static string? username;
        public static string? Username { get => username ; set => username = value; }
                
        protected static int GetNext()
        {
            return sessionID++;
            //throw new NotImplementedException();
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

        protected static void Login()
        {
            // Console.WriteLine(access.GetType());  // same as  Console.WriteLine(accessType);
            
            Console.Write("Username: "); Username = Console.ReadLine();
            Console.Write("Password: "); var password = GetHiddenConsoleInput();
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
                    Console.WriteLine($"Connection State {sqlConnection.State}");
                }
            }

            using (sqlConnection)
            {
                var userInformationFromDatabase = string.Empty;
                //var cmd = new SqlCommand("SELECT information from polygon.users where username = @Username and pwd = CONVERT(varbinary,@Password);", sqlConnection);
                var cmd = new SqlCommand("exec usp_userLoginSuccessful @Username, @password", sqlConnection);

                //cmd.Parameters.AddWithValue("@Username", username); 
                //cmd.Parameters.AddWithValue("@Password", password); //This didn't work because C# passes UTF16 string which converts differently into binary; Latin1 is UTF8.

                cmd.Parameters.AddWithValue("@Username", Username);
                cmd.Parameters.Add("@Password", SqlDbType.NVarChar);   //VarChar is UTF8
                cmd.Parameters["@Password"].Value = password;

                SqlDataReader sqlDataReader;
                var connectionSuccessful = false;

                try
                {
                    sqlDataReader = cmd.ExecuteReader();
                    connectionSuccessful = true;


                    while (sqlDataReader.Read())
                    {
                        UserIsLoggedIn = true;
                        var access = new Access() { SessionID = Access.GetNext() };
                        Console.WriteLine("Session ID: " + access.SessionID);

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
                    Console.WriteLine("Your Information:\t" + userInformationFromDatabase);
                }
                else
                {
                    Console.WriteLine("Login Failed for " + username + " ({0})", password);
                    Console.WriteLine(cmd.CommandText);
                    Console.WriteLine();
                    if (!connectionSuccessful) Console.WriteLine(cmd.ExecuteNonQuery());
                    Console.WriteLine("Connection State {0}", sqlConnection.State);

                }
            }

        }

    }
}