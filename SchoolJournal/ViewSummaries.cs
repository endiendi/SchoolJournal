using System.Diagnostics;

namespace SchoolJournal
{
    public class ViewSummaries : Tools
    {

        public static void ViewTheStudentsAverageAcrossAllSubjects(IStudent studentW, string studentName, string surNameStude, string subject)
        {
            var tools = new Tools();
            Console.CursorVisible = false;
            var statistics = studentW.GetStatistics();
            if (statistics.Count != 0)
            {
                if (statistics.Average < 3)
                {
                    tools.WritelineColor(ConsoleColor.Red, $"\t{subject} średnia z ocen - {statistics.AverageLetter} - {statistics.Average:N1}");
                }
                else
                {
                tools.WritelineColor(ConsoleColor.Green, $"\t{subject} średnia z ocen - {statistics.AverageLetter} - {statistics.Average:N1}");
                }
            }
            else
            {
                tools.WritelineColor(ConsoleColor.Black, $"\t{subject} - Brak danych");
            }
        }
        public static void ViewTheStudentsAverageAcrossAllStudent(IStudent studentW, string studentName, string surNameStude, string subject)
        {
            Console.CursorVisible = false;
            var statistics = studentW.GetStatistics();
            var tools = new Tools();
            if (statistics.Count != 0)
            {
                if (statistics.Average < 3)
                {
                    tools.WritelineColor(ConsoleColor.Red, $"\t{studentName} {surNameStude} średnia z ocen - {statistics.AverageLetter} - {statistics.Average:N1}");
                }
                else
                {
                tools.WritelineColor(ConsoleColor.Green, $"\t{studentName} {surNameStude} średnia z ocen - {statistics.AverageLetter} - {statistics.Average:N1}");
                }
            }
            else
            {
                tools.WritelineColor(ConsoleColor.Black, $"\t{studentName} {surNameStude} - Brak danych");
            }
        }
        public static void ViewStudentStatisticsForSubject(IStudent studentW, string fileName)
        {
            Console.CursorVisible = false;
            var statistics = studentW.GetStatistics();
            var tools = new Tools();
            if (statistics.Count != 0)
            {
                tools.WritelineColor(ConsoleColor.Green, $"\tOceny ucznia: " +
                                                         $"{tools.ReadTheEvaluationFromTheSubject(fileName)}");
                tools.WritelineColor(ConsoleColor.Green, $"\tLiczna uzuskanych ocen {statistics.Count}. Suma ocen {statistics.Sum}.\n" +
                                                         $"\tŚrednia ocena wyrażona literą - {statistics.AverageLetter}\n" +
                                                         $"\tŚrednia: {statistics.Average:N1}\n" +
                                                         $"\tMax: {statistics.Max:N1}\n" +
                                                         $"\tMin: {statistics.Min:N1}\n");
                //Console.WriteLine("TEST");
                tools.WritelineColorChoice("Wciśnij [Enter] aby zakończyć podsumowanie.");
            }
            else
            {
                tools.WritelineColor(ConsoleColor.Black, $"\tBrak danych\n");
            }
        }
    }
}
