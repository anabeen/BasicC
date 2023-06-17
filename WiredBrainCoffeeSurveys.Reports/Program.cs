using System;
using System.Collections.Generic;
using System.IO;
using System.Numerics;
using System.Threading.Tasks;

namespace WiredBrainCoffeeSurveys.Reports
{
    class Program
    {
        static void Main(string[] args)
        {
            PrelimaryReport();

            CEOsQuestions();

            TaskReport();

        }

        private static void TaskReport()
        {
            List<string> tasks = new List<string>();

            var foodScoreAndCoffeeScore = (Q1Results.FoodScore > Q1Results.CoffeeScore) ? "Discuss with staff to improve coffee and check the ingredients" : "Talk with leadership to reward staff";
            tasks.Add(foodScoreAndCoffeeScore);

            // add task based on response rate we got from survey
            var responseRate = Q1Results.NumberResponded / Q1Results.NumberSurveyed;
            if (responseRate < 0.33)
            {
                tasks.Add("Create program to send out reminder to take survey");
            }
            else if (responseRate > 0.33 && responseRate < 0.66)
            {
                tasks.Add("Send out free coffee coupon to customers");
            }
            else
            {
                tasks.Add("Send out discount coffee coupon to customers");
            }

            // add task based on what area to improve on that user sent out in the response data
            switch (Q1Results.AreaToImprove)
            {
                case "MobileApp":
                    tasks.Add("Contact consulting firm about the app");
                    break;
                case "RewardsProgram ":
                    tasks.Add("Revisit the rewards deals");
                    break;
                case "Cleanliness":
                    tasks.Add("Contact the cleaning vendor");
                    break;

            }

            string writeToFile = "";

            // view in console the details
            for (int i = 0; i < tasks.Count; i++)
            {
                Console.WriteLine(tasks[i]);
                writeToFile  = $"{writeToFile} {tasks[i]}\n";
                
            }

            File.WriteAllText(path: @"C:\Users\nabeen\source\repos\csharp-program-flow\MyTaskReport.txt", writeToFile);
        }

        private static void CEOsQuestions()
        {
            // is coffee score higher than food score?
            var foodScoreAndCoffeeScore = (Q1Results.FoodScore > Q1Results.CoffeeScore) ? "Discuss with staff to improve coffee and check the ingredients" : "Talk with leadership to reward staff";
            Console.WriteLine(foodScoreAndCoffeeScore);

            //would customer recommend us?
            var WouldRecommend = (Q1Results.WouldRecommend >= 7) ? "Yes would recommend" : "No wouldn't recommend";
            Console.WriteLine(WouldRecommend);

            //Are granola and cappuccino the least and most popular products?
            var leastAndMostPopularProduct = (Q1Results.FavoriteProduct == "Cappucino" &&
                Q1Results.LeastFavoriteProduct == "Granola") ? "granola and cappuccino the least and most popular products" : "No, granola and cappuccino are not the least and most popular products";
            Console.WriteLine(leastAndMostPopularProduct);
        }

        private static void PrelimaryReport()
        {
            // find response rate
            var responseRate = (Q1Results.NumberResponded / Q1Results.NumberSurveyed) * 100;

            // find unanswered count of the survey
            var unansweredCount = Q1Results.NumberSurveyed - Q1Results.NumberResponded;

            // overall score 
            var overallScoreAvg = (Q1Results.ServiceScore + Q1Results.CoffeeScore
                + Q1Results.PriceScore + Q1Results.FoodScore) / 4;

            Console.WriteLine($"Response rate {responseRate}%");
            Console.WriteLine($"unanswered Response {unansweredCount}");
            Console.WriteLine($"Average score of the store {overallScoreAvg}");
        }
    }
}
