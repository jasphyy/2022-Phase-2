using Microsoft.EntityFrameworkCore;
using MSA.Phase2.OfficialAPI;
using MSA.Phase2.OfficialAPI.Data;
using MSA.Phase2.OfficialAPI.Service;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddDbContext<TeamAPIDbContext>(options => options.UseInMemoryDatabase("TeamDb"));
builder.Services.AddSwaggerDocument(options =>
{
    options.DocumentName = "GenshinTeamAPI";
    options.Version = "V1";
    options.Title = "Genshin Team API";
});

//Genshin API as a client
builder.Services.AddHttpClient<IGenshinDevService, GenshinDevService>(configureClient: client =>
{
    client.BaseAddress = new Uri("https://api.genshin.dev/characters");
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseOpenApi();
    app.UseSwaggerUi3();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
