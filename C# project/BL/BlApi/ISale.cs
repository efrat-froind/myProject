using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlApi;

public interface ISale
{
    int Create(BO.Sale item);
    BO.Sale? Read(int id);
    List<BO.Sale?> ReadAll(Func<BO.Sale, bool>? filter = null);
    void Update(BO.Sale item);
    void Delete(int id);
}
