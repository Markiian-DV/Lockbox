using Lockbox.Infrastructure;
using Lockbox.Infrastructure.Data;
using Lockbox.Application;
using Microsoft.AspNetCore.Identity;
using Lockbox.Infrastructure.Identity;
using Lockbox.Web.Identity;
using Lockbox.Web.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddSpaStaticFiles(configuration => {
    configuration.RootPath = "wwwroot/client";
});
builder.Services.AddScoped<UserContext>();
builder.Services
    .AddApplicationServices()
    .AddInfrastructureServices(builder.Configuration);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services
     .AddTransient<Microsoft.AspNetCore.Identity.UI.Services.IEmailSender, IdentityEmailSender>()
     .AddIdentityCore<ApplicationUser>(opt =>
     {
         opt.SignIn.RequireConfirmedEmail = true;
     })
     .AddRoles<IdentityRole>()
     .AddEntityFrameworkStores<ApplicationDbContext>()
     .AddApiEndpoints();

builder.Services
    .AddAuthorization()
    .AddAuthentication()
    .AddCookie(IdentityConstants.ApplicationScheme, opt => 
    {
        opt.LoginPath="/User/Login";
    });


var app = builder.Build();
Console.WriteLine(app.Environment.IsDevelopment());
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapWhen(context => !context.Request.Path.StartsWithSegments("/api") && 
                           !context.Request.Path.StartsWithSegments("/swagger"), 
        client =>
        {
            client.UseSpa(spa =>
            {
                spa.UseProxyToSpaDevelopmentServer("http://host.docker.internal:6363");
            });
        });
    
    app.UseSwagger();
    app.UseSwaggerUI();
    app.ApplyMigrations();
}
else
{
    // Use HSTS and HTTPS redirection in production
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
    app.UseHttpsRedirection();

    // Serve static files for production
    app.UseSpaStaticFiles();
    app.MapWhen(context => !context.Request.Path.StartsWithSegments("/api") && 
                           !context.Request.Path.StartsWithSegments("/swagger"), 
        client =>
        {
            client.UseSpa(spa =>
            {
                // idk how it works)))
                // spa.Options.SourcePath = "wwwroot/client/";
            });
        });
}

app.MapGroup("api/identity")
   .MapIdentityApi<ApplicationUser>();

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
