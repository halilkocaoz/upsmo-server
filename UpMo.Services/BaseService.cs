using AutoMapper;
using Microsoft.Extensions.Logging;
using UpMo.Data;

namespace UpMo.Services
{
    public class BaseService
    {
        protected readonly IMapper _mapper;
        protected readonly UpMoContext _context;
        protected readonly ILogger _logger;

        public BaseService(IMapper mapper,
                           UpMoContext context,
                           ILogger logger)
        {
            _mapper = mapper;
            _context = context;
            _logger = logger;
        }

        public BaseService(IMapper mapper,
                   UpMoContext context)
        {
            _mapper = mapper;
            _context = context;
        }
    }
}