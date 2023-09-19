using Dateisystem.BusinessLayer;
using Dateisystem.Data;

internal class Program
{
    private static void Main(string[] args)
    {
        ConsoleHelper.Start();

        //First time path etc.
        //string path = ConsoleHelper.GetInput();
        //FileDirAccessLayer.PrepDir(path);

        FileDirAccessLayer.Display();
        Stats.DisplayStats();
        Console.ReadLine();
    }
}