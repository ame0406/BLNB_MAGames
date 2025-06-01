using api.BLogics;
using api.Data;
using api.DataAccessLayer;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();

//BL
builder.Services.AddScoped<DropDeepSeachBL>();
builder.Services.AddScoped<StocksBL>();

//DL
builder.Services.AddScoped<Base_ObjDL>();
builder.Services.AddScoped<DropDeepSearchDL>();
builder.Services.AddScoped<StocksDL>();
builder.Services.AddScoped<VenteEbayDL>();
builder.Services.AddScoped<VenteMKPDL>();
builder.Services.AddScoped<LotsDL>();

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<DataContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
