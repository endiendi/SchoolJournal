namespace SchoolJournal.Menu
{
    public class VerticalMenu
    {
        public VerticalMenu(int activeMenuPosition, List<string> selectTablesMenu)
        {
            this.SelectTablesMenu = selectTablesMenu;
            this.ActiveMenuPosition = activeMenuPosition;
        }

        public List<string> SelectTablesMenu { get; private set; }

        public int ActiveMenuPosition { get; private set; }

        public void MenuShow()
        {
            Screen.CleanScreen();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(Screen.initialMessagePU);
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine("\n\n");
            var i = 0;
            foreach (var result in SelectTablesMenu)
            {
                if (i == ActiveMenuPosition)
                {
                    Console.BackgroundColor = ConsoleColor.Cyan;
                    Console.ForegroundColor = ConsoleColor.DarkCyan;
                    Console.WriteLine("\t{0,-25}", result);
                }
                else
                {
                    Console.BackgroundColor = ConsoleColor.Gray;
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.WriteLine("\t{0,-25}", result);
                }
                i++;
            }
            Console.BackgroundColor = ConsoleColor.DarkGray;
        }

        public void SelectingOptions()
        {
            Console.CursorVisible = false;
            do
            {
                ConsoleKeyInfo key = Console.ReadKey();
                if (key.Key == ConsoleKey.UpArrow)
                {
                    this.ActiveMenuPosition = ActiveMenuPosition > 0 ? ActiveMenuPosition - 1 : SelectTablesMenu.Count - 1;
                    MenuShow();
                }
                else if (key.Key == ConsoleKey.DownArrow)
                {
                    this.ActiveMenuPosition = (ActiveMenuPosition + 1) % SelectTablesMenu.Count;
                    MenuShow();
                }
                else if (key.Key == ConsoleKey.Escape)
                {
                    this.ActiveMenuPosition = SelectTablesMenu.Count - 1;
                    break;
                }
                else if (key.Key == ConsoleKey.Enter)
                {
                    break;
                }
                else if (key.Key == ConsoleKey.D1 || key.Key == ConsoleKey.NumPad1)
                {
                    this.ActiveMenuPosition = 0;
                    MenuShow();
                }
                else if (key.Key == ConsoleKey.D2 || key.Key == ConsoleKey.NumPad2)
                {
                    this.ActiveMenuPosition = 1;
                    MenuShow();
                }
                else if (key.Key == ConsoleKey.D3 || key.Key == ConsoleKey.NumPad3)
                {
                    this.ActiveMenuPosition = 2;
                    MenuShow();
                }
                else if (key.Key == ConsoleKey.D4 || key.Key == ConsoleKey.NumPad4)
                {
                    this.ActiveMenuPosition = 3;
                    MenuShow();
                }
            }
            while (true);
        }
    }
}

