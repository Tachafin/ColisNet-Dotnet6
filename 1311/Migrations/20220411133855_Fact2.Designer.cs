﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using _1311.Models;

namespace _1311.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20220411133855_Fact2")]
    partial class Fact2
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.12")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RoleId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("_1311.Models.AppUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("Adresse")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("BoutiqueId")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("FirstName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("BoutiqueId")
                        .IsUnique()
                        .HasFilter("[BoutiqueId] IS NOT NULL");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("_1311.Models.Bank", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Rib")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Bank");
                });

            modelBuilder.Entity("_1311.Models.BonsLivraison", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("Date_creation")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("id");

                    b.HasIndex("UserId");

                    b.ToTable("BonsLivraison");
                });

            modelBuilder.Entity("_1311.Models.Boutique", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("BankeId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RegistreCommerce")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("VilleId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("BankeId")
                        .IsUnique()
                        .HasFilter("[BankeId] IS NOT NULL");

                    b.HasIndex("VilleId");

                    b.ToTable("Boutique");
                });

            modelBuilder.Entity("_1311.Models.Client", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("NomComplet")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Telephone")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("VilleId")
                        .HasColumnType("int");

                    b.Property<string>("adresse")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("VilleId");

                    b.ToTable("Client");
                });

            modelBuilder.Entity("_1311.Models.Colis", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("BonsLivraisonId")
                        .HasColumnType("int");

                    b.Property<int>("ClientId")
                        .HasColumnType("int");

                    b.Property<DateTime>("Date_creation")
                        .HasColumnType("datetime2");

                    b.Property<string>("Etat")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("FactureId")
                        .HasColumnType("int");

                    b.Property<bool>("Isverified")
                        .HasColumnType("bit");

                    b.Property<int?>("ListeRamassageId")
                        .HasColumnType("int");

                    b.Property<string>("Numero_Colis")
                        .HasColumnType("nvarchar(max)");

                    b.Property<float>("Prix")
                        .HasColumnType("real");

                    b.Property<string>("Statut")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("id");

                    b.HasIndex("BonsLivraisonId");

                    b.HasIndex("ClientId");

                    b.HasIndex("FactureId");

                    b.HasIndex("ListeRamassageId");

                    b.HasIndex("UserId");

                    b.ToTable("Colis");
                });

            modelBuilder.Entity("_1311.Models.Demande", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("DateCreation")
                        .HasColumnType("datetime2");

                    b.Property<int?>("LivreurId")
                        .HasColumnType("int");

                    b.Property<string>("Nom")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("Resolu")
                        .HasColumnType("bit");

                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("LivreurId");

                    b.HasIndex("UserId");

                    b.ToTable("Demande");
                });

            modelBuilder.Entity("_1311.Models.Facture", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("DateCreation")
                        .HasColumnType("datetime2");

                    b.Property<int?>("Frais")
                        .HasColumnType("int");

                    b.Property<int?>("LivreurId")
                        .HasColumnType("int");

                    b.Property<int?>("Net")
                        .HasColumnType("int");

                    b.Property<string>("Nom")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Statut")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("Total")
                        .HasColumnType("int");

                    b.Property<string>("Type")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("LivreurId");

                    b.HasIndex("UserId");

                    b.ToTable("Facture");
                });

            modelBuilder.Entity("_1311.Models.ListeRamassage", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("DateCreation")
                        .HasColumnType("datetime2");

                    b.Property<string>("Etat")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("LivreurId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int?>("VilleId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("LivreurId");

                    b.HasIndex("UserId");

                    b.HasIndex("VilleId");

                    b.ToTable("ListeRamassage");
                });

            modelBuilder.Entity("_1311.Models.Livreur", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Adresse")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("VilleId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserId")
                        .IsUnique()
                        .HasFilter("[UserId] IS NOT NULL");

                    b.HasIndex("VilleId");

                    b.ToTable("Livreur");
                });

            modelBuilder.Entity("_1311.Models.Reclamation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("DateCreation")
                        .HasColumnType("datetime2");

                    b.Property<string>("Message")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Reponse")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("Statut")
                        .HasColumnType("bit");

                    b.Property<string>("Sujet")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Reclamation");
                });

            modelBuilder.Entity("_1311.Models.Ville", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("Statut")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.ToTable("Ville");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("_1311.Models.AppUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("_1311.Models.AppUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("_1311.Models.AppUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("_1311.Models.AppUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("_1311.Models.AppUser", b =>
                {
                    b.HasOne("_1311.Models.Boutique", "Boutique")
                        .WithOne("User")
                        .HasForeignKey("_1311.Models.AppUser", "BoutiqueId");

                    b.Navigation("Boutique");
                });

            modelBuilder.Entity("_1311.Models.BonsLivraison", b =>
                {
                    b.HasOne("_1311.Models.AppUser", "User")
                        .WithMany("BonsLivraisons")
                        .HasForeignKey("UserId");

                    b.Navigation("User");
                });

            modelBuilder.Entity("_1311.Models.Boutique", b =>
                {
                    b.HasOne("_1311.Models.Bank", "Bank")
                        .WithOne("Boutique")
                        .HasForeignKey("_1311.Models.Boutique", "BankeId");

                    b.HasOne("_1311.Models.Ville", "Ville")
                        .WithMany("Boutique")
                        .HasForeignKey("VilleId");

                    b.Navigation("Bank");

                    b.Navigation("Ville");
                });

            modelBuilder.Entity("_1311.Models.Client", b =>
                {
                    b.HasOne("_1311.Models.Ville", "Ville")
                        .WithMany()
                        .HasForeignKey("VilleId");

                    b.Navigation("Ville");
                });

            modelBuilder.Entity("_1311.Models.Colis", b =>
                {
                    b.HasOne("_1311.Models.BonsLivraison", "BonsLivraison")
                        .WithMany("Colis")
                        .HasForeignKey("BonsLivraisonId");

                    b.HasOne("_1311.Models.Client", "Client")
                        .WithMany("colis")
                        .HasForeignKey("ClientId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("_1311.Models.Facture", "Facture")
                        .WithMany("Colis")
                        .HasForeignKey("FactureId");

                    b.HasOne("_1311.Models.ListeRamassage", "ListeRamassage")
                        .WithMany("Colis")
                        .HasForeignKey("ListeRamassageId");

                    b.HasOne("_1311.Models.AppUser", "User")
                        .WithMany("Colis")
                        .HasForeignKey("UserId");

                    b.Navigation("BonsLivraison");

                    b.Navigation("Client");

                    b.Navigation("Facture");

                    b.Navigation("ListeRamassage");

                    b.Navigation("User");
                });

            modelBuilder.Entity("_1311.Models.Demande", b =>
                {
                    b.HasOne("_1311.Models.Livreur", "Livreur")
                        .WithMany("Demande")
                        .HasForeignKey("LivreurId");

                    b.HasOne("_1311.Models.AppUser", "User")
                        .WithMany("Demandes")
                        .HasForeignKey("UserId");

                    b.Navigation("Livreur");

                    b.Navigation("User");
                });

            modelBuilder.Entity("_1311.Models.Facture", b =>
                {
                    b.HasOne("_1311.Models.Livreur", "Livreur")
                        .WithMany("Facture")
                        .HasForeignKey("LivreurId");

                    b.HasOne("_1311.Models.AppUser", "User")
                        .WithMany("Facture")
                        .HasForeignKey("UserId");

                    b.Navigation("Livreur");

                    b.Navigation("User");
                });

            modelBuilder.Entity("_1311.Models.ListeRamassage", b =>
                {
                    b.HasOne("_1311.Models.Livreur", "Livreur")
                        .WithMany("ListeRamassage")
                        .HasForeignKey("LivreurId");

                    b.HasOne("_1311.Models.AppUser", "User")
                        .WithMany("ListeRamassages")
                        .HasForeignKey("UserId");

                    b.HasOne("_1311.Models.Ville", "Ville")
                        .WithMany("ListeRamassage")
                        .HasForeignKey("VilleId");

                    b.Navigation("Livreur");

                    b.Navigation("User");

                    b.Navigation("Ville");
                });

            modelBuilder.Entity("_1311.Models.Livreur", b =>
                {
                    b.HasOne("_1311.Models.AppUser", "User")
                        .WithOne("Livreur")
                        .HasForeignKey("_1311.Models.Livreur", "UserId");

                    b.HasOne("_1311.Models.Ville", "Ville")
                        .WithMany("Livreur")
                        .HasForeignKey("VilleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");

                    b.Navigation("Ville");
                });

            modelBuilder.Entity("_1311.Models.Reclamation", b =>
                {
                    b.HasOne("_1311.Models.AppUser", "User")
                        .WithMany("reclamations")
                        .HasForeignKey("UserId");

                    b.Navigation("User");
                });

            modelBuilder.Entity("_1311.Models.AppUser", b =>
                {
                    b.Navigation("BonsLivraisons");

                    b.Navigation("Colis");

                    b.Navigation("Demandes");

                    b.Navigation("Facture");

                    b.Navigation("ListeRamassages");

                    b.Navigation("Livreur");

                    b.Navigation("reclamations");
                });

            modelBuilder.Entity("_1311.Models.Bank", b =>
                {
                    b.Navigation("Boutique");
                });

            modelBuilder.Entity("_1311.Models.BonsLivraison", b =>
                {
                    b.Navigation("Colis");
                });

            modelBuilder.Entity("_1311.Models.Boutique", b =>
                {
                    b.Navigation("User");
                });

            modelBuilder.Entity("_1311.Models.Client", b =>
                {
                    b.Navigation("colis");
                });

            modelBuilder.Entity("_1311.Models.Facture", b =>
                {
                    b.Navigation("Colis");
                });

            modelBuilder.Entity("_1311.Models.ListeRamassage", b =>
                {
                    b.Navigation("Colis");
                });

            modelBuilder.Entity("_1311.Models.Livreur", b =>
                {
                    b.Navigation("Demande");

                    b.Navigation("Facture");

                    b.Navigation("ListeRamassage");
                });

            modelBuilder.Entity("_1311.Models.Ville", b =>
                {
                    b.Navigation("Boutique");

                    b.Navigation("ListeRamassage");

                    b.Navigation("Livreur");
                });
#pragma warning restore 612, 618
        }
    }
}
