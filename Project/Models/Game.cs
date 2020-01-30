using ConsoleAdventure.Project.Interfaces;

namespace ConsoleAdventure.Project.Models
{
  public class Game : IGame
  {
    public IRoom CurrentRoom { get; set; }
    public IPlayer CurrentPlayer { get; set; }

    //NOTE Make yo rooms here...
    public void Setup()
    {
      Room Woods = new Room("The forest path", "The path into the forest is is pitch black. If you walk down it, you may get lost.");
      Room FrontPorch = new Room("Front porch", "As you walk up the stairs to the front door, the boards creak under your footsteps.");
      Room EastSide = new Room("Eastern side of the house", "Nothing of notable description.");
      Room WestSide = new Room("Western side of the house", "Nothing of notable description.");
      Room BackOfHouse = new Room("Back of the house", "As you come around to the back of the house, you notice a single window.");
      Room LivingRoom = new Room("House Interior", "To the west of you lies a stairwell leading downwards, and to the north is another door.");
      Room Den = new Room("Den", "Inside the second room of the house is a surprisingly well-preserved den. Inside the den is a writing desk with a single wingback chair. There is a faded painting on the western wall, the only item not in decent condition.");
      Room Basement = new Room("Basement", "With the stairs groaning under your weight with every footstep, you descend into the darkness. It is too dark to see anything.");


    }
  }
}