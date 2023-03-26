namespace SchoolJournal
{
    public class AddRatings : Tools
    {
        public AddRatings(string studentName, string surNameStude, string subject)
        {
            this.StudentName = studentName;
            this.SurNameStude = surNameStude;
            this.Subject = subject;
        }

        public string StudentName { get; private set; }
        public string SurNameStude { get; private set; }
        public string Subject { get; private set; }
        public void EnterRatings(IStudent student, string studentName, string surNameStude, string subject)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(Screen.initialMessagePU);
            var screen = new Screen();
            screen.SelectionMessage(StudentName, SurNameStude, Subject);
            screen.AdditionalHeader();
            while (true)
            {
                Console.WriteLine();
                var oldX = Console.CursorLeft;
                var oldY = Console.CursorTop;
                Console.SetCursorPosition(oldX, oldY + 8);
                WritelineColorChoice("Wybierz [Q] lub [Enter] aby zakończyć wprowadzanie i wyświetlić podsumowanie.");
                Console.SetCursorPosition(oldX, oldY);
                Console.CursorVisible = true;
                Console.BackgroundColor = ConsoleColor.DarkGray;
                Console.ForegroundColor = ConsoleColor.Black;
                Console.Write($"\tPodaj ocenę ucznia: ");
                var imput = Console.ReadLine();
                Console.Clear();
                if (imput.ToUpper() == "Q" || imput.ToUpper() == "")
                {
                    break;
                }
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine(Screen.initialMessagePU);
                screen.SelectionMessage(StudentName, SurNameStude, Subject);
                screen.AdditionalHeader();
                try
                {
                    student.AddGrade(imput);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"\tBłąd wyjątku \n{ex.Message}");
                }
            }
            Console.Clear();
        }

    }
}