using static SchoolJournal.SchoolJournalBase;

namespace SchoolJournal
{
    public interface IStudent
    {
        event GradeAddedDelegate GradeAdded;
        string Name { get; }
        string SurName { get; }
        string Subject { get; }
        void AddGrade(float grade);
        void AddGrade(string grade);
        void AddGrade(double grade);
        void AddGrade(long grade);
        void AddGrade(decimal grade);
        void AddGrade(char grade);
        Statistics GetStatistics();
        public void LowRatingMessage(float grade);
    }
}
