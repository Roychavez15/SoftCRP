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
        public DbSet<LogNovedad> logNovedades { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<Menu>()
            //.Property(p => p.Price)
            //.HasColumnType("decimal(18,2)");

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
