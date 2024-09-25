using FinanceiroWeb.Data; // Namespace onde seu DbContext est�
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Adicionar a configura��o do servi�o do DbContext
builder.Services.AddDbContext<FinanceiroContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Adicionar os servi�os de controllers e views
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configura��o do pipeline de requisi��o HTTP
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts(); // For�a o uso de HTTPS
}

app.UseHttpsRedirection();
app.UseStaticFiles(); // Habilita arquivos est�ticos como CSS, JS, imagens, etc.

app.UseRouting();

app.UseAuthorization();

// Configura a rota padr�o
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Dashboard}/{action=Index}/{id?}");

app.Run();
