using System;

namespace ConsoleAdventure.Project.Models
{
  class Message
  {
    public string Body { get; set; }

    public ConsoleColor Color { get; set; }

    public void Print()
    {
      var defaultColor = Console.ForegroundColor;
      Console.ForegroundColor = Color;
      Console.WriteLine(Body);
      Console.ForegroundColor = defaultColor;
    }

    public Message(string body)
    {
      Body = body;
      Color = Console.ForegroundColor;
    }

    public Message(string body, ConsoleColor color)
    {
      Body = body;
      Color = color;
    }
  }
}