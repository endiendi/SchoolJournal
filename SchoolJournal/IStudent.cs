using static SchoolJournal.SchoolJournalBase;

namespace SchoolJournal
{
    public interface IStudent
    {
        string Name { get; }
        string Surname { get; }
        string SchoolSubject { get; }

        event GradeAddedDelegate GradeAdded;
        void AddGrade(float grade);
        void AddGrade(string grade);
        void AddGrade(double grade);
        void AddGrade(long grade);
        void AddGrade(decimal grade);
        void AddGrade(char grade);
        Statistics GetStatistics();
    }
}
