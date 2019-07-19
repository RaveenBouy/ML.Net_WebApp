using Microsoft.ML;
using SentimentAnalysis.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace SentimentAnalysis.Classes
{
    public class ModelConsumer
    {
        /// <summary>
        /// Pass the string that wanted to be evaluated, returns false for toxic text and true for non-toxic text.
        /// </summary>
        /// <param name="sentiment"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static ModelOutput GetSentiment(string sentiment, SentimentList value)
        {
            MLContext mlContext = new MLContext();
            Stream modelStream = new MemoryStream(GetSentimentModel(value));

            // Load the model
            ITransformer mlModel = mlContext.Model.Load(modelStream, out DataViewSchema inputSchema);
            var predEngine = mlContext.Model.CreatePredictionEngine<ModelInput, ModelOutput>(mlModel);

            // Try a single prediction
            ModelOutput predictionResult = predEngine.Predict(new ModelInput { SentimentText = sentiment });

            return predictionResult;
        }

        private static byte[] GetSentimentModel(SentimentList value)
        {
            string modelName = "";

            switch (value)
            {
                case SentimentList.Common:
                    {
                        modelName = "Model_ML_Common.zip";
                        break;
                    }
                case SentimentList.Movie:
                    {
                        modelName = "Model_ML_Movie.zip";
                        break;
                    }
                case SentimentList.Shop:
                    {
                        modelName = "Model_ML_Shop.zip";
                        break;
                    }
            }

            return File.ReadAllBytes(modelName);
        }
    }
}
