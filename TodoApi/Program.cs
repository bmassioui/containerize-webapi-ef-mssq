using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using TodoApi.Models;

var MyAllowSpecificOrigins = "_todoApiAllowSpecificOrigins";
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
                      builder =>
                      {
                          // Allow all
                          builder.AllowAnyOrigin();
                          builder.AllowAnyHeader();
                          builder.AllowAnyMethod();
                      });
});

// Add services to the container.
builder.Services.AddControllers();

// Use MSSQL
builder.Services.AddDbContext<TodoContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("ToDoItemConnectionString")));

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "Todo API",
        Description = "An ASP.NET Core Web API for managing ToDo items",
        TermsOfService = new Uri("https://example.com/terms"),
        Contact = new OpenApiContact
        {
            Name = "Bouchaib MASSIOUI",
            Url = new Uri("https://bouchaibmassioui.com/")
        }
    });

    // Swagger XML Documentation File
    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
});

// Register AutoMapper
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

// Microsoft automatically TRIMS Async Keyword from async methods
// Configure Mvc to suppress Async suffix in Action Names 
// Read more: https://stackoverflow.com/a/63834605 or https://docs.microsoft.com/en-us/dotnet/core/compatibility/aspnetcore#mvc-async-suffix-trimmed-from-controller-action-names
builder.Services.AddMvc(options =>
{
    options.SuppressAsyncSuffixInActionNames = false;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "Todo API v1");
        options.RoutePrefix = string.Empty; // see: https://docs.microsoft.com/en-us/aspnet/core/tutorials/getting-started-with-swashbuckle?view=aspnetcore-6.0&tabs=visual-studio-code#add-and-configure-swagger-middleware
    });
}

app.UseHttpsRedirection();

app.UseCors(MyAllowSpecificOrigins);

app.UseAuthorization();

app.MapControllers();

app.Run();
