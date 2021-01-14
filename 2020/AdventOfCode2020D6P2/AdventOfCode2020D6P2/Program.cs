using System;
using System.Collections.Generic;

namespace AdventOfCodeD6P2
{
    class Program
    {
        static void Main(string[] args)
        {
            string questionFile = System.IO.File.ReadAllText(@"C:\Users\aalinn\source\repos\AdventOfCode\2020\AdventOfCode2020D6P1\QuestionFile.txt");

            questionFile = questionFile.Replace(Environment.NewLine, "|");
            questionFile = questionFile.Replace("||", "*");
            questionFile = questionFile.Replace("|", ",");

            string[] initialQuestionList = questionFile.Split('*');

            List<string[]> questionList = new List<string[]>();

            foreach (string questionSet in initialQuestionList)
            {
                string[] familyQuestion = questionSet.Split(',');

                questionList.Add(familyQuestion);
            }

            int totalYesByGroup = 0;

            foreach (char letter in "abcdefghijklmnopqrstuvwxyz")
            {
                foreach (string[] questionSet in questionList)
                {
                    bool countLetter = true;
                    foreach (string question in questionSet)
                    {
                        if (!question.Contains(letter))
                        {
                            countLetter = false;
                            break;
                        }
                    }
                    
                    if (countLetter)
                    {
                        totalYesByGroup++;
                    }
                }
            }

            Console.WriteLine($"Total Yes Answers by Group: {totalYesByGroup}");

        }
    }
}
