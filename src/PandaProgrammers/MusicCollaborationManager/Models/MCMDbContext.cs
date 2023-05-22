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

    public virtual DbSet<Poll> Polls { get; set; }

    public virtual DbSet<Prompt> Prompts { get; set; }

    public virtual DbSet<SpotifyAuthorizationNeededListener> SpotifyAuthorizationNeededListeners { get; set; }

    public virtual DbSet<Tutorial> Tutorials { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Name=MCMConnection");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Comment>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Comment__3214EC2774053D49");

            entity.ToTable("Comment");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.ListenerId).HasColumnName("ListenerID");
            entity.Property(e => e.Message)
                .IsRequired()
                .HasMaxLength(300);
            entity.Property(e => e.SpotifyId)
                .IsRequired()
                .HasMaxLength(128)
                .HasColumnName("SpotifyID");

            entity.HasOne(d => d.Listener).WithMany(p => p.Comments)
                .HasForeignKey(d => d.ListenerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Fk_Comment_Listener_ID");
        });

        modelBuilder.Entity<Listener>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Listener__3214EC27F70E25AB");

            entity.ToTable("Listener");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.AspnetIdentityId)
                .IsRequired()
                .HasMaxLength(64)
                .HasColumnName("ASPNetIdentityID");
            entity.Property(e => e.AuthRefreshToken).HasMaxLength(512);
            entity.Property(e => e.AuthToken).HasMaxLength(512);
            entity.Property(e => e.FirstName)
                .IsRequired()
                .HasMaxLength(64);
            entity.Property(e => e.FriendId).HasColumnName("FriendID");
            entity.Property(e => e.LastName)
                .IsRequired()
                .HasMaxLength(64);
            entity.Property(e => e.SpotifyId)
                .HasMaxLength(128)
                .HasColumnName("SpotifyID");
            entity.Property(e => e.SpotifyUserName).HasMaxLength(128);
            entity.Property(e => e.Theme).HasMaxLength(255);
        });

        modelBuilder.Entity<Playlist>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Playlist__3214EC277D22E16C");

            entity.ToTable("Playlist");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.ServiceId).HasColumnName("ServiceID");
        });

        modelBuilder.Entity<Poll>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Polls__3214EC274641B1B1");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.PollId)
                .IsRequired()
                .HasMaxLength(64)
                .HasColumnName("PollID");
            entity.Property(e => e.SpotifyPlaylistId)
                .IsRequired()
                .HasMaxLength(64)
                .HasColumnName("SpotifyPlaylistID");
            entity.Property(e => e.SpotifyTrackUri)
                .IsRequired()
                .HasMaxLength(64);
        });

        modelBuilder.Entity<Prompt>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Prompts__3214EC27183288FC");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Prompt1)
                .IsRequired()
                .HasMaxLength(512)
                .HasColumnName("Prompt");
        });

        modelBuilder.Entity<SpotifyAuthorizationNeededListener>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__SpotifyA__3214EC270EBDB195");

            entity.ToTable("SpotifyAuthorizationNeededListener");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Email)
                .IsRequired()
                .HasMaxLength(256);
            entity.Property(e => e.ListenerId).HasColumnName("ListenerID");
            entity.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(64);

            entity.HasOne(d => d.Listener).WithMany(p => p.SpotifyAuthorizationNeededListeners)
                .HasForeignKey(d => d.ListenerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Fk_SpotifyAuthorizationNeededListener_Listener_ID");
        });

        modelBuilder.Entity<Tutorial>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Tutorial__3214EC27665D3E3B");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Link)
                .IsRequired()
                .HasMaxLength(512);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
