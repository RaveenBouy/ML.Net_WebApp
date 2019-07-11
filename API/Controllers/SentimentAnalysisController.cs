using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    public class SentimentAnalysisController : Controller
    {
        [HttpGet("api/sa/test")]
        public string Test()
        {
            return "Test 123";
        }
    }
}