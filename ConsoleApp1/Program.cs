using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public class Program
    {
        public static void Main(string[] args)
        {
            string GetSHA1Hash(string input)
            {
                System.Security.Cryptography.SHA1CryptoServiceProvider x = new System.Security.Cryptography.SHA1CryptoServiceProvider();
                //byte[] bs = System.Text.Encoding.ASCII.GetBytes(input);
                //byte[] bs = System.Text.Encoding.UTF7.GetBytes(input);
                byte[] bs = System.Text.Encoding.UTF8.GetBytes(input);
                //byte[] bs = System.Text.Encoding.Unicode.GetBytes(input);
                //byte[] bs = System.Text.Encoding.UTF32.GetBytes(input);
                bs = x.ComputeHash(bs);
                System.Text.StringBuilder s = new System.Text.StringBuilder();
                foreach (byte b in bs)
                {
                    s.Append(b.ToString("x2").ToLower());
                }
                string password = s.ToString();
                return password;
            }

            Console.WriteLine(GetSHA1Hash("ZSJR6F").ToUpper()); 

        }
    }
}
