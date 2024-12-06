
using System;

namespace FinalProject
{
    public class QuestionAndAnswers : Displayable
    {
        public List<Number> AnswerOptions { get; set; }
        public Displayable Question {  get; set; }

        public QuestionAndAnswers(List<int> numberOptions = null, ImageType imageType = ImageType.None, int[] questionSize = null, int[] promptSize=null,String questionPrompt="BASE PROMPT"): base()
        {
            if (questionSize == null)
            {
                questionSize = new int[] {Width/3,Height/3}; // default question size
            }
            if (promptSize == null)
            {
                promptSize = new int[] { Width / 3, Height / 3 }; // default question size
            }
            SetOptions(numberOptions, imageType, questionSize);

        }
        private void SetQuestionPrompt(String promptString, int[] promptSize)
        {
            // make a text class after kiran implements the Displayable constructor
            Question = new Text(x: x + Width / 2, y: 0, width: promptSize[0], height: promptSize[1]);
        }
        private void SetOptions(List<int> nums,ImageType numType, int[] size)
        {
            AnswerOptions = new List<Number>();
            int y = yStart + Height - size[1];
            for (int xIndex = 0; x < nums.Count; x++)
            {
                int x = xStart + xIndex * size[0];
                AnswerOptions.Add(new Number(val: nums[x], imageType: numType, width: size[0], height: size[1],y:y,x:x));
            }
        }

    }
}
