var builder = WebApplication.CreateBuilder(args);

// INIZIALIZZO I SERVIZI.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// INIZIALIZZO I MIDDLEWARE

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
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
