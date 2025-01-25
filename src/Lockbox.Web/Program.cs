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
    configuration.RootPath = "lockboxui/dist";
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

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapWhen(context => !context.Request.Path.StartsWithSegments("/api") && 
                           !context.Request.Path.StartsWithSegments("/swagger"), 
        client =>
        {
            client.UseSpa(spa =>
            {
                spa.UseProxyToSpaDevelopmentServer("https://localhost:6363");
            });
        });
    
    app.UseSwagger();
    app.UseSwaggerUI();
}
else
{
    // Use HSTS and HTTPS redirection in production
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
    app.UseHttpsRedirection();

    // Serve static files for production
    app.UseStaticFiles();
    app.UseSpaStaticFiles();
}

app.MapGroup("api/identity")
   .MapIdentityApi<ApplicationUser>();

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
