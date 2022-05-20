using Microsoft.AspNetCore.Mvc;

namespace Kaffeeproject.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class KaffeemaschinenController : ControllerBase
    {
        private readonly ILogger<KaffeemaschinenController> _logger;

        private static Kaffeemaschine maschine = new Kaffeemaschine();
        public KaffeemaschinenController(ILogger<KaffeemaschinenController> logger)
        {
            _logger = logger;
        }


        [HttpGet()]
        [Route("wasser")]
        public double GetWasser()
        {
            return maschine.Wasser;
        }

        [HttpGet()]
        [Route("bohen")]
        public double GetBohnen()
        {
            return maschine.Kaffeebohnen;
        }



        [HttpPut]
        [Route("WasserF�llen")]
        public IActionResult wasserAuffuellen(double menge)
        {
            double wasseraufgef�llt = menge + maschine.Wasser;

            // do this in the class
            if(wasseraufgef�llt > maschine.maxWasser)
            {
                return Problem(detail:"to much Water");
            }
            maschine.Wasser = wasseraufgef�llt;
            return Ok("Water has been filled up by " + menge + " water now: " + wasseraufgef�llt );
        }

        [HttpPut]
        [Route("BohnenF�llen")]
        public IActionResult BohnenAuffuellen(double menge)
        {
            double bohnenaufgef�llt = menge + maschine.Kaffeebohnen;
            if (bohnenaufgef�llt > maschine.maxBohnen)
            {
                return Problem(detail: "to much Beans");
            }
            maschine.Kaffeebohnen = bohnenaufgef�llt;
            return Ok("Beans has been filled up by " + menge + " Beans now: " + bohnenaufgef�llt);
        }

        [HttpPut]
        [Route("KaffeeZubereiten")] //                     1                           1
        public IActionResult Kaffeezubereiten(double WasserZuKaffeeVerh�ltnis, double produzierenderKaffe)
        {
            
            double coffee = produzierenderKaffe / WasserZuKaffeeVerh�ltnis;
            double water = produzierenderKaffe - coffee;

            if(coffee > maschine.Kaffeebohnen)
            {
                return Problem(detail: "to less Water");
            }
            else if(water > maschine.Wasser)
            {
                return Problem(detail: "to less Water");
            }

            maschine.Kaffeebohnen -= coffee;
            maschine.Wasser -= water;
            return Ok("water: " + water + " coffee: " + coffee);
        }
    }
}