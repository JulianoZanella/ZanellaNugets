using Zanella.DocumentHelper.CSV;

namespace DocumentHelper.Test.Models
{
    public class CSVData
    {
        [CSV_Column(2, Header = "Code")]
        public int Id { get; set; }

        [CSV_Column(0, Header = "Name")]
        public string Name { get; set; } = string.Empty;

        [CSV_Column(1, Header = "Description")]
        public string Description { get; set; } = string.Empty;

        public long IgnoredProperty { get; set; }
    }
}
