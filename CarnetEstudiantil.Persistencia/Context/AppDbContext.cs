using System;
using System.Collections.Generic;
using System.Text;
using CarnetEstudiantil.Persistencia.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;

namespace CarnetEstudiantil.Persistencia.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Estudiante> Estudiantes { get; set; }
        public DbSet<Carnet> Carnets { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            //USUARIOS
            modelBuilder.Entity<Usuario>()
                .ToTable("Usuarios");
            modelBuilder.Entity<Usuario>()
                .HasKey(u => u.IdUsuario);

            //ESTUDIANTES
            modelBuilder.Entity<Estudiante>()
                .ToTable("Estudiantes");
            modelBuilder.Entity<Estudiante>()
                .HasKey(e => e.IdEstudiante);

            //CARNETS
            modelBuilder.Entity<Carnet>()
                .ToTable("Carnets");
            modelBuilder.Entity<Carnet>()
                .HasKey(c => c.IdCarnet);

            // -------------RELACIONES ENTRE ENTIDADES-----------------//

            // Configuración de la relación entre Usuario y Estudiante
            modelBuilder.Entity<Usuario>()
                .HasOne(u => u.Estudiante)
                .WithOne(e => e.Usuario)
                .HasForeignKey<Estudiante>(e => e.IdUsuario);
            // Configuración de la relación entre Estudiante y Carnet
            modelBuilder.Entity<Estudiante>()
                .HasOne(e => e.Carnet)
                .WithOne(c => c.Estudiante)
                .HasForeignKey<Carnet>(c => c.IdEstudiante);

        }
    }
}
