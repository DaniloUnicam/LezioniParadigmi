using Microsoft.AspNetCore.Mvc;
using System.Net;
using Unicam.Paradigmi.Test.Models;

namespace Unicam.Paradigmi.Web.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class AziendeController : ControllerBase
    {
        public List<Azienda> aziende = new List<Azienda>();

        public AziendeController() {
            this.aziende.Add(new Azienda()
            {
                IdAzienda = 1,
                Citta = "Tolentino",
                RagioneSociale = "PCSNET",
                Cap = "54321"
            });

        }

        [HttpGet]
        [Route("list")]
        public IEnumerable<Azienda> GetAziende()
        {
            return aziende;
        }

        [HttpGet]
        //id:int significa che prenderà il parametro dal body (del metodo) nome id, tipo int
        [Route("get/{id:int}")]
        public Azienda getAzienda(int id) {
            return aziende.Where(w => w.IdAzienda == id).First();
        }

        [HttpPost]
        [Route("new")]
        public IActionResult CreateAzienda([FromBody]Azienda azienda)
        {
            return Ok();
        }
    }
}
