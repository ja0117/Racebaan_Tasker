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
            DrawTrack(Race.Track);
            Data.CurrentRace.DriversChanged += DriversChanged;
        }

        public static void DrawTrack(Track track)
        {
            //Console.SetCursorPosition(cursorX, cursorY);

            // Set Startposition Cursor
            Console.SetCursorPosition(cursorX, cursorY);
            // Draw Each individual SectionType

            // Get current track part
            //string[] TrackPart = GetSectionType(track.Sections.First.Value.SectionType);

            foreach (Section section in Race.Track.Sections) {
                string[] TrackPart = GetSectionType(section.SectionType);
                TrackPart = AddParticipants(section, TrackPart);
                foreach (string line in TrackPart) {
                    Console.Write(line);
                    //Thread.Sleep(10);

                    switch (Orientation) {
                        case 0:
                            Console.CursorTop += 1;
                            Console.CursorLeft -= TrackSize;
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

        public static string[] AddParticipants(Section section, string[] trackPart) {
            SectionData sectionData = Race.GetSectionData(section);
            
            if (sectionData.Left != null) {
                for (int i = 0; i < trackPart.Length; i++) {
                    trackPart[i] = trackPart[i].Replace("1", sectionData.Left.Name.Substring(0, 1));
                }
            }
            else {
                for (int i = 0; i < trackPart.Length; i++) {
                    trackPart[i] = trackPart[i].Replace("1", " ");
                }
            }


            if (sectionData.Right != null) {
                for (int i = 0; i < trackPart.Length; i++) {
                    trackPart[i] = trackPart[i].Replace("2", sectionData.Right.Name.Substring(0, 1));
                }
            }
            else {
                for (int i = 0; i < trackPart.Length; i++) {
                    trackPart[i] = trackPart[i].Replace("2", " ");
                }
            }
            return trackPart;
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
