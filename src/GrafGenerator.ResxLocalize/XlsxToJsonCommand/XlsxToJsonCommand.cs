using System.IO;
using System.Management.Automation;

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
                ThrowTerminatingError(new ErrorRecord(new FileNotFoundException("Input XLSX file not found.", SourceXlsxPath),
                    "FileNotFound", ErrorCategory.InvalidArgument, SourceXlsxPath));
            }
        }

        protected override void ProcessRecord()
        {

        }

        #endregion
    }
}