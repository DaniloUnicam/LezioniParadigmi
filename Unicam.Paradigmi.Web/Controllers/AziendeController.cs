using Microsoft.AspNetCore.Mvc;
using System.Net;
using Unicam.Paradigmi.Application.Abstractions.Services;
using Unicam.Paradigmi.Application.Models.Requests;
using Unicam.Paradigmi.Application.Models.Responses;
using Unicam.Paradigmi.Application.Validators;
using Unicam.Paradigmi.Models.Repositories;
using Unicam.Paradigmi.Test.Models;

namespace Unicam.Paradigmi.Web.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class AziendeController : ControllerBase
    {

        public readonly IAziendaService _aziendaService;

        public AziendeController(IAziendaService aziendaService) {
            this._aziendaService = aziendaService;
        }

        [HttpGet]
        [Route("list")]
        public IEnumerable<Azienda> GetAziende()
        {
            return this._aziendaService.GetAziende();
        }

        [HttpGet]
        //id:int significa che prenderà il parametro dal body (del metodo) nome id, tipo int
        [Route("get/{id:int}")]
        public Azienda getAzienda(int id) {
            return _aziendaService.GetAzienda(id);
        }

        //tramite [FromBody], forzo il metodo nel prendere il body dal parametro
        [HttpPost]
        [Route("new")]
        public IActionResult CreateAzienda(CreateAziendaRequest request)
        {
            var validator = new CreateAziendaRequestValidator();
            validator.Validate(request);
            var azienda = request.toEntity();
            _aziendaService.AddAzienda(azienda);

            var response = new CreateAziendaResponse();
            response.Azienda = new Application.Models.Dtos.AziendaDTO(azienda);
            return Ok(response);
        }
    }
}
