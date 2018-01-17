using System;
using System.Collections.Generic;
using System.Linq;
using CSVInventoryStorage.Gui.Controls;
using CSVInventoryStorage.Extensions;

namespace CSVInventoryStorage.Gui.Forms
{
    public class ItemList : IForm
    {
        public string Name() => "Item List";

        public List<IControl> Controls()
        {
            var final = new List<IControl> {
                new Label(String.Format("   {{gray}}{0,15}{1,15}{2,15}{3,15}{4,15}{5,15}", "Description", "InventoryGroup", "InventoryId", "SerialNumber", "AddedBy", "AddedAt"))
            };

            var items = Inventory.Storage.GetInstance().GetItems();

            for (var i = 0; i < items.Count(); i++)
            {
                var item = items[i];

                var format = String.Format("{0,15}{1,15}{2,15}{3,15}{4,15}{5,15}",
                                           item.Description.Abbreviate(10),
                                           item.InventoryGroup.Abbreviate(12),
                                           item.InventoryId.Abbreviate(10),
                                           item.SerialNumber.Abbreviate(),
                                           item.AddedBy.Abbreviate(),
                                           item.AddedAt.ToString("dd.MM.yy").Abbreviate());

                if (Helper.ValidKey(i))
                    final.Add(
                        new Button(
                            Helper.DigitToKey(i),
                            format,
                            () => Renderer.Render(new ItemView(item))));
            }

            final.Add(new Button(ConsoleKey.A, "Add item", () =>
            {
                // TODO: fuck this
                Renderer.Render(new TextInput("Item creation", "Description: ", description =>
                {
                    Renderer.Render(new TextInput("Item creation", "InventoryGroup: ", inventoryGroup =>
                    {
                        Renderer.Render(new TextInput("Item creation", "InventoryId: ", inventoryId =>
                        {
                            Renderer.Render(new TextInput("Item creation", "SerialNumber: ", serialNumber =>
                            {
                                Inventory.Storage.GetInstance().AddItem(new Inventory.Item
                                {
                                    Description = description,
                                    InventoryGroup = inventoryGroup,
                                    InventoryId = inventoryId,
                                    SerialNumber = serialNumber,
                                    AddedBy = System.Security.Principal.WindowsIdentity.GetCurrent().Name,
                                    AddedAt = DateTime.Now
                                });
                            }));
                        }));
                    }));
                }));
                return false;
            }));
            final.Add(new Button(ConsoleKey.Backspace, "Back", () => true));

            return final;
        }

        public bool Proxy() => false;

        public bool KeyDown(ConsoleKeyInfo key) => false;
    }
}