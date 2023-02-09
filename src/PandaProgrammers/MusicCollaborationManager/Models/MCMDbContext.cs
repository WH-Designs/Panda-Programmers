using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace MusicCollaborationManager.Models;

public partial class MCMDbContext : DbContext
{
    public MCMDbContext()
    {
    }

    public MCMDbContext(DbContextOptions<MCMDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Comment> Comments { get; set; }

    public virtual DbSet<Listener> Listeners { get; set; }

    public virtual DbSet<Playlist> Playlists { get; set; }

    public virtual DbSet<Theme> Themes { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder
                .UseLazyLoadingProxies()        
                .UseSqlServer("Name=MCMConnection");
        }
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Comment>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Comment__3214EC27BD2E5118");

            entity.HasOne(d => d.Listener).WithMany(p => p.Comments)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Fk_Comment_Listener_ID");

            entity.HasOne(d => d.Playlist).WithMany(p => p.Comments)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Fk_Comment_Playlist_ID");
        });

        modelBuilder.Entity<Listener>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Listener__3214EC278EEF8DAB");
        });

        modelBuilder.Entity<Playlist>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Playlist__3214EC278D078F50");
        });

        modelBuilder.Entity<Theme>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Theme__3214EC274BB513C1");

            entity.HasOne(d => d.Listener).WithMany(p => p.Themes)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Fk_Theme_Listener_ID");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
