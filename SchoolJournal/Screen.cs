namespace SchoolJournal
{
    public class Screen : Tools
    {
        private const string folder = "SchoolJournal";
        public Screen()
        {

        }

        private const string initialMessagePU =
                            "\t===========================================\n" +
                            "\t       Witamy w dzienniku szkolnym\n" +
                            "\t===========================================\n";
        private const string initialMessageE =
                            "\tZakres oceny ucznia to 6-1 lub\n" +
                            "\tA-6, B-5, C-4, D-3, E-2, F-0\n";
        public void MainHeader()
        {
            WritelineColor(ConsoleColor.Yellow, initialMessagePU);
        }
        public void AdditionalHeader()
        {
            WritelineColor(ConsoleColor.Yellow, initialMessageE);
            MenuQ();
        }

        public void SelectionMessage(string studentName, string surNameStude, string subject)
        {
            MainHeader();
            WritelineColor(ConsoleColor.Blue,
                $"\t\tWybrany przedmiot {subject.ToUpper()}  \n" +
                $"\t\tWybrany uczeń  {studentName.ToUpper()} {surNameStude.ToUpper()}\n");
        }
        public void Menu(string choice)
        {
            int fillInSpaceR = 50;
            const int arraySize = 8;
            const char sign = ' ';
            string[] menuSelection = new string[arraySize];
            menuSelection[0] = "P";
            menuSelection[1] = "U";
            menuSelection[2] = "O";
            menuSelection[3] = "M";
            menuSelection[4] = "W";
            menuSelection[5] = "WUP";
            menuSelection[6] = "WKP";
            menuSelection[7] = "Q";

            string[] selectTablesMenu = new string[arraySize];
            selectTablesMenu[0] = ($"\t  Wybierz [{menuSelection[0]}] aby wybrać przedmiot.");
            selectTablesMenu[1] = ($"\t  Wybierz [{menuSelection[1]}] aby wybrać ucznia.");
            selectTablesMenu[2] = ($"\t  Wybierz [{menuSelection[2]}] aby dodać ocenę uczniowi.");
            selectTablesMenu[3] = ($"\t  Wybierz [{menuSelection[3]}] aby dodać ocenę uczniowi bez zapisu do pliku.");
            selectTablesMenu[4] = ($"\t  Wybierz [{menuSelection[4]}] aby wyświetlić podsumowanie ocen ucznia z wybranego przedmiotu.");
            selectTablesMenu[5] = ($"\t  Wybierz [{menuSelection[5]}] aby wyświetlić podsumowanie ocen ucznia z wszystkich przedmiotów.");
            selectTablesMenu[6] = ($"\t  Wybierz [{menuSelection[6]}] aby wyświetlić podsumowanie ocen wszystkich uczniów z wybranego subjectu.");
            selectTablesMenu[7] = ($"\t  Wybierz [{menuSelection[7]}] aby wyjść.");

            string menuTekst = string.Empty;
            for (var i = 0; selectTablesMenu.Length > i; i++)
            {
                menuTekst = selectTablesMenu[i];
                if (fillInSpaceR < menuTekst.Length)
                {
                    fillInSpaceR = menuTekst.Length + 3;
                }
            }
            Console.BackgroundColor = ConsoleColor.Cyan;
            WritelineColor(ConsoleColor.Black, "\t".PadRight(fillInSpaceR, sign));
            for (var i = 0; selectTablesMenu.Length > i; i++)
            {
                if (choice == menuSelection[i])
                {
                    selectTablesMenu[i] = selectTablesMenu[i].PadRight(fillInSpaceR, sign);
                    Console.BackgroundColor = ConsoleColor.DarkCyan;
                    WritelineColor(ConsoleColor.Cyan, selectTablesMenu[i]);
                    Console.ResetColor();
                }
                else
                {
                    selectTablesMenu[i] = selectTablesMenu[i].PadRight(fillInSpaceR, sign);
                    Console.BackgroundColor = ConsoleColor.Cyan;
                    WritelineColor(ConsoleColor.Black, selectTablesMenu[i]);
                    Console.ResetColor();
                }
            }
            Console.BackgroundColor = ConsoleColor.Cyan;
            WritelineColor(ConsoleColor.Black, "\t".PadRight(fillInSpaceR, sign));
            Console.WriteLine("\n");
        }
        public void ViewStudentStatisticsForSubject(IStudent studentW, string fileName)
        {
            var tools = new Tools();
            var statistics = studentW.GetStatistics();

            if (statistics.Count != 0)
            {
                Console.ForegroundColor = ConsoleColor.White;
                tools.WritelineColor(ConsoleColor.White, $"\t               Podsumowanie\n" +
                                                         $"\t===========================================\n");
                MenuQ();
                Console.ResetColor();
                tools.WritelineColor(ConsoleColor.Green, $"\tOceny ucznia: " +
                                                         $"{ReadTheEvaluationFromTheSubject(fileName)}");
                tools.WritelineColor(ConsoleColor.Green, $"\tLiczna uzuskanych ocen {statistics.Count}. Suma ocen {statistics.Sum}.\n" +
                                                         $"\tŚrednia ocena wyrażona literą - {statistics.AverageLetter}\n" +
                                                         $"\tŚrednia: {statistics.Average:N1}\n" +
                                                         $"\tMax: {statistics.Max:N1}\n" +
                                                         $"\tMin: {statistics.Min:N1}\n");
            }
            else
            {
                Console.WriteLine($"\tBrak danych\n");
            }
        }
        public void SummaryOfTheHeadlines(string studentNameGlobal, string surNameStude, string subject)
        {
            var tools = new Tools();
            SelectionMessage(studentNameGlobal, surNameStude, subject);
            Console.ForegroundColor = ConsoleColor.White;
            tools.WritelineColor(ConsoleColor.White, $"\t               Podsumowanie\n" +
                                                     $"\t===========================================\n");
            Console.ResetColor();
            MenuQ();
        }
        public void ViewTheStudentsAverageAcrossAllSubjects(IStudent studentW, string studentName, string surNameStude, string subject)
        {
            var tools = new Tools();
            var statistics = studentW.GetStatistics();
            if (statistics.Count != 0)
            {
                tools.WritelineColor(ConsoleColor.Green, $"\t{subject} średnia z ocen - {statistics.AverageLetter} - {statistics.Average:N1}");
            }
            else
            {
                Console.WriteLine($"\t{subject} - Brak danych");
            }
        }
        public void ViewTheStudentsAverageAcrossAllStudent(IStudent studentW, string studentName, string surNameStude, string subject)
        {
            var tools = new Tools();
            var statistics = studentW.GetStatistics();
            if (statistics.Count != 0)
            {
                tools.WritelineColor(ConsoleColor.Green, $"\t{studentName} {surNameStude} średnia z ocen - {statistics.AverageLetter} - {statistics.Average:N1}");
            }
            else
            {
                Console.WriteLine($"\t{studentName} {surNameStude} - Brak danych");
            }
        }
        public void SummaryScreen(IStudent student, string studentName, string surNameStude, string subject)
        {
            var fileName = @$"{folder}\{studentName}_{surNameStude}_{subject}.txt";
            SelectionMessage(studentName, surNameStude, subject);
            ViewStudentStatisticsForSubject(student, fileName);
            Console.ReadLine();
            Console.Clear();
        }
        public void MenuQ()
        {
            Console.BackgroundColor = ConsoleColor.Cyan;
            WritelineColor(ConsoleColor.Black, $"\t   Wybierz [Q] aby wrócić. ".PadRight(43, ' '));
            Console.ResetColor();
            Console.WriteLine($"\t\n");
        }
    }
}
