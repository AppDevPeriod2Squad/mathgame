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
        private Func<object, double> changingDoNotIncludeNumber;

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
        public double DoNotIncludeNumber
        {
            get { return GetExcluded(); } set { doNotIncludeNumber_ = value; }
        }
        private double doNotIncludeNumber_;
        private double min_;
        private double max_;
        public double Interval { get; set; } // is the amnt of steps inbetween each possible number
        // for example if you want every int from 1-9 the Interval would be 1
        private Random random = new Random();
        public Range(double min, double max,
            Func<object, double> changingMin = null,
            Func<object, double> changingMax = null,
            double doNotIncludeNumber = -1, 
            Func<object,double> changingDoNotIncludeNum = null,
            double interval = 1)
        {
            Min = min;
            Max = max;
            Interval = interval;
            doNotIncludeNumber_ = doNotIncludeNumber;
            this.changingDoNotIncludeNumber = changingDoNotIncludeNum;
            this.changingMin = changingMin;
            this .changingMax = changingMax;
        }
        public double GenerateRandom()
        {
            if (DoNotIncludeNumber < 0)
            {
                return random.Next((int)Min, (int)Max);
            }
            else
            {
                if (random.Next(0, 2) == 1){
                    return random.Next((int)Min, (int)DoNotIncludeNumber);
                }
                else
                {
                    if (DoNotIncludeNumber > Max)
                    {
                        return Max; // maybe temp??
                    }
                    return random.Next((int)DoNotIncludeNumber+1, (int)Max+1);
                }
            }
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
        private double GetExcluded()
        {
            if (changingDoNotIncludeNumber == null)
            {
                return doNotIncludeNumber_;
            }
            else
            {
                return changingDoNotIncludeNumber.Invoke(0);
            }
        }

    }

}
