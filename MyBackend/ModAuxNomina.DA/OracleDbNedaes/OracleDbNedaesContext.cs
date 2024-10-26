using System;
using Microsoft.EntityFrameworkCore;
using MyBackend.ModAuxNomina.Models.Oracle;

namespace MyBackend.ModAuxNomina.DA.OracleNedaes
{
    public partial class OracleDbNedaesContext : DbContext
    {
        public OracleDbNedaesContext(DbContextOptions<OracleDbNedaesContext> options)
            : base(options)
        {
        }

        public virtual DbSet<EmpleadoOracle> VM_DATOS_EMPLEADOS_NEDAES { get; set; }
        public virtual DbSet<TnomiCur> TNOMICUR { get; set; }
        public virtual DbSet<Hperret> HPERRET { get; set; }
        public virtual DbSet<Hpeirpfn> HPEIRPFN { get; set; }
        public virtual DbSet<Tcrcuant> TCRCUANT { get; set; }
        public virtual DbSet<Tcrcuantpaex> TCRCUANTPAEX { get; set; }
        public virtual DbSet<Ttipjor> TTIPJOR { get; set; }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<TnomiCur>(entity =>
            {
                entity.ToTable("TNOMICUR");
                entity.HasKey(k => new { k.CDHABIL, k.CDCLASNM });
                entity.HasQueryFilter(e => e.CDHABIL == ConstantesHabilitacion.HACIENDA);
            });

            modelBuilder.Entity<EmpleadoOracle>(entity =>
           {
               entity.ToTable("VM_DATOS_EMPLEADOS_NEDAES");
               entity.HasNoKey();
               entity.HasQueryFilter(e => e.CDHABIL == ConstantesHabilitacion.HACIENDA);
           });

            // ------------  Hperret 
            modelBuilder.Entity<Hperret>()
                .HasKey(e => new { e.CDHABIL, e.CDCLASNM, e.CDDNI, e.CDDUP, e.CDNUMNOM, e.FENOMDES, e.FENOMHAS });

            modelBuilder.Entity<Hperret>()
                .HasOne(h => h.Ttipjor)  
                .WithOne(t => t.Hperret)  
                .HasForeignKey<Hperret>(h => new { h.CDHABIL, h.CDTIPJOR })  
                .HasPrincipalKey<Ttipjor>(t => new { t.CDHABIL, t.CDTIPJOR });

            // ------------  Hpeirpfn
            modelBuilder.Entity<Hpeirpfn>()
                .HasKey(e => new { e.CDHABIL, e.CDCLASNM, e.CDDNI, e.CDDUP, e.CDNUMNOM, e.FENOMDES });

            // ------------  Tcrcuant
            modelBuilder.Entity<Tcrcuant>()
                .HasKey(e => new { e.CDHABIL, e.CDCLASNM, e.CDCONCRT, e.CDIMPORT, e.FEVIDES });

            // ------------  Tcrcuantpaex
            modelBuilder.Entity<Tcrcuantpaex>()
                .HasKey(e => new { e.CDHABIL, e.CDCLASNM, e.CDCLAPEX, e.CDNUPAEX, e.CDCONCRT, e.CDIMPORT, e.FEVIDES });


            // ------------ Relación entre Hperret y Hpeirpfn
            modelBuilder.Entity<Hperret>()
                    .HasOne(h => h.Hpeirpfn)
                    .WithOne(hp => hp.Hperret)
                    .HasForeignKey<Hpeirpfn>(hp => new
                    {
                        hp.CDHABIL,
                        hp.CDCLASNM,
                        hp.CDDNI,
                        hp.CDDUP,
                        hp.FENOMDES
                    }) // Llaves foráneas que conectan ambas tablas
                    .HasPrincipalKey<Hperret>(h => new
                    {
                        h.CDHABIL,
                        h.CDCLASNM,
                        h.CDDNI,
                        h.CDDUP,
                        h.FENOMDES
                    }); // Llaves principales en Hperret


            // // Relación entre Hperret y Tcrcuant
            // modelBuilder.Entity<Hperret>()
            //         .HasMany(h => h.Tcrcuant)
            //         .WithOne(t => t.Hperret)
            //         .HasForeignKey(t => new { t.CDHABIL, t.CDCLASNM });

            modelBuilder.Entity<Hperret>()
                .HasOne(h => h.Tcrcuant)  
                .WithOne(t => t.Hperret)  
                .HasForeignKey<Hperret>(h => new { h.CDHABIL, h.CDCLASNM, h.KKGRUPO })  
                .HasPrincipalKey<Tcrcuant>(t => new { t.CDHABIL, t.CDCLASNM, t.CDIMPORT });

            // Relación entre Hperret y Tcrcuantpaex
           modelBuilder.Entity<Hperret>()
                .HasOne(h => h.Tcrcuantpaex)  
                .WithOne(t => t.Hperret)  
                .HasForeignKey<Hperret>(h => new { h.CDHABIL, h.CDCLASNM, h.KKGRUPO })  
                .HasPrincipalKey<Tcrcuantpaex>(t => new { t.CDHABIL, t.CDCLASNM, t.CDIMPORT });
        }
    }
}

