using System.Net.Http.Headers;

namespace SchoolJournal.Menu
{
    public class MenuApp
    {
        public MenuApp(int activeMenuPosition, string studentName, string surNameStude, string subject)
        {
            this.StudentName = studentName;
            this.SurNameStude = surNameStude;
            this.Subject = subject;
            this.ActiveMenuPosition = activeMenuPosition;

        }
        public string StudentName { get; private set; }
        public string SurNameStude { get; private set; }
        public string Subject { get; private set; }
        public int ActiveMenuPosition { get; private set; }

        static string fileNameP = StudentInFile.fileNameP;
        static string fileNameU = StudentInFile.fileNameU;
        const string separator = " ";


        List<string> selectTablesMenu = new List<string>  {" [U] Wybierz uczeń. ",
                                                         " [P] Wybierz przedmiot. " ,
                                                         " [O] Dodaj oceny dla ucznia z przedmiotu. ",
                                                         " [M] Dodaj oceny dla ucznia z przedmiotu symulacja bez zapisu do pliku.",
                                                         " [W] Podzumowanie ocen ucznia z przedmiotu.",
                                                         " [E] Podzumowanie ocen ucznia z wszystkich przedmiotu.",
                                                         " [R] Podsumowanie wszystkich ucznów z przedmiotu.",
                                                         " [C] Wyczyść dane.",
                                                         " [ESC] Wyjście"};
        public void StartMenu()
        {
            Console.Title = "Dziennik szkolny.";
            Console.CursorVisible = false;

            while (true)
            {
                MenuShow();
                SelectingOptions();
                RunOptions();
            }
        }
        public void MenuShow()
        {
            Console.CursorVisible = false;
            Console.BackgroundColor = ConsoleColor.DarkGray;
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(Screen.initialMessagePU);
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            var screen = new Screen();
            screen.SelectionMessage(StudentName, SurNameStude, Subject);
            var i = 0;
            foreach (var result in selectTablesMenu)
            {
                if (i == ActiveMenuPosition)
                {
                    Console.BackgroundColor = ConsoleColor.Cyan;
                    Console.ForegroundColor = ConsoleColor.DarkCyan;
                    Console.WriteLine("\t{0,-75}", result);
                    Console.BackgroundColor = ConsoleColor.Gray;
                    Console.ForegroundColor = ConsoleColor.DarkCyan;
                }
                else
                {
                    Console.BackgroundColor = ConsoleColor.Gray;
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.WriteLine("\t{0,-75}", result);
                    Console.BackgroundColor = ConsoleColor.Gray;
                }
                i++;
            }
            Console.BackgroundColor = ConsoleColor.DarkGray;
        }
        private void SelectingOptions()
        {

            Console.CursorVisible = false;
            do
            {
                ConsoleKeyInfo key = Console.ReadKey();
                if (key.Key == ConsoleKey.UpArrow)
                {
                    ActiveMenuPosition = ActiveMenuPosition > 0 ? ActiveMenuPosition - 1 : selectTablesMenu.Count - 1;
                    MenuShow();
                }
                else if (key.Key == ConsoleKey.DownArrow)
                {
                    ActiveMenuPosition = (ActiveMenuPosition + 1) % selectTablesMenu.Count;
                    MenuShow();
                }
                else if (key.Key == ConsoleKey.Escape)
                {
                    ActiveMenuPosition = selectTablesMenu.Count - 1;
                    break;

                }
                else if (key.Key == ConsoleKey.Enter)
                {
                    break;
                }
                else if (key.Key == ConsoleKey.U)
                {
                    ActiveMenuPosition = 0;
                    MenuShow();
                }
                else if (key.Key == ConsoleKey.P)
                {
                    ActiveMenuPosition = 1;
                    MenuShow();
                }
                else if (key.Key == ConsoleKey.O)
                {
                    ActiveMenuPosition = 2;
                    MenuShow();
                }
                else if (key.Key == ConsoleKey.M)
                {
                    ActiveMenuPosition = 3;
                    MenuShow();
                }
                else if (key.Key == ConsoleKey.W)
                {
                    ActiveMenuPosition = 4;
                    MenuShow();
                }
                else if (key.Key == ConsoleKey.E)
                {
                    ActiveMenuPosition = 5;
                    MenuShow();
                }
                else if (key.Key == ConsoleKey.R)
                {
                    ActiveMenuPosition = 6;
                    MenuShow();
                }
                else if (key.Key == ConsoleKey.C)
                {
                    ActiveMenuPosition = 7;
                    MenuShow();
                }

            }
            while (true);
        }
        private void RunOptions()
        {
            var student1 = new StudentInFile(StudentName, SurNameStude, Subject);

            switch (ActiveMenuPosition)
            {
                case 0:
                    Console.Clear();
                    SelectionMenuS();
                    break;
                case 1:
                    Console.Clear();
                    SelectionMenuP();
                    break;
                case 2:
                    Console.Clear();
                    SelectionMenuOU();
                    break;
                case 3:
                    Console.Clear();
                    SelectionMenuM(student1);
                    break;
                case 4:
                    Console.Clear();
                    SelectionMenuW(student1);
                    break;
                case 5:
                    Console.Clear();
                    SelectionMenuWUK(student1, fileNameP);
                    break;
                case 6:
                    Console.Clear();
                    SelectionMenuWKP(student1, fileNameU);
                    break;
                case 7:
                    Console.Clear();
                    StudentName = string.Empty;
                    SurNameStude = string.Empty;
                    Subject = string.Empty;
                    break;
                case 8:
                    Console.BackgroundColor = ConsoleColor.Black;
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Clear();
                    Environment.Exit(0);
                    break;
            }
        }
        static void StudentInGradeAdded(object sender, EventArgs args, float grades)
        {
            var tools = new Tools();
            var oldX = Console.CursorLeft;
            var oldY = Console.CursorTop;
            Console.SetCursorPosition(oldX, oldY + 8);
            tools.WritelineColor(ConsoleColor.Black, $"\tUczeń otrzymał ocenę poniżej 3. Powiadom rodziców o ocenie {grades}.\n");
            Console.SetCursorPosition(oldX, oldY);
        }
        public void SelectionMenuS()
        {
            string student = $"{StudentName} {SurNameStude}";
            var underTheMenuStudent = new UnderTheMenuStudent(student);
            underTheMenuStudent.StartMenuStudent();
            student = underTheMenuStudent.Student;
            string[] arrayStudent = student.Split(Screen.separator);
            if (arrayStudent.Length > 1)
            {
                StudentName = arrayStudent[0];
                SurNameStude = arrayStudent[1];
            }
            else
            {
                StudentName = string.Empty;
                SurNameStude = string.Empty;
            }
        }
        public void SelectionMenuP()
        {
            var underTheMenuSubject = new UnderTheMenuSubject(Subject);
            underTheMenuSubject.StartMenuSubjectn();
            Subject = underTheMenuSubject.Subject;
        }
        public void SelectionMenuOU()
        {
            var intheEvaluationMenu = new IntheEvaluationMenu(ActiveMenuPosition, StudentName, SurNameStude, Subject);
            intheEvaluationMenu.StartMenuOcena();
        }
        public void SelectionMenuO(IStudent student)
        {
            var dataInput = new AddRatings(StudentName, SurNameStude, Subject);
            var tools = new Tools();
            var screen = new Screen();
            if (tools.CheckIsNameSurnamesubject(ActiveMenuPosition, StudentName, SurNameStude, Subject))
            {
                student = new StudentInFile(StudentName, SurNameStude, Subject);
                student.GradeAdded += StudentInGradeAdded;
                dataInput.EnterRatings(student, StudentName, SurNameStude, Subject);
                screen.SummaryScreen(student, StudentName, SurNameStude, Subject);
            }
            else
            {
                Console.ReadKey();
            }
        }
        public void SelectionMenuM(IStudent student)
        {
            var dataInput1 = new AddRatings(StudentName, SurNameStude, Subject);
            var tools = new Tools();
            var screen = new Screen();
            if (tools.CheckIsNameSurnamesubject(ActiveMenuPosition, StudentName, SurNameStude, Subject))
            {
                student = new StudentInMemory(StudentName, SurNameStude, Subject);
                student.GradeAdded += StudentInGradeAdded;
                dataInput1.EnterRatings(student, StudentName, SurNameStude, Subject);
                screen.SummaryScreen(student, StudentName, SurNameStude, Subject);
            }
            else
            {
                Console.ReadKey();
            }
        }
        public void SelectionMenuW(IStudent student)
        {
            var dataInput = new AddRatings(StudentName, SurNameStude, Subject);
            var fileName = @$"{StudentInFile.folder}\{StudentName}_{SurNameStude}_{Subject}.txt";
            var tools = new Tools();
            var screen = new Screen();
            if (tools.CheckIsNameSurnamesubject(ActiveMenuPosition, StudentName, SurNameStude, Subject))
            {
                Console.Clear();
                student = new StudentInFile(StudentName, SurNameStude, Subject);
                screen.SummaryOfTheHeadlines(StudentName, SurNameStude, Subject);
                ViewSummaries.ViewStudentStatisticsForSubject(student, fileName);
                Console.ReadLine();
            }
            else
            {
                Console.ReadKey();
            }
        }
        public void SelectionMenuWUK(IStudent student, string fileNameP)
        {
            var tools = new Tools();
            var screen = new Screen();
            var dataInput = new AddRatings(StudentName, SurNameStude, Subject);
            var readFromTheFile = tools.ReadFromTheFilesaj(fileNameP);
            readFromTheFile.Sort();
            if (tools.CheckIsNameSurnamesubject(ActiveMenuPosition, StudentName, SurNameStude, "NULL"))
            {
                Console.Clear();
                screen.SummaryOfTheHeadlines(StudentName, SurNameStude, "");
                foreach (var result in readFromTheFile)
                {
                    student = new StudentInFile(StudentName, SurNameStude, result);
                    ViewSummaries.ViewTheStudentsAverageAcrossAllSubjects(student, StudentName, SurNameStude, result);
                }
                tools.WritelineColorChoice("Wciśnij [Enter] aby zakończyć podsumowanie.");
                Console.ReadLine();
            }
            else
            {
                Console.ReadKey();
            }
        }
        public void SelectionMenuWKP(IStudent student, string fileNameU)
        {
            var dataInput = new AddRatings(StudentName, SurNameStude, Subject);
            var tools = new Tools();
            var screen = new Screen();
            if (tools.CheckIsNameSurnamesubject(ActiveMenuPosition, "NULL", "NULL", Subject))
            {
                Console.Clear();
                screen.SummaryOfTheHeadlines("", "", Subject);
                var readFromTheFile = tools.ReadFromTheFilesaj(fileNameU);
                tools.SortByTurning(readFromTheFile);
                var studentNameWkp = string.Empty;
                var surNameStudeWkp = string.Empty;
                foreach (var nameTogether in tools.SortByTurning(readFromTheFile))
                {
                    string[] nameInTheTable = nameTogether.Split(separator);
                    for (int i = 0; i < nameInTheTable.Length; i++)
                    {
                        if (i == 0)
                        {
                            studentNameWkp = nameInTheTable[i];
                        }
                        if (i == 1)
                        {
                            surNameStudeWkp = nameInTheTable[i];
                        }
                    }
                    student = new StudentInFile(studentNameWkp, surNameStudeWkp, Subject);
                    ViewSummaries.ViewTheStudentsAverageAcrossAllStudent(student, studentNameWkp, surNameStudeWkp, Subject);
                }
                tools.WritelineColorChoice("Wciśnij [Enter] aby zakończyć podsumowanie.");
                Console.ReadLine();
            }
            else
            {
                Console.ReadKey();
            }
        }
    }
}
