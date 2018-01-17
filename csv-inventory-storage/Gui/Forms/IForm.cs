using System;
using System.Collections.Generic;
using CSVInventoryStorage.UI.Controls;

namespace CSVInventoryStorage.UI.Forms
{
    public interface IForm
    {
		string Name();
        List<IControl> Controls();
    }
}
