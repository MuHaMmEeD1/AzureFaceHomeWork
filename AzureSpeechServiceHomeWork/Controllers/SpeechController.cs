using Microsoft.AspNetCore.Mvc;
using Microsoft.CognitiveServices.Speech;
using System.Threading.Tasks;

namespace AzureSpeechServiceHomeWork.Controllers
{
    

    namespace YourProject.Controllers
    {
        public class SpeechController : Controller
        {
            private readonly IConfiguration _configuration;

            public SpeechController(IConfiguration configuration)
            {
                _configuration = configuration;
            }

            public IActionResult Index()
            {
                return View();
            }

            [HttpPost]
            public async Task<IActionResult> TextToSpeech(string text)
            {
                var speechKey = _configuration["AzureSpeech:Key"];
                var speechRegion = _configuration["AzureSpeech:Region"];

                var config = SpeechConfig.FromSubscription(speechKey, speechRegion);

                using (var synthesizer = new SpeechSynthesizer(config))
                {
                    var result = await synthesizer.SpeakTextAsync(text);
                    if (result.Reason == ResultReason.SynthesizingAudioCompleted)
                    {
                        ViewBag.Message = "Speech synthesis succeeded!";
                    }
                    else
                    {
                        ViewBag.Message = $"Speech synthesis failed: {result.Reason}";
                    }
                }

                return View("Index");
            }
        }
    }

}
