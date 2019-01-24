using System;
using System.Diagnostics;

namespace BooleanParser.Interactive
{
    public class Program
    {
        private static void Main(string[] args)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("Enter a boolean expression to evaluate");

            new Parser("TRUE").Parse();

            while (true)
            {
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write("> ");

                var input = Console.ReadLine();

                try
                {
                    var stopwatch = Stopwatch.StartNew();
                    bool result = new Parser(input.ToUpper()).Parse();
                    stopwatch.Stop();

                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Evaluated to " +
                        $"{result} in {stopwatch.ElapsedMilliseconds}ms " +
                        $"({stopwatch.ElapsedTicks} ticks)");
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
