using Microsoft.EntityFrameworkCore;

public class AppDbContext : DbContext
{
    //Construtor que recebe as opções de configuração do contexto
    public AppDbContext (DbContextOptions<AppDbContext> options) : base(options) {}
    //Propriedade DbSet que representa a tabela de produtos no banco de dados
    public DbSet<Product> Products { get; set; }

}