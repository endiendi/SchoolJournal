namespace SchoolJournal.Menu
{
    public class UnderTheMenuSubject
    {
        public UnderTheMenuSubject(string subject)
        {
            this.Subject = subject;
        }

        public string Subject { get; private set; }

        public List<string> selectTablesMenu = new List<string> {" Wybierz przedmiot.",
                                                                 " Dodaj przedmiot.",
                                                                 " Usuń przedmiot." ,
                                                                 " [ESC] wróć do menu."};
        private List<string> listOfFromTheFile = new List<string>();
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
                RunOptionsSubject();
                break;
            }
        }
        private void RunOptionsSubject()
        {
            var tools = new Tools();
            listOfFromTheFile = tools.SortByTurning(tools.ReadFromTheFilesaj(StudentInFile.fileNameP));
            switch (activeMenuPosition)
            {
                case 0:
                    Console.Clear();
                    var choiceHorizontal = new ChoiceHorizontal(Subject, listOfFromTheFile);
                    choiceHorizontal.StartMenu(selectTablesMenu[activeMenuPosition]);
                    Subject = choiceHorizontal.Choice;
                    break;
                case 1:
                    Console.Clear();
                    var addRemove = new AddRemove(1, selectTablesMenu[activeMenuPosition], "Wpisz nazwę przedmiotu. ", "Dodano przedmiot nowy: ", "Przedmiot jest już w bazie dznych. ", StudentInFile.fileNameP);
                    addRemove.SelectedAdd();
                    StartMenuSubjectn();
                    break;
                case 2:
                    Console.Clear();
                    var choiceHorizontal1 = new ChoiceHorizontal(Subject, listOfFromTheFile);
                    choiceHorizontal1.StartMenu(selectTablesMenu[activeMenuPosition]);
                    var toRemoval = choiceHorizontal1.Choice;
                    AddRemove.SelectedDelete(toRemoval, StudentInFile.fileNameP);
                    Subject = string.Empty;
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