using IdentityCustomMVC.Data;
using IdentityCustomMVC.Entities;
using IdentityCustomMVC.Interfaces;
using IdentityCustomMVC.Interfaces.Emails;
using IdentityCustomMVC.Repositories;
using IdentityCustomMVC.Services;
using IdentityCustomMVC.Services.Emails;
using IdentityCustomMVC.Settings.Emails;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);


#region STRING DE CONEXAO MYSQL

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<AppDbContext>(options => options.UseMySql(connectionString, ServerVersion.Parse("8.0.29")));

#endregion

#region CONFIGURA O IDENTITY NA CLASSE PROGRAM

builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
                     .AddEntityFrameworkStores<AppDbContext>()
                     .AddTokenProvider<DataProtectorTokenProvider<ApplicationUser>>(TokenOptions.DefaultProvider);
#endregion

#region INJECAO DE DEPENDENCIAS, REGISTROS DE SERVIÇOS.

//*INTERFACE PARA CRIACAO INICIAL DAS ROLES E USERS.
builder.Services.AddScoped<ISeedUserRoleInitial, SeedUserRoleInitial>();

//*INTERFACE GENERICA
builder.Services.AddSingleton(typeof(IGeneric<>), typeof(GenericRepository<>));

//*INTERFACE DE PRODUTO.
builder.Services.AddScoped<IProduct, ProductRepository>();

//*INTERFACE DE USUARIO.
builder.Services.AddScoped<IUser, UserRepository>();

//*INTERFACE DE ACCOUNT.
builder.Services.AddScoped<IAccount, AccountRepository>();

#endregion

#region INJECAO DE DEPENDENCIA E SERVICOS DE ENVIOS DE EMAILS


//*CONFIGURANDO VIDA UTIL DO TOKEN
builder.Services.Configure<DataProtectionTokenProviderOptions>(opt =>
   opt.TokenLifespan = TimeSpan.FromHours(2));

//*OBTEM A CONFIGURAÇÃO FEITA NA appsettings.json
//builder.Services.Configure<GMailSettings>(builder.Configuration.GetSection(nameof(GMailSettings)));
builder.Services.Configure<SendinBlueSettings>(builder.Configuration.GetSection(nameof(SendinBlueSettings)));
//builder.Services.Configure<SendGridSettings>(builder.Configuration.GetSection(nameof(SendGridSettings)));

//*INJETANDO CLASSE DO PROVEDOR UTILIZADO PARA ENVIO DE EMAILS
//builder.Services.AddScoped<IEmailService, GMailService>();
builder.Services.AddScoped<IEmailService, SendinBlueService>();
//builder.Services.AddScoped<IEmailService, SendGridService>();


#endregion

#region CONFIGURANDO A CLASSE DE EmailConfiguration DO PROJETO EmailService.

//*REGISTRANDO CONFIGURAÇÃO DO MÉTODO FormOptions
builder.Services.Configure<FormOptions>(o => {
    o.ValueLengthLimit = int.MaxValue;
    o.MultipartBodyLengthLimit = int.MaxValue;
    o.MemoryBufferThreshold = int.MaxValue;
});


//Senha do app = Nome:EmailService | Senha:ywwzbtnnliwhnxdq

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
