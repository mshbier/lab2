using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using ServerApp.Models;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<DataContext>(opts => {
opts.UseSqlServer(builder.Configuration[
"ConnectionStrings:DefaultConnection"]);
opts.EnableSensitiveDataLogging(true);
});

// Add services to the container.
builder.Services.AddControllersWithViews();

//Add and configure Swagger middleware

builder.Services.AddEndpointsApiExplorer();

// <snippet_Services>
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "ServerApp",
        Description = "An ASP.NET MVC Core Web API "
    });

    // using System.Reflection;
    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
});




var app = builder.Build();

var context = app.Services.CreateScope().ServiceProvider
.GetRequiredService<DataContext>();
//SeedData.SeedDatabase(context);

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
    //Enable the middleware for serving the generated JSON document and the Swagger UI
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
    options.RoutePrefix = string.Empty;
});
app.UseSwagger(options =>
{
    options.SerializeAsV2 = true;
});

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
