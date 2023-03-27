using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Mvc;

using Application;
using CustomerAPI.Extensions;
using CustomerAPI.Presentation.Filters.ActionFilters;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.ConfigureUnitOfWork();
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
builder.Services.ConfigureCors();
builder.Services.ConfigureIIS();
builder.Services.ConfigureApplicationServices();
builder.Services.ConfigureSwagger();
builder.Services.AddScoped<ValidationFilterAttribute>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger(); 
    app.UseSwaggerUI(s =>
    { 
        s.SwaggerEndpoint("/swagger/v1/swagger.json", "Customer API v1"); 
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
app.UseAuthorization();

app.MapControllers();

app.Run();