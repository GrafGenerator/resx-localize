using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Management.Automation;
using ClosedXML.Excel;
using Newtonsoft.Json.Linq;

namespace GrafGenerator.ResxLocalize.XlsxToJsonCommand
{
    [Cmdlet(VerbsData.Convert, "XlsxToJson", DefaultParameterSetName = "DefaultParameterSet")]
    public class XlsxToJsonCommand : PSCmdlet
    {
        #region Parameters

        [Parameter(Position = 0, ParameterSetName = "DefaultParameterSet", Mandatory = true)]
        [ValidateNotNullOrEmpty]
        public string SourceXlsxPath { get; set; }

        #endregion

        #region Overrides

        protected override void BeginProcessing()
        {
            if (!File.Exists(SourceXlsxPath))
            {
                ThrowTerminatingError(
                    new ErrorRecord(new FileNotFoundException("Input XLSX file not found.", SourceXlsxPath),
                        "FileNotFound", ErrorCategory.InvalidArgument, SourceXlsxPath));
            }
        }

        protected override void ProcessRecord()
        {
            var workbook = new XLWorkbook(SourceXlsxPath);
            var strings = workbook.Worksheets.Select(GatherStringsFromWorksheet).SelectMany(w => w);

            var array = new JArray();

            foreach (var s in strings)
            {
                var o = new JObject();

                foreach (var kv in s)
                {
                    o.Add(kv.Key, new JValue(kv.Value));
                }

                array.Add(o);
            }

            var fileName = Path.GetFileNameWithoutExtension(SourceXlsxPath);
            var outputPath = fileName + ".strings.json";

            File.WriteAllText(outputPath, array.ToString());
        }

        #endregion

        #region Helpers

        private Dictionary<string, string>[] GatherStringsFromWorksheet(IXLWorksheet worksheet)
        {
            var keys = GetKeys(worksheet).ToArray();
            var keysCount = keys.Length;
            var rows = new List<string[]>();
            var currentRow = 2;

            var row = GetRow(worksheet, currentRow, keysCount).ToArray();

            while (row.HasValues())
            {
                rows.Add(row);
                currentRow++;
                row = GetRow(worksheet, currentRow, keysCount).ToArray();
            }

            return rows
                .Select(a => a
                    .Select((c, i) => new {Key = keys[i], Value = c})
                    .ToDictionary(r => r.Key, r => r.Value)
                )
                .ToArray();
        }

        private IEnumerable<string> GetKeys(IXLWorksheet worksheet)
        {
            var cell = worksheet.Cell(1, 1);
            while (!string.IsNullOrEmpty(cell.Value.ToString()))
            {
                yield return cell.Value.ToString();
                cell = cell.CellRight();
            }
        }

        private IEnumerable<string> GetRow(IXLWorksheet worksheet, int row, int cellCount)
        {
            var cell = worksheet.Cell(row, 1);
            var i = cellCount;

            while (i > 0)
            {
                yield return cell.Value.ToString();
                cell = cell.CellRight();
                i--;
            }
        }

        #endregion
    }
}