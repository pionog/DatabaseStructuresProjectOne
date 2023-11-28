/*
 Simple sorting program. Functions are written in English but params in the program are written in Polish
 */
using System.IO;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;

namespace StrukturyBazDanych
{
    public class Menu //decorator for titlebar and options
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

        public static void printTitlebar(Menu menu, string? errorMessage = null) {
            int length = menu.title.Length;
            string bars = new('=', length + 6);
            Console.Clear();
            if (!string.IsNullOrEmpty(errorMessage))
            {
                Console.WriteLine(errorMessage);
                Console.WriteLine();
            }
            Console.WriteLine(bars);
            Console.WriteLine("|| " + menu.title + " ||");
            Console.WriteLine(bars);
            Console.WriteLine("");
        }
        public static void printTitlebarAndOptions(Menu menu, string? errorMessage = null) //printing given menu i.e. title bar and options underneath
        {
            printTitlebar(menu, errorMessage);
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
                if (command == "help") {
                    return -1;
                }
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
        public static int serveOption(Menu menu, string? errorMessage = null) //combine of printing title bar and options with reading input from keyboard
        {
            printTitlebarAndOptions(menu, errorMessage);
            return readOption(menu.options);
        }
        public static string defaultPathAndSelectFile() {
            {
                
                string path = AppDomain.CurrentDomain.BaseDirectory;
                Console.Clear();
                Console.WriteLine("Domyślna ścieżka pliku to: ");
                Console.WriteLine(path);
                Console.WriteLine("Czy w niej znajduje się plik? [t/n]");
                bool givenAnswer = false;
                string answer;
                string pathTemplate = @"^[a-zA-Z]:\\[^\/:*?""<>|\r\n]*$";
                while (!givenAnswer)
                {
                    answer = Console.ReadLine();
                    if (answer == "t")
                    {
                        givenAnswer = true;
                    }
                    else if (answer == "n")
                    {
                        givenAnswer = true;
                        Console.WriteLine("Podaj ścieżkę do pliku:");
                        bool czyPoprawnaSciezka = false;
                        while (!czyPoprawnaSciezka)
                        {
                            path = Console.ReadLine();
                            czyPoprawnaSciezka = Regex.IsMatch(path, pathTemplate);
                            if (czyPoprawnaSciezka)
                            {
                                Console.WriteLine("Podana ścieżka to: {0}", path);
                            }
                            else if (path == "back" || path == "exit") {
                                return Constants.BACK_STRING;
                            }
                            else
                            {
                                Console.WriteLine("Podano nieprawidłową ścieżkę! Podaj poprawną:");
                            }
                        }
                    }
                    else
                    {
                        Console.WriteLine("Nieprawidłowa odpowiedź!");
                    }
                }
                string fileTemplate = @"^[^\/:*?""<>|]+$";
                bool czyPoprawnyPlik = false;
                string fileName = "";
                Console.WriteLine("Podaj nazwę pliku:");
                while (!czyPoprawnyPlik)
                {
                    fileName = Console.ReadLine();
                    czyPoprawnyPlik = Regex.IsMatch(fileName, fileTemplate);
                    if (fileName == "back" || fileName == "exit") {
                        return Constants.BACK_STRING;
                    }
                    else if (czyPoprawnyPlik)
                    {
                        Console.WriteLine("Podany plik to: {0}", fileName);
                    }
                    else
                    {
                        Console.WriteLine("Podano nieprawidłową nazwę pliku! Podaj poprawną:");
                    }
                }
                string filePath = Path.Combine(path, fileName);
                bool fileExists = System.IO.File.Exists(filePath);
                if (fileExists)
                {
                    return filePath;
                }
                else {
                    Console.WriteLine("Podany plik nie istnieje!");
                    return Constants.NULL_STRING;
                }
            }
        }
        public static void helpSection(Menu menu, params object[] args) {
            Menu helpMenu = new Menu("HELP FOR: " + menu.title, menu.options, menu.args);
            printTitlebar(helpMenu);
            for (int i = 0; i < menu.options -1; i++) //no need to bring definition of going back to previous section so one option less
            {
                Console.WriteLine("{0} - {1}", menu.args[i], args[i]);
            }
            Console.WriteLine();
            Console.WriteLine("Wciśnij ENTER, by kontynuować.");
            while (!(Console.ReadKey().Key == ConsoleKey.Enter)) { 
                
            }
        }
    }
}