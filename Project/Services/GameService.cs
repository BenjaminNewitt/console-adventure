using System.Collections.Generic;
using ConsoleAdventure.Project.Interfaces;
using ConsoleAdventure.Project.Models;

namespace ConsoleAdventure.Project
{
  public class GameService : IGameService
  {
    private IGame _game { get; set; }

    public List<string> Messages { get; set; }
    public GameService()
    {
      _game = new Game();
      Messages = new List<string>();
    }

    // NOTE Utility Messages
    // TODO move messages to utils
    #region
    public void PrintTitle()
    {
      Messages.Add(@"
 ▄█  ███▄▄▄▄            ███        ▄█    █▄       ▄████████       ▄█     █▄   ▄██████▄   ▄██████▄  ████████▄     ▄████████ 
███  ███▀▀▀██▄      ▀█████████▄   ███    ███     ███    ███      ███     ███ ███    ███ ███    ███ ███   ▀███   ███    ███ 
███▌ ███   ███         ▀███▀▀██   ███    ███     ███    █▀       ███     ███ ███    ███ ███    ███ ███    ███   ███    █▀  
███▌ ███   ███          ███   ▀  ▄███▄▄▄▄███▄▄  ▄███▄▄▄          ███     ███ ███    ███ ███    ███ ███    ███   ███        
███▌ ███   ███          ███     ▀▀███▀▀▀▀███▀  ▀▀███▀▀▀          ███     ███ ███    ███ ███    ███ ███    ███ ▀███████████ 
███  ███   ███          ███       ███    ███     ███    █▄       ███     ███ ███    ███ ███    ███ ███    ███          ███ 
███  ███   ███          ███       ███    ███     ███    ███      ███ ▄█▄ ███ ███    ███ ███    ███ ███   ▄███    ▄█    ███ 
█▀    ▀█   █▀          ▄████▀     ███    █▀      ██████████       ▀███▀███▀   ▀██████▀   ▀██████▀  ████████▀   ▄████████▀  
                                                                                                                           
");
    }
    public void PrintCredit()
    {
      Messages.Add(@"
       _                       _        o                                  
 _    (_| _ __  _    |_  \/   |_) _ __  |  _ __  o __    |\| _     o _|__|_
(_|   __|(_||||(/_   |_) /    |_)(/_| |_| (_|||| | | |   | |(/_\^/ |  |_ |_
");
    }

    // TODO Rewrite PrintIntroMessage
    public void PrintIntroMessage()
    {
      Messages.Add($"{_game.CurrentRoom.Description}");
    }

    public void PrintChooseAny()
    {
      Messages.Add("press any key to continue");
    }

    public void PrintInvalidInput()
    {
      Messages.Add("action not recognized");
    }

    public void PrintCurrentRoomDes()
    {
      Messages.Add($"{_game.CurrentRoom.Description}");
    }

    public void PrintQuitMessage()
    {
      Messages.Add(@"
▄█▄    ████▄ █▀▄▀█ ▄███▄       ███   ██   ▄█▄    █  █▀        ▄▄▄▄▄   ████▄ ████▄    ▄            
█▀ ▀▄  █   █ █ █ █ █▀   ▀      █  █  █ █  █▀ ▀▄  █▄█         █     ▀▄ █   █ █   █     █           
█   ▀  █   █ █ ▄ █ ██▄▄        █ ▀ ▄ █▄▄█ █   ▀  █▀▄       ▄  ▀▀▀▀▄   █   █ █   █ ██   █          
█▄  ▄▀ ▀████ █   █ █▄   ▄▀     █  ▄▀ █  █ █▄  ▄▀ █  █       ▀▄▄▄▄▀    ▀████ ▀████ █ █  █          
▀███▀           █  ▀███▀       ███      █ ▀███▀    █                              █  █ █ ██ ██ ██ 
               ▀                       █          ▀                               █   ██          
                                      ▀                                                           
");
    }

    #endregion

    // NOTE Actions
    #region
    public void Go(string direction)
    {
      if (direction != "north" && direction != "south" && direction != "east" && direction != "west")
      {
        PrintInvalidInput();
      }
      else
      {
        IRoom newRoom = _game.CurrentRoom.ChangeRoom(direction);
        if (newRoom == null)
        {
          Messages.Add("You can't travel in that direction");
        }
        else if (newRoom != null)
        {
          _game.CurrentRoom = newRoom;
          PrintCurrentRoomDes();
        }
      }
    }
    public void Help()
    {
      Messages.Add(@"
~~~^(|)^~~~^(|)^~~~^(|)^~~~^(|)^~~~^(|)^~~~^(|)^~~~{Actions}~~~^(|)^~~~^(|)^~~~^(|)^~~~^(|)^~~~^(|)^~~~^(|)^~~~

    go (direction)~~~~~~~~~~~~~~~ input your cardinal direction to move about the environment
    look          ~~~~~~~~~~~~~~~ check your surroundings
    inventory     ~~~~~~~~~~~~~~~ view your inventory
    take (item)   ~~~~~~~~~~~~~~~ attempt to move an item from the environment to add it to your inventory
    use (item)    ~~~~~~~~~~~~~~~ attempt to use an item from your inventory with the environment
    quit          ~~~~~~~~~~~~~~~ quit the game
      ");
    }

    public void Inventory()
    {
      Messages.Add("~~~~~{Inventory}~~~~~");
      int index = 0;
      foreach (Item item in _game.CurrentPlayer.Inventory)
      {
        Messages.Add($"{_game.CurrentPlayer.Inventory[index].Name}");
        index++;
      }
    }

    public void Look()
    {
      PrintCurrentRoomDes();
    }

    public void Quit()
    {
      PrintQuitMessage();
    }
    public void TakeItem(string itemName)
    {
      Item foundItem = _game.CurrentRoom.Items.Find(i => i.Name.ToLower() == itemName);
      if (foundItem == null)
      {
        PrintInvalidInput();
      }
      else
      {
        _game.CurrentRoom.RemoveItem(foundItem);
        _game.CurrentPlayer.AddItem(foundItem);
        Messages.Add($"You added the {itemName} to inventory");
      }
    }


    public void UseItem(string itemName)
    {
      Item foundItem = _game.CurrentPlayer.Inventory.Find(i => i.Name.ToLower() == itemName);
      if (foundItem == null)
      {
        PrintInvalidInput();
      }
      else
      {
        switch (itemName)
        {
          case "lantern":
            Messages.Add("You light the lantern.");
            Messages.Add("");
            checkItemUse(itemName);
            break;
          case "compass":
            Messages.Add("You bring out the compass.");
            break;
          case "map":
            Messages.Add("You unfurl the map.");
            break;
          default:
            PrintInvalidInput();
            break;
        }
      }
    }
    #endregion

    public void checkItemUse(string itemName)
    {
      bool isItemUsable = _game.CurrentRoom.CheckItemUse(itemName);
      if (isItemUsable == true)
      {

        switch (_game.CurrentRoom.Name)
        {
          case "Basement":
            if (itemName == "lantern")
            {
              updateDesc("With lantern in hand, you descend the stairway. The air is musty and thick, oppressing your senses. Only the lantern's dim light gives you some semblance of comfort. A single table lies against the western-most wall.");
              removeUsableItem(itemName);
              PrintCurrentRoomDes();
              updateDesc("Reigniting your lantern, you take in your surroundings once more. The table set up against the wall to the west remains as the only notable feature.");
            }
            else
            {
              Messages.Add($"The {itemName} has no use here");
            };
            break;
          default:
            Messages.Add("You can't use that item here");
            break;
        }
      }
      else
      {
        Messages.Add("That item can't be used here");
      }
    }
    public void updateDesc(string desc)
    {
      _game.CurrentRoom.ChangeDesc(desc);
    }

    public void removeUsableItem(string itemName)
    {
      _game.CurrentRoom.RemoveUsableItem(itemName);
    }

    // NOTE Unused Actions
    // TODO Remove if never used + remove from IGameService
    #region
    public void Reset()
    {
      throw new System.NotImplementedException();
    }

    public void Setup(string playerName)
    {
      throw new System.NotImplementedException();
    }
    #endregion
  }
}