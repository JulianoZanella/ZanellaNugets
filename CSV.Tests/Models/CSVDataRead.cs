using Zanella.CSV;

namespace CSV.Tests.Models
{
    public class CSVDataRead : CSVData, ICSVObject
    {
        public int Line { get; set; }
        public IList<Exception> Exceptions { get; set; } = new List<Exception>();
    }
}
