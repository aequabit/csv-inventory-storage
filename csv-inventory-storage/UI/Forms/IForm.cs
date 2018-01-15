using System;
using System.Collections.Generic;

namespace CSVInventoryStorage.UI.Forms
{
    public interface IForm
    {
		string FormName();
        Dictionary<ConsoleKey, Option> Options();
    }
}
