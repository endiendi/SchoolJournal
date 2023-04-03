namespace SchoolJournal.Menu
{
    public class UnderTheMenuStudent
    {
        public UnderTheMenuStudent(string student)
        {
            this.Student = student;
        }

        public string Student { get; private set; }

        private readonly List<string> selectTablesMenu = new()
        {
                                                " Wybierz ucznia.",
                                                " Dodaj ucznia.",
                                                " Usuń ucznia." ,
                                                " [ESC] wróć do menu."};

        private List<string> listOfFromTheFile = new();

        private int activeMenuPosition;

        public void StartMenuStudent()
        {
            Console.Title = "Dziennik szkolny.";
            Console.CursorVisible = false;
            activeMenuPosition = 0;
            while (true)
            {
                var verticalMenu = new VerticalMenu(activeMenuPosition, selectTablesMenu);
                verticalMenu.MenuShow();
                verticalMenu.SelectingOptions();
                activeMenuPosition = verticalMenu.ActiveMenuPosition;
                StartOptionsStudent();
                break;
            }
        }

        private void StartOptionsStudent()
        {
            listOfFromTheFile = Tools.SortBbyLastNname(Tools.ReadingWithFiles(StudentInFile.fileNameU));
            switch (activeMenuPosition)
            {
                case 0:
                    Screen.CleanScreen();
                    var choiceHorizontal = new ChoiceHorizontal(Student, listOfFromTheFile);
                    choiceHorizontal.StartMenu(selectTablesMenu[activeMenuPosition]);
                    if (choiceHorizontal.Choice != string.Empty)
                    {
                        Student = choiceHorizontal.Choice;
                    }
                    break;
                case 1:
                    Screen.CleanScreen();
                    IntheEvaluationMenu.AddAStudentOrSubject(2,
                        selectTablesMenu[activeMenuPosition],
                        "Wpisz imię i nazwisko ucznia oddzielonąc spacją. ",
                        "Dodano nowego ucznia: ",
                        "Uczeń jest już w bazie danych. ",
                        StudentInFile.fileNameU);
                    StartMenuStudent();
                    break;
                case 2:
                    Screen.CleanScreen();
                    var choiceHorizontal1 = new ChoiceHorizontal(Student, listOfFromTheFile);
                    choiceHorizontal1.StartMenu(selectTablesMenu[activeMenuPosition]);
                    var toRemoval = choiceHorizontal1.Choice;
                    if (Screen.WhetherDelete(toRemoval) == true)
                    {
                        IntheEvaluationMenu.RemoveTheValueFromTheFile(toRemoval, StudentInFile.fileNameU);
                        Student = string.Empty;
                    }
                    StartMenuStudent();
                    break;
                case 3:
                    Console.Clear();
                    break;
            }
            activeMenuPosition = 0;
        }
    }
}

