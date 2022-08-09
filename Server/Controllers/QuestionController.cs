using BlazorPOC2.Shared;
using BlazorPOC2.Shared.Common;
using BlazorPOC2.Shared.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BlazorPOC2.Server.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class QuestionController : ControllerBase
    {
        private readonly ILogger<QuestionController> _logger;
        public QuestionController(ILogger<QuestionController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public Task<List<Question>> Get()
        {
            return Task.FromResult(QuestionData.GetQuestions().ToList());
        }
    }
}
