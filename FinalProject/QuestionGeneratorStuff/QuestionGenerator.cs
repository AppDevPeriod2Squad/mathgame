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
        private void GeneratePrompt(QuestionSubType subType, QuestionSuperType superType)
        {
            int numOfNumbersInQuestion = 0;
            switch (subType)
            {
                case QuestionSubType.ToTen:
                    numOfNumbersInQuestion = 2;
                    break;
            }

        }
    }
}
