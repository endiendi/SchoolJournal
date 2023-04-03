namespace SchoolJournal
{
    public class Tools
    {

        public static string ReverseTheOrder(string textToReverse)
        {
            string[] delimitedText = textToReverse.Split(Screen.separator);
            string invertedText = string.Empty;
            for (int i = 0; i < delimitedText.Length; i++)
            {
                invertedText += delimitedText[delimitedText.Length - 1 - i] + Screen.separator;
            }
            return invertedText;
        }

        public static List<string> ReadingWithFiles(string fileName)
        {
            var readFromFile = new List<string>();
            if (File.Exists($"{fileName}"))
            {
                using var reade = File.OpenText($"{fileName}");
                var line = reade.ReadLine();
                while (line != null && line != "")
                {
                    try
                    {
                        var namber = line;
                        readFromFile.Add(namber.ToUpper());
                        line = reade.ReadLine();
                    }
                    catch
                    {
                        Console.WriteLine($"Plik {fileName} jest uszkodzony, usuń plik! \n");
                        break;
                    }
                }
            }
            return readFromFile;
        }

        public static bool CheckIsNameSurnamesubject(string studentName, string surNameStude, string subject)
        {
            var resultOfTheTest = true;
            Screen.CleanScreen();
            if (string.IsNullOrEmpty(studentName) && string.IsNullOrEmpty(surNameStude) && string.IsNullOrEmpty(subject))
            {
                Screen.WritelineColor(ConsoleColor.Yellow, Screen.initialMessagePU);
                Screen.WritelineColor(ConsoleColor.DarkRed, $"\n\n\n\t   Brak wybranego ucznia i przedmiotu.");
                Screen.Announcement(ConsoleColor.DarkCyan, ConsoleColor.Cyan, 9, "\n\t Wciśnij [Enter] lub dowolny znak. ");
                resultOfTheTest = false;
            }
            else if (string.IsNullOrEmpty(studentName) && string.IsNullOrEmpty(surNameStude))
            {
                Screen.WritelineColor(ConsoleColor.Yellow, Screen.initialMessagePU);
                Screen.WritelineColor(ConsoleColor.DarkRed, $"\n\n\n\t   Brak wybranego ucznia.");
                Screen.Announcement(ConsoleColor.DarkCyan, ConsoleColor.Cyan, 9, "\n\t Wciśnij [Enter] lub dowolny znak. ");
                resultOfTheTest = false;
            }
            else if (string.IsNullOrEmpty(subject))
            {
                Screen.WritelineColor(ConsoleColor.Yellow, Screen.initialMessagePU);
                Screen.WritelineColor(ConsoleColor.DarkRed, $"\n\n\n\t   Brak wybranego przedmiotu.");
                Screen.Announcement(ConsoleColor.DarkCyan, ConsoleColor.Cyan, 9, "\n\t Wciśnij [Enter] lub dowolny znak. ");
                resultOfTheTest = false;
            }
            return resultOfTheTest;
        }

        public static List<string> SortBbyLastNname(List<string> toBeSorted)
        {
            List<string> reverseList = new();
            List<string> listToBeReversed = new();
            foreach (var results in toBeSorted)
            {
                reverseList.Add(ReverseTheOrder(results));
            }
            reverseList.Sort();
            foreach (var invertedData in reverseList)
            {
                listToBeReversed.Add(ReverseTheOrder(invertedData).Trim());
            }
            return listToBeReversed;
        }

        public static void CreateFolder(string folder)
        {
            string path = @$".\{folder}";
            try
            {
                if (!Directory.Exists(path))
                {
                    DirectoryInfo directory = Directory.CreateDirectory(path);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Operacja nie powiodła się: {0}", e.ToString());
            }
        }

        public static void SaveGradeFile(string toSave, string fileName)
        {
            CreateFolder($"{SchoolJournalBase.folder}");
            using var writer = File.AppendText(fileName.ToLower());
            if (toSave != string.Empty)
            {
                writer.WriteLine(toSave.ToUpper());
            }
        }

        public static bool CheckIfItsAlreadyThere(string aSample, string fileNameS)
        {
            var whatRecognized = false;
            aSample = aSample.ToUpper();
            var readFromTheFile = ReadingWithFiles(fileNameS);
            foreach (var result in readFromTheFile)
            {
                if (result == aSample)
                {
                    whatRecognized = true;
                    break;
                }
                else
                {
                    whatRecognized = false;
                }
            }
            return whatRecognized;
        }
    }
}

