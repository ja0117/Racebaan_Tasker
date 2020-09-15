using System;
using System.Collections.Generic;
using System.Text;
using Model;

namespace Controller
{
    
    public static class Data
    {
        public static Competition CompetitionVal { get; set; }

        public static void Initialize(Competition _competition)
        {
            CompetitionVal = _competition;
            // Call the method to add participants and tracks to the competition
            AddParticipants();
            AddTracksToCompetition();
        }

        // Adding a few Participants to the Competition
        public static void AddParticipants()
        {
            CompetitionVal.Participants.Add(new Driver("Gabe Newell",      0, new Car(1, 1000, 200, false), TeamColors.Blue));
            CompetitionVal.Participants.Add(new Driver("Travis Scott",     0, new Car(1, 900,  220, false), TeamColors.Green));
            CompetitionVal.Participants.Add(new Driver("Pierce Brosnan",   0, new Car(1, 1100, 190, false), TeamColors.Red));
            CompetitionVal.Participants.Add(new Driver("Hope Oscott",      0, new Car(1, 1000, 210, false), TeamColors.Yellow));
        }

        public static void AddTracksToCompetition()
        {
            CompetitionVal.Tracks.Enqueue(new Track("Graven", new SectionTypes[] { 
                SectionTypes.StartGrid,
                SectionTypes.RightCorner,
                SectionTypes.Straight,
                SectionTypes.Straight,
                SectionTypes.RightCorner,
                SectionTypes.Straight,
                SectionTypes.LeftCorner,
                SectionTypes.RightCorner,
                SectionTypes.Straight,
                SectionTypes.Straight,
                SectionTypes.Straight,
                SectionTypes.Finish
            }));
            CompetitionVal.Tracks.Enqueue(new Track("Lunar", new SectionTypes[] {
                SectionTypes.StartGrid,
                SectionTypes.Straight,
                SectionTypes.Straight,
                SectionTypes.Straight,
                SectionTypes.RightCorner,
                SectionTypes.RightCorner,
                SectionTypes.Straight,
                SectionTypes.Straight,
                SectionTypes.Straight,
                SectionTypes.Straight,
                SectionTypes.RightCorner,
                SectionTypes.RightCorner,
                SectionTypes.Finish
            }));
        }

    }
}
