using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using LibraryMvc.Models;

namespace LibraryMvc.Data
{
    public class LibraryMvcContext : DbContext
    {
        public LibraryMvcContext(DbContextOptions<LibraryMvcContext> options)
            : base(options)
        {
        }

        public DbSet<LibraryMvc.Models.Library> Library { get; set; } = default!;
        public DbSet<LibraryMvc.Models.Shelf> Shelf { get; set; } = default!;
        public DbSet<LibraryMvc.Models.Book> Book { get; set; } = default!;
        public DbSet<LibraryMvc.Models.Set> Set { get; set; } = default!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Library>()
                .HasIndex(l => l.genre)
                .IsUnique();

            modelBuilder.Entity<Library>()
                .HasMany(l => l.Shelves)
                .WithOne(s => s.Library)
                .HasForeignKey(s => s.LibraryId)
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Shelf>()
                .HasMany(s => s.Books)
                .WithOne(b => b.Shelf)
                .HasForeignKey(b => b.ShelfId)
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Set>()
                .HasMany(s => s.Books)
                .WithOne(b => b.Set)
                .HasForeignKey(b => b.SetId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
