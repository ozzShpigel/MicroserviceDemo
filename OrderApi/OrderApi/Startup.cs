using FluentValidation;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using OrderApi.Data.Database;
using OrderApi.Data.Repository.v1;
using OrderApi.Domain.Entities;
using OrderApi.Messaging.Receive.Options.v1;
using OrderApi.Messaging.Receive.Receiver.v1;
using OrderApi.Models.v1;
using OrderApi.Service.v1.Command;
using OrderApi.Service.v1.Query;
using OrderApi.Service.v1.Services;
using OrderApi.Validators.v1;
using System.Reflection;

namespace OrderApi
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
            services.AddHealthChecks();
            services.AddOptions();

            var serviceClientSettingsConfig = Configuration.GetSection("RabbitMq");
            var serviceClientSettings = serviceClientSettingsConfig.Get<RabbitMqConfiguration>();
            services.Configure<RabbitMqConfiguration>(serviceClientSettingsConfig);

            bool.TryParse(Configuration["BaseServiceSettings:UseInMemoryDatabase"], out var useInMemory);

            if (!useInMemory)
            {
                services.AddDbContext<OrderContext>(options =>
                {
                    options.UseSqlServer(Configuration.GetConnectionString("OrderDatabase"), b => b.MigrationsAssembly("OrderApi"));
                });
            }
            else
            {
                services.AddDbContext<OrderContext>(options => options.UseInMemoryDatabase("data-in-memory"));
            }

            services.AddAutoMapper(typeof(Startup));

            services.AddMvc().AddFluentValidation();

            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = actionContext =>
                {
                    var actionExecutingContext =
                        actionContext as ActionExecutingContext;

                    if (actionContext.ModelState.ErrorCount > 0
                        && actionExecutingContext?.ActionArguments.Count == actionContext.ActionDescriptor.Parameters.Count)
                    {
                        return new UnprocessableEntityObjectResult(actionContext.ModelState);
                    }

                    return new BadRequestObjectResult(actionContext.ModelState);
                };
            });
            var ass = Assembly.GetExecutingAssembly();
            services.AddMediatR(Assembly.GetExecutingAssembly(), typeof(ICustomerNameUpdateService).Assembly);

            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped<IOrderRepository, OrderRepository>();

            services.AddTransient<IValidator<OrderModel>, OrderModelValidator>();

            services.AddTransient<IRequestHandler<GetPaidOrderQuery, List<Order>>, GetPaidOrderQueryHandler>();
            services.AddTransient<IRequestHandler<GetOrderByIdQuery, Order>, GetOrderByIdQueryHandler>();
            services.AddTransient<IRequestHandler<GetOrderByCustomerGuidQuery, List<Order>>, GetOrderByCustomerGuidQueryHandler>();
            services.AddTransient<IRequestHandler<CreateOrderCommand, Order>, CreateOrderCommandHandler>();
            services.AddTransient<IRequestHandler<PayOrderCommand, Order>, PayOrderCommandHandler>();
            services.AddTransient<IRequestHandler<UpdateOrderCommand>, UpdateOrderCommandHandler>();
            services.AddTransient<ICustomerNameUpdateService, CustomerNameUpdateService>();

            if (serviceClientSettings.Enabled)
            {
                services.AddHostedService<CustomerFullNameUpdateReceiver>();
            }
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();

                bool.TryParse(Configuration["BaseServiceSettings:UseInMemoryDatabase"], out var useInMemory);

                if (!useInMemory)
                {
                    using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
                    {
                        var context = serviceScope.ServiceProvider.GetRequiredService<OrderContext>();
                        context.Database.Migrate();
                    }
                }
            }
            else
            {
                app.UseHsts();

                using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
                {
                    var context = serviceScope.ServiceProvider.GetRequiredService<OrderContext>();
                    context.Database.Migrate();
                }
            }

            app.UseHttpsRedirection();
            app.UseSwagger();
            app.UseSwaggerUI();
            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHealthChecks("/health");
            });
        }
    }
}
