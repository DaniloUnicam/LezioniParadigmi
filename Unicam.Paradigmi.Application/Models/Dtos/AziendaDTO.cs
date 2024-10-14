using Unicam.Paradigmi.Test.Models;

namespace Unicam.Paradigmi.Application.Models.Dtos
{
    //non è detto che i DTO siano uguali, si rendono uguali per gestirli meglio
    public class AziendaDTO
    {
        public AziendaDTO()
        {

        }

        //questo costruttore ci permette di fare mapping
        public AziendaDTO(Azienda azienda)
        {
            //possibile effettuare mapping automatico con librerie AutoMapper
            Id = azienda.IdAzienda;
            RagioneSociale = azienda.RagioneSociale;
            Citta = azienda.Citta;
            Cap = azienda.Cap;
        }

        public int Id { get; set; }
        public string RagioneSociale { get; set; }

        public string Citta {  get; set; }

        public string Cap {  get; set; }

    }
}
