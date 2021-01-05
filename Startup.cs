using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using CosmoResearch.Services;
using CosmoResearch.Settings;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.Azure.Cosmos.Table;
using CosmoResearch.GraphQL.Node;

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

            services.AddSingleton<TableService>();

            services.AddGrpc();

            services
                .AddGraphQLServer()
                .AddQueryType(d => d.Name("Query"))
                    .AddTypeExtension<NodeQuery>()
                .AddDataLoader<NodeByKeyDataLoader>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGrpcService<DataUploadService>();

                endpoints.MapGraphQL();
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
