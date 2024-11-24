using SpendSmartV3.Data;

using SpendSmartV3.Services.Account;
using SpendSmartV3.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using SpendSmartV3.Data.Core.Interfaces;
using SpendSmartV3.Data.Core.Repositories;
using SpendSmartV3.Data.Core;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

string connectionString = builder.Configuration.GetConnectionString("DefaultConnection")!;

builder.Services.AddDbContext<SpendSmartDbContext>(options => options.UseSqlServer(connectionString));
builder.Services.AddIdentity<IdentityUser, IdentityRole>().AddEntityFrameworkStores<SpendSmartDbContext>();
builder.Services.AddScoped<IExpenseServices, ExpenseServices>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IAccountServices, AccountServices>();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Account}/{action=Login}/{id?}");

app.UseAuthentication();
app.UseAuthorization();

app.Run();
