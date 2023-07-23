using Zanella.DocumentHelper.CSV;

namespace DocumentHelper.Test.Models
{
    public class CSVDataRead : CSVData, ICSVObject
    {
        public int Line { get; set; }
        public IList<Exception> Exceptions { get; set; } = new List<Exception>();
    }
}
