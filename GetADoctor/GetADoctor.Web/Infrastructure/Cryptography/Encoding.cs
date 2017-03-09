using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GetADoctor.Web.Infrastructure.Cryptography
{
    public class Encoding
    {
        public static string EncrytedString(string str)
        {
            string encryptedString = "";
            byte[] encryptedValue = System.Text.Encoding.Unicode.GetBytes(str);
            encryptedString = Convert.ToBase64String(encryptedValue);
            return encryptedString;
        }

        public static string DecrytedString(string str)
        {
            string decryptedString = "";
            byte[] decryptedValue = Convert.FromBase64String(str);
            decryptedString = System.Text.Encoding.Unicode.GetString(decryptedValue, 0, decryptedValue.Length);
            return decryptedString;
        }
    }
}