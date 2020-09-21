using System;
using Model;
using Controller;
namespace console_project
{
    class Program
    {
        static void Main(string[] args)
        {
            Controller.Data.Initialize();
            Controller.Data.NextRace();
            Console.WriteLine($"{Controller.Data.CurrentRace.Track.Name}");
        }
    }
}
