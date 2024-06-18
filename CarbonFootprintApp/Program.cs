using CarbonFootprintApp.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews().AddXmlDataContractSerializerFormatters();

// Database connection string path.
builder.Services.AddDbContext<CarbonFootprintDbContext>(optins =>
{
    optins.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

// Add identity method.
builder.Services.AddIdentity<ApplicationUser, IdentityRole>().AddEntityFrameworkStores<CarbonFootprintDbContext>();

// Add identity password configuration
builder.Services.Configure<IdentityOptions>(options =>
{
    options.Password.RequiredLength = 5;
    options.Password.RequiredUniqueChars = 3;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
else
{
    var developerExceptionPageOptions = new DeveloperExceptionPageOptions
    {
        SourceCodeLineCount = 10
    };

    app.UseDeveloperExceptionPage(developerExceptionPageOptions);
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Account}/{action=Login}/{id?}");

app.Run();