using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Globalization;
using Tradify.Core.Resources.Service;
using Tradify.Infrastructure.Context;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<ApplicationDbContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("First")));

#region Dependencies

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

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
