# Zanella.CSV

Contains utilities for Documents

## Installation

Use [nuget manager](https://www.nuget.org/packages/Zanella.DocumentHelper/) to install.

## Usage

```csharp

using Zanella.DocumentHelper.CSV;

public class CSVData : ICSVObject
{
    [CSV_Column(2)]
    public int Id { get; set; }

    [CSV_Column(0)]
    public string Name { get; set; } = string.Empty;

    [CSV_Column(1)]
    public string Description { get; set; } = string.Empty;

    public long IgnoredProperty { get; set; }

    public int Line { get; set; }

    public IList<Exception> Exceptions { get; set; } = new List<Exception>();
}

// Read content to object list
var csvContent = @"Joe Test;description 1;1
Mari Test;description 2;2";
var list = Helper.Read<CSVData>(csvContent);

// Create CSV content from objects
csvContent = Helper.Write(list);

// Or use instance for same data objects:
var csvHelper = new Helper<CSVDataRead>();
var firstList = csvHelper.Read(firstContent);
var secondList = csvHelper.Read(secondContent);

```

## Contributing

Pull requests are welcome. For major changes, please open an issue first
to discuss what you would like to change.

Please make sure to update tests as appropriate.

## License

[MIT](https://choosealicense.com/licenses/mit/)
