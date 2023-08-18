using OrderManagementApi.Extensions.Startup;

var builder = WebApplication.CreateBuilder(args);

{
    builder.ConfigureDatabase()
           .ConfigureServices();

    builder.Services.AddControllers();
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();
    builder.Services.AddHealthChecks();
}

var app = builder.Build();

{
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.SetupDatabase();

    app.UseAuthorization();

    app.MapControllers();

    app.UseHealthChecks("/health");
}

app.Run();