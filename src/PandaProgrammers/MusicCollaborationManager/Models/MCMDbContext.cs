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

<<<<<<< HEAD
    public virtual DbSet<Poll> Polls { get; set; }

=======
>>>>>>> dev
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Name=MCMConnection");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Comment>(entity =>
        {
<<<<<<< HEAD
            entity.HasKey(e => e.Id).HasName("PK__Comment__3214EC27D6E96E08");
=======
            entity.HasKey(e => e.Id).HasName("PK__Comment__3214EC274FA24D65");
>>>>>>> dev

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
<<<<<<< HEAD
            entity.HasKey(e => e.Id).HasName("PK__Listener__3214EC27CBB586FA");
=======
            entity.HasKey(e => e.Id).HasName("PK__Listener__3214EC270A6840D0");
>>>>>>> dev

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
<<<<<<< HEAD
            entity.HasKey(e => e.Id).HasName("PK__Playlist__3214EC27F368DDDB");
=======
            entity.HasKey(e => e.Id).HasName("PK__Playlist__3214EC271706E24D");
>>>>>>> dev

            entity.ToTable("Playlist");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.ServiceId).HasColumnName("ServiceID");
        });

        modelBuilder.Entity<Poll>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Polls__3214EC27C6EA2370");

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

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
