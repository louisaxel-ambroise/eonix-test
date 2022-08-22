using Eonix.Api.Database;
using Eonix.Api.Utils;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers(opt => opt.SuppressAsyncSuffixInActionNames = false);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<EonixContext>(opt => opt.UseSqlite(builder.Configuration.GetConnectionString("PersonnesDatabase")));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.ApplyMygrations();
app.UseExceptionHandler(ExceptionHandler.Options);
app.MapControllers();
app.Run();
