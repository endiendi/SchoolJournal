namespace SchoolJournal.Menu
{
    public class IntheEvaluationMenu
    {
        public IntheEvaluationMenu(int activeMenuPosition, string studentName, string surNameStude, string subject)
        {
            this.StudentName = studentName;
            this.SurNameStude = surNameStude;
            this.Subject = subject;
        }
        public string StudentName { get; private set; }
        public string SurNameStude { get; private set; }
        public string Subject { get; private set; }
        static List<string> selectTablesMenu = new List<string>  {" Dodaj ocenę.",
                                                                  " Usuń ocenę." ,
                                                                  " [ESC] wróć do menu."};
        private List<string> listOfFromTheFile = new List<string>();
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
                RunOptionsStudent();
                break;
            }
        }
        private void RunOptionsStudent()
        {
            string sciezkaUczen = @$"{StudentInFile.folder}\{StudentName}_{SurNameStude}_{Subject}.txt";
            var tools = new Tools();
            listOfFromTheFile = tools.SortByTurning(tools.ReadFromTheFilesaj(sciezkaUczen));
            switch (activeMenuPosition)
            {
                case 0:
                    Console.Clear();
                    var student1 = new StudentInFile(StudentName, SurNameStude, Subject);
                    var menuApp = new MenuApp(activeMenuPosition, StudentName, SurNameStude, Subject);
                    menuApp.SelectionMenuO(student1);
                    break;
                case 1:
                    if (tools.CheckIsNameSurnamesubject(activeMenuPosition, StudentName, SurNameStude, Subject))
                    {
                        Console.Clear();
                        var wyborPoziomy1 = new ChoiceHorizontal(Subject, listOfFromTheFile);
                        wyborPoziomy1.StartMenu(selectTablesMenu[activeMenuPosition]);
                        var toRemoval = wyborPoziomy1.Choice;
                        AddRemove.SelectedDelete(toRemoval, sciezkaUczen);
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
    }
}
