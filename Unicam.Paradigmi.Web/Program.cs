using Unicam.Paradigmi.Application.Services;
using Unicam.Paradigmi.Application.Middlewares;
using Unicam.Paradigmi.Application.Abstractions.Services;
using Unicam.Paradigmi.Application.Options;
using Unicam.Paradigmi.Models.Repositories;
using Unicam.Paradigmi.Models.Context;
using Microsoft.EntityFrameworkCore;
using FluentValidation.AspNetCore;
using FluentValidation;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

//Web è una web API, è l'interfaccia UserInterface (UI)
//E' quello da dove accede l'utente, è il punto d'ingresso
// INIZIALIZZO I SERVIZI.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
//AddSwaggerGen è il servizio che si occupa di generare l'endpoint che serve
//a swagger a dire quali sono le api che esistono all'interno del progetto
builder.Services.AddSwaggerGen();
//validazione per ogni singolo ogetto che dobbiamo validare (con i campi obbligatori)
//di norma si creano i DTO per far sì che ogni singolo oggetto abbia solo una validazione
builder.Services.AddFluentValidationAutoValidation();
//con questo comando andremo a prendere tutte le assembly (librerie) nel percorso .Application, con
//una validazione all'interno
//I validatori che sono riconosciuti dalla dependency injection che riescono ad effettuare
//la validazione automatica sono tutti quelli che si trovano dentro la nostra unicam.paradigmi.application
builder.Services.AddValidatorsFromAssembly(AppDomain.CurrentDomain.GetAssemblies().SingleOrDefault
    (assembly => assembly.GetName().Name == "Unicam.Paradigmi.Application"));
//ora aggiungo MyDbContext come servizio, altrimenti l'entity framework non verrà riconosciuto
//all'aggiunzione di AziendaRepository come servizio
builder.Services.AddDbContext<MyDbContext>(conf =>
{
    conf.UseSqlServer("data source=localhost;Initial catalog= paradigmi;User Id=paradigmi;Password=paradigmi;");
});
//AddScoped è un metodo che permette di creare un'istanza di un oggetto
//per ogni richiesta che arriva al server HTTP
//IAziendaService deve essere risolto con un'istanza di AziendaService
builder.Services.AddScoped<IAziendaService, AziendaService>();
builder.Services.AddScoped<AziendaRepository>();

/*Singleton = istanza globale di un singolo oggetto
 * builder.Services.AddSingleton
 * 
 * Scoped = un'istanza per ogni richiesta HTTP
 * builder.Services.AddScoped
 * 
 * Transient = un'istanza per un determinato oggetto
 * builder.Services.AddTransient
 */

//la configurazione email all'interno di appsettings, posso leggerla
//in questa maniera:
//i : dopo emailoption sono un separatore per andare a leggere il valore nel json
//questa configurazione la possiamo iniettare in qualsiasi oggetto
string host = builder.Configuration.GetValue<string>("EmailOption:Host");
//qui prendo un array di stringhe presente all'interno del file json
string[] tos = builder.Configuration.GetValue<string[]>("EmailOption:Tos");

//posso iniettare la configurazione email in un oggetto EmailOption
var emailOption = new EmailOption();

builder.Configuration.GetSection("EmailOption").Bind(emailOption);

//se avessi la necessità di implementare emailoption su 4-5 classi, dovrò fare l'iniezione
//di dipendenza in tutte le classi, quindi è meglio fare l'iniezione di dipendenza
builder.Services.Configure<EmailOption>(
    builder.Configuration.GetSection("EmailOption"));
var app = builder.Build();

// INIZIALIZZO I MIDDLEWARE

app.UseMiddleware<MiddlewareExample>();

// Configure the HTTP request pipeline.
//se l'enviroment è in modalità development, parte in modalità swagger
if (app.Environment.IsDevelopment())
{
    //swagger è un tool di terze parti che va a spazzolare le api che andiamo ad impostare
    //all'interno della nostra applicazione e le mappa in un'interfaccia grafica
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.Use(async (HttpContext context, Func<Task> next) =>
{
    await context.Response.WriteAsync("Prova");
    await next.Invoke();
});

//questo middleware verifica se la chiamata è HTTPS, se lo è, la redireziona su HTTPS
//se è http e non https, la chiamata non passa avanti con next.Invoke(), errore 302
app.UseHttpsRedirection();
//questo middleware verifica se l'utente è autorizzato o no a fare
//una determinata azione, se è https;
//se non è autorizzato, la palla non passa avanti con next.Invoke()
app.UseAuthorization();
//questo middleware mappa gli oggetti che esistono nel progetto di tipo controller
//i controller saranno il punto d'ingresso delle nostre API
//ovvero gli endpoint che andremo ad inserire nelle nostre API
app.MapControllers();

app.Run();
