namespace SchoolJournal.Menu
{
    public class UnderTheMenuSubject
    {
        public UnderTheMenuSubject(string subject)
        {
            this.Subject = subject;
        }

        public string Subject { get; private set; }

        private readonly List<string> selectTablesMenu = new()
        {
                                               " Wybierz przedmiot.",
                                               " Dodaj przedmiot.",
                                               " Usuń przedmiot." ,
                                               " [ESC] wróć do menu."};

        private List<string> listOfFromTheFile = new();

        private int activeMenuPosition;

        public void StartMenuSubjectn()
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
                StartOptionsSubject();
                break;
            }
        }

        private void StartOptionsSubject()
        {
            listOfFromTheFile = Tools.SortBbyLastNname(Tools.ReadingWithFiles(StudentInFile.fileNameP));
            switch (activeMenuPosition)
            {
                case 0:
                    Console.Clear();
                    var choiceHorizontal = new ChoiceHorizontal(Subject, listOfFromTheFile);
                    choiceHorizontal.StartMenu(selectTablesMenu[activeMenuPosition]);
                    if (choiceHorizontal.Choice != string.Empty)
                    {
                        Subject = choiceHorizontal.Choice;
                    }
                    break;
                case 1:
                    Console.Clear();
                    IntheEvaluationMenu.AddAStudentOrSubject(
                        1,
                        selectTablesMenu[activeMenuPosition],
                        "Wpisz nazwę przedmiotu. ",
                        "Dodano przedmiot nowy: ",
                        "Przedmiot jest już w bazie dznych. ",
                        StudentInFile.fileNameP);
                    StartMenuSubjectn();
                    break;
                case 2:
                    Console.Clear();
                    var choiceHorizontal1 = new ChoiceHorizontal(Subject, listOfFromTheFile);
                    choiceHorizontal1.StartMenu(selectTablesMenu[activeMenuPosition]);
                    var toRemoval = choiceHorizontal1.Choice;
                    if (Screen.WhetherDelete(toRemoval) == true)
                    {
                        IntheEvaluationMenu.RemoveTheValueFromTheFile(toRemoval, StudentInFile.fileNameP);
                        Subject = string.Empty;
                    }
                    StartMenuSubjectn();
                    break;
                case 3:
                    Console.Clear();
                    break;
            }
            activeMenuPosition = 0;
        }
    }
}
