using Microsoft.EntityFrameworkCore;

namespace OPDTask6
{
    public class Todo_list
    {
        public int Id { get; set; }
        public string? Date { get; set; }
        public string? Name { get; set; }
    }
    public class MiniApp : DbContext
    {
        public DbSet<Todo_list> Tasks { get; set; } = null!;
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=TodolistDB;Trusted_Connection=True;");
        }
    }
}