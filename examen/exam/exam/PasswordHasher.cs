using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Cryptography;


namespace exam
{
    public class PasswordHasher
    {
        // hash a password with a salt using SHA256

        public static string Hash(string password)
        {
            SHA256Managed hashstring = new SHA256Managed();
            var bytes = System.Text.Encoding.UTF8.GetBytes(password + "TheGOdEmp3rorOFManK1ndD3mandsSEcurITY");
            var hash = hashstring.ComputeHash(bytes);
            Console.WriteLine(Convert.ToBase64String(hash));
            return Convert.ToBase64String(hash);
        }
        // verify a password against a hash
        public static bool VerifyPassword(string password, string hash)
        {
            return Hash(password) == hash;
        }



    }
}
