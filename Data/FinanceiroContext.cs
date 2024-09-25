using Microsoft.EntityFrameworkCore;
using FinanceiroWeb.Models;

namespace FinanceiroWeb.Data
{
    public class FinanceiroContext : DbContext
    {
        public FinanceiroContext(DbContextOptions<FinanceiroContext> options) : base(options)
        {
        }

        public DbSet<NotaFiscal> NotasFiscais { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<NotaFiscal>()
                .HasKey(nf => nf.NotaId); 
        }
    }
}
