using Microsoft.ML;
using SentimentAnalysis.Models;
using SentimentAnalysisBuilder;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using static Microsoft.ML.DataOperationsCatalog;

namespace SentimentAnalysis.Classes
{
    public class DataSet
    {
        private static string DataListPath { get; set; }

        private static IDataView GetDataset(MLContext mLContext)
        {
            return mLContext.Data.LoadFromTextFile<ModelInput>(DataListPath, separatorChar: ',', hasHeader: true, allowQuoting: true);
        }

        /// <summary>
        /// Get the split Dataset as Trainset and Testset, default Test Fraction is 0.2(20% from the entire dataset)
        /// </summary>
        /// <param name="mLContext"></param>
        /// <param name="testFraction"></param>
        /// <returns></returns>
        public static TrainTestData GetTrainTestDataset(MLContext mLContext, SentimentList value, double testFraction = 0.2)
        {
            FindPath(value);

            var dataView = GetDataset(mLContext);
            TrainTestData trainTest = mLContext.Data.TrainTestSplit(dataView, testFraction: testFraction);
            return trainTest;
        }

        private static void FindPath(SentimentList value)
        {
            string dataset = "";

            switch (value)
            {
                case SentimentList.Common:
                    dataset = Resource.Dataset_ML_Common;
                    break;
                case SentimentList.Movie:
                    dataset = Resource.Dataset_ML_Movie;
                    break;
                case SentimentList.Shop:
                    dataset = Resource.Dataset_ML_Shop;
                    break;
            }

            var domain = AppDomain.CurrentDomain.BaseDirectory.Split(Path.DirectorySeparatorChar);
            StringBuilder path = new StringBuilder();

            for (int i = 0; i < domain.Length - 4; i++)
            {
                path.Append($"{domain[i]}/");
            }

            DataListPath = path.Append($@"DataSet/{dataset}").ToString();
        }
    }
}
