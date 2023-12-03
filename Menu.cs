/*
 Simple sorting program. Functions are written in English but params in the program are written in Polish
 */
using Microsoft.VisualBasic.FileIO;
using System.IO;
using System.Net.NetworkInformation;
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
                Program.colorText(errorMessage, ConsoleColor.Red);
                Console.WriteLine();
            }
            Program.colorText(bars, ConsoleColor.Yellow);
            Program.colorText("||", ConsoleColor.Yellow, endl: false);
            Program.colorText(" " + title + " ", ConsoleColor.DarkRed, endl: false);
            Program.colorText("||", ConsoleColor.Yellow);
            Program.colorText(bars, ConsoleColor.Yellow);
            Console.WriteLine("");
        }
        public static void printTitlebarAndOptions(Menu menu, string? errorMessage = null) //printing given menu i.e. title bar and options underneath
        {
            printTitlebar(menu.title, errorMessage);
            Console.WriteLine("Wybierz akcję:");
            object[] args = menu.args;
            foreach (string arg in args)
            {
                Program.colorText((Array.IndexOf(args, arg) + 1).ToString(), ConsoleColor.Blue, endl: false);
                Console.WriteLine(". " + arg);
            }
        }
        public static void pressEnter() //press ENTER to continue in given section
        {
            Console.WriteLine();
            Console.Write("Wciśnij ");
            Program.colorText("ENTER", ConsoleColor.Red, endl: false);
            Console.Write(", by kontynuować.\n");
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
                Console.Write("Czy w niej znajduje się plik? [");
                Program.colorText("t", ConsoleColor.Green, endl: false);
                Console.Write("/");
                Program.colorText("n", ConsoleColor.Red, endl: false);
                Console.Write("]\n");
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
                        bool isGivenPathValid = false;
                        while (!isGivenPathValid)
                        {
                            path = Console.ReadLine();
                            isGivenPathValid = Regex.IsMatch(path, pathTemplate);
                            if (isGivenPathValid)
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
                bool isFilenameValid = false;
                string fileName = "";
                Console.WriteLine("Podaj nazwę pliku:");
                while (!isFilenameValid)
                {
                    fileName = Console.ReadLine();
                    isFilenameValid = Regex.IsMatch(fileName, fileTemplate);
                    if (fileName == "back" || fileName == "exit") {
                        return Constants.BACK_STRING;
                    }
                    else if (isFilenameValid)
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

        public static string selectPath() //handle file path selection
        {
            {

                string path = AppDomain.CurrentDomain.BaseDirectory;
                Console.Clear();
                Console.WriteLine("Domyślna ścieżka pliku to: ");
                Console.WriteLine(path);
                Console.Write("Czy w niej znajduje się katalog? [");
                Program.colorText("t", ConsoleColor.Green, endl: false);
                Console.Write("/");
                Program.colorText("n", ConsoleColor.Red, endl: false);
                Console.Write("]\n");
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
                        Console.WriteLine("Podaj ścieżkę do rodzica katalogu:");
                        bool isGivenPathValid = false;
                        while (!isGivenPathValid)
                        {
                            path = Console.ReadLine();
                            isGivenPathValid = Regex.IsMatch(path, pathTemplate);
                            if (isGivenPathValid)
                            {
                                Console.WriteLine("Podana ścieżka to: {0}", path);
                            }
                            else if (path == "back" || path == "exit")
                            {
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
                bool isPathValid = false;
                string directory = "";
                string folderRegex = @"^[^\/:*?""<>|]+$";
                Console.WriteLine("Podaj nazwę katalogu:");
                while (!isPathValid)
                {
                    directory = Console.ReadLine();
                    isPathValid = Regex.IsMatch(directory, folderRegex);
                    if (directory == "back" || directory == "exit")
                    {
                        return Constants.BACK_STRING;
                    }
                    else if (isPathValid)
                    {
                        Console.WriteLine("Podany katalog to: {0}", directory);
                    }
                    else
                    {
                        Console.WriteLine("Podano nieprawidłową nazwę katalogu! Podaj poprawną:");
                    }
                }
                string directoryPath = Path.Combine(path, directory);
                bool pathExists = System.IO.Directory.Exists(directoryPath);
                if (pathExists)
                {
                    return directoryPath;
                }
                else
                {
                    Console.WriteLine("Podany katalog nie istnieje!");
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
        public static File generateRecords(string fileName = "test.txt") {
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
            path += fileName;
            System.IO.File.Delete(path);
            Record[] records = new Record[howMany];
            Console.WriteLine();
            Console.WriteLine("Wygenerowano następujące rekordy:");
            for(int i = 0;i < howMany;i++)
            {
                records[i] = new Record();
                Console.WriteLine(Constants.LIST_ELEMENT + records[i].ToString());
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
        }

        public static File keyboardInput(string fileName = "test.txt") {
            printTitlebar("Kreator rekordów");
            Console.WriteLine("Zapraszamy w kreatorze rekordów!");
            Console.WriteLine("Proszę podać ile rekordów łącznie ma zostać utworzonych: ");
            bool parsed = false;
            string howMany;
            string doubleString;
            double number = 0.0;
            int howManyRecords = 0;
            int howManyUsersRecords = 0;
            while (!parsed)
            {
                howMany = Console.ReadLine();
                parsed = Int32.TryParse(howMany, out howManyRecords);
            }
            parsed = false;
            Console.Write("Czy wszystkie rekordy będą wypełnione przez użytkownika? ["); 
            Program.colorText("t", ConsoleColor.Green, endl: false);
            Console.Write("/");
            Program.colorText("n", ConsoleColor.Red, endl: false);
            Console.Write("]\n");
            string givenAnswer = String.Empty;
            while (!parsed)
            {
                givenAnswer = Console.ReadLine();
                if (givenAnswer == "t")
                {
                    howManyUsersRecords = howManyRecords;
                    parsed = true;
                }
                else if (givenAnswer == "n")
                {
                    Console.WriteLine("Proszę zatem podać, ile rekordów będzie podanych:");
                    while (!parsed)
                    {
                        howMany = Console.ReadLine();
                        parsed = Int32.TryParse(howMany, out howManyUsersRecords);
                        if (howManyUsersRecords < 0) {
                            Console.WriteLine("Nie można podać liczby ujemnej!");
                            parsed = false;
                        }
                        if (howManyUsersRecords > howManyRecords) { 
                            Console.WriteLine("Nie można wpisać więcej rekordów, niż się to wcześniej zadeklarowało!");
                            parsed = false;
                        }
                    }
                }
                else
                {
                    Console.Write("Nie uzyskano odpowiedniej odpowiedzi. Należy odpowiedzieć \"");
                    Program.colorText("t", ConsoleColor.Green, endl: false);
                    Console.Write("\" lub \"");
                    Program.colorText("n", ConsoleColor.Red, endl: false);
                    Console.Write("\"\n");
                }
            }
            string path = AppDomain.CurrentDomain.BaseDirectory;
            path += fileName;
            System.IO.File.Delete(path);
            Console.Clear();
            Console.WriteLine("Rozpoczęto procedurę wpisywania rekordów!");
            pressEnter();
            using (var stream = System.IO.File.Open(path, FileMode.OpenOrCreate))
            {
                using (var writer = new BinaryWriter(stream, Encoding.UTF8, false))
                {
                    for (int j = 0; j < howManyUsersRecords; j++)
                    {
                        Record record = new Record();
                        Console.Clear();
                        Console.WriteLine($"Podaj {j}. rekord:");
                        Record recordFromBuffer = new();
                        for (int i = 0; i < Constants.NUMBERS_IN_RECORD; i++)
                        {
                            Console.WriteLine($"Podaj {i}. liczbę rekordu:");
                            parsed = false;
                            while (!parsed) {
                                doubleString = Console.ReadLine();
                                parsed = Double.TryParse(doubleString, out number);
                                if (!parsed) {
                                    Console.WriteLine("Nie podano liczby!");
                                }
                                if (number <= 0) {
                                    parsed = false;
                                    Console.WriteLine("Podaj dodatnią liczbę!");
                                }
                            }
                            writer.Write(number);
                            record.data[i] = number;
                        }
                        writer.Flush();
                        Console.WriteLine(j + "." + record.ToString());
                    }
                    Console.WriteLine();
                    for (int j = howManyUsersRecords; j < howManyRecords; j++) {
                        Record record = new();
                        for (int i = 0; i < Constants.NUMBERS_IN_RECORD; i++)
                        {
                            writer.Write(record.data[i]);
                        }
                        writer.Flush();
                    }

                }
            }
            return new File(path);
        }
    }
}