using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SortingAlgorithmsSimulation
{
    public enum MoveType
    {
        LINE_TO_BOTTOM,
        BOTTOM_TO_LINE,
        LEFT_TO_RIGHT,
        RIGHT_TO_LEFT,
        LEFT_TO_RIGHT_OR_RIGHT_TO_LEFT,
        LEFT_TO_RIGHT_AND_RIGHT_TO_LEFT,
        CHANGED
    }

    public class Status
    {
        public MoveType mType { get; set; }
        public int Pos1 { get; set; }
        public int Pos2 { get; set; }
    }
}
