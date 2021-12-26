using System.Linq;
using API.Error;
using Core.Iterfaces;
using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace API.Extensions
{
    public static class ApplicationServicesExtions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services){
             services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped(typeof(IGenericRepository<>), typeof(Infrastructure.Data.GenericRepository<>));
            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = actionContext =>
                  {
                      var errors = actionContext.ModelState.Where(e => e.Value.Errors.Count > 0)
                      .SelectMany(x => x.Value.Errors).Select(x => x.ErrorMessage).ToArray();
                      var errorResponse = new ValidationErrorResponse
                      {
                          Errors = errors,
                      };
                      return new BadRequestObjectResult(errorResponse);
                  };
            });
            return services;
        }
    }
}