using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject.QuestionGeneratorStuff
{
    public class QuestionGenerator
    {
        private List<ImageType>? PotentialAnswerTypes { get; set; }
        private List<Number> Answers { get; set; }
        private Displayable QuestionPrompt { get; set; }
        public QuestionGenerator(int numOfAnswers = 4, List<ImageType>? potentialAnswerTypes = null, QuestionSuperType superType = QuestionSuperType.None, QuestionSubType subType = QuestionSubType.None)
        {

        }
        private void GeneratePrompt(QuestionSubType subType, QuestionSuperType superType, int numOfNumbersInQuestion = 0,int numOfNumbersInAnswer=1)
        {
            QuestionGeneratorNumberRange range;
            switch (subType)
            {
                case QuestionSubType.ToTen:
                    range = new QuestionGeneratorNumberRange(0, 10, 1);
                    break;
            }
            switch (superType)
            {
                case QuestionSuperType.FindGreatest:
                    numOfNumbersInAnswer = 1;
                    numOfNumbersInQuestion = 0;
                    QuestionPrompt = new Prompt("What is greater?");
                    break;
            }

        }
    }
}
