var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// ---> AÑADE ESTE BLOQUE DE CÓDIGO <---
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
// Aquí registraríamos nuestro DbContext, pero como aún no lo tenemos,
// solo vamos a imprimir la cadena de conexión en la consola para verificar que la recibe.
Console.WriteLine($"---> Cadena de conexión recibida: {connectionString}");
// --- FIN DEL BLOQUE ---


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
