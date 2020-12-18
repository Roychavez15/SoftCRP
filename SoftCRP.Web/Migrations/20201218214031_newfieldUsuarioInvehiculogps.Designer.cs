﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SoftCRP.Web.Data;

namespace SoftCRP.Web.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20201218214031_newfieldUsuarioInvehiculogps")]
    partial class newfieldUsuarioInvehiculogps
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.14-servicing-32113")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Name")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("RoleId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider");

                    b.Property<string>("ProviderKey");

                    b.Property<string>("ProviderDisplayName");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("LoginProvider");

                    b.Property<string>("Name");

                    b.Property<string>("Value");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("SoftCRP.Web.Data.Entities.Analisis", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Cedula")
                        .IsRequired()
                        .HasMaxLength(20);

                    b.Property<DateTime>("Fecha");

                    b.Property<string>("Observaciones");

                    b.Property<string>("Placa")
                        .IsRequired()
                        .HasMaxLength(10);

                    b.Property<int>("tipoAnalisisId");

                    b.Property<string>("userId");

                    b.HasKey("Id");

                    b.HasIndex("tipoAnalisisId");

                    b.HasIndex("userId");

                    b.ToTable("Analises");
                });

            modelBuilder.Entity("SoftCRP.Web.Data.Entities.ArchivoAnalisis", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ArchivoPath");

                    b.Property<DateTime>("Fecha");

                    b.Property<string>("TipoArchivo");

                    b.Property<int?>("analisisId");

                    b.Property<long>("tamanio");

                    b.Property<string>("userId");

                    b.HasKey("Id");

                    b.HasIndex("analisisId");

                    b.HasIndex("userId");

                    b.ToTable("ArchivosAnalisis");
                });

            modelBuilder.Entity("SoftCRP.Web.Data.Entities.ArchivoCapacitaciones", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ArchivoPath");

                    b.Property<DateTime>("Fecha");

                    b.Property<string>("TipoArchivo");

                    b.Property<int?>("capacitacionId");

                    b.Property<long>("tamanio");

                    b.Property<string>("userId");

                    b.HasKey("Id");

                    b.HasIndex("capacitacionId");

                    b.HasIndex("userId");

                    b.ToTable("archivoCapacitaciones");
                });

            modelBuilder.Entity("SoftCRP.Web.Data.Entities.ArchivoNovedades", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ArchivoPath");

                    b.Property<DateTime>("Fecha");

                    b.Property<string>("TipoArchivo");

                    b.Property<int?>("novedadId");

                    b.Property<long>("tamanio");

                    b.Property<string>("userId");

                    b.HasKey("Id");

                    b.HasIndex("novedadId");

                    b.HasIndex("userId");

                    b.ToTable("archivoNovedades");
                });

            modelBuilder.Entity("SoftCRP.Web.Data.Entities.ArchivoTramites", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ArchivoPath");

                    b.Property<DateTime>("Fecha");

                    b.Property<string>("TipoArchivo");

                    b.Property<long>("tamanio");

                    b.Property<int?>("tramiteId");

                    b.Property<string>("userId");

                    b.HasKey("Id");

                    b.HasIndex("tramiteId");

                    b.HasIndex("userId");

                    b.ToTable("archivoTramites");
                });

            modelBuilder.Entity("SoftCRP.Web.Data.Entities.Capacitacion", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("Fecha");

                    b.Property<string>("Link");

                    b.Property<string>("Test");

                    b.Property<int>("tipoCapacitacionId");

                    b.Property<string>("userId");

                    b.HasKey("Id");

                    b.HasIndex("tipoCapacitacionId");

                    b.HasIndex("userId");

                    b.ToTable("capacitaciones");
                });

            modelBuilder.Entity("SoftCRP.Web.Data.Entities.Cliente", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Apellido")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<string>("Cedula")
                        .IsRequired()
                        .HasMaxLength(10);

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.HasKey("Id");

                    b.ToTable("Clientes");
                });

            modelBuilder.Entity("SoftCRP.Web.Data.Entities.Gama", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("GamaSustituto");

                    b.Property<int>("Monto");

                    b.HasKey("Id");

                    b.ToTable("gamas");
                });

            modelBuilder.Entity("SoftCRP.Web.Data.Entities.Incidencia", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<decimal>("AceleracionesBruscas")
                        .HasColumnType("decimal(18,1)");

                    b.Property<decimal>("ExcesoVelocidad")
                        .HasColumnType("decimal(18,1)");

                    b.Property<decimal>("FrenazoBrusco")
                        .HasColumnType("decimal(18,1)");

                    b.Property<decimal>("GiroBrusco")
                        .HasColumnType("decimal(18,1)");

                    b.Property<string>("UserId");

                    b.Property<bool>("isActive");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("incidencias");
                });

            modelBuilder.Entity("SoftCRP.Web.Data.Entities.Log", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("Fecha");

                    b.Property<string>("IP");

                    b.Property<string>("Level");

                    b.Property<string>("Message");

                    b.Property<string>("Module");

                    b.Property<string>("userId");

                    b.HasKey("Id");

                    b.HasIndex("userId");

                    b.ToTable("Logs");
                });

            modelBuilder.Entity("SoftCRP.Web.Data.Entities.LogNovedad", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Estado");

                    b.Property<DateTime>("Fecha");

                    b.Property<string>("Observaciones");

                    b.Property<string>("UsuarioId");

                    b.Property<int?>("novedadId");

                    b.HasKey("Id");

                    b.HasIndex("UsuarioId");

                    b.HasIndex("novedadId");

                    b.ToTable("logNovedades");
                });

            modelBuilder.Entity("SoftCRP.Web.Data.Entities.Novedad", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Cedula")
                        .IsRequired()
                        .HasMaxLength(20);

                    b.Property<string>("EstadoSolucion");

                    b.Property<DateTime>("Fecha");

                    b.Property<DateTime?>("FechaSolucion");

                    b.Property<string>("Motivo")
                        .IsRequired();

                    b.Property<string>("Observaciones");

                    b.Property<string>("Placa")
                        .IsRequired()
                        .HasMaxLength(10);

                    b.Property<string>("Solucion");

                    b.Property<string>("SubMotivo")
                        .IsRequired();

                    b.Property<string>("ViaIngreso")
                        .IsRequired();

                    b.Property<string>("userId");

                    b.Property<string>("userSolucionId");

                    b.HasKey("Id");

                    b.HasIndex("userId");

                    b.HasIndex("userSolucionId");

                    b.ToTable("novedades");
                });

            modelBuilder.Entity("SoftCRP.Web.Data.Entities.TipoAnalisis", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Tipo")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<bool>("isActive");

                    b.HasKey("Id");

                    b.ToTable("TiposAnalisis");
                });

            modelBuilder.Entity("SoftCRP.Web.Data.Entities.TipoCapacitacion", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Tipo")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.Property<bool>("isActive");

                    b.HasKey("Id");

                    b.ToTable("tipoCapacitaciones");
                });

            modelBuilder.Entity("SoftCRP.Web.Data.Entities.TipoNovedades", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Tipo")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<bool>("isActive");

                    b.HasKey("Id");

                    b.ToTable("tipoNovedades");
                });

            modelBuilder.Entity("SoftCRP.Web.Data.Entities.TipoTramite", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Tipo")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<bool>("isActive");

                    b.HasKey("Id");

                    b.ToTable("tipoTramites");
                });

            modelBuilder.Entity("SoftCRP.Web.Data.Entities.Tramite", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Anio")
                        .IsRequired();

                    b.Property<string>("Cedula")
                        .IsRequired()
                        .HasMaxLength(20);

                    b.Property<DateTime>("Fecha");

                    b.Property<string>("Mes")
                        .IsRequired();

                    b.Property<string>("Observaciones");

                    b.Property<string>("Placa")
                        .IsRequired()
                        .HasMaxLength(10);

                    b.Property<int>("tipoTramiteId");

                    b.Property<string>("userId");

                    b.HasKey("Id");

                    b.HasIndex("tipoTramiteId");

                    b.HasIndex("userId");

                    b.ToTable("tramites");
                });

            modelBuilder.Entity("SoftCRP.Web.Data.Entities.User", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccessFailedCount");

                    b.Property<string>("Address")
                        .HasMaxLength(100);

                    b.Property<string>("Cedula")
                        .IsRequired()
                        .HasMaxLength(20);

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Email")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(200);

                    b.Property<string>("ImageUrl");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(200);

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256);

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<string>("SecurityStamp");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName")
                        .HasMaxLength(256);

                    b.Property<bool>("isActive");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("SoftCRP.Web.Data.Entities.Vehiculo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Placa");

                    b.Property<string>("gps_id");

                    b.Property<string>("gps_provider");

                    b.Property<string>("userId");

                    b.HasKey("Id");

                    b.HasIndex("userId");

                    b.ToTable("vehiculos");
                });

            modelBuilder.Entity("SoftCRP.Web.Data.Entities.VehiculoGps", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("ahorro");

                    b.Property<int>("anio");

                    b.Property<int>("conductores");

                    b.Property<int>("dia");

                    b.Property<int>("hardbraking");

                    b.Property<decimal>("kilometerstraveled")
                        .HasColumnType("decimal(18,4)");

                    b.Property<string>("latitude");

                    b.Property<string>("longitude");

                    b.Property<int>("mes");

                    b.Property<decimal>("score");

                    b.Property<int>("sharpacceleration");

                    b.Property<int>("sharpturn");

                    b.Property<int>("siniestros");

                    b.Property<int>("speeding");

                    b.Property<int>("talleres");

                    b.Property<int>("trips");

                    b.Property<string>("usuario");

                    b.Property<int?>("vehiculoId");

                    b.HasKey("Id");

                    b.HasIndex("vehiculoId");

                    b.ToTable("vehiculosGps");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("SoftCRP.Web.Data.Entities.User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("SoftCRP.Web.Data.Entities.User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("SoftCRP.Web.Data.Entities.User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("SoftCRP.Web.Data.Entities.User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("SoftCRP.Web.Data.Entities.Analisis", b =>
                {
                    b.HasOne("SoftCRP.Web.Data.Entities.TipoAnalisis", "tipoAnalisis")
                        .WithMany()
                        .HasForeignKey("tipoAnalisisId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("SoftCRP.Web.Data.Entities.User", "user")
                        .WithMany()
                        .HasForeignKey("userId");
                });

            modelBuilder.Entity("SoftCRP.Web.Data.Entities.ArchivoAnalisis", b =>
                {
                    b.HasOne("SoftCRP.Web.Data.Entities.Analisis", "analisis")
                        .WithMany("ArchivosAnalisis")
                        .HasForeignKey("analisisId");

                    b.HasOne("SoftCRP.Web.Data.Entities.User", "user")
                        .WithMany()
                        .HasForeignKey("userId");
                });

            modelBuilder.Entity("SoftCRP.Web.Data.Entities.ArchivoCapacitaciones", b =>
                {
                    b.HasOne("SoftCRP.Web.Data.Entities.Capacitacion", "capacitacion")
                        .WithMany("archivoCapacitaciones")
                        .HasForeignKey("capacitacionId");

                    b.HasOne("SoftCRP.Web.Data.Entities.User", "user")
                        .WithMany()
                        .HasForeignKey("userId");
                });

            modelBuilder.Entity("SoftCRP.Web.Data.Entities.ArchivoNovedades", b =>
                {
                    b.HasOne("SoftCRP.Web.Data.Entities.Novedad", "novedad")
                        .WithMany("archivoNovedades")
                        .HasForeignKey("novedadId");

                    b.HasOne("SoftCRP.Web.Data.Entities.User", "user")
                        .WithMany()
                        .HasForeignKey("userId");
                });

            modelBuilder.Entity("SoftCRP.Web.Data.Entities.ArchivoTramites", b =>
                {
                    b.HasOne("SoftCRP.Web.Data.Entities.Tramite", "tramite")
                        .WithMany("archivoTramites")
                        .HasForeignKey("tramiteId");

                    b.HasOne("SoftCRP.Web.Data.Entities.User", "user")
                        .WithMany()
                        .HasForeignKey("userId");
                });

            modelBuilder.Entity("SoftCRP.Web.Data.Entities.Capacitacion", b =>
                {
                    b.HasOne("SoftCRP.Web.Data.Entities.TipoCapacitacion", "tipoCapacitacion")
                        .WithMany()
                        .HasForeignKey("tipoCapacitacionId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("SoftCRP.Web.Data.Entities.User", "user")
                        .WithMany()
                        .HasForeignKey("userId");
                });

            modelBuilder.Entity("SoftCRP.Web.Data.Entities.Incidencia", b =>
                {
                    b.HasOne("SoftCRP.Web.Data.Entities.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("SoftCRP.Web.Data.Entities.Log", b =>
                {
                    b.HasOne("SoftCRP.Web.Data.Entities.User", "user")
                        .WithMany()
                        .HasForeignKey("userId");
                });

            modelBuilder.Entity("SoftCRP.Web.Data.Entities.LogNovedad", b =>
                {
                    b.HasOne("SoftCRP.Web.Data.Entities.User", "Usuario")
                        .WithMany()
                        .HasForeignKey("UsuarioId");

                    b.HasOne("SoftCRP.Web.Data.Entities.Novedad", "novedad")
                        .WithMany("logNovedades")
                        .HasForeignKey("novedadId");
                });

            modelBuilder.Entity("SoftCRP.Web.Data.Entities.Novedad", b =>
                {
                    b.HasOne("SoftCRP.Web.Data.Entities.User", "user")
                        .WithMany()
                        .HasForeignKey("userId");

                    b.HasOne("SoftCRP.Web.Data.Entities.User", "userSolucion")
                        .WithMany()
                        .HasForeignKey("userSolucionId");
                });

            modelBuilder.Entity("SoftCRP.Web.Data.Entities.Tramite", b =>
                {
                    b.HasOne("SoftCRP.Web.Data.Entities.TipoTramite", "tipoTramite")
                        .WithMany()
                        .HasForeignKey("tipoTramiteId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("SoftCRP.Web.Data.Entities.User", "user")
                        .WithMany()
                        .HasForeignKey("userId");
                });

            modelBuilder.Entity("SoftCRP.Web.Data.Entities.Vehiculo", b =>
                {
                    b.HasOne("SoftCRP.Web.Data.Entities.User", "user")
                        .WithMany()
                        .HasForeignKey("userId");
                });

            modelBuilder.Entity("SoftCRP.Web.Data.Entities.VehiculoGps", b =>
                {
                    b.HasOne("SoftCRP.Web.Data.Entities.Vehiculo", "vehiculo")
                        .WithMany()
                        .HasForeignKey("vehiculoId");
                });
#pragma warning restore 612, 618
        }
    }
}
