using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using SponteAPI.Data;
using SponteAPI.Models;
using SponteAPI.Validators;

namespace apisponte
{
  public class Startup
  {
    public Startup(IConfiguration configuration)
    {
      Configuration = configuration;
    }

    public IConfiguration Configuration { get; }
    public void ConfigureServices(IServiceCollection services)
    {

      services.AddScoped<ICategoryRepo, CategoryRepo>();
      services.AddScoped<IProductRepo, ProductRepo>();
      services.AddTransient<IValidator<Category>, CategoryValidator>();
      services.AddTransient<IValidator<Product>, ProductValidator>();

      services.AddDbContext<ProductContext>(opt => opt.UseSqlServer
            (Configuration.GetConnectionString("SponteAPIConnection")));
      services.AddControllers();
      services.AddSwaggerGen(c =>
      {
        c.SwaggerDoc("v1", new OpenApiInfo
        {
          Version = "v1",
          Title = "API de Produtos Sponte",
          Description = "Uma API simples em .NET Core para controle de produtos e categorias",
          Contact = new OpenApiContact
          {
            Name = "Reinaldo Hassel Neto",
            Email = "reinaldo@sponte.com.br",
            Url = new Uri("https://sponte.com.br")
          },
        });
      });
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
      if (env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
      }

      app.UseSwagger(c =>
      {
        c.SerializeAsV2 = true;
      });

      app.UseSwaggerUI(c =>
      {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "API");
        c.RoutePrefix = string.Empty;
      });

      app.UseHttpsRedirection();

      app.UseRouting();

      app.UseAuthorization();

      app.UseEndpoints(endpoints =>
      {
        endpoints.MapControllers();
      });
    }
  }
}
