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
        [Route("WasserFüllen")]
        public IActionResult wasserAuffuellen(double menge)
        {
            double wasseraufgefüllt = menge + maschine.Wasser;

            // do this in the class
            if(wasseraufgefüllt > maschine.maxWasser)
            {
                return Problem(detail:"to much Water");
            }
            maschine.Wasser = wasseraufgefüllt;
            return Ok("Water has been filled up by " + menge + " water now: " + wasseraufgefüllt );
        }

        [HttpPut]
        [Route("BohnenFüllen")]
        public IActionResult BohnenAuffuellen(double menge)
        {
            double bohnenaufgefüllt = menge + maschine.Kaffeebohnen;
            if (bohnenaufgefüllt > maschine.maxBohnen)
            {
                return Problem(detail: "to much Beans");
            }
            maschine.Kaffeebohnen = bohnenaufgefüllt;
            return Ok("Beans has been filled up by " + menge + " Beans now: " + bohnenaufgefüllt);
        }

        [HttpPut]
        [Route("KaffeeZubereiten")] //                     1                           1
        public IActionResult Kaffeezubereiten(double WasserZuKaffeeVerhältnis, double produzierenderKaffe)
        {
            
            double coffee = produzierenderKaffe / WasserZuKaffeeVerhältnis;
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