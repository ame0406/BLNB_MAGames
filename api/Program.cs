using api.BLogics;
using api.Data;
using api.DataAccessLayer;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();

//BL
builder.Services.AddScoped<DropDeepSeachBL>();
builder.Services.AddScoped<StocksBL>();
builder.Services.AddScoped<StatistiquesBL>();

//DL
builder.Services.AddScoped<Base_ObjDL>();
builder.Services.AddScoped<DropDeepSearchDL>();
builder.Services.AddScoped<StocksDL>();
builder.Services.AddScoped<VenteEbayDL>();
builder.Services.AddScoped<VenteMKPDL>();
builder.Services.AddScoped<LotsDL>();
builder.Services.AddScoped<StatistiquesDL>();

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<DataContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddHttpContextAccessor();

builder.Services.AddDistributedMemoryCache(); // ✅ nécessaire pour la session

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromHours(8); // tu peux la remettre si besoin
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

builder.Services.AddControllers()
    .AddJsonOptions(x =>
        x.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles
    );



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseDeveloperExceptionPage();
}


app.UseDeveloperExceptionPage();

app.UseAuthorization();
app.UseSession(); // ✅ active la session ici

app.MapControllers();

app.Run();
