using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    public interface IRepository
    {
        List<Namad> GetNamadList(int period);
        void SaveNamads(List<Namad> namads);
    }
}
