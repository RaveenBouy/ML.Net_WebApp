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
        /// <returns></returns>
        public static ModelOutput GetSentiment(string sentiment)
        {
            MLContext mlContext = new MLContext();
            ITransformer mlModel = mlContext.Model.Load(Path.Combine(Environment.CurrentDirectory, "Model_ML.zip"), out DataViewSchema inputSchema);
            var predEngine = mlContext.Model.CreatePredictionEngine<ModelInput, ModelOutput>(mlModel);

            // Try a single prediction
            ModelOutput predictionResult = predEngine.Predict(new ModelInput { SentimentText = sentiment });

            return predictionResult;
        }
    }
}
