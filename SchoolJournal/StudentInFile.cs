namespace SchoolJournal
{
    public class StudentInFile : SchoolJournalBase
    {
        public override event GradeAddedDelegate GradeAdded;
        private List<float> grades = new List<float>();
        private string fileName = "grades.txt";
        public const string folder = "SchoolJournal";
        public const string fileNameP = @$"{folder}\przedmioty.txt";
        public const string fileNameU = @$"{folder}\uczniowie.txt";
        public StudentInFile(string name, string surname, string schoolsubject)
            : base(name, surname, schoolsubject)
        {
            this.Name = name;
            this.Surname = surname;
            this.SchoolSubject = schoolsubject;
            fileName = @$"{folder}\{Name}_{Surname}_{SchoolSubject}.txt";
        }
        public string Name { get; private set; }
        public string Surname { get; private set; }
        public string SchoolSubject { get; private set; }
        public override void AddGrade(float grade)
        {
            {
                if (grade >= -0.5 && grade <= 6.5)
                {
                    SaveGradeFile(grade, fileName);
                    if (grade < 3)
                    {
                        Eventa(grade);
                    }
                }
                else
                {
                    throw new Exception($"\tNie prawidłowa ocena. Poprawna to 6-1 lub A,B,C,D,E,F\n");
                }
            }
        }
        public override void AddGrade(string grade)
        {
            float halfPoints = 0;
            string polarization = "";

            grade = grade.ToUpper();
            if (grade.Contains("+"))
            {
                halfPoints = 0.5f;
                polarization += "+";
            }
            else if (grade.Contains("-"))
            {
                halfPoints = -0.5f;
                polarization += "-";
            }
            if (grade == $"{polarization}6" || grade == $"6{polarization}" || grade == $"A{polarization}" || grade == $"{polarization}A")
            {
                this.AddGrade(6 + halfPoints);
            }
            else if (grade == $"{polarization}5" || grade == $"5{polarization}" || grade == $"B{polarization}" || grade == $"{polarization}B")
            {
                this.AddGrade(5 + halfPoints);
            }
            else if (grade == $"{polarization}4" || grade == $"4{polarization}" || grade == $"C{polarization}" || grade == $"{polarization}C")
            {
                this.AddGrade(4 + halfPoints);
            }
            else if (grade == $"{polarization}3" || grade == $"3{polarization}" || grade == $"D{polarization}" || grade == $"{polarization}D")
            {
                this.AddGrade(3 + halfPoints);
            }
            else if (grade == $"{polarization}2" || grade == $"2{polarization}" || grade == $"E{polarization}" || grade == $"{polarization}E")
            {
                this.AddGrade(2 + halfPoints);
            }
            else if (grade == $"{polarization}1" || grade == $"1{polarization}" || grade == $"F{polarization}" || grade == $"{polarization}F")
            {
                this.AddGrade(1 + halfPoints);
            }
            else
            {
                throw new Exception("\tWprowadzono błędną ocenę, wprowadź: \"A,B,C,D,E,F, 6-1\" lub \"Q\" aby wyjść\n");
            }
        }
        public override void AddGrade(double grade)
        {
            SaveGradeFile((float)grade, fileName);
        }
        public override void AddGrade(long grade)
        {
            SaveGradeFile((float)grade, fileName);
        }
        public override void AddGrade(decimal grade)
        {
            SaveGradeFile((float)grade, fileName);
        }
        public override void AddGrade(char grade)
        {
            switch (grade)
            {
                case 'A':
                case 'a':
                    SaveGradeFile(6, fileName);
                    break;
                case 'B':
                case 'b':
                    SaveGradeFile(5, fileName);
                    break;
                case 'C':
                case 'c':
                    SaveGradeFile(4, fileName);
                    break;
                case 'D':
                case 'd':
                    SaveGradeFile(3, fileName);
                    break;
                case 'E':
                case 'e':
                    SaveGradeFile(2, fileName);
                    break;
                case 'F':
                case 'f':
                    SaveGradeFile(1, fileName);
                    break;
                default:
                    throw new Exception("\tWprowadzono niewłaściwą literę - \"A,B,C,D,E,F\" lub \"Q\" aby wyjść");
            }
        }
        public override Statistics GetStatistics()
        {
            var statistics = new Statistics();
            var grades = ReadGradesFromFile($"{fileName}");

            foreach (var grade in grades)
            {
                statistics.AddGrade(grade);
            }
            return statistics;
        }
        private List<float> ReadGradesFromFile(string fileNameS)
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
        private void SaveGradeFile(float grade, string fileNameS)
        {
            Tools.Folder($"{folder}");
            using (var writer = File.AppendText($"{fileNameS}"))
            {
                writer.WriteLine(grade);
            }
        }
        private void Eventa(float grade)
        {
            if (GradeAdded != null)
            {
                GradeAdded(this, EventArgs.Empty, grade);
            }
        }

    }
}