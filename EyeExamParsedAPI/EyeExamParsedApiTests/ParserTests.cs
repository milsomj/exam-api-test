using Xunit;
using EyeExamParsedAPI.Services;
using EyeExamParsedAPI.DTOs;

namespace EyeExamParsedApiTests
{
    public class ParserTests
    {
        private EyeExamApiParser parser;
        public ParserTests()
        {
            parser = new EyeExamApiParser();
        }

        #region TestData
        RawScheduleNoticeOfLease rawDataNoNotes = new RawScheduleNoticeOfLease()
        {
            EntryNumber = "1",
            EntryDate = "",
            EntryType = "Schedule of Notices of Leases",
            EntryText = new List<string>() {
                    "09.07.2009      Endeavour House, 47 Cuba      06.07.2009      EGL557357  ",
                    "Edged and       Street, London                125 years from             ",
                    "numbered 2 in                                 1.1.2009                   ",
                    "blue (part of)"
                }
        };

        RawScheduleNoticeOfLease rawDataWithNotes = new RawScheduleNoticeOfLease()
        {
            EntryNumber = "4",
            EntryDate = "",
            EntryType = "Schedule of Notices of Leases",
            EntryText = new List<string>() {
                   "24.07.1989      17 Ashworth Close (Ground     01.06.1989      TGL24029   ",
                    "Edged and       and First Floor Flat)         125 years from             ",
                    "numbered 19                                   1.6.1989                   ",
                    "(Part of) in                                                             ",
                    "brown                                                                    ",
                    "NOTE 1: A Deed of Rectification dated 7 September 1992 made between (1) Orbit Housing Association and (2) John Joseph McMahon Nellie Helen McMahon and John George McMahon is supplemental to the Lease dated 1 June 1989 of 17 Ashworth Close referred to above. The lease actually comprises the second floor flat numbered 24 (Part of) on the filed plan. (Copy Deed filed under TGL24029)",
                    "NOTE 2: By a Deed dated 23 May 1996 made between (1) Orbit Housing Association (2) John Joseph McMahon Nellie Helen McMahon and John George McMahon and (3) Britannia Building Society the terms of the lease were varied. (Copy Deed filed under TGL24029).",
                    "NOTE 3: A Deed dated 13 February 1997 made between (1) Orbit Housing Association (2) John Joseph McMahon and others and (3) Britannia Building Society is supplemental to the lease. It substitutes a new plan for the original lease plan. (Copy Deed filed under TGL24029)"
                }
        };

        ParsedScheduleNoticeOfLease parsedDataNoNotes = new ParsedScheduleNoticeOfLease()
        {
            EntryNumber = 1,
            EntryDate = null,
            RegistrationDateAndPlanRef = "09.07.2009 Edged and numbered 2 in blue (part of)",
            PropertyDescription = "Endeavour House, 47 Cuba Street, London",
            DateOfLeaseAndTerm = "06.07.2009 125 years from 1.1.2009",
            LesseesTitle = "EGL557357"
        };

        ParsedScheduleNoticeOfLease parsedDataWithNotes = new ParsedScheduleNoticeOfLease()
        {
            EntryNumber = 4,
            EntryDate = null,
            RegistrationDateAndPlanRef = "24.07.1989 Edged and numbered 19 (Part of) in brown",
            PropertyDescription = "17 Ashworth Close (Ground and First Floor Flat)",
            DateOfLeaseAndTerm = "01.06.1989 125 years from 1.6.1989",
            LesseesTitle = "TGL24029",
            Notes = new List<string>() {
                "NOTE 1: A Deed of Rectification dated 7 September 1992 made between (1) Orbit Housing Association and (2) John Joseph McMahon Nellie Helen McMahon and John George McMahon is supplemental to the Lease dated 1 June 1989 of 17 Ashworth Close referred to above. The lease actually comprises the second floor flat numbered 24 (Part of) on the filed plan. (Copy Deed filed under TGL24029)",
                "NOTE 2: By a Deed dated 23 May 1996 made between (1) Orbit Housing Association (2) John Joseph McMahon Nellie Helen McMahon and John George McMahon and (3) Britannia Building Society the terms of the lease were varied. (Copy Deed filed under TGL24029).",
                "NOTE 3: A Deed dated 13 February 1997 made between (1) Orbit Housing Association (2) John Joseph McMahon and others and (3) Britannia Building Society is supplemental to the lease. It substitutes a new plan for the original lease plan. (Copy Deed filed under TGL24029)"
            }
        };
        #endregion

        [Fact]
        public void TestParseNoNotes()
        {
            //When
            var parsed = parser.parseScheduleNoticeOfLease(rawDataNoNotes);

            //Then
            Assert.Equal(parsedDataNoNotes.EntryNumber, parsed.EntryNumber);
            Assert.Equal(parsedDataNoNotes.EntryDate, parsed.EntryDate);
            Assert.Equal(parsedDataNoNotes.RegistrationDateAndPlanRef, parsed.RegistrationDateAndPlanRef);
            Assert.Equal(parsedDataNoNotes.PropertyDescription, parsed.PropertyDescription);
            Assert.Equal(parsedDataNoNotes.DateOfLeaseAndTerm, parsed.DateOfLeaseAndTerm);
            Assert.Equal(parsedDataNoNotes.LesseesTitle, parsed.LesseesTitle);
            Assert.Equal(parsedDataNoNotes.Notes, parsed.Notes);
        }

        [Fact]
        public void TestParse()
        {
            //When
            var parsed = parser.parseScheduleNoticeOfLease(rawDataWithNotes);

            //Then
            Assert.Equal(parsedDataWithNotes.EntryNumber, parsed.EntryNumber);
            Assert.Equal(parsedDataWithNotes.EntryDate, parsed.EntryDate);
            Assert.Equal(parsedDataWithNotes.RegistrationDateAndPlanRef, parsed.RegistrationDateAndPlanRef);
            Assert.Equal(parsedDataWithNotes.PropertyDescription, parsed.PropertyDescription);
            Assert.Equal(parsedDataWithNotes.DateOfLeaseAndTerm, parsed.DateOfLeaseAndTerm);
            Assert.Equal(parsedDataWithNotes.LesseesTitle, parsed.LesseesTitle);
            Assert.Equal(parsedDataWithNotes.Notes, parsed.Notes);
        }
    }
}