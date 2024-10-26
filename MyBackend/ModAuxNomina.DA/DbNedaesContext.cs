using Microsoft.EntityFrameworkCore;
using MyBackend.ModAuxNomina.Models.Anticipos;

namespace MyBackend.ModAuxNomina.DA
{
    public partial class DbNedaesContext : DbContext
    {
        public DbNedaesContext()
        {
        }

        public DbNedaesContext(DbContextOptions<DbNedaesContext> options)
            : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // ------------ Empleado 
            modelBuilder.Entity<Empleado>()
                .HasKey(e => new { e.dni, e.claseNomina, e.codigoHabilitacion, e.duplicado, e.numeroNomina });

            modelBuilder.Entity<Empleado>()
                .HasAlternateKey(e => new { e.dni, e.claseNomina });

            modelBuilder.Entity<Empleado>()
                .HasQueryFilter(e => e.codigoHabilitacion == ConstantesHabilitacion.HACIENDA &&
                                        e.duplicado == "N" &&
                                        e.numeroNomina == "01");

            // Configuración de la relación entre Anticipo y Empleado usando la clave alternativa
            modelBuilder.Entity<Anticipo>()
                .HasOne(a => a.Empleado)
                .WithMany()
                .HasForeignKey(a => new { a.dni, a.claseNomina })
                .HasPrincipalKey(e => new { e.dni, e.claseNomina });


            // ------------  TPersonales 
            modelBuilder.Entity<TPersonales>()
                    .HasKey(e => new { e.dni, e.claseNomina });

            modelBuilder.Entity<TPersonales>()
                    .HasQueryFilter(a => a.codigoHabilitacion == ConstantesHabilitacion.HACIENDA &&
                                            a.duplicado == "N");

            modelBuilder.Entity<Anticipo>()
                .HasOne(a => a.TPersonales)
                .WithMany()
                .HasForeignKey(a => new { a.dni, a.claseNomina })
                .HasPrincipalKey(e => new { e.dni, e.claseNomina });

            // ------------  AmortizacionDetalle 
            modelBuilder.Entity<AmortizacionDetalle>().HasNoKey();

            // ------------ OtrosConceptos
            modelBuilder.Entity<OtrosConceptos>()
               .HasKey(e => new { e.idTipoAnticipo, e.idTipoObservacion });

            // ------------  Amortizacion 
            modelBuilder.Entity<Amortizacion>()
                .HasOne(a => a.TiposObservacion)
                .WithMany()
                .HasForeignKey(a => a.tipoAmortizacion);

            // ------------  OtrosConceptos
            modelBuilder.Entity<OtrosConceptos>()
                .HasOne(oc => oc.TiposObservacion)
                .WithMany()
                .HasForeignKey(oc => oc.idTipoObservacion);

            // ------------  AmortizacionDetalle 
            modelBuilder.Entity<AnticipoHistorial>().HasNoKey();

            // ------------  SalidaCalculoAnticipo 
            modelBuilder.Entity<SalidaCalculoAnticipo>().HasNoKey();

            modelBuilder.Entity<VSqlTcrcuant>()
                .HasKey(e => new { e.CDHABIL, e.CDCLASNM, e.CDCONCRT, e.CDIMPORT, e.FEVIDES });

            modelBuilder.Entity<VSqlTcrcuantpaex>()
                .HasKey(e => new { e.CDHABIL, e.CDCLASNM, e.CDCLAPEX, e.CDNUPAEX, e.CDCONCRT, e.CDIMPORT, e.FEVIDES });
        }

        public virtual DbSet<Empleado> DATOS_PERSONA { get; set; }
        public virtual DbSet<Nomina> V_SQL_TCLASNOM { get; set; }
        public virtual DbSet<Anticipo> HAT_ANTICIPOS { get; set; }
        public virtual DbSet<Amortizacion> HAT_AMORTIZACIONES { get; set; }
        public virtual DbSet<TPersonales> V_SQL_TPERSONALES { get; set; }
        public virtual DbSet<EstadosAnticipos> HAT_ESTADO_ANTICIPOS { get; set; }
        public virtual DbSet<TiposObservacion> HAT_TIPOS_OBSERVACION { get; set; }
        public virtual DbSet<OtrosConceptos> HAT_OTROS_CONCEPTOS { get; set; }
        public virtual DbSet<AnticipoHistorial> AnticipoHistorial { get; set; }
        public virtual DbSet<AmortizacionDetalle> AmortizacionDetalle { get; set; }
        public virtual DbSet<AnticipoCompleto> AnticipoCompleto { get; set; }
        public virtual DbSet<AnticipoCompletoSP> AnticipoCompletoSP { get; set; }
        public virtual DbSet<VSqlTtipjor> V_SQL_TTIPJOR { get; set; }
        public DbSet<SalidaCalculoAnticipo> SalidaCalculoAnticipos { get; set; }
        public virtual DbSet<VSqlTcrcuantpaex> V_SQL_TCRCUANTPAEX { get; set; }
        public virtual DbSet<VSqlTcrcuant> V_SQL_TCRCUANT { get; set; }
    }
}
