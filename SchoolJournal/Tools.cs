using SchoolJournal.Menu;
using System.IO;
using static System.Net.Mime.MediaTypeNames;

namespace SchoolJournal
{
    public class Tools
    {
        public void WritelineColor(ConsoleColor color, string text)
        {
            Console.BackgroundColor = ConsoleColor.DarkGray;
            Console.ForegroundColor = color;
            Console.WriteLine(text);
        }
        public void WritelineColorChoice(string text)
        {
            Console.BackgroundColor = ConsoleColor.Cyan;
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine($"\n\n\t {text} ");
            Console.BackgroundColor = ConsoleColor.DarkGray;
            Console.ForegroundColor = ConsoleColor.Black;
        }
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
        public bool CheckIsNameSurnamesubject(int activeMenuPosition, string studentName, string surNameStude, string subject)
        {
            var resultOfTheTest = true;
            if (string.IsNullOrEmpty(studentName) && string.IsNullOrEmpty(surNameStude) && string.IsNullOrEmpty(subject))
            {
                WritelineColor(ConsoleColor.Yellow, Screen.initialMessagePU);
                WritelineColor(ConsoleColor.DarkRed, $"\n\n\n\t   Brak wybranego ucznia i przedmiotu.");
                resultOfTheTest = false;
            }
            else if (string.IsNullOrEmpty(studentName) && string.IsNullOrEmpty(surNameStude))
            {
                WritelineColor(ConsoleColor.Yellow, Screen.initialMessagePU);
                WritelineColor(ConsoleColor.DarkRed, $"\n\n\n\t   Brak wybranego ucznia.");
                resultOfTheTest = false;
            }
            else if (string.IsNullOrEmpty(subject))
            {
                WritelineColor(ConsoleColor.Yellow, Screen.initialMessagePU);
                WritelineColor(ConsoleColor.DarkRed, $"\n\n\n\t   Brak wybranego przedmiotu.");
                resultOfTheTest = false;
            }
            return resultOfTheTest;
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
            string read = string.Empty;
            var evaluationReader = ReadFromTheFilesaj(fileName);
            evaluationReader.Sort();
            evaluationReader.Reverse();
            foreach (var results in evaluationReader)
            {
                read += read = ($"{results};");
            }

            return read;
        }
        public static void Folder(string folder)
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
        public void SaveGradeFile(string toSave, string fileName)
        {
            Folder($"{StudentInFile.folder}");
            using (var writer = File.AppendText(fileName.ToLower()))
            {
                if (toSave != string.Empty)
                {
                    writer.WriteLine(toSave.ToUpper());
                }
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
        public bool CheckIfItsAlreadyThere(string aSample, string fileNameS)
        {
            var whatRecognized = false;
            aSample = aSample.ToUpper();
            var readFromTheFile = ReadFromTheFilesaj(fileNameS);
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
