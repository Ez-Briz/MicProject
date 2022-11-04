using API.Data;
using API.Interfaces;
using API.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<AppDbContext>(options => {
    options.UseSqlite(builder.Configuration.GetConnectionString("Default Connection"));
});
builder.Services.AddScoped<IUserService, UserService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// using(var scope = app.Services.CreateScope())
// {
//     var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
//     await context.Database.MigrateAsync();
// }

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
