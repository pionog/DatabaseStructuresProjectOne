using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace StrukturyBazDanych
{
    class Program
    {
        static void Main(string[] args)
        {
            //initialization of variables and Menu classes
            int answer;
            Menu mainMenu = new("Witaj w programie sortującym rekordy!", 2, "Przejdź do sortowania", "Wyjdź z programu");
            Menu sortingMenu = new("Sortowanie", 2, "coś", "Powróć do poprzedniej sekcji");

            /*      MAIN MENU       */
            main_menu:
            answer = Menu.serveOption(mainMenu); // in serveOption method there's a while loop to print menu and then to read input from the keyboard
            switch (answer) {
                /*      SORTING SECTION     */
                case 1: {
                    sorting_section:
                    answer = Menu.serveOption(sortingMenu);
                    switch (answer) {
                        case 1: {
                            System.Environment.Exit(0); // placeholder exit
                            break;
                        }
                        /*      RETURN TO PREVIOUS SECTION (MAIN MENU)      */
                        case 2: {
                            goto main_menu;
                        }
                    }
                    break;
                }
                /*      EXIT        */
                case 2: {
                    Console.Clear();
                    Console.WriteLine("Wychodzenie z programu...");
                    System.Environment.Exit(0);
                    break;
                }
            }
        }
    }
}
