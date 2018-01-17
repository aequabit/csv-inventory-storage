using System;
using System.Collections.Generic;
using CSVInventoryStorage.Gui.Controls;

namespace CSVInventoryStorage.Gui.Forms
{
	public class ItemAdd : IForm
	{
		public string Name() => "Create Item";

		public List<IControl> Controls() => new List<IControl> {
            
		};

		public bool Proxy() => false;

		public bool KeyDown(ConsoleKeyInfo key) => false;
	}
}
