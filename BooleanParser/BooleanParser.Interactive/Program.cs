using System;

namespace BooleanParser.Interactive
{
    public class Program
    {
        private static void Main(string[] args)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("Enter a boolean expression to evaluate");

            while (true)
            {
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write("> ");

                var input = Console.ReadLine();

                try
                {
                    bool result = new Parser(input.ToUpper()).Parse();

                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine(result);
                }
                catch (Exception e)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(e.Message);
                }
            }
        }
    }
}
