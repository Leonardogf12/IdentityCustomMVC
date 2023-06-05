using IdentityCustomMVC.Data;
using IdentityCustomMVC.Entities;
using IdentityCustomMVC.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Razor.Language.Intermediate;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;

var builder = WebApplication.CreateBuilder(args);

#region STRING DE CONEXAO MYSQL

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<AppDbContext>(options => options.UseMySql(connectionString, ServerVersion.Parse("8.0.29")));

#endregion

#region CONFIGURA O IDENTITY NA CLASSE PROGRAM

builder.Services.AddIdentity<IdentityUser, IdentityRole>()
                        .AddEntityFrameworkStores<AppDbContext>();
#endregion

#region INJECAO DE DEPENDENCIAS, REGISTROS.

builder.Services.AddScoped<ISeedUserRoleInitial, SeedUserRoleInitial>();

#endregion

// Add services to the container.
builder.Services.AddControllersWithViews();

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

#region CHAMA FUNCAO PARA CRIACAO DAS ROLES INICIAIS E USUARIO MASTER.

await CreateRoleAndUserProfile(app);

#endregion

#region MIDDLEWARE DE AUTENTICACAO

app.UseAuthentication();

#endregion

app.UseAuthorization();

#region ROTA AREA ADMIN

app.MapControllerRoute(
    name: "MinhaArea",
    pattern: "{area:exists}/{controller=Admin}/{action=Index}/{id?}");

#endregion

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

#region METODOS PARA CRIACAO DAS ROLES E USUARIO MASTER NA INICIALIZAÇÃO.

async Task CreateRoleAndUserProfile(WebApplication app)
{
    var scopedFactory = app.Services.GetService<IServiceScopeFactory>();
    using (var scope = scopedFactory.CreateScope()) { 
    
        var service = scope.ServiceProvider.GetService<ISeedUserRoleInitial>();
        await service.SeedRoleAsync();
        await service.SeedUserAsync();
    }
}

#endregion
