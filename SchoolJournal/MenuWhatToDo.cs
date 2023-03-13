namespace SchoolJournal
{
    public class MenuWhatToDo
    {
        public MenuWhatToDo(string studentName, string surNameStude, string subject)
        {
            this.StudentName = studentName;
            this.SurNameStude = surNameStude;
            this.Subject = subject;
        }
        public string StudentName { get; private set; }
        public string SurNameStude { get; private set; }
        public string Subject { get; private set; }
        const string separator = " ";
        void StudentInGradeAdded(object sender, EventArgs args, float grades)
        {
            Console.Write($"\tZapisano ocenę \"Event\" {grades} do pliku.\n");
        }
        void StudentInGradeAddedS(object sender, EventArgs args, float grades)
        {
            Console.Write($"\tZapisano ocenę \"Event\" {grades} do pamięci.\n");
        }
        public void SelectionMenuP(string imput, string subjectM, string studentNameM, string surNameStudeM, string fileNameP)
        {
            var dataInput = new DataInput(studentNameM, surNameStudeM, subjectM);
            this.Subject = dataInput.SelectAddsubject(imput, subjectM, studentNameM, surNameStudeM, fileNameP);
            Console.Clear();
        }
        public void SelectionMenuU(string imput, string subjectM, string studentNameM, string surNameStudeM, string fileNameU)
        {
            var dataInput = new DataInput(studentNameM, surNameStudeM, subjectM);
            var studentChoice = dataInput.SelectAddStudent(imput, subjectM, studentNameM, surNameStudeM, fileNameU);
            this.StudentName = studentChoice[0];
            this.SurNameStude = studentChoice[1];
            Console.Clear();
        }
        public void SelectionMenuO(IStudent student1, string studentName, string surNameStude, string subject)
        {
            var tools = new Tools();
            var showScreen = new Screen();
            var dataInput = new DataInput(studentName, surNameStude, subject);
            if (tools.CheckIsNameSurnamesubject(studentName, surNameStude, subject))
            {
                student1 = new StudentInFile(studentName, surNameStude, subject);
                student1.GradeAdded += StudentInGradeAdded;
                dataInput.EnterRatings(student1, studentName, surNameStude, subject);
                showScreen.SummaryScreen(student1, studentName, surNameStude, subject);
            }
        }
        public void SelectionMenuM(IStudent student, string studentName, string surNameStude, string subject)
        {
            var tools = new Tools();
            var showScreen = new Screen();
            var dataInput = new DataInput(studentName, surNameStude, subject);
            if (tools.CheckIsNameSurnamesubject(studentName, surNameStude, subject))
            {
                student = new StudentInMemory(studentName, surNameStude, subject);
                student.GradeAdded += StudentInGradeAddedS;
                dataInput.EnterRatings(student, subject, studentName, surNameStude);
                showScreen.SummaryScreen(student, studentName, surNameStude, subject);
            }
        }
        public void SelectionMenuW(IStudent student, string studentName, string surNameStude, string subject)
        {
            var tools = new Tools();
            var showScreen = new Screen();
            var dataInput = new DataInput(studentName, surNameStude, subject);
            var fileName = @$"SchoolSubject\{studentName}_{surNameStude}_{subject}.txt";

            if (tools.CheckIsNameSurnamesubject(studentName, surNameStude, subject))
            {
                Console.Clear();
                showScreen.SelectionMessage(studentName, surNameStude, subject);
                student = new StudentInFile(studentName, surNameStude, subject);
                showScreen.ViewStudentStatisticsForSubject(student, fileName);
                Console.ReadLine();
                Console.Clear();
            }
        }
        public void SelectionMenuWUK(IStudent student, string studentName, string surNameStude, string subject, string fileNameP)
        {
            var tools = new Tools();
            var showScreen = new Screen();
            var dataInput = new DataInput(studentName, surNameStude, subject);
            var readFromTheFile = tools.ReadFromTheFilesaj(fileNameP);
            if (tools.CheckIsNameSurnamesubject(studentName, surNameStude, "NULL"))
            {
                Console.Clear();
                showScreen.SummaryOfTheHeadlines(studentName, surNameStude, "");
                foreach (var subjects in readFromTheFile)
                {
                    student = new StudentInFile(studentName, surNameStude, subjects);
                    showScreen.ViewTheStudentsAverageAcrossAllSubjects(student, studentName, surNameStude, subjects);
                }
                Console.ReadLine();
                Console.Clear();
            }
        }
        public void SelectionMenuWKP(IStudent student, string studentName, string surNameStude, string subject, string fileNameU)
        {
            var tools = new Tools();
            var showScreen = new Screen();
            var dataInput = new DataInput(studentName, surNameStude, subject);
            if (tools.CheckIsNameSurnamesubject("NULL", "NULL", subject))
            {
                Console.Clear();
                showScreen.SummaryOfTheHeadlines("", "", subject);
                var readFromTheFile = tools.ReadFromTheFilesaj(fileNameU);
                tools.SortByTurning(readFromTheFile);
                var studentNameWkp = string.Empty;
                var surNameStudeWkp = string.Empty;
                foreach (var nameTogether in tools.SortByTurning(readFromTheFile))
                {
                    string[] nameInTheTable = nameTogether.Split(separator);
                    for (int i = 0; i < nameInTheTable.Length; i++)
                    {
                        if (i == 0)
                        {
                            studentNameWkp = nameInTheTable[i];
                        }
                        if (i == 1)
                        {
                            surNameStudeWkp = nameInTheTable[i];
                        }
                    }
                    student = new StudentInFile(studentNameWkp, surNameStudeWkp, subject);
                    showScreen.ViewTheStudentsAverageAcrossAllStudent(student, studentNameWkp, surNameStudeWkp, subject);
                }
                Console.ReadLine();
                Console.Clear();
            }
        }
    }
}
