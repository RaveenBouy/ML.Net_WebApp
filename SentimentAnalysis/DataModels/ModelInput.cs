using Microsoft.ML.Data;
using System.Runtime.Serialization;

namespace SentimentAnalysis.Models
{
    [DataContract]
    public class ModelInput
    {   
        /// <summary>
        /// The Column that holds the Text
        /// </summary>
        [DataMember]
        [LoadColumn(0)]
        public string SentimentText { get; set; }

        /// <summary>
        /// The column that holds the Binary sentiment(0 or 1)
        /// </summary>
        [LoadColumn(1), ColumnName("Label")]
        public bool Sentiment { get; set; }    
    }
}
