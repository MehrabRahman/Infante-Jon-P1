using DL;
using BL;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
// Add references to interface repos
builder.Services.AddScoped<ISRepo>(ctx => new DBStoreRepo(builder.Configuration.GetConnectionString("P1DB")));
builder.Services.AddScoped<ISBL, StoreBL>();
builder.Services.AddScoped<IURepo>(ctx => new DBUserRepo(builder.Configuration.GetConnectionString("P1DB")));
builder.Services.AddScoped<IUBL, UserBL>();



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
