/* 2023 Piotr Noga
 * Database Structures project - "Sorting Files" using natural merge method
 * This program is mainly written in English (comments, variables, etc.) but interface is written in Polish.
 */

using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DatabasesStructure
{
    class Program
    {
        public static int diskSaves { get; set; } = 0;
        public static int diskReads { get; set; } = 0;

        static void Main(string[] args)
        {
            //initialization of variables and Menu classes
            int answer; //answer handle to use properly menu
            string? errorMessage = null; //handle of error message
            bool selectedRecordsType = false; //prevents use of sort before records method being selected
            File file = null; //file handle to the file which contains records
            Menu mainMenu = new("Witaj w programie sortującym rekordy!", 3, "Przejdź do wyboru sposobu tworzenia rekordów", "Przejdź do sortowania", "Wyjdź z programu");
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
                        /*      FILE SELECTION      */
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
                                    file = new File(filePath);
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
                        /*      RECORDS FROM KEYBOARD INPUT     */
                        case 2: {
                            break;
                        }
                        /*      RECORDS GENERATION      */
                        case 3: {
                            answer = Menu.serveOption(generatingRecords);
                            switch (answer) {
                                case 1: {
                                    file = Menu.generateRecords();
                                    if (file != null) {
                                        selectedRecordsType = true;
                                    }
                                    goto main_menu;
                                }
                                case 2:{ 
                                    goto records_creation;
                                }
                            }
                            break;
                        }
                        /*      RETURN TO PREVIOUS SECTION      */
                        case 4: {
                            goto main_menu;
                        }
                        /*      HELP SECTION        */
                        case -1: {
                            Menu.helpSection(recordsCreationMenu, "Należy wybrać plik, w którym znajdują się zapisane juz rekordy.", "Użytkownik samemu wpisuje rekordy, które będą użyte w dalszej części programu.", "Program automatycznie wygeneruje rekordy.");
                            goto records_creation;
                        }
                    }
                    break;
                }
                /*      SORTING SECTION     */
                case 2: {
                    if (!selectedRecordsType)
                    {
                        Console.Clear();
                        Console.WriteLine("Nie możesz przejść do sortowania, dopóki nie wybierzesz sposobu tworzenia rekordów!");
                        Menu.clickEnter();
                    }
                    else {
                            Program.split(file);
                    }
                    goto main_menu;
                }
                /*      EXIT        */
                case 3: {
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

        public static void split(File file) {
            /*      INITIALISING FILES TO TAPES      */
            //File file is input in this case
            //both tapes use the same directory + have same name (with right letter attached) as input file
            File outputA = new(file.getSpecificName(1) + System.IO.Path.DirectorySeparatorChar + file.getSpecificName(0) + "A" + file.getSpecificName(2));
            File outputB = new(file.getSpecificName(1) + System.IO.Path.DirectorySeparatorChar + file.getSpecificName(0) + "B" + file.getSpecificName(2));
            if (System.IO.File.Exists(outputA.path)) //make sure that tape file does not exist
            { 
                System.IO.File.Delete(outputA.path);
            }
            if (System.IO.File.Exists(outputB.path)) //make sure that tape file does not exist
            {
                System.IO.File.Delete(outputB.path);
            }

            /*      INITIALISING TAPES      */
            Tape inputTape = new Tape(file, true);
            Tape tapeA = new Tape(outputA, false);
            Tape tapeB = new Tape(outputB, false);

            /*      INITIALISING VARIABLES      */
            Record? record; // record handle
            double previousGeometricMean = 0;
            double currentGeometricMean = 0;
            bool writeTapeA = true;

            while (true)
            {
                record = inputTape.getNextRecord();
                if(record == null)
                {
                    break;
                }
                currentGeometricMean = record.geometricMean();
                if (previousGeometricMean > currentGeometricMean) {
                    if (writeTapeA)
                    {
                        writeTapeA = false;
                    }
                    else {
                        writeTapeA = true;
                    }
                }
                if (writeTapeA)
                {
                    tapeA.saveRecord(record);
                }
                else { 
                    tapeB.saveRecord(record);
                }
                previousGeometricMean = currentGeometricMean;
            }
            Console.WriteLine("Taśma A zawiera:");
            tapeA.flushTape();
            Console.WriteLine("Taśma B zawiera:");
            tapeB.flushTape();
            Menu.clickEnter();
        }
    }
}
