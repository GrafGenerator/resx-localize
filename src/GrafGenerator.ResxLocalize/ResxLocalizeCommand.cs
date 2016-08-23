﻿using System;
using System.Globalization;
using System.IO;
using System.Management.Automation;
using Newtonsoft.Json.Linq;

namespace GrafGenerator.ResxLocalize
{
    [Cmdlet(VerbsCommon.New, "LocalizedResx", DefaultParameterSetName = "StringsFileInputParameterSet")]
    public class ResxLocalizeCommand : PSCmdlet
    {
        private JObject _jsonInput;

        #region Parameters

        [Parameter(Position = 0, Mandatory = true)]
        [Parameter(ParameterSetName = "StringsFileInputParameterSet")]
        [Parameter(ParameterSetName = "StringsTextInputParameterSet")]
        [ValidateNotNullOrEmpty]
        public string SourceResxPath { get; set; }


        [Parameter(Position = 1, ParameterSetName = "StringsFileInputParameterSet", Mandatory = true)]
        [ValidateNotNullOrEmpty]
        public string StringSourcePath { get; set; }

        [Parameter(Position = 1, ParameterSetName = "StringsTextInputParameterSet", Mandatory = true)]
        [ValidateNotNullOrEmpty]
        public string StringSource { get; set; }

        [Parameter(ParameterSetName = "StringsFileInputParameterSet")]
        [Parameter(ParameterSetName = "StringsTextInputParameterSet")]
        [Parameter(Position = 2, Mandatory = true)]
        [ValidateNotNullOrEmpty]
        public string SourceCulture { get; set; }

        [Parameter(Position = 3, Mandatory = false)]
        public string[] TargetCultures { get; set; }

        #endregion

        #region Overrides

        protected override void BeginProcessing()
        {
            if (!File.Exists(SourceResxPath))
            {
                ThrowTerminatingError(new ErrorRecord(new FileNotFoundException("Input RESX file not found.", SourceResxPath),
                    "FileNotFound", ErrorCategory.InvalidArgument, SourceResxPath));
            }

            if (ParameterSetName.Equals("StringsFileInputParameterSet") && !File.Exists(StringSourcePath))
            {
                ThrowTerminatingError(new ErrorRecord(new FileNotFoundException("Input strings source file not found.", StringSourcePath),
                    "FileNotFound", ErrorCategory.InvalidArgument, StringSourcePath));
            }

            if (TargetCultures == null || TargetCultures.Length == 0)
            {
                TargetCultures = new[] {"ar-AE", "cs-CZ", "el-GR", "es-CL", "kk-KZ", "ky-KG", "ru-RU", "uk-UA"};
            }

            SourceCulture = CultureInfo.CreateSpecificCulture(SourceCulture).Name;


            var stringJsonContent = ParameterSetName.Equals("StringsFileInputParameterSet")
                ? File.ReadAllText(StringSourcePath)
                : StringSource;

            if (string.IsNullOrWhiteSpace(stringJsonContent))
            {
                ThrowTerminatingError(new ErrorRecord(new InvalidOperationException("No JSON input provided."), 
                    "InvalidOperation", ErrorCategory.InvalidOperation, stringJsonContent));
            }

            _jsonInput = JObject.Parse(stringJsonContent);
        }

        protected override void ProcessRecord()
        {

        }

        #endregion
    }
}