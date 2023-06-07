using IdentityCustomMVC.Entities;
using IdentityCustomMVC.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace IdentityCustomMVC.Data
{
    public class AppDbContext : IdentityDbContext<ApplicationUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }

        //public DbSet<ApplicationUser> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<ApplicationUser>().ToTable("AspNetUsers").HasKey(t => t.Id);

            base.OnModelCreating(builder);
           
            //*PRODUTO INICIAL.
            builder.Entity<Product>().HasData(new Product
            {
                Id = 1,
                Name = "Teclado Logitech",
                Description = "Teclado Logitech G213",

            });
           
        }

        #region CONFIGURACAO DA STRING DE CONEXAO NECESSARIA POIS ESTOU HERDANDO ( AppDbContext : IdentityDbContext<User> )_ .

        //*CASO NAO REALIZE ESSE OVERRIDE DARÁ UM ERRO DE DbContext SEM ENTIDADE NO CONSTRUTOR.

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) //*
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseMySql(GetStringConnection(), ServerVersion.Parse("8.0.29"));
                base.OnConfiguring(optionsBuilder);
            }
        }

        private string GetStringConnection()
        {
            return "server=localhost;userid=root;password=123456;database=identitycustommvcoficial";
        }

        public DbSet<IdentityCustomMVC.Models.ChangePasswordViewModel> ChangePasswordViewModel { get; set; } = default!;

        public DbSet<IdentityCustomMVC.Models.ForgotPasswordViewModel> ForgotPasswordViewModel { get; set; } = default!;

        public DbSet<IdentityCustomMVC.Models.ResetPasswordViewModel> ResetPasswordViewModel { get; set; } = default!;

        #endregion
    }
}

