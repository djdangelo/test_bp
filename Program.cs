using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Nexo.Api.Features.Client.Repository;
using test_bp.Features.Accounts;
using test_bp.Features.Accounts.Interface;
using test_bp.Features.Accounts.Repository;
using test_bp.Features.Accounts.Service;
using test_bp.Features.Agency;
using test_bp.Features.Agency.Interface;
using test_bp.Features.Agency.Repository;
using test_bp.Features.Agency.Service;
using test_bp.Features.Auth;
using test_bp.Features.Auth.Service;
using test_bp.Features.Client;
using test_bp.Features.Client.Interface;
using test_bp.Features.Client.Service;
using test_bp.Features.Role;
using test_bp.Features.Role.Interface;
using test_bp.Features.Role.Repository;
using test_bp.Features.Role.Service;
using test_bp.Features.SystemSetting;
using test_bp.Features.SystemSetting.Interface;
using test_bp.Features.SystemSetting.Repository;
using test_bp.Features.SystemSetting.Service;
using test_bp.Features.Transactions;
using test_bp.Features.Transactions.Interface;
using test_bp.Features.Transactions.Repository;
using test_bp.Features.Transactions.Service;
using test_bp.Features.User;
using test_bp.Features.User.Interface;
using test_bp.Features.User.Repository;
using test_bp.Features.User.Service;
using test_bp.Infrastructure.Persistense;
using test_bp.Infrastructure.Security.Extensions;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddHttpContextAccessor();

builder.Services.AddBpSecurity(builder.Configuration);
builder.Services.AddScoped<ISystemSettingRepository, SystemSettingRepository>();
builder.Services.AddScoped<SystemSettingService>();
builder.Services.AddScoped<IAgencyRepository, AgencyRepository>();
builder.Services.AddScoped<AgencyService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<AuthService>();
builder.Services.AddScoped<IRoleRepository, RoleRepository>();
builder.Services.AddScoped<RoleService>();
builder.Services.AddValidatorsFromAssembly(typeof(Program).Assembly);
builder.Services.AddScoped<IClientRepository, ClientRepository>();
builder.Services.AddScoped<ClientService>();
builder.Services.AddScoped<IAccountRepository, AccountRepository>();
builder.Services.AddScoped<AccountService>();
builder.Services.AddScoped<ITransactionsRepository, TransactionRepository>();
builder.Services.AddScoped<TransactionService>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        policy =>
        {
            policy.AllowAnyOrigin()
                  .AllowAnyMethod()
                  .AllowAnyHeader()
                  .SetIsOriginAllowedToAllowWildcardSubdomains();
        });
});

// DbContext - SQL Server
builder.Services.AddDbContext<BpContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("StoreConnection")));

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "Bp API",
        Version = "v1",
        Description = "API test"
    });

    c.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
    {
        Description = "Autenticación JWT usando el esquema Bearer. Ejemplo: 'Bearer 12345abcdef'",
        Name = "Authorization",
        In = Microsoft.OpenApi.Models.ParameterLocation.Header,
        Type = Microsoft.OpenApi.Models.SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });

    c.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
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
            Array.Empty<string>()
        }
    });
});

builder.Services.AddOpenApi();

var app = builder.Build();

app.MapSettingEndpoints();
app.MapAgencyEndpoints();
app.MapUserEndpoints();
app.MapAuthEndpoints();
app.MapRoleEndpoints();
app.MapClientEndpoints();
app.MapAccountsEndpoints();
app.MapTransactionsEndpoints();

// Seed de la base de datos
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<BpContext>();
    context.Database.Migrate();
}

// Configure the HTTP request pipeline.
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Nexo Services API v1");
    });
}

app.UseCors("AllowAll");
app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.Run();
