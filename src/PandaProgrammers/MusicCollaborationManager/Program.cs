using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MusicCollaborationManager.Data;
namespace MCM;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MusicCollaborationManager.Data;
using MusicCollaborationManager.Models;
using MusicCollaborationManager.Utilities;
using System.Runtime.Serialization;


public class Program { 
    public static void Main(string[] args) {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddControllersWithViews();
        var MCMconnectionString = builder.Configuration.GetConnectionString("MCMConnection");
        builder.Services.AddDbContext<MCMDbContext>(options => options
                                    .UseLazyLoadingProxies()   
                                    .UseSqlServer(MCMconnectionString));

        var connectionString = builder.Configuration.GetConnectionString("AuthenticationConnection") ?? throw new InvalidOperationException("Connection string 'AuthenticationConnection' not found.");
        builder.Services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(connectionString));
        builder.Services.AddDatabaseDeveloperPageExceptionFilter();

        builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
            .AddEntityFrameworkStores<ApplicationDbContext>();
        builder.Services.AddControllersWithViews();

        var app = builder.Build();

        //After build has been called and before run, configure for auth seed data
        using (var scope = app.Services.CreateScope())
        {
            var services = scope.ServiceProvider;
            try
            {
                var config = app.Services.GetService<IConfiguration>();
                var testUserPw = config["SeedUserPw"];
                SeedUsers.Initialize(services, SeedData.UserSeedData, testUserPw).Wait();
            }
            catch (Exception ex)
            {
                var logger = services.GetRequiredService<ILogger<Program>>();
                logger.LogError(ex, "An error occured seeding the DB");
            }
        }

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseMigrationsEndPoint();
        }
        else
        {
            app.UseExceptionHandler("/Home/Error");
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseRouting();

        app.UseAuthorization();

        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}/{id?}");
        app.MapRazorPages();

        app.Run();

    }
}

