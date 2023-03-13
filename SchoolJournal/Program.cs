using SchoolJournal;

string studentName = string.Empty;
string surNameStude = string.Empty;
string subject = string.Empty;

var student = new StudentInMemory(studentName, surNameStude, subject);
var student1 = new StudentInFile(studentName, surNameStude, subject);
var showScreen = new Screen();
var tools = new Tools();
var menuWhatToDo = new MenuWhatToDo(studentName, surNameStude, subject);
tools.Folder($"SchoolJournal");
string fileNameP = @$"SchoolJournal\przedmioty.txt";
string fileNameU = @$"SchoolJournal\uczniowie.txt";
Console.Title = "Dziennik szkolny.";
while (true)
{
    showScreen.SelectionMessage(studentName, surNameStude, subject);
    showScreen.Menu("");
    var imput = Console.ReadLine();
    Console.Clear();
    if (imput.ToUpper() == "Q")
    {
        break;
    }
    else if (imput.ToUpper() == "P")
    {
        menuWhatToDo.SelectionMenuP(imput, subject, studentName, surNameStude, fileNameP);
        subject = menuWhatToDo.Subject;
    }
    else if ((imput.ToUpper() == "U"))
    {
        menuWhatToDo.SelectionMenuU(imput, subject, studentName, surNameStude, fileNameU);
        studentName = menuWhatToDo.StudentName;
        surNameStude = menuWhatToDo.SurNameStude;
    }
    else if ((imput.ToUpper() == "O"))
    {
        menuWhatToDo.SelectionMenuO(student1, studentName, surNameStude, subject);
    }
    else if ((imput.ToUpper() == "M"))
    {
        menuWhatToDo.SelectionMenuM(student, studentName, surNameStude, subject);
    }
    else if ((imput.ToUpper() == "W"))
    {
        menuWhatToDo.SelectionMenuW(student1, studentName, surNameStude, subject);
    }
    else if ((imput.ToUpper() == "WUP"))
    {
        menuWhatToDo.SelectionMenuWUK(student, studentName, surNameStude, subject, fileNameP);
    }
    else if ((imput.ToUpper() == "WKP"))
    {
        menuWhatToDo.SelectionMenuWKP(student, studentName, surNameStude, subject, fileNameU);
    }
    else
    {
        tools.WritelineColor(ConsoleColor.Red, "\tNie prawidłowy wybór.");
    }
}
