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

    #endregion
    public void Go(string direction)
    {
      _game.CurrentRoom = _game.CurrentRoom.ChangeRoom(direction);
      PrintCurrentRoomDes();
    }
    public void Help()
    {
      throw new System.NotImplementedException();
    }

    public void Inventory()
    {
      int index = 0;
      foreach (Item item in _game.CurrentPlayer.Inventory)
      {
        Messages.Add($"{_game.CurrentPlayer.Inventory[index]}");
        index++;
      }
    }

    public void Look()
    {
      PrintCurrentRoomDes();
    }

    public void Quit()
    {
      throw new System.NotImplementedException();
    }
    ///<summary>
    ///Restarts the game 
    ///</summary>
    public void Reset()
    {
      throw new System.NotImplementedException();
    }

    public void Setup(string playerName)
    {
      throw new System.NotImplementedException();
    }
    ///<summary>When taking an item be sure the item is in the current room before adding it to the player inventory, Also don't forget to remove the item from the room it was picked up in</summary>
    public void TakeItem(string itemName)
    {
      throw new System.NotImplementedException();
    }
    ///<summary>
    ///No need to Pass a room since Items can only be used in the CurrentRoom
    ///Make sure you validate the item is in the room or player inventory before
    ///being able to use the item
    ///</summary>
    public void UseItem(string itemName)
    {
      throw new System.NotImplementedException();
    }
  }
}