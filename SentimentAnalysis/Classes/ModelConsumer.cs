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
        public static async System.Threading.Tasks.Task<ModelOutput> GetSentimentAsync(string sentiment, SentimentList value)
        {
            MLContext mlContext = new MLContext();
            Stream modelStream = new MemoryStream(await GetSentimentTypeAsync(value));

            ITransformer mlModel = mlContext.Model.Load(modelStream, out DataViewSchema inputSchema);
            var predEngine = mlContext.Model.CreatePredictionEngine<ModelInput, ModelOutput>(mlModel);

            // Try a single prediction
            ModelOutput predictionResult = predEngine.Predict(new ModelInput { SentimentText = sentiment });

            return predictionResult;
        }

        private static async System.Threading.Tasks.Task<byte[]> GetSentimentTypeAsync(SentimentList value)
        {
            switch (value)
            {
                case SentimentList.Common:
                    {
                        return File.Exists(Resource.SA_Model_Common) ? await File.ReadAllBytesAsync(Resource.SA_Model_Common) : null;                    
                    }
                case SentimentList.Movie:
                    {
                        return File.Exists(Resource.SA_Model_Movie) ? await File.ReadAllBytesAsync(Resource.SA_Model_Movie) : null;
                    }
                case SentimentList.Shop:
                    {
                        return File.Exists(Resource.SA_Model_Shop) ? await File.ReadAllBytesAsync(Resource.SA_Model_Shop) : null;
                    }
                default:
                    return null;
            }
        }
    }

    public enum SentimentList
    {
        Common,
        Movie,
        Shop
    }
}
