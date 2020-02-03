using System.Collections.Generic;
using ConsoleAdventure.Project.Models;

namespace ConsoleAdventure.Project.Interfaces
{
  public interface IRoom
  {
    string Name { get; set; }
    string Description { get; set; }
    List<Item> Items { get; set; }
    Dictionary<string, IRoom> Exits { get; set; }

    bool IsHidden { get; set; }

    List<string> UsableItems { get; set; }

    List<string> UsableInventoryItems { get; set; }


    public IRoom ChangeRoom(string direction)
    {
      if (Exits.ContainsKey(direction))
      {
        IRoom newRoom = Exits[direction];
        return newRoom;
      }
      return null;
    }

    public bool CheckItemUse(string itemName)
    {
      if (UsableInventoryItems.Contains(itemName.ToLower()))
      {
        return true;
      }
      return false;
    }

    public void RemoveUsableItem(string itemName)
    {
      UsableItems.Remove(itemName);
    }
    public void RemoveItem(Item foundItem)
    {
      Items.Remove(foundItem);
    }

    public void ChangeDesc(string newDesc)
    {
      Description = newDesc;
    }

    public void RevealHiddenItem(string itemName)
    {
      Item revealedItem = Items.Find(i => i.Name == itemName);

      revealedItem.IsHidden = false;
    }
  }
}
