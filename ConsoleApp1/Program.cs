﻿using ExcelDataReader;
using MyTradyExtension;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trady.Analysis;
using Trady.Analysis.Extension;
using Trady.Core.Infrastructure;
using System.Data;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            string _path = "GOST1-a.xls";
            var t = new List<RowData>();
            using (var stream = File.Open(_path, FileMode.Open, FileAccess.Read))
            {
                using (var reader = ExcelReaderFactory.CreateReader(stream))
                {
                    var candles = new List<IOhlcv>();

                    var result = reader.AsDataSet();
                    
                    for (int i = 1; i < result.Tables[0].Rows.Count; i++)
                    {
                        var row = result.Tables[0].Rows[i];
                       
                        t.Add(GetRecord(row));
                    }
                }
            }
            //var rozes = new List<Roz>();
            var namad = new Namad() {Code=t.First().Code,Name=t.First().Namad };
            var moamele = new List<Moamelat>();

            foreach (var item in t)
            {
                var r = new Roz() { Miladi = item.Miladi, Shamsi = item.Shamsi, Step = "Day" };

                moamele.Add(new Moamelat() {
                    Arzesh = item.ArzeshMoamele,
                    Close = item.close,
                    High = item.hight,
                    Last = item.Last,
                    Low = item.low,
                    Namad = namad,
                    Open = item.Open,
                    Roz = r,
                    Tedad = item.TedadMoamele,
                    Vol = item.vol
                });
            }

            using (var db = new Context())
            {
               
                db.Moamelats.AddRange(moamele);
                db.SaveChanges();

                var d = db.Moamelats.ToList();
                Console.WriteLine(t.Count);
            }
            //Indicator();

            //Rules();

            //Strategy();

            Console.ReadKey();
        }

        private static RowData GetRecord(DataRow row)
        {
            var rr = new RowData();
            //{
            rr.Namad = row.Field<string>(0);
            rr.Miladi = row.Field<string>(1);
            rr.Open = decimal.Parse(row.Field<string>(2));
            rr.hight = decimal.Parse(row.Field<string>(3));
            rr.low = decimal.Parse(row.Field<string>(4));
            rr.close = decimal.Parse(row.Field<string>(5));
            rr.vol = decimal.Parse(row.Field<string>(6));
            rr.ArzeshMoamele = decimal.Parse(row.Field<string>(7));
            rr.TedadMoamele = decimal.Parse(row.Field<string>(8));
            rr.Code = row.Field<string>(9);
            rr.Latin = row.Field<string>(10);
            rr.Name = row.Field<string>(11);
            rr.Shamsi = row.Field<string>(12);
            rr.YesterDay = decimal.Parse(row.Field<string>(13));
            rr.Last = decimal.Parse(row.Field<string>(14));
            //};

            return rr;
        }

        private static async Task Strategy()
        {
            var data = await GetData();

            // Build buy rule & sell rule based on various patterns
            var buyRule = Trady.Analysis.Rule.Create(c => c.IsFullStoBullishCross(14, 3, 3))
                .And(c => c.IsMacdOscBullish(12, 26, 9))
                .And(c => c.IsSmaOscBullish(10, 30))
                .And(c => c.IsAccumDistBullish());

            var sellRule = Trady.Analysis.Rule.Create(c => c.IsFullStoBearishCross(14, 3, 3))
                .Or(c => c.IsMacdBearishCross(12, 24, 9))
                .Or(c => c.IsSmaBearishCross(10, 30));

            // Create portfolio instance by using PortfolioBuilder
            var runner = new Trady.Analysis.Backtest.Builder()
                .Add(data,100)
                .Buy(buyRule)
                .Sell(sellRule)
                .BuyWithAllAvailableCash()
                .FlatExchangeFeeRate(0.001m)
                .Premium(1)
                .Build();

            // Start backtesting with the portfolio
            var result = await runner.RunAsync(1000);

            // Get backtest result for the portfolio
            Console.WriteLine(string.Format("Transaction count: {0:#.##}, P/L ratio: {1:0.##}%, Principal: {2:#}, Total: {3:#}",
                result.Transactions.Count(),
                result.TotalCorrectedProfitLossRatio,
                result.TotalPrincipal,
                result.TotalCorrectedBalance));

            var d = result.Transactions;

            foreach (var item in d)
            {
                Console.WriteLine(item.DateTime + " - " + item.Quantity);
            }


        }


            private static async Task Rules()
        {
            var data = await GetData();

            var rule = Trady.Analysis.Rule.Create(c => c.IsBullish()).And(k=>k.IsMacdOscBearish());

            // Use context here for caching indicator results
            using (var ctx = new AnalyzeContext(data))
            {
                var validObjects = new SimpleRuleExecutor(ctx, rule).Execute();

                foreach (var item in validObjects)
                {
                    Console.WriteLine(item.GetPersianDate() + " - "  + item.IsBullish());
                }
                
            }
        }

        private static async Task Indicator()
        {
            var data = await GetData();

            var RsiIndicator = data.Select(p=>p.Close).Sma(14).Diff(3);

            foreach (var item in RsiIndicator)
            {
                Console.WriteLine(item.Value + " - " + item.ToString());
            }

            Console.ReadKey();
        }

        private static async Task<IReadOnlyList<IOhlcv>> GetData()
        {
            ExcelImporter importer = new ExcelImporter("symbolData.xlsx");

            return await importer.ImportAsync("");
        }
    }

    internal class RowData
    {
        public decimal hight { get; set; }
        public decimal low { get; set; }
        public decimal close { get; set; }
        public decimal vol { get; set; }

        public string Namad { get; set; }
        public string Miladi { get; internal set; }
        public decimal Open { get; internal set; }
        public decimal ArzeshMoamele { get; internal set; }
        public decimal TedadMoamele { get; internal set; }
        public string Code { get; internal set; }
        public string Latin { get; internal set; }
        public string Name { get; internal set; }
        public string Shamsi { get; internal set; }
        public decimal YesterDay { get; internal set; }
        public decimal Last { get; internal set; }
    }
}
