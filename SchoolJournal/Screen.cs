using SchoolJournal.Menu;

namespace SchoolJournal
{
    public class Screen
    {

        public const string separator = " ";
        public const string initialMessagePU =
        "\t=========================================================================\n" +
        "\t                      Witamy w dzienniku szkolnym\n" +
        "\t=========================================================================\n";

        private const string initialMessageE =
            "\t      Zakres oceny ucznia to 6-1 lub A-6, B-5, C-4, D-3, E-2, F-0 " +
            "\"+\" to plus 0,4 \"-\" to minus 0,4\n";

        public static void DisplaysTheSecondHeading()
        {
            WritelineColor(ConsoleColor.Yellow, initialMessageE);
        }

        public static void StudentAndSubjectSelectionMessage(string studentName, string surNameStude, string subject)
        {
            WritelineColor(ConsoleColor.Black,
                $"\t\tWybrany uczeń  {studentName.ToUpper()} {surNameStude.ToUpper()}\n" +
                $"\t\tWybrany przedmiot {subject.ToUpper()}\n");
        }

        public static void DisplayHeaderSummary(string studentName, string surNameStude, string subject)
        {
            WritelineColor(ConsoleColor.Yellow, initialMessagePU);
            StudentAndSubjectSelectionMessage(studentName, surNameStude, subject);
            Console.ForegroundColor = ConsoleColor.White;
            WritelineColor(ConsoleColor.White,
                $"\t                               Podsumowanie\n" +
                $"\t=========================================================================\n");
        }

        public static void DisplaysTheSummaryScreen(IStudent student, string studentName, string surNameStude, string subject)
        {
            DisplayHeaderSummary(studentName, surNameStude, subject);
            MenuApp.ViewStudentStatisticsForSubject(student);
            Console.ReadLine();
            CleanScreen();
        }

        public static void Announcement(ConsoleColor colorText, ConsoleColor colorBackgrounds, int howMuchLower, string messageText)
        {
            var oldX = Console.CursorLeft;
            var oldY = Console.CursorTop;
            var initialTextColor = Console.ForegroundColor;
            var initialBackgroundColor = Console.BackgroundColor;
            Console.SetCursorPosition(oldX, oldY + howMuchLower);
            Console.ForegroundColor = colorText;
            Console.BackgroundColor = colorBackgrounds;
            Console.WriteLine($"{messageText}");
            Console.BackgroundColor = initialBackgroundColor;
            Console.ForegroundColor = initialTextColor;
            Console.SetCursorPosition(oldX, oldY);
        }

        public static void WritelineColor(ConsoleColor color, string text)
        {
            var initialTextColor = Console.ForegroundColor;
            var initialBackgroundColor = Console.BackgroundColor;
            Console.BackgroundColor = ConsoleColor.DarkGray;
            Console.ForegroundColor = color;
            Console.WriteLine($"{text}");
            Console.BackgroundColor = initialBackgroundColor;
            Console.ForegroundColor = initialTextColor;
        }

        public static bool WhetherDelete(string whatDeletion)
        {
            var toRemove = false;
            if (whatDeletion != string.Empty)
            {
                Console.BackgroundColor = ConsoleColor.DarkGray;
                Console.Clear();
                WritelineColor(ConsoleColor.Yellow, initialMessagePU);
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.Write($"\n\n\n\t   Czy napewna usunąć {whatDeletion}? T\\N ");
                Console.ForegroundColor = ConsoleColor.Black;
                Console.CursorVisible = true;
                var choice = Console.ReadLine();
                if (choice.ToUpper() == "T")
                {
                    toRemove = true;
                }
                Console.CursorVisible = false;
            }
            return toRemove;
        }

        public static void CleanScreen()
        {
            Console.BackgroundColor = ConsoleColor.DarkGray;
            Console.Clear();
        }
    }
}