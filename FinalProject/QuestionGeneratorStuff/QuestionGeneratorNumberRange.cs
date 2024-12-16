using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject.QuestionGeneratorStuff
{
    public class Range
    {
        private Func<object, double> changingMax;
        private Func<object, double> changingMin;
        // Mins and Maxes are INCLUSIVE of themselves
        public double Min
        {
            get { return GetMin(); }
            set { min_ = value; }
        }
        public double Max 
        { 
            get { return GetMax(); }
            set { max_ = value; }
        }
        private double min_;
        private double max_;
        public double Interval { get; set; } // is the amnt of steps inbetween each possible number
        // for example if you want every int from 1-9 the Interval would be 1
        public Range(double min, double max, Func<object, double> changingMin = null, Func<object, double> changingMax = null, double interval = 1)
        {
            Min = min;
            Max = max;
            Interval = interval;
            this.changingMin = changingMin;
            this .changingMax = changingMax;
        }
        private double GetMin()
        {
            if (changingMin == null)
            {
                return min_;
            }
            else
            {
                return changingMin.Invoke(0);
            }
        }
        private double GetMax()
        {
            if (changingMax == null)
            {
                return max_;
            }
            else
            {
                return changingMax.Invoke(0);
            }
        }

    }

}
