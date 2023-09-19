using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;

namespace Dateisystem.BusinessLayer
{
    public static class ConsoleHelper
    {
        //Get path
        public static string? GetInput()
        {
            try
            {
                string? input = Console.ReadLine();

                while (input == "" || input == null || !Directory.Exists(input))
                {
                    GetInput();
                    input = Console.ReadLine();
                }
                return input;
            }
            catch (Exception e)
            {
                Console.WriteLine($"{e.Message} \n {e.InnerException}.");
                return null;
            }
        }

        //Start text
        public static void Start()
        {
            try
            {
                Console.WriteLine("Behold...\n");
            }
            catch (Exception e)
            {
                Console.WriteLine($"{e.Message} \n {e.InnerException}.");
            }
        }

    }
}
