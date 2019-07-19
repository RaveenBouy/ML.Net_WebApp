using SentimentAnalysis.Classes;
using System;
using System.IO;

namespace SentimentAnalysis
{
    class Program
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            var sentimentType = SentimentList.Shop;

            Console.WriteLine("<=============== Started Building The Model =============>");
            ModelBuilder.CreateModel(sentimentType);
            Console.WriteLine("<=============== Model Build is Complete ================>");
        }
    }
}