using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApiTrain.Application.Mapping;
using WebApiTrain.Application.Services;
using WebApiTrain.Application.Validations;
using WebApiTrain.Application.Validations.FluentValidations.Report;
using WebApiTrain.Domain.Dto.Report;
using WebApiTrain.Domain.Interfaces.Services;
using WebApiTrain.Domain.Interfaces.Validations;

namespace WebApiTrain.Application.DependencyInjection
{
    public static class DependencyInjection
    {
        public static void AddApplication(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(ReportMapping));
            InitServices(services);
        }

        private static void InitServices(this IServiceCollection services)
        {
            services.AddScoped<IReportValidator, ReportValidator>();
            services.AddScoped<IValidator<CreateReportDto>, CreateReportValidator>();
            services.AddScoped<IValidator<UpdateReportDto>, UpdateReportValidator>();
            services.AddScoped<IReportService, ReportService>();
        }
    }
}
