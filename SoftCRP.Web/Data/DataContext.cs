using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SoftCRP.Web.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SoftCRP.Web.Data
{
    public class DataContext : IdentityDbContext<User>
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }
        public DbSet<Cliente> Clientes { get; set; }

        public DbSet<TipoAnalisis> TiposAnalisis { get; set; }
        public DbSet<Analisis> Analises { get; set; }
        public DbSet<ArchivoAnalisis> ArchivosAnalisis { get; set; }


        public DbSet<TipoNovedades> tipoNovedades { get; set; }
        public DbSet<Novedad> novedades { get; set; }
        public DbSet<ArchivoNovedades> archivoNovedades { get; set; }


        public DbSet<TipoTramite> tipoTramites { get; set; }
        public DbSet<Tramite> tramites { get; set; }
        public DbSet<ArchivoTramites> archivoTramites { get; set; }


        public DbSet<TipoCapacitacion> tipoCapacitaciones { get; set; }
        public DbSet<Capacitacion> capacitaciones { get; set; }
        public DbSet<ArchivoCapacitaciones> archivoCapacitaciones { get; set; }

        //Logs
        public DbSet<Log> Logs { get; set; }
        public DbSet<LogNovedad> logNovedades { get; set; }

        // version 2

        public DbSet<Vehiculo> vehiculos { get; set; }
        public DbSet<VehiculoGps> vehiculosGps { get; set; }
        public DbSet<Gama> gamas { get; set; }
        public DbSet<Incidencia> incidencias { get; set; }
        public DbSet<Ciudad> ciudades { get; set; }

        public DbSet<LastRecordIturan> lastRecords { get; set; }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<VehiculoGps>()
            .Property(p => p.kilometerstraveled)
            .HasColumnType("decimal(18,4)");

            modelBuilder.Entity<Incidencia>()
            .Property(p => p.AceleracionesBruscas)
            .HasColumnType("decimal(18,1)");

            modelBuilder.Entity<Incidencia>()
            .Property(p => p.ExcesoVelocidad)
            .HasColumnType("decimal(18,1)");

            modelBuilder.Entity<Incidencia>()
            .Property(p => p.FrenazoBrusco)
            .HasColumnType("decimal(18,1)");

            modelBuilder.Entity<Incidencia>()
            .Property(p => p.GiroBrusco)
            .HasColumnType("decimal(18,1)");

            var cascadeFKs = modelBuilder.Model
                .G­etEntityTypes()
                .SelectMany(t => t.GetForeignKeys())
                .Where(fk => !fk.IsOwnership && fk.DeleteBehavior == DeleteBehavior.Casca­de);
            foreach (var fk in cascadeFKs)
            {
                fk.DeleteBehavior = DeleteBehavior.Restr­ict;
            }

            base.OnModelCreating(modelBuilder);
        }
    }
}
