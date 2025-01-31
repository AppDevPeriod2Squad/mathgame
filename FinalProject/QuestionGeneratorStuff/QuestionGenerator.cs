﻿using System;
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
            handler = questionClickedHandler;
            this.potentialTypes = potentialAnswerTypes;
        }
        private double GetSumExcludedNum()
        {
            double max = CorrectAnswer.EvaluateEquation();
            double currentTotal = currentGeneratingGroup.EvaluateEquation();
            max -= currentTotal;
            if (max < 1)
            {
                max = 1;
            }
            return max;
        }
        private double GetDifferenceExcludedNum()
        {
            double max = CorrectAnswer.EvaluateEquation();
            double currentTotal = currentGeneratingGroup.EvaluateEquation();
            max = currentTotal - max;
            if (max < 1)
            {
                max = 1;
            }
            return max;
        }
        private double GetProductExcludedNum()
        {
            double max = CorrectAnswer.EvaluateEquation();
            double currentTotal = currentGeneratingGroup.EvaluateEquation();
            if (currentTotal != 0)
            {
                max /= currentTotal;
            }
            
            if (max < 1)
            {
                max = 1;
            }
           
            return max;
        }
        private double GetDivisionExcludedNum()
        {
            double max = CorrectAnswer.EvaluateEquation();
            double currentTotal = currentGeneratingGroup.EvaluateEquation();
            if (currentTotal != 0)
            {
                max /= currentTotal;
                max = 1 / max;
            }

            if (max < 1)
            {
                max = 1;
            }

            return max;
        }

            public QuestionAndAnswers Generate(QuestionSuperType superType, QuestionSubType subType=QuestionSubType.None, List<SymbolType> possibleSymbolTypes = null)
        {
            Answers = new List<Displayable>();
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
                    correctAnswerRange = new Range(2, 6);
                    promptString = "Which is greatest?";
                    numOfAnswers = 4;
                    break;
                case QuestionSuperType.Addition:
                    numOfNumbersInQuestion = 1;
                    numOfNumbersInAnswers = 2;
                    potentialAnswerRange = new Range(1, 6,changingDoNotIncludeNum:changingDoNotIncludeNum=> GetSumExcludedNum());
                    correctAnswerRange = new Range(1, 6);
                    promptString = "What adds up to {replace}?";
                    numOfAnswers = 4;
                    break;
                case QuestionSuperType.Multiplication:
                    numOfNumbersInQuestion = 1;
                    numOfNumbersInAnswers = 2;
                    numOfAnswers = 4;
                    promptString = "What multiplies to {replace}?";
                    correctAnswerRange = new Range(1, 6);
                    potentialAnswerRange = new Range(1, 6, changingDoNotIncludeNum: changingDoNotIncludeNum => GetProductExcludedNum());
                    symbolTypes = new List<SymbolType>() { SymbolType.Multiply };
                    break;
                case QuestionSuperType.Division:
                    numOfNumbersInQuestion = 1;
                    numOfNumbersInAnswers = 3;
                    numOfAnswers = 4;
                    promptString = "What divides to {replace}?";
                    correctAnswerRange = new Range(1, 6);
                    potentialAnswerRange = new Range(1, 6, changingDoNotIncludeNum: changingDoNotIncludeNum => GetDivisionExcludedNum());
                    symbolTypes = new List<SymbolType>() { SymbolType.Division };
                    break;
                case QuestionSuperType.Subtraction:
                    numOfNumbersInQuestion = 1;
                    numOfNumbersInAnswers = 2;
                    potentialAnswerRange = new Range(1, 6, changingDoNotIncludeNum: changingDoNotIncludeNum => GetDifferenceExcludedNum());
                    correctAnswerRange = new Range(1, 6);
                    promptString = "What subtracts to {replace}?";
                    numOfAnswers = 4;
                    symbolTypes = new List<SymbolType>() { SymbolType.Minus };
                    break;
            }


            // later add functionallity to numOfNumbersInQuestions replacing certain parts of the prompt string

            // generates the answers
            CorrectAnswer = GenQuestionList(correctAnswerRange);

            for (int i = 0; i < numOfAnswers; i++)
            {
                Answers.Add(GenQuestionList(potentialAnswerRange));
            }
            Answers[rand.Next(0,Answers.Count)] = CorrectAnswer;
            promptString = EditPromptString(promptString);
            QuestionPrompt = new Prompt(promptString);
            return new QuestionAndAnswers(Answers, QuestionPrompt,correctAnswer:CorrectAnswer,questionClickedHandler:handler);
        }
        private GroupOfDisplayables GenQuestionList(Range range)
        {
            currentGeneratingGroup = new GroupOfDisplayables(new List<Displayable>());
            for (int i = 0; i < numOfNumbersInAnswers; i++)
                {
                //    int lowerBound = (int)range.Min;
                //    int upperBound = (int)range.Max;
                //    int generatedNum = rand.Next(lowerBound, upperBound + 1);
                int generatedNum = (int)range.GenerateRandom();
                ImageType type = potentialTypes[rand.Next(potentialTypes.Count)];

                currentGeneratingGroup.DisplayableGroup.Add(new Number(val: generatedNum, imageType: type));
                if (i != numOfNumbersInAnswers - 1)
                {
                    currentGeneratingGroup.DisplayableGroup.Add(new Symbol(symbolType: symbolTypes[rand.Next(0, symbolTypes.Count - 1)]));
                }
            }
            return currentGeneratingGroup;
        }
        private String EditPromptString(String prompt)
        {
            String editedPrompt = "";
            bool reading = false;
            for (int i = 0; i < prompt.Length;i++)
            {
                Char c = prompt[i];
                if (c == '{')
                {
                    reading = true;
                }
                else if (c == '}')
                {
                    reading = false;
                    editedPrompt += CorrectAnswer.EvaluateEquation();
                }
                else if (!reading)
                {
                    editedPrompt += c;
                }
            }
            return editedPrompt;
        }
    }
}
