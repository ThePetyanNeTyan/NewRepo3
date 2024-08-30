using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApiTrain.Domain.Dto.Report;
using WebApiTrain.Domain.Entity;

namespace WebApiTrain.Application.Mapping
{
    public class ReportMapping: Profile
    {
        public ReportMapping() 
        {
            CreateMap<Report, ReportDto>().ReverseMap();
        }
    }
}
