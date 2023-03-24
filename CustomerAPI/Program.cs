using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Mvc;
using MediatR;
using FluentValidation;

using Contracts;
using Shared.DTO;
using Application.Behaviors;
using CustomerAPI.Extensions;
using CustomerAPI.Presentation.Filters.ActionFilters;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.ConfigureRepositoryManager();
builder.Services.Configure<ApiBehaviorOptions>(options => {
    options.SuppressModelStateInvalidFilter = true;
});

builder.Services.AddControllers(config => {
    config.RespectBrowserAcceptHeader = true; 
    config.ReturnHttpNotAcceptable = true; 
    config.CacheProfiles.Add("120SecondsDuration", new CacheProfile { Duration = 120 });
  }).AddXmlDataContractSerializerFormatters()
    .AddApplicationPart(typeof(CustomerAPI.Presentation.AssemblyReference).Assembly);
builder.Services.ConfigureSqlContext(builder.Configuration);
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddAutoMapper(typeof(Program));
builder.Services.ConfigureCors();
builder.Services.ConfigureIIS();
builder.Services.ConfigureResponseCaching();
builder.Services.ConfigureHttpCacheHeaders();
builder.Services.AddHttpContextAccessor();
builder.Services.ConfigureSwagger();
builder.Services.AddMediatR(typeof(Application.AssemblyReference).Assembly);
builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
builder.Services.AddScoped<ValidationFilterAttribute>();
builder.Services.AddValidatorsFromAssembly(typeof(Application.AssemblyReference).Assembly);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger(); 
    app.UseSwaggerUI(s =>
    { 
        s.SwaggerEndpoint("/swagger/v1/swagger.json", "Customer API v1"); 
        s.SwaggerEndpoint("/swagger/v2/swagger.json", "Customer API v2"); 
    });

    // ===
    // app.UseDeveloperExceptionPage();
}
else 
    app.UseHsts();
// ===

app.UseHttpsRedirection();

// ===
app.UseStaticFiles(); 

app.UseForwardedHeaders(new ForwardedHeadersOptions
{
    ForwardedHeaders = ForwardedHeaders.All 
});

app.UseCors("CorsPolicy");
// ===
app.UseResponseCaching();

app.UseHttpCacheHeaders();

app.UseAuthorization();

app.MapControllers();

app.Run();