using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MusicCollaborationManager.DAL.Abstract;
using MusicCollaborationManager.DAL.Concrete;
using MusicCollaborationManager.Data;
using MusicCollaborationManager.Services.Concrete;
using MusicCollaborationManager.Services.Abstract;
using SpotifyAPI.Web;
namespace MCM;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MusicCollaborationManager.Data;
using MusicCollaborationManager.Models;
using MusicCollaborationManager.Utilities;
using Reminders.DAL.Abstract;
using Reminders.DAL.Concrete;
using System.Runtime.Serialization;

public class Program { 

   

  
    public static void Main(string[] args) {
        
        var builder = WebApplication.CreateBuilder(args);

        string clientID = "3501352792214d5398432642bc300544";
        string clientSecret = builder.Configuration["SpotifySecret"];

        builder.Services.AddControllersWithViews();
        var MCMconnectionString = builder.Configuration.GetConnectionString("MCMConnection");
        builder.Services.AddDbContext<MCMDbContext>(options => options
                                    .UseLazyLoadingProxies()   
                                    .UseSqlServer(MCMconnectionString));

        var connectionString = builder.Configuration.GetConnectionString("AuthenticationConnection") ?? throw new InvalidOperationException("Connection string 'AuthenticationConnection' not found.");
        builder.Services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(connectionString));

        builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
        builder.Services.AddScoped<IListenerRepository, ListenerRepository>();

        builder.Services.AddDatabaseDeveloperPageExceptionFilter();

        builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
            .AddRoles<IdentityRole>()                           //enables roles, ie admin
            .AddEntityFrameworkStores<ApplicationDbContext>();
        builder.Services.AddControllersWithViews();
        builder.Services.AddScoped<ISpotifyVisitorService, SpotifyVisitorService>(s => new SpotifyVisitorService(clientID, clientSecret));

        builder.Services.AddSwaggerGen();
        var app = builder.Build();

        //After build has been called and before run, configure for auth seed data
        using (var scope = app.Services.CreateScope())
        {
            var services = scope.ServiceProvider;
            try
            {
                var config = app.Services.GetService<IConfiguration>();
                var testUserPw = config["SeedUserPw"];
                var adminPw = config["SeedAdminPw"];

                SeedUsers.Initialize(services, SeedData.UserSeedData, testUserPw).Wait();
                SeedUsers.InitializeAdmin(services, "admin@example.com", "admin", adminPw, "The", "Admin").Wait();
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

                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI();
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

