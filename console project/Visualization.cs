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
            "----",
            "  | ",
            "  | ",
            "----"
        };

        private static string[] _finishHorizontal = {
            "----",
            "  # ",
            "  # ",
            "----"
        };

        private static string[] _finishVertical = {
            "|  |",
            "|##|",
            "|  |",
            "|  |"
        };

        private static string[] _straightHorizontal = {
            "----",
            "    ",
            "    ",
            "----"
        };

        private static string[] _straightVertical = {
            "|  |",
            "|  |",
            "|  |",
            "|  |"
        };

        private static string[] _cornerRightHtoV = {
            @"--\ ",
            @"   \",
            @"\  |",
            @"|  |"
        };

        private static string[] _cornerRightVtoH = {
            @"|  |",
            @"/  |",
            @"   /",
            @"--/ "
        };

        private static string[] _cornerLeftHtoV = {
            @"|  |",
            @"|  \",
            @"\   ",
            @" \--"
        };

        private static string[] _cornerLeftVtoH = {
            @" /--",
            @"/   ",
            @"|  /",
            @"|  |"
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
                        Console.SetCursorPosition(cursorX, cursorY);
                    }
                    if (track.Sections.First.Value.SectionType == SectionTypes.LeftCorner || track.Sections.First.Value.SectionType == SectionTypes.RightCorner) {
                        if (isStraight) {
                            isStraight = false;
                        }
                        else {
                            isStraight = true;
                        }
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
                        cursorX = cursorX + TrackSize;
                        return _straightHorizontal;
                    }
                    else {
                        cursorY = cursorY + TrackSize;
                        return _straightVertical;
                    }
                case SectionTypes.LeftCorner:
                    if (Orientation == 1 || Orientation == 3) {
                        //cursorX = cursorX + 4;
                        return _cornerLeftHtoV;
                    }
                    else {
                        return _cornerLeftVtoH;
                    }
                case SectionTypes.RightCorner:
                    if (isStraight)
                        return _cornerRightHtoV;
                    else
                        return _cornerRightVtoH;
                case SectionTypes.StartGrid:
                    cursorX = cursorX + TrackSize;
                    return _start;
                case SectionTypes.Finish:
                    if (isStraight)
                        return _finishHorizontal;
                    else
                        return _finishVertical;
                default:
                    return null;
            }
        }
    }
}
