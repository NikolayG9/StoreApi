using Store.API.Extensions;
using Store.API.Middlewares;
using Store.Application.Extensions;
using Store.Application.Options;
using Store.Domain.Entities;
using Store.Infrastructure.Extensions;
using Serilog;
using Store.Infrastructure.Seeders;

try
{
    var builder = WebApplication.CreateBuilder(args);

    builder.AddPresentation();
    builder.Services.AddApplication();
    builder.Services.AddInfrastructure(builder.Configuration);
    
    builder.Services.Configure<GmailOptions>(builder.Configuration.GetSection(GmailOptions.GmailOptionsKey));
    builder.Services.Configure<BlobStorageOptions>(builder.Configuration.GetSection(BlobStorageOptions.BlobStorageOptionsKey));

    var app = builder.Build();


    var scope = app.Services.CreateScope();
    var seeder = scope.ServiceProvider.GetRequiredService<IStoreSeeder>();

    await seeder.Seed();

    // Configure the HTTP request pipeline.
    app.UseMiddleware<ErrorHandlingMiddleware>();
    app.UseMiddleware<RequestTimeLoggingMiddleware>();

    app.UseSerilogRequestLogging();

    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.MapGroup("api/identity").MapIdentityApi<User>();

    app.UseHttpsRedirection();

    app.UseAuthorization();

    app.MapControllers();

    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Application startup failed");
}
finally
{
    Log.CloseAndFlush();
}