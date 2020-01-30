using System.Collections.Generic;
using ConsoleAdventure.Project.Interfaces;

namespace ConsoleAdventure.Project.Models
{
  public class Room : IRoom
  {

    public string Name { get; set; }
    public string Description { get; set; }
    public List<Item> Items { get; set; }
    public Dictionary<string, IRoom> Exits { get; set; }

    public Room(string name, string description)
    {
      Name = name;
      Description = description;
      Items = new List<Item>();
      Exits = new Dictionary<string, IRoom>();
    }

    public Item TakeItem(Room currentRoom, Item item)
    {
      currentRoom.Items.Remove(item);
      return item;
    }

    public Room ChangeRoom(Room currentRoom, Room enteringRoom)
    {
      if (currentRoom.Exits.ContainsValue(enteringRoom))
      {
        return enteringRoom;
      }
      return null;
    }
  }
}