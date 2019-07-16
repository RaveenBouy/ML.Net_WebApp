using SentimentAnalysis.Classes;
using System;
using System.IO;

namespace SentimentAnalysis
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("<=============== Started Building The Model =============>");
            ModelBuilder.CreateModel();
            Console.WriteLine("<=============== Model Build is Complete ================>");
        }
    }
}
