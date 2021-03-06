using System.Collections.Generic;
using ConsoleAdventure.Project.Interfaces;

namespace ConsoleAdventure.Project.Models
{
  public class Room : IRoom
  {

    public string Name { get; set; }
    public string Description { get; set; }

    public bool IsHidden { get; set; }
    public List<Item> Items { get; set; }
    public Dictionary<string, IRoom> Exits { get; set; }

    public List<string> UsableItems { get; set; }

    public List<string> UsableInventoryItems { get; set; }

    public Room(string name, string description, bool isHidden)
    {
      Name = name;
      Description = description;
      Items = new List<Item>();
      Exits = new Dictionary<string, IRoom>();
      UsableItems = new List<string>();
      UsableInventoryItems = new List<string>();
      IsHidden = isHidden;
    }


  }
}