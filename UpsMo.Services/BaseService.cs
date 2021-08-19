using AutoMapper;
using Microsoft.Extensions.Logging;
using UpsMo.Data;

namespace UpsMo.Services
{
    public class BaseService
    {
        protected readonly IMapper _mapper;
        protected readonly UpsMoContext _context;
        protected readonly ILogger _logger;

        public BaseService(IMapper mapper,
                           UpsMoContext context,
                           ILogger logger)
        {
            _mapper = mapper;
            _context = context;
            _logger = logger;
        }

        public BaseService(IMapper mapper,
                   UpsMoContext context)
        {
            _mapper = mapper;
            _context = context;
        }
    }
}