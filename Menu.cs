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

        public static void printTitle(Menu menu) //printing given menu i.e. title bar and options underneath
        {
            int length = menu.title.Length;
            string bars = new('=', length+6);
            Console.Clear();
            Console.WriteLine(bars);
            Console.WriteLine("|| " + menu.title + " ||");
            Console.WriteLine(bars);
            Console.WriteLine("");
            Console.WriteLine("Wybierz akcję:");
            object[] args = menu.args;
            foreach (string arg in args)
            {
                Console.WriteLine((Array.IndexOf(args, arg) + 1) + ". " + arg);
            }
        }
        public static int readOption(int howManyOptions) //reading input from the keyboard so user can select desirable option
        {
            while (true)
            {
                string? command = Console.ReadLine(); //read input
                try
                {
                    int number = Int32.Parse(command); //try to parse string to integer

                    if (number < 1 || number > howManyOptions) //if answer does not correspond to any of options then users hould write the correct one
                    {
                        Console.WriteLine("Podano błędny numer. Proszę wpisać poprawną opcję.");
                    }
                    else //answer is fits in the range of options
                    {
                        return number;
                    }
                }
                catch //user does not type number correctly
                {
                    Console.WriteLine("Nie podano liczby!");
                }
            }
        }
        public static int serveOption(Menu menu) //combine of printing title bar and options with reading input from keyboard
        { 
            printTitle(menu);
            return readOption(menu.options);
        }
    }
}