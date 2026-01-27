using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Globalization;
using Tradify.Core.Dependencies;
using Tradify.Core.MiddleWare;
using Tradify.Core.Resources.Service;
using Tradify.Infrastructure.Context;
using Tradify.Infrastructure.Dependencies;
using Tradify.Service.Dependencies;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<ApplicationDbContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("First")));

#region Dependencies
builder.Services.AddInfrasturcureDepndencies().AddServicesDepencies().AddCoreDependencies()
    .AdServiceRegisteration(builder.Configuration);

builder.Services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();
builder.Services.AddTransient<IUrlHelper>(x =>
{
    var actionContext = x.GetRequiredService<IActionContextAccessor>().ActionContext;
    var factory = x.GetRequiredService<IUrlHelperFactory>();
    return factory.GetUrlHelper(actionContext);
});

#endregion
#region Localization
builder.Services.AddControllersWithViews();
builder.Services.AddLocalization(opt =>
opt.ResourcesPath = ""
);
builder.Services.Configure<RequestLocalizationOptions>(options => {
    List<CultureInfo> supportCultures = new List<CultureInfo>{
      new CultureInfo("en-US"),
           
            new CultureInfo("ar-EG")
};
    options.DefaultRequestCulture = new RequestCulture("en-US");
    options.SupportedCultures= supportCultures;
    options.SupportedUICultures= supportCultures;

}
    );

#endregion
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();





var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}

#region Localization Middleware
var options = app.Services.GetService<IOptions<RequestLocalizationOptions>>();
app.UseRequestLocalization(options!.Value);
#endregion
app.UseMiddleware<ErrorHandlerMiddleware>();
app.UseStaticFiles();   
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
