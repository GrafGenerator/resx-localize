# resx-localize
A set of two Powershell cmdlets for localizing RESX file from strings source:
* **New-LocalizedResx** - takes RESX file and strings source and performing RESX localization.
* **Convert-XlsxToJson** - takes specially formatted XLSX file with strings and converts it to JSON strings file.  

## Input format
* RESX file - usual RESX file which will be localized. String values from this files used as keys to search translations.
* JSON file - strings source in JSON format. This file must contain JS array with object, each object should contain in format `"culture": "translation"`.

JSON file sample:
```json
[
    {
        "en": "1",
        "ru": "2",
        "cn": "3",
    },
    {
        "en": "4",
        "ru": "5",
        "cn": "6",
    }
]
``` 

## New-LocalizedResx
|Position|Parameter|Required|Description|Default value|
|---|---|---|---|---|
|0|SourceResxFile|yes|Path to source RESX file| - |
|1|SourceStringsFile *or* SourceStrings|yes|Path to JSON file with strings *or* JSON string| - |
|2|SourceCulture|yes|Source culture key, which will be used to match strings from RESX with strings from strings file| - |
|3|TargetCultures|no|target cultures keys, which will be used to geenrate localized RESX files| `@("ar-AE","cs-CZ","el-GR","es-CL","kk-KZ","ky-KG","ru-RU","uk-UA")`|

Given input *{filename}*.resx, this cmdlet produces localized *{filename}*.*{culture}*.resx for each specified target culture in the same folder as the original RESX file.
Note that this cmdlet remove any duplicate strings keys (source culture) from strings file before processing.

## Copy-XlsxToJson
|Position|Parameter|Required|Description|Default value|
|---|---|---|---|---|
|0|SourceXlsxFile|yes|Path to source XLSX file| - |

Given input *{filename}*.xlsx, this cmdlet produces strings source file *{filename}*.strings.json.

XLSX file can contain one or more sheets with strings, data on each sheet must start from A1 cell.
First row must contain culture codes (keys), first empty cell in the row treated as end of columns.
Each row below the first row, that has at least one non-empty cell will be transformed to appropriate JSON object.
First row with no data treated as end of rows.

## Instalation
1. Clone repository and open *build* folder
2. Run *build-and-deploy.ps1*.
3. Open new Powershell window - functions should be loaded and ready for use.