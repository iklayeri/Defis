using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Defis.Models;

public partial class DataContext : DbContext
{
    public DataContext()
    {
    }

    public DataContext(DbContextOptions<DataContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Auteur> Auteurs { get; set; }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<Film> Films { get; set; }

    public virtual DbSet<Location> Locations { get; set; }

    public virtual DbSet<Utilisateur> Utilisateurs { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("server=5.182.33.229;Port=5430;Database=movies;Username=postgres;Password=postgres");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Auteur>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("auteurs_pkey");

            entity.ToTable("auteurs");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Code)
                .HasMaxLength(50)
                .HasColumnName("code");
            entity.Property(e => e.DateCreation)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("date_creation");
            entity.Property(e => e.DateModification)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("date_modification");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.EstActif)
                .HasDefaultValue(false)
                .HasColumnName("est_actif");
            entity.Property(e => e.Libelle)
                .HasMaxLength(220)
                .HasColumnName("libelle");
            entity.Property(e => e.ModifierPar)
                .HasMaxLength(30)
                .HasColumnName("modifier_par");
            entity.Property(e => e.Userid)
                .HasMaxLength(30)
                .HasColumnName("userid");
        });

        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("categories_pkey");

            entity.ToTable("categories");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Code)
                .HasMaxLength(50)
                .HasColumnName("code");
            entity.Property(e => e.DateCreation)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("date_creation");
            entity.Property(e => e.DateModification)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("date_modification");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.EstActif)
                .HasDefaultValue(false)
                .HasColumnName("est_actif");
            entity.Property(e => e.Libelle)
                .HasMaxLength(220)
                .HasColumnName("libelle");
            entity.Property(e => e.ModifierPar)
                .HasMaxLength(30)
                .HasColumnName("modifier_par");
            entity.Property(e => e.Userid)
                .HasMaxLength(30)
                .HasColumnName("userid");
        });

        modelBuilder.Entity<Film>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("films_pkey");

            entity.ToTable("films");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.AuteurId).HasColumnName("auteur_id");
            entity.Property(e => e.CategorieId).HasColumnName("categorie_id");
            entity.Property(e => e.Code)
                .HasMaxLength(50)
                .HasColumnName("code");
            entity.Property(e => e.DateCreation)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("date_creation");
            entity.Property(e => e.DateModification)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("date_modification");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.Duree).HasColumnName("duree");
            entity.Property(e => e.EstActif)
                .HasDefaultValue(false)
                .HasColumnName("est_actif");
            entity.Property(e => e.EstDisponible)
                .HasDefaultValue(true)
                .HasColumnName("est_disponible");
            entity.Property(e => e.ModifierPar)
                .HasMaxLength(30)
                .HasColumnName("modifier_par");
            entity.Property(e => e.Titre)
                .HasMaxLength(220)
                .HasColumnName("titre");
            entity.Property(e => e.Userid)
                .HasMaxLength(30)
                .HasColumnName("userid");

            entity.HasOne(d => d.Auteur).WithMany(p => p.Films)
                .HasForeignKey(d => d.AuteurId)
                .HasConstraintName("films_auteur_id_fkey");

            entity.HasOne(d => d.Categorie).WithMany(p => p.Films)
                .HasForeignKey(d => d.CategorieId)
                .HasConstraintName("films_categorie_id_fkey");
        });

        modelBuilder.Entity<Location>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("locations_pkey");

            entity.ToTable("locations");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.DateCreation)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("date_creation");
            entity.Property(e => e.DateDebut)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("date_debut");
            entity.Property(e => e.DateFin)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("date_fin");
            entity.Property(e => e.DateModification)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("date_modification");
            entity.Property(e => e.EstActif)
                .HasDefaultValue(false)
                .HasColumnName("est_actif");
            entity.Property(e => e.FilmId).HasColumnName("film_id");
            entity.Property(e => e.ModifierPar)
                .HasMaxLength(30)
                .HasColumnName("modifier_par");
            entity.Property(e => e.Userid)
                .HasMaxLength(30)
                .HasColumnName("userid");
            entity.Property(e => e.UtilisateurId).HasColumnName("utilisateur_id");

            entity.HasOne(d => d.Film).WithMany(p => p.Locations)
                .HasForeignKey(d => d.FilmId)
                .HasConstraintName("locations_film_id_fkey");

            entity.HasOne(d => d.Utilisateur).WithMany(p => p.Locations)
                .HasForeignKey(d => d.UtilisateurId)
                .HasConstraintName("locations_utilisateur_id_fkey");
        });

        modelBuilder.Entity<Utilisateur>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("utilisateurs_pkey");

            entity.ToTable("utilisateurs");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.AdresseGeographique).HasColumnName("adresse_geographique");
            entity.Property(e => e.DateCreation)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("date_creation");
            entity.Property(e => e.DateModification)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("date_modification");
            entity.Property(e => e.EstActif)
                .HasDefaultValue(false)
                .HasColumnName("est_actif");
            entity.Property(e => e.ModifierPar)
                .HasMaxLength(30)
                .HasColumnName("modifier_par");
            entity.Property(e => e.Nom)
                .HasMaxLength(200)
                .HasColumnName("nom");
            entity.Property(e => e.Prenoms)
                .HasMaxLength(255)
                .HasColumnName("prenoms");
            entity.Property(e => e.Telephone)
                .HasMaxLength(50)
                .HasColumnName("telephone");
            entity.Property(e => e.Userid)
                .HasMaxLength(30)
                .HasColumnName("userid");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
