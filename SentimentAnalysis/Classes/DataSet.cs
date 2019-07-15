using Microsoft.ML;
using SentimentAnalysis.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using static Microsoft.ML.DataOperationsCatalog;

namespace SentimentAnalysis.Classes
{
    public class DataSet
    {
        private static readonly string _dataListPath = Path.Combine(Environment.CurrentDirectory, "dataset.csv");

        private static IDataView GetDataset(MLContext mLContext)
        {
            return mLContext.Data.LoadFromTextFile<ModelInput>(_dataListPath, separatorChar: ',', hasHeader: true, allowQuoting: true);
        }

        /// <summary>
        /// Get the split Dataset as Trainset and Testset, default Test Fraction is 0.2(20% from the entire dataset)
        /// </summary>
        /// <param name="mLContext"></param>
        /// <param name="testFraction"></param>
        /// <returns></returns>
        public static TrainTestData GetTrainTestDataset(MLContext mLContext, double testFraction = 0.2)
        {
            var dataView = GetDataset(mLContext);
            TrainTestData trainTest = mLContext.Data.TrainTestSplit(dataView, testFraction: testFraction);
            return trainTest;
        }
    }
}
