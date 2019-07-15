using Microsoft.ML;
using Microsoft.ML.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SentimentAnalysis.Classes
{
    public class ModelEvaluator
    {
        /// <summary>
        /// Using test data to validate the model's performance
        /// </summary>
        /// <param name="mLContext"></param>
        /// <param name="model"></param>
        /// <param name="testingDataset"></param>
        public static void Evaluate(MLContext mLContext, ITransformer model, IDataView testingDataset)
        {
            Console.WriteLine("<======== Evaluating Model accuracy with Test data ======>");

            IDataView predictions = model.Transform(testingDataset);
            var metrics = mLContext.BinaryClassification.Evaluate(predictions);

            Console.WriteLine("          Model quality metrics evaluation");
            Console.WriteLine("--------------------------------");
            Console.WriteLine($"          Accuracy: {metrics.Accuracy:P2}");
            Console.WriteLine($"          Auc: {metrics.AreaUnderRocCurve:P2}");
            Console.WriteLine($"          F1Score: {metrics.F1Score:P2}");
            Console.WriteLine("=============== End of model evaluation ===============");
        }
    }
}
