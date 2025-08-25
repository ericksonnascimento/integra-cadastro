using IntegraCadastro.Api.Common;
using IntegraCadastro.Api.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddControllers()
    .AddNewtonsoftJson();

builder.Services.AddOpenApi();

builder.Services.Configure<AppSettings>(builder.Configuration.GetSection("AppSettings"));
builder.Services.AddSingleton(builder.Configuration.GetConnectionString("DefaultConnection"));
builder.Services.AddScoped<IIntegraService, IntegraService>();

var app = builder.Build();

//if (app.Environment.IsDevelopment())
//{
app.MapOpenApi();
//}

//app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();