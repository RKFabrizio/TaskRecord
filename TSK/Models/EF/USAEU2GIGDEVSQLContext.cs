using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace TSK.Models.EF
{
    public partial class USAEU2GIGDEVSQLContext : DbContext
    {
        public USAEU2GIGDEVSQLContext()
        {
        }

        public USAEU2GIGDEVSQLContext(DbContextOptions<USAEU2GIGDEVSQLContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Actividad> Actividads { get; set; }
        public virtual DbSet<Auditorium> Auditoria { get; set; }
        public virtual DbSet<ClaseMantencion> ClaseMantencions { get; set; }
        public virtual DbSet<Complemento> Complementos { get; set; }
        public virtual DbSet<Componente> Componentes { get; set; }
        public virtual DbSet<Condicion> Condicions { get; set; }
        public virtual DbSet<Consecuencium> Consecuencia { get; set; }
        public virtual DbSet<Disciplina> Disciplinas { get; set; }
        public virtual DbSet<Empresa> Empresas { get; set; }
        public virtual DbSet<Entrega> Entregas { get; set; }
        public virtual DbSet<Equipo> Equipos { get; set; }
        public virtual DbSet<Estado> Estados { get; set; }
        public virtual DbSet<Flotum> Flota { get; set; }
        public virtual DbSet<Frecuencium> Frecuencia { get; set; }
        public virtual DbSet<Herramientum> Herramienta { get; set; }
        public virtual DbSet<Nivel> Nivels { get; set; }
        public virtual DbSet<Operacion> Operacions { get; set; }
        public virtual DbSet<Pm> Pms { get; set; }
        public virtual DbSet<PmSistema> PmSistemas { get; set; }
        public virtual DbSet<PmsisActividad> PmsisActividads { get; set; }
        public virtual DbSet<Rango> Rangos { get; set; }
        public virtual DbSet<RepDetalle> RepDetalles { get; set; }
        public virtual DbSet<RepEntrega> RepEntregas { get; set; }
        public virtual DbSet<RepLider> RepLiders { get; set; }
        public virtual DbSet<RepSistema> RepSistemas { get; set; }
        public virtual DbSet<RepTecnico> RepTecnicos { get; set; }
        public virtual DbSet<Reporte> Reportes { get; set; }
        public virtual DbSet<Repuesto> Repuestos { get; set; }
        public virtual DbSet<Rol> Rols { get; set; }
        public virtual DbSet<Sector> Sectors { get; set; }
        public virtual DbSet<Sistema> Sistemas { get; set; }
        public virtual DbSet<Tab> Tabs { get; set; }
        public virtual DbSet<Tabla> Tablas { get; set; }
        public virtual DbSet<Tarea> Tareas { get; set; }
        public virtual DbSet<TipoEquipo> TipoEquipos { get; set; }
        public virtual DbSet<Turno> Turnos { get; set; }
        public virtual DbSet<Unidad> Unidads { get; set; }
        public virtual DbSet<UnidadMedidum> UnidadMedida { get; set; }
        public virtual DbSet<Usuario> Usuarios { get; set; }
        public virtual DbSet<VwPmSistema> VwPmSistemas { get; set; }
        public virtual DbSet<VwPmsisActividad> VwPmsisActividads { get; set; }
        public virtual DbSet<VwSistema> VwSistemas { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=SGK-SOPORTE-03;Database=USAEU2GIG-DEV-SQL;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Actividad>(entity =>
            {
                entity.HasKey(e => e.IdAct);

                entity.ToTable("ACTIVIDAD");

                entity.Property(e => e.IdAct).HasColumnName("ID_ACT");

                entity.Property(e => e.Especificacion)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("ESPECIFICACION");

                entity.Property(e => e.Extracolumn1)
                    .IsUnicode(false)
                    .HasColumnName("EXTRACOLUMN1");

                entity.Property(e => e.Extracolumn2)
                    .IsUnicode(false)
                    .HasColumnName("EXTRACOLUMN2");

                entity.Property(e => e.Extracolumn3)
                    .IsUnicode(false)
                    .HasColumnName("EXTRACOLUMN3");

                entity.Property(e => e.Fin).HasColumnName("FIN");

                entity.Property(e => e.Habilitado)
                    .HasColumnName("HABILITADO")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.IdClm)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("ID_CLM");

                entity.Property(e => e.IdCon)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("ID_CON");

                entity.Property(e => e.IdFrc).HasColumnName("ID_FRC");

                entity.Property(e => e.IdRan).HasColumnName("ID_RAN");

                entity.Property(e => e.IdUm)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("ID_UM");

                entity.Property(e => e.Inicio).HasColumnName("INICIO");

                entity.Property(e => e.Referencia)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("REFERENCIA");

                entity.Property(e => e.ReferenciaUrl)
                    .HasColumnType("text")
                    .HasColumnName("REFERENCIA_URL");

                entity.Property(e => e.Titulo)
                    .HasMaxLength(800)
                    .IsUnicode(false)
                    .HasColumnName("TITULO");

                entity.HasOne(d => d.IdClmNavigation)
                    .WithMany(p => p.Actividads)
                    .HasForeignKey(d => d.IdClm)
                    .HasConstraintName("FK_ACTIVIDAD_CLASE_MANTENCION");

                entity.HasOne(d => d.IdConNavigation)
                    .WithMany(p => p.Actividads)
                    .HasForeignKey(d => d.IdCon)
                    .HasConstraintName("FK_ACTIVIDAD_CONSECUENCIA");

                entity.HasOne(d => d.IdFrcNavigation)
                    .WithMany(p => p.Actividads)
                    .HasForeignKey(d => d.IdFrc)
                    .HasConstraintName("FK_ACTIVIDAD_FRECUENCIA");

                entity.HasOne(d => d.IdRanNavigation)
                    .WithMany(p => p.Actividads)
                    .HasForeignKey(d => d.IdRan)
                    .HasConstraintName("FK_ACTIVIDAD_RANGO");

                entity.HasOne(d => d.IdUmNavigation)
                    .WithMany(p => p.Actividads)
                    .HasForeignKey(d => d.IdUm)
                    .HasConstraintName("FK_ACTIVIDAD_UNIDAD_MEDIDA");
            });

            modelBuilder.Entity<Auditorium>(entity =>
            {
                entity.HasKey(e => e.IdAud)
                    .HasName("XPKAUDITORIA");

                entity.ToTable("AUDITORIA");

                entity.Property(e => e.IdAud).HasColumnName("ID_AUD");

                entity.Property(e => e.Dia)
                    .HasColumnType("datetime")
                    .HasColumnName("DIA");

                entity.Property(e => e.Extracolumn1)
                    .IsUnicode(false)
                    .HasColumnName("EXTRACOLUMN1");

                entity.Property(e => e.Extracolumn2)
                    .IsUnicode(false)
                    .HasColumnName("EXTRACOLUMN2");

                entity.Property(e => e.Extracolumn3)
                    .IsUnicode(false)
                    .HasColumnName("EXTRACOLUMN3");

                entity.Property(e => e.Habilitado)
                    .HasColumnName("HABILITADO")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.IdOpe).HasColumnName("ID_OPE");

                entity.Property(e => e.IdTab).HasColumnName("ID_TAB");

                entity.Property(e => e.IdUsr).HasColumnName("ID_USR");

                entity.HasOne(d => d.IdOpeNavigation)
                    .WithMany(p => p.Auditoria)
                    .HasForeignKey(d => d.IdOpe)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_AUDITORIA_OPERACION");

                entity.HasOne(d => d.IdUsrNavigation)
                    .WithMany(p => p.Auditoria)
                    .HasForeignKey(d => d.IdUsr)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_AUDITORIA_USUARIO");
            });

            modelBuilder.Entity<ClaseMantencion>(entity =>
            {
                entity.HasKey(e => e.IdClm)
                    .HasName("XPKCLASE_MANTENCION");

                entity.ToTable("CLASE_MANTENCION");

                entity.Property(e => e.IdClm)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("ID_CLM");

                entity.Property(e => e.Extracolumn1)
                    .IsUnicode(false)
                    .HasColumnName("EXTRACOLUMN1");

                entity.Property(e => e.Extracolumn2)
                    .IsUnicode(false)
                    .HasColumnName("EXTRACOLUMN2");

                entity.Property(e => e.Extracolumn3)
                    .IsUnicode(false)
                    .HasColumnName("EXTRACOLUMN3");

                entity.Property(e => e.Habilitado)
                    .HasColumnName("HABILITADO")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("NOMBRE");
            });

            modelBuilder.Entity<Complemento>(entity =>
            {
                entity.HasKey(e => e.IdCom)
                    .HasName("XPKCOMPLEMENTO");

                entity.ToTable("COMPLEMENTO");

                entity.Property(e => e.IdCom).HasColumnName("ID_COM");

                entity.Property(e => e.Extracolumn1).HasColumnName("EXTRACOLUMN1");

                entity.Property(e => e.Extracolumn2).HasColumnName("EXTRACOLUMN2");

                entity.Property(e => e.Extracolumn3)
                    .IsUnicode(false)
                    .HasColumnName("EXTRACOLUMN3");

                entity.Property(e => e.Habilitado)
                    .HasColumnName("HABILITADO")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.IdEqu).HasColumnName("ID_EQU");

                entity.Property(e => e.IdHerr).HasColumnName("ID_HERR");

                entity.Property(e => e.IdPms).HasColumnName("ID_PMS");

                entity.Property(e => e.IdRep).HasColumnName("ID_REP");

                entity.HasOne(d => d.IdEquNavigation)
                    .WithMany(p => p.Complementos)
                    .HasForeignKey(d => d.IdEqu)
                    .HasConstraintName("FK_COMPLEMENTO_EQUIPO");

                entity.HasOne(d => d.IdHerrNavigation)
                    .WithMany(p => p.Complementos)
                    .HasForeignKey(d => d.IdHerr)
                    .HasConstraintName("FK_COMPLEMENTO_HERRAMIENTA");

                entity.HasOne(d => d.IdPmsNavigation)
                    .WithMany(p => p.Complementos)
                    .HasForeignKey(d => d.IdPms)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_COMPLEMENTO_PM_SISTEMA");

                entity.HasOne(d => d.IdRepNavigation)
                    .WithMany(p => p.Complementos)
                    .HasForeignKey(d => d.IdRep)
                    .HasConstraintName("FK_COMPLEMENTO_REPUESTO");
            });

            modelBuilder.Entity<Componente>(entity =>
            {
                entity.HasKey(e => e.IdCom)
                    .HasName("XPKCOMPONENTE");

                entity.ToTable("COMPONENTE");

                entity.Property(e => e.IdCom).HasColumnName("ID_COM");

                entity.Property(e => e.Extracolumn1)
                    .IsUnicode(false)
                    .HasColumnName("EXTRACOLUMN1");

                entity.Property(e => e.Extracolumn2)
                    .IsUnicode(false)
                    .HasColumnName("EXTRACOLUMN2");

                entity.Property(e => e.Extracolumn3)
                    .IsUnicode(false)
                    .HasColumnName("EXTRACOLUMN3");

                entity.Property(e => e.Habilitado)
                    .HasColumnName("HABILITADO")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.IdSis).HasColumnName("ID_SIS");

                entity.HasOne(d => d.IdSisNavigation)
                    .WithMany(p => p.Componentes)
                    .HasForeignKey(d => d.IdSis)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_COMPONENTE_SISTEMA");
            });

            modelBuilder.Entity<Condicion>(entity =>
            {
                entity.HasKey(e => e.IdCod)
                    .HasName("XPKCONDICION");

                entity.ToTable("CONDICION");

                entity.Property(e => e.IdCod)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("ID_COD");

                entity.Property(e => e.Extracolumn1)
                    .IsUnicode(false)
                    .HasColumnName("EXTRACOLUMN1");

                entity.Property(e => e.Extracolumn2)
                    .IsUnicode(false)
                    .HasColumnName("EXTRACOLUMN2");

                entity.Property(e => e.Extracolumn3)
                    .IsUnicode(false)
                    .HasColumnName("EXTRACOLUMN3");

                entity.Property(e => e.Habilitado)
                    .HasColumnName("HABILITADO")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("NOMBRE");
            });

            modelBuilder.Entity<Consecuencium>(entity =>
            {
                entity.HasKey(e => e.IdCon)
                    .HasName("XPKCONSECUENCIA");

                entity.ToTable("CONSECUENCIA");

                entity.Property(e => e.IdCon)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("ID_CON");

                entity.Property(e => e.Extracolumn1)
                    .IsUnicode(false)
                    .HasColumnName("EXTRACOLUMN1");

                entity.Property(e => e.Extracolumn2)
                    .IsUnicode(false)
                    .HasColumnName("EXTRACOLUMN2");

                entity.Property(e => e.Extracolumn3)
                    .IsUnicode(false)
                    .HasColumnName("EXTRACOLUMN3");

                entity.Property(e => e.Habilitado)
                    .HasColumnName("HABILITADO")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("NOMBRE");
            });

            modelBuilder.Entity<Disciplina>(entity =>
            {
                entity.HasKey(e => e.IdDis)
                    .HasName("XPKDISCIPLINA");

                entity.ToTable("DISCIPLINA");

                entity.Property(e => e.IdDis).HasColumnName("ID_DIS");

                entity.Property(e => e.Extracolumn1)
                    .IsUnicode(false)
                    .HasColumnName("EXTRACOLUMN1");

                entity.Property(e => e.Extracolumn2)
                    .IsUnicode(false)
                    .HasColumnName("EXTRACOLUMN2");

                entity.Property(e => e.Extracolumn3)
                    .IsUnicode(false)
                    .HasColumnName("EXTRACOLUMN3");

                entity.Property(e => e.Habilitado)
                    .HasColumnName("HABILITADO")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.IdNiv)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("ID_NIV");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("NOMBRE");

                entity.HasOne(d => d.IdNivNavigation)
                    .WithMany(p => p.Disciplinas)
                    .HasForeignKey(d => d.IdNiv)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_DISCIPLINA_NIVEL");
            });

            modelBuilder.Entity<Empresa>(entity =>
            {
                entity.HasKey(e => e.IdEmp)
                    .HasName("XPKEMPRESA");

                entity.ToTable("EMPRESA");

                entity.Property(e => e.IdEmp).HasColumnName("ID_EMP");

                entity.Property(e => e.Extracolumn1)
                    .IsUnicode(false)
                    .HasColumnName("EXTRACOLUMN1");

                entity.Property(e => e.Extracolumn2)
                    .IsUnicode(false)
                    .HasColumnName("EXTRACOLUMN2");

                entity.Property(e => e.Extracolumn3)
                    .IsUnicode(false)
                    .HasColumnName("EXTRACOLUMN3");

                entity.Property(e => e.Habilitado)
                    .HasColumnName("HABILITADO")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("NOMBRE");
            });

            modelBuilder.Entity<Entrega>(entity =>
            {
                entity.HasKey(e => e.IdEnt);

                entity.ToTable("ENTREGA");

                entity.Property(e => e.IdEnt).HasColumnName("ID_ENT");

                entity.Property(e => e.Extracolumn1)
                    .HasColumnType("text")
                    .HasColumnName("EXTRACOLUMN1");

                entity.Property(e => e.Extracolumn2)
                    .HasColumnType("text")
                    .HasColumnName("EXTRACOLUMN2");

                entity.Property(e => e.Extracolumn3)
                    .HasColumnType("text")
                    .HasColumnName("EXTRACOLUMN3");

                entity.Property(e => e.Habilitado)
                    .HasColumnName("HABILITADO")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("NOMBRE");
            });

            modelBuilder.Entity<Equipo>(entity =>
            {
                entity.HasKey(e => e.IdEqu)
                    .HasName("XPKEQUIPO");

                entity.ToTable("EQUIPO");

                entity.Property(e => e.IdEqu).HasColumnName("ID_EQU");

                entity.Property(e => e.Extracolumn1)
                    .IsUnicode(false)
                    .HasColumnName("EXTRACOLUMN1");

                entity.Property(e => e.Extracolumn2)
                    .IsUnicode(false)
                    .HasColumnName("EXTRACOLUMN2");

                entity.Property(e => e.Extracolumn3)
                    .IsUnicode(false)
                    .HasColumnName("EXTRACOLUMN3");

                entity.Property(e => e.Habilitado)
                    .HasColumnName("HABILITADO")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("NOMBRE");
            });

            modelBuilder.Entity<Estado>(entity =>
            {
                entity.HasKey(e => e.IdEst);

                entity.ToTable("ESTADO");

                entity.Property(e => e.IdEst)
                    .ValueGeneratedNever()
                    .HasColumnName("ID_EST");

                entity.Property(e => e.Habilitado).HasColumnName("HABILITADO");

                entity.Property(e => e.Valor)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("VALOR");
            });

            modelBuilder.Entity<Flotum>(entity =>
            {
                entity.HasKey(e => e.IdFlt)
                    .HasName("XPKFLOTA");

                entity.ToTable("FLOTA");

                entity.Property(e => e.IdFlt).HasColumnName("ID_FLT");

                entity.Property(e => e.Extracolumn1)
                    .IsUnicode(false)
                    .HasColumnName("EXTRACOLUMN1");

                entity.Property(e => e.Extracolumn2)
                    .IsUnicode(false)
                    .HasColumnName("EXTRACOLUMN2");

                entity.Property(e => e.Extracolumn3)
                    .IsUnicode(false)
                    .HasColumnName("EXTRACOLUMN3");

                entity.Property(e => e.Flota)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("FLOTA");

                entity.Property(e => e.Habilitado)
                    .HasColumnName("HABILITADO")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.IdTeq).HasColumnName("ID_TEQ");

                entity.HasOne(d => d.IdTeqNavigation)
                    .WithMany(p => p.Flota)
                    .HasForeignKey(d => d.IdTeq)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_FLOTA_TIPO_EQUIPO");
            });

            modelBuilder.Entity<Frecuencium>(entity =>
            {
                entity.HasKey(e => e.IdFrc)
                    .HasName("XPKFRECUENCIA");

                entity.ToTable("FRECUENCIA");

                entity.Property(e => e.IdFrc).HasColumnName("ID_FRC");

                entity.Property(e => e.Extracolumn1)
                    .IsUnicode(false)
                    .HasColumnName("EXTRACOLUMN1");

                entity.Property(e => e.Extracolumn2)
                    .IsUnicode(false)
                    .HasColumnName("EXTRACOLUMN2");

                entity.Property(e => e.Extracolumn3)
                    .IsUnicode(false)
                    .HasColumnName("EXTRACOLUMN3");

                entity.Property(e => e.Habilitado)
                    .HasColumnName("HABILITADO")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("NOMBRE");
            });

            modelBuilder.Entity<Herramientum>(entity =>
            {
                entity.HasKey(e => e.IdHerr)
                    .HasName("XPKHERRAMIENTA");

                entity.ToTable("HERRAMIENTA");

                entity.Property(e => e.IdHerr).HasColumnName("ID_HERR");

                entity.Property(e => e.Extracolumn1)
                    .IsUnicode(false)
                    .HasColumnName("EXTRACOLUMN1");

                entity.Property(e => e.Extracolumn2)
                    .IsUnicode(false)
                    .HasColumnName("EXTRACOLUMN2");

                entity.Property(e => e.Extracolumn3)
                    .IsUnicode(false)
                    .HasColumnName("EXTRACOLUMN3");

                entity.Property(e => e.Habilitado)
                    .HasColumnName("HABILITADO")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("NOMBRE");
            });

            modelBuilder.Entity<Nivel>(entity =>
            {
                entity.HasKey(e => e.IdNiv);

                entity.ToTable("NIVEL");

                entity.Property(e => e.IdNiv)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("ID_NIV");

                entity.Property(e => e.Extracolumn1)
                    .IsUnicode(false)
                    .HasColumnName("EXTRACOLUMN1");

                entity.Property(e => e.Extracolumn2)
                    .IsUnicode(false)
                    .HasColumnName("EXTRACOLUMN2");

                entity.Property(e => e.Extracolumn3)
                    .IsUnicode(false)
                    .HasColumnName("EXTRACOLUMN3");

                entity.Property(e => e.Habilitado)
                    .HasColumnName("HABILITADO")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("NOMBRE");
            });

            modelBuilder.Entity<Operacion>(entity =>
            {
                entity.HasKey(e => e.IdOpe)
                    .HasName("XPKOPERACION");

                entity.ToTable("OPERACION");

                entity.Property(e => e.IdOpe).HasColumnName("ID_OPE");

                entity.Property(e => e.Extracolumn1)
                    .IsUnicode(false)
                    .HasColumnName("EXTRACOLUMN1");

                entity.Property(e => e.Extracolumn2)
                    .IsUnicode(false)
                    .HasColumnName("EXTRACOLUMN2");

                entity.Property(e => e.Extracolumn3)
                    .IsRequired()
                    .IsUnicode(false)
                    .HasColumnName("EXTRACOLUMN3");

                entity.Property(e => e.Habilitado)
                    .HasColumnName("HABILITADO")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("NOMBRE");
            });

            modelBuilder.Entity<Pm>(entity =>
            {
                entity.HasKey(e => e.IdPm)
                    .HasName("XPKPM");

                entity.ToTable("PM");

                entity.Property(e => e.IdPm).HasColumnName("ID_PM");

                entity.Property(e => e.Descripcion)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("DESCRIPCION");

                entity.Property(e => e.Extracolumn1)
                    .IsUnicode(false)
                    .HasColumnName("EXTRACOLUMN1");

                entity.Property(e => e.Extracolumn2)
                    .IsUnicode(false)
                    .HasColumnName("EXTRACOLUMN2");

                entity.Property(e => e.Extracolumn3)
                    .IsUnicode(false)
                    .HasColumnName("EXTRACOLUMN3");

                entity.Property(e => e.Habilitado)
                    .HasColumnName("HABILITADO")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.IdFlt).HasColumnName("ID_FLT");

                entity.Property(e => e.IdPmcopy)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("ID_PMCOPY");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("NOMBRE");

                entity.HasOne(d => d.IdFltNavigation)
                    .WithMany(p => p.Pms)
                    .HasForeignKey(d => d.IdFlt)
                    .HasConstraintName("FK_PM_FLOTA");
            });

            modelBuilder.Entity<PmSistema>(entity =>
            {
                entity.HasKey(e => e.IdPms)
                    .HasName("XPKPM_SISTEMA");

                entity.ToTable("PM_SISTEMA");

                entity.Property(e => e.IdPms).HasColumnName("ID_PMS");

                entity.Property(e => e.Extracolumn1)
                    .IsUnicode(false)
                    .HasColumnName("EXTRACOLUMN1");

                entity.Property(e => e.Extracolumn2)
                    .IsUnicode(false)
                    .HasColumnName("EXTRACOLUMN2");

                entity.Property(e => e.Extracolumn3)
                    .IsUnicode(false)
                    .HasColumnName("EXTRACOLUMN3");

                entity.Property(e => e.Habilitado)
                    .HasColumnName("HABILITADO")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.IdDis).HasColumnName("ID_DIS");

                entity.Property(e => e.IdPm).HasColumnName("ID_PM");

                entity.Property(e => e.IdSis).HasColumnName("ID_SIS");

                entity.HasOne(d => d.IdDisNavigation)
                    .WithMany(p => p.PmSistemas)
                    .HasForeignKey(d => d.IdDis)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PM_SISTEMA_DISCIPLINA");

                entity.HasOne(d => d.IdPmNavigation)
                    .WithMany(p => p.PmSistemas)
                    .HasForeignKey(d => d.IdPm)
                    .HasConstraintName("FK_PM_SISTEMA_PM");

                entity.HasOne(d => d.IdSisNavigation)
                    .WithMany(p => p.PmSistemas)
                    .HasForeignKey(d => d.IdSis)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PM_SISTEMA_SISTEMA");
            });

            modelBuilder.Entity<PmsisActividad>(entity =>
            {
                entity.HasKey(e => new { e.IdAct, e.IdPms })
                    .HasName("XPKPMSIS_ACTIVIDAD");

                entity.ToTable("PMSIS_ACTIVIDAD");

                entity.Property(e => e.IdAct).HasColumnName("ID_ACT");

                entity.Property(e => e.IdPms).HasColumnName("ID_PMS");

                entity.Property(e => e.Extracolumn1)
                    .IsUnicode(false)
                    .HasColumnName("EXTRACOLUMN1");

                entity.Property(e => e.Extracolumn2)
                    .IsUnicode(false)
                    .HasColumnName("EXTRACOLUMN2");

                entity.Property(e => e.Extracolumn3)
                    .IsUnicode(false)
                    .HasColumnName("EXTRACOLUMN3");

                entity.Property(e => e.Habilitado)
                    .HasColumnName("HABILITADO")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Orden).HasColumnName("ORDEN");

                entity.HasOne(d => d.IdActNavigation)
                    .WithMany(p => p.PmsisActividads)
                    .HasForeignKey(d => d.IdAct)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PMSIS_ACTIVIDAD_ACTIVIDAD");

                entity.HasOne(d => d.IdPmsNavigation)
                    .WithMany(p => p.PmsisActividads)
                    .HasForeignKey(d => d.IdPms)
                    .HasConstraintName("FK_PMSIS_ACTIVIDAD_PM_SISTEMA");
            });

            modelBuilder.Entity<Rango>(entity =>
            {
                entity.HasKey(e => e.IdRan);

                entity.ToTable("RANGO");

                entity.Property(e => e.IdRan).HasColumnName("ID_RAN");

                entity.Property(e => e.Extracolumn1)
                    .IsUnicode(false)
                    .HasColumnName("EXTRACOLUMN1");

                entity.Property(e => e.Extracolumn2)
                    .IsUnicode(false)
                    .HasColumnName("EXTRACOLUMN2");

                entity.Property(e => e.Extracolumn3)
                    .IsUnicode(false)
                    .HasColumnName("EXTRACOLUMN3");

                entity.Property(e => e.Habilitado).HasColumnName("HABILITADO");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("NOMBRE");
            });

            modelBuilder.Entity<RepDetalle>(entity =>
            {
                entity.HasKey(e => new { e.IdAct, e.IdRepsis })
                    .HasName("XPKREP_DETALLE");

                entity.ToTable("REP_DETALLE");

                entity.Property(e => e.IdAct).HasColumnName("ID_ACT");

                entity.Property(e => e.IdRepsis).HasColumnName("ID_REPSIS");

                entity.Property(e => e.Especificacion)
                    .HasColumnType("text")
                    .HasColumnName("ESPECIFICACION");

                entity.Property(e => e.Extracolumn1)
                    .IsUnicode(false)
                    .HasColumnName("EXTRACOLUMN1");

                entity.Property(e => e.Extracolumn2)
                    .IsUnicode(false)
                    .HasColumnName("EXTRACOLUMN2");

                entity.Property(e => e.Extracolumn3)
                    .IsUnicode(false)
                    .HasColumnName("EXTRACOLUMN3");

                entity.Property(e => e.Habilitado)
                    .HasColumnName("HABILITADO")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.IdEst)
                    .HasColumnName("ID_EST")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Observacion)
                    .HasColumnType("text")
                    .HasColumnName("OBSERVACION");

                entity.Property(e => e.Orden).HasColumnName("ORDEN");

                entity.Property(e => e.Referencia)
                    .HasColumnType("text")
                    .HasColumnName("REFERENCIA");

                entity.Property(e => e.ReferenciaUrl)
                    .HasColumnType("text")
                    .HasColumnName("REFERENCIA_URL");

                entity.Property(e => e.Titulo)
                    .HasMaxLength(800)
                    .IsUnicode(false)
                    .HasColumnName("TITULO");

                entity.Property(e => e.Valor).HasColumnName("VALOR");

                entity.HasOne(d => d.IdEstNavigation)
                    .WithMany(p => p.RepDetalles)
                    .HasForeignKey(d => d.IdEst)
                    .HasConstraintName("FK_REP_DETALLE_ESTADO");

                entity.HasOne(d => d.IdRepsisNavigation)
                    .WithMany(p => p.RepDetalles)
                    .HasForeignKey(d => d.IdRepsis)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_REP_DETALLE_REP_SISTEMA");
            });

            modelBuilder.Entity<RepEntrega>(entity =>
            {
                entity.HasKey(e => e.IdRent)
                    .HasName("XPKREP_ENTREGA");

                entity.ToTable("REP_ENTREGA");

                entity.Property(e => e.IdRent).HasColumnName("ID_RENT");

                entity.Property(e => e.Extracolumn1)
                    .IsUnicode(false)
                    .HasColumnName("EXTRACOLUMN1");

                entity.Property(e => e.Extracolumn2)
                    .IsUnicode(false)
                    .HasColumnName("EXTRACOLUMN2");

                entity.Property(e => e.Extracolumn3)
                    .IsUnicode(false)
                    .HasColumnName("EXTRACOLUMN3");

                entity.Property(e => e.Habilitado)
                    .HasColumnName("HABILITADO")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.IdEnt).HasColumnName("ID_ENT");

                entity.Property(e => e.IdRep).HasColumnName("ID_REP");

                entity.Property(e => e.Resultado)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("RESULTADO");

                entity.HasOne(d => d.IdEntNavigation)
                    .WithMany(p => p.RepEntregas)
                    .HasForeignKey(d => d.IdEnt)
                    .HasConstraintName("FK_REP_ENTREGA_ENTREGA");

                entity.HasOne(d => d.IdRepNavigation)
                    .WithMany(p => p.RepEntregas)
                    .HasForeignKey(d => d.IdRep)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_REP_ENTREGA_REPORTE");
            });

            modelBuilder.Entity<RepLider>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("REP_LIDER");

                entity.Property(e => e.IdRep).HasColumnName("ID_REP");

                entity.Property(e => e.IdTur).HasColumnName("ID_TUR");

                entity.Property(e => e.IdUser).HasColumnName("ID_USER");

                entity.HasOne(d => d.IdRepNavigation)
                    .WithMany()
                    .HasForeignKey(d => d.IdRep)
                    .HasConstraintName("FK_REP_LIDER_REPORTE");

                entity.HasOne(d => d.IdTurNavigation)
                    .WithMany()
                    .HasForeignKey(d => d.IdTur)
                    .HasConstraintName("FK_REP_LIDER_TURNO");

                entity.HasOne(d => d.IdUserNavigation)
                    .WithMany()
                    .HasForeignKey(d => d.IdUser)
                    .HasConstraintName("FK_REP_LIDER_USUARIO");
            });

            modelBuilder.Entity<RepSistema>(entity =>
            {
                entity.HasKey(e => e.IdRepsis);

                entity.ToTable("REP_SISTEMA");

                entity.Property(e => e.IdRepsis).HasColumnName("ID_REPSIS");

                entity.Property(e => e.Avance)
                    .HasColumnName("AVANCE")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Extracolumn1)
                    .IsUnicode(false)
                    .HasColumnName("EXTRACOLUMN1");

                entity.Property(e => e.Extracolumn2)
                    .IsUnicode(false)
                    .HasColumnName("EXTRACOLUMN2");

                entity.Property(e => e.Extracolumn3)
                    .IsUnicode(false)
                    .HasColumnName("EXTRACOLUMN3");

                entity.Property(e => e.Habilitado)
                    .HasColumnName("HABILITADO")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.IdCod)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("ID_COD");

                entity.Property(e => e.IdRep).HasColumnName("ID_REP");

                entity.Property(e => e.NomDisciplina)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("NOM_DISCIPLINA");

                entity.Property(e => e.NomSector)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("NOM_SECTOR");

                entity.Property(e => e.NomSistema)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("NOM_SISTEMA");

                entity.HasOne(d => d.IdRepNavigation)
                    .WithMany(p => p.RepSistemas)
                    .HasForeignKey(d => d.IdRep)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_REP_SISTEMA_REPORTE");
            });

            modelBuilder.Entity<RepTecnico>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("REP_TECNICO");

                entity.Property(e => e.IdRep).HasColumnName("ID_REP");

                entity.Property(e => e.IdTur).HasColumnName("ID_TUR");

                entity.Property(e => e.IdUsr).HasColumnName("ID_USR");

                entity.HasOne(d => d.IdRepNavigation)
                    .WithMany()
                    .HasForeignKey(d => d.IdRep)
                    .HasConstraintName("FK_REP_TECNICO_REPORTE");

                entity.HasOne(d => d.IdTurNavigation)
                    .WithMany()
                    .HasForeignKey(d => d.IdTur)
                    .HasConstraintName("FK_REP_TECNICO_TURNO");

                entity.HasOne(d => d.IdUsrNavigation)
                    .WithMany()
                    .HasForeignKey(d => d.IdUsr)
                    .HasConstraintName("FK_REP_TECNICO_USUARIO");
            });

            modelBuilder.Entity<Reporte>(entity =>
            {
                entity.HasKey(e => e.IdRep)
                    .HasName("XPKREPORTE");

                entity.ToTable("REPORTE");

                entity.Property(e => e.IdRep).HasColumnName("ID_REP");

                entity.Property(e => e.Avance)
                    .HasColumnName("AVANCE")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Comentario)
                    .HasColumnType("text")
                    .HasColumnName("COMENTARIO");

                entity.Property(e => e.Creado)
                    .HasColumnName("CREADO")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Extracolumn1)
                    .IsUnicode(false)
                    .HasColumnName("EXTRACOLUMN1");

                entity.Property(e => e.Extracolumn2)
                    .IsUnicode(false)
                    .HasColumnName("EXTRACOLUMN2");

                entity.Property(e => e.Extracolumn3)
                    .IsUnicode(false)
                    .HasColumnName("EXTRACOLUMN3");

                entity.Property(e => e.Fecha)
                    .HasColumnType("datetime")
                    .HasColumnName("FECHA");

                entity.Property(e => e.Habilitado)
                    .HasColumnName("HABILITADO")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Horometro).HasColumnName("HOROMETRO");

                entity.Property(e => e.IdPm).HasColumnName("ID_PM");

                entity.Property(e => e.IdUni).HasColumnName("ID_UNI");

                entity.HasOne(d => d.IdPmNavigation)
                    .WithMany(p => p.Reportes)
                    .HasForeignKey(d => d.IdPm)
                    .HasConstraintName("FK_REPORTE_PM");

                entity.HasOne(d => d.IdUniNavigation)
                    .WithMany(p => p.Reportes)
                    .HasForeignKey(d => d.IdUni)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_REPORTE_UNIDAD");
            });

            modelBuilder.Entity<Repuesto>(entity =>
            {
                entity.HasKey(e => e.IdRep)
                    .HasName("XPKREPUESTO");

                entity.ToTable("REPUESTO");

                entity.Property(e => e.IdRep).HasColumnName("ID_REP");

                entity.Property(e => e.Extracolumn1)
                    .IsUnicode(false)
                    .HasColumnName("EXTRACOLUMN1");

                entity.Property(e => e.Extracolumn2)
                    .IsUnicode(false)
                    .HasColumnName("EXTRACOLUMN2");

                entity.Property(e => e.Extracolumn3)
                    .IsUnicode(false)
                    .HasColumnName("EXTRACOLUMN3");

                entity.Property(e => e.Habilitado)
                    .HasColumnName("HABILITADO")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("NOMBRE");

                entity.Property(e => e.Nroparte)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("NROPARTE");
            });

            modelBuilder.Entity<Rol>(entity =>
            {
                entity.HasKey(e => e.IdRol)
                    .HasName("XPKROL");

                entity.ToTable("ROL");

                entity.Property(e => e.IdRol).HasColumnName("ID_ROL");

                entity.Property(e => e.Administrador).HasColumnName("ADMINISTRADOR");

                entity.Property(e => e.Extracolumn1)
                    .IsUnicode(false)
                    .HasColumnName("EXTRACOLUMN1");

                entity.Property(e => e.Extracolumn2)
                    .IsUnicode(false)
                    .HasColumnName("EXTRACOLUMN2");

                entity.Property(e => e.Extracolumn3)
                    .IsUnicode(false)
                    .HasColumnName("EXTRACOLUMN3");

                entity.Property(e => e.Habilitado)
                    .HasColumnName("HABILITADO")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("NOMBRE");
            });

            modelBuilder.Entity<Sector>(entity =>
            {
                entity.HasKey(e => e.IdSec)
                    .HasName("XPKSECTOR");

                entity.ToTable("SECTOR");

                entity.Property(e => e.IdSec).HasColumnName("ID_SEC");

                entity.Property(e => e.Extracolumn1)
                    .IsUnicode(false)
                    .HasColumnName("EXTRACOLUMN1");

                entity.Property(e => e.Extracolumn2)
                    .IsUnicode(false)
                    .HasColumnName("EXTRACOLUMN2");

                entity.Property(e => e.Extracolumn3)
                    .IsUnicode(false)
                    .HasColumnName("EXTRACOLUMN3");

                entity.Property(e => e.Habilitado)
                    .HasColumnName("HABILITADO")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("NOMBRE");
            });

            modelBuilder.Entity<Sistema>(entity =>
            {
                entity.HasKey(e => e.IdSis)
                    .HasName("XPKSISTEMA");

                entity.ToTable("SISTEMA");

                entity.Property(e => e.IdSis).HasColumnName("ID_SIS");

                entity.Property(e => e.Extracolumn1)
                    .IsUnicode(false)
                    .HasColumnName("EXTRACOLUMN1");

                entity.Property(e => e.Extracolumn2)
                    .IsUnicode(false)
                    .HasColumnName("EXTRACOLUMN2");

                entity.Property(e => e.Extracolumn3)
                    .IsUnicode(false)
                    .HasColumnName("EXTRACOLUMN3");

                entity.Property(e => e.Habilitado)
                    .HasColumnName("HABILITADO")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.IdCod)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("ID_COD");

                entity.Property(e => e.IdSec).HasColumnName("ID_SEC");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("NOMBRE");

                entity.HasOne(d => d.IdCodNavigation)
                    .WithMany(p => p.Sistemas)
                    .HasForeignKey(d => d.IdCod)
                    .HasConstraintName("FK_SISTEMA_CONDICION");

                entity.HasOne(d => d.IdSecNavigation)
                    .WithMany(p => p.Sistemas)
                    .HasForeignKey(d => d.IdSec)
                    .HasConstraintName("FK_SISTEMA_SECTOR");
            });

            modelBuilder.Entity<Tab>(entity =>
            {
                entity.ToTable("TABS");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Habilitado)
                    .HasColumnName("HABILITADO")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("NAME");

                entity.Property(e => e.Page)
                    .IsRequired()
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("PAGE");
            });

            modelBuilder.Entity<Tabla>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("TABLA");

                entity.Property(e => e.Extracolumn1)
                    .IsUnicode(false)
                    .HasColumnName("EXTRACOLUMN1");

                entity.Property(e => e.Extracolumn2)
                    .IsUnicode(false)
                    .HasColumnName("EXTRACOLUMN2");

                entity.Property(e => e.Extracolumn3)
                    .IsRequired()
                    .IsUnicode(false)
                    .HasColumnName("EXTRACOLUMN3");

                entity.Property(e => e.Habilitado)
                    .HasColumnName("HABILITADO")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.IdTab)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("ID_TAB");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("NOMBRE");

                entity.HasOne(d => d.IdTabNavigation)
                    .WithMany()
                    .HasForeignKey(d => d.IdTab)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TABLA_AUDITORIA");
            });

            modelBuilder.Entity<Tarea>(entity =>
            {
                entity.HasKey(e => e.IdTar)
                    .HasName("XPKTAREA");

                entity.ToTable("TAREA");

                entity.Property(e => e.IdTar).HasColumnName("ID_TAR");

                entity.Property(e => e.Extracolumn1)
                    .IsUnicode(false)
                    .HasColumnName("EXTRACOLUMN1");

                entity.Property(e => e.Extracolumn2)
                    .IsUnicode(false)
                    .HasColumnName("EXTRACOLUMN2");

                entity.Property(e => e.Extracolumn3)
                    .IsUnicode(false)
                    .HasColumnName("EXTRACOLUMN3");

                entity.Property(e => e.Habilitado)
                    .HasColumnName("HABILITADO")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.IdAct).HasColumnName("ID_ACT");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(800)
                    .IsUnicode(false)
                    .HasColumnName("NOMBRE");

                entity.HasOne(d => d.IdActNavigation)
                    .WithMany(p => p.Tareas)
                    .HasForeignKey(d => d.IdAct)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TAREA_ACTIVIDAD");
            });

            modelBuilder.Entity<TipoEquipo>(entity =>
            {
                entity.HasKey(e => e.IdTeq)
                    .HasName("XPKTIPO_EQUIPO");

                entity.ToTable("TIPO_EQUIPO");

                entity.Property(e => e.IdTeq).HasColumnName("ID_TEQ");

                entity.Property(e => e.Extracolumn1)
                    .IsUnicode(false)
                    .HasColumnName("EXTRACOLUMN1");

                entity.Property(e => e.Extracolumn2)
                    .IsUnicode(false)
                    .HasColumnName("EXTRACOLUMN2");

                entity.Property(e => e.Extracolumn3)
                    .IsUnicode(false)
                    .HasColumnName("EXTRACOLUMN3");

                entity.Property(e => e.Habilitado)
                    .HasColumnName("HABILITADO")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.IdEmp).HasColumnName("ID_EMP");

                entity.Property(e => e.TEquipo)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("T_EQUIPO");

                entity.HasOne(d => d.IdEmpNavigation)
                    .WithMany(p => p.TipoEquipos)
                    .HasForeignKey(d => d.IdEmp)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TIPO_EQUIPO_EMPRESA");
            });

            modelBuilder.Entity<Turno>(entity =>
            {
                entity.HasKey(e => e.IdTur);

                entity.ToTable("TURNO");

                entity.Property(e => e.IdTur).HasColumnName("ID_TUR");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("NOMBRE");
            });

            modelBuilder.Entity<Unidad>(entity =>
            {
                entity.HasKey(e => e.IdUni)
                    .HasName("XPKUNIDAD");

                entity.ToTable("UNIDAD");

                entity.Property(e => e.IdUni).HasColumnName("ID_UNI");

                entity.Property(e => e.Extracolumn1)
                    .IsUnicode(false)
                    .HasColumnName("EXTRACOLUMN1");

                entity.Property(e => e.Extracolumn2)
                    .IsUnicode(false)
                    .HasColumnName("EXTRACOLUMN2");

                entity.Property(e => e.Extracolumn3)
                    .IsUnicode(false)
                    .HasColumnName("EXTRACOLUMN3");

                entity.Property(e => e.Habilitado)
                    .HasColumnName("HABILITADO")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.IdFlt).HasColumnName("ID_FLT");

                entity.Property(e => e.Unidad1)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("UNIDAD");

                entity.HasOne(d => d.IdFltNavigation)
                    .WithMany(p => p.Unidads)
                    .HasForeignKey(d => d.IdFlt)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UNIDAD_FLOTA");
            });

            modelBuilder.Entity<UnidadMedidum>(entity =>
            {
                entity.HasKey(e => e.IdUm)
                    .HasName("XPKUNIDAD_MEDIDA");

                entity.ToTable("UNIDAD_MEDIDA");

                entity.Property(e => e.IdUm)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("ID_UM");

                entity.Property(e => e.Extracolumn1)
                    .IsUnicode(false)
                    .HasColumnName("EXTRACOLUMN1");

                entity.Property(e => e.Extracolumn2)
                    .IsUnicode(false)
                    .HasColumnName("EXTRACOLUMN2");

                entity.Property(e => e.Extracolumn3)
                    .IsUnicode(false)
                    .HasColumnName("EXTRACOLUMN3");

                entity.Property(e => e.Habilitado)
                    .HasColumnName("HABILITADO")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("NOMBRE");
            });

            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.HasKey(e => e.IdUsr)
                    .HasName("XPKUSUARIO");

                entity.ToTable("USUARIO");

                entity.Property(e => e.IdUsr).HasColumnName("ID_USR");

                entity.Property(e => e.Contrasena)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("CONTRASENA");

                entity.Property(e => e.Extracolumn1)
                    .IsUnicode(false)
                    .HasColumnName("EXTRACOLUMN1");

                entity.Property(e => e.Extracolumn2)
                    .IsUnicode(false)
                    .HasColumnName("EXTRACOLUMN2");

                entity.Property(e => e.Extracolumn3)
                    .IsUnicode(false)
                    .HasColumnName("EXTRACOLUMN3");

                entity.Property(e => e.Habilitado)
                    .HasColumnName("HABILITADO")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.IdDis).HasColumnName("ID_DIS");

                entity.Property(e => e.IdRol).HasColumnName("ID_ROL");

                entity.Property(e => e.Lider)
                    .HasColumnName("LIDER")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("NOMBRE");

                entity.Property(e => e.Usuario1)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("USUARIO");

                entity.HasOne(d => d.IdDisNavigation)
                    .WithMany(p => p.Usuarios)
                    .HasForeignKey(d => d.IdDis)
                    .HasConstraintName("FK_USUARIO_DISCIPLINA");

                entity.HasOne(d => d.IdRolNavigation)
                    .WithMany(p => p.Usuarios)
                    .HasForeignKey(d => d.IdRol)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_USUARIO_ROL");
            });

            modelBuilder.Entity<VwPmSistema>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("VW_PM_Sistema");

                entity.Property(e => e.Condicion)
                    .HasMaxLength(60)
                    .IsUnicode(false)
                    .HasColumnName("CONDICION");

                entity.Property(e => e.Disciplina)
                    .HasMaxLength(60)
                    .IsUnicode(false)
                    .HasColumnName("DISCIPLINA");

                entity.Property(e => e.IdCod)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("ID_COD")
                    .IsFixedLength();

                entity.Property(e => e.IdDis).HasColumnName("ID_DIS");

                entity.Property(e => e.IdPm)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("ID_PM")
                    .IsFixedLength();

                entity.Property(e => e.IdPms).HasColumnName("ID_PMS");

                entity.Property(e => e.IdSec).HasColumnName("ID_SEC");

                entity.Property(e => e.IdSis).HasColumnName("ID_SIS");

                entity.Property(e => e.Pm)
                    .HasMaxLength(60)
                    .IsUnicode(false)
                    .HasColumnName("PM");

                entity.Property(e => e.Sector)
                    .HasMaxLength(60)
                    .IsUnicode(false)
                    .HasColumnName("SECTOR");

                entity.Property(e => e.Sistema)
                    .HasMaxLength(60)
                    .IsUnicode(false)
                    .HasColumnName("SISTEMA");
            });

            modelBuilder.Entity<VwPmsisActividad>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("VW_PMSIS_Actividad");

                entity.Property(e => e.Especificacion)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("ESPECIFICACION");

                entity.Property(e => e.Fin).HasColumnName("FIN");

                entity.Property(e => e.Habilitado).HasColumnName("HABILITADO");

                entity.Property(e => e.IdAct).HasColumnName("ID_ACT");

                entity.Property(e => e.IdClm)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("ID_CLM");

                entity.Property(e => e.IdCon)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("ID_CON");

                entity.Property(e => e.IdFrc).HasColumnName("ID_FRC");

                entity.Property(e => e.IdPms).HasColumnName("ID_PMS");

                entity.Property(e => e.IdRan).HasColumnName("ID_RAN");

                entity.Property(e => e.IdUm)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("ID_UM");

                entity.Property(e => e.Inicio).HasColumnName("INICIO");

                entity.Property(e => e.Orden).HasColumnName("ORDEN");

                entity.Property(e => e.Referencia)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("REFERENCIA");

                entity.Property(e => e.ReferenciaUrl)
                    .HasColumnType("text")
                    .HasColumnName("REFERENCIA_URL");

                entity.Property(e => e.Titulo)
                    .HasMaxLength(800)
                    .IsUnicode(false)
                    .HasColumnName("TITULO");
            });

            modelBuilder.Entity<VwSistema>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("VW_Sistema");

                entity.Property(e => e.Condicion)
                    .HasMaxLength(60)
                    .IsUnicode(false)
                    .HasColumnName("CONDICION");

                entity.Property(e => e.IdCod)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("ID_COD")
                    .IsFixedLength();

                entity.Property(e => e.IdSec).HasColumnName("ID_SEC");

                entity.Property(e => e.IdSis).HasColumnName("ID_SIS");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(60)
                    .IsUnicode(false)
                    .HasColumnName("NOMBRE");

                entity.Property(e => e.Sector)
                    .HasMaxLength(60)
                    .IsUnicode(false)
                    .HasColumnName("SECTOR");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
