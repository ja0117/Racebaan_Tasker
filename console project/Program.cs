using System;
using Model;
using Controller;
using System.Threading;
namespace console_project
{
    class Program
    {
        static void Main(string[] args)
        {

            Controller.Data.Initialize();
            Controller.Data.NextRace();
            Console.WriteLine($"{Controller.Data.CurrentRace.Track.Name}");
            // DrawTrack
            Visualization.DrawTrack(Controller.Data.CurrentRace.Track);

            for(; ; )
            {
                Thread.Sleep(100);
            }
        }
    }
}
