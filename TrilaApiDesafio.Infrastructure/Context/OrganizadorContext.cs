using Microsoft.EntityFrameworkCore;
using TrilhaApiDesafio.TrilaApiDesafio.Domain.Models;

namespace TrilhaApiDesafio.TrilaApiDesafio.Infrastructure.Context
{
    public class OrganizadorContext : DbContext
    {
        public OrganizadorContext(DbContextOptions<OrganizadorContext> options) : base(options)
        {

        }

        public DbSet<Tarefa> Tarefas { get; set; }
    }
}