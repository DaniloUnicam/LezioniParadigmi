using System.ComponentModel.DataAnnotations;
using Unicam.Paradigmi.Test.Models;

namespace Unicam.Paradigmi.Application.Models.Requests
{
    //CLASSE DataTransferObject (DTO)
    //queste classi servono a stratificare, in modo che ogni layer viva di vita propria
    //in questo modo, se dovrò ampliare le mie richieste, non modificherò Azienda   
    public class CreateAziendaRequest
    {
        //Validation: le data annotation obbligano l'inserimento dei campi nelle richieste http
        //per esempio, una stringa non potrà essere string.Empty (ovvero "")
        [Required(ErrorMessage = "Il campo Ragione Sociale è obbligatorio")]
        [MinLength(1)]
        [MaxLength(100)]
        //ma l'alternativa è il pacchetto nuGet Fluent Validation AspnetCore
        public string RagioneSociale {  get; set; } = string.Empty;

        public string Citta { get; set; } = string.Empty;

        public string Cap { get; set; } = string.Empty;

        //effettuo un mapping 
        public Azienda toEntity()
        {
            var azienda = new Azienda();
            azienda.RagioneSociale = RagioneSociale;
            azienda.Citta = Citta;
            azienda.Cap = Cap;
            return azienda;
        }
    }
}
