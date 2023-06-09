using DogsHouse.DAL.Persistance;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using DogsHouse.DAL.Repositories.Wrapper;
using DogsHouse.BLL.DTO.Dogs;
using AspNetCoreRateLimit;

namespace DogsHouse.WebApi.Extensions
{
    public static class ServiceConfigurationExtensions
    {
        public static void AddServices(this IServiceCollection services, ConfigurationManager configuration)
        {
            services.AddScoped<IRepositoriesWrapper, RepositoriesWrapper>();
            services.AddDbContext<DogsHouseDBContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"), opt =>
                {
                    opt.MigrationsAssembly(typeof(DogsHouseDBContext).Assembly.GetName().Name)
                       .MigrationsHistoryTable("__EFMigrationsHistory", schema: "entity_framework");
                });
            });
            services.AddControllers();
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(AppDomain.CurrentDomain.GetAssemblies())
                                          .RegisterServicesFromAssemblyContaining<DogDTO>());
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

        }

        public static void ConfigureRateLimiter(this IServiceCollection services, ConfigurationManager configuration)
        {
            services.AddMemoryCache();
            services.Configure<IpRateLimitOptions>(options =>
            {
                options.EnableEndpointRateLimiting = false;
                options.StackBlockedRequests = false;
                options.HttpStatusCode = StatusCodes.Status429TooManyRequests;
                options.GeneralRules = new List<RateLimitRule>
                {
                    new RateLimitRule
                    {
                        Endpoint = configuration["RateLimitRuleProps:Endpoint"],
                        Period = configuration["RateLimitRuleProps:Period"],
                        Limit = double.Parse(configuration["RateLimitRuleProps:Limit"]),
                    }
                };
                
            });
            services.AddSingleton<IIpPolicyStore, MemoryCacheIpPolicyStore>();
            services.AddSingleton<IRateLimitCounterStore, MemoryCacheRateLimitCounterStore>();
            services.AddSingleton<IRateLimitConfiguration, RateLimitConfiguration>();
            services.AddSingleton<IProcessingStrategy, AsyncKeyLockProcessingStrategy>();
            services.AddInMemoryRateLimiting();
        }
    
        public static void AddSwaggerService(this IServiceCollection services)
        {
            services.AddSwaggerGen(opt =>
            {
                opt.SwaggerDoc("v1", new OpenApiInfo { Title = "MyApi", Version = "v1" });

                opt.CustomSchemaIds(x => x.FullName);
            });
        }
    }
}
