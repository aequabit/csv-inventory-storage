using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSVInventoryStorage
{
    public interface ICommand
    {
        string commandName();
        int argCount();
        string usage();
        string action(object[] args);
    }
}
