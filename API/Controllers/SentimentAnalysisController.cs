using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SentimentAnalysis.Classes;
using SentimentAnalysis.Models;

namespace API.Controllers
{
    [ApiController]
    public class SentimentAnalysisController : Controller
    {
        [HttpGet("Api/SentimentAnalysis/Common")]
        public async Task<ModelOutput> TestAsync([FromQuery] string text)
        {
            return await ModelConsumer.GetSentimentAsync(text, SentimentList.Common);
        }
    }
}