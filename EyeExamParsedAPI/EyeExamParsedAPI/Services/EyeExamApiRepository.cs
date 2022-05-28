using EyeExamParsedAPI.Interfaces;
using EyeExamParsedAPI.DTOs;
using System.Text;

namespace EyeExamParsedAPI.Services
{
    public class EyeExamApiRepository : IEyeExamRepository
    {
        static HttpClient client = new HttpClient();
        private readonly IConfiguration Configuration;

        #region Constants
        public const string AUTH_USERNAME = "APIUserName";
        public const string AUTH_PASSWORD = "APIPassword";
        public const string CONFIG_HOST = "EyeExamApiHost";
        public const string CONFIG_PORT = "EyeExamApiPort";
        public const string CONFIG_SCHEME = "EyeExamApiScheme";
        public const string CONFIG_PATH = "EyeExamApiPath";
        #endregion

        public EyeExamApiRepository(IConfiguration configuration)
        {
            Configuration = configuration;
            var byteArray = Encoding.ASCII.GetBytes(Configuration[AUTH_USERNAME]+":"+ Configuration[AUTH_PASSWORD]);
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));
        }

        public List<RawScheduleNoticeOfLease> GetRawSchedule()
        {
            var uri = new UriBuilder();
            uri.Host = Configuration[CONFIG_HOST];
            uri.Port = int.Parse(Configuration[CONFIG_PORT]);
            uri.Scheme = Configuration[CONFIG_SCHEME];
            uri.Path = Configuration[CONFIG_PATH];

            return GetRawScheduleAsync(uri.ToString()).GetAwaiter().GetResult();
        }

        public List<ParsedScheduleNoticeOfLease> GetParsedScheduleList()
        {
            var parser = new EyeExamApiParser();
            return parser.parseListScheduleNoticeOfLease(GetRawSchedule());
        }

        static async Task<List<RawScheduleNoticeOfLease>> GetRawScheduleAsync(string path)
        {
            var rawSchedList = new List<RawScheduleNoticeOfLease>();

            HttpResponseMessage response = await client.GetAsync(path);
            if (response.IsSuccessStatusCode)
            {
                rawSchedList = await response.Content.ReadFromJsonAsync<List<RawScheduleNoticeOfLease>>();
            }
            return rawSchedList;
        }
    }
}
