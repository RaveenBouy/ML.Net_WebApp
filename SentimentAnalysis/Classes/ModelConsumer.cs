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
            Stream modelStream = new MemoryStream(Resource.Model_ML_Common);

            // Load the model
            ITransformer mlModel = mlContext.Model.Load(modelStream, out DataViewSchema inputSchema);
            var predEngine = mlContext.Model.CreatePredictionEngine<ModelInput, ModelOutput>(mlModel);

            // Try a single prediction
            ModelOutput predictionResult = predEngine.Predict(new ModelInput { SentimentText = sentiment });

            return predictionResult;
        }

        private static async System.Threading.Tasks.Task<byte[]> GetSentimentTypeAsync(SentimentList value)
        {
            FindPath();

            switch (value)
            {
                case SentimentList.Common:
                    {
                        return File.Exists(Resource.SA_Model_Common) ? await File.ReadAllBytesAsync(Resource.SA_Model_Common) : throw new FileNotFoundException();                    
                    }
                case SentimentList.Movie:
                    {
                        return File.Exists(Resource.SA_Model_Movie) ? await File.ReadAllBytesAsync(Resource.SA_Model_Movie) : throw new FileNotFoundException();
                    }
                case SentimentList.Shop:
                    {
                        return File.Exists(Resource.SA_Model_Shop) ? await File.ReadAllBytesAsync(Resource.SA_Model_Shop) : throw new FileNotFoundException();
                    }
                default:
                    return null;
            }
        }

        private static string FindPath()
        {
            var wtf = File.Exists(Resource.SA_Model_Common);
            var domain = AppDomain.CurrentDomain.BaseDirectory.Split(Path.DirectorySeparatorChar);
            var aaa = Path.GetDirectoryName(Environment.CurrentDirectory);

            var files = Directory.GetFiles(@"/app");
            var files2 = Directory.GetFiles(@"/bin");
            string[] filesss = Directory.GetFiles(@"/", "Model_ML_Common.zip", SearchOption.AllDirectories);


            StringBuilder path = new StringBuilder();

            for (int i = 0; i < domain.Length - 4; i++)
            {
                path.Append($"{domain[i]}/");
            }

            path.Append(@"Models/Model_ML_Common.zip");

            return path.ToString();
        }
    }

    public enum SentimentList
    {
        Common,
        Movie,
        Shop
    }
}
