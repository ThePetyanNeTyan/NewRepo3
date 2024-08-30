using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApiTrain.Domain.Dto.Report;

namespace WebApiTrain.Application.Validations.FluentValidations.Report
{
    public class UpdateReportValidator: AbstractValidator<UpdateReportDto>
    {
        public UpdateReportValidator()
        {
            RuleFor(x => x.Id).NotEmpty();
            RuleFor(x => x.Name).NotEmpty().MaximumLength(200);
            RuleFor(x => x.Name).NotEmpty().MaximumLength(1000);
        }
       
    }
}
