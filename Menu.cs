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
        public static string defaultPathAndSelectFile() {
            {
                string nullString = "?null";
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
                                return nullString;
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
                        return nullString;
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
                    return nullString;
                }
            }
        }
    }
}