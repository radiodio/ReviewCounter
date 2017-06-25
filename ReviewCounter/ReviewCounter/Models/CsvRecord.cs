using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReviewCounter.Models
{
    public class CsvRecord
    {
        public string Project { get; set; }
        public string Version { get; set; }
        public int Backlog { get; set; }
        public string ProcessOutput { get; set; }
        public string Reviewee { get; set; }
        public string Reviewer { get; set; }
        public string Date { get; set; }
        public int Time { get; set; }

        static public CsvRecord ConvertToCsvRecord(ReviewTime rt)
        {
            var csvRecord = new CsvRecord();
            csvRecord.Project = rt.Review.Project.Name;
            csvRecord.Version = rt.Review.Version.Name;
            csvRecord.Backlog = rt.Review.Ticket;
            csvRecord.ProcessOutput = rt.Review.Output.ProcessOutput;
            csvRecord.Reviewee = rt.Review.Author.Name;
            csvRecord.Reviewer = rt.Member.Name;
            csvRecord.Date = rt.Date.ToString();
            csvRecord.Time = rt.Time;

            return csvRecord;
        }

        static public List<CsvRecord> ConvertToCsvRecord(List<ReviewTime> list)
        {
            var csvList = list.Select(item => ConvertToCsvRecord(item)).ToList();
            return csvList;
        }
    }
}
