using System;
using System.Collections.Generic;

namespace Core
{
    internal class BackTerstRunner
    {
        private List<IStrategy> strategies;

        public BackTerstRunner(List<IStrategy> strategies)
        {
            this.strategies = strategies;
        }

        internal BackTestResult GetTestResult()
        {
            throw new NotImplementedException();
        }
    }
}