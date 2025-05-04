using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using OfficeOpenXml.Utils;
using WebApplication2.Cnfigurations;
using WebApplication2.DTO;
using WebApplication2.Entities;
using WebApplication2.Interfaces;
using WebApplication2.Persistence;
using WebApplication2.Repositories;
using WebApplication2.Services;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddAuthorization();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        var config = builder.Configuration;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            // указывает, будет ли валидироваться издатель при валидации токена
            ValidateIssuer = true,
            // строка, представляющая издателя
            ValidIssuer = config["JwtSettings:Issuer"],
            // будет ли валидироваться потребитель токена
            ValidateAudience = true,
            // установка потребителя токена
            ValidAudience = config["JwtSettings:Audience"],
            // будет ли валидироваться время существования
            ValidateLifetime = true,
            // установка ключа безопасности
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["JwtSettings:Key"]!)),
            // валидация ключа безопасности
            ValidateIssuerSigningKey = true,
        };
    });
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1",new(){Title = "Medicine API", Version = "v1"});
    options.AddSecurityDefinition("Bearer",new Microsoft.OpenApi.Models.OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = Microsoft.OpenApi.Models.SecuritySchemeType.Http,
        Scheme = "Bearer",
       BearerFormat = "JWT",
       In = Microsoft.OpenApi.Models.ParameterLocation.Header,
       Description = "Введите токен  Jwt  формате: Bearer {token}"
    });
    options.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
    {
        {
            new Microsoft.OpenApi.Models.OpenApiSecurityScheme
            {
                Reference = new Microsoft.OpenApi.Models.OpenApiReference
                {
                    Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] { }
        }
    });
});
        
// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var connectionString = builder.Configuration.GetConnectionString("PostgressConnectionString");
builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection("JWTSettings"));
    builder.Services.AddDbContext<CashierContext>(m => m.UseNpgsql(connectionString));
builder.Services.AddScoped<ICashierService, CashierService>();
builder.Services.AddScoped<ICashierRepository, CashierRepository>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();
builder.Services.AddScoped<PasswordHasher<User>>();





var app = builder.Build();


app.UseAuthentication();    
app.UseAuthorization();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet("/weatherforecast", () =>
    {
        var forecast = Enumerable.Range(1, 5).Select(index =>
                new WeatherForecast
                (
                    DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                    Random.Shared.Next(-20, 55),
                    summaries[Random.Shared.Next(summaries.Length)]
                ))
            .ToArray();
        return forecast;
    })
    .WithName("GetWeatherForecast")
    .WithOpenApi();

app.MapGet("/cashiers",[Authorize(Roles = "Admin")] (ICashierRepository repository) =>
{
    return Results.Ok(repository.GetAllCashiers());
}).RequireAuthorization();
app.MapPost("/cashiers", [Authorize(Roles = "Admin")](ICashierRepository repository, Cashiers cashier) =>
{

    var added = repository.AddCashier(cashier);
    return Results.Created("/cashiers/" + added.CashierID, added);

}).RequireAuthorization();

app.MapGet("/cashiers/{id}", (Guid id,HttpContext httpContent, ICashierRepository repository) =>
    {
        var userIdClaim = httpContent.User.Claims.FirstOrDefault(claim => claim.Type == "Id");
        if (userIdClaim == null)
            return Results.Unauthorized();
        var userId=Guid.Parse(userIdClaim.Value);
        if (id != userId)
            return Results.Forbid();
        var cashier=repository.GetCashierById(userId);
        if (cashier == null)
            return Results.NotFound("Кассир не найден для этого пользователя");
        return Results.Ok(cashier);
    }).RequireAuthorization()
    .WithName("GetCasierById")
    .WithOpenApi();


app.MapDelete("/cashiers/{id}", [Authorize(Roles = "Admin")](Guid id, ICashierRepository repository) =>
{
    var cashier = repository.DeleteCashierById(id);
    return cashier is not null ? Results.Ok(cashier) : Results.NotFound();

}).RequireAuthorization();

app.MapPut("/cashiers/{id}", [Authorize(Roles = "Admin")](Guid id, CashierDto cashier, ICashierRepository repo) =>
    {
        var updatedCashier = repo.UpdateCashierById(cashier, id);
        return updatedCashier is not null ? Results.Ok(updatedCashier) : Results.NotFound();
    })
    .WithName("UpdateCashier")
    .WithOpenApi()
    .RequireAuthorization();

app.MapGet("/cashiers/excel", (ICashierService service) =>
{
    var fileBytes = service.GenerateCasiersFile();
    return Results.File(fileBytes, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
        "cashiers.xlsx");
});

app.MapPost("/register", (IUserService service, RegisterDto registerDto) =>
{
    var result = service.Register(registerDto);
    if (!result)
        return Results.BadRequest("Пользователь с таким именем уже сушествует или пароль пустой!");
    return Results.Ok("Пользователь успешно зарегистрирован");
});
app.MapPost("/login", (IUserService service, LoginDto loginDto) =>
{
    var token = service.Login(loginDto);
    if (token == null)
        return Results.Unauthorized();
    return Results.Ok(token);
});

app.MapPost("/register-cashier", (ICashierService service, RegisterCashierDto registerCashierDto) =>
{
    var result = service.RegisterCashier(registerCashierDto);
    if (!result)
        return Results.BadRequest("Ошибка при регистрации кассира!");
    return Results.Ok("Кассир успешно зарегистрирован");

})
.WithName("RegisterCashier")
.WithOpenApi();


app.MapGet("cashiers/me", [Authorize(Roles = "Admins")](HttpContext httpContext, ICashierRepository repository) =>
    {
        var userIdClaim = httpContext.User.Claims.FirstOrDefault(claim => claim.Type == "Id");
        if (userIdClaim == null)
            return Results.Unauthorized();
        var userId = Guid.Parse(userIdClaim.Value);
        var cashier = repository.GetCashierById(userId);
        if (cashier == null)
            return Results.NotFound("Кассир не найден!");
        return Results.Ok(cashier);
    
    }).WithName("GetCurrentCashier")
    .WithOpenApi();


app.MapPost("user/add", [Authorize(Roles = "Admin")](IUserRepository repo, UserDto user) =>
{
    
    var added = repo.AddUser(user);
    return Results.Created("/user/" + added.Id, added);
}).RequireAuthorization();

app.MapGet("users", [Authorize(Roles = "Admin")](IUserRepository repo) =>
{
    return Results.Ok(repo.GetAllUsers());
    
}).RequireAuthorization();

app.MapDelete("delete/users", [Authorize(Roles = "Admin")](Guid id, IUserRepository repo) =>
{

    var user = repo.DeleteUserById(id);
    return user is not null ? Results.Ok(user) : Results.NotFound();

}).RequireAuthorization();
app.MapPut("update/user", [Authorize(Roles = "Admin")](Guid Id, IUserRepository repo,UserDto user) =>
{
    
    var updatedUser = repo.UpdateUser(user, Id);
    return updatedUser is not null ? Results.Ok(updatedUser) : Results.NotFound();
}).WithName("UpdateUser")
.WithOpenApi()
.RequireAuthorization();






SeedAdminUser(app);

app.Run();

void SeedAdminUser(WebApplication app)
{
    using var scope = app.Services.CreateScope();
    var context = scope.ServiceProvider.GetRequiredService<CashierContext>();
    var passwordHasher = scope.ServiceProvider.GetRequiredService<IPasswordHasher<User>>();
    if (!context.Users.Any(u => u.Role == Roles.Admin))
    {
        var admin = new User
        {
            Username = "Admin",
            Role = Roles.Admin
        };
        admin.Password = passwordHasher.HashPassword(admin, "111Amdin111");
        context.Users.Add(admin);
        context.SaveChanges();
    }
}

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}