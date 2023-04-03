namespace SchoolJournal
{
    public class StudentInFile : SchoolJournalBase
    {
        private readonly string fileName = "grades.txt";
        public StudentInFile(string name, string surName, string subject)
            : base(name, surName, subject)
        {
            this.Name = name;
            this.SurName = surName;
            this.Subject = subject;
            fileName = @$"{folder}\{Name}{separatorFile}{SurName}{separatorFile}{Subject}.txt";

        }

        public new string Name { get; private set; }

        public new string SurName { get; private set; }

        public new string Subject { get; private set; }

        public override void AddGrade(float grade)
        {
            {
                if (grade >= -0.5 && grade <= 6.5)
                {
                    SaveGradeFile(grade, fileName);
                    if (grade < 3)
                    {
                        LowRatingMessage(grade);
                    }
                }
                else
                {
                    throw new Exception($"\tNie prawidłowa ocena. \n\tPoprawna to 6-1 lub A,B,C,D,E,F\n");
                }
            }
        }

        public override Statistics GetStatistics()
        {
            var statistics = new Statistics();
            var grades = ReadGradesFromFile($"{fileName}");
            grades.Sort();
            grades.Reverse();
            foreach (var grade in grades)
            {
                statistics.AddGrade(grade);
            }
            return statistics;
        }

        private static List<float> ReadGradesFromFile(string fileNameS)
        {
            var grades = new List<float>();
            if (File.Exists($"{fileNameS}"))
            {
                using (var reade = File.OpenText($"{fileNameS}"))
                {
                    var line = reade.ReadLine();
                    while (line != null)
                    {
                        try
                        {
                            var namber = float.Parse(line);
                            grades.Add(namber);
                            line = reade.ReadLine();
                        }
                        catch
                        {
                            Console.WriteLine($"\tPlik {fileNameS} jest uszkodzony, usuń plik! \n");
                            break;
                        }
                    }
                }
            }
            return grades;
        }

        private static void SaveGradeFile(float grade, string fileNameS)
        {
            Tools.CreateFolder($"{folder}");
            using (var writer = File.AppendText($"{fileNameS}"))
            {
                writer.WriteLine(grade);
            }
        }
    }
}