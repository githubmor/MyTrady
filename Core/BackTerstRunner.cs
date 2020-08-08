using System;
using System.Collections.Generic;

namespace Core
{
    internal class BackTerstRunner
    {
        private List<Strategy> strategies;

        public BackTerstRunner(List<Strategy> strategies)
        {
            this.strategies = strategies;
        }

        internal BackTestResult GetTestResult()
        {
            throw new NotImplementedException();
        }
    }
}