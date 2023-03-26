using System;

namespace SchoolJournal
{
    public class StudentInMemory : SchoolJournalBase
    {
        public override event GradeAddedDelegate GradeAdded;
        private List<float> grades = new List<float>();
        public StudentInMemory(string name, string surname, string schoolsubject)
            : base(name, surname, schoolsubject)
        {
            this.Name = name;
            this.Surname = surname;
            this.SchoolSubject = schoolsubject;
        }

        public string Name { get; private set; }
        public string Surname { get; private set; }
        public string SchoolSubject { get; private set; }


        public override void AddGrade(float grade)
        {
            if (grade >= -0.5 && grade <= 6.5)
            {
                this.grades.Add(grade);
                if (grade < 3)
                {
                    Eventa(grade);
                }
            }
            else
            {
                throw new Exception($"\tPunkty poza zakresem, prawidłowy zakres to 0,5-6,5\n");
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
            this.AddGrade((float)grade);
        }
        public override void AddGrade(long grade)
        {
            this.AddGrade((float)grade);
        }
        public override void AddGrade(decimal grade)
        {
            this.AddGrade((float)grade);
        }
        public override void AddGrade(char grade)
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
        public override Statistics GetStatistics()
        {

            var statistics = new Statistics();
            {
                foreach (var gradr in this.grades)
                {
                    statistics.AddGrade(gradr);
                }
            }
            return statistics;     
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
