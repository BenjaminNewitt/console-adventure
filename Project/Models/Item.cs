using System.Collections.Generic;
using ConsoleAdventure.Project.Interfaces;

namespace ConsoleAdventure.Project.Models
{
  public class Item : IItem
  {

    public string Name { get; set; }
    public string Description { get; set; }
    public bool IsHidden { get; set; }
    public Item(string name, string description, bool isHidden)
    {
      Name = name;
      Description = description;
      IsHidden = isHidden;
    }
  }
}