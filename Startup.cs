using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using CosmoResearch.Services;
using CosmoResearch.Settings;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.Azure.Cosmos.Table;
using CosmoResearch.GraphQL.Data;
using HotChocolate.Types.Descriptors;
using CosmoResearch.GraphQL.Common;

namespace CosmoResearch
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            ConfigureTableDb(services);

            services.AddSingleton<DataService>();

            services.AddSingleton<ITypeInspector, InheritanceAwareTypeInspector>();

            services
                .AddGraphQLServer()
                .AddQueryType(d => d.Name("Query"))
                    .AddTypeExtension<DataQuery>()
                .AddDataLoader<DataByKeyDataLoader>()
                .AddType<DataType>();

            services.AddGrpc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();
            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGraphQL();

                endpoints.MapGrpcService<DataUploadService>();
            });
        }

        private void ConfigureTableDb(IServiceCollection services) 
        {
            var section = Configuration.GetSection(nameof(DatabaseSettings));

            services.Configure<DatabaseSettings>(section);
            
            services.AddSingleton<IDatabaseSettings>(sp =>
                sp.GetRequiredService<IOptions<DatabaseSettings>>().Value
            );

            services.AddSingleton<CloudTable>(sp => 
                DatabaseUtils.CreateTable(section.Get<DatabaseSettings>())
            );
            
        }
    }
}
