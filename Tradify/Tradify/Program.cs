using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Serilog;
using System.Globalization;
using Tradify.Core.Dependencies;
using Tradify.Core.MiddleWare;
using Tradify.Core.Resources.Service;
using Tradify.Data.Entities;
using Tradify.Data.Entities.Identity;
using Tradify.Infrastructure.Context;
using Tradify.Infrastructure.Dependencies;
using Tradify.Infrastructure.Seeder;
using Tradify.RealTimeService.AddDependencies;
using Tradify.RealTimeService.HubNegotiation;
using Tradify.Service.Dependencies;
using Twilio.TwiML.Voice;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers(); 

builder.Services.AddDbContext<ApplicationDbContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("First")).UseLazyLoadingProxies());
builder.Configuration.AddJsonFile("Secret.json");

#region Dependencies
builder.Services.AddInfrasturcureDepndencies().AddServicesDepencies().AddCoreDependencies().AddRealTimeDepndencies()
    .AdServiceRegisteration(builder.Configuration);

builder.Services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();
builder.Services.AddTransient<IUrlHelper>(x =>
{
    var actionContext = x.GetRequiredService<IActionContextAccessor>().ActionContext;
    var factory = x.GetRequiredService<IUrlHelperFactory>();
    return factory.GetUrlHelper(actionContext);
});
builder.Services.AddMemoryCache();
#endregion
#region Localization
builder.Services.AddControllersWithViews();
builder.Services.AddLocalization(opt =>
opt.ResourcesPath = "Resources"
);
builder.Services.Configure<RequestLocalizationOptions>(options => {
    List<CultureInfo> supportCultures = new List<CultureInfo>{
      new CultureInfo("en-US"),

            new CultureInfo("ar-EG")
};
    options.DefaultRequestCulture = new RequestCulture("en-US");
    options.SupportedCultures = supportCultures;
    options.SupportedUICultures = supportCultures;

}
    );

#endregion


//Serilog
//Log.Logger = new LoggerConfiguration().ReadFrom.Configuration(builder.Configuration).CreateLogger();
//builder.Services.AddLogging();
builder.Host.UseSerilog((context, config) =>
{
    config.ReadFrom.Configuration(context.Configuration);
});

// اختياري
builder.Services.AddLogging();
builder.Services.AddSwaggerGen();
builder.Services.Configure<FormOptions>(options =>
{
    options.MultipartBodyLengthLimit = 104857600; // 100 MB
});


var app = builder.Build();
using (var scope = app.Services.CreateScope())
{
    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<Role>>();
    var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

    await RoleSeeder.SeedAsync(roleManager);

    await UserSeeder.SeedAsync(userManager,builder.Configuration);
    //   await SellerSeeder.SeedAsync(context);
    //   await StoreSeeder.SeedAsync(context);
    //  await StoreImageSeeder.SeedAsync(context);
    //  await CategorySeeder.SeedAsync(context);
    //   await InstructorSeeader.SeedAsync(context);

    // await InstructorImageSeeder.SeedAsync(context);

    //  await EducationSeeder.SeedAsync(context);
    //   await CertificationsSeeder.SeedAsync(context);
    //  await ServiceSeeder.SeedAsync(context);
    //  await InstructorSchedulesSeeder.SeedAsync(context);

    // await CartSeeder.SeedAsync(context);
    // await InstructorReviewSeeder.SeedAsync(context);
  //  await ProductSeeder.SeedAsync(context);
   // await ProductReviewSeeder.SeedAsync(context);

}


// Configure the HTTP request pipeline.

app.UseSwagger();
    app.UseSwaggerUI();


#region Localization Middleware
var options = app.Services.GetService<IOptions<RequestLocalizationOptions>>();
app.UseRequestLocalization(options!.Value);
#endregion
app.UseMiddleware<ErrorHandlerMiddleware>();
app.UseStaticFiles();
app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.MapHub<HubNegotiation>("/ConnectionHub");
app.Run();