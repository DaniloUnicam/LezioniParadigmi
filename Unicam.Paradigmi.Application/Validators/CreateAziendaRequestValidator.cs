using FluentValidation;
using Unicam.Paradigmi.Application.Models.Requests;

namespace Unicam.Paradigmi.Application.Validators
{
    public class CreateAziendaRequestValidator : AbstractValidator<CreateAziendaRequest>
    {
        //inserisco le regole che voglio vengano rispettate
        //per ogni regola un messaggio
        public CreateAziendaRequestValidator()
        {
            RuleFor(m => m.RagioneSociale)
                .NotEmpty()
                .WithMessage("Il campo Ragione Sociale è obbligatorio (nullo)")
                .NotNull()
                .WithMessage("Il campo Ragione Sociale è obbligatorio (vuoto)")
                .MinimumLength(3)
                .WithMessage("Il campo Ragione Sociale deve essere almeno di 3 caratteri");

            RuleFor(m => m.Citta)
                .Custom(ValidaCitta);
        }

        private void ValidaCitta(string value, ValidationContext<CreateAziendaRequest> context)
        {
            if(value.Length == 0)
            {
                context.AddFailure("Il campo citta è obbligatorio");
            }
            //TODO: Fare una query sul database per verificare se la città esiste
        }
    }
}
