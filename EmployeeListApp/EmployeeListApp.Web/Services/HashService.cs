using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeListApp.WEB
{
    public static class HashService
    {
        public static byte[] GetHash(string value)
        {
            var tmpSource = ASCIIEncoding.ASCII.GetBytes(value);
            return new MD5CryptoServiceProvider().ComputeHash(tmpSource);
        }

        public static bool HashCompare(byte[] hash_1, byte[] hash_2)
        {
            bool isEqual = false;
            if (hash_1.Length == hash_2.Length)
            {
                int i = 0;
                while ((i < hash_1.Length) && (hash_1[i] == hash_2[i]))
                {
                    i += 1;
                }
                if (i == hash_1.Length)
                {
                    isEqual = true;
                }
            }
            return isEqual;
        }
    }
}
