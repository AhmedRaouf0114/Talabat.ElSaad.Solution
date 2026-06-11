using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StackExchange.Redis;
using Talabat.APIs.Error;
using Talabat.APIs.Helper;
using Talabat.APIs.Middlewares;
using Talabat.Core.Entities.Identity;
using Talabat.Core.Repositories.Contract;
using Talabat.Repository;
using Talabat.Repository.Data;
using Talabat.Repository.Identity;




var builder = WebApplication.CreateBuilder(args);
#region Configuration Service

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();



builder.Services.AddDbContext<StoreContext>(opthion =>
{
    opthion.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
    /*.UseLazyLoadingProxies()*/;
});

builder.Services.AddDbContext<AppIdentityDbContext>(opthion =>
{
    opthion.UseSqlServer(builder.Configuration.GetConnectionString("IdentityConnection"));
});

builder.Services.AddIdentity<AppUser, IdentityRole>(option =>
{

}).AddEntityFrameworkStores<AppIdentityDbContext>();

builder.Services.AddSingleton<IConnectionMultiplexer>((serviceprovider) =>
{
    var connectionstring = builder.Configuration.GetConnectionString("Redis");
    return ConnectionMultiplexer.Connect(connectionstring);
});

builder.Services.AddScoped(typeof(IGenaricRepository<>),typeof(GenaricRepository<>));
builder.Services.AddScoped<IBasketRepository,BasketRepository>();

builder.Services.AddAutoMapper(typeof(MapperProfile));
//builder.Services.AddTransient<ExceptionMiddleware>();


builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.InvalidModelStateResponseFactory = (actionContext) =>
    {
        var errors = actionContext.ModelState.Where(P => P.Value.Errors.Count > 0)
                                             .SelectMany(P => P.Value.Errors)
                                             .Select(P => P.ErrorMessage)
                                             .ToList();

        var Response = new ApiValidationErrorResponse()
        {
            Errors = errors
        };

        return new BadRequestObjectResult(Response);
    };
});

#endregion


var app = builder.Build();

#region Update_DataBase and DataSeeding

using var Scoped = app.Services.CreateScope();
var Services = Scoped.ServiceProvider;
var _Dbcontext = Services.GetRequiredService<StoreContext>();
var _IdentityDbcontext = Services.GetRequiredService<AppIdentityDbContext>();

try
{
    await _Dbcontext.Database.MigrateAsync();
    await StoreContextSeed.SeedAsync(_Dbcontext);

    await _IdentityDbcontext.Database.MigrateAsync();
    var _UserManger = Services.GetRequiredService<UserManager<AppUser>>();
    await AppIdentityDbContextSeed.SeedUsersAsync(_UserManger);
}
catch (Exception ex)
{

    var logger = Services.GetRequiredService<ILogger<Program>>();
    logger.LogError(ex, "An error occurred while migrating or seeding the database.");
}

#endregion


#region Configure Kestrel

app.UseMiddleware<ExceptionMiddleware>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseStatusCodePagesWithReExecute("/errors/{0}");

app.UseHttpsRedirection();

app.UseStaticFiles();

app.MapControllers();

app.Run(); 
#endregion
