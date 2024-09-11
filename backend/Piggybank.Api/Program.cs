using Piggybank.Api.Configurations;
using Piggybank.Api.Middlewares;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Services.AddApplicationServices(builder.Configuration);

WebApplication app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<ErrorHandlingMiddleware>();
app.UseHttpsRedirection();
app.MapControllers();

app.Run();
