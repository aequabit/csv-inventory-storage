using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSVInventoryStorage
{
    public interface ICommand
    {
        string CommandName();
        int ArgCount();
        string Usage();
        string Action(object[] args);
    }
}
