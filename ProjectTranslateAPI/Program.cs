using ProjectTranslateAPI;
using ProjectTranslateAPI.Model;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
var appconfig = builder.Configuration.GetSection("AppConfig");
WebAppAPIConfiguration apiConfiguration = appconfig.Get<WebAppAPIConfiguration>();
builder.Services.AddSingleton(apiConfiguration);
builder.Services.WebbAppSignUpService();

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
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
