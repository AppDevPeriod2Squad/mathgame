using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject
{
    public class QuestionEventArgs : EventArgs
    {
        public bool WasCorrect { get; set; }
        public QuestionEventArgs(bool wasCorrect)
        {
            WasCorrect = wasCorrect;
        }
    }
}
