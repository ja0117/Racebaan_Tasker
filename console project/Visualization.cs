using Controller;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;

namespace console_project
{
    public static class Visualization
    {

        private static Race Race;

        // Variable to determen orientation
        private static int Orientation = 1;
        // 0 is North
        // 1 is East
        // 2 is South
        // 3 is West


        // Cursor position
        private static int cursorX = 20;
        private static int cursorY = 3;

        // Width / Height of track
        private static int TrackSize = 4;

        #region Graphics

        private static string[] _start = {
            "════",
            "  1|",
            "2|  ",
            "════"
        };

        private static string[] _finishHorizontal = {
            "════",
            "  ░1",
            "  ░2",
            "════"
        };

        private static string[] _finishVertical = {
            "║  ║",
            "║░░║",
            "║12║",
            "║  ║"
        };

        private static string[] _straightHorizontal = {
            "════",
            "  1 ",
            "  2 ",
            "════"
        };

        private static string[] _straightVertical = {
            "║1 ║",
            "║  ║",
            "║ 2║",
            "║  ║"
        };

        private static string[] _cornerRight0 = {
            @" ╔══",
            @"╔╝1 ",
            @"║  2",
            @"║  ╔"
        };

        private static string[] _cornerRight1 = {
            @"══╗ ",
            @" 1╚╗",
            @" 2 ║",
            @"╗  ║"
        };

        private static string[] _cornerRight2 = {
            @"╝1 ║",
            @"  2║",
            @"  ╔╝",
            @"══╝ "
        };

        private static string[] _cornerRight3 = {
            @"║ 1╚",
            @"║   ",
            @"╚╗ 2",
            @" ╚══"
        };

        private static string[] _cornerLeft3 = {
            @" ╔══",
            @"╔╝ 1",
            @"║ 2 ",
            @"║  ╔"
        };

        private static string[] _cornerLeft1 = {
            @"╝1 ║",
            @"  2║",
            @"  ╔╝",
            @"══╝ "
        };

        private static string[] _cornerLeft2 = {
            @"║1 ╚",
            @"║   ",
            @"╚╗ 2",
            @" ╚══"
        };

        private static string[] _cornerLeft0 = {
            @"══╗ ",
            @"2 ╚╗",
            @"   ║",
            @"╗1 ║"
        };

        #endregion
        
        public static void Initialize(Race race)
        {
            Race = race;
            Data.CurrentRace.DriversChanged += DriversChanged;
            Race.LapsFinished = new Dictionary<IParticipant, int>();
            AddParticipantsToScoreList();
            DrawTrack(Race.Track);

        }

        private static void AddParticipantsToScoreList() {
            foreach (IParticipant p in Race.Participants) {
                Race.LapsFinished.Add(p, 0);
            }
        }

        public static void DrawTrack(Track track)
        {
            DrawScore();
            // Set Startposition Cursor
            Console.SetCursorPosition(cursorX, cursorY);
            // Draw Each individual SectionType

            // Get current track part
            //string[] TrackPart = GetSectionType(track.Sections.First.Value.SectionType);

            Console.SetCursorPosition(cursorX, cursorY);

            foreach (Section section in Race.Track.Sections) {
                string[] TrackPart = GetSectionType(section.SectionType);
                // If one of the participants is finished
                foreach (string line in TrackPart) {
                    string replacedLine = AddParticipants(section, line);
                    Console.Write(replacedLine);
                    switch (Orientation) {
                        case 0:
                            Console.CursorTop += 1;
                            Console.CursorLeft -= TrackSize;
                            //Console.SetCursorPosition(cursorX -= TrackSize, cursorY += 1)
                            break;
                        case 1:  // Going Straight works!
                            Console.CursorTop += 1;
                            Console.CursorLeft -= TrackSize;
                            break;
                        case 2:
                            Console.CursorTop += 1;
                            Console.CursorLeft -= TrackSize;
                            break;
                        case 3:
                            Console.CursorTop += 1;
                            Console.CursorLeft -= TrackSize;
                            break;
                    }
                }

                if (section.SectionType == SectionTypes.LeftCorner) {
                    ClampOrientationMinus();
                }
                if (section.SectionType == SectionTypes.RightCorner) {
                    ClampOrientationPlus();
                }

                switch (Orientation) {
                    case 0:
                        Console.SetCursorPosition(cursorX, cursorY -= TrackSize);
                        break;
                    case 1:
                        Console.SetCursorPosition(cursorX += TrackSize, cursorY);
                        break;
                    case 2:
                        Console.SetCursorPosition(cursorX, cursorY += TrackSize);
                        break;
                    case 3:
                        Console.SetCursorPosition(cursorX -= TrackSize, cursorY);
                        break;
                }
            }
        }

        public static string AddParticipants(Section section, string trackPart) {
            SectionData sectionData = Race.GetSectionData(section);

            var replace = new string(trackPart);

            replace = replace.Replace("1", sectionData.Left != null ? sectionData.Left.Name.ToCharArray()[0].ToString() : " ");
            replace = replace.Replace("2", sectionData.Right != null ? sectionData.Right.Name.ToCharArray()[0].ToString() : " ");

            return replace;

        }

        

        //Event handler for DriversChanged
        private static void DriversChanged(object source, DriversChangedEventArgs e) {
            DrawTrack(e.Track);
        }


        private static string[] GetSectionType(SectionTypes s)
        {
            switch (s)
            {
                case SectionTypes.Straight:
                    if (Orientation == 1 || Orientation == 3) {
                        return _straightHorizontal;
                    }
                    else {
                        return _straightVertical;
                    }
                case SectionTypes.LeftCorner:
                    switch (Orientation) {
                        case 0:
                            return _cornerLeft0;
                        case 1:
                            return _cornerLeft1;
                        case 2:
                            return _cornerLeft2;
                        case 3:
                            return _cornerLeft3;
                        default:
                            return null;
                    }
                case SectionTypes.RightCorner:
                    switch (Orientation) {
                        case 0:
                            return _cornerRight0;
                        case 1:
                            return _cornerRight1;
                        case 2:
                            return _cornerRight2;
                        case 3:
                            return _cornerRight3;
                        default:
                            return null;
                    }
                case SectionTypes.StartGrid:
                    return _start;
                case SectionTypes.Finish:
                    if (Orientation == 1 || Orientation == 3) {
                        return _finishHorizontal;
                    }
                    else {
                        return _finishVertical;
                    }
                default:
                    return null;
            }
        }

        private static void DrawScore() {
            int si = 2;
            Console.SetCursorPosition(65, 2);
            Console.Write("========= Max Laps: 3 =========");
            foreach (var item in Race.LapsFinished) {
                Console.SetCursorPosition(65, si += 1);
                Console.Write($"={item.Key.Name} ".PadRight(20) + $"Laps: {item.Value}".PadRight(10) + "=");
            }
            Console.SetCursorPosition(65, si += 1);
            Console.Write("===============================");
        }

        private static int ClampOrientationPlus() {
            Orientation += 1;
            if (Orientation == 4) {
                Orientation = 0;
                return Orientation;
            }
            return Orientation;
        }

        private static int ClampOrientationMinus() {
            Orientation -= 1;
            if (Orientation == -1) {
                Orientation = 3;
                return Orientation;
            }
            return Orientation;
        }
    }
}
