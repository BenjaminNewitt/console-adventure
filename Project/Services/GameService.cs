using System.Collections.Generic;
using ConsoleAdventure.Project.Interfaces;
using ConsoleAdventure.Project.Models;
using System;

namespace ConsoleAdventure.Project
{
  class GameService : IGameService
  {
    private IGame _game { get; set; }
    public List<Message> Messages { get; set; } = new List<Message>();

    public GameService()
    {
      _game = new Game();
    }

    // NOTE Utility Messages
    #region
    public void PrintTitle()
    {
      Messages.Add(new Message(MessageUtils.Title, ConsoleColor.DarkGreen
      ));
    }
    public void PrintCredit()
    {
      Messages.Add(new Message(MessageUtils.Credits));
    }

    public void PrintCongrats()
    {
      Messages.Add(new Message(MessageUtils.Congrats, ConsoleColor.Yellow));
    }
    public void PrintGameOver()
    {
      Messages.Add(new Message(MessageUtils.GameOver, ConsoleColor.DarkRed));
    }

    public void PrintIntroMessage()
    {
      Messages.Add(new Message($"{_game.CurrentRoom.Description}"));
    }

    public void PrintChooseAny()
    {
      Messages.Add(new Message("press any key to continue"));
    }

    public void PrintInvalidInput()
    {
      Messages.Add(new Message("action not recognized"));
    }

    public void PrintCurrentRoomDes()
    {
      Messages.Add(new Message($"{_game.CurrentRoom.Description}"));
    }

    public void PrintQuitMessage()
    {
      Messages.Add(new Message(MessageUtils.QuitGame, ConsoleColor.DarkGray));
    }

    public void PrintRuleOne()
    {
      Messages.Add(new Message(MessageUtils.RulerOne));
    }

    public void PrintHelp()
    {
      Messages.Add(new Message(MessageUtils.Help));
    }

    #endregion

    // NOTE Actions
    #region
    public bool Go(string direction)
    {
      bool IsEndOfGame = false;
      if (direction != "north" && direction != "south" && direction != "east" && direction != "west")
      {
        // if not a direction
        PrintInvalidInput();
        return true;
      }
      else
      {
        IRoom newRoom = _game.CurrentRoom.ChangeRoom(direction);
        if (newRoom == null)
        {
          if (_game.CurrentRoom.Name == "The forest path" && direction == "south")
          {
            // sets IsEndOfGame if trying to exit
            Endgame();
            IsEndOfGame = true;
          }
          else
          {
            // if no room available, tells player they can't go that way
            Messages.Add(new Message("You can't travel in that direction"));
          }
        }
        else if (newRoom != null)
        {
          // if the player is allowed to travel there
          if (newRoom.IsHidden == false)
          {
            _game.CurrentRoom = newRoom;
            PrintCurrentRoomDes();
          }
          else
          {
            // if they're not yet allowed
            Messages.Add(new Message("You can't go that way"));
          }
        }
      }
      if (IsEndOfGame == true)
      {
        return false;
      }
      else
      {
        return true;
      }
    }
    public void Help()
    {
      Messages.Add(new Message(@"
~~~^(|)^~~~^(|)^~~~^(|)^~~~^(|)^~~~^(|)^~~~^(|)^~~~^(|)^~~~^(|)^~~~^(|)^~~~^(|)^~~~{Actions}~~~^(|)^~~~^(|)^~~~^(|)^~~~^(|)^~~~^(|)^~~~^(|)^~~~^(|)^~~~^(|)^~~~^(|)^~~~~

         go (direction) ~~~~~~~~~~~~~~~ input your cardinal direction to move about the environment
         look           ~~~~~~~~~~~~~~~ check your surroundings
         inventory      ~~~~~~~~~~~~~~~ view your inventory
         take (item)    ~~~~~~~~~~~~~~~ attempt to move an item from the environment to add it to your inventory
         use (item)     ~~~~~~~~~~~~~~~ attempt to use an item from your inventory with the environment
         quit           ~~~~~~~~~~~~~~~ quit the game
      "));
    }

    public void Inventory()
    {
      Messages.Add(new Message(@"
~~^(|)^~~~^(|)^~~~^(|)^~~~^(|)^~~~^(|)^~~~^(|)^~~~^(|)^~~~^(|)^~~~^(|)^~~~^(|)^~~~{Inventory}~~~^(|)^~~~^(|)^~~~^(|)^~~~^(|)^~~~^(|)^~~~^(|)^~~~^(|)^~~~^(|)^~~~^(|)^~~~"));
      int index = 0;
      foreach (Item item in _game.CurrentPlayer.Inventory)
      {
        Messages.Add(new Message($"          {_game.CurrentPlayer.Inventory[index].Name} ~~~~~ {_game.CurrentPlayer.Inventory[index].Description}"));
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
        if (itemName == "painting")
        {
          UseItem(itemName);
        }
        else
        {
          PrintInvalidInput();
        }
      }
      else
      {
        if (foundItem.IsHidden == false)
        {
          _game.CurrentRoom.RemoveItem(foundItem);
          _game.CurrentPlayer.AddItem(foundItem);
          Messages.Add(new Message($"You added the {itemName} to your inventory"));
          OnTaken(itemName);
        }
        else
        {
          PrintInvalidInput();
        }
      }
    }

    public void FilterItemUse(string itemName)
    {
      // Check player inventory for item
      Item foundItem = _game.CurrentPlayer.Inventory.Find(i => i.Name.ToLower() == itemName);
      if (foundItem == null)
      {
        // if not found in inventory, check if usable item is in the room
        string usableItem = _game.CurrentRoom.UsableItems.Find(i => i == itemName);
        if (usableItem == null)
        {
          // if neither true, print invalid input
          PrintInvalidInput();
        }
        else
        {
          UseItem(usableItem);
          // If item exists in room, pass it to UseItem
        }
      }
      else
      {
        // On use inventory item
        switch (itemName)
        {
          case "lantern":
            Messages.Add(new Message("You light the lantern."));
            Messages.Add(new Message(""));
            UseInventoryItem(itemName);
            break;
          case "compass":
            Messages.Add(new Message("You bring out the compass."));
            Messages.Add(new Message(""));
            UseInventoryItem(itemName);
            break;
          case "map":
            Messages.Add(new Message("You unfurl the map."));
            Messages.Add(new Message(""));
            UseInventoryItem(itemName);
            break;
          case "paper":
            Messages.Add(new Message("You unfold the note."));
            Messages.Add(new Message(""));
            break;
          default:
            PrintInvalidInput();
            break;
        }
      }
    }
    #endregion

    // NOTE ITEM METHODS
    #region
    public void UseItem(string itemName)
    {
      if (_game.CurrentRoom.UsableItems.Contains(itemName))
      {

        // Use item that exists in the room
        switch (itemName)
        {
          // checks if a specific item is used in specific situations
          case "painting":
            Messages.Add(new Message("Removing the painting from the wall reveals a very old map, one that looks like it could fall apart at any moment."));
            RevealHiddenItem("Map");
            RemoveUsableItem(itemName);
            UpdateDesc("Inside the second room of the house is a surprisingly well-preserved den. Inside the den is a writing desk with a single wingback chair facing the northern wall, where a single window sits. A fragile map hangs on the wall.");
            break;
          case "door":
            Messages.Add(new Message("With some effort, you force the door open and enter the room."));
            Messages.Add(new Message(""));
            UpdateDesc("Inside of the house lies an empty room.\nTo the west of you lies a stairwell leading downwards, and to the north lies the den.");
            RemoveUsableItem(itemName);
            ChangeRoomOnUnlock();
            break;
          default:
            break;
        }
      }
      else
      {
        PrintInvalidInput();
      }
    }

    public void UseInventoryItem(string itemName)
    {
      // check if item being used from the player's inventory can be used in the current room
      bool isItemUsable = _game.CurrentRoom.CheckItemUse(itemName);
      if (isItemUsable == true)
      {

        switch (_game.CurrentRoom.Name)
        {
          case "Basement":
            if (itemName == "lantern")
            {
              UpdateDesc("With lantern in hand, you descend the stairway. The air is musty and thick, oppressing your senses. Only the lantern's dim light gives you some semblance of comfort. A single table lies against the western-most wall. A compass lays on top of the table.");
              RemoveUsableItem(itemName);
              RevealHiddenItem("Compass");
              PrintCurrentRoomDes();
              UpdateDesc("Reigniting your lantern, you take in your surroundings once more. The table set up against the wall remains as the only notable feature. A compass lays on top of the table.");
            }
            else
            {
              Messages.Add(new Message($"The {itemName} has no use here."));
            };
            break;
          default:
            Messages.Add(new Message($"bringing out the {itemName} does nothing here."));
            break;
        }
      }
      else
      {
        Messages.Add(new Message($"Using the {itemName} has no affect."));
      }
    }

    public void OnTaken(string itemName)
    {
      // if the item is taken, update the description of the room
      switch (itemName)
      {
        case "map":
          UpdateDesc("Inside the second room of the house is a surprisingly well-preserved den. Inside the den is a writing desk with a single wingback chair facing the northern wall, where a single window sits.");
          break;
        case "compass":
          UpdateDesc("Reigniting your lantern, you take in your surroundings once more. The table set up against the wall remains as the only notable feature.");
          break;
        case "lantern":
          UpdateDesc("As you come around to the back of the house, you notice a single shuttered window.");
          break;
        case "paper":
          UpdateDesc("The boards creak under your footsteps as you step onto the porch. To the north, the front door of the house lies open.");
          break;
        default:
          break;
      }
    }
    #endregion

    // NOTE ROOM METHODS
    #region
    public void UpdateDesc(string desc)
    {
      _game.CurrentRoom.ChangeDesc(desc);
    }

    public void RemoveUsableItem(string itemName)
    {
      _game.CurrentRoom.RemoveUsableItem(itemName);
    }
    public void RevealHiddenItem(string itemName)
    {
      _game.CurrentRoom.RevealHiddenItem(itemName);
    }
    public void ChangeRoomOnUnlock()
    {
      if (_game.CurrentRoom.Name == "House Interior")
      {
        IRoom newRoom = _game.CurrentRoom.ChangeRoom("north");
        _game.CurrentRoom = newRoom;
        SwitchIsHidden();
      }
    }
    public void SwitchIsHidden()
    {
      _game.CurrentRoom.SwitchIsHidden();
      PrintCurrentRoomDes();
    }
    #endregion


    // NOTE WIN/LOSE CONDITION
    #region
    public void Endgame()
    {
      int index = 0;
      int score = 0;
      foreach (Item item in _game.CurrentPlayer.Inventory)
      {
        switch (_game.CurrentPlayer.Inventory[index].Name)
        {
          case "Compass":
          case "Lantern":
          case "Map":
            score++;
            break;
          default:
            break;
        }
        index++;
      }
      if (score == 3)
      {
        YouWin();
      }
      else
      {
        YouLose();
      }
    }

    public void YouWin()
    {
      PrintCongrats();
    }

    public void YouLose()
    {
      PrintGameOver();
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