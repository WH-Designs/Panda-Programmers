using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MusicCollaborationManager.DAL.Abstract;
using MusicCollaborationManager.DAL.Concrete;
using MusicCollaborationManager.Data;
using MusicCollaborationManager.Services.Concrete;
using MusicCollaborationManager.Services.Abstract;
using SpotifyAPI.Web;
using MusicCollaborationManager.Models;
using MusicCollaborationManager.Utilities;
using System.Runtime.Serialization;
using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using static SpotifyAPI.Web.Scopes;
using Microsoft.AspNetCore.Identity.UI.Services;
using OpenAI.Net;

/*using WebPWrecover.Services;*/

namespace MCM;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        string clientID = builder.Configuration["SpotifyClientID"];
        string clientSecret = builder.Configuration["SpotifySecret"];
        string redirectUri = builder.Configuration["RedirectUri"];
        string deepAiKey = builder.Configuration["DeepAiKey"];
        string sendGridKey = builder.Configuration["SendGridKey"];
        string openAiKey = builder.Configuration["OpenAiKey"];
        string youTubeKey = builder.Configuration["YT_ApiKey"];
        string pollsKey = builder.Configuration["PollsApiKey"];

        builder.Services.AddControllersWithViews();
        var MCMconnectionString = builder.Configuration.GetConnectionString("MCMConnection");
        builder.Services.AddDbContext<MCMDbContext>(
            options => options.UseLazyLoadingProxies().UseSqlServer(MCMconnectionString)
        );

        var connectionString =
            builder.Configuration.GetConnectionString("AuthenticationConnection")
            ?? throw new InvalidOperationException(
                "Connection string 'AuthenticationConnection' not found."
            );
        builder.Services.AddDbContext<ApplicationDbContext>(
            options => options.UseSqlServer(connectionString)
        );

        builder.Services.AddScoped<DbContext, MCMDbContext>();
        builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
        builder.Services.AddScoped<IListenerRepository, ListenerRepository>();
        builder.Services.AddScoped<ICommentRepository, CommentRepository>();
        builder.Services.AddScoped<IPlaylistPollRepository, PlaylistPollRepository>();
        builder.Services.AddScoped<ITutorialRepository, TutorialRepository>();
        builder.Services.AddScoped<IPromptRepository, PromptRepository>();
        builder.Services.AddScoped<ISpotifyAuthorizationNeededRepository, SpotifyAuthorizationNeededRepository>();

        builder.Services.AddDatabaseDeveloperPageExceptionFilter();

        builder.Services
            .AddDefaultIdentity<IdentityUser>(
                options => options.SignIn.RequireConfirmedAccount = true
            )
            .AddRoles<IdentityRole>() //enables roles, ie admin
            .AddEntityFrameworkStores<ApplicationDbContext>();
        builder.Services.AddControllersWithViews();
        builder.Services.AddScoped<ISpotifyVisitorService, SpotifyVisitorService>(
            s => new SpotifyVisitorService(clientID, clientSecret)
        );
        builder.Services.AddScoped<SpotifyAuthService>(
            s => new SpotifyAuthService(clientID, clientSecret, redirectUri)
        );

        builder.Services.AddScoped<AuthMessageSenderOptions>();

        builder.Services.AddTransient<IEmailSender, EmailSender>();
        builder.Services.Configure<AuthMessageSenderOptions>(builder.Configuration);

        builder.Services.AddOpenAIServices(options =>
        {
            options.ApiKey = openAiKey;
        });

        builder.Services.AddScoped<IMCMOpenAiService,  MCMOpenAiService>();

        builder.Services.AddHttpContextAccessor();
        builder.Services.AddSingleton(SpotifyClientConfig.CreateDefault());
        builder.Services.AddScoped<SpotifyClientBuilder>();

        builder.Services.AddScoped<IDeepAiService, DeepAiService>(
            d => new DeepAiService(deepAiKey)
        );
        builder.Services.AddScoped<IYouTubeService, YouTubeService>(s => new YouTubeService(youTubeKey));
        builder.Services.AddScoped<IPollsService, PollsService>(s => new PollsService(pollsKey));

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
                SeedUsers
                    .InitializeAdmin(
                        services,
                        "admin@example.com",
                        "admin",
                        adminPw,
                        "The",
                        "Admin",
                        4
                    )
                    .Wait();
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
            // app.UseMigrationsEndPoint();

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

        //app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseRouting();
        app.UseAuthentication();
        app.UseAuthorization();

        app.MapControllerRoute(name: "default", pattern: "{controller=Home}/{action=Index}/{id?}");

        app.MapRazorPages();
        app.Run();
    }
}
