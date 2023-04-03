namespace SchoolJournal
{
    public class StudentInMemory : SchoolJournalBase
    {
        private List<float> grades = new();

        public StudentInMemory(string name, string surName, string subject)
            : base(name, surName, subject)
        {
            this.Name = name;
            this.SurName = surName;
            this.Subject = subject;
        }

        public new string Name { get; private set; }

        public new string SurName { get; private set; }

        public new string Subject { get; private set; }

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

        public override void AddGrade(float grade)
        {
            if (grade >= -0.5 && grade <= 6.5)
            {
                this.grades.Add(grade);
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

        public override Statistics GetStatistics()
        {
            grades.Sort();
            grades.Reverse();
            var statistics = new Statistics();
            {
                foreach (var gradr in this.grades)
                {
                    statistics.AddGrade(gradr);
                }
            }
            return statistics;
        }
    }
}
