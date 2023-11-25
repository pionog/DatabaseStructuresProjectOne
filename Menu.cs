/*
 Simple sorting program. Functions are written in English but params in the program are written in Polish
 */
using System.Runtime.CompilerServices;

namespace StrukturyBazDanych
{
    public class Menu
    {
        private string title { get; set; } = string.Empty;
        private int options { get; set; }
        private object[] args { get; set; }

        public Menu(string title = "Generic title", int options = 1, params object[] args)
        {
            this.title = title;
            this.options = options;
            this.args = args;
        }

        public static void printTitle(Menu menu) //
        {
            Console.Clear();
            Console.WriteLine("\t=====================================");
            Console.WriteLine("||\t" + menu.title + "\t||");
            Console.WriteLine("\t=====================================");
            Console.WriteLine("");
            Console.WriteLine("Wybierz akcję:");
            object[] args = menu.args;
            foreach (string arg in args)
            {
                Console.WriteLine((Array.IndexOf(args, arg) + 1) + ". " + arg);
            }
        }
        public static int readOption(int howManyOptions)
        {
            while (true)
            {
                string? command = Console.ReadLine();
                try
                {
                    int number = Int32.Parse(command);

                    if (number < 1 || number > howManyOptions)
                    {
                        Console.WriteLine("Podano błędny numer. Proszę wpisać poprawną opcję.");
                    }
                    else
                    {
                        return number;
                    }
                }
                catch
                {
                    Console.WriteLine("Nie podano liczby!");
                }
            }
        }
        public static int serveOption(Menu menu) {
            printTitle(menu);
            return readOption(menu.options);
        }
    }
}