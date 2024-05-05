using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace SumerProject.Assets
{
    public static class GlobalUser
    {
        public static string FirstName { get; set; }
        public static string LastName { get; set; }
        public static string Number { get; set; }
        public static int ID_User { get; set; }
        public static byte[] ImageRes { get; set; }


        public static void SetUser(string firstName, string lastName, string number, int idUser, byte[] imageres)
        {
            FirstName = firstName;
            LastName = lastName;
            Number = number;
            ID_User = idUser;
            ImageRes = imageres;
        }


        public static void ClearUser()
        {
            FirstName = null;
            LastName = null;
            Number = null;
            ID_User = 0;
        }
    }
}
