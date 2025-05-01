using Microsoft.EntityFrameworkCore;
using OpenTelemetry.Trace;
using Thunders.TechTest.ApiService;
using Thunders.TechTest.ApiService.Application.Commands;
using Thunders.TechTest.ApiService.Application.Messages;
using Thunders.TechTest.ApiService.Interfaces;
using Thunders.TechTest.ApiService.Middlewares;
using Thunders.TechTest.ApiService.Services;
using Thunders.TechTest.OutOfBox.Database;
using Thunders.TechTest.OutOfBox.Queues;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();
builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
});

var features = Features.BindFromConfiguration(builder.Configuration);

// Add services to the container.
builder.Services.AddProblemDetails();

if (features.UseMessageBroker)
{
    builder.Services.AddBus(builder.Configuration, new SubscriptionBuilder()
         .Add<RegisterTollUsageMessage>());

    builder.Services.AddScoped<IMessageSender, RebusMessageSender>();
}

if (features.UseEntityFramework)
{
    builder.Services.AddSqlServerDbContext<AppDbContext>(builder.Configuration);
    builder.Services.AddScoped<ITollUsageRepository, TollUsageRepository>();
}

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(EnqueueTollUsageCommand).Assembly));
builder.Services.AddOpenTelemetry()
    .WithTracing(tracing =>
    {
        tracing
            .AddAspNetCoreInstrumentation()
            .AddHttpClientInstrumentation()
            .AddEntityFrameworkCoreInstrumentation();
    });

var app = builder.Build();

if (features.UseEntityFramework)
{
    using var scope = app.Services.CreateScope();
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.Migrate();
}

// Configure the HTTP request pipeline.
app.UseMiddleware<ErrorHandlingMiddleware>();

app.MapDefaultEndpoints();

app.MapControllers();
app.UseSwagger();
app.UseSwaggerUI();
app.UseDeveloperExceptionPage();

app.Run();