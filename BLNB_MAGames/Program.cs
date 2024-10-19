using BLNB_MAGames;
using BLNB_MAGames.Pages;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
	.AddInteractiveServerComponents();

builder.Services.AddControllers();

builder.Services.AddHttpClient<ApiService>(); // Configurer HttpClient
builder.Services.AddScoped<ApiService>();

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

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
	.AllowAnonymous()
	.AddInteractiveServerRenderMode();

//app.UseStatusCodePagesWithRedirects("/404");

app.Run();
