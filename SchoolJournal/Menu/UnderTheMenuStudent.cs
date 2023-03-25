namespace SchoolJournal.Menu
{
    public class UnderTheMenuStudent
    {
        public UnderTheMenuStudent(string student)
        {
            this.Student = student;
        }
        public string Student { get; private set; }
        private List<string> selectTablesMenu = new List<string> {" Wybierz ucznia.",
                                                                  " Dodaj ucznia.",
                                                                  " Usuń ucznia." ,
                                                                  " [ESC] wróć do menu."};
        private List<string> listOfFromTheFile = new List<string>();
        private int activeMenuPosition;
        public void StartMenuStudent()
        {
            Console.Title = "Dziennik szkolny.";
            Console.CursorVisible = false;

            activeMenuPosition = 0;
            var oldX = Console.CursorLeft;
            var oldY = Console.CursorTop;
            Console.SetCursorPosition(oldX, oldY + 8);
            var tools = new Tools();
            tools.WritelineColorChoice("Wybierz [Q] lub [Enter] aby zakończyć wprowadzanie i wyświetlić podsumowanie.");
            Console.SetCursorPosition(oldX, oldY);

            while (true)
            {
                var verticalMenu = new VerticalMenu(activeMenuPosition, selectTablesMenu);
                verticalMenu.MenuShow();
                verticalMenu.SelectingOptions();
                activeMenuPosition = verticalMenu.ActiveMenuPosition;
                RunOptionsStudent();
                break;
            }
        }
        private void RunOptionsStudent()
        {
            var tools = new Tools();
            listOfFromTheFile = tools.SortByTurning(tools.ReadFromTheFilesaj(StudentInFile.fileNameU));
            switch (activeMenuPosition)
            {
                case 0:
                    Console.Clear();
                    var choiceHorizontal = new ChoiceHorizontal(Student, listOfFromTheFile);
                    choiceHorizontal.StartMenu(selectTablesMenu[activeMenuPosition]);
                    Student = choiceHorizontal.Choice;
                    break;
                case 1:
                    Console.Clear();
                    var addU = new AddRemove(2, selectTablesMenu[activeMenuPosition], "Wpisz imię i nazwisko ucznia oddzielonąc spacją. ", "Dodano nowego ucznia: ", "Uczeń jest już w bazie dznych. ", StudentInFile.fileNameU);
                    addU.SelectedAdd();
                    StartMenuStudent();
                    break;
                case 2:
                    Console.Clear();
                    var choiceHorizontal1 = new ChoiceHorizontal(Student, listOfFromTheFile);
                    choiceHorizontal1.StartMenu(selectTablesMenu[activeMenuPosition]);
                    var toRemoval = choiceHorizontal1.Choice;
                    AddRemove.SelectedDelete(toRemoval, StudentInFile.fileNameU);
                    Student = string.Empty;
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
