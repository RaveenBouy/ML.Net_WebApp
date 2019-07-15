using Microsoft.ML;
using Microsoft.ML.Trainers;
using SentimentAnalysis.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SentimentAnalysis.Classes
{
    public class ModelTrainer
    {
        public static IEstimator<ITransformer> BuildTrainingPipeline(MLContext mLContext)
        {
            var dataProcessPipeline = mLContext.Transforms.Text.FeaturizeText(outputColumnName: "Features", inputColumnName: nameof(ModelInput.SentimentText));
            var trainer = mLContext.BinaryClassification.Trainers.SgdCalibrated(new SgdCalibratedTrainer.Options() { LearningRate = 0.5f, L2Regularization = 1E-06f, ConvergenceTolerance = 0.0001f, NumberOfIterations = 50, Shuffle = true, LabelColumnName = "Label", FeatureColumnName = "Features" });
            var trainingPipeline = dataProcessPipeline.Append(trainer);

            return trainingPipeline;
        }

        /// <summary>
        /// Train the Model with Training Dataset
        /// </summary>
        /// <param name="mLContext"></param>
        /// <param name="trainingDataset"></param>
        /// <returns></returns>
        public static ITransformer TrainModel(IDataView trainingDataset, IEstimator<ITransformer> trainingPipeline)
        {
            Console.WriteLine("<=============== Initiated Model Training ===============>");

            ITransformer model = trainingPipeline.Fit(trainingDataset);

            Console.WriteLine("<=============== Model Training Is Complete =============>");

            return model;
        }
    }
}
