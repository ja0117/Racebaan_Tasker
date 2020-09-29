using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

namespace console_project
{
    public static class Visualization
    {
        // Variable to see if we're going straight
        private static bool isStraight = true;

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
            "  ¦ ",
            "  ¦ ",
            "════"
        };

        private static string[] _finishHorizontal = {
            "════",
            "  ░ ",
            "  ░ ",
            "════"
        };

        private static string[] _finishVertical = {
            "║  ║",
            "║░░║",
            "║  ║",
            "║  ║"
        };

        private static string[] _straightHorizontal = {
            "════",
            "    ",
            "    ",
            "════"
        };

        private static string[] _straightVertical = {
            "║  ║",
            "║  ║",
            "║  ║",
            "║  ║"
        };

        private static string[] _cornerRightHtoV = {
            @"══╗ ",
            @"  ╚╗",
            @"   ║",
            @"╗  ║"
        };

        private static string[] _cornerRightVtoH = {
            @"╝  ║",
            @"   ║",
            @"  ╔╝",
            @"══╝ "
        };

        private static string[] _cornerLeftVtoH = {
            @"║  ╚",
            @"║   ",
            @"╚╗  ",
            @" ╚══"
        };

        private static string[] _cornerLeftHtoV = {
            @" ╔══",
            @"╔╝  ",
            @"║   ",
            @"║  ╔"
        };


        #endregion

        public static void Initialize()
        {
            
        }

        public static void DrawTrack(Track track)
        {
            //Console.SetCursorPosition(cursorX, cursorY);
            if (track.Sections.First.Value.SectionType == SectionTypes.StartGrid && track.Sections.Last.Value.SectionType == SectionTypes.Finish)
            {
                // Set Startposition Cursor
                Console.SetCursorPosition(cursorX, cursorY);
                // Draw Each individual SectionType
                while (track.Sections.Count > 0)
                {
                    // Get current track part
                    string[] TrackPart = GetSectionType(track.Sections.First.Value.SectionType);

                    for (int j = 0; j < _start.Length; j++)
                    {
                        for (int k = 0; k < _start[j].Length; k++)
                        {
                            Console.Write(TrackPart[j][k].ToString());
                        }
                        // If track is going East
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

                    if (track.Sections.First.Value.SectionType == SectionTypes.LeftCorner) {
                        ClampOrientationMinus();
                    }
                    if (track.Sections.First.Value.SectionType == SectionTypes.RightCorner) {
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

                    
                    track.Sections.RemoveFirst();
                }
                //Draw Startgrid
                
            }
            else
            {
                Console.WriteLine("Track does not contain Start and / or Finish");
            }
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
                    if (Orientation == 1 || Orientation == 3) {
                        return _cornerLeftHtoV;
                    }
                    else {
                        return _cornerLeftVtoH;
                    }
                case SectionTypes.RightCorner:
                    if (Orientation == 1 || Orientation == 3) {
                        return _cornerRightHtoV;
                    }
                    else {
                        return _cornerRightVtoH;
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
