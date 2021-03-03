using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Servicios.api.Libreria.Core;
using Servicios.api.Libreria.Core.ContextMongoDB;
using Servicios.api.Libreria.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Servicios.api.Libreria
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            /*archivo json: para acceder a la bd y sus atributos de forma dinámica.
              -clase MongoSettings: para representar los setting del archivo jason
              -clase startup: se referencia los atributos de la clase MongoSettings, ya que este archivo es 
               el primero que se levanta en el proyecto ejecutando todos los servicios al iniciar*/

            services.Configure<MongoSettings>(
                options =>
                {
                    options.ConnectionString = Configuration.GetSection("MongoDb:ConnectionString").Value;
                    options.Database = Configuration.GetSection("MongoDb:Database").Value;
                }
                );
            /*singleton permite que el objeto se mantenga siempre con vida
             cuando se consulte por un objeto MongoSettings va a evaluar si existe y nos devuelve 
            el objeto en sesion, pero si no existe nos va a crear ese objeto tipo MongoSettings*/
            services.AddSingleton<MongoSettings>();

            /*se utiliza para que se creen nuevas instancias cada vez que el cliente ejecute un api*/
            services.AddTransient<IAutorContext, AutorContext>();

            services.AddTransient<IAutorRepository, AutorRepository>();

            /*se trabaja cuando el cliente haga una consulta y luego se autodestruye*/
            services.AddScoped(typeof(IMongoRepository<>), typeof(MongoRepository<>));

            services.AddControllers();

            /*configuracion para que cualquier cliente ingrese a la pagina*/
            services.AddCors(opt =>
            {
                opt.AddPolicy("CorsRule", rule =>
                {
                    rule.AllowAnyHeader().AllowAnyMethod().WithOrigins("*");
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

            app.UseRouting();

            /*le pasamos el nombre de la regla creada anteriormente*/
            app.UseCors("CorsRule");

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
