using System.IdentityModel.Tokens.Jwt;
using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using MiniCatalog.Api.Configurations;
using MiniCatalog.Api.Middlewares;
using MiniCatalog.Application.Interfaces.Repositories;
using MiniCatalog.Application.Interfaces.Services;
using MiniCatalog.Application.Services;
using MiniCatalog.Application.Settings;
using MiniCatalog.Application.Validators;
using MiniCatalog.Infra.Persistence.Context;
using MiniCatalog.Infra.Persistence.Repositories;
using MiniCatalog.Infra.Services;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSwaggerConfiguration();

builder.Services.AddDbContext<AppDbContext>(options 
    => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddIdentityCore<IdentityUser>(options => {
        options.Password.RequireDigit = true;
        options.Password.RequiredLength = 8;
        options.User.RequireUniqueEmail = true;
    })
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<AppDbContext>()
    .AddDefaultTokenProviders()
    .AddSignInManager<SignInManager<IdentityUser>>();

builder.Services.ConfigureApplicationCookie(options =>
{
    options.Events.OnRedirectToLogin = context =>
    {
        context.Response.StatusCode = StatusCodes.Status401Unauthorized;
        return Task.CompletedTask;
    };
});

builder.Services.Configure<MongoDbSettings>(
    builder.Configuration.GetSection("MongoDbSettings"));

builder.Services.AddScoped<AuditDbContext>(sp =>
{
    var settings = sp.GetRequiredService<IOptions<MongoDbSettings>>().Value;
    return new AuditDbContext(settings);
});

MongoGuidConfiguration.Configure();

var jwtSection = builder.Configuration.GetSection("JwtSettings");
builder.Services.Configure<JwtSettings>(jwtSection);

var jwtSettings = jwtSection.Get<JwtSettings>();

builder.Services.AddSingleton(jwtSettings!);


builder.Services.AddJwtAuth(jwtSettings!);

builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();

builder.Services.AddScoped<IItemRepository, ItemRepository>();
builder.Services.AddScoped<ICategoriaRepository, CategoriaRepository>();
builder.Services.AddScoped<CategoriaService>();
builder.Services.AddScoped<ItemService>();
builder.Services.AddScoped<IAuditLogRepository, AuditLogRepository>();
builder.Services.AddScoped<IAuditService, AuditService>();
builder.Services.AddScoped<ReportService>();
builder.Services.AddScoped<IImportService, ImportService>();

builder.Services.AddHttpClient();

builder.Services.AddAppAuthorization();

builder.Services.AddValidatorsFromAssemblyContaining<ItemRequestValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<CategoriaRequestValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<CategoriaUpdateValidator>();
var app = builder.Build();

app.UseMiddleware<ExceptionMiddleware>();

app.UseCors(x => x.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

app.UseSwagger();
app.UseSwaggerUI();

app.UseAuthentication();
app.UseAuthorization();

//app.UseHttpsRedirection();
app.MapControllers();
app.Run();