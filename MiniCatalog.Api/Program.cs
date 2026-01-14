using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using MiniCatalog.Api.Configurations;
using MiniCatalog.Api.Middlewares;
using MiniCatalog.Application.Interfaces.Repositories;
using MiniCatalog.Application.Interfaces.Services;
using MiniCatalog.Application.Services;
using MiniCatalog.Infra.Persistence.Context;
using MiniCatalog.Infra.Persistence.Context.Configurations;
using MiniCatalog.Infra.Persistence.Repositories;
using MiniCatalog.Infra.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AppDbContext>(options 
    => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.Configure<MongoDbSettings>(
    builder.Configuration.GetSection("MongoDbSettings"));

builder.Services.AddScoped<AuditDbContext>(sp =>
{
    var settings = sp.GetRequiredService<IOptions<MongoDbSettings>>().Value;
    return new AuditDbContext(settings);
});

MongoGuidConfiguration.Configure();

builder.Services.AddScoped<IItemRepository, ItemRepository>();
builder.Services.AddScoped<ICategoriaRepository, CategoriaRepository>();
builder.Services.AddScoped<CategoriaService>();
builder.Services.AddScoped<ItemService>();
builder.Services.AddScoped<IAuditLogRepository, AuditLogRepository>();
builder.Services.AddScoped<IAuditService, AuditService>();




var app = builder.Build();

app.UseMiddleware<ExceptionMiddleware>();

app.UseSwagger();
app.UseSwaggerUI();

//app.UseHttpsRedirection();
app.MapControllers();
app.Run();