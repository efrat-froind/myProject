using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlApi;

public interface IClient
{
    int Create(BO.Client item);
    BO.Client? Read(int id);
    List<BO.Client?> ReadAll(Func<BO.Client, bool>? filter = null);
    void Update(BO.Client item);
    void Delete(int id);
    bool IsExist(int id);
}
