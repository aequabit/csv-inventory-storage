using System;
using System.Collections.Generic;
using CSVInventoryStorage.Gui.Controls;

namespace CSVInventoryStorage.Gui.Forms
{
    public interface IForm
    {
		string Name();
        List<IControl> Controls();
        bool Proxy();
        bool KeyDown(ConsoleKeyInfo key);
    }
}
