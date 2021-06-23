using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace Pokedex.Api
{
		public class Startup
		{
				public void ConfigureServices(IServiceCollection services)
				{
						services.AddControllers();
						services.AddHttpClient();
						services.AddSwaggerGen(c =>
						{
								c.SwaggerDoc("v1", new OpenApiInfo { Title = "Pokedex", Version = "v1" });
						});
				}

				public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
				{
						if (env.IsDevelopment())
						{
								app.UseDeveloperExceptionPage();
								app.UseSwagger();
								app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Pokedex v1"));
						}

						app.UseHttpsRedirection();

						app.UseRouting();

						app.UseEndpoints(endpoints =>
						{
								endpoints.MapControllers();
						});
				}
		}
}
