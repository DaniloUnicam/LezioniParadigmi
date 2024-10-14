using Unicam.Paradigmi.Application.Models.Dtos;

namespace Unicam.Paradigmi.Application.Models.Responses
{
    public class CreateAziendaResponse
    {
        //restituirà un DTO
        public AziendaDTO Azienda { get; set; } = null!;
    }
}
