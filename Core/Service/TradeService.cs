using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Service
{
    public class TradeService : ITradeService
    {
        IRepository _repository;
        public TradeService(IRepository repository)
        {
            _repository = repository;
        }
        public List<Namad> GetNamads(int period)
        {
            return _repository.GetNamadList(period);
        }

        public void SaveNamads(List<Namad> namads)
        {
            _repository.SaveNamads(namads);
        }
    }
}
