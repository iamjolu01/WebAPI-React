using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using MiniValidation;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors();
builder.Services.AddDbContext<HouseDbContext>(o => 
    o.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking));
builder.Services.AddScoped<IHouseRepository, HouseRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(p => p.WithOrigins("http://localhost:3000").AllowAnyHeader().AllowAnyMethod());

app.UseHttpsRedirection();


app.MapGet("/houses", (IHouseRepository repo) => repo.GetAll())
    .Produces<HouseDto[]>(StatusCodes.Status200OK);

app.MapGet("/houses/{houseId:int}", async (int houseId, IHouseRepository repo) => 
{
    var house = await repo.Get(houseId);
    if(house == null)
        return Results.NotFound($"House with ID {houseId} not found.");
    return Results.Ok(house);
}).ProducesProblem(404).Produces<HouseDetailDto>(StatusCodes.Status200OK);

app.MapPost("/houses", async ([FromBody]HouseDetailDto dto, IHouseRepository repo) =>{
    if(!MiniValidator.TryValidate(dto, out var errors))
        return Results.ValidationProblem(errors);
    var newHouse = await repo.Add(dto);
    return Results.Created($"/houses/{newHouse.Id}", newHouse);
}).Produces<HouseDetailDto>(StatusCodes.Status201Created).ProducesValidationProblem();

app.MapPut("/houses", async ([FromBody]HouseDetailDto dto, IHouseRepository repo) =>{
    if(!MiniValidator.TryValidate(dto, out var errors))
        return Results.ValidationProblem(errors);
    if(await repo.Get(dto.Id) == null)
        return Results.Problem($"House with ID {dto.Id} not found.", statusCode: 404);
    var updateHouse = await repo.Update(dto);
    return Results.Ok(updateHouse);
}).ProducesProblem(404).Produces<HouseDetailDto>(StatusCodes.Status200OK).ProducesValidationProblem();

app.MapDelete("/houses/{houseId:int}", async (int houseId, IHouseRepository repo) =>{
    if(await repo.Get(houseId) == null)
        return Results.Problem($"House with ID {houseId} not found.", statusCode: 404);
    await repo.Delete(houseId);
    return Results.Ok();
}).ProducesProblem(404).Produces(StatusCodes.Status200OK);

app.Run();

