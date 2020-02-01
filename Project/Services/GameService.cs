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
            Messages.Add("You used the lantern");
            break;
          case "compass":
            Messages.Add("You used the compass");
            break;
          case "map":
            Messages.Add("You used the map");
            break;
          default:
            PrintInvalidInput();
            break;
        }
      }
    }
    #endregion

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