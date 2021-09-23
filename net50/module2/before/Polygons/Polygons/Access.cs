using System;

namespace Polygons
{
    public class Access
    {
        private static bool userIsLoggedIn;
        public static bool UserIsLoggedIn { get => userIsLoggedIn; set => userIsLoggedIn = value; }


        private static int sessionID;
        public int SessionID { get; internal set; }
        

        protected static int GetNext()
        {
            return sessionID++;
            //throw new NotImplementedException();
        }
    }
}