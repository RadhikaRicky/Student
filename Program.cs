using DinkToPdf;
using DinkToPdf.Contracts;
using Microsoft.EntityFrameworkCore;
using System.Configuration;
using User.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<DatabaseContext >(options=>options.UseSqlServer(
    builder.Configuration.GetConnectionString("xyz")
    ));

//Add pdf
builder.Services.AddSingleton(typeof(IConverter),
    new SynchronizedConverter(new PdfTools()));

var app = builder.Build();


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=student}/{action=show}");

app.Run();
