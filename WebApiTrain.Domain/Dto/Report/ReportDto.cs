﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApiTrain.Domain.Dto.Report
{
    public record ReportDto(long Id, string Name, string Description, string DateCreated);
}