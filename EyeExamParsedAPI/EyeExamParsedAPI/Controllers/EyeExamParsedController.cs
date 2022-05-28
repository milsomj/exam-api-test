using EyeExamParsedAPI.Interfaces;
using EyeExamParsedAPI.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace EyeExamParsedAPI.Controllers
{
    


    [Route("api/[controller]")]
    [ApiController]
    public class EyeExamParsedController : ControllerBase
    {
        private readonly IConfiguration Configuration;
        private readonly IMemoryCache MemoryCache;
        private IEyeExamRepository EyeExamRepository;
        public List<ParsedScheduleNoticeOfLease> ResolvedRequest;
        
        public EyeExamParsedController(IConfiguration configuration, IEyeExamRepository eyeExamRepository, IMemoryCache memoryCache)
        {
            Configuration = configuration;
            EyeExamRepository = eyeExamRepository;
            MemoryCache = memoryCache;
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet(Name = "ParsedScheduleNoticeOfLease")]
        public List<ParsedScheduleNoticeOfLease> ParsedScheduleNoticeOfLease()
        {

            var result = new List<ParsedScheduleNoticeOfLease>();

            if (!MemoryCache.TryGetValue("KEY",out result))
            {
                result = EyeExamRepository.GetParsedScheduleList();

                var cacheEntryOptions = new MemoryCacheEntryOptions()
                    .SetSlidingExpiration(TimeSpan.FromHours(1));

                MemoryCache.Set("KEY", result, cacheEntryOptions);
            }

            return result;
        }
    }
}
