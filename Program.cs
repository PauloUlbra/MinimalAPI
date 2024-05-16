using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

//configurar o DbContext com o SQLite
builder.Services.AddDbContext<AppDbContext>(options =>
options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

//registrando o repositório
builder.Services.AddScoped<IProductRepository, ProductRepository>();

var app = builder.Build();

//Criar o banco de dados e aplicar as migrações
using(var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    dbContext.Database.Migrate();
}

app.MapGet("/", () => "Hello World!");

//endpoint para obter todos os produtos
app.MapGet("/products", async (IProductRepository repository) => 
Results.Ok(await repository.GetAll()));

//endpoint para obter um produto pelo id
app.MapGet("/products/{id}", async (int id, IProductRepository repository) =>
{
    var product = await repository.GetByID(id);
    return product is not null ? Results.Ok(product) : Results.NotFound();
});

app.MapPost("/products", async (Product product, IProductRepository repository) =>
{
    await repository.Add(product);
    return Results.Ok(product);
});

app.MapPut("/products/{id}", async (int id, Product updateProduct, IProductRepository repository) =>
{
    await repository.Update(updateProduct);
    return Results.NoContent();
});

app.MapDelete("/products/{id}", async (int id, IProductRepository repository) =>
{
    var product = repository.GetByID(id);
    if(product is null)
    {
        return Results.NotFound();
    }
    await repository.Delete(id);
    return Results.NoContent();
});
app.Run();
