using Piggybank.Api.Configurations;
using Piggybank.Api.Middlewares;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Services.AddApplicationServices(builder.Configuration);

WebApplication app = builder.Build();

// Configure the HTTP request pipeline.
await app.AddRoles();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<ErrorHandlingMiddleware>();
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();
