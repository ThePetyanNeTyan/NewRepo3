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
            CreateMap<Report, ReportDto>()
                .ForCtorParam(ctorParamName:"Id", m=>m.MapFrom(s=>s.Id))
                .ForCtorParam(ctorParamName: "Name", m => m.MapFrom(s => s.Name))
                .ForCtorParam(ctorParamName: "Description", m => m.MapFrom(s => s.Description))
                .ForCtorParam(ctorParamName: "DateCreated", m => m.MapFrom(s => s.CreatedAt))
                .ReverseMap();
        }
    }
}
