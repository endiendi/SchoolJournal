namespace SchoolJournal
{
    public abstract class SchoolJournalBase : IStudent
    {
        public delegate void GradeAddedDelegate(object sender, EventArgs args, float punkty);
        public abstract event GradeAddedDelegate GradeAdded;
        protected SchoolJournalBase(string name, string surname, string schoolsubject)
        {
            this.Name = name;
            this.Surname = surname;
            this.SchoolSubject = schoolsubject;
        }
        public string Name { get; private set; }
        public string Surname { get; private set; }
        public string SchoolSubject { get; private set; }
        public abstract void AddGrade(float grade);
        public abstract void AddGrade(string grade);
        public abstract void AddGrade(double grade);
        public abstract void AddGrade(long grade);
        public abstract void AddGrade(decimal grade);
        public abstract void AddGrade(char grade);
        public abstract Statistics GetStatistics();

    }
}

