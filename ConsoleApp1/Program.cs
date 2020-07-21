using MyTradyExtension;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trady.Analysis;
using Trady.Analysis.Extension;
using Trady.Core.Infrastructure;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            //Indicator();

            Rules();

            //Strategy();

            Console.ReadKey();
        }

        private static async Task Strategy()
        {
            var data = await GetData();

            // Build buy rule & sell rule based on various patterns
            var buyRule = Rule.Create(c => c.IsFullStoBullishCross(14, 3, 3))
                .And(c => c.IsMacdOscBullish(12, 26, 9))
                .And(c => c.IsSmaOscBullish(10, 30))
                .And(c => c.IsAccumDistBullish());

            var sellRule = Rule.Create(c => c.IsFullStoBearishCross(14, 3, 3))
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

            var rule = Rule.Create(c => c.IsBullish()).And(k=>k.IsMacdOscBearish());

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
}
