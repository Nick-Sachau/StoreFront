﻿using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace StoreFront.DATA.EF.Models
{
    public partial class StoreFrontContext : DbContext
    {
        public StoreFrontContext()
        {
        }

        public StoreFrontContext(DbContextOptions<StoreFrontContext> options)
            : base(options)
        {
        }

        public virtual DbSet<City> Cities { get; set; } = null!;
        public virtual DbSet<Order> Orders { get; set; } = null!;
        public virtual DbSet<OrderPokemon> OrderPokemons { get; set; } = null!;
        public virtual DbSet<Pokemon> Pokemons { get; set; } = null!;
        public virtual DbSet<PokemonType> PokemonTypes { get; set; } = null!;
        public virtual DbSet<TrainerDetail> TrainerDetails { get; set; } = null!;
        public virtual DbSet<Type> Types { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=.\\sqlexpress;database=StoreFront;trusted_connection=true;multipleactiveresultsets=true;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<City>(entity =>
            {
                entity.Property(e => e.CityId)
                    .ValueGeneratedNever()
                    .HasColumnName("CityID");

                entity.Property(e => e.CityName)
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.Property(e => e.OrderId)
                    .ValueGeneratedNever()
                    .HasColumnName("OrderID");

                entity.Property(e => e.OrderDate).HasColumnType("datetime");

                entity.Property(e => e.ShipCity)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ShipState)
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.ShipToName)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.ShipZip)
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.TrainerId)
                    .HasMaxLength(128)
                    .HasColumnName("TrainerID");

                entity.HasOne(d => d.Trainer)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.TrainerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Orders_TrainerDetails");
            });

            modelBuilder.Entity<OrderPokemon>(entity =>
            {
                entity.ToTable("OrderPokemon");

                entity.Property(e => e.OrderPokemonId)
                    .ValueGeneratedNever()
                    .HasColumnName("OrderPokemonID");

                entity.Property(e => e.OrderId).HasColumnName("OrderID");

                entity.Property(e => e.PokemonId).HasColumnName("PokemonID");

                entity.Property(e => e.ProductPrice).HasColumnType("money");

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.OrderPokemons)
                    .HasForeignKey(d => d.OrderId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_OrderPokemon_Orders");

                entity.HasOne(d => d.Pokemon)
                    .WithMany(p => p.OrderPokemons)
                    .HasForeignKey(d => d.PokemonId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_OrderPokemon_Pokemon");
            });

            modelBuilder.Entity<Pokemon>(entity =>
            {
                entity.ToTable("Pokemon");

                entity.Property(e => e.PokemonId).HasColumnName("PokemonID");

                entity.Property(e => e.CityId).HasColumnName("CityID");

                entity.Property(e => e.PokemonBall)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.PokemonDescription)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.PokemonImage)
                    .HasMaxLength(75)
                    .IsUnicode(false);

                entity.Property(e => e.PokemonName)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.PokemonPrice).HasColumnType("money");

                entity.HasOne(d => d.City)
                    .WithMany(p => p.Pokemons)
                    .HasForeignKey(d => d.CityId)
                    .HasConstraintName("FK_Pokemon_Shelter");
            });

            modelBuilder.Entity<PokemonType>(entity =>
            {
                entity.ToTable("Pokemon_Types");

                entity.Property(e => e.PokemonTypeId).HasColumnName("PokemonTypeID");

                entity.Property(e => e.PokemonId).HasColumnName("PokemonID");

                entity.Property(e => e.TypeId).HasColumnName("TypeID");

                entity.HasOne(d => d.Pokemon)
                    .WithMany(p => p.PokemonTypes)
                    .HasForeignKey(d => d.PokemonId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Pokemon_Types_Pokemon");

                entity.HasOne(d => d.Type)
                    .WithMany(p => p.PokemonTypes)
                    .HasForeignKey(d => d.TypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Pokemon_Types_Types");
            });

            modelBuilder.Entity<TrainerDetail>(entity =>
            {
                entity.HasKey(e => e.TrainerId);

                entity.Property(e => e.TrainerId)
                    .HasMaxLength(128)
                    .HasColumnName("TrainerID");

                entity.Property(e => e.Address)
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.City)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.FirstName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.LastName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Phone)
                    .HasMaxLength(24)
                    .IsUnicode(false);

                entity.Property(e => e.State)
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.Zip)
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .IsFixedLength();
            });

            modelBuilder.Entity<Type>(entity =>
            {
                entity.Property(e => e.TypeId).HasColumnName("TypeID");

                entity.Property(e => e.TypeName)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
