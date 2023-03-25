using static System.Net.Mime.MediaTypeNames;

namespace SchoolJournal
{
    public class AddRemove
    {
        public AddRemove(int howManyWords, string whereAmI, string textFirst, string textSecond, string thirdText, string fileName)
        {
            this.TextFirst = textFirst;
            this.TextSecond = textSecond;
            this.ThirdText = thirdText;
            this.FileName = fileName;
            this.HowManyWords = howManyWords;
            this.WhereAmI = whereAmI;
        }
        public string TextFirst { get; private set; }
        public string TextSecond { get; private set; }
        public string ThirdText { get; private set; }
        public string FileName { get; private set; }
        public int HowManyWords { get; private set; }
        public string WhereAmI { get; private set; }

        private static void WyswietlNaglowek()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(Screen.initialMessagePU);
            Console.BackgroundColor = ConsoleColor.DarkGray;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.Black;
        }
        public void SelectedAdd()
        {
            var tools = new Tools();
            var ListOfFromTheFile = tools.SortByTurning(tools.ReadFromTheFilesaj(FileName));
            var choosesubject = string.Empty;
            do
            {
                Console.Clear();
                Console.BackgroundColor = ConsoleColor.DarkGray;
                WyswietlNaglowek();
                Console.WriteLine($"\t\t{WhereAmI}\n\n");
                foreach (var result in ListOfFromTheFile)
                {
                    Console.Write($" [{result.ToString()}] ");
                }
                Console.CursorVisible = true;
                Console.Write($"\n\n\t{TextFirst}");
                var oldX = Console.CursorLeft;
                var oldY = Console.CursorTop;
                Console.SetCursorPosition(oldX, oldY + 8);
                tools.WritelineColorChoice("Wciśnij [Enter] aby zakończyć.");
                Console.SetCursorPosition(oldX, oldY);
                var chooseSubjectReadLine = Console.ReadLine();

                if (chooseSubjectReadLine != string.Empty)
                {
                    var NawName = tools.CheckIfItsAlreadyThere(chooseSubjectReadLine.ToUpper(), FileName);
                    if (NawName == false)
                    {
                        choosesubject = chooseSubjectReadLine.ToUpper();
                        string[] arrayStudent = choosesubject.Split(Screen.separator);
                        if (HowManyWords == 2 && arrayStudent.Length == 1)
                        {
                            tools.WritelineColor(ConsoleColor.Red, $"\n\tBrak nazwiska");
                            Console.CursorVisible = false;
                            Console.ReadLine();
                        }
                        else
                        {
                            tools.SaveGradeFile(choosesubject.ToUpper(), FileName);
                            tools.WritelineColor(ConsoleColor.Green, $"\n\t{TextSecond} {choosesubject}");
                            Console.ReadLine();
                        }
                    }
                    else
                    {
                        Console.WriteLine($"\t{ThirdText}{chooseSubjectReadLine.ToUpper()}");
                        choosesubject = string.Empty;
                        Console.ReadLine();
                    }
                }
                else
                {
                    choosesubject = string.Empty;
                    break;
                }
            }
            while (choosesubject != string.Empty && choosesubject.Split(Screen.separator).Length == 1 && HowManyWords == 2 ||
                   choosesubject == string.Empty);
        }
        public static void DeletingFileContents(string fileName)
        {
            Tools.Folder($"{StudentInFile.folder}");
            using (Stream stream = new FileStream(fileName, FileMode.Create))
            {
                using (StreamWriter czysc = new StreamWriter(stream))
                {
                    czysc.WriteLine(string.Empty);
                }
                File.CreateText(fileName).Close();
            }
        }
        public static void SelectedDelete(string DoUsuniecia, string fileName)
        {
            List<string> ReadFromFile = new List<string>();
            var tools = new Tools();

            ReadFromFile = tools.ReadFromTheFilesaj(fileName);
            ReadFromFile.Remove(DoUsuniecia);
            DeletingFileContents(fileName);

            foreach (var result in ReadFromFile)
            {
                tools.SaveGradeFile(result, fileName);
            }
        }
    }
}
