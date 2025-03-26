using Microsoft.EntityFrameworkCore;
using TarefaApi.Models;

namespace TarefaApi.Dados
{
    public class TarefaContexto : DbContext
    {
        public DbSet<Tarefa> Tarefa { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(connectionString: "Data Source=tarefas.sqlite");
            base.OnConfiguring(optionsBuilder);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Tarefa>()
                .HasKey(t => t.Codigo);

            modelBuilder.Entity<Tarefa>()
                .Property(t => t.Status)
                .HasConversion(
                    v => v.ToString(),
                    v => (Status)Enum.Parse(typeof(Status), v)
                );
        }
    }
}
