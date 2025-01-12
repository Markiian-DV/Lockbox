using Lockbox.Infrastructure;
using Lockbox.Infrastructure.Data;
using Lockbox.Application;
using Microsoft.AspNetCore.Identity;
using Lockbox.Infrastructure.Identity;
using Lockbox.Web.Identity;
using Lockbox.Web.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

builder.Services.AddSpaStaticFiles(configuration => {
    configuration.RootPath = "lockboxui/dist";
});
//add web services:
builder.Services.AddScoped<UserContext>();
//end web services
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
    app.MapWhen(y => y.Request.Path.StartsWithSegments("/app"), client =>
    {
        client.UseSpa(spa =>
        {
            spa.UseProxyToSpaDevelopmentServer("https://localhost:6363");
        });
    });
    
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapGroup("api/identity")
   .MapIdentityApi<ApplicationUser>();

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
