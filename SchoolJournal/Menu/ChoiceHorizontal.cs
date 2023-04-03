namespace SchoolJournal.Menu
{
    public class ChoiceHorizontal
    {
        public ChoiceHorizontal(string choice, List<string> listOfFromTheFile)
        {
            this.Choice = choice;
            this.ListOfFromTheFile = listOfFromTheFile;
        }

        public string Choice { get; private set; }
        public List<string> ListOfFromTheFile { get; private set; }

        private int activeMenuPosition;

        public void StartMenu(string whereAmI)
        {
            Console.Title = "Dziennik szkolny.";
            Console.CursorVisible = false;
            while (true)
            {
                var horizontalMenu = new HorizontalMenu(activeMenuPosition, whereAmI, ListOfFromTheFile);
                horizontalMenu.MenuShow();
                horizontalMenu.SelectingOptions();
                activeMenuPosition = horizontalMenu.ActiveMenuPosition;
                Choice = CheckWhetherESC(ListOfFromTheFile[activeMenuPosition]);
                activeMenuPosition = 0;
                Console.Clear();
                break;
            }
        }

        public static string CheckWhetherESC(string whatToCheck)
        {
            string? afterChecking;
            if (whatToCheck == "ESC] wróć do menu.")
            {
                afterChecking = string.Empty;
            }
            else
            {
                afterChecking = whatToCheck;
            }
            return afterChecking;
        }
    }
}

