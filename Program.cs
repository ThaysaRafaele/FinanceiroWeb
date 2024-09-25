using FinanceiroWeb.Data; // Namespace onde seu DbContext está
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Adicionar a configuração do serviço do DbContext
builder.Services.AddDbContext<FinanceiroContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Adicionar os serviços de controllers e views
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configuração do pipeline de requisição HTTP
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts(); // Força o uso de HTTPS
}

app.UseHttpsRedirection();
app.UseStaticFiles(); // Habilita arquivos estáticos como CSS, JS, imagens, etc.

app.UseRouting();

app.UseAuthorization();

// Configura a rota padrão
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Dashboard}/{action=Index}/{id?}");

app.Run();
