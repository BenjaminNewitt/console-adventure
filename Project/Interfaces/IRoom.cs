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


    public IRoom ChangeRoom(string direction)
    {
      if (Exits.ContainsKey(direction))
      {
        IRoom newRoom = Exits[direction];
        return newRoom;
      }
      return null;
    }

    public void RemoveItem(Item foundItem)
    {
      Items.Remove(foundItem);
    }

    public void ChangeDesc(string newDesc)
    {
      Description = newDesc;
    }
  }
}
