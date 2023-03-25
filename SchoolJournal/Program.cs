using SchoolJournal;
using SchoolJournal.Menu;
var studentName = string.Empty;
var surNameStude = string.Empty;
var subject = string.Empty;
var activeMenuPosition = 0;
Console.Title = "Dziennik szkolny.";
var menuApp = new MenuApp(activeMenuPosition, studentName, surNameStude, subject);
menuApp.StartMenu();



