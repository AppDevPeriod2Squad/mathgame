using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject.QuestionGeneratorStuff
{
    public class QuestionGeneratorNumberRange
    {
        // Mins and Maxes are INCLUSIVE of themselves
        public double Min { get; set; }
        public double Max { get; set; }
        public double Interval { get; set; } // is the amnt of steps inbetween each possible number
        // for example if you want every int from 1-9 the Interval would be 1
        public QuestionGeneratorNumberRange(double min, double max, double interval = 1)
        {
            Min = min;
            Max = max;
            Interval = interval;
        }
    }
}
