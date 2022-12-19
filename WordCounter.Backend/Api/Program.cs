using Api;
using Api.Data;
using Api.Models;
using Api.Serivces;
using TextProcessor.Core;
using TextProcessor.Steps.Processors;
using WordCounter.Core;

var builder = WebApplication.CreateBuilder(args);

Module.RegisterDependencies(builder.Services);
builder.Services.AddCors();
builder.Services.AddControllers();

var app = builder.Build();

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseCors(x => { 
    x.AllowAnyOrigin();
    x.AllowAnyMethod();
    x.AllowAnyHeader();
});

app.MapControllers();
    
app.Run();