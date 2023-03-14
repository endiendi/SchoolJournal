namespace SchoolJournal
{
    public class DataInput : Tools
    {
        private const string folder = "SchoolJournal";
        public DataInput(string studentName, string surNameStude, string subject)
        {
            this.StudentName = studentName;
            this.SurNameStude = surNameStude;
            this.Subject = subject;
        }
        public string StudentName { get; private set; }
        public string SurNameStude { get; private set; }
        public string Subject { get; private set; }
        public string SelectAddsubject(string imput, string subject, string studentName, string surNameStude, string fileNameP)
        {
            var choosesubject = string.Empty;
            string messageCommand = string.Empty;
            string enterMessage = $"\tWpisz nazwę przedmiotu z listy powyżej lub wprowadź nowy. ";
            choosesubject = EnterTheStudentData(messageCommand, enterMessage, imput, subject, studentName, surNameStude, fileNameP);
            var subjectFromFile = SelectionFilter(choosesubject.ToUpper(), fileNameP);
            if (subjectFromFile != choosesubject.ToUpper())
            {
                if (IsItInTheDatabase(choosesubject, fileNameP) == true)
                {
                    choosesubject = choosesubject.ToUpper();
                    Folder($"SchoolJournal");
                    SaveGradeFile(choosesubject.ToUpper(), fileNameP);
                }
                else
                {
                    this.Subject = subject;
                    choosesubject = Subject.ToUpper();
                }
            }
            return choosesubject;
        }
        public string[] SelectAddStudent(string imput, string subjectm, string studentNamem, string surNameStudem, string fileNameU)
        {
            string[] studentChoice = new string[2];
            studentChoice[0] = (string.Empty);
            studentChoice[1] = (string.Empty);
            var nameStudemW = (string.Empty);
            var surNameStudemW = (string.Empty);
            string messageCommand = $"\tWybierz ucznia z listy powyżej lub wprowadź nowego.";
            string enterMessage = $"\tPodaj imię ucznia: ";
            nameStudemW = EnterTheStudentData(messageCommand, enterMessage, imput, subjectm, studentNamem, surNameStudem, fileNameU);
            Console.Clear();
            enterMessage = $"\tPodaj nazwisko ucznia: ";
            surNameStudemW = EnterTheStudentData(messageCommand, enterMessage, imput, subjectm, studentNamem, surNameStudem, fileNameU);
            var firstNameLastName = $"{nameStudemW} {surNameStudemW}";
            var studentSFile = SelectionFilter(firstNameLastName.ToUpper(), fileNameU);
            if (firstNameLastName == " ")
            {
                firstNameLastName = "";
            }
            if (studentSFile != firstNameLastName.ToUpper())
            {
                if (IsItInTheDatabase(firstNameLastName.ToUpper(), fileNameU) == true)
                {
                    studentChoice[0] = nameStudemW;
                    studentChoice[1] = surNameStudemW;
                    firstNameLastName = $"{studentChoice[0]}{separator}{studentChoice[1]}";
                    Folder($"SchoolJournal");
                    SaveGradeFile(firstNameLastName.ToUpper(), fileNameU);
                }
                else
                {
                    studentChoice[0] = studentNamem;
                    studentChoice[1] = surNameStudem;
                }
            }
            else
            {
                studentChoice[0] = nameStudemW;
                studentChoice[1] = surNameStudemW;
            }
            return studentChoice;
        }
        public void EnterRatings(IStudent student, string studentName, string surNameStude, string subject)
        {
            var showScreen = new Screen();
            showScreen.SelectionMessage(studentName, surNameStude, subject);
            showScreen.AdditionalHeader();
            while (true)
            {
                Console.Write($"\tPodaj ocenę ucznia: ");
                var imput = Console.ReadLine();
                Console.Clear();
                if (imput.ToUpper() == "Q" || imput.ToUpper()=="")
                {
                    break;
                }
                showScreen.SelectionMessage(studentName, surNameStude, subject);
                showScreen.AdditionalHeader();

                try
                {
                    student.AddGrade(imput);     
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"\tBłąd wyjątku \n{ex.Message}");
                }
            }
            Console.Clear();
        }
    }
}
