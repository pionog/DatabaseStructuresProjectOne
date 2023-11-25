/*
 Simple sorting program. Functions are written in English but params in the program are written in Polish
 */

public class Menu
{
    public static void Main(string[] args)
    {
        while (true)
        {
            /*      MAIN MENU       */
            int options = 2;
            printTitle("Witaj w programie sortującym rekordy!", options, "Przejdź do sortowania", "Wyjdź z programu");
            int answer = readOption(options);
            /*      SORTING SECTION     */
            if (answer == 1) {
                options = 2;
                printTitle("Sortowanie", options, "coś", "Powróć do poprzedniej sekcji");
                answer = readOption(options);
            }
            /*      EXIT        */
            else if (answer == 2)
            {
                Console.Clear();
                Console.WriteLine("Wychodzenie z programu...");
                System.Environment.Exit(0);
            }
        }
    }

    public static void printTitle(string title, int howManyOptions, params object[] args) //
    {
        Console.Clear();
        Console.WriteLine("\t=====================================");
        Console.WriteLine("||\t" + title + "\t||");
        Console.WriteLine("\t=====================================");
        Console.WriteLine("");
        Console.WriteLine("Wybierz akcję:");
        foreach (string arg in args)
        {
            Console.WriteLine((Array.IndexOf(args, arg)+1) + ". " + arg);
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
            catch (Exception e)
            {
                Console.WriteLine("Nie podano liczby!");
            }
        }
    }
}