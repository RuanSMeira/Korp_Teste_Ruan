using Korp_Teste_Ruan_Backend.Data;
using Korp_Teste_Ruan_Backend.Interfaces;
using Korp_Teste_Ruan_Backend.Repositories;
using Korp_Teste_Ruan_Backend.Services;
using Korp_Teste_Ruan_Backend.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// DbContext
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Repositories
builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();
builder.Services.AddScoped<IEmpresaRepository, EmpresaRepository>();
builder.Services.AddScoped<IProdutoRepository, ProdutoRepository>();
builder.Services.AddScoped<IItemNotaFiscalRepository, ItemNotaFiscalRepository>();
builder.Services.AddScoped<INotaFiscalRepository, NotaFiscalRepository>();

// Services
builder.Services.AddScoped<UsuarioService>();
builder.Services.AddScoped<EmpresaService>();
builder.Services.AddScoped<ProdutoService>();
builder.Services.AddScoped<ItemNotaFiscalService>();
builder.Services.AddScoped<NotaFiscalService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthorization();

app.MapStaticAssets();
app.MapControllers(); // Necessário para as rotas [ApiController] tipo api/empresas, api/produtos, api/notafiscal, etc.

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

app.Run();