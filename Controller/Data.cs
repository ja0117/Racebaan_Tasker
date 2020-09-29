using System;
using System.Collections.Generic;
using System.Text;
using Model;

namespace Controller
{
    
    public static class Data
    {
        public static Competition CompetitionVal { get; set; }
        public static Race CurrentRace { get; set; }

        public static void Initialize()
        {
            CompetitionVal = new Competition();
            // Call the method to add participants and tracks to the competition
            AddParticipants();
            AddTracksToCompetition();
        }

        // Adding a few Participants to the Competition
        public static void AddParticipants()
        {
            CompetitionVal.Participants.Add(new Driver("Gabe Newell",      0, new Car(1, 1, 9, false), TeamColors.Blue));
            CompetitionVal.Participants.Add(new Driver("Travis Scott",     0, new Car(1, 9, 7, false), TeamColors.Green));
            CompetitionVal.Participants.Add(new Driver("Pierce Brosnan",   0, new Car(1, 5, 6, false), TeamColors.Red));
            CompetitionVal.Participants.Add(new Driver("Hope Oscott",      0, new Car(1, 6, 5, false), TeamColors.Yellow));
        }

        public static void AddTracksToCompetition()
        {

            CompetitionVal.Tracks.Enqueue(new Track("Graven", new SectionTypes[] {
                SectionTypes.StartGrid,
                SectionTypes.Straight,
                SectionTypes.Straight,
                SectionTypes.RightCorner,
                SectionTypes.RightCorner,
                SectionTypes.Straight,
                SectionTypes.Straight,
                SectionTypes.LeftCorner,
                SectionTypes.LeftCorner,
                SectionTypes.Straight,
                SectionTypes.Straight,
                SectionTypes.Straight,
                SectionTypes.Straight,
                SectionTypes.RightCorner,
                SectionTypes.RightCorner,
                SectionTypes.Straight,
                SectionTypes.Straight,
                SectionTypes.LeftCorner,
                SectionTypes.LeftCorner,
                SectionTypes.Straight,
                SectionTypes.Straight,
                SectionTypes.Straight,
                SectionTypes.Finish
            }));

            CompetitionVal.Tracks.Enqueue(new Track("Lunar", new SectionTypes[] {
                SectionTypes.StartGrid,
                SectionTypes.RightCorner,
                SectionTypes.RightCorner,
                SectionTypes.RightCorner,
                SectionTypes.RightCorner,
                SectionTypes.Finish
            }));
        }

        public static void NextRace()
        {
            Track NextTrack = CompetitionVal.NextTrack();

            if (NextTrack != null)
            {
                CurrentRace = new Race(NextTrack, CompetitionVal.Participants);
            }
        }

    }
}
