namespace SchoolJournal
{
    public class Tools
    {
        public const string separator = " ";

        public void WritelineColor(ConsoleColor color, string text)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(text);
            Console.ResetColor();
        }
        public string ReverseTheOrder(string textToReverse)
        {
            string[] delimitedText = textToReverse.Split(separator);
            string invertedText = string.Empty;
            for (int i = 0; i < delimitedText.Length; i++)
            {
                invertedText += delimitedText[delimitedText.Length - 1 - i] + separator;
            }
            return invertedText;
        }
        public string SelectionFilter(string aSample, string fileNameS)
        {
            var whatRecognized = string.Empty;
            aSample = aSample.ToUpper();
            var readFromTheFile = ReadFromTheFilesaj(fileNameS);
            foreach (var readFromTheFiles in readFromTheFile)
            {
                if (readFromTheFiles == aSample)
                {
                    whatRecognized = readFromTheFiles;
                }
                else if (whatRecognized != aSample)
                {
                    whatRecognized = string.Empty;
                }
            }
            return whatRecognized.ToUpper();
        }
        public List<string> ReadFromTheFilesaj(string fileName)
        {
            var subject = new List<string>();
            if (File.Exists($"{fileName}"))
            {
                using (var reade = File.OpenText($"{fileName}"))
                {
                    var line = reade.ReadLine();
                    while (line != null && line != "")
                    {
                        try
                        {
                            var namber = line;
                            subject.Add(namber.ToUpper());
                            line = reade.ReadLine();
                        }
                        catch
                        {
                            Console.WriteLine($"Plik {fileName} jest uszkodzony, usuń plik! \n");
                            break;
                        }
                    }
                }
            }
            return subject;
        }
        public void SaveGradeFile(string toSave, string fileNameS)
        {
            using (var writer = File.AppendText(fileNameS.ToLower()))
            {
                writer.WriteLine(toSave.ToUpper());
            }
        }
        public bool IsItInTheDatabase(string nechoosesubject, string fileNameP)
        {
            var isInTheBase = false;
            Console.WriteLine($"\tNie ma danych w bazie. Czy dopisać [T/N]?");
            while (true)
            {
                var imputTN = Console.ReadLine();
                if (imputTN.ToUpper() == "T")
                {
                    isInTheBase = true;
                    break;
                }
                else if (imputTN.ToUpper() == "N")
                {
                    Console.Clear();
                    break;
                }
            }
            return isInTheBase;
        }
        public bool CheckIsNameSurnamesubject(string studentName, string surNameStude, string subject)
        {
            var resultOfTheTest = true;
            if (string.IsNullOrEmpty(studentName) || string.IsNullOrEmpty(surNameStude))
            {
                WritelineColor(ConsoleColor.Red, $"\t   Wybierz [U] aby wybrać ucznia.");
                resultOfTheTest = false;
            }
            else if (string.IsNullOrEmpty(subject))
            {
                WritelineColor(ConsoleColor.Red, $"\t   Wybierz [P] aby wybrać przedmiot.");
                resultOfTheTest = false;
            }
            return resultOfTheTest;
        }
        public string EnterTheStudentData(string messageCommand, string enterMessage, string imput, string subject, string studentName, string surNameStude, string fileNameU)
        {
            string studentData = string.Empty;
            while (studentData == string.Empty)
            {
                Screen showScreen = new Screen();
                showScreen.MainHeader();
                var readFromTheFile = ReadFromTheFilesaj(fileNameU);
                showScreen.MenuQ();
                DisplayResult(readFromTheFile);
                Console.WriteLine($"\t{messageCommand}\n");
                Console.Write($"\t{enterMessage} ");
                var enteredData = Console.ReadLine();
                if (enteredData.ToUpper() == "Q" || enteredData.ToUpper() == string.Empty)
                {
                    if (imput.ToUpper() == "P")
                    {
                        studentData = (subject);
                        break;
                    }
                    if (imput.ToUpper() == "U")
                    {
                        studentData = (studentName);
                        break;
                    }
                }
                else
                {
                    var NawName = SelectionFilter(enteredData, fileNameU);
                    if (enteredData != string.Empty)
                    {
                        studentData = (enteredData);
                    }
                }
            }
            return studentData;
        }
        public void DisplayResult(List<string> result)
        {
            var listSorted = SortByTurning(result);
            Console.Write($"\t");
            foreach (var listSorteds in listSorted)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write($"[{listSorteds}] ");
                Console.ResetColor();
            }
            Console.WriteLine("\n");
        }
        public List<string> SortByTurning(List<string> result)
        {
            List<string> reverseList = new List<string>();
            List<string> listToBeReversed = new List<string>();
            foreach (var results in result)
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
        public string ReadTheEvaluationFromTheSubject(string fileName)
        {
            string odczytane = string.Empty;
            var evaluationReader = ReadFromTheFilesaj(fileName);
            evaluationReader.Sort();
            evaluationReader.Reverse();
            foreach (var results in evaluationReader)
            {
                odczytane += odczytane = ($"{results} ");
            }

            return odczytane;
        }
        public void Folder(string folder)
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
        public int MenuBeamLength(string tekst)
        {
            int fillInSpaceR = 50;
            string menuTekst = string.Empty;
            for (var i = 0; tekst.Length > i; i++)
            {
                menuTekst = tekst;
                if (fillInSpaceR < menuTekst.Length)
                {
                    fillInSpaceR = menuTekst.Length + 3;
                }
            }
            return fillInSpaceR;
        }

    }
}
