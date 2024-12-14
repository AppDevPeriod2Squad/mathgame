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
        private Random rand;
        private Range potentialAnswerRange;
        private Range correctAnswerRange;
        private int numOfAnswers;
        private List<ImageType> potentialTypes;
        private List<SymbolType> symbolTypes;
        private List<Displayable> Answers { get; set; }
        private Displayable QuestionPrompt { get; set; }
        private GroupOfDisplayables CorrectAnswer {  get; set; }
        private GroupOfDisplayables currentGeneratingGroup;
        private int numOfNumbersInAnswers;
        private EventHandler handler;
        public QuestionGenerator(EventHandler? questionClickedHandler, int numOfAnswers = 4, List<ImageType>? potentialAnswerTypes = null, QuestionSuperType superType = QuestionSuperType.None, QuestionSubType subType = QuestionSubType.None)
        {
            Answers = new List<Displayable>();
            handler = questionClickedHandler;

        }
        private double GetSumMax()
        {
            double max = CorrectAnswer.EvaluateEquation();
            double currentTotal = currentGeneratingGroup.EvaluateEquation();
            max -= currentTotal;

           if (max < 0)
            {
                max = 0; // TEMP
            }
            return max;
        }
        private double GetSumMin(int genCount)
        {
            if (genCount+1 == numOfNumbersInAnswers)
            {
                return GetSumMax();
            }
            return 1;
        }
        public QuestionAndAnswers GeneratePromptQuestionSuperType(QuestionSuperType superType, QuestionSubType subType=QuestionSubType.None,List<ImageType> potentialTypes = null, List<SymbolType> possibleSymbolTypes = null)
        {
            superType = QuestionSuperType.Addition;
                //test code
            this.potentialTypes = potentialTypes;
            int numOfNumbersInQuestion = 0;
            numOfNumbersInAnswers = 0;
            numOfAnswers = 0;
            symbolTypes = possibleSymbolTypes;
            rand = new Random();
            potentialAnswerRange = new Range(0,0);
            correctAnswerRange = new Range(0,0);
            if (possibleSymbolTypes == null)
            {
                symbolTypes = new List<SymbolType>() { SymbolType.Plus};
            }
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
                    potentialAnswerRange = new Range(1,6,changingMax:changingMax=>(CorrectAnswer.EvaluateEquation()-1));
                    correctAnswerRange = new Range(6, 6);
                    promptString = "Which is greatest?";
                    numOfAnswers = 4;
                    break;
                case QuestionSuperType.Addition:
                    numOfNumbersInQuestion = 1;
                    numOfNumbersInAnswers = 2;
                    potentialAnswerRange = new Range(1, 6, changingMax: changingMax => (GetSumMax()-1));
                    correctAnswerRange = new Range(1, 6);
                    promptString = "What is the sum?";
                    numOfAnswers = 4;
                    break;
            }


            // later add functionallity to numOfNumbersInQuestions replacing certain parts of the prompt string
            QuestionPrompt = new Prompt(promptString);

            // generates the answers
            CorrectAnswer = GenQuestionList(correctAnswerRange);

            for (int i = 0; i < numOfAnswers; i++)
            {
                Answers.Add(GenQuestionList(potentialAnswerRange));
            }
            ImageType t = potentialTypes[rand.Next(potentialTypes.Count - 1)];
            Answers[rand.Next(0,Answers.Count-1)] = CorrectAnswer;

            return new QuestionAndAnswers(Answers, QuestionPrompt,correctAnswer:CorrectAnswer,questionClickedHandler:handler);
        }
        private GroupOfDisplayables GenQuestionList(Range range)
        {
            currentGeneratingGroup = new GroupOfDisplayables(new List<Displayable>());
            for (int i = 0; i < numOfNumbersInAnswers; i++)
            {
                int lowerBound = (int)range.Min;
                int upperBound = (int)range.Max;
                int generatedNum = rand.Next(lowerBound, upperBound + 1);
                ImageType type = potentialTypes[rand.Next(potentialTypes.Count - 1)];
                currentGeneratingGroup.DisplayableGroup.Add(new Number(val: generatedNum, imageType: type));
                if (i != numOfNumbersInAnswers - 1)
                {
                    currentGeneratingGroup.DisplayableGroup.Add(new Symbol(symbolType: symbolTypes[rand.Next(0, symbolTypes.Count - 1)]));
                }
            }
            return currentGeneratingGroup;
        }
    }
}
