using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unicam.Paradigmi.Application.Abstractions.Services;
using Unicam.Paradigmi.Application.Options;
using Unicam.Paradigmi.Application.Services;
using Microsoft.Extensions.Options;

//il middleware non deve fare niente, deve solo passare la palla al prossimo middleware
namespace Unicam.Paradigmi.Application.Middlewares
{
    public class MiddlewareExample
    {
        // RequestDelegate è un delegate che rappresenta un middleware
        private RequestDelegate _next;

        //Nel caso in cui volessi utilizzare un servizio, ovvero dependency injection
        //devo iniettarlo nel costruttore del middleware istanziandolo prima
        private readonly AziendaService _aziendaService;

        //Nel costruttore potrò inserire successivamente i servizi che mi servono
        //Ovvero oltre al RequestDelegate, posso inserire AziendaService aziendaService

        public MiddlewareExample(RequestDelegate next)
        {
            _next = next;
        }

        //Posso iniettare i servizi nel costruttore del middleware
        //oppure nel metodo Invoke() del middleware
        //Se successivamente rimuovessi AddScoped<AziendaService>() dal Program.cs
        //e non iniettassi il servizio nel costruttore, ma lo iniettassi nel metodo Invoke()
        //avrei un errore di runtime
        //Iniettando un interfaccia, posso iniettare qualsiasi classe che implementa quell'interfaccia
        //andando a fare un'astrazione del servizio
        public async Task Invoke(HttpContext context,
            IAziendaService aziendaService,
            IConfiguration configuration,
            //se vogliamo leggere la configurazione email, possiamo passare il servizio
            //come IOptions<EmailOption> emailOption
            IOptions<EmailOption> emailOption)
        {
            Console.WriteLine(emailOption.Value.Host);

            context.RequestServices.GetRequiredService<IAziendaService>();
            //Per andare al prossimo middleware, devo chiamare
            //il metodo Invoke() del delegate
            await _next.Invoke(context);

            //Se voglio bloccare la chiamata, non devo chiamare il metodo Invoke(),
            //ma posso scrivere un messaggio di errore
            //context.Response.WriteAsync("Blocco la chiamata");

        }
    }
}
