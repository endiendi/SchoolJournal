namespace SchoolJournal.Menu
{
    public class HorizontalMenu
    {
        public HorizontalMenu(int activeMenuPosition, string whereAmIMeny, List<string> selectTablesMenu)
        {
            this.SelectTablesMenu = selectTablesMenu;
            this.ActiveMenuPosition = activeMenuPosition;
            this.WhereAmIMeny = whereAmIMeny;
            this.SelectTablesMenu.Add($"ESC] wróć do menu.");
        }

        public List<string> SelectTablesMenu { get; private set; }
        public int ActiveMenuPosition { get; private set; }
        public string WhereAmIMeny { get; private set; }

        public void MenuShow()
        {
            Screen.CleanScreen();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(Screen.initialMessagePU);
            Console.ForegroundColor = ConsoleColor.Black;
            Console.WriteLine($"\t\t{WhereAmIMeny}");
            Console.WriteLine();
            var i = 0;
            foreach (var result in SelectTablesMenu)
            {
                if (i == ActiveMenuPosition)
                {
                    Console.BackgroundColor = ConsoleColor.Cyan;
                    Console.ForegroundColor = ConsoleColor.DarkCyan;
                    Console.Write("{0,-" + result.Length + "}", " [" + result + "] ");
                    Console.BackgroundColor = ConsoleColor.DarkGray;
                    Console.Write(" ");
                }
                else
                {
                    Console.BackgroundColor = ConsoleColor.Gray;
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.Write("{0,-" + result.Length + "}", " [" + result + "] ");
                    Console.BackgroundColor = ConsoleColor.DarkGray;
                    Console.Write(" ");
                }
                i++;
            }
            Screen.Announcement(ConsoleColor.DarkCyan, ConsoleColor.Cyan, 9, "\n\t Wybór strzałlki < >  [ESC] powrót. ");
        }

        public void SelectingOptions()
        {
            Console.CursorVisible = false;
            do
            {
                ConsoleKeyInfo key = Console.ReadKey();
                if (key.Key == ConsoleKey.LeftArrow || key.Key == ConsoleKey.UpArrow)
                {
                    ActiveMenuPosition = ActiveMenuPosition > 0 ? ActiveMenuPosition - 1 : SelectTablesMenu.Count - 1;
                    MenuShow();
                }
                else if (key.Key == ConsoleKey.RightArrow || key.Key == ConsoleKey.DownArrow)
                {
                    ActiveMenuPosition = (ActiveMenuPosition + 1) % SelectTablesMenu.Count;
                    MenuShow();
                }
                else if (key.Key == ConsoleKey.Escape)
                {
                    ActiveMenuPosition = SelectTablesMenu.Count - 1;
                    break;
                }
                else if (key.Key == ConsoleKey.Enter)
                {
                    break;
                }
            }
            while (true);
        }
    }
}
