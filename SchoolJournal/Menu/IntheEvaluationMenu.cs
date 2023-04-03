namespace SchoolJournal.Menu
{
    public class IntheEvaluationMenu
    {
        public IntheEvaluationMenu(string studentName, string surNameStude, string subject)
        {
            this.StudentName = studentName;
            this.SurNameStude = surNameStude;
            this.Subject = subject;
        }

        public string StudentName { get; private set; }

        public string SurNameStude { get; private set; }

        public string Subject { get; private set; }

        private readonly List<string> selectTablesMenu = new()
        {
                                                                  " Dodaj ocenę.",
                                                                  " Usuń ocenę." ,
                                                                  " [ESC] wróć do menu."};

        private List<string> listOfFromTheFile = new();

        private int activeMenuPosition;

        public void StartMenuOcena()
        {
            Console.Title = "Dziennik szkolny.";
            Console.CursorVisible = false;
            activeMenuPosition = 0;
            var verticalMenu = new VerticalMenu(activeMenuPosition, selectTablesMenu);
            while (true)
            {
                verticalMenu.MenuShow();
                verticalMenu.SelectingOptions();
                activeMenuPosition = verticalMenu.ActiveMenuPosition;
                StartOptionsStudent();
                break;
            }
        }

        private void StartOptionsStudent()
        {
            string pathToTheStudentFile =
                @$"{SchoolJournalBase.folder}\{StudentName}{SchoolJournalBase.separatorFile}{SurNameStude}{SchoolJournalBase.separatorFile}{Subject}.txt";
            listOfFromTheFile = Tools.SortBbyLastNname(Tools.ReadingWithFiles(pathToTheStudentFile));
            switch (activeMenuPosition)
            {
                case 0:
                    Console.Clear();
                    var student = new StudentInFile(StudentName, SurNameStude, Subject);
                    var menuApp = new MenuApp(activeMenuPosition, StudentName, SurNameStude, Subject);
                    menuApp.SelectionMenuO(student);
                    break;
                case 1:
                    if (Tools.CheckIsNameSurnamesubject(StudentName, SurNameStude, Subject))
                    {
                        Console.Clear();
                        var choiceHorizontal = new ChoiceHorizontal(Subject, listOfFromTheFile);
                        choiceHorizontal.StartMenu(selectTablesMenu[activeMenuPosition]);
                        var toRemoval = choiceHorizontal.Choice;
                        if (Screen.WhetherDelete(toRemoval) == true)
                        {
                            RemoveTheValueFromTheFile(toRemoval, pathToTheStudentFile);
                        }
                        StartMenuOcena();
                    }
                    else
                    {
                        Console.ReadKey();
                    }
                    Console.Clear();
                    break;
                case 2:
                    Console.Clear();
                    break;
            }
            activeMenuPosition = 0;
        }

        public static void DeletingFileContents(string fileName)
        {
            Tools.CreateFolder($"{SchoolJournalBase.folder}");
            using Stream stream = new FileStream(fileName, FileMode.Create);
            using (StreamWriter cleanTheFile = new(stream))
            {
                cleanTheFile.WriteLine(string.Empty);
            }
            File.CreateText(fileName).Close();
        }

        public static void RemoveTheValueFromTheFile(string whatToRemove, string fileName)
        {
            List<string> readFromFile;
            readFromFile = Tools.ReadingWithFiles(fileName);
            readFromFile.Remove(whatToRemove);
            DeletingFileContents(fileName);
            foreach (var result in readFromFile)
            {
                Tools.SaveGradeFile(result, fileName);
            }
        }

        private static void DisplayTheFirstHeader()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(Screen.initialMessagePU);
            Console.BackgroundColor = ConsoleColor.DarkGray;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.Black;
        }

        public static void AddAStudentOrSubject(
            int howManyWords,
            string whereAmI,
            string textFirst,
            string textSecond,
            string thirdText,
            string fileName)
        {
            var ListOfFromTheFile = Tools.SortBbyLastNname(Tools.ReadingWithFiles(fileName));
            do
            {
                Console.Clear();
                Console.BackgroundColor = ConsoleColor.DarkGray;
                DisplayTheFirstHeader();
                Console.WriteLine($"\t\t{whereAmI}\n\n");
                foreach (var result in ListOfFromTheFile)
                {
                    Console.Write($" [{result}] ");
                }
                Console.CursorVisible = true;
                Console.Write($"\n\n\t{textFirst}");
                Screen.Announcement(ConsoleColor.DarkCyan, ConsoleColor.Cyan, 7, "\n\t Wciśnij [Enter] lub dowolny znak aby zakończyć. ");
                var enteredValue = Console.ReadLine();
                if (enteredValue != string.Empty)
                {
                    string studentToChange = ClearStringFromSpace(enteredValue.ToUpper());
                    var nawName = Tools.CheckIfItsAlreadyThere(studentToChange.ToUpper(), fileName);
                    if (nawName == false)
                    {
                        string[] arrayStudent = studentToChange.ToUpper().Split(Screen.separator);
                        if (howManyWords == 2 && arrayStudent.Length == 1)
                        {
                            Screen.Announcement(ConsoleColor.DarkRed, ConsoleColor.DarkGray, 2, $"\n\tBrak nazwiska. ");
                            Console.CursorVisible = false;
                            Console.ReadLine();
                        }
                        else
                        {
                            Tools.SaveGradeFile(studentToChange.ToUpper(), fileName);
                            Screen.WritelineColor(ConsoleColor.Green, $"\n\t{textSecond} {studentToChange}");
                            Console.ReadLine();
                            break;
                        }
                    }
                    else
                    {
                        Console.WriteLine($"\t{thirdText} {studentToChange.ToUpper()}");
                        Console.ReadLine();
                    }
                }
                else
                {
                    break;
                }
            }
            while (true);
        }

        public static string ClearStringFromSpace(string forCleaning)
        {
            string afterCleaning = string.Empty;
            string[] arrayStudent = forCleaning.Split(Screen.separator);
            for (var i = 0; i < arrayStudent.Length; i++)
            {
                if (i < 1)
                {
                    if (arrayStudent[i] != string.Empty)
                    {
                        afterCleaning += $"{arrayStudent[i]}{Screen.separator}";
                    }
                }
                else
                {
                    if (arrayStudent[i] != string.Empty)
                    {
                        afterCleaning += $"{arrayStudent[i]}{Screen.separator}";
                    }
                }
            }
            afterCleaning = afterCleaning.TrimEnd();
            return afterCleaning;
        }
    }
}