using Microsoft.ML;
using SentimentAnalysisBuilder;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using static Microsoft.ML.DataOperationsCatalog;

namespace SentimentAnalysis.Classes
{
    public class ModelBuilder
    {
        //Create the MLContext object, which is the basis of ML.Net
        private static MLContext _mLContext = new MLContext();

        public static void CreateModel(SentimentList value)
        {
            //Get the dataset
            TrainTestData dataSet = DataSet.GetTrainTestDataset(_mLContext, value, 0.1);

            //Get the model training pipeline
            var pipeline = ModelTrainer.BuildTrainingPipeline(_mLContext, value);

            //Get the trained model
            var model = ModelTrainer.TrainModel(dataSet.TrainSet, pipeline);

            //Evaluate the Model
            ModelEvaluator.Evaluate(_mLContext, model, dataSet.TestSet);

            //Save the Model
            SaveModel(_mLContext, model, dataSet.TrainSet.Schema, value);
        }

        public static void SaveModel(MLContext mLContext, ITransformer model, DataViewSchema modelInputSchema, SentimentList value)
        {
            mLContext.Model.Save(model, modelInputSchema, FindPath(value));
            Console.WriteLine("<========================================================>");
            Console.WriteLine("<=== Model Saved to the current application directory ===>");
        }

        private static string FindPath(SentimentList value)
        {
            string modelName = "";

            switch (value)
            {
                case SentimentList.Common: modelName = Resource.Model_ML_Name_Common;
                    break;
                case SentimentList.Movie: modelName = Resource.Model_ML_Name_Movie;
                    break;
                case SentimentList.Shop: modelName = Resource.Model_ML_Name_Shop;
                    break;
            }

            var domain = AppDomain.CurrentDomain.BaseDirectory.Split(Path.DirectorySeparatorChar);
            StringBuilder path = new StringBuilder();

            for (int i = 0; i < domain.Length - 4; i++)
            {
                path.Append($"{domain[i]}/");
            }

            path.Append($@"Models/{modelName}");

            return path.ToString();
        }
    }
}
