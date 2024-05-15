using Microsoft.EntityFrameworkCore;
using Microsoft.Net.Http.Headers;
using ObecnaKniznicaAPI.Data;
using ObecnaKniznicaAPI.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection") ??
        throw new InvalidOperationException("Connection String 'DefaultConnection' not found."));
});
builder.Services.AddScoped<IBookService, BookService>(); // instancia vytvorena per request, zdiela sa v ramci poziadavky. (narozdiel od 'transient, kde sa aj v ramci jednej ziadosti vytvaraju nove instancie').

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseCors(policy =>
    {
        policy.WithOrigins("https://localhost:7018", "https://localhost:7018")
        .AllowAnyMethod().WithHeaders(HeaderNames.ContentType);
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
