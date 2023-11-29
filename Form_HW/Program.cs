using EF_Form_HW.Data.Entities;
using EF_Form_HW.Services.Valid;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using MySqlConnector;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddSingleton<IValidationService, MyValidationService>();
builder.Services.AddSingleton<expandingMethods>();

builder.Configuration.AddJsonFile("dbsettings.json");

String? connectString = builder.Configuration.GetConnectionString("PlanetScale");

MySqlConnection connection = new(connectString);

builder.Services.AddDbContext<DataContext>(options =>

    options.UseMySql(
        connection,
        ServerVersion.AutoDetect(connection),
                            serverOptions => serverOptions.MigrationsHistoryTable(
                tableName: HistoryRepository.DefaultTableName,
                schema: "EF_FORM_HW").SchemaBehavior(
                    MySqlSchemaBehavior.Translate,
                    (schema, table) => $"{schema}_{table}")

    ));


builder.Services.AddDistributedMemoryCache();

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromSeconds(10);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

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

app.UseSession();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
