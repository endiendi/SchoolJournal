namespace SchoolJournal
{
    public class Screen : Tools
    {

        public Screen()
        {

        }
        public const string separator = " ";
        public const string initialMessagePU =
                            "\t==========================================================================\n" +
                            "\t                      Witamy w dzienniku szkolnym\n" +
                            "\t==========================================================================\n";
        private const string initialMessageE =
                            "\t      Zakres oceny ucznia to 6-1 lub A-6, B-5, C-4, D-3, E-2, F-0 + to plus 0,5 - to minus 0,5\n";
         public void AdditionalHeader()
        {
            WritelineColor(ConsoleColor.Yellow, initialMessageE);
        }
        public void SelectionMessage(string studentName, string surNameStude, string subject)
        {
            WritelineColor(ConsoleColor.Black,
                $"\t\tWybrany uczeń  {studentName.ToUpper()} {surNameStude.ToUpper()}\n" +
                $"\t\tWybrany przedmiot {subject.ToUpper()}\n");
        }
        public  void SummaryOfTheHeadlines(string studentName, string surNameStude, string subject)
        {
            WritelineColor(ConsoleColor.Yellow, initialMessagePU);
            SelectionMessage(studentName, surNameStude, subject);
            Console.ForegroundColor = ConsoleColor.White;
            WritelineColor(ConsoleColor.White, $"\t                                    Podsumowanie\n" +
                                                     $"\t==========================================================================\n");
        }
        public void SummaryScreen(IStudent student, string studentName, string surNameStude, string subject)
        {
            var fileName = @$"{StudentInFile.folder}\{studentName}_{surNameStude}_{subject}.txt";
            SummaryOfTheHeadlines(studentName, surNameStude, subject);
            ViewSummaries.ViewStudentStatisticsForSubject(student, fileName);
            Console.ReadLine();
            Console.Clear();
        }
    }
}
