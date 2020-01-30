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
      //NOTE ROOMS
      #region
      // TODO Update descriptions
      Room Woods = new Room("The forest path", "The path into the forest is is pitch black. If you walk down it, you may get lost.");
      Room FrontPorch = new Room("Front porch", "As you walk up the stairs to the front door, the boards creak under your footsteps.");
      Room EastSide = new Room("Eastern side of the house", "Nothing of notable description.");
      Room WestSide = new Room("Western side of the house", "Nothing of notable description.");
      Room BackOfHouse = new Room("Back of the house", "As you come around to the back of the house, you notice a single window.");
      Room LivingRoom = new Room("House Interior", "To the west of you lies a stairwell leading downwards, and to the north is another door.");
      Room Den = new Room("Den", "Inside the second room of the house is a surprisingly well-preserved den. Inside the den is a writing desk with a single wingback chair. There is a faded painting on the western wall, the only item not in decent condition.");
      Room Basement = new Room("Basement", "With the stairs groaning under your weight with every footstep, you descend into the darkness. It is too dark to see anything.");

      Woods.Exits.Add("north", FrontPorch);
      FrontPorch.Exits.Add("north", LivingRoom);
      FrontPorch.Exits.Add("east", EastSide);
      FrontPorch.Exits.Add("west", WestSide);
      EastSide.Exits.Add("north", BackOfHouse);
      WestSide.Exits.Add("north", BackOfHouse);
      BackOfHouse.Exits.Add("east", EastSide);
      BackOfHouse.Exits.Add("west", WestSide);
      LivingRoom.Exits.Add("north", Den);
      LivingRoom.Exits.Add("south", FrontPorch);
      LivingRoom.Exits.Add("west", Basement);
      Den.Exits.Add("south", LivingRoom);

      #endregion

      //NOTE ITEMS
      #region 
      Item Map = new Item("Map", "While faded, the parchment appears to be a map of the woods. A dark red X on the map appears to mark the location of the house.");
      Item Compass = new Item("Compass", "A rusted, but usable, compass.");
      Item Lantern = new Item("Lantern", "Although the lantern appears the be very old, it seems to work properly once lit.");

      #endregion
    }
  }
}