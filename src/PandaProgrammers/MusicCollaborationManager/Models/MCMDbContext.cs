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
            optionsBuilder.UseSqlServer("Name=MCMConnection");
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Comment>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Comment__3214EC279D191712");

            entity.ToTable("Comment");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.ListenerId).HasColumnName("ListenerID");
            entity.Property(e => e.Message)
                .IsRequired()
                .HasMaxLength(300);
            entity.Property(e => e.PlaylistId).HasColumnName("PlaylistID");

            entity.HasOne(d => d.Listener).WithMany(p => p.Comments)
                .HasForeignKey(d => d.ListenerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Fk_Comment_Listener_ID");

            entity.HasOne(d => d.Playlist).WithMany(p => p.Comments)
                .HasForeignKey(d => d.PlaylistId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Fk_Comment_Playlist_ID");
        });

        modelBuilder.Entity<Listener>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Listener__3214EC270E713F05");

            entity.ToTable("Listener");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.AspnetIdentityId)
                .IsRequired()
                .HasMaxLength(64)
                .HasColumnName("ASPNetIdentityID");
            entity.Property(e => e.FirstName)
                .IsRequired()
                .HasMaxLength(64);
            entity.Property(e => e.FriendId).HasColumnName("FriendID");
            entity.Property(e => e.LastName)
                .IsRequired()
                .HasMaxLength(64);
            entity.Property(e => e.SpotifyId)
                .IsRequired()
                .HasMaxLength(128)
                .HasColumnName("SpotifyID");
        });

        modelBuilder.Entity<Playlist>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Playlist__3214EC2736AC0CC5");

            entity.ToTable("Playlist");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.ServiceId).HasColumnName("ServiceID");
        });

        modelBuilder.Entity<Theme>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Theme__3214EC27612BA41A");

            entity.ToTable("Theme");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Font)
                .IsRequired()
                .HasMaxLength(32);
            entity.Property(e => e.ListenerId).HasColumnName("ListenerID");
            entity.Property(e => e.PrimaryColor)
                .IsRequired()
                .HasMaxLength(6);
            entity.Property(e => e.SecondaryColor)
                .IsRequired()
                .HasMaxLength(6);

            entity.HasOne(d => d.Listener).WithMany(p => p.Themes)
                .HasForeignKey(d => d.ListenerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Fk_Theme_Listener_ID");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
