using ExcelDataReader;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Trady.Core;
using Trady.Core.Infrastructure;
using Trady.Core.Period;

namespace MyTradyExtension
{
    public class ExcelImporter : IImporter
    {
        private string _path;
        private readonly CultureInfo _culture;
        private string _format;
        private string _delimiter;
        private bool _hasHeader = true;

        public ExcelImporter(string path) : this(path, CultureInfo.CurrentCulture)
        {
        }

        public ExcelImporter(string path, CultureInfo culture)
        {
            _path = path;
            _culture = culture;
        }

        public ExcelImporter(string path, ExcelImportConfiguration configuration) : this(path, configuration.CultureInfo)
        {
            _format = configuration.DateFormat;
            _delimiter = configuration.Delimiter;
            _hasHeader = configuration.HasHeaderRecord;
        }

        public async Task<IReadOnlyList<IOhlcv>> ImportAsync(string symbol, DateTime? startTime = null,
            DateTime? endTime = null, PeriodOption period = PeriodOption.Daily,
            CancellationToken token = default(CancellationToken))
            => await Task.Factory.StartNew(() =>
            {
                using (var stream = File.Open(_path, FileMode.Open, FileAccess.Read))
                {
                    using (var reader = ExcelReaderFactory.CreateReader(stream))
                    {
                        var candles = new List<IOhlcv>();

                        var result = reader.AsDataSet();

                        for (int i = 1; i < result.Tables[0].Rows.Count; i++)
                        {
                            var row = result.Tables[0].Rows[i];
                            var date = GetDate(row.Field<double>(2));
                            if ((!startTime.HasValue || date >= startTime) && (!endTime.HasValue || date <= endTime))
                                candles.Add(GetRecord(row));
                        }

                        return candles.OrderBy(c => c.DateTime).ToList();
                    }
                }
            }, token);

        private DateTime GetDate(double v)
        {
            int day = (int)v % 100;

            var month = (((int)v - day) % 10000) / 100;

            var year = (int)v / 10000;

            return new DateTime(year, month, day);
        }

        private IOhlcv GetRecord(DataRow row) => new Candle(
                GetDate(row.Field<double>(2)),
                Convert.ToDecimal(row.Field<double>(10)),
                Convert.ToDecimal(row.Field<double>(9)),
                Convert.ToDecimal(row.Field<double>(8)),
                Convert.ToDecimal(row.Field<double>(6)),
                Convert.ToDecimal(row.Field<double>(4))
            );
    }
}