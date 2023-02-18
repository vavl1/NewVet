using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Dal.DbModels;

public partial class DefaultDbContext : DbContext
{
    public DefaultDbContext()
    {
    }

    public DefaultDbContext(DbContextOptions<DefaultDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Animal> Animals { get; set; }

    public virtual DbSet<AnimalOwner> AnimalOwners { get; set; }

    public virtual DbSet<Diagnosis> Diagnoses { get; set; }

    public virtual DbSet<Treatment> Treatments { get; set; }

    public virtual DbSet<Vet> Vets { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=IGA;Initial Catalog=VetClinicDb; TrustServerCertificate=True; Integrated security=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Animal>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Animals__3214EC07F2EA635F");

            entity.Property(e => e.Birthay).HasColumnType("datetime");

            entity.HasOne(d => d.AnimalOwnerNavigation).WithMany(p => p.Animals)
                .HasForeignKey(d => d.AnimalOwner)
                .HasConstraintName("FK__Animals__AnimalO__15502E78");

            entity.HasOne(d => d.Vet).WithMany(p => p.Animals)
                .HasForeignKey(d => d.VetId)
                .HasConstraintName("FK__Animals__VetId__145C0A3F");
        });

        modelBuilder.Entity<AnimalOwner>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__AnimalOw__3214EC0750803F04");

            entity.Property(e => e.Adress).HasColumnName("adress");
        });

        modelBuilder.Entity<Diagnosis>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Diagnose__3214EC075CF939FB");

            entity.Property(e => e.Date).HasColumnType("datetime");

            entity.HasOne(d => d.Animal).WithMany(p => p.Diagnoses)
                .HasForeignKey(d => d.AnimalId)
                .HasConstraintName("FK__Diagnoses__Anima__1920BF5C");

            entity.HasOne(d => d.Vet).WithMany(p => p.Diagnoses)
                .HasForeignKey(d => d.VetId)
                .HasConstraintName("FK__Diagnoses__VetId__182C9B23");
        });

        modelBuilder.Entity<Treatment>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Treatmen__3214EC07C513B4D6");

            entity.Property(e => e.DateEnd).HasColumnType("datetime");
            entity.Property(e => e.DateStart).HasColumnType("datetime");

            entity.HasOne(d => d.Animal).WithMany(p => p.Treatments)
                .HasForeignKey(d => d.AnimalId)
                .HasConstraintName("FK__Treatment__Anima__1CF15040");

            entity.HasOne(d => d.Vet).WithMany(p => p.Treatments)
                .HasForeignKey(d => d.VetId)
                .HasConstraintName("FK__Treatment__VetId__1BFD2C07");
        });

        modelBuilder.Entity<Vet>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Vets__3214EC077D9725FB");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
