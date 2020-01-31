using System;
using System.Collections.Generic;
using ConsoleAdventure.Project.Interfaces;
using ConsoleAdventure.Project.Models;

namespace ConsoleAdventure.Project.Controllers
{

  public class GameController : IGameController
  {
    private GameService _gameService = new GameService();

    private bool _playing = true;

    //NOTE Makes sure everything is called to finish Setup and Starts the Game loop
    public void Run()
    {
      _gameService.PrintTitle();
      _gameService.PrintCredit();
      _gameService.PrintChooseAny();
      Print();
      Console.ReadKey();
      _gameService.PrintIntroMessage();
      while (_playing)
      {
        Console.Clear();
        Print();
        Console.WriteLine("");
        GetUserInput();
      }
      Console.Clear();
      Print();
      Console.ReadKey();
      Console.Clear();
    }

    //NOTE Gets the user input, calls the appropriate command, and passes on the option if needed.
    public void GetUserInput()
    {
      Console.Write("What will you do? ");
      string input = Console.ReadLine().ToLower() + " ";
      string command = input.Substring(0, input.IndexOf(" "));
      string option = input.Substring(input.IndexOf(" ") + 1).Trim();
      //NOTE this will take the user input and parse it into a command and option.
      //IE: take silver key => command = "take" option = "silver key"

      switch (command)
      {
        case "quit":
          _gameService.Quit();
          _playing = false;
          break;
        case "help":
          _gameService.Help();
          break;
        case "go":
        case "walk":
        case "run":
        case "travel":
          _gameService.Go(option);
          break;
        case "look":
          _gameService.Look();
          break;
        case "inventory":
          _gameService.Inventory();
          break;
        case "take":
          _gameService.TakeItem(option);
          break;
        default:
          _gameService.PrintInvalidInput();
          break;
      }
    }

    //NOTE this should print your messages for the game.
    private void Print()
    {
      foreach (string message in _gameService.Messages)
      {
        Console.WriteLine(message);
      }
      _gameService.Messages.Clear();
    }

  }
}