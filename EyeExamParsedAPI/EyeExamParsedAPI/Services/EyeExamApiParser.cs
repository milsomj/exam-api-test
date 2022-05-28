using EyeExamParsedAPI.DTOs;
using System.Text;

namespace EyeExamParsedAPI.Services
{
    public class EyeExamApiParser
    {
        private const int col1Index = 0;
        private const int col2Index = 16;
        private const int col3Index = 46;
        private const int col4Index = 62;

        public List<ParsedScheduleNoticeOfLease> parseListScheduleNoticeOfLease(List<RawScheduleNoticeOfLease> rawNoticeList)
        {
            var result = new List<ParsedScheduleNoticeOfLease>();
            foreach(var l in rawNoticeList)
            {
                result.Add(parseScheduleNoticeOfLease(l));
            }

            return result;
        }

        public ParsedScheduleNoticeOfLease parseScheduleNoticeOfLease(RawScheduleNoticeOfLease rawNotice)
        {
            var regDatePlan = new StringBuilder();
            var propertyDescr = new StringBuilder();
            var dateAndTerm = new StringBuilder();
            var leseesTitle = new StringBuilder();
            var notes = new List<string>();

            //Sample
            //15.11.2018      Ground Floor Premises         10.10.2018      TGL513556  
            //0               16                            46              62              
            foreach (var entry in rawNotice.EntryText)
            {
                if (entry.Substring(0, 4) == "NOTE")
                {
                    notes.Add(entry);
                }
                else
                {
                    regDatePlan.Append(entry.Substring(col1Index, Math.Min(col2Index - col1Index,entry.Length)).Trim() + " ");
                    if (col2Index < entry.Length)
                        propertyDescr.Append(entry.Substring(col2Index, Math.Min(col3Index, entry.Length - 1) - col2Index).Trim() + " ");
                    if (col3Index < entry.Length)
                        dateAndTerm.Append(entry.Substring(col3Index, Math.Min(col4Index, entry.Length - 1) - col3Index).Trim() + " ");
                    if (col4Index < entry.Length)
                        leseesTitle.Append(entry.Substring(col4Index).Trim() + " ");
                }
            }

            return new ParsedScheduleNoticeOfLease()
            {
                EntryNumber = int.Parse(rawNotice.EntryNumber),
                EntryDate = rawNotice.EntryDate == "" ? null : DateOnly.Parse(rawNotice.EntryDate),
                RegistrationDateAndPlanRef = regDatePlan.ToString().Trim(),
                PropertyDescription = propertyDescr.ToString().Trim(),
                DateOfLeaseAndTerm = dateAndTerm.ToString().Trim(),
                LesseesTitle = leseesTitle.ToString().Trim(),
                Notes = notes
            };
        }
    }
}
