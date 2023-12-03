/* 2023 Piotr Noga
 * Database Structures project - "Sorting Files" using natural merge method
 * This program is mainly written in English (comments, variables, etc.) but interface is written in Polish.
 */

using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DatabasesStructure
{
    class Program
    {
        public static int diskSaves { get; set; } = 0;
        public static int diskReads { get; set; } = 0;
        public static int phases { get; set; } = 0;

        static void Main(string[] args)
        {
            /*      VARIABLES INITIALIZATION        */
            int answer; //answer handle to use properly menu
            string? errorMessage = null; //handle of error message
            bool selectedRecordsType = false; //prevents use of sort before records method being selected
            bool chosenDirectory = false; //
            string directory = "";
            File file = null; //file handle to the file which contains records

            /*      DEFAULT CONSOLE STYLE       */
            Console.BackgroundColor = ConsoleColor.Black; //make sure host console has black background
            Console.ForegroundColor = ConsoleColor.White; //make sure host console has white text

            /*      MENUS INITIALIZATION        */
            Menu mainMenu = new("Witaj w programie sortującym rekordy!", 3, "Przejdź do wyboru sposobu tworzenia rekordów", "Przejdź do sortowania", "Wyjdź z programu");
            Menu recordsCreationMenu = new("Sposób tworzenia rekordów", 4, "Wybór pliku z rekordami", "Wpisanie rekordów z klawiatury", "Generowanie rekordów", "Powróć do głównego menu");
            Menu recordsFromFileMenu = new("Rekordy z pliku", 3, "Wybór pliku", "Wybór folderu z serią plików", "Powróć do wyboru sposobu tworzenia rekordów");
            Menu generatingRecords = new("Generowanie rekordów", 2, "Przejdź do sekcji generowania rekordów", "Powróć do wyboru sposobu tworzenia rekordów");
            Menu keyboardInput = new("Wprowadzanie danych", 2, "Przejdź do wprowadzania danych z klawiatury", "Powróć do wyboru sposobu tworzenia rekordów");

            /*      MAIN MENU       */
            main_menu:
            answer = Menu.serveOption(mainMenu); // in serveOption method there is a while loop to print menu and then to read input from the keyboard
            switch (answer) {
                /*      RECORDS METHOD SELECTION SECTION     */
                case 1: {
                    records_creation:
                    answer = Menu.serveOption(recordsCreationMenu, errorMessage);
                    errorMessage = null; //reset errorMessage if there was something
                    switch (answer) {
                        /*      FILE SELECTION      */
                        case 1: 
                        {
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
                                    file = new File(filePath);
                                    if (file != null)
                                    {
                                        selectedRecordsType = true;
                                        chosenDirectory = false;
                                    }
                                    goto main_menu;
                                }
                                /*      SELECT DIRECTORY WITH SERIES OF FILES       */        
                                case 2:
                                {
                                    directory = Menu.defaultPathAndSelectFile();
                                    if (directory == "?null")
                                    {
                                        errorMessage = "Nie podano prawidłowej ścieżki! Powracam do sekcji.";
                                        goto file_section;
                                    }
                                    else if (directory == "?back")
                                    {
                                        errorMessage = "Powrócono do poprzedniej sekcji.";
                                        goto file_section;
                                    }
                                    if (System.IO.Directory.Exists(directory))
                                    {
                                        selectedRecordsType = true;
                                        chosenDirectory = true;
                                    }
                                    goto main_menu;
                                }
                                /*      RETURN TO PREVIOUS SECTION (MAIN MENU)      */
                                case 3:
                                {
                                    goto records_creation;
                                }
                            }
                            break;
                        }
                        /*      RECORDS FROM KEYBOARD INPUT     */
                        case 2: 
                        {
                            keyboard_input:
                            answer = Menu.serveOption(keyboardInput, errorMessage);
                            switch (answer) {
                                case 1: {
                                    file = Menu.keyboardInput();
                                    if (file != null)
                                    {
                                        selectedRecordsType = true;
                                    }
                                    Console.WriteLine("Plik z rekordami wygląda następująco: ");
                                    file.print();
                                    Menu.pressEnter();
                                    goto main_menu;
                                }
                                case 2: {
                                    goto records_creation;    
                                }
                                case -1: {
                                    Menu.helpSection(keyboardInput, "Umożliwia wprowadzenie rekordów za pomocą klawiatury. Użytkownik musi wpisać, ile rekordów chce utworzyć, ile chce wypełnić samemu oraz podaje poszczególne rekordy.");
                                    goto keyboard_input;
                                }
                            }
                            break;
                        }
                        /*      RECORDS GENERATION      */
                        case 3: 
                        {
                            generate_records:
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
                                case -1: {
                                    Menu.helpSection(generatingRecords, "Należy podać, ile rekordów ma zostać wygenerowanych");
                                    goto generate_records;
                                }
                            }
                            break;
                        }
                        /*      RETURN TO PREVIOUS SECTION      */
                        case 4: 
                        {
                            goto main_menu;
                        }
                        /*      HELP SECTION        */
                        case -1: 
                        {
                            Menu.helpSection(recordsCreationMenu, "Należy wybrać plik, w którym znajdują się już zapisane rekordy.", "Użytkownik samemu wpisuje rekordy, które będą użyte w dalszej części programu.", "Program automatycznie wygeneruje rekordy za użytkownika.");
                            goto records_creation;
                        }
                    }
                    break;
                }
                /*      SORTING SECTION     */
                case 2: 
                {
                        if (!selectedRecordsType)
                        {
                            Console.Clear();
                            Console.WriteLine("Nie możesz przejść do sortowania, dopóki nie wybierzesz sposobu tworzenia rekordów!");
                            Menu.pressEnter();
                        }
                        else
                        {
                            bool parsed = false;
                            bool viewPhases = false;
                            
                            Console.Clear();
                            Console.Write("Czy chcesz wyświetlać statystyki co każdą fazę? [");
                            colorText("t", ConsoleColor.Green, endl: false);
                            Console.Write("/");
                            colorText("n", ConsoleColor.Red, endl: false);
                            Console.Write("]\n");
                            while (!parsed)
                            {
                                string view = Console.ReadLine();
                                if (view == "t")
                                {
                                    parsed = true;
                                    viewPhases = true;
                                }
                                else if (view == "n")
                                {
                                    parsed = true;
                                    viewPhases = false;
                                }
                                else
                                {
                                    Console.Write("Nie uzyskano poprawnej odpowiedzi. Oczekiwano albo \"");
                                    colorText("t", ConsoleColor.Green, endl: false);
                                    Console.Write("\", albo \"");
                                    colorText("n", ConsoleColor.Red, endl: false);
                                    Console.Write("\"\n");
                                }
                            }
                            Console.WriteLine("Plik źródłowy przed sortowaniem wygląda następująco:");
                            file.print();
                            Menu.pressEnter();
                            File copyFile = file.makeCopy("result");
                            bool isNotSortedYet = true;
                            Console.Clear();
                            Program.diskReads = 0;
                            Program.diskSaves = 0;
                            Program.phases = 0;
                            int previousDiskReads = 0;
                            int previousDiskSaves = 0;
                            
                            while (isNotSortedYet)
                            {
                                Program.phases++;
                                Program.split(copyFile, viewPhases);
                                isNotSortedYet = Program.sort(copyFile, viewPhases);
                                if (viewPhases)
                                {
                                    
                                    Console.Clear();
                                    Menu.printTitlebar("Statystyki procesu sortowania");
                                    Console.Write("Odczytów z dysku: ");
                                    colorText((Program.diskReads - previousDiskReads).ToString(), ConsoleColor.Green);
                                    Console.Write("Zapisów na dysku: ");
                                    colorText((Program.diskSaves - previousDiskSaves).ToString(), ConsoleColor.Green);
                                    Console.Write("Liczba faz: ");
                                    colorText(Program.phases.ToString(), ConsoleColor.Green);
                                    previousDiskReads = Program.diskReads;
                                    previousDiskSaves = Program.diskSaves;
                                    Menu.pressEnter();
                                }
                            }
                            Console.Clear();
                            Console.WriteLine("Pomyślnie zakończono proces sortowania!");
                            Menu.pressEnter();
                            Console.Clear();
                            Menu.printTitlebar("Ostateczny wygląd posortowanego pliku");
                            copyFile.print();
                            Console.WriteLine();
                            Console.Write("Wykonano następującą liczbę zapisów na dysku: "); colorText(Program.diskSaves.ToString(), ConsoleColor.Green);
                            Console.Write("Wykonano następującą liczbę odczytów na dysku: "); colorText(Program.diskReads.ToString(), ConsoleColor.Green);
                            Console.Write("Sortowanie potrzebowało następującą liczbę faz: "); colorText(Program.phases--.ToString(), ConsoleColor.Green);
                            Menu.pressEnter();
                            System.IO.File.Delete(copyFile.path);
                            string fileA = file.getSpecificName(1) + System.IO.Path.DirectorySeparatorChar + file.getSpecificName(0) + "-resultA" + file.getSpecificName(2);
                            System.IO.File.Delete(fileA);
                            string fileB = file.getSpecificName(1) + System.IO.Path.DirectorySeparatorChar + file.getSpecificName(0) + "-resultB" + file.getSpecificName(2);
                            System.IO.File.Delete(fileB);

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
                    Menu.helpSection(mainMenu, "Wybór sposobu generowania rekordów, które będą sortowane", "Uruchomienie sortowania pliku, w którym znajdują się rekordy.");
                    goto main_menu;
            }
        }

        public static void split(File file, bool view = false) {
            /*      INITIALISING FILES TO TAPES      */
            //File file is input in this case
            //both tapes use the same directory + have same name (with right letter attached) as input file
            File outputA = new(file.getSpecificName(1) + System.IO.Path.DirectorySeparatorChar + file.getSpecificName(0) + "A" + file.getSpecificName(2));
            File outputB = new(file.getSpecificName(1) + System.IO.Path.DirectorySeparatorChar + file.getSpecificName(0) + "B" + file.getSpecificName(2));
            using (FileStream fs = System.IO.File.Open(outputA.path, FileMode.OpenOrCreate)) //open file to erase content of file
            {
                fs.SetLength(0); //erase content of file by setting its size to zero
            }
            using (FileStream fs = System.IO.File.Open(outputB.path, FileMode.OpenOrCreate)) //open file to erase content of file
            {
                fs.SetLength(0); //erase content of file by setting its size to zero
            }

            /*      INITIALISING TAPES      */
            Tape outputTape = new Tape(file, true);
            Tape tapeA = new Tape(outputA, false);
            Tape tapeB = new Tape(outputB, false);

            /*      INITIALISING VARIABLES      */
            Record? record; // record handle
            bool flushed = false;
            double previousGeometricMean = 0;
            double currentGeometricMean = 0;
            bool writeTapeA = true;

            while (true)
            {
                record = outputTape.getNextRecord();
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
                    if ((tapeA.index == tapeA.bufferSize) && view) 
                    {
                        Console.WriteLine("Taśma A ma pełny bufor! Wygląda następująco:");
                    }
                    tapeA.saveRecord(record, view);
                }
                else 
                {
                    if ((tapeB.index == tapeB.bufferSize) && view)
                    {
                        Console.WriteLine("Taśma B ma pełny bufor! Wygląda następująco:");
                    }
                    tapeB.saveRecord(record, view);
                }
                previousGeometricMean = currentGeometricMean;
            }
            //end of loop. there are probably few records stored in buffers yet. if so flush them
            if (tapeA.buffer[0] != null && tapeB.buffer[0] != null) {
                //debug
                if (tapeA.index > tapeB.index)
                {
                    if (view)
                    {
                        Console.WriteLine("Taśma A zawiera:");
                    }
                    tapeA.flushTape(view);
                    if (view)
                    {
                        Console.WriteLine("Taśma B zawiera:");
                    }
                    tapeB.flushTape(view);
                    flushed = true;
                }
                else if (tapeA.index < tapeB.index)
                {
                    if (view)
                    {
                        Console.WriteLine("Taśma B zawiera:");
                    }
                    tapeB.flushTape(view);
                    if (view)
                    {
                        Console.WriteLine("Taśma A zawiera:");
                    }
                    tapeA.flushTape(view);
                    flushed = true;
                }
                else {
                    if (tapeA.buffer[0].geometricMean() > tapeB.buffer[0].geometricMean())
                    {
                        if (view)
                        {
                            Console.WriteLine("Taśma A zawiera:");
                        }
                        tapeA.flushTape(view);
                        if (view) { 
                            Console.WriteLine("Taśma B zawiera:");
                        }
                        tapeB.flushTape(view);
                        flushed = true;
                    }
                    else {
                        if (view)
                        {
                            Console.WriteLine("Taśma B zawiera:");
                        }
                        tapeB.flushTape(view);
                        if (view)
                        {
                            Console.WriteLine("Taśma A zawiera:");
                        }
                        tapeA.flushTape(view);
                        
                        flushed = true;
                    }
                }
            }
            else if (tapeA.buffer[0] != null)
            {
                if (view)
                {
                    Console.WriteLine("Taśma A zawiera:");
                }
                tapeA.flushTape(view);
                flushed = true;
            }
            else if (tapeB.buffer[0] != null)
            {
                if (view)
                {
                    Console.WriteLine("Taśma B zawiera:");
                }
                tapeB.flushTape(view);
                flushed = true;
            }
            if (view)
            {
                Menu.pressEnter();
            }
        }

        public static bool sort(File file, bool view = false) {
            
            /*      INITIALISING FILES TO TAPES      */
            //File file is output in this case
            //both tapes use the same directory + have same name (with right letter attached) as output file
            File outputA = new(file.getSpecificName(1) + System.IO.Path.DirectorySeparatorChar + file.getSpecificName(0) + "A" + file.getSpecificName(2));
            File outputB = new(file.getSpecificName(1) + System.IO.Path.DirectorySeparatorChar + file.getSpecificName(0) + "B" + file.getSpecificName(2));


            /*      INITIALISING TAPES      */
            Tape outputTape = new Tape(file, false);
            Tape tapeA = new Tape(outputA, true);
            Tape tapeB = new Tape(outputB, true);

            /*      INITIALISING VARIABLES      */
            Record? recordA = tapeA.getNextRecord(); //record from tape A handle
            Record? recordB = tapeB.getNextRecord(); //record from tape B handle
            double previousRecordAGeometricMean = 0;
            double previousRecordBGeometricMean = 0;
            double recordAGeometricMean = 0;
            double recordBGeometricMean = 0;

            //if there is no records on tape B then tape A has sorted all records
            if (recordB == null) 
            {
                return false;
            }
            /*      ERASING CONTENT OF OUTPUT FILE      */
            using (FileStream fs = System.IO.File.Open(file.path, FileMode.Open)) //open file because append cannot allow to do instruction below
            {
                fs.SetLength(0); //erase content of file by setting its size to zero
            }
            while (true) { 
                if ((recordA != null) && (recordB != null)) //both tapes have some records to read
                {
                    recordAGeometricMean = recordA.geometricMean();
                    recordBGeometricMean = recordB.geometricMean();
                    if (recordAGeometricMean < previousRecordAGeometricMean)
                    {
                        //if current geometric mean from tape A is smaller than previous one from this tape, then we have to change to tape B
                        while ((recordB != null) && (recordBGeometricMean > previousRecordBGeometricMean))
                        {
                            outputTape.saveRecord(recordB, view);
                            previousRecordBGeometricMean = recordBGeometricMean;
                            recordB = tapeB.getNextRecord();
                        }
                        previousRecordAGeometricMean = 0;
                        previousRecordBGeometricMean = 0;
                    }
                    else if (recordBGeometricMean < previousRecordBGeometricMean)
                    {
                        //if current geometric mean from tape B is smaller than previous one from this tape, then we have to change to tape A
                        while ((recordA != null) && (recordAGeometricMean > previousRecordAGeometricMean))
                        {
                            outputTape.saveRecord(recordA, view);
                            previousRecordAGeometricMean = recordAGeometricMean;
                            recordA = tapeA.getNextRecord();
                        }
                        previousRecordAGeometricMean = 0;
                        previousRecordBGeometricMean = 0;
                    }
                    else
                    {
                        if (recordAGeometricMean < recordBGeometricMean)
                        {
                            outputTape.saveRecord(recordA, view);
                            previousRecordAGeometricMean = recordAGeometricMean;
                            recordA = tapeA.getNextRecord();
                        }
                        else
                        {
                            outputTape.saveRecord(recordB, view);
                            previousRecordBGeometricMean = recordBGeometricMean;
                            recordB = tapeB.getNextRecord();
                        }
                    }
                }
                // jezeli rekordy z tasmy A zostaly juz zapisane na tasme wynikowa to przepisujemy reszte rekordow z tasmy B
                else if (recordA == null)
                {
                    while (recordB != null)
                    {
                        outputTape.saveRecord(recordB, view);
                        recordB = tapeB.getNextRecord();
                    }
                    break;
                }
                // jezeli rekordy z tasmy B zostaly juz zapisane na tasme wynikowa to przepisujemy reszte rekordow z tasmy A
                else if (recordB == null)
                {
                    while (recordA != null)
                    {
                        outputTape.saveRecord(recordA, view);
                        recordA = tapeA.getNextRecord();
                    }
                    break;
                }
            }
            //after while loop there is still could some records in a buffer. it is needed to flush them to the file
            if (outputTape.buffer[0] != null)
            {
                outputTape.flushTape(view);
            }

            if (view)
            {
                Console.Clear();
                Console.WriteLine("Taśma A:");
                tapeA.file.print();
                Console.WriteLine();
                Console.WriteLine("Taśma B:");
                tapeB.file.print();
                Console.WriteLine();
                Console.WriteLine("Taśma output:");
                outputTape.file.print();
                Menu.pressEnter();
                Console.Clear();
            }
            return true;
        }

        //function to change color of the text in a console
        public static void colorText(string text, ConsoleColor color, ConsoleColor restoredColor = ConsoleColor.White, bool endl = true) {
            Console.ForegroundColor = color;
            Console.Write(text);
            if (endl) Console.Write("\n");
            Console.ForegroundColor = restoredColor;
        }
    }
}
