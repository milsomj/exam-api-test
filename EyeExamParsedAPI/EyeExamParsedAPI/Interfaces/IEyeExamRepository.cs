using EyeExamParsedAPI.DTOs;


namespace EyeExamParsedAPI.Interfaces
{
    public interface IEyeExamRepository
    {
        public List<ParsedScheduleNoticeOfLease> GetParsedScheduleList();
    }
}