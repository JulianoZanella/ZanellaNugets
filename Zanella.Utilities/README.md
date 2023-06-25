# Zanella.Utilities

Contains basic utilities

## Installation

Use [nuget manager](https://www.nuget.org/packages/Zanella.Utilities/) to install.

## Usage

```csharp
using Zanella.Utilities.Validation;

// Validate CPF
bool isValidCPF = DocumentValidation.IsValidCPF("000.000.000-00");

// Validate CNPJ
bool isValidCPNJ = DocumentValidation.IsValidCPNJ("00.000.000/0001-26");

// Format
string formatedCPF = DocumentFormatter.FormatCPF("12345678909");

// Use Attribute
[CPF_CNPJ_Document(EDocumentType.CPF)]
public string? CPF { get; set; }

// Validate attributes
var test = new Example()
{
    CPF = "12345678909",
};
var result = DataAnnotationsValidation.Validate(test);
var successValidation = result.IsSuccess;
```

## Contributing

Pull requests are welcome. For major changes, please open an issue first
to discuss what you would like to change.

Please make sure to update tests as appropriate.

## License

[MIT](https://choosealicense.com/licenses/mit/)
