using System.Globalization;

namespace MyTradyExtension
{
    public class ExcelImportConfiguration
    {
        public string Delimiter { get; set; } = ",";
        public string DateFormat { get; set; }
        public string Culture { get; set; }
        public bool HasHeaderRecord { get; set; } = true;
        public CultureInfo CultureInfo => string.IsNullOrEmpty(Culture) ? null : CultureInfo.GetCultureInfo(Culture);
    }
}