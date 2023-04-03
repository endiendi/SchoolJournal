namespace SchoolJournal
{
    public abstract class SchoolJournalBase : IStudent
    {
        public const string separatorFile = " ";
        public const string folder = "SchoolJournal";
        public const string fileNameP = @$"{folder}\przedmioty.txt";
        public const string fileNameU = @$"{folder}\uczniowie.txt";

        public delegate void GradeAddedDelegate(object sender, EventArgs args, float grades);

        public event GradeAddedDelegate? GradeAdded;

        protected SchoolJournalBase(string name, string surName, string subject)
        {
            this.Name = name;
            this.SurName = surName;
            this.Subject = subject;
        }

        public string Name { get; private set; }

        public string SurName { get; private set; }

        public string Subject { get; private set; }

        public abstract void AddGrade(float grade);

        public abstract void AddGrade(double grade);

        public abstract void AddGrade(long grade);

        public abstract void AddGrade(decimal grade);

        public void AddGrade(string grade)
        {
            float halfPoints = 0;
            string polarization = string.Empty;

            grade = grade.ToUpper();
            if (grade.Contains('+'))
            {
                halfPoints = 0.4f;
                polarization += "+";
            }
            else if (grade.Contains('-'))
            {
                halfPoints = -0.4f;
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
                if (halfPoints == -0.4f)
                {
                    halfPoints = 0;
                }
                this.AddGrade(1 + halfPoints);
            }
            else
            {
                throw new Exception($"\tNie prawidłowa ocena. \n\tPoprawna to 6-1 lub A,B,C,D,E,F\n");
            }
        }

        public void AddGrade(char grade)
        {
            switch (grade)
            {
                case 'A':
                case 'a':
                    this.AddGrade(6);
                    break;
                case 'B':
                case 'b':
                    this.AddGrade(5);
                    break;
                case 'C':
                case 'c':
                    this.AddGrade(4);
                    break;
                case 'D':
                case 'd':
                    this.AddGrade(3);
                    break;
                case 'E':
                case 'e':
                    this.AddGrade(2);
                    break;
                case 'F':
                case 'f':
                    this.AddGrade(1);
                    break;
                default:
                    throw new Exception("\tWprowadzono niewłaściwą literę - ocenę prawidłowa to: \"A,B,C,D,E,F\"  aby wyjść wybierz \"Q\" ");
            }
        }

        public abstract Statistics GetStatistics();
        public void LowRatingMessage(float grade)
        {
            if (GradeAdded != null)
            {
                GradeAdded(this, EventArgs.Empty, grade);
            }
        }
    }
}