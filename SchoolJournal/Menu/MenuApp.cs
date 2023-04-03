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

        private const string separator = " ";

        private readonly List<string> selectTablesMenu = new()
        {
                                            " [U] Wybierz uczeń. ",
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
            Screen.CleanScreen();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(Screen.initialMessagePU);
            Screen.StudentAndSubjectSelectionMessage(StudentName, SurNameStude, Subject);
            var i = 0;
            foreach (var result in selectTablesMenu)
            {
                if (i == ActiveMenuPosition)
                {
                    Console.BackgroundColor = ConsoleColor.Cyan;
                    Console.ForegroundColor = ConsoleColor.DarkCyan;
                    Console.WriteLine("\t{0,-75}", result);
                }
                else
                {
                    Console.BackgroundColor = ConsoleColor.Gray;
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.WriteLine("\t{0,-75}", result);
                }
                i++;
            }
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
            var student = new StudentInFile(StudentName, SurNameStude, Subject);

            switch (ActiveMenuPosition)
            {
                case 0:
                    SelectionMenuS();
                    break;
                case 1:
                    SelectionMenuP();
                    break;
                case 2:
                    SelectionMenuOU();
                    break;
                case 3:
                    SelectionMenuM(student);
                    break;
                case 4:
                    SelectionMenuW(student);
                    break;
                case 5:
                    SelectionMenuWUK(student);
                    break;
                case 6:
                    SelectionMenuWKP();
                    break;
                case 7:
                    Screen.CleanScreen();
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

        private static void StudentInGradeAdded(object sender, EventArgs args, float grades)
        {
            Screen.Announcement(ConsoleColor.DarkRed, ConsoleColor.DarkGray, 4, $"\n\tUczeń otrzymał ocenę poniżej 3. Powiadom rodziców o ocenie {grades}.\n");
        }

        public void SelectionMenuS()
        {
            string student = $"{StudentName} {SurNameStude}";
            var underTheMenuStudent = new UnderTheMenuStudent(student);
            Screen.CleanScreen();
            underTheMenuStudent.StartMenuStudent();
            student = underTheMenuStudent.Student;
            string[] arrayStudent = student.Split(Screen.separator);
            var studentName = string.Empty;
            var surNameStude = string.Empty;
            for (var i = 0; i < arrayStudent.Length; i++)
            {
                if (i == 0)
                {
                    studentName = arrayStudent[i];
                }
                else if (i == arrayStudent.Length - 1)
                {
                    surNameStude = $"{surNameStude}{arrayStudent[i]}";
                }
                else
                {
                    surNameStude = $"{surNameStude}{arrayStudent[i]}{Screen.separator}";
                }
            }
            StudentName = studentName;
            SurNameStude = surNameStude;
        }

        public void SelectionMenuP()
        {
            var underTheMenuSubject = new UnderTheMenuSubject(Subject);
            Screen.CleanScreen();
            underTheMenuSubject.StartMenuSubjectn();
            Subject = underTheMenuSubject.Subject;
        }

        public void SelectionMenuOU()
        {
            var intheEvaluationMenu = new IntheEvaluationMenu(StudentName, SurNameStude, Subject);
            intheEvaluationMenu.StartMenuOcena();
        }

        public void SelectionMenuO(IStudent student)
        {
            if (Tools.CheckIsNameSurnamesubject(student.Name, student.SurName, Subject))
            {
                student = new StudentInFile(student.Name, student.SurName, Subject);
                student.GradeAdded += StudentInGradeAdded;
                EnterRatings(student);
                Screen.DisplaysTheSummaryScreen(student, student.Name, student.SurName, Subject);
            }
            else
            {
                Console.ReadKey();
            }
        }

        public void SelectionMenuM(IStudent student)
        {
            if (Tools.CheckIsNameSurnamesubject(student.Name, student.SurName, Subject))
            {
                Screen.CleanScreen();
                student = new StudentInMemory(student.Name, student.SurName, Subject);
                student.GradeAdded += StudentInGradeAdded;
                EnterRatings(student);
                Screen.DisplaysTheSummaryScreen(student, student.Name, student.SurName, Subject);
            }
            else
            {
                Console.ReadKey();
            }
        }

        public void SelectionMenuW(IStudent student)
        {
            if (Tools.CheckIsNameSurnamesubject(student.Name, student.SurName, Subject))
            {
                Screen.CleanScreen();
                student = new StudentInFile(student.Name, student.SurName, Subject);
                Screen.DisplayHeaderSummary(student.Name, student.SurName, Subject);
                ViewStudentStatisticsForSubject(student);
                Console.ReadLine();
            }
            else
            {
                Console.ReadKey();
            }
        }

        public static void SelectionMenuWUK(IStudent student)
        {
            var fileName = @$"{StudentInFile.fileNameP}";
            var readFromTheFile = Tools.ReadingWithFiles(fileName);
            readFromTheFile.Sort();
            if (Tools.CheckIsNameSurnamesubject(student.Name, student.SurName, "NULL"))
            {
                Screen.CleanScreen();
                Screen.DisplayHeaderSummary(student.Name, student.SurName, "");
                Screen.WritelineColor(ConsoleColor.DarkYellow, "\t\tŚrednia z ocen ucznia.\n");
                foreach (var result in readFromTheFile)
                {
                    student = new StudentInFile(student.Name, student.SurName, result);
                    ViewTheStudentsAverageAcrossAllSubjects(student, result);
                }
                Screen.Announcement(ConsoleColor.DarkCyan, ConsoleColor.Cyan, 3, "\n\t Wciśnij [Enter] aby zakończyć podsumowanie. ");
                Console.ReadLine();
            }
            else
            {
                Console.ReadKey();
            }
        }

        public void SelectionMenuWKP()
        {
            var fileName = @$"{StudentInFile.fileNameU}";
            if (Tools.CheckIsNameSurnamesubject("NULL", "NULL", Subject))
            {
                Screen.CleanScreen();
                Screen.DisplayHeaderSummary("", "", Subject);
                var readFromTheFile = Tools.ReadingWithFiles(fileName);
                Tools.SortBbyLastNname(readFromTheFile);
                var studentNameWkp = string.Empty;
                var surNameStudeWkp = string.Empty;
                Screen.WritelineColor(ConsoleColor.DarkYellow, "\t\tŚrednia z ocen uczniów z przedmiotu.\n");
                foreach (var nameTogether in Tools.SortBbyLastNname(readFromTheFile))
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
                    var student = new StudentInFile(studentNameWkp, surNameStudeWkp, Subject);
                    ViewTheStudentsAverageAcrossAllStudent(student);
                }
                Screen.Announcement(ConsoleColor.DarkCyan, ConsoleColor.Cyan, 3, "\n\t Wciśnij [Enter] aby zakończyć podsumowanie. ");
                Console.ReadLine();
            }
            else
            {
                Console.ReadKey();
            }
        }

        public static void ViewTheStudentsAverageAcrossAllSubjects(IStudent student, string subject)
        {
            Console.CursorVisible = false;
            string format = "\t{0,-15}{1,-5}{2,-5:N1}";
            var statistics = student.GetStatistics();
            if (statistics.Count != 0)
            {
                if (statistics.Average < 3)
                {
                    Screen.WritelineColor(ConsoleColor.DarkRed, string.Format(format, subject, statistics.AverageLetter, statistics.Average));
                }
                else
                {
                    Screen.WritelineColor(ConsoleColor.Green, string.Format(format, subject, statistics.AverageLetter, statistics.Average));
                }
            }
            else
            {
                Screen.WritelineColor(ConsoleColor.Black, string.Format(format, subject, "", "Brak danych"));
            }
        }

        public static void ViewTheStudentsAverageAcrossAllStudent(IStudent student)
        {
            Console.CursorVisible = false;
            string format = "\t{0,-25}{1,-5}{2,-5:N1}";
            var statistics = student.GetStatistics();
            if (statistics.Count != 0)
            {
                if (statistics.Average < 3)
                {
                    Screen.WritelineColor(ConsoleColor.DarkRed, string.Format(format, $"{student.Name} {student.SurName}", statistics.AverageLetter, statistics.Average));
                }
                else
                {
                    Screen.WritelineColor(ConsoleColor.Green, string.Format(format, $"{student.Name} {student.SurName}", statistics.AverageLetter, statistics.Average));
                }
            }
            else
            {
                Screen.WritelineColor(ConsoleColor.Black, string.Format(format, $"{student.Name} {student.SurName}", "Brak danych", ""));
            }
        }

        public static void ViewStudentStatisticsForSubject(IStudent student)
        {
            Console.CursorVisible = false;
            var statistics = student.GetStatistics();
            if (statistics.Count != 0)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write($"\tOceny ucznia: ");
                ChangeColorofLowRatings(ConsoleColor.DarkRed, ConsoleColor.DarkYellow, ConsoleColor.Green, statistics.PointsCollected);
                Screen.WritelineColor(ConsoleColor.Green, $"\n\tUczeń uzyskał {statistics.Count} ocen. Suma ocen to {statistics.Sum}.");
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write($"\tŚrednia ocena wyrażona literą: ");
                ColorForHighAndLowRatings(ConsoleColor.DarkRed, ConsoleColor.DarkYellow, ConsoleColor.Green, statistics.AverageLetter);
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write($"\tŚrednia ocena wyrażona cyfrom: ");
                ColorForHighAndLowRatings(ConsoleColor.DarkRed, ConsoleColor.DarkYellow, ConsoleColor.Green, statistics.Average);
                Screen.WritelineColor(ConsoleColor.Green, $"\tMax: {statistics.Max:N1}\n" +
                $"\tMin: {statistics.Min:N1}\n");
                Screen.Announcement(ConsoleColor.DarkCyan, ConsoleColor.Cyan, 3, "\n\t Wciśnij [Enter] aby zakończyć podsumowanie. ");
            }
            else
            {
                Screen.WritelineColor(ConsoleColor.Black, $"\tBrak danych\n");
            }
        }

        public void EnterRatings(IStudent student)
        {
            Screen.CleanScreen();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(Screen.initialMessagePU);
            Screen.StudentAndSubjectSelectionMessage(student.Name, student.SurName, Subject);
            Screen.DisplaysTheSecondHeading();
            while (true)
            {
                Screen.Announcement(ConsoleColor.DarkCyan, ConsoleColor.Cyan, 10, "\n\tWybierz [Q] lub [Enter] aby zakończyć wprowadzanie i wyświetlić podsumowanie.");
                var statistics = student.GetStatistics();
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write($"\tOceny ucznia: ");
                ChangeColorofLowRatings(ConsoleColor.DarkRed, ConsoleColor.DarkYellow, ConsoleColor.Green, statistics.PointsCollected);
                Console.CursorVisible = true;
                Console.BackgroundColor = ConsoleColor.DarkGray;
                Console.ForegroundColor = ConsoleColor.Black;
                Console.Write($"\n\n\tDodaj ocenę ucznia: ");
                var imput = Console.ReadLine();
                Console.Clear();
                if (imput.ToUpper() == "Q" || imput == string.Empty)
                {
                    break;
                }
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine(Screen.initialMessagePU);
                Screen.StudentAndSubjectSelectionMessage(student.Name, student.SurName, Subject);
                Screen.DisplaysTheSecondHeading();
                try
                {
                    student.AddGrade(imput);
                }
                catch (Exception ex)
                {
                    Screen.Announcement(ConsoleColor.DarkRed, ConsoleColor.DarkGray, 4, $"\n\tBłąd wyjątku! \n{ex.Message}");
                }
            }
            Console.Clear();
        }

        public static void ChangeColorofLowRatings(ConsoleColor colorLow, ConsoleColor colorLowHigh, ConsoleColor colorHigh, List<float> grades)
        {
            if (grades != null)
            {
                foreach (var grade in grades)
                {
                    {
                        if (grade < 3)
                        {
                            Console.ForegroundColor = colorLow;
                            Console.Write($"{grade}");
                            Console.ForegroundColor = colorHigh;
                            Console.Write($"|");
                        }
                        else if (grade < 5)
                        {
                            Console.ForegroundColor = colorLowHigh;
                            Console.Write($"{grade}");
                            Console.ForegroundColor = colorLowHigh;
                            Console.Write($"|");
                        }
                        else
                        {
                            Console.ForegroundColor = colorHigh;
                            Console.Write($"{grade}|");
                        }
                    }
                }
            }
        }

        public static void ColorForHighAndLowRatings(ConsoleColor colorLow, ConsoleColor colorLowHigh, ConsoleColor colorHigh, float grades)
        {
            if (grades < 3)
            {
                Console.ForegroundColor = colorLow;
                Console.Write($"{grades:N1}\n");
            }
            else if (grades < 5)
            {
                Console.ForegroundColor = colorLowHigh;
                Console.Write($"{grades:N1}\n");
            }
            else
            {
                Console.ForegroundColor = colorHigh;
                Console.Write($"{grades:N1}\n");
            }
        }

        public static void ColorForHighAndLowRatings(ConsoleColor colorLow, ConsoleColor colorLowHigh, ConsoleColor colorHigh, string grades)
        {
            if (grades == "F" || grades == "E" || grades == "E+")
            {
                Console.ForegroundColor = colorLow;
                Console.Write($"{grades}\n");
            }
            else if (grades == "D" || grades == "D+" || grades == "C" || grades == "C+")
            {
                Console.ForegroundColor = colorLowHigh;
                Console.Write($"{grades:N1}\n");
            }
            else
            {
                Console.ForegroundColor = colorHigh;
                Console.Write($"{grades}\n");
            }
        }
    }
}
