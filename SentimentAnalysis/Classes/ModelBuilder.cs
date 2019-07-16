using Microsoft.ML;
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

        public static void CreateModel()
        {
            //Get the dataset
            TrainTestData dataSet = DataSet.GetTrainTestDataset(_mLContext, 0.1);

            //Get the model training pipeline
            var pipeline = ModelTrainer.BuildTrainingPipeline(_mLContext);

            //Get the trained model
            var model = ModelTrainer.TrainModel(dataSet.TrainSet, pipeline);

            //Evaluate the Model
            ModelEvaluator.Evaluate(_mLContext, model, dataSet.TestSet);

            //Save the Model
            SaveModel(_mLContext, model, dataSet.TrainSet.Schema);
        }

        public static void SaveModel(MLContext mLContext, ITransformer model, DataViewSchema modelInputSchema)
        {
            mLContext.Model.Save(model, modelInputSchema, FindPath());
            Console.WriteLine("<========================================================>");
            Console.WriteLine("<=== Model Saved to the current application directory ===>");
        }

        private static string FindPath()
        {
            var domain = AppDomain.CurrentDomain.BaseDirectory.Split(Path.DirectorySeparatorChar);
            StringBuilder path = new StringBuilder();

            for (int i = 0; i < domain.Length - 4; i++)
            {
                path.Append($"{domain[i]}/");
            }

            path.Append(@"Models/Model_ML_Common.zip");

            return path.ToString();
        }
    }
}
