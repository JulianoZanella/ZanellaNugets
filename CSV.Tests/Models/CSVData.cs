using Zanella.CSV;

namespace CSV.Tests.Models
{
    public class CSVData
    {
        [CSV_Column(2)]
        public int Id { get; set; }

        [CSV_Column(0)]
        public string Name { get; set; } = string.Empty;

        [CSV_Column(1)]
        public string Description { get; set; } = string.Empty;

        public long IgnoredProperty { get; set; }
    }
}
