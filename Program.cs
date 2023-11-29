/* 2023 Piotr Noga
 * Database Structures project - "Sorting Files" using natural merge method
 * This program is mainly written in English (comments, variables, etc.) but interface is written in Polish.
 */

using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DatabasesStructure
{
    class Program
    {
        static void Main(string[] args)
        {
            //initialization of variables and Menu classes
            int answer;
            string? errorMessage = null;
            Menu mainMenu = new("Witaj w programie sortującym rekordy!", 2, "Przejdź do wyboru sposobu tworzenia rekordów", "Wyjdź z programu");
            Menu recordsCreationMenu = new("Sposób tworzenia rekordów", 4, "Wybór pliku z rekordami", "Wpisanie rekordów z klawiatury", "Generowanie rekordów", "Powróć do głównego menu");
            Menu recordsFromFileMenu = new("Rekordy z pliku", 2, "Wybór pliku", "Powróć do sekcji tworzenia rekordów");
            Menu generatingRecords = new("Generowanie rekordów", 2, "Przejdź do sekcji generowania rekordów", "Powróć do sekcji tworzenia rekordów");

            /*      MAIN MENU       */
            main_menu:
            answer = Menu.serveOption(mainMenu); // in serveOption method there's a while loop to print menu and then to read input from the keyboard
            switch (answer) {
                /*      RECORDS METHOD SELECTION SECTION     */
                case 1: {
                    records_creation:
                    answer = Menu.serveOption(recordsCreationMenu, errorMessage);
                    errorMessage = null;
                    switch (answer) {
                        case 1: {
                            file_section:
                            answer = Menu.serveOption(recordsFromFileMenu, errorMessage);
                            errorMessage = null;
                            switch (answer)
                            {
                                /*      SELECT FILE     */
                                case 1:
                                {
                                    string filePath = Menu.defaultPathAndSelectFile();
                                    if (filePath == "?null")
                                    {
                                        errorMessage = "Nie podano prawidłowej ścieżki! Powracam do sekcji.";
                                        goto file_section;
                                    }
                                    else if (filePath == "?back") {
                                        errorMessage = "Powrócono do poprzedniej sekcji.";
                                        goto file_section;
                                    }
                                    Console.WriteLine("Czy chcesz wczytać plik: {0} ?", filePath);
                                    File file = new File(filePath);
                                    Console.WriteLine(file.getSpecificName(2));
                                    File backup = file.makeBackup();
                                    System.Environment.Exit(0); // placeholder exit
                                    break;
                                }
                                /*      RETURN TO PREVIOUS SECTION (MAIN MENU)      */
                                case 2:
                                {
                                    goto records_creation;
                                }
                            }
                            break;
                        }
                        case 2: {
                            break;
                        }
                        case 3: {
                            answer = Menu.serveOption(generatingRecords);
                            switch (answer) {
                                case 1: {
                                    Menu.generateRecords();
                                    break;   
                                }
                                case 2:{ 
                                    goto records_creation;
                                }
                            }
                            break;
                        }
                        case 4: {
                            goto main_menu;
                        }
                        case -1: {
                            Menu.helpSection(recordsCreationMenu, "Należy wybrać plik, w którym znajdują się zapisane juz rekordy.", "Użytkownik samemu wpisuje rekordy, które będą użyte w dalszej części programu.", "Program automatycznie wygeneruje rekordy.");
                            goto records_creation;
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
                case -1:
                    Menu.helpSection(mainMenu, "Faktyczne uruchomienie aplikacji.");
                    goto main_menu;
            }
        }
    }
}
