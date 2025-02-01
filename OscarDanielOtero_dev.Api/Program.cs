using BusinessLogicServices;
using BusinessLogicServices.JobServices;
using FirestoreInfrastructureServices;
using Microsoft.AspNetCore.Mvc;
using Models.Documents.Profile;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add firestore
await builder.Services.AddFirestoreCollectionServices(builder.Configuration.GetConnectionString("FirestoreDbProjectId")!, builder.Environment.IsDevelopment());

// Add Business Logic Services
builder.Services.AddJobServices();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapPost("/addJob", async ([FromServices]IJobService service, [FromBody] JobDocument newJob) => await service.AddJob(newJob))
    .WithName("AddJob")
    .WithOpenApi();

app.Run();