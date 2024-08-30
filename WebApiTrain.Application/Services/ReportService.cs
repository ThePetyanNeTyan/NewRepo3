using Microsoft.EntityFrameworkCore;
using WebApiTrain.Domain.Entity;
using WebApiTrain.Domain.Interfaces.Repositories;
using WebApiTrain.Domain.Interfaces.Services;
using WebApiTrain.Domain.Result;
using Serilog;
using WebApiTrain.Application.Resources;
using WebApiTrain.Domain.Enum;
using WebApiTrain.Domain.Dto.Report;
using WebApiTrain.Domain.Interfaces.Validations;
using AutoMapper;

namespace WebApiTrain.Application.Services
{
    public class ReportService : IReportService
    {
        private readonly IBaseRepository<Report> _reportRepository;
        private readonly ILogger _logger;
        private readonly IReportValidator _reportValidator;
        private readonly IBaseRepository<User> _userRepository;
        private readonly IMapper _mapper;

        public ReportService(IBaseRepository<Report> reportRepository, IBaseRepository<User> userRepository, ILogger logger,
            IReportValidator reportValidator, IMapper mapper)
        {
            _reportRepository = reportRepository;
            _userRepository = userRepository;
            _logger = logger;
            _reportValidator = reportValidator;
            _mapper = mapper;
        }

        ///<inheritdoc/>
        public async Task<BaseResult<ReportDto>> CreateReportAsync(CreateReportDto dto)
        {
            try
            {
                var user = await _userRepository.GetAll().FirstOrDefaultAsync(x => x.Id == dto.UserId);            
                var report = await _reportRepository.GetAll().FirstOrDefaultAsync(x => x.Name == dto.Name);
                var result = _reportValidator.CreateValidator(report, user);
                if (!result.IsSuccess)
                {
                    return new BaseResult<ReportDto>()
                    {
                        ErrorMessage = result.ErrorMessage,
                        ErrorCode = result.ErrorCode
                    };
                }

                report = new Report()
                {
                    Name = dto.Name,
                    Description = dto.Description,
                    UserId = user.Id
                };
                await _reportRepository.CreateAsync(report);
                return new BaseResult<ReportDto>()
                {
                    Data = _mapper.Map<ReportDto>(report)
                };
            }
            catch (Exception ex) 
            {
                _logger.Error(ex, ex.Message);
                return new BaseResult<ReportDto>()
                {
                    ErrorMessage = ErrorMessage.InternalServerError,
                    ErrorCode = (int)ErrorCodes.InternalServerError
                };
            }
        }

        ///<inheritdoc/>
        public async Task<BaseResult<ReportDto>> DeleteReportAsync(long id)
        {
            try
            {
                var report = await _reportRepository.GetAll().FirstOrDefaultAsync(x => x.Id == id);
                var result = _reportValidator.ValidateOnNull(report);
                if (!result.IsSuccess)
                {
                    return new BaseResult<ReportDto>()
                    {
                        ErrorMessage = result.ErrorMessage,
                        ErrorCode = result.ErrorCode
                    };
                }

                await _reportRepository.RemoveAsync(report);
                return new BaseResult<ReportDto>()
                {
                    Data = _mapper.Map<ReportDto>(report)
                };
            }
            catch (Exception ex)
            {
                _logger.Error(ex, ex.Message);
                return new BaseResult<ReportDto>()
                {
                    ErrorMessage = ErrorMessage.InternalServerError,
                    ErrorCode = (int)ErrorCodes.InternalServerError
                };
            }
        }

        ///<inheritdoc/>
        public Task<BaseResult<ReportDto>> GetReportByIdAsync(long id)
        {
            ReportDto? report;

            try
            {
                report = _reportRepository.GetAll()
                    .AsEnumerable()
                    .Select(x => new ReportDto(x.Id, x.Name, x.Description, x.CreatedAt.ToLongDateString()))
                    .FirstOrDefault(x => x.Id == id);

            }
            catch (Exception ex) 
            {
                _logger.Error(ex, ex.Message);
                return Task.FromResult(new BaseResult<ReportDto>()
                {
                    ErrorMessage = ErrorMessage.InternalServerError,
                    ErrorCode = (int)ErrorCodes.InternalServerError
                });
            }

            if (report == null) 
            {
                _logger.Warning($"Отчет с {id} не найден", id);
                return Task.FromResult(new BaseResult<ReportDto>()
                {
                    ErrorMessage = ErrorMessage.ReportNotFound,
                    ErrorCode = (int)ErrorCodes.ReportNotFound
                });
            }

            return Task.FromResult(new BaseResult<ReportDto>()
            {
                Data = report
            });
        }

        ///<inheritdoc/>
        public async Task<CollectionResuslt<ReportDto>> GetReportsAsync(long userId)
        {
            ReportDto[] reports;

            try
            {
                reports = await _reportRepository.GetAll()
                    .Where(x => x.UserId == userId)
                    .Select(x => new ReportDto(x.Id, x.Name, x.Description, x.CreatedAt.ToLongDateString()))
                    .ToArrayAsync();
            }
            catch(Exception ex) 
            {
                _logger.Error(ex, ex.Message);
                return new CollectionResuslt<ReportDto>()
                {
                    ErrorMessage = ErrorMessage.InternalServerError,
                    ErrorCode = (int)ErrorCodes.InternalServerError
                };
            }

            if (!reports.Any())
            {
                _logger.Warning(ErrorMessage.ReportsNotFound, reports.Length);
                return new CollectionResuslt<ReportDto>()
                {
                    ErrorMessage = ErrorMessage.ReportsNotFound,
                    ErrorCode = (int)ErrorCodes.ReportsNotFound
                };
            }

            return new CollectionResuslt<ReportDto>()
            {
                Data = reports,
                Count = reports.Length
            };
        }

        public async Task<BaseResult<ReportDto>> UpdateReportAsync(UpdateReportDto dto)
        {
            try
            {
                var report = await _reportRepository.GetAll().FirstOrDefaultAsync(x => x.Id == dto.Id);
                var result = _reportValidator.ValidateOnNull(report);
                if (!result.IsSuccess)
                {
                    return new BaseResult<ReportDto>()
                    {
                        ErrorMessage = result.ErrorMessage,
                        ErrorCode = result.ErrorCode
                    };
                }

                report.Name = dto.Name;
                report.Description = dto.Description;

                await _reportRepository.UpdateAsync(report);

                return new BaseResult<ReportDto>()
                {
                    Data = _mapper.Map<ReportDto>(report)
                };

            }
            catch (Exception ex)
            {
                _logger.Error(ex, ex.Message);
                return new BaseResult<ReportDto>()
                {
                    ErrorMessage = ErrorMessage.InternalServerError,
                    ErrorCode = (int)ErrorCodes.InternalServerError
                };
            }
        }
    }
}
