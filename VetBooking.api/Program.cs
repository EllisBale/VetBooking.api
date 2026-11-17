using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;
using VetBooking.api.Data;
using VetBooking.api.Middleware;
using VetBooking.api.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddScoped<IAppointmentTypeService, AppointmentTypeService>();
builder.Services.AddScoped<IOwnerService, OwnerService>();
builder.Services.AddScoped<IPetService, PetService>();
builder.Services.AddScoped<IVetService, VetService>();
builder.Services.AddScoped<IAppointmentService, AppointmentService>();
builder.Services.AddScoped<IVetScheduleService, VetScheduleService>();


builder.Services.AddExceptionHandler<GlobalExceptionHandler>();

builder.Services.AddProblemDetails();


builder.Services.AddCors(options =>
{
    options.AddPolicy("Frontend", policy =>
       policy.WithOrigins("http://localhost:5173")
            .AllowAnyHeader()
            .AllowAnyMethod());
});


var app = builder.Build();

app.UseExceptionHandler();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}


app.UseHttpsRedirection();

app.UseAuthorization();

app.UseCors("Frontend");

app.MapControllers();

app.Run();
