using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject.QuestionGeneratorStuff
{
    public class QuestionGenerator
    {
        private List<ImageType>? PotentialAnswerTypes { get; set; }
        private List<Displayable> Answers { get; set; }
        private Displayable QuestionPrompt { get; set; }
        private double CorrectAnswer {  get; set; } 
        public QuestionGenerator(int numOfAnswers = 4, List<ImageType>? potentialAnswerTypes = null, QuestionSuperType superType = QuestionSuperType.None, QuestionSubType subType = QuestionSubType.None)
        {
            Answers = new List<Displayable>();

        }
        public QuestionAndAnswers GeneratePromptQuestionSuperType(QuestionSuperType superType, QuestionSubType subType=QuestionSubType.None,List<ImageType> potentialTypes = null)
        {
            int numOfNumbersInQuestion = 0;
            int numOfNumbersInAnswers = 0;
            int numOfAnswers = 0;
            Random rand = new Random();
            Range potentialAnswerRange = new Range(0,0);
            Range correctAnswerRange = new Range(0,0);
            if (potentialTypes == null)
            {
                potentialTypes = new List<ImageType>() { ImageType.Dice };
            }
            String promptString = "";
            switch (superType)
            {
                case QuestionSuperType.FindGreatest:
                    numOfNumbersInQuestion = 0;
                    numOfNumbersInAnswers = 1;
                    potentialAnswerRange = new Range(1,6,changingMax:changingMax=>(CorrectAnswer-1));
                    correctAnswerRange = new Range(6, 6);
                    promptString = "Which is greatest?";
                    numOfAnswers = 4;
                    break;
            }


            // later add functionallity to numOfNumbersInQuestions replacing certain parts of the prompt string
            QuestionPrompt = new Prompt(promptString);

            // generates the answers
            CorrectAnswer = rand.Next((int)(correctAnswerRange.Min), (int)correctAnswerRange.Max + 1);
            for (int i = 0; i < numOfAnswers; i++)
            {
                int upperBound = (int)potentialAnswerRange.Max;
                int generatedNum = rand.Next((int)(potentialAnswerRange.Min), upperBound+1);
                ImageType type = potentialTypes[rand.Next(potentialTypes.Count-1)];
                Answers.Add(new Number(val:generatedNum,imageType:type));
                
            }
            ImageType t = potentialTypes[rand.Next(potentialTypes.Count - 1)];
            Answers[rand.Next(0,Answers.Count-1)] = (new Number(val: (int)CorrectAnswer, imageType: t));

            return new QuestionAndAnswers(Answers, QuestionPrompt);
        }
    }
}
