/*
 Simple sorting program. Functions are written in English but params in the program are written in Polish
 */
using Microsoft.VisualBasic.FileIO;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;

namespace DatabasesStructure
{
    public class Menu //class made to store more complicated menus
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

        /*      BASIC OPERATIONS        */
        public static void printTitlebar(string title, string? errorMessage = null) //prints title bar in a frame
        {
            int length = title.Length;
            string bars = new('=', length + 6);
            Console.Clear();
            if (!string.IsNullOrEmpty(errorMessage))
            {
                Console.WriteLine(errorMessage);
                Console.WriteLine();
            }
            Console.WriteLine(bars);
            Console.WriteLine("|| " + title + " ||");
            Console.WriteLine(bars);
            Console.WriteLine("");
        }
        public static void printTitlebarAndOptions(Menu menu, string? errorMessage = null) //printing given menu i.e. title bar and options underneath
        {
            printTitlebar(menu.title, errorMessage);
            Console.WriteLine("Wybierz akcję:");
            object[] args = menu.args;
            foreach (string arg in args)
            {
                Console.WriteLine((Array.IndexOf(args, arg) + 1) + ". " + arg);
            }
        }
        public static void pressEnter() //press ENTER to continue in given section
        {
            Console.WriteLine();
            Console.WriteLine("Wciśnij ENTER, by kontynuować.");
            while (!(Console.ReadKey().Key == ConsoleKey.Enter))
            {

            }
        }

        /*      BASIC MENU     */
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
        
        /*      ADVANCED MENUS       */
        public static string defaultPathAndSelectFile() //handle file path selection
        {
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
        public static void helpSection(Menu menu, params object[] args) //handle help section
        {
            printTitlebar(menu.title);
            for (int i = 0; i < menu.options -1; i++) //no need to bring definition of going back to previous section so one option less
            {
                Console.WriteLine("{0} - {1}", menu.args[i], args[i]);
            }
            pressEnter();
        }
        public static File generateRecords() {
            printTitlebar("Generowanie rekordów"); // titlebar
            Console.WriteLine("Proszę wpisać ile rekordów ma zostać wygenerowanych:"); //ask user to type number of records
            bool parsed = false;
            int howMany = 0;
            while (!parsed) //loops until user gives valid value
            {
                string? input = Console.ReadLine();
                
                parsed = Int32.TryParse(input, out howMany);
                if (!parsed)
                {
                    Console.WriteLine("Podaj liczbę!");
                }
                else if (howMany <= 0) {
                    Console.WriteLine("Liczba musi być dodatnia!");
                    parsed = false;
                }
            }
            string path = AppDomain.CurrentDomain.BaseDirectory;
            path += "test.txt";
            System.IO.File.Delete(path);
            Record[] records = new Record[howMany];
            Console.WriteLine();
            Console.WriteLine("Wygenerowano następujące rekordy:");
            for(int i = 0;i < howMany;i++)
            {
                records[i] = new Record();
                Console.WriteLine(records[i].ToString());
            }
            pressEnter();
            /* testowanie zapisu i otwierania pliku*/
            using (var stream = System.IO.File.Open(path, FileMode.OpenOrCreate))
            {
                using (var writer = new BinaryWriter(stream, Encoding.UTF8, false))
                {
                    for (int j = 0; j < howMany; j++)
                    {
                        Record recordFromBuffer = records[j]; //taking single record containing NUMBERS_IN_RECORD numbers
                        for (int i = 0; i < Constants.NUMBERS_IN_RECORD; i++)
                        {
                            writer.Write(recordFromBuffer.data[i]);
                        }
                        writer.Flush();
                    }

                }
            }
            Console.WriteLine("Pomyślnie zapisano rekordy do pliku");
            return new File(path);
            /*Console.WriteLine("Próba odczytu z pliku:");
            bool eof = false;
            howMany = 0;
            using (var stream = System.IO.File.Open(path, FileMode.Open))
            {
                using (var reader = new BinaryReader(stream, Encoding.UTF8, false))
                {

                    while (!eof)
                    {
                        Record recordFromFile = new(new double[Constants.NUMBERS_IN_RECORD]);
                        for (int i = 0; (i < Constants.NUMBERS_IN_RECORD) && !eof; ++i)
                        {
                            try //it prevents from reading too far
                            {
                                recordFromFile.data[i] = reader.ReadDouble();
                            }
                            catch //if binaryReader could not read data, then it was the end of file
                            {
                                eof = true;
                                break;
                            }
                        }
                        if (!eof)
                        {
                            howMany++;
                            Console.WriteLine(howMany.ToString() + recordFromFile.ToString());
                        }
                    }
                }
            }*/
            System.Environment.Exit(0);
        }
    }
}