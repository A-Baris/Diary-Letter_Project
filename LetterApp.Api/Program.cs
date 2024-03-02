using LetterApp.Dal.ProjectContext;
using LetterApp.IOC.Container;
using Microsoft.EntityFrameworkCore;
using FluentValidation.AspNetCore;
using LetterApp.Api.AutoMapper;

var builder = WebApplication.CreateBuilder(args);



builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddDbContext<LetterAppContext>(option => option.UseSqlServer(builder.Configuration.GetConnectionString("LetterAppSql")));
builder.Services.AddControllers().AddFluentValidation(x => x.RegisterValidatorsFromAssemblyContaining<Program>());
builder.Services.AddAutoMapper(typeof(MapProfile));
builder.Services.AddCors(options =>
{
    options.AddPolicy("MyCorsPolicy", builder =>
    {
        builder
            .WithOrigins("https://localhost:7137/")
            .AllowAnyMethod()
            .AllowAnyHeader();
    });
});

ServiceConfig.ServiceConfiguration(builder.Services, builder.Configuration);


var app = builder.Build();

app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthorization();
app.UseCors("MyCorsPolicy");
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();

    endpoints.MapControllerRoute(
        name: "user",
        pattern: "api/user",
        defaults: new { controller = "User", action = "GetUsers" });
});

app.MapControllers();


app.Run();
