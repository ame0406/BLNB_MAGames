using BLNB_MAGames;
using BLNB_MAGames.Pages;
using BLNB_MAGames.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
	.AddInteractiveServerComponents();

builder.Services.AddControllers();

builder.Services.AddHttpClient<ApiService>(); // Configurer HttpClient
builder.Services.AddScoped<ApiService>();
builder.Services.AddScoped<ProfileStateService>();
builder.Services.AddScoped<CartService>();

builder.Services.AddHttpContextAccessor(); // utile si tu veux accéder au contexte partout
builder.Services.AddDistributedMemoryCache(); // Nécessaire pour IDistributedCache
builder.Services.AddSession(options =>
{
    //options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

var app = builder.Build();

app.MapControllers();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
	app.UseExceptionHandler("/Error", createScopeForErrors: true);
	// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
	app.UseHsts();
}

app.UseHttpsRedirection();

app.UseSession();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
	.AllowAnonymous()
	.AddInteractiveServerRenderMode();

//app.UseStatusCodePagesWithRedirects("/404");

app.Run();
