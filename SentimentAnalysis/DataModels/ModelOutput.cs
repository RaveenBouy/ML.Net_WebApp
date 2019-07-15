using Microsoft.ML.Data;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace SentimentAnalysis.Models
{
    [DataContract]
    public class ModelOutput : ModelInput
    {
        /// <summary>
        /// The field that holds the final prediction
        /// </summary>
        [DataMember]
        [ColumnName("PredictedLabel")]
        public bool Prediction { get; set; }

        /// <summary>
        /// The score calibrated to the likelihood of the text having positive sentiment
        /// </summary>
        [DataMember]
        public float Probability { get; set; }

        /// <summary>
        /// The raw score calculated by the model
        /// </summary>
        [DataMember]
        public float Score { get; set; }
    }
}
