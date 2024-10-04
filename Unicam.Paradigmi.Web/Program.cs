using Unicam.Paradigmi.Application.Services;
using Unicam.Paradigmi.Application.Middlewares;
using Unicam.Paradigmi.Application.Abstractions.Services;
using Unicam.Paradigmi.Application.Options;

var builder = WebApplication.CreateBuilder(args);

//Web � una web API, � l'interfaccia UserInterface (UI)
//E' quello da dove accede l'utente, � il punto d'ingresso
// INIZIALIZZO I SERVIZI.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
//AddSwaggerGen � il servizio che si occupa di generare l'endpoint che serve
//a swagger a dire quali sono le api che esistono all'interno del progetto
builder.Services.AddSwaggerGen();
//AddScoped � un metodo che permette di creare un'istanza di un oggetto
//per ogni richiesta che arriva al server HTTP
//IAziendaService deve essere risolto con un'istanza di AziendaService
builder.Services.AddScoped<IAziendaService, AziendaService>();

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

//se avessi la necessit� di implementare emailoption su 4-5 classi, dovr� fare l'iniezione
//di dipendenza in tutte le classi, quindi � meglio fare l'iniezione di dipendenza
builder.Services.Configure<EmailOption>(
    builder.Configuration.GetSection("EmailOption"));
var app = builder.Build();

// INIZIALIZZO I MIDDLEWARE

app.UseMiddleware<MiddlewareExample>();

// Configure the HTTP request pipeline.
//se l'enviroment � in modalit� development, parte in modalit� swagger
if (app.Environment.IsDevelopment())
{
    //swagger � un tool di terze parti che va a spazzolare le api che andiamo ad impostare
    //all'interno della nostra applicazione e le mappa in un'interfaccia grafica
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.Use(async (HttpContext context, Func<Task> next) =>
{
    await context.Response.WriteAsync("Prova");
    await next.Invoke();
});

//questo middleware verifica se la chiamata � HTTPS, se lo �, la redireziona su HTTPS
//se � http e non https, la chiamata non passa avanti con next.Invoke(), errore 302
app.UseHttpsRedirection();
//questo middleware verifica se l'utente � autorizzato o no a fare
//una determinata azione, se � https;
//se non � autorizzato, la palla non passa avanti con next.Invoke()
app.UseAuthorization();
//questo middleware mappa gli oggetti che esistono nel progetto di tipo controller
//i controller saranno il punto d'ingresso delle nostre API
//ovvero gli endpoint che andremo ad inserire nelle nostre API
app.MapControllers();

app.Run();
