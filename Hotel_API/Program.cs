using Hotel_API.DbContexts;
using Hotel_API.Services;
using Hotel_API.Services.InterfacesRepository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Serilog;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers(option => option.ReturnHttpNotAcceptable = true)
    .AddXmlDataContractSerializerFormatters()
    .AddNewtonsoftJson(option =>
    {
        option.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
        option.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
        option.SerializerSettings.Formatting = Formatting.Indented;
    });
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<HotelContext>(
    options => options.UseSqlServer(builder.Configuration["ConnectionStrings:HotelDBConnectionString"]));


builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddScoped<IHotelRepository,HotelRepository>();
builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
builder.Services.AddScoped<IRoomRepository, RoomRepository>();


builder.Services.AddAuthentication().AddJwtBearer(options =>
{
    options.TokenValidationParameters = new()
    {

        ValidIssuer = builder.Configuration["Authentication:Issuer"],
        ValidAudience = builder.Configuration["Authentication:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(builder.Configuration["Authentication:SecretKey"])),
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateIssuerSigningKey = true
    };
});

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Error()
    .WriteTo.Console()
    .WriteTo.File("D:/Hotel_Log/applog.txt", rollingInterval: RollingInterval.Day)
    .CreateLogger();

builder.Host.UseSerilog();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
